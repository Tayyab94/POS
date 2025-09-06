using POS_Shop.Helpers;
using POS_Shop.Views;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
          
            InitializeComponent();
            logoutBtn.Enabled = (bool)Properties.Settings.Default.IsLoggedIn ? true : false;
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
           
            SessionManager.Logout();
            foreach (Form form in Application.OpenForms.Cast<Form>().ToList())
            {
                if (form != this)
                    form.Close();
                //form.Close();
            }
        }

    



        private void CitySectionBtn_Click(object sender, EventArgs e)
        {
            if (IsFormOpen<CityForm>())
            {
                // Bring the existing form to front instead of Opening a new One
                var existingForm= Application.OpenForms.OfType<CityForm>().First();
                existingForm.BringToFront();
                return;
            }

            var cityForm = new CityForm();
            cityForm.FormClosed += (s, args) => UpdateButtonStates("CityForm");
            CitySectionBtn.Enabled = false;
            NavigationHelper.OpenForm(this, cityForm);
        }

        // Public method to update button states from child forms
        public void UpdateButtonStates(string formName)
        {
            //// Disable CategoryBtn if any CategoryForm is already open
            switch (formName)
            {
                case "CategoryForm":
                    //CategoryBtn.Enabled = !IsFormOpen<CategoryForm>();
                    break;
                case "CityForm":
                    CitySectionBtn.Enabled = !IsFormOpen<CityForm>();
                    break;
                default:
                    throw new ArgumentException("Unknown form name");
            }
         
        }

        // Helper method to check if a specific form type is open
        private bool IsFormOpen<T>() where T : Form
        {
            return Application.OpenForms.OfType<T>().Any();
        }

        private void X_CloseBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
