using POS_Shop.Helpers;
using POS_Shop.Views.Account;
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
            MainPanel.Controls.Clear();
            MainPanel.Controls.Add(new Views.Controllers.City.CityControl());
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
    }
}
