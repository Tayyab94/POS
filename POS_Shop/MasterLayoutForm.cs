using POS_Shop.Helpers;
using POS_Shop.Models.AuthModel;
using POS_Shop.Views.Account;
using POS_Shop.Views.Account.Auth;
using POS_Shop.Views.BillScreen;
using POS_Shop.Views.Controllers.Product;
using POS_Shop.Views.DB_Screens;
using POS_Shop.Views.LicenseManagement;
using POS_Shop.Views.Reports;
using POS_Shop.Views.Settings;
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
           
            this.KeyPreview = true; // Enable form to capture key events
            this.KeyDown += MasterLayoutForm_KeyDown;
            this.Load += MasterLayoutForm_Load;
        }

        private void MasterLayoutForm_Load(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.UserRole== AuthUserRole.Cashier.ToString())
            {
                // Restrict access to certain features for Cashier role
          
                userManagementToolStripMenuItem.Visible= false;
              
            }
            LoadHomeUI();
        }

        private void LoadHomeUI()
        {
            //MainPanel.Controls.Add(userCtrl);
            try
            {
                LoadingManager.ShowLoading();

                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);
                var userCtrl = new Views.Controllers.HomeControlUI();
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
            //MainPanel.Padding = new Padding(0);
            //MainPanel.Margin = new Padding(0);
            //var userCtrl = new Views.Controllers.HomeControlUI();
            //userCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel
            //MainPanel.Controls.Clear();
            //MainPanel.Controls.Add(userCtrl);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SessionManager.Logout();

            base.OnFormClosing(e);
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

                var ProductFormCtrl = new ProductListControl();
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
                //ImportExcelFile importExcelForm = new ImportExcelFile();
                //importExcelForm.Owner = this;
                //importExcelForm.Show();

                var importExcelForm = new ImportForm();
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
                var BillPadForm = new POS_Shop.Views.BillScreen.BillPadForm();
                //BillPadForm.Owner = this;
                //BillPadForm.Show();
                this.Hide();
                BillPadForm.ShowDialog();
                this.Show();

            LoadHomeUI();
        }
        private void MasterLayoutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.B && e.Control) // Ctrl + S to Save and Print
            {

                BillPadBtn.PerformClick();
            }
        }
        private void MainLogoImgBtn_Click(object sender, EventArgs e)
        {
            LoadHomeUI();
        }

        private void SaleReportBtn_Click(object sender, EventArgs e)
        {
            var saleChartForm = new SalesChartForm();
            saleChartForm.ShowDialog();
            this.Show();

        }

        private void importCustomerExcelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                ImportCustomerFile importExcelForm = new ImportCustomerFile();
                importExcelForm.Owner = this;
                importExcelForm.Show();
            }
            finally
            {
                LoadingManager.HideLoading();
            }
        }

        private void databaseSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Don't Remove this code, without testing this.
            //using (var settingsForm = new SettingsForm())
            //{
            //    settingsForm.ShowDialog();
            //    //LoadDatabaseInfo(); // Refresh status
            //}
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printersScannersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Views.Settings.PirntersAndScanners printerScannerForm = new PirntersAndScanners();
            printerScannerForm.Owner = this;
            printerScannerForm.Show();
        }

        private void systemConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var settingsForm = new POS_Shop.Views.Settings.ConfigSettingForm();
            settingsForm.ShowDialog();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new LicenseForm())
            {
                settingsForm.ShowDialog();
                //LoadDatabaseInfo(); // Refresh status
            }
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if user has permission
            if (Properties.Settings.Default.UserRole != AuthUserRole.SuperAdmin.ToString())
            {
                MessageBox.Show("Only administrators can manage users.",
                    "Access Denied",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var userManagementForm = new UserManagementForm();
            userManagementForm.ShowDialog();
        }

        private void ProductUnitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);

                var ProductUnitFormCtrl = new ProductUnitControl();
                ProductUnitFormCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(ProductUnitFormCtrl);
            }
            finally
            {
                LoadingManager.HideLoading();
            }
        }

        private void ProductListBtn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);

                var ProductFormCtrl = new ProductListControl();
                ProductFormCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(ProductFormCtrl);
            }
            finally
            {
                LoadingManager.HideLoading();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            LoadHomeUI();
        }

        private void SuppliersBtn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    LoadingManager.ShowLoading();
            //    MainPanel.Controls.Clear();
            //    MainPanel.Controls.Add(new Views.Controllers.Country.CountryControl1());
            //}
            //catch (Exception ex)
            //{

            //}
            //finally
            //{
            //    LoadingManager.HideLoading();
            //}

            try
            {
                LoadingManager.ShowLoading();
                MainPanel.Padding = new Padding(0);
                MainPanel.Margin = new Padding(0);

                var supplierCtrl = new Views.Controllers.Supplier.SupplierControl();
                supplierCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

                MainPanel.Controls.Clear();
                MainPanel.Controls.Add(supplierCtrl);
            }
            finally
            {
                LoadingManager.HideLoading();
            }


        }
    }
}
