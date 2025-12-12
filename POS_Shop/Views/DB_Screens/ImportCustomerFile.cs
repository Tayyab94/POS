using ExcelDataReader;
using Org.BouncyCastle.Bcpg.Sig;
using POS_Shop.Helpers;
using POS_Shop.Models;
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
    public partial class ImportCustomerFile : Form
    {
        public ImportCustomerFile()
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
                SaveUpdatedPriceBtn.Visible = true;
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



                    filtered.Columns.Add("ID", typeof(int));
                    filtered.Columns.Add("Name", typeof(string));
                    filtered.Columns.Add("Address", typeof(string));
                    filtered.Columns.Add("Phone", typeof(string));
                    filtered.Columns.Add("CityId", typeof(int));
                    filtered.Columns.Add("Active", typeof(bool));

                    // Copy rows
                    foreach (DataRow row in currentTable.Rows)
                    {
                        //// Skip rows that are empty or header duplicates
                        //if (row[0] == DBNull.Value || row[0].ToString() == "ID")
                        //    continue;

                        // Convert Active column to boolean properly
                        bool activeValue = false;
                        if (row[5] != DBNull.Value && row[5] != null)
                        {
                            string activeStr = row[5].ToString().ToUpper();
                            activeValue = (activeStr == "TRUE" || activeStr == "1" || activeStr == "YES");
                        }

                        filtered.Rows.Add(
                            row[0],
                            row[1],
                            row[2],
                            row[3],
                            row[4],
                            activeValue
                        );
                    }

                    updatedCustomerListGrid.DataSource = filtered;
                    updatedCustomerListGrid.AllowUserToAddRows = false;



                    // Add Delete button column
                    DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn();
                    deleteButtonColumn.HeaderText = "Action";
                    deleteButtonColumn.Name = "Delete";
                    deleteButtonColumn.Text = "Delete";
                    deleteButtonColumn.UseColumnTextForButtonValue = true;
                    updatedCustomerListGrid.Columns.Add(deleteButtonColumn);

                    // Handle the button click event
                    updatedCustomerListGrid.CellClick += (sender1, e1) =>
                    {
                        if (e1.ColumnIndex == updatedCustomerListGrid.Columns["Delete"].Index && e1.RowIndex >= 0)
                        {
                            // Confirm deletion
                            DialogResult result = MessageBox.Show("Are you sure you want to delete this row?",
                                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                // Remove the row from the DataTable
                                DataRowView rowView = (DataRowView)updatedCustomerListGrid.Rows[e1.RowIndex].DataBoundItem;
                                DataRow rowToDelete = rowView.Row;

                                // Remove from DataTable
                                ((DataTable)updatedCustomerListGrid.DataSource).Rows.Remove(rowToDelete);

                                // Optional: Refresh the grid
                                updatedCustomerListGrid.Refresh();
                            }
                        }
                    };
                }

            }
        }

        private void SaveUpdatedPriceBtn_Click(object sender, EventArgs e)
        {
            if (updatedCustomerListGrid.Rows.Count != 0 && updatedCustomerListGrid.Rows != null)
            {
                try
                {
                    LoadingManager.ShowLoading();
                    DataTable dataTable = (DataTable)updatedCustomerListGrid.DataSource;
                    if (dataTable == null || dataTable.Rows.Count == 0)
                    {
                        MessageBox.Show("No data to import. Please load data from an Excel file first.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    using (var context = new POSDbContext())
                    {
                        int updatedCount = 0;
                        int addedCount = 0;
                        var CustomerToAddList = new List<Models.Customer>();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            //if (row.IsNull("ID") || string.IsNullOrEmpty(row["Name"].ToString()))
                            //    continue;

                            //int productId = Convert.ToInt32(row["ID"]);
                            var pName = row[1].ToString();
                            var existingCustomer = context.Customers.Where(s => s.CustomerName == pName).FirstOrDefault();


                            if (existingCustomer != null)
                            {
                                // Update existing product
                                existingCustomer.CustomerName = GetStringOrNull(row["Name"]);
                                existingCustomer.CustomerAddress = GetStringOrNull(row["Address"]);
                                existingCustomer.ContactNo = GetStringOrNull(row["Phone"]);
                                existingCustomer.CityId = int.Parse(row["CityId"].ToString());
                               // existingCustomer.IsDeleted = bool.Parse(row["Active"].ToString());

                                bool isActive = false;

                                if (row["Active"] != DBNull.Value && row["Active"] != null)
                                {
                                    string activeStr = row["Active"].ToString().ToUpper();
                                    isActive = activeStr == "TRUE" || activeStr == "1" || activeStr == "YES";
                                }

                                existingCustomer.IsDeleted = !isActive;
                                context.Entry(existingCustomer).State = EntityState.Modified;
                                updatedCount++;
                            }
                            else
                            {
                                bool isActive = false;

                                if (row["Active"] != DBNull.Value && row["Active"] != null)
                                {
                                    string activeStr = row["Active"].ToString().ToUpper();
                                    isActive = activeStr == "TRUE" || activeStr == "1" || activeStr == "YES";
                                }

                                // Add new product
                                var newProduct = new Models.Customer
                                {
                                    //Id = productId,
                                    CustomerName = GetStringOrNull(row["Name"]),
                                    CustomerAddress = GetStringOrNull(row["Address"]),
                                    ContactNo = GetStringOrNull(row["Phone"]),
                                    CityId =int.Parse(row["CityId"].ToString()),
                                    IsDeleted =isActive
                                };

                                CustomerToAddList.Add(newProduct);
                                addedCount++;
                            }
                        }

                        if (CustomerToAddList.Count > 0)
                            context.Customers.AddRange(CustomerToAddList);
                        int savedRecords = context.SaveChanges();

                        LoadingManager.HideLoading();
                        MessageBox.Show($"Successfully imported {(addedCount + updatedCount)} records to database!",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.Close();
                    }
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

        private string GetStringOrNull(object value)
        {
            if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                return null;

            return value.ToString();
        }
    }
}
