using POS_Shop.Models;
using POS_Shop.Models.LoanModelsV1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreensV1
{
    /// <summary>
    /// Standalone form to manually post a Loan (debit) or Advance (credit)
    /// against any customer — without needing an invoice.
    ///
    /// Use cases:
    ///   • Customer owes money from before this software was installed
    ///   • Customer gave advance cash outside the billing screen
    ///   • Any offline / walk-in transaction that needs recording
    /// </summary>
    public partial class ManualLedgerEntryForm : Form
    {
        // ─── Fields ──────────────────────────────────────────────────────────
        private List<CustomerSearchResult> _customers = new List<CustomerSearchResult>();
        private int? _selectedCustomerId;
        private string _selectedCustomerName;
        private decimal _selectedCustomerBalance;
        private bool _isSaving;
        private Timer _searchDebounce;

        public bool EntrySaved { get; private set; }

        // ─── Constructor ─────────────────────────────────────────────────────
        public ManualLedgerEntryForm()
        {
            InitializeComponent();

            this.Load += ManualLedgerEntryForm_Load;
        }

        /// <summary>Open pre-selected for a known customer (e.g. from AllCustomerBalancesForm).</summary>
        public ManualLedgerEntryForm(int customerId, string customerName, decimal currentBalance, string note)
            : this()
        {
            _selectedCustomerId = customerId;
            _selectedCustomerName = customerName;
            _selectedCustomerBalance = currentBalance;
          txtNote.Text = note;
        }

        private void InitializeCustomComponents()
        {
            // Fix form layout
            this.pnlBody.Dock = DockStyle.Fill;
            this.pnlBody.AutoScroll = true;

            // Fix suggestions panel - will be positioned dynamically
            this.pnlCustomerSuggestions.Visible = false;
            this.pnlCustomerSuggestions.BringToFront();

            // Fix listbox in suggestions
            this.lbSuggestions.Dock = DockStyle.Fill;

            // Initialize timer properly
            _searchDebounce = new Timer { Interval = 300 };
            _searchDebounce.Tick += SearchDebounce_Tick;

            // Wire up resize event
          //  this.Resize += ManualLedgerEntryForm_Resize;
        }

        // ─── Load ─────────────────────────────────────────────────────────────
        private void ManualLedgerEntryForm_Load(object sender, EventArgs e)
        {
            InitializeCustomComponents();
           

            SetupEntryTypePanel();
            SetupPaymentMethodDropdown();
            SetupValidation();

            if (_selectedCustomerId.HasValue)
            {
                int cid = _selectedCustomerId.Value;
                // Pre-fill customer
                txtCustomerSearch.Text = _selectedCustomerName;
                txtCustomerSearch.Enabled = false;
                txtCustomerSearch.ReadOnly = true;
                txtCustomerSearch.BackColor = Color.FromArgb(240, 240, 240);
             //   _selectedCustomerId = _selectedCustomerId.Value;
                ShowSelectedCustomer(cid, txtCustomerSearch.Text, _selectedCustomerBalance);
            }
            else
            {
                txtCustomerSearch.Focus();
            }

            UpdateSaveButtonState();
        }

        private void ManualLedgerEntryForm_Resize(object sender, EventArgs e)
        {
            // Reposition suggestions panel if visible
            if (pnlCustomerSuggestions.Visible)
            {
                PositionSuggestionsPanel();
            }
        }

        private void PositionSuggestionsPanel()
        {
            pnlCustomerSuggestions.Location = new Point(
                txtCustomerSearch.Left,
                txtCustomerSearch.Bottom + 2);
            pnlCustomerSuggestions.Width = txtCustomerSearch.Width;
        }

        // ─── Setup ────────────────────────────────────────────────────────────
        private void SetupEntryTypePanel()
        {
            // Wire radio buttons
            rbLoan.CheckedChanged += RbLoan_CheckedChanged;
            rbAdvance.CheckedChanged += RbAdvance_CheckedChanged;
            rbLoan.Checked = true;
        }

        private void RbLoan_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLoan.Checked) OnEntryTypeChanged();
        }

        private void RbAdvance_CheckedChanged(object sender, EventArgs e)
        {
            if (rbAdvance.Checked) OnEntryTypeChanged();
        }

        private void OnEntryTypeChanged()
        {
            if (rbLoan.Checked)
            {
                pnlLoanIndicator.BackColor = Color.FromArgb(192, 0, 0);
                lblEntryTypeDesc.Text = "Customer owes you money — adds to their outstanding loan balance.";
                lblEntryTypeDesc.ForeColor = Color.FromArgb(160, 0, 0);
                lblAmountLabel.Text = "Loan Amount (PKR):";
                pnlPaymentMethod.Visible = false; // Loan has no payment method
            }
            else
            {
                pnlLoanIndicator.BackColor = Color.FromArgb(0, 102, 204);
                lblEntryTypeDesc.Text = "Customer is depositing money in advance — adds credit to their account.";
                lblEntryTypeDesc.ForeColor = Color.FromArgb(0, 80, 160);
                lblAmountLabel.Text = "Advance Amount (PKR):";
                pnlPaymentMethod.Visible = true;
            }

            UpdatePreview();
            UpdateSaveButtonState();
        }

        private void SetupPaymentMethodDropdown()
        {
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add("Cash");
            cmbPaymentMethod.Items.Add("Bank Transfer");
            cmbPaymentMethod.Items.Add("Cheque");
            cmbPaymentMethod.Items.Add("JazzCash");
            cmbPaymentMethod.Items.Add("EasyPaisa");
            cmbPaymentMethod.SelectedIndex = 0;
            cmbPaymentMethod.SelectedIndexChanged += CmbPaymentMethod_SelectedIndexChanged;
        }

        private void CmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleReferenceField();
        }

        private void ToggleReferenceField()
        {
            bool show = cmbPaymentMethod.SelectedItem?.ToString() == "Cheque"
                     || cmbPaymentMethod.SelectedItem?.ToString() == "Bank Transfer";
            lblReferenceNo.Visible = show;
            txtReferenceNo.Visible = show;
        }

        private void SetupValidation()
        {
            txtAmount.KeyPress += TxtAmount_KeyPress;
            txtAmount.TextChanged += TxtAmount_TextChanged;
            txtNote.TextChanged += TxtNote_TextChanged;
        }

        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
                e.Handled = true;
            if (e.KeyChar == '.' && txtAmount.Text.Contains('.'))
                e.Handled = true;
        }

        private void TxtAmount_TextChanged(object sender, EventArgs e)
        {
            UpdatePreview();
            UpdateSaveButtonState();
        }

        private void TxtNote_TextChanged(object sender, EventArgs e)
        {
            UpdateSaveButtonState();
        }

        // ─── Customer Search ─────────────────────────────────────────────────
        private void txtCustomerSearch_TextChanged(object sender, EventArgs e)
        {
            // If customer is pre-selected and field is read-only, don't search
            if (txtCustomerSearch.ReadOnly) return;

            // Clear selected customer when search text changes
            _selectedCustomerId = null;
            _selectedCustomerName = null;
            pnlSelectedCustomer.Visible = false;

            // Hide suggestions panel
            pnlCustomerSuggestions.Visible = false;

            UpdateSaveButtonState();

            if (txtCustomerSearch.Text.Trim().Length < 2)
            {
                _searchDebounce.Stop();
                return;
            }

            _searchDebounce.Stop();
            _searchDebounce.Start();
        }

        private async void SearchDebounce_Tick(object sender, EventArgs e)
        {
            _searchDebounce.Stop();

            // Don't search if customer is already selected and field is read-only
            if (txtCustomerSearch.ReadOnly) return;

            await SearchCustomersAsync(txtCustomerSearch.Text.Trim());
        }

        private async Task SearchCustomersAsync(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText)) return;

            try
            {
                //using (var context = new POSDbContext())
                //{
                //    var results = await context.Customers
                //        .Where(c => c.CustomerName.Contains(searchText)
                //                 || c.ContactNo.Contains(searchText))
                //        .OrderBy(c => c.CustomerName)
                //        .Take(10)
                //        .Select(c => new CustomerSearchResult
                //        {
                //            Id = c.Id,
                //            CustomerName = c.CustomerName,
                //            ContactNo = c.ContactNo
                //        })
                //        .ToListAsync();

                //    // Fetch balances for the results
                //    var ids = results.Select(r => r.Id).ToList();
                //    var lastEntries = await context.CustomerLedgerEntries
                //        .Where(e2 => ids.Contains(e2.CustomerId))
                //        .GroupBy(e2 => e2.CustomerId)
                //        .Select(g => new
                //        {
                //            CustomerId = g.Key,
                //            Balance = g.OrderByDescending(x => x.Id)
                //                       .Select(x => x.Balance)
                //                       .FirstOrDefault()
                //        })
                //        .ToListAsync();

                //    foreach (var r in results)
                //    {
                //        var entry = lastEntries.FirstOrDefault(x => x.CustomerId == r.Id);
                //        r.CurrentBalance = entry?.Balance ?? 0;
                //    }

                //    _customers = results;

                //    // Only show suggestions if no customer is selected
                //    if (!_selectedCustomerId.HasValue)
                //    {
                //        BindSuggestions();
                //    }
                //}


                using (var context = new POSDbContext())
                {
                    // Token split - compatible with all .NET versions
                    var tokens = searchText
                        .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim())
                        .Where(t => t.Length > 0)
                        .ToList();

                    // Build base query with token matching
                    IQueryable<Customer> query = context.Customers;

                    foreach (var token in tokens)
                    {
                        var t = token; // capture for closure
                        query = query.Where(c =>
                            c.CustomerName.Contains(t) ||
                            c.ContactNo.Contains(t));
                    }

                    // Step 1: Fetch matched customers (max 10)
                    var results = await query
                        .OrderBy(c => c.CustomerName)
                        .Take(10)
                        .Select(c => new CustomerSearchResult
                        {
                            Id = c.Id,
                            CustomerName = c.CustomerName,
                            ContactNo = c.ContactNo
                        })
                        .ToListAsync();

                    // Step 2: Fetch balances in ONE query using GroupBy (same as your original)
                    var ids = results.Select(r => r.Id).ToList();

                    var lastEntries = await context.CustomerLedgerEntries
                        .Where(e => ids.Contains(e.CustomerId))
                        .GroupBy(e => e.CustomerId)
                        .Select(g => new
                        {
                            CustomerId = g.Key,
                            Balance = g.OrderByDescending(x => x.Id)
                                          .Select(x => x.Balance)
                                          .FirstOrDefault()
                        })
                        .ToListAsync();

                    // Step 3: Map balances in-memory (O(n) with dictionary instead of O(n²) with FirstOrDefault)
                    var balanceMap = lastEntries.ToDictionary(x => x.CustomerId, x => x.Balance);

                    foreach (var r in results)
                    {
                        r.CurrentBalance = balanceMap.TryGetValue(r.Id, out var balance) ? balance : 0;
                    }

                    _customers = results;

                    if (!_selectedCustomerId.HasValue)
                    {
                        BindSuggestions();
                    }
                }
            }
            catch
            {
                // silently ignore search errors
            }
        }

        private void BindSuggestions()
        {
            lbSuggestions.Items.Clear();

            if (!_customers.Any())
            {
                pnlCustomerSuggestions.Visible = false;
                return;
            }

            foreach (var c in _customers)
            {
                string balStr = c.CurrentBalance > 0 ? $" 🔴 PKR {c.CurrentBalance:N0} Loan"
                              : c.CurrentBalance < 0 ? $" 🔵 PKR {Math.Abs(c.CurrentBalance):N0} Advance"
                              : " ✅ Clear";
                lbSuggestions.Items.Add($"{c.CustomerName}  |  {c.ContactNo}  {balStr}");
            }

            // Position suggestions panel
            PositionSuggestionsPanel();
            pnlCustomerSuggestions.Height = Math.Min(200, lbSuggestions.Items.Count * 25 + 5);
            pnlCustomerSuggestions.Visible = true;
            pnlCustomerSuggestions.BringToFront();
        }

        private void lbSuggestions_Click(object sender, EventArgs e)
        {
            if (lbSuggestions.SelectedIndex >= 0)
                SelectSuggestion(lbSuggestions.SelectedIndex);
        }

        private void lbSuggestions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lbSuggestions.IndexFromPoint(e.Location);
            if (index >= 0)
                SelectSuggestion(index);
        }

        private void lbSuggestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbSuggestions.SelectedIndex >= 0)
            {
                SelectSuggestion(lbSuggestions.SelectedIndex);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                pnlCustomerSuggestions.Visible = false;
                txtCustomerSearch.Focus();
            }
        }

        private void SelectSuggestion(int index)
        {
            if (index < 0 || index >= _customers.Count) return;

            var cust = _customers[index];

            // Hide suggestions panel immediately
            pnlCustomerSuggestions.Visible = false;

            // Update text box and show selected customer
            txtCustomerSearch.Text = cust.CustomerName;
            ShowSelectedCustomer(cust.Id, cust.CustomerName, cust.CurrentBalance);
        }

        private void ShowSelectedCustomer(int id, string name, decimal balance)
        {
            _selectedCustomerId = id;
            _selectedCustomerName = name;
            _selectedCustomerBalance = balance;
            txtAmount.Text=Math.Abs(balance).ToString();
            lblSelectedName.Text = name;

            if (balance > 0)
            {
                
                rbAdvance.Checked = true;
                rbLoan.Checked = false;
                lblSelectedBalance.Text = $"Outstanding Loan: PKR {balance:N2}";
                lblSelectedBalance.ForeColor = Color.FromArgb(192, 0, 0);
               
            }
            else if (balance < 0)
            {
                rbAdvance.Checked = false;
                rbLoan.Checked = true;
                lblSelectedBalance.Text = $"Advance Credit: PKR {Math.Abs(balance):N2}";
                lblSelectedBalance.ForeColor = Color.FromArgb(0, 102, 204);
            }
            else
            {
                lblSelectedBalance.Text = "No outstanding balance";
                lblSelectedBalance.ForeColor = Color.Gray;
            }

            pnlSelectedCustomer.Visible = true;

            // Make sure suggestions are hidden
            pnlCustomerSuggestions.Visible = false;

            UpdatePreview();
            UpdateSaveButtonState();

            txtAmount.Focus();
        }

        // ─── Preview ──────────────────────────────────────────────────────────
        private void UpdatePreview()
        {
            if (_selectedCustomerId == null ||
                !decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                lblPreview.Text = "—";
                lblPreview.ForeColor = Color.Gray;
                return;
            }

            decimal newBalance = rbLoan.Checked
                ? _selectedCustomerBalance + amount
                : _selectedCustomerBalance - amount;

            string label = newBalance > 0 ? $"PKR {newBalance:N2} Loan Outstanding"
                         : newBalance < 0 ? $"PKR {Math.Abs(newBalance):N2} Advance Credit"
                         : "✅ Fully Settled";

            lblPreview.Text = $"New balance → {label}";
            lblPreview.ForeColor = newBalance > 0 ? Color.FromArgb(192, 0, 0)
                                 : newBalance < 0 ? Color.FromArgb(0, 102, 204)
                                 : Color.FromArgb(39, 174, 96);
        }

        private void UpdateSaveButtonState()
        {
            bool canSave = _selectedCustomerId.HasValue
                        && decimal.TryParse(txtAmount.Text, out decimal a) && a > 0;

            SaveBtn.Enabled = canSave;
            SaveBtn.BackColor = canSave
                ? (rbLoan.Checked ? Color.FromArgb(192, 0, 0) : Color.FromArgb(0, 102, 204))
                : Color.FromArgb(180, 180, 180);
        }

        // ─── Save ─────────────────────────────────────────────────────────────
        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (_isSaving) return;
            if (!ValidateInput()) return;

            _isSaving = true;
            SaveBtn.Enabled = false;
            SaveBtn.Text = "Saving...";

            try
            {
                decimal amount = decimal.Parse(txtAmount.Text);
                string note = txtNote.Text.Trim();
                string method = cmbPaymentMethod.SelectedItem?.ToString() ?? "Cash";
                string refNo = txtReferenceNo.Text.Trim();
                string createdBy = "User"; // Replace with your session user
                bool isLoan = rbLoan.Checked;

                using (var context = new POSDbContext())
                using (var tx = context.Database.BeginTransaction())
                {
                    try
                    {
                        var repo = new CustomerLedgerRepository(context);

                        if (isLoan)
                        {
                            // Manual loan: customer owes us money
                            await repo.PostAdjustmentAsync(
                                _selectedCustomerId.Value,
                                +amount,   // positive = debit = customer owes more
                                string.IsNullOrWhiteSpace(note) ? "Manual loan entry" : note,
                                createdBy);
                        }
                        else
                        {
                            // Advance deposit: customer is giving us money upfront
                            await repo.PostAdvanceDepositAsync(
                                _selectedCustomerId.Value,
                                amount,
                                method,
                                refNo,
                                string.IsNullOrWhiteSpace(note) ? "Manual advance deposit" : note,
                                createdBy);
                        }

                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }

                EntrySaved = true;

                string typeLabel = isLoan ? "Loan" : "Advance";
                string msg = $"{typeLabel} of PKR {amount:N2} recorded for {_selectedCustomerName}.";
                MessageBox.Show(msg, "✅ Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ask: save another or close?
                var again = MessageBox.Show(
                    "Do you want to add another entry for this customer?",
                    "Add Another?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (again == DialogResult.Yes)
                    await ResetForNextEntryAsync();
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving entry:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSaving = false;
                UpdateSaveButtonState();
                SaveBtn.Text = "💾  Save Entry";
            }
        }

        private async Task ResetForNextEntryAsync()
        {
            // Clear customer selection
            _selectedCustomerId = null;
            _selectedCustomerName = null;
            _selectedCustomerBalance = 0;
            txtCustomerSearch.Text = "";
            txtCustomerSearch.Enabled = true;
            pnlSelectedCustomer.Visible = false;
            pnlCustomerSuggestions.Visible = false;

            // Clear amount, note, reference
            txtAmount.Clear();
            txtNote.Clear();
            txtReferenceNo.Clear();
            cmbPaymentMethod.SelectedIndex = 0;

            // Reset to loan mode (reflows layout via OnEntryTypeChanged)
            rbLoan.Checked = true;

            // Reset preview
            lblPreview.Text = "—";
            lblPreview.ForeColor = System.Drawing.Color.Gray;

            // Ready for next entry
            txtCustomerSearch.Focus();
            //// Clear amount & note but keep customer selected
            //txtAmount.Clear();
            //txtNote.Clear();
            //txtReferenceNo.Clear();
            //rbLoan.Checked = true;

            //// Refresh customer balance
            //if (_selectedCustomerId.HasValue)
            //{
            //    await RefreshSelectedCustomerBalanceAsync(_selectedCustomerId.Value);
            //}

            //UpdatePreview();
            //txtAmount.Focus();
        }

        private async Task RefreshSelectedCustomerBalanceAsync(int customerId)
        {
            using (var context = new POSDbContext())
            {
                var repo = new CustomerLedgerRepository(context);
                decimal balance = await repo.GetCurrentBalanceAsync(customerId);

                // Update display without changing selection
                _selectedCustomerBalance = balance;

                if (balance > 0)
                {
                    lblSelectedBalance.Text = $"Outstanding Loan: PKR {balance:N2}";
                    lblSelectedBalance.ForeColor = Color.FromArgb(192, 0, 0);
                }
                else if (balance < 0)
                {
                    lblSelectedBalance.Text = $"Advance Credit: PKR {Math.Abs(balance):N2}";
                    lblSelectedBalance.ForeColor = Color.FromArgb(0, 102, 204);
                }
                else
                {
                    lblSelectedBalance.Text = "No outstanding balance";
                    lblSelectedBalance.ForeColor = Color.Gray;
                }
            }
        }

        private bool ValidateInput()
        {
            if (_selectedCustomerId == null)
            {
                MessageBox.Show("Please select a customer first.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerSearch.Focus();
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than 0.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (!rbLoan.Checked) // Advance payment
            {
                if (cmbPaymentMethod.SelectedItem == null)
                {
                    MessageBox.Show("Please select a payment method.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPaymentMethod.Focus();
                    return false;
                }

                bool needsRefNo = cmbPaymentMethod.SelectedItem.ToString() == "Cheque"
                               || cmbPaymentMethod.SelectedItem.ToString() == "Bank Transfer";

                if (needsRefNo && string.IsNullOrWhiteSpace(txtReferenceNo.Text))
                {
                    MessageBox.Show("Please enter a reference number for this payment method.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtReferenceNo.Focus();
                    return false;
                }
            }

            string typeLabel = rbLoan.Checked ? "Loan" : "Advance";
            var confirm = MessageBox.Show(
                $"Post this entry?\n\n" +
                $"Customer : {_selectedCustomerName}\n" +
                $"Type     : {typeLabel}\n" +
                $"Amount   : PKR {amount:N2}\n" +
                (string.IsNullOrWhiteSpace(txtNote.Text) ? "" : $"Note     : {txtNote.Text}\n") +
                (!rbLoan.Checked ? $"Method   : {cmbPaymentMethod.SelectedItem}\n" : "") +
                (!string.IsNullOrWhiteSpace(txtReferenceNo.Text) ? $"Ref No   : {txtReferenceNo.Text}\n" : ""),
                "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            return confirm == DialogResult.Yes;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtCustomerSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && pnlCustomerSuggestions.Visible)
            {
                lbSuggestions.Focus();
                if (lbSuggestions.Items.Count > 0)
                    lbSuggestions.SelectedIndex = 0;
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape && pnlCustomerSuggestions.Visible)
            {
                pnlCustomerSuggestions.Visible = false;
                e.Handled = true;
            }
        }

        private void txtCustomerSearch_Leave(object sender, EventArgs e)
        {
            // Don't hide immediately to allow clicking on suggestions
            if (!pnlCustomerSuggestions.Focused && !lbSuggestions.Focused)
            {
                // Small delay to allow click events on suggestions
                Task.Delay(200).ContinueWith(_ =>
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (!pnlCustomerSuggestions.Focused && !lbSuggestions.Focused)
                                pnlCustomerSuggestions.Visible = false;
                        }));
                    }
                });
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && SaveBtn.Enabled)
                SaveBtn.PerformClick();
        }
    }

    // ─── Search DTO ──────────────────────────────────────────────────────────
    public class CustomerSearchResult
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
