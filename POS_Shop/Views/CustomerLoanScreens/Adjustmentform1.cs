using POS_Shop.Models;
using POS_Shop.Repositories.LoanRepositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreens
{
    /// <summary>
    /// Simple admin dialog to post a manual debit or credit adjustment.
    /// Opened from CustomerLedgerForm → "Adjustment" button.
    /// </summary>
    public class AdjustmentForm1 : Form
    {
        private readonly int _customerId;
        private readonly string _customerName;
        private readonly decimal _currentBalance;

        private Label lblCust, lblBalLbl, lblBal, lblAmtLbl, lblNotesLbl;
        private TextBox txtAmount, txtNotes;
        private RadioButton rbDebit, rbCredit;
        private Button btnSave, btnCancel;

        public AdjustmentForm1(int customerId, string customerName, decimal balance)
        {
            _customerId = customerId;
            _customerName = customerName;
            _currentBalance = balance;
            Build();
        }

        private void Build()
        {
            this.Text = "Post Manual Adjustment";
            this.Size = new Size(440, 360);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(245, 245, 245);
            this.Font = new Font("Segoe UI", 9F);

            int y = 20;
            AddLabel(this, "Customer:", 20, y, bold: true);
            lblCust = AddLabel(this, _customerName, 130, y, bold: false, color: Color.FromArgb(30, 80, 162));
            y += 30;

            AddLabel(this, "Current Balance:", 20, y, bold: true);
            lblBal = AddLabel(this, "", 160, y, bold: false);
            if (_currentBalance > 0)
            { lblBal.Text = $"Dr Rs. {_currentBalance:N0}"; lblBal.ForeColor = Color.FromArgb(198, 40, 40); }
            else if (_currentBalance < 0)
            { lblBal.Text = $"Cr Rs. {Math.Abs(_currentBalance):N0}"; lblBal.ForeColor = Color.FromArgb(21, 101, 192); }
            else
            { lblBal.Text = "Nil"; lblBal.ForeColor = Color.FromArgb(46, 125, 50); }
            y += 36;

            // Direction
            var grp = new GroupBox { Location = new Point(20, y), Size = new Size(380, 50), Text = "Direction" };
            rbDebit = new RadioButton
            {
                Location = new Point(10, 22),
                Size = new Size(170, 22),
                Text = "Debit (↑ customer owes more)",
                Checked = true,
                Font = new Font("Segoe UI", 9F)
            };
            rbCredit = new RadioButton
            {
                Location = new Point(200, 22),
                Size = new Size(170, 22),
                Text = "Credit (↓ reduce balance)",
                Font = new Font("Segoe UI", 9F)
            };
            grp.Controls.Add(rbDebit);
            grp.Controls.Add(rbCredit);
            this.Controls.Add(grp);
            y += 60;

            AddLabel(this, "Amount (Rs):", 20, y, bold: true);
            txtAmount = new TextBox
            {
                Location = new Point(150, y - 2),
                Size = new Size(160, 26),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = HorizontalAlignment.Right
            };
            this.Controls.Add(txtAmount);
            y += 36;

            AddLabel(this, "Notes:", 20, y, bold: true);
            txtNotes = new TextBox
            {
                Location = new Point(150, y - 2),
                Size = new Size(250, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(txtNotes);
            y += 76;

            btnSave = new Button
            {
                Location = new Point(200, y),
                Size = new Size(120, 36),
                Text = "✔  Post Adjustment",
                BackColor = Color.FromArgb(30, 80, 162),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new Button
            {
                Location = new Point(330, y),
                Size = new Size(70, 36),
                Text = "Cancel",
                BackColor = Color.FromArgb(200, 200, 200),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            this.Controls.Add(btnCancel);
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amt) || amt <= 0)
            {
                MessageBox.Show("Enter a valid amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNotes.Text))
            {
                MessageBox.Show("Notes are required for adjustments.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dc = rbDebit.Checked ? "D" : "C";

            try
            {
                using (var db = new POSDbContext())
                {
                    var repo = new CustomerLedgerRepository(db);
                    await repo.PostAdjustmentAsync(
                        _customerId, amt, dc,
                        txtNotes.Text.Trim(),
                        Environment.UserName);
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static Label AddLabel(Control parent, string text, int x, int y,
            bool bold, Color? color = null)
        {
            var lbl = new Label
            {
                AutoSize = true,
                Location = new Point(x, y),
                Size = new Size(200, 22),
                Text = text,
                Font = new Font("Segoe UI", 9F, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = color ?? Color.FromArgb(50, 50, 50)
            };
            parent.Controls.Add(lbl);
            return lbl;
        }

        //private Label lblBal;
        //private Label lblCust;
    }
}
