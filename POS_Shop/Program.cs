using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Models.LicenseModels;
using POS_Shop.Repositories;
using POS_Shop.Services;
using POS_Shop.Views.Account;
using POS_Shop.Views.LicenseManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop
{
    internal static class Program
    {
        private static Mutex _mutex;
        private static DailyBackgroundService _backgroundService;


        private static readonly ILicenseService _licenseService = new LicenseService();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Ensure only one instance runs
            bool createdNew;
            _mutex = new Mutex(true, "POSAppInstance", out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("Application is already running!",
                              "POS System",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

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

            // Show splash screen (optional)
            ShowSplashScreen();

            // Initialize database - THIS HAPPENS ONCE PER INSTALLATION
            if (!InitializeApplication())
            {
                // Initialization failed
                return;
            }

            // Initialize background service
            InitializeBackgroundService();


            InitializeCountryCityDbCheck();
            InitializeCategorySubcategoryDbCheck();

            InitializeLic();


            // Check license on startup
            if (!CheckLicense())
            {
                // Show license activation form
                //using (var licenseForm = new ActivationLicenseForm())
                using (var licenseForm = new LicenseForm())
                {
                    licenseForm.ShowDialog();

                    // 
                    //if (licenseForm.ShowDialog() != DialogResult.OK)
                    //{
                    //    MessageBox.Show("Application requires a valid license to run.\n\n" +
                    //                  "Please contact support for a license key.",
                    //                  "License Required",
                    //                  MessageBoxButtons.OK,
                    //                  MessageBoxIcon.Warning);
                    //    return;
                    //}

                    ShowLoginAndMainApplication();
                }
            }else
            {
                ShowLoginAndMainApplication();
            }

               

            // Keep mutex alive
            GC.KeepAlive(_mutex);
            _backgroundService?.Dispose();

        }


        private static void ShowLoginAndMainApplication()
        {
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

                // Show main profile form (MasterLayoutForm)
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
        }

        static void InitializeLic()
        {
            using(var context = new POSDbContext())
            {
                if (!context.Licenses.Any())
                {
                    context.Licenses.AddOrUpdate(
                        new AppLicense
                        {
                            UserName = "Admin",
                            LicenseKey = "TRIAL-1234-5678-9012",
                            MacAddress = "00-00-00-00-00-00",
                            HardwareId = "sample-hardware-id",
                            LicenseType = LicenseType.Trial,
                            IssueDate = DateTime.Now,
                            ExpiryDate = DateTime.Now.AddDays(15),
                            IsActive = true
                        },
                        new AppLicense
                        {
                            UserName = "Admin",
                            LicenseKey = "YEARLY-ABCD-EFGH-IJKL",
                            MacAddress = "00-00-00-00-00-00",
                            HardwareId = "sample-hardware-id",
                            LicenseType = LicenseType.OneYear,
                            IssueDate = DateTime.Now,
                            ExpiryDate = DateTime.Now.AddYears(1),
                            IsActive = true
                        },
                        new AppLicense
                        {
                            UserName = "Admin",
                            LicenseKey = "LIFETIME-MNOP-QRST-UVWX",
                            MacAddress = "00-00-00-00-00-00",
                            HardwareId = "sample-hardware-id",
                            LicenseType = LicenseType.Lifetime,
                            IssueDate = DateTime.Now,
                            ExpiryDate = DateTime.MaxValue,
                            IsActive = true
                        }
                    );

                    context.SaveChanges();
                }
            }
            // Seed sample data
          
        }

        private static bool CheckLicense()
        {
            try
            {
                if (!_licenseService.CheckLicenseFileExists())
                {
                    // No license file found
                    return false;
                }

                var licenseInfo = _licenseService.ReadLicenseFile();
                if (licenseInfo == null || !licenseInfo.IsValid)
                {
                    // Show appropriate message
                    if (licenseInfo != null)
                    {
                        if (licenseInfo.LicenseType == LicenseType.Trial)
                        {
                            int remaining = _licenseService.GetRemainingDays();
                            if (remaining <= 0)
                            {
                                MessageBox.Show($"Your trial period has expired.\n\n" +
                                              $"Please purchase a full license to continue using the software.",
                                              "Trial Expired",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Your license has expired or is invalid.\n\n" +
                                          $"Please renew your license to continue.",
                                          "License Expired",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                        }
                    }
                    return false;
                }

                // Show remaining days for trial
                if (licenseInfo.LicenseType == LicenseType.Trial)
                {
                    int remainingDays = _licenseService.GetRemainingDays();
                    if (remainingDays <= 3)
                    {
                        MessageBox.Show($"Trial Version\n" +
                                      $"Remaining Days: {remainingDays}\n\n" +
                                      $"Please purchase a full license before your trial expires.",
                                      "Trial Version",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"License validation error: {ex.Message}",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }



        /// <summary>
        /// Shows splash screen (optional)
        /// </summary>
        private static void ShowSplashScreen()
        {
            // You can create a simple splash form or skip this
            // For now, just show loading cursor
            Cursor.Current = Cursors.WaitCursor;
        }

        /// <summary>
        /// Initializes application and database
        /// </summary>
        public static bool InitializeApplication()
        {
            try
            {
                // This will:
                // 1. First time: Ask user to locate database (ONCE)
                // 2. Subsequent times: Use saved path automatically

                if (!DatabasePathManager.Initialize())
                {
                    MessageBox.Show("Application cannot start without a valid database.",
                              "Initialization Failed",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                    return false;
                }

                // Test Entity Framework connection
                using (var db = new POSDbContext())
                {
                    if (!db.TestConnection())
                    {
                        MessageBox.Show("Cannot connect to database using Entity Framework.",
                                      "Database Error",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                        return false;
                    }

                    // Optional: Log successful initialization
                    Console.WriteLine("✓ Database initialized successfully");
                    Console.WriteLine($"✓ {db.GetDatabaseInfo()}");
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application initialization failed:\n{ex.Message}",
                             "Fatal Error",
                             MessageBoxButtons.OK,
                             MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private static void InitializeCountryCityDbCheck()
        {
            using (var context = new POSDbContext())
            {
                var HasCountry = context.Countries.Any();
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
                        CountryId = countryId
                    });

                    context.SaveChanges();

                }

            }
        }


        private static void InitializeCategorySubcategoryDbCheck()
        {
            using (var context = new POSDbContext())
            {
                var HasCategory = context.Categories.Any();
                var HasSubcategory = context.SubCategories.Any();

                if (!HasCategory)
                {

                    context.Categories.Add(new Category()
                    {
                        name = "Other Category",
                         isActive=true
                    });

                    context.SaveChanges();

                }

                if (!HasSubcategory)
                {
                    var categoryId = context.Categories.FirstOrDefault(x => x.name == "Other Category").id;
                    context.SubCategories.Add(new SubCategory()
                    {
                        name = "Other Subcategory",
                        isActive = true,
                        categoryId= categoryId
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


#region New Code without ConnectionString Configured Exception Handling

//    internal static class Program
//    {
//        private static Mutex _mutex;
//        private static DailyBackgroundService _backgroundService;
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {

//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            // Set the unhandled exception mode for the UI thread
//            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
//            // Subscribe to the event for unhandled UI thread exceptions
//            Application.ThreadException += Application_ThreadException;

//            // Subscribe to the event for unhandled non-UI thread exceptions
//            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);

//            // Initialize background service
//            InitializeBackgroundService();


//            InitializeCountryCityDbCheck();
//            while (true)
//            {
//                // Show login if not authenticated
//                if (!Properties.Settings.Default.IsLoggedIn ||
//                    string.IsNullOrEmpty(Properties.Settings.Default.AuthToken))
//                {
//                    using (var login = new LoginForm())
//                    {
//                        if (login.ShowDialog() != DialogResult.OK)
//                        {
//                            _backgroundService?.Dispose();
//                            return;  // Exit application if login cancelled
//                        }
//                    }
//                }

//                // Show main profile form
//                using (var profile = new MasterLayoutForm())
//                {
//                    Application.Run(profile);

//                    // If we get here, profile form was closed
//                    if (!Properties.Settings.Default.IsLoggedIn)
//                    {
//                        continue; // Show login again if logged out
//                    }
//                    else
//                    {
//                        break; // Exit application if profile closed normally
//                    }

//                }

//            }

//        }


//        private static void InitializeCountryCityDbCheck()
//        {
//            using (var context = new POSDbContext())
//            {
//                var HasCountry = context.Countries.Any();
//                var HasCity = context.Cities.Any();

//                if (!HasCountry)
//                {

//                    context.Countries.Add(new Country()
//                    {
//                        CountryName = "Pakistan",
//                        IsActive = true
//                    });

//                    context.SaveChanges();

//                }

//                if (!HasCity)
//                {
//                    var countryId = context.Countries.FirstOrDefault(x => x.CountryName == "Pakistan").Id;
//                    context.Cities.Add(new City()
//                    {
//                        Name = "Gujranwala",
//                        IsActive = true,
//                        CountryId = countryId
//                    });

//                    context.SaveChanges();

//                }

//            }
//        }

//        private static void InitializeBackgroundService()
//        {
//            try
//            {
//                _backgroundService = new DailyBackgroundService();
//                Logger.LogMessage("Background Service started.");
//            }
//            catch (Exception ex)
//            {

//                Logger.LogMessage($"Failed to Initialze background service: {ex.Message}");

//                throw;
//            }
//        }

//        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
//        {
//            // Log the exception using the Logger class
//            Logger.LogException(e.Exception);
//            MessageBox.Show("An unexpected error occurred. A log has been created.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            // Decide if you want to exit or continue. Continuing can be dangerous.
//            // Application.Exit();
//        }

//        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
//        {
//            // Log the exception using the Logger class
//            Logger.LogException(e.ExceptionObject as Exception);
//            MessageBox.Show("A critical error has occurred. The application will now close.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
//            Environment.Exit(1);
//        }
//    }

#endregion
