using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace POS_Shop.Views.Controllers.Category
{
    public partial class CategoryControl : UserControl
    {
        public CategoryControl()
        {
            InitializeComponent();
            LoadCategoriesForDataGridView();
        }

        private void LoadCategoriesForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                ICategoryRepository categoryRepo = new CategoryRepository(context);
                var categories = categoryRepo.GetAll().ToList();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Is Active", typeof(bool));

                foreach (var category in categories)
                {
                    dt.Rows.Add(category.id, category.name, category.isActive);
                }

                //CountryDatagridView.AutoGenerateColumns = true;

                CategoryDatagridView.ReadOnly = true;
                CategoryDatagridView.AllowUserToAddRows = false;

                CategoryDatagridView.DataSource = dt;
            }
        }

        private void SaveCategoryBtn_Click(object sender, EventArgs e)
        {

            if (Regex.IsMatch(categoryNameTxt.Text, "^[a-zA-Z ]*$")== false)
            {
                MessageBox.Show("Please enter a valid category name (letters and spaces only).");
                return;

            }

            using (var context = new POSDbContext())
            {   

                ICategoryRepository categoryRepository = new CategoryRepository(context);

                categoryRepository.Insert(new Models.Category()
                {
                    name = categoryNameTxt.Text,
                    isActive = true
                });
                categoryRepository.Save();
            }
            MessageBox.Show("Category saved successfully!");

            LoadCategoriesForDataGridView();
        }



        private string _lastValidText = "";

        private void CategoryNameTxt_Changed(object sender, EventArgs e)
        {
            // Regex for allowing letters and spaces, and nothing else.
            if (Regex.IsMatch(categoryNameTxt.Text, "^[a-zA-Z ]*$"))
            {
                _lastValidText = categoryNameTxt.Text; // Update if valid.
            }
            else
            {
                // If the pasted text is invalid, revert to the last valid state.
                categoryNameTxt.Text = _lastValidText;
                // Optionally, place the cursor at the end for a better user experience.
                categoryNameTxt.SelectionStart = categoryNameTxt.Text.Length;
            }
        }



        private async void updateCategoryBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(categoryIdTxt.Text) || !int.TryParse(categoryIdTxt.Text, out int categoryId) || categoryId <= 0)
            {
                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new POSDbContext())
            {
                ICategoryRepository categoryRepo = new CategoryRepository(context);

                try
                {
                    //ICityRepository cityRepository = new CityRepository(context);
                   
                    var response = await categoryRepo.UpdateCategory(new Models.Category()
                    {
                        id = categoryId,
                        name = categoryNameTxt.Text,
                        isActive = true
                    });
                    if (response)
                        MessageBox.Show("Record has been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                catch (Exception ex)
                {

                    throw;
                }




                LoadCategoriesForDataGridView();
            }
        }


        private void CategoryDatagridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CategoryDatagridView.Rows[e.RowIndex];
                categoryNameTxt.Text = row.Cells["Name"].Value.ToString();
                categoryIdTxt.Text = row.Cells["ID"].Value.ToString();
                updateCategoryBtn.Enabled = true;
                updateCategoryBtn.IdleFillColor = Color.OrangeRed;
            }
        }

        //private void categoryNameTxt_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    // only uppercase and lowercase letters (a-z, A-Z) from start to end.
        //    if (Regex.IsMatch(categoryNameTxt.Text, "^[a-zA-Z]*$"))
        //    {

        //        e.Handled = false; // Allow the input
        //          categoryNameTxt.BackColor = SystemColors.Window;
        //    }
        //    else
        //    {
        //       e.Handled = true; // Reject the input
        //        categoryNameTxt.BackColor = Color.Red;
        //    }
        //}
    }
}
