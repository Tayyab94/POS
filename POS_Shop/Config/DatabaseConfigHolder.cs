using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Config
{
    /// <summary>
    /// Runtime connection string holder.
    ///
    /// Ye ek simple static class hai jo connection string hold karta hai.
    /// Program.cs mein set hota hai, POSDbContext mein read hota hai.
    ///
    /// ─────────────────────────────────────────────────────────
    /// APNE POSDbContext MEIN YE CHANGES KAREIN:
    /// ─────────────────────────────────────────────────────────
    ///
    ///   public class POSDbContext : DbContext
    ///   {
    ///       // Parameterless constructor — runtime connection use karo
    ///       public POSDbContext()
    ///           : base(DatabaseConfigHolder.ConnectionString)   // ← ye line
    ///       {
    ///       }
    ///
    ///       // ... baqi sab waise hi rakho
    ///   }
    ///
    /// ─────────────────────────────────────────────────────────
    /// AGAR APP.CONFIG WALA APPROACH CHAHIYE:
    /// ─────────────────────────────────────────────────────────
    ///
    ///   Agar POSDbContext "name=POS_Shop" use karta hai to
    ///   ConfigManager.PatchAppConfig() method call karo jo
    ///   runtime mein ConfigurationManager ki connectionStrings
    ///   update kar deta hai. (Advanced — zaroorat par batao)
    ///
    /// </summary>
    public static class DatabaseConfigHolder
    {
        /// <summary>
        /// Program.cs mein ApplyDatabaseConfig() se set hoti hai.
        /// POSDbContext constructor mein base(ConnectionString) pass karo.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// DbContext ke liye ready hai?
        /// </summary>
        public static bool IsReady =>
            !string.IsNullOrWhiteSpace(ConnectionString);
    }
}
