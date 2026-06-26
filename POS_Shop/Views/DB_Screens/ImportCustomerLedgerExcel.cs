using ExcelDataReader;
using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Models.LoanModelsV1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.DB_Screens
{
    public partial class ImportCustomerLedgerExcel : Form
    {
        public ImportCustomerLedgerExcel()
        {
            InitializeComponent();
        }

        private void BrowsUpdatedExcelFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Set the filter to show only .bak files
            ofd.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm|All files|*.*";
            ofd.Title = "Select an Excel File";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImportUpdatedFilePathTxt.Text = ofd.FileName;
                LoadUpdatedDataBtn.Enabled = true;
            }
        }

        private void LoadUpdatedDataBtn_Click(object sender, EventArgs e)
        {
            using (var stream = File.Open(ImportUpdatedFilePathTxt.Text, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                SaveUpdatedDataBtn.Visible = true;
                //// Register encoding provider (needed for older Excel files, e.g., .xls)
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var conf = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };


                    var dataSet = reader.AsDataSet(conf);

                    if (dataSet.Tables.Count == 0)
                    {
                        MessageBox.Show("No worksheets found in the file.", "No data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    var currentTable = dataSet.Tables[0];

                    DataTable filtered = new DataTable();
                    // Add only required columns

                    filtered.Columns.Add("Customer ID", typeof(int));
                    filtered.Columns.Add("Customer Name", typeof(string));
                    filtered.Columns.Add("Balance (PKR)", typeof(string));
                    filtered.Columns.Add("Status", typeof(string));
                    filtered.Columns.Add("Last Transaction", typeof(string));

                    // Copy rows
                    foreach (DataRow row in currentTable.Rows)
                    {
                        //// Skip rows that are empty or header duplicates
                        //if (row[0] == DBNull.Value || row[0].ToString() == "ID")
                        //    continue;

                        //// Convert Active column to boolean properly
                        //bool activeValue = false;
                        //if (row[6] != DBNull.Value && row[6] != null)
                        //{
                        //    string activeStr = row[6].ToString().ToUpper();
                        //    activeValue = (activeStr == "TRUE" || activeStr == "1" || activeStr == "YES");
                        //}

                        filtered.Rows.Add(
                            row[0],
                            row[1],
                            row[2],
                            row[3],
                            row[4]
                        );
                    }

                    CustomerLedgerListGrid.DataSource = filtered;
                    CustomerLedgerListGrid.AllowUserToAddRows = false;



                    // Add Delete button column
                    DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                    deleteButtonColumn.HeaderText = "Action";
                    deleteButtonColumn.Name = "Delete";
                    deleteButtonColumn.Text = "Delete";
                    deleteButtonColumn.UseColumnTextForButtonValue = true;
                    CustomerLedgerListGrid.Columns.Add(deleteButtonColumn);

                    // Handle the button click event
                    CustomerLedgerListGrid.CellClick += (sender1, e1) =>
                    {
                        if (e1.ColumnIndex == CustomerLedgerListGrid.Columns["Delete"].Index && e1.RowIndex >= 0)
                        {
                            // Confirm deletion
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this row?",
                                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                // Remove the row from the DataTable
                                DataRowView rowView = (DataRowView)CustomerLedgerListGrid.Rows[e1.RowIndex].DataBoundItem;
                                DataRow rowToDelete = rowView.Row;

                                // Remove from DataTable
                                ((DataTable)CustomerLedgerListGrid.DataSource).Rows.Remove(rowToDelete);

                                // Optional: Refresh the grid
                                CustomerLedgerListGrid.Refresh();
                            }
                        }
                    };
                }
            }
        }

        private async void SaveUpdatedDataBtn_Click(object sender, EventArgs e)
        {
            if (CustomerLedgerListGrid.Rows.Count != 0 && CustomerLedgerListGrid.Rows != null)
            {
                try
                {
                    LoadingManager.ShowLoading();
                    DataTable dataTable = (DataTable)CustomerLedgerListGrid.DataSource;
                    if (dataTable == null || dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No data to import. Please load data from an Excel file first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    await BulkImportLedgerFromExcelAsync(dataTable, "User");

                    LoadingManager.HideLoading();
                    MessageBox.Show($"Record imported successfully to database!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                catch (Exception)
                {
                    LoadingManager.HideLoading();
                    throw;
                }
            }
            else
            {
                MessageBox.Show($"Please Upload the Customers",
                          "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        public async Task BulkImportLedgerFromExcelAsync(
    DataTable dataTable, string createdBy)
        {
            // 1. Collect all unique customer identifiers from the sheet up-front
            var customerNames = dataTable.Rows
                .Cast<DataRow>()
                .Where(r => r[3].ToString() != "CLEAR")
                .Select(r => r[1].ToString().Trim())   // assuming col 0 = customer name/id
                .Distinct()
                .ToList();
            using (var _context = new POSDbContext())
            {
                // 2. Load all needed customers in ONE query
                var customers = await _context.Customers
                    .Where(c => customerNames.Contains(c.CustomerName))
                    .ToDictionaryAsync(c => c.CustomerName, c => c);

                // 3. Load current balances for all those customers in ONE query
                var customerIds = customers.Values.Select(c => c.Id).ToList();

                var balances = await _context.CustomerLedgerEntries
                    .Where(e => customerIds.Contains(e.CustomerId))
                    .GroupBy(e => e.CustomerId)
                    .Select(g => new { CustomerId = g.Key, Balance = g.OrderByDescending(e => e.Id).FirstOrDefault().Balance })
                    .ToDictionaryAsync(x => x.CustomerId, x => x.Balance);

                // 4. Process all rows in memory — zero extra DB calls inside loop
                var entriesToAdd = new List<CustomerLedgerEntry>();
                var errors = new List<string>();

                foreach (DataRow row in dataTable.Rows)
                {
                    string status = row[3].ToString().Trim().ToUpper();
                    if (status == "CLEAR") continue;

                    string customerName = row[1].ToString().Trim();
                    decimal amount = decimal.Parse(row[2].ToString());
                    //if (!decimal.TryParse(row[2].ToString(), out decimal amount) || amount <= 0)
                    //{
                    //    errors.Add($"Invalid amount for customer '{customerName}'. Skipping.");
                    //    continue;
                    //}

                    string note = row[2].ToString().Trim();   // adjust column index as needed

                    //if (!customers.TryGetValue(customerName, out var customer))
                    //{
                    //    errors.Add($"Customer '{customerName}' not found in DB. Skipping.");
                    //    continue;
                    //}

                    // Track running balance in-memory (no DB call needed)
                    //decimal prevBalance = balances.Where(s => s.Key == customer.Id).Select(s => s.Value);

                    // Track running balance in-memory (no DB call needed)
                    //decimal prevBalance = balances.GetValueOrDefault(customer.Id, 0);
                    balances.TryGetValue(Convert.ToInt32(row[0].ToString()), out var prevBalance);
                    decimal newBalance;
                    CustomerLedgerEntry entry;

                    if (status == "LOAN")
                    {
                        // Customer owes us → Debit
                        newBalance = prevBalance + amount;
                        entry = new CustomerLedgerEntry
                        {
                            CustomerId = Convert.ToInt32(row[0].ToString()),
                            EntryDate = DateTime.Now,
                            EntryType = LedgerEntryType.Adjustment.ToString(),
                            Debit = amount,
                            Credit = 0,
                            Balance = newBalance,
                            ReferenceType = "ADJUSTMENT",
                            Note = string.IsNullOrWhiteSpace(note) ? "Excel import – loan" : note,
                            CreatedBy = createdBy
                        };
                    }
                    else if (status == "ADVANCE")
                    {
                        // Customer paid us upfront → Credit
                        newBalance = prevBalance - amount;
                        entry = new CustomerLedgerEntry
                        {
                            CustomerId = Convert.ToInt32(row[0].ToString()),
                            EntryDate = DateTime.Now,
                            EntryType = LedgerEntryType.AdvanceDeposit.ToString(),
                            Debit = 0,
                            Credit = amount,
                            Balance = -1 * newBalance,
                            ReferenceType = "ADVANCE",
                            Note = string.IsNullOrWhiteSpace(note) ? "Excel import – advance" : note,
                            CreatedBy = createdBy
                        };
                    }
                    else
                    {
                        errors.Add($"Unknown status '{status}' for customer '{customerName}'. Skipping.");
                        continue;
                    }

                    // Update in-memory balance tracker for subsequent rows of same customer
                    balances[Convert.ToInt32(row[0].ToString())] = newBalance;
                    entriesToAdd.Add(entry);
                }

                // 5. ONE bulk insert — single round-trip to DB
                if (entriesToAdd.Any())
                {
                    _context.CustomerLedgerEntries.AddRange(entriesToAdd);
                    await _context.SaveChangesAsync();   // single SaveChanges for entire import
                }

                // 6. Report skipped rows to caller
                if (errors.Any())
                    throw new ImportValidationException(errors);  // see below
            }

        }
    }

    public class ImportValidationException : Exception
    {
        public List<string> Errors { get; }
        public ImportValidationException(List<string> errors)
            : base("Some rows were skipped during import.")
        {
            Errors = errors;
        }
    }
}
