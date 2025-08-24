using POS_Shop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace POS_Shop.Views.Account
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
          
            InitializeComponent();
            userNamelbl.AutoSize = true;
            userPasswordLbl.AutoSize = true;
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            // Simple validation
            if (string.IsNullOrWhiteSpace(userEmailTxt.Text) ||
                string.IsNullOrWhiteSpace(UserPasswordTxt.Text))
            {
                MessageBox.Show("Please enter both username and password", "Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (AuthenticateUser(userEmailTxt.Text, UserPasswordTxt.Text))
            {

                SessionManager.Login(userEmailTxt.Text);

                //Properties.Settings.Default.IsLoggedIn = true;
                //Properties.Settings.Default.AuthToken = GenerateToken();
                //Properties.Settings.Default.UserName = userEmailTxt.Text;
                //Properties.Settings.Default.Save();


                // this.Hide(); // Hide login form instead of closing if you might need it again
                //UserProfile profileForm = new UserProfile();
                //profileForm.Show();


                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            else
            {
                MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private bool AuthenticateUser(string username, string password)
        {
            // This is a mock authentication - replace with real logic
            return username == "admin" && password == "password";
        }

        private string GenerateToken()
        {
            // In a real app, this would come from your authentication server
            return Guid.NewGuid().ToString();
        }

    
    }
}
