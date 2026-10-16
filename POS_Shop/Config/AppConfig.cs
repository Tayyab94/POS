using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Config
{
    /// <summary>
    /// Database connection configuration model.
    /// Ye class dbconfig.json mein serialize hoti hai.
    /// </summary>
    public class AppConfig
    {
        // ── DB Type ───────────────────────────────────────────────
        /// <summary>"SqlServer" ya "LocalDb"</summary>
        public string DatabaseType { get; set; }

        // ── SQL Server fields ─────────────────────────────────────
        /// <summary>e.g.  DESKTOP-XYZ\SQLEXPRESS</summary>
        public string ServerName { get; set; }

        /// <summary>e.g.  POS_Shop_DB</summary>
        public string DatabaseName { get; set; }

        /// <summary>"Windows" ya "SqlAuth"</summary>
        public string AuthType { get; set; }

        /// <summary>SQL Auth ke liye username (Windows Auth mein null)</summary>
        public string SqlUsername { get; set; }

        /// <summary>
        /// SQL Auth ke liye password.
        /// NOTE: Production mein encrypt karke store karo — abhi plain hai demo ke liye.
        /// </summary>
        public string SqlPassword { get; set; }

        // ── LocalDB fields ────────────────────────────────────────
        /// <summary>e.g.  C:\Data\POS.mdf</summary>
        public string LocalDbPath { get; set; }

        // ── Connection String Builder ─────────────────────────────

        /// <summary>
        /// EF + ADO.NET dono ke liye connection string build karta hai.
        /// Auth type automatically handle hoti hai.
        /// </summary>
        public string BuildConnectionString()
        {
            if (DatabaseType == "SqlServer")
            {
                string baseStr = $"Data Source={ServerName};Initial Catalog={DatabaseName};" +
                                 $"MultipleActiveResultSets=True;App=POS_Shop;";

                if (AuthType == "SqlAuth")
                {
                    // SQL Server Authentication
                    return baseStr +
                           $"Integrated Security=False;" +
                           $"User ID={SqlUsername};Password={SqlPassword};";
                }
                else
                {
                    // Windows Authentication (default)
                    return baseStr + "Integrated Security=True;";
                }
            }

            if (DatabaseType == "LocalDb")
            {
                string dbName = Path.GetFileNameWithoutExtension(LocalDbPath);
                return $@"Data Source=(LocalDB)\MSSQLLocalDB;" +
                       $@"AttachDbFilename={LocalDbPath};" +
                       $@"Database={dbName};" +
                       $@"Integrated Security=True;MultipleActiveResultSets=True;";
            }

            return string.Empty;
        }

        /// <summary>
        /// "Master" DB se connect karne ke liye connection string.
        /// Databases ki list fetch karne mein use hoti hai.
        /// </summary>
        public string BuildMasterConnectionString()
        {
            if (DatabaseType != "SqlServer") return string.Empty;

            string baseStr = $"Data Source={ServerName};Initial Catalog=master;" +
                             $"Connection Timeout=8;";

            if (AuthType == "SqlAuth")
                return baseStr + $"Integrated Security=False;User ID={SqlUsername};Password={SqlPassword};";
            else
                return baseStr + "Integrated Security=True;";
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(DatabaseType)) return false;

            if (DatabaseType == "SqlServer")
            {
                bool baseOk = !string.IsNullOrWhiteSpace(ServerName) &&
                              !string.IsNullOrWhiteSpace(DatabaseName);

                if (AuthType == "SqlAuth")
                    return baseOk &&
                           !string.IsNullOrWhiteSpace(SqlUsername) &&
                           !string.IsNullOrWhiteSpace(SqlPassword);

                return baseOk; // Windows Auth
            }

            if (DatabaseType == "LocalDb")
                return !string.IsNullOrWhiteSpace(LocalDbPath) &&
                       System.IO.File.Exists(LocalDbPath);

            return false;
        }
    }
}
