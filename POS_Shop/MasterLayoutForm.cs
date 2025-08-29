using POS_Shop.Helpers;
using POS_Shop.Views.Account;
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
        }

        private void cityBtn_Click(object sender, EventArgs e)
        {
           
            MainPanel.Padding = new Padding(0);
            MainPanel.Margin = new Padding(0);

            var userCtrl = new Views.Controllers.City.CityControl();
            userCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(userCtrl);
        }

        private void CountryBtn_Click(object sender, EventArgs e)
        {
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(new Views.Controllers.Country.CountryControl1());
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

        private void backupDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            BackUpForm backupForm = new BackUpForm();
            backupForm.Owner = this;
            backupForm.Show();
        }

        private void restoreDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            RestoreDbForm restoreDbForm = new RestoreDbForm();
            restoreDbForm.Owner = this;
            restoreDbForm.Show();
        }

        private void CategoryBtn_Click(object sender, EventArgs e)
        {
            MainPanel.Padding = new Padding(0);
            MainPanel.Margin = new Padding(0);

            var categoryCtrl = new Views.Controllers.Category.CategoryControl();
            categoryCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(categoryCtrl);
        }

        private void SubCategoryBtn_Click(object sender, EventArgs e)
        {
            MainPanel.Padding = new Padding(0);
            MainPanel.Margin = new Padding(0);

            var subcategoryCtrl = new Views.Controllers.SubCategory.SubCategoryForm();
            subcategoryCtrl.Dock = DockStyle.Fill; // Ensures it fills the panel

            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(subcategoryCtrl);
        }
    }
}
