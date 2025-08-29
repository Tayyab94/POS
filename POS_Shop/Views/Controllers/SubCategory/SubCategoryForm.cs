using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.SubCategory
{
    public partial class SubCategoryForm : UserControl
    {
        public SubCategoryForm()
        {
            InitializeComponent();
            this.Load += SubcategoryControl_Load;
        }

        private async void SubcategoryControl_Load(object sender, EventArgs e)
        {
            loadCategiryForDropdown(); // If this is sync, keep it here
           await LoadSubCategoryForDataGridView(); // Now you can await it safely
        }


        private void loadCategiryForDropdown()
        {
            using (var context = new POSDbContext())
            {
                var countriesList = context.Categories.ToList();
                CategoryDropDownLst.Items.Clear();
                CategoryDropDownLst.DataSource = countriesList;
                CategoryDropDownLst.DisplayMember = "Name";
                CategoryDropDownLst.ValueMember = "Id";
            }
        }

        private async Task LoadSubCategoryForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                ISubCategoryRepository cityRepository = new SubCategoryRepository(context);
                var cities = await cityRepository.GetSubcategoriesListAsync();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Category Id", typeof(int));
                dt.Columns.Add("Category Name", typeof(string));
                dt.Columns.Add("IsActive", typeof(string));

                foreach (var country in cities)
                {
                    dt.Rows.Add(country.Id, country.Name, country.CategoryId, country.CategoryName, country.IsActive);
                }
                SubcategoryDatagridView.ReadOnly = true;
                SubcategoryDatagridView.AllowUserToAddRows = false;

                SubcategoryDatagridView.DataSource = dt;
                SubcategoryDatagridView.Columns[2].Visible = false;
            }
        }



        private string _lastValidText = "";

        private void subcategoryNameTxt_Changed(object sender, EventArgs e)
        {
            // Regex for allowing letters and spaces, and nothing else.
            if (Regex.IsMatch(subcategoryNameTxt.Text, "^[a-zA-Z ]*$"))
            {
                _lastValidText = subcategoryNameTxt.Text; // Update if valid.
            }
            else
            {
                // If the pasted text is invalid, revert to the last valid state.
                subcategoryNameTxt.Text = _lastValidText;
                // Optionally, place the cursor at the end for a better user experience.
                subcategoryNameTxt.SelectionStart = subcategoryNameTxt.Text.Length;
            }
        }


        private async void SaveSubcategoryBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(subcategoryNameTxt.Text))
            {
                MessageBox.Show("Please Enter City Name or Select Country", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int selectedIndex = CategoryDropDownLst.SelectedIndex - 1; // Adjust for default item

            using (var context = new POSDbContext())
            {

                int selectedId = Convert.ToInt32(CategoryDropDownLst.SelectedValue);
                ISubCategoryRepository subcatRepo = new SubCategoryRepository(context);

                subcatRepo.Insert(new Models.SubCategory
                {
                    name = subcategoryNameTxt.Text,
                    isActive = true,
                    categoryId = selectedId,
                });
                subcatRepo.Save();
            }
            MessageBox.Show("Subcategory saved successfully!");
            await LoadSubCategoryForDataGridView();
        }

        private void SubcategoryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = SubcategoryDatagridView.Rows[e.RowIndex];
                subcategoryNameTxt.Text = row.Cells["Name"].Value.ToString();
                SubcategoryIdTxt.Text = row.Cells["ID"].Value.ToString();
                if (row.Cells["Category Id"] != null && row.Cells["Category Id"].Value != null)
                {
                    int countryId = Convert.ToInt32(row.Cells["Category Id"].Value);
                    CategoryDropDownLst.SelectedValue = countryId;
                }

                Updatesubcategorybtn.Enabled = true;
                Updatesubcategorybtn.Visible = true;
                Updatesubcategorybtn.IdleFillColor = Color.OrangeRed;
            }
        }

        private async void Updatesubcategorybtn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(SubcategoryIdTxt.Text) || !int.TryParse(SubcategoryIdTxt.Text, out int subCategoryId) || subCategoryId <= 0)
            {
                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new POSDbContext())
            {
                ISubCategoryRepository subCategoryRepository = new SubCategoryRepository(context);
                var response = await subCategoryRepository.UpdateSubCategory(new Models.SubCategory()
                {
                    id = subCategoryId,
                    name = subcategoryNameTxt.Text,
                    isActive = true,
                    categoryId = Convert.ToInt32(CategoryDropDownLst.SelectedValue),
                });

                if (response)
                    MessageBox.Show("Record has been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                await LoadSubCategoryForDataGridView();
            }
        }
    }
}
