using POS_Shop.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Helpers
{
    /// <summary>
    /// Manage the database file paths and related configurations. with one-time setup methods.
    /// </summary>
    public static class DatabasePathManager
    {

        private static string _cachedPath = null;
        private static bool _isInitialized = false;
        private static readonly object _lock = new object();

        /// <summary>
        /// Initializes database connection - called once at app startup
        /// </summary>
        

        public static bool Initialize()
        {
            lock(_lock)
            {
                if (_isInitialized)
                    return true;

                try
                {
                    string dbPath = GetDatabasePath();
                    if (string.IsNullOrEmpty(dbPath))
                        return false;

                    if(TestConnection(dbPath))
                    {
                        //_cachedPath = dbPath;
                        _isInitialized = true;
                        return true;
                    }else
                    {
                        // Connection failed - reset path and retry
                        ResetDatabasePath();
                        return Initialize(); // Recursive retry
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to initialize database: {ex.Message}",
                               "Initialization Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets database path - uses saved path or asks user ONCE
        /// </summary>
        public static string GetDatabasePath()
        {
            // Return cached path if available
            if(_cachedPath!= null && File.Exists(_cachedPath))
               return _cachedPath;

            // Check the Save User Setting

            string savedPath = Properties.Settings.Default.DatabasePath;
            if(!string.IsNullOrEmpty(savedPath) && File.Exists(savedPath))
            {
                _cachedPath = savedPath;
                return _cachedPath;
            }

            // FIRST TIME: Database not found in saved location
            return AskUserForDatabasePath();
        }

        /// <summary>
        /// Asks user to locate database file (ONE-TIME only)
        /// </summary>
        private static string AskUserForDatabasePath()
        {
            // Show friendly first-time setup message
            DialogResult result = MessageBox.Show(
                "Welcome to POS System!\n\n" +
                "This is your first time running the application.\n" +
                "Please locate your 'ShopPOSDB.mdf' database file.\n\n" +
                "This is a ONE-TIME setup. The application will remember this location forever.",
                "First-Time Setup",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);

            if (result == DialogResult.Cancel)
            {
                // User doesn't want to continue
                Application.Exit();
                return null;
            }

            // Let user browse for database file
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Locate ShopPOSDB.mdf Database File";
                dialog.Filter = "SQL Server Database (*.mdf)|*.mdf|All Files (*.*)|*.*";
                dialog.FileName = "ShopPOSDB.mdf";
                dialog.CheckFileExists = true;
                dialog.CheckPathExists = true;

                // Start in common locations
                string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                dialog.InitialDirectory = exePath;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Verify it's a valid SQL Server database
                    if (IsValidDatabaseFile(dialog.FileName))
                    {
                        // SAVE FOREVER in user settings
                        Settings.Default.DatabasePath = dialog.FileName;
                        Settings.Default.Save();

                        _cachedPath = dialog.FileName;

                        // Show confirmation
                        MessageBox.Show(
                            $"✓ Database location saved successfully!\n\n" +
                            $"Location: {dialog.FileName}\n\n" +
                            $"The application will use this location automatically every time.",
                            "Setup Complete",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        return dialog.FileName;
                    }
                    else
                    {
                        MessageBox.Show(
                            "The selected file doesn't appear to be a valid SQL Server database.\n" +
                            "Please select a valid 'ShopPOSDB.mdf' file.",
                            "Invalid File",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        // Try again
                        return AskUserForDatabasePath();
                    }
                }
                else
                {
                    // User cancelled file dialog
                    MessageBox.Show(
                        "Database setup was cancelled. The application cannot run without a database.",
                        "Setup Cancelled",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    Application.Exit();
                    return null;
                }
            }
        }


        /// <summary>
        /// Tests if the file is a valid SQL Server database
        /// </summary>
        private static bool IsValidDatabaseFile(string filePath)
        {
            try
            {
                // Basic checks
                if (!File.Exists(filePath)) return false;
                if (!filePath.EndsWith(".mdf", StringComparison.OrdinalIgnoreCase)) return false;

                // Try to attach it to verify it's a valid database
                string testConnection = $@"Data Source=(LocalDB)\MSSQLLocalDB;
                                         AttachDbFilename={filePath};
                                         Integrated Security=True;
                                         Connect Timeout=5";

                using (var connection = new SqlConnection(testConnection))
                {
                    connection.Open();

                    // Try a simple query to verify it's our database
                    using (var command = new SqlCommand(
                        "SELECT CASE WHEN EXISTS (SELECT * FROM sys.tables WHERE name IN ('Products', 'Orders', 'Cities')) THEN 1 ELSE 0 END",
                        connection))
                    {
                        object result = command.ExecuteScalar();
                        return result != null && Convert.ToInt32(result) == 1;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tests connection to database
        /// </summary>
        private static bool TestConnection(string dbPath)
        {
            try
            {
                string connectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;
                                           AttachDbFilename={dbPath};
                                           Integrated Security=True;
                                           Connect Timeout=30;
                                           MultipleActiveResultSets=True";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Verify it's our database by checking for a known table
                    using (var command = new SqlCommand(
                        "SELECT COUNT(*) FROM sys.tables WHERE name IN ('Products', 'Cities')",
                        connection))
                    {
                        int tableCount = (int)command.ExecuteScalar();
                        return tableCount >= 2; // At least Products and Sales tables
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot connect to database:\n{ex.Message}\n\n" +
                              "The database might have been moved or is corrupted.",
                              "Connection Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return false;
            }
        }


        /// <summary>
        /// Gets the Entity Framework connection string
        /// </summary>
        public static string GetConnectionString()
        {
            string dbPath = GetDatabasePath();

            if (string.IsNullOrEmpty(dbPath))
                throw new InvalidOperationException("Database path not set");

            return $@"Data Source=(LocalDB)\MSSQLLocalDB;
                     AttachDbFilename={dbPath};
                     Integrated Security=True;
                     Connect Timeout=30;
                     MultipleActiveResultSets=True";
        }

        /// <summary>
        /// Resets the database path (forces user to select again)
        /// </summary>
        public static void ResetDatabasePath()
        {
            Settings.Default.DatabasePath = "";
            Settings.Default.Save();
            _cachedPath = null;
            _isInitialized = false;
        }

        /// <summary>
        /// Gets current database location (for display purposes)
        /// </summary>
        public static string GetCurrentDatabaseInfo()
        {
            string path = GetDatabasePath();

            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return "Database: Not set";

            var fileInfo = new FileInfo(path);
            return $"Database: {Path.GetFileName(path)}\n" +
                   $"Location: {Path.GetDirectoryName(path)}\n" +
                   $"Size: {fileInfo.Length / 1024 / 1024} MB\n" +
                   $"Last Modified: {fileInfo.LastWriteTime:yyyy-MM-dd HH:mm}";
        }

        /// <summary>
        /// Changes database location (optional - for settings)
        /// </summary>
        public static bool ChangeDatabaseLocation()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Select New Database File";
                dialog.Filter = "SQL Server Database (*.mdf)|*.mdf";
                dialog.FileName = "ShopPOSDB.mdf";
                dialog.CheckFileExists = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (IsValidDatabaseFile(dialog.FileName))
                    {
                        // Save new location
                        Settings.Default.DatabasePath = dialog.FileName;
                        Settings.Default.Save();
                        _cachedPath = dialog.FileName;
                        _isInitialized = false;

                        // Re-initialize with new path
                        return Initialize();
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// Creates backup of current database
        /// </summary>
        public static string BackupDatabase()
        {
            string dbPath = GetDatabasePath();

            if (string.IsNullOrEmpty(dbPath) || !File.Exists(dbPath))
                throw new FileNotFoundException("Database file not found");

            string backupDir = Path.Combine(Path.GetDirectoryName(dbPath), "Backups");
            Directory.CreateDirectory(backupDir);

            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupFile = Path.Combine(backupDir, $"ShopPOSDB_Backup_{timestamp}.mdf");

            // Copy database file
            File.Copy(dbPath, backupFile, true);

            // Copy log file if exists
            string logFile = dbPath.Replace(".mdf", "_log.ldf");
            string backupLogFile = backupFile.Replace(".mdf", "_log.ldf");

            if (File.Exists(logFile))
                File.Copy(logFile, backupLogFile, true);

            return backupFile;
        }
    }
}
