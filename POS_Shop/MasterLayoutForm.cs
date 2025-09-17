using POS_Shop.Helpers;
using POS_Shop.Views.Account;
using POS_Shop.Views.BillScreen;
using POS_Shop.Views.DB_Screens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop
{
    public partial class MasterLayoutForm : Form
    {
        public MasterLayoutForm()
        {
            InitializeComponent();
            this.MinimumSize = new Size(200, 150); // Set to a reasonable small size
        }

        private async void cityBtn_Click(object sender, EventArgs e)
        {

            //MainPanel.Padding = new Padding(0);
            //MainPanel.Margin = new Padding(0);

            //var userCtrl = new Views.Controllers.City.CityControl();
            //userCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

            //MainPanel.Controls.Clear();
            //MainPanel.Controls.Add(userCtrl);
            try
            {
                LoadingManager.ShowLoading();

                // Load the city control asynchronously to keep UI responsive
                //await System.Threading.Tasks.Task.Run(() =>
                //{
                //    System.Threading.Thread.Sleep(2000); // Simulating long load
                //});

                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);
                var userCtrl = new Views.Controllers.City.CityControl();
                userCtrl.Dock = DockStyle.Fill;

                // Update UI controls on the main thread
                MainPanel.Invoke(new Action(() =>
                {
                    MainPanel.Controls.Clear();
                    MainPanel.Controls.Add(userCtrl);
                }));
            }
            finally
            {
                LoadingManager.HideLoading();
            }
        }

        private void CountryBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(new Views.Controllers.Country.CountryControl1());
            }
            catch (Exception ex)
            {

            }finally
            {
                LoadingManager.HideLoading();
            }
            
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            SessionManager.Logout();
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                //if (form != this)
                //    form.Close();
                form.Close();
            }

            var loginForm = new LoginForm();
            loginForm.Show();
        }

      

        private void CategoryBtn_Click(object sender, EventArgs e)
        {
            try
                {
                LoadingManager.ShowLoading();
                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);
                var categoryCtrl = new Views.Controllers.Category.CategoryControl();
                categoryCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel
                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(categoryCtrl);
            }
            finally
            {
                LoadingManager.HideLoading();
            }
           
        }

        private void SubCategoryBtn_Click(object sender, EventArgs e)
        {
            try
                {
                LoadingManager.ShowLoading();
                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);

                var subcategoryCtrl = new Views.Controllers.SubCategory.SubCategoryForm();
                subcategoryCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(subcategoryCtrl);
            }
            finally
            {
                LoadingManager.HideLoading();
            }
         
        }

        private void ProductSectrionBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);

                var ProductFormCtrl = new Views.Product.ProductFromControl();
                ProductFormCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(ProductFormCtrl);
            }
            finally
            {
                LoadingManager.HideLoading();
            }
        }

        private void importExcelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                ImportExcelFile importExcelForm = new ImportExcelFile();
                importExcelForm.Owner = this;
                importExcelForm.Show();
            }
            finally
            {
                LoadingManager.HideLoading();
            }
        }

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            BackUpForm backupForm = new BackUpForm();
            backupForm.Owner = this;
            backupForm.Show();
        }

        private void restoreDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreDbForm restoreDbForm = new RestoreDbForm();
            restoreDbForm.Owner = this;
            restoreDbForm.Show();
        }

        private void CustomerSectionBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);

                var CustomerFormCtrl = new Views.Controllers.Customers.CustomerFormControl();
                CustomerFormCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(CustomerFormCtrl);
            }
            finally
            {
                LoadingManager.HideLoading();
            }
        }

        private void BillPadBtn_Click(object sender, EventArgs e)
        {
                var BillPadForm = new BillPadForm();
                //BillPadForm.Owner = this;
                //BillPadForm.Show();
                this.Hide();
                BillPadForm.ShowDialog();
                this.Show();

        }
    }
}
