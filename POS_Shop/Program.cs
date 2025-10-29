using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Services;
using POS_Shop.Views.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop
{
    internal static class Program
    {
        private  static DailyBackgroundService _backgroundService;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set the unhandled exception mode for the UI thread
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            // Subscribe to the event for unhandled UI thread exceptions
            Application.ThreadException += Application_ThreadException;

            // Subscribe to the event for unhandled non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize background service
            InitializeBackgroundService();


            InitializeCountryCityDbCheck();
            while (true)
            {
                // Show login if not authenticated
                if (!Properties.Settings.Default.IsLoggedIn ||
                    string.IsNullOrEmpty(Properties.Settings.Default.AuthToken))
                {
                    using (var login = new LoginForm())
                    {
                        if (login.ShowDialog() != DialogResult.OK)
                        {
                            _backgroundService?.Dispose();
                            return;  // Exit application if login cancelled
                        }
                    }
                }

                // Show main profile form
                using (var profile = new MasterLayoutForm())
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

            _backgroundService?.Dispose();

        }


        private static void InitializeCountryCityDbCheck()
        {
            using (var context = new POSDbContext())
            {
                var HasCountry= context.Countries.Any();
                var HasCity = context.Cities.Any();

                if (!HasCountry)
                {
                    
                    context.Countries.Add(new Country()
                    {
                        CountryName = "Pakistan",
                        IsActive = true
                    });

                    context.SaveChanges();
                 
                }

                if (!HasCity)
                {
                    var countryId = context.Countries.FirstOrDefault(x => x.CountryName == "Pakistan").Id;
                    context.Cities.Add(new City()
                    {
                        Name = "Gujranwala",
                        IsActive = true,
                        CountryId=countryId
                    });

                    context.SaveChanges();

                }

            }
        }

        private static void InitializeBackgroundService()
        {
            try
            {
                _backgroundService = new DailyBackgroundService();
                Logger.LogMessage("Background Service started.");
            }
            catch (Exception ex)
            {

                Logger.LogMessage($"Failed to Initialze background service: {ex.Message}");

                throw;
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception using the Logger class
            Logger.LogException(e.Exception);
            MessageBox.Show("An unexpected error occurred. A log has been created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Decide if you want to exit or continue. Continuing can be dangerous.
            // Application.Exit();
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception using the Logger class
            Logger.LogException(e.ExceptionObject as Exception);
            MessageBox.Show("A critical error has occurred. The application will now close.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Environment.Exit(1);
        }
    }
}
