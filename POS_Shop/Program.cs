using POS_Shop.Views.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            while (true)
            {
                // Show login if not authenticated
                if (!Properties.Settings.Default.IsLoggedIn ||
                    string.IsNullOrEmpty(Properties.Settings.Default.AuthToken))
                {
                    using (var login = new LoginForm())
                    {
                        if (login.ShowDialog() != DialogResult.OK)
                            return; // Exit application if login cancelled
                    }
                }

                // Show main profile form
                using (var profile = new MainForm())
                {
                    Application.Run(profile);

                    // If we get here, profile form was closed
                    if (!Properties.Settings.Default.IsLoggedIn)
                    {
                        continue; // Show login again if logged out
                    }
                    else
                    {
                        break; // Exit application if profile closed normally
                    }

                }

            }

        }
    }
}
