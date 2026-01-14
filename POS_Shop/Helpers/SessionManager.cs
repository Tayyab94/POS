using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Helpers
{
    public static class SessionManager
    {
        public static bool IsLoggedIn => !string.IsNullOrEmpty(AuthToken);
        public static string AuthToken { get; set; }
        public static string UserName { get; set; }

        //public static void Login(string username)
        //{
        //    AuthToken = Guid.NewGuid().ToString();
        //    UserName = username;

        //    Properties.Settings.Default.IsLoggedIn = true;
        //    Properties.Settings.Default.UserRole = AuthToken;
        //    Properties.Settings.Default.UserName = username;

        //    Properties.Settings.Default.Save();
        //}

        public static void Login(string username, int userId, string userRole)
        {
            AuthToken = Guid.NewGuid().ToString();
            UserName = username;

            Properties.Settings.Default.IsLoggedIn = true;
            Properties.Settings.Default.UserRole = userRole;
            Properties.Settings.Default.UserName = username;
            Properties.Settings.Default.UserId = userId.ToString();
            Properties.Settings.Default.Save();
        }

        public static void Logout()
        {
            //AuthToken = string.Empty;
            //UserName = string.Empty;

            //Properties.Settings.Default.IsLoggedIn = false;
            //Properties.Settings.Default.UserRole = string.Empty;
            //Properties.Settings.Default.UserName = string.Empty;
            //Properties.Settings.Default.Save();


            AuthToken = string.Empty;
            UserName = string.Empty;

            Properties.Settings.Default.IsLoggedIn = false;
            Properties.Settings.Default.UserRole = string.Empty;
            Properties.Settings.Default.UserName = string.Empty;
            Properties.Settings.Default.UserId = string.Empty;
            Properties.Settings.Default.Save();
        }
            
       
    }
    public static class NavigationHelper
    {
        public static void OpenForm(Form currentForm, Form nextForm)
        {
            currentForm.Hide();

            nextForm.FormClosed += (s, args) =>
            {
                if (!currentForm.IsDisposed)
                {
                    currentForm.Show();
                }
            };

            nextForm.Show();

        }

        public static void ReturnToDashboard()
        {
            // Bring dashboard to front if it exists
            //var dashboard = Application.OpenForms.OfType<DashboardForm>().FirstOrDefault();
            //if (dashboard != null)
            //{
            //    dashboard.BringToFront();
            //}
            //else
            //{
            //    // Optional: Create new dashboard if none exists
            //    // var newDashboard = new YourDashboardForm();
            //    // newDashboard.Show();
            //}
        }
    }

}
