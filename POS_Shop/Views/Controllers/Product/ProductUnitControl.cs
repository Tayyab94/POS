using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Product
{
    public partial class ProductUnitControl : UserControl
    {
        public ProductUnitControl()
        {
            InitializeComponent();
            LoadProductUnitForDataGridView();
        }

        private void LoadProductUnitForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                IProductUnitRepository productUnitRepo = new ProductUnitRepository(context);
                var prodInits = productUnitRepo.GetAll().ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
               dt.Columns.Add("Abbreviation", typeof(string));
                dt.Columns.Add("CreatedAt", typeof(DateTime));
                dt.Columns.Add("Is Active", typeof(bool));

                foreach (var prdUnit in prodInits)
                {
                    dt.Rows.Add(prdUnit.Id, prdUnit.Name, prdUnit.Abbreviation,prdUnit.CreatedAt.ToShortDateString(), prdUnit.IsActive);
                }

                //ProductUnitDatagridView.AutoGenerateColumns = true;

                ProductUnitDatagridView.ReadOnly = true;
                ProductUnitDatagridView.AllowUserToAddRows = false;

                ProductUnitDatagridView.DataSource = dt;
            }
        }

        private void SaveProdUnitBtn_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren(ValidationConstraints.Enabled))
            {
                // There are invalid controls
                MessageBox.Show("Please correct the errors before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(ProdUnitNameTxt .Text, @"^[\p{L}\s]*$") == false)
            {
                MessageBox.Show("Please enter a valid name (Urdu, English letters, and spaces).");
                return;
            }

            using (var context = new POSDbContext())
            {

                IProductUnitRepository categoryRepository = new ProductUnitRepository(context);

                categoryRepository.Insert(new Models.ProductUnit()
                {
                    Name = ProdUnitNameTxt.Text,
                    Abbreviation= ProdUnitAbbreviationTxt.Text,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = ProdUnitActiveChkBox.Checked? true : false
                });
                categoryRepository.Save();
            }
            MessageBox.Show("Unit saved successfully!");
            ClearFormFunction();
            LoadProductUnitForDataGridView();
        }

        private async void updateProductIUnitBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(productUnitIdTxt.Text) || !int.TryParse(productUnitIdTxt.Text, out int prodUnitId) || prodUnitId <= 0)
            {
                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new POSDbContext())
            {
                var prodUnitRepo = new ProductUnitRepository(context);

                try
                {
                    //ICityRepository cityRepository = new CityRepository(context);
                    var data= prodUnitRepo.GetById(int.Parse(productUnitIdTxt.Text));
                    data.Id = int.Parse(productUnitIdTxt.Text);
                    data.Name = ProdUnitNameTxt.Text;
                    data.Abbreviation = ProdUnitAbbreviationTxt.Text;
                    data.IsActive = ProdUnitActiveChkBox.Checked ? true : false;

                    prodUnitRepo.Update(data);
                    //prodUnitRepo.Update(new Models.ProductUnit()
                    //{
                    //    Id =int.Parse(productUnitIdTxt.Text),
                    //    Name = ProdUnitNameTxt.Text,
                    //    Abbreviation = ProdUnitAbbreviationTxt.Text,
                    //    IsActive= ProdUnitActiveChkBox.Checked ? true : false

                    //});
                    prodUnitRepo.Save();
                    ClearFormFunction();
                    LoadProductUnitForDataGridView();
                    MessageBox.Show("Record has been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
        }

        private void RemoveProductUnitBtn_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this Category?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmResult == DialogResult.Yes)
            {

                var prdUnitId = Convert.ToInt32(productUnitIdTxt.Text);
                using (var context = new POSDbContext())
                {
                    var prodUnitRepo = new ProductUnitRepository(context);
                    var data = prodUnitRepo.GetById(prdUnitId);
                    if (data != null)
                    {
                        try
                        {
                            prodUnitRepo.Delete(prdUnitId);
                            prodUnitRepo.Save();
                            MessageBox.Show("Unit deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFormFunction();
                            LoadProductUnitForDataGridView();
                          
                        }

                        catch (DbUpdateException dbEx) when (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                        {
                            MessageBox.Show("This Entity is being used by other records and cannot be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            // Don't navigate away, stay on current page
                            context.ChangeTracker.DetectChanges();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Unit not found for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        string _lastValidText = "";

        private void ProdUnitNameTxt_TextChange(object sender, EventArgs e)
        {
            // Regex for allowing letters and spaces, and nothing else.
            if (Regex.IsMatch(ProdUnitNameTxt.Text, @"^[a-zA-Z\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\s]*$"))
            {
                _lastValidText = ProdUnitNameTxt.Text; // Update if valid.
            }
            else
            {
                // If the pasted text is invalid, revert to the last valid state.
                ProdUnitNameTxt.Text = _lastValidText;
                // Optionally, place the cursor at the end for a better user experience.
                ProdUnitNameTxt.SelectionStart = ProdUnitNameTxt.Text.Length;
            }
        }

        private void ProdUnitNameTxt_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(ProdUnitNameTxt.Text) || !Regex.IsMatch(ProdUnitNameTxt.Text, @"^[a-zA-Z\u0600-\u06FF\u0750-\u077F\u08A0-\u08FF\s]*$"))
            {
                e.Cancel = true; // Cancel the event
                ProdUnitNameTxt.BackColor = Color.Red;
                errorProvider.SetError(ProdUnitNameTxt, "Please enter a valid name (Urdu, English letters, and spaces).");
            }
            else
            {
                e.Cancel = false; // Allow the event to proceed
                ProdUnitNameTxt.BackColor = SystemColors.Window;
                errorProvider.SetError(ProdUnitNameTxt, string.Empty); // Clear any previous error message
            }
        }

        private void ProductUnitDatagridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = ProductUnitDatagridView.Rows[e.RowIndex];
                ProdUnitNameTxt.Text = row.Cells["Name"].Value.ToString();
                productUnitIdTxt.Text = row.Cells["ID"].Value.ToString();
                ProdUnitAbbreviationTxt.Text = row.Cells["Abbreviation"].Value.ToString();
                ProdUnitActiveChkBox.Checked = Convert.ToBoolean(row.Cells["Is Active"].Value);
                updateProductIUnitBtn.Enabled = true;
                RemoveProductUnitBtn.Enabled = true;
                RemoveProductUnitBtn.Visible = true;
            }
        }


        // Clear form fields
        private void ClearFormFunction()
        {
            ProdUnitNameTxt.Clear();
            productUnitIdTxt.Clear();
            ProdUnitAbbreviationTxt.Clear();
            ProdUnitActiveChkBox.Checked=true;
           
        }
    }
}
