using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Helpers
{
    /// <summary>
    /// SQL Server instances + databases dhundhta hai.
    /// Background thread mein call karo — UI freeze na ho.
    /// </summary>
    public static class SqlServerHelper
    {
        // ══════════════════════════════════════════════════════════
        //  SERVER LIST
        // ══════════════════════════════════════════════════════════

        /// <summary>
        /// Local machine par installed SQL Server instances return karta hai.
        /// Registry → SMO → Common Defaults (fallback chain)
        /// </summary>
        public static List<string> GetLocalSqlServerInstances()
        {
            var result = new List<string>();

            result.AddRange(GetFromRegistry());

            if (result.Count == 0)
                result.AddRange(GetFromSmo());

            if (result.Count == 0)
                result.AddRange(GetCommonDefaults());

            // Deduplicate
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var clean = new List<string>();
            foreach (var s in result)
                if (seen.Add(s)) clean.Add(s);

            return clean;
        }

        // ══════════════════════════════════════════════════════════
        //  DATABASE LIST  (credentials ke saath)
        // ══════════════════════════════════════════════════════════

        /// <summary>
        /// Diye gaye server + credentials se user databases ki list fetch karta hai.
        /// Windows Auth ke liye sqlUser/sqlPass null pass karo.
        /// Returns: (databases list, error message)
        /// </summary>
        public static (List<string> Databases, string Error) GetDatabases(
            string serverName,
            bool windowsAuth,
            string sqlUser = null,
            string sqlPass = null)
        {
            var list = new List<string>();

            try
            {
                string connStr = BuildMasterConnStr(serverName, windowsAuth, sqlUser, sqlPass);

                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    const string sql = @"
                        SELECT name
                        FROM   sys.databases
                        WHERE  name NOT IN ('master','tempdb','model','msdb')
                          AND  state_desc = 'ONLINE'
                        ORDER  BY name";

                    using (var cmd = new SqlCommand(sql, conn))
                    using (var rdr = cmd.ExecuteReader())
                        while (rdr.Read())
                            list.Add(rdr.GetString(0));
                }

                return (list, null);
            }
            catch (Exception ex)
            {
                return (list, ex.Message);
            }
        }

        // ══════════════════════════════════════════════════════════
        //  CONNECTION TEST
        // ══════════════════════════════════════════════════════════

        /// <summary>
        /// Full connection string se test karta hai.
        /// Returns: (success, errorMessage)
        /// </summary>
        public static (bool Success, string Error) TestConnection(string connectionString)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return (true, null);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        // ══════════════════════════════════════════════════════════
        //  PRIVATE HELPERS
        // ══════════════════════════════════════════════════════════

        private static string BuildMasterConnStr(
            string serverName,
            bool windowsAuth,
            string sqlUser,
            string sqlPass)
        {
            string baseStr = $"Data Source={serverName};Initial Catalog=master;Connection Timeout=8;";

            if (windowsAuth)
                return baseStr + "Integrated Security=True;";
            else
                return baseStr + $"Integrated Security=False;User ID={sqlUser};Password={sqlPass};";
        }

        private static List<string> GetFromRegistry()
        {
            var list = new List<string>();
            string mach = Environment.MachineName;

            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(
                    @"SOFTWARE\Microsoft\Microsoft SQL Server"))
                    ReadInstances(key, mach, list);

                if (list.Count == 0)
                    using (var key = Registry.LocalMachine.OpenSubKey(
                        @"SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server"))
                        ReadInstances(key, mach, list);
            }
            catch { }

            return list;
        }

        private static void ReadInstances(RegistryKey key, string machine, List<string> list)
        {
            if (key == null) return;
            var instances = key.GetValue("InstalledInstances") as string[];
            if (instances == null) return;

            foreach (var inst in instances)
            {
                if (string.IsNullOrWhiteSpace(inst)) continue;
                list.Add(inst.Equals("MSSQLSERVER", StringComparison.OrdinalIgnoreCase)
                    ? machine
                    : $@"{machine}\{inst}");
            }
        }

        private static List<string> GetFromSmo()
        {
            var list = new List<string>();
            try
            {
                var asm = System.Reflection.Assembly.Load(
                    "Microsoft.SqlServer.Smo, Version=16.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
                var smoApp = asm?.GetType("Microsoft.SqlServer.Management.Smo.SmoApplication");
                var method = smoApp?.GetMethod("EnumAvailableSqlServers", new[] { typeof(bool) });
                if (method != null)
                {
                    var dt = method.Invoke(null, new object[] { false }) as System.Data.DataTable;
                    if (dt != null)
                        foreach (System.Data.DataRow row in dt.Rows)
                        {
                            string n = row["Name"]?.ToString();
                            if (!string.IsNullOrWhiteSpace(n)) list.Add(n);
                        }
                }
            }
            catch { }
            return list;
        }

        private static List<string> GetCommonDefaults()
        {
            string m = Environment.MachineName;
            return new List<string>
            {
                $@"{m}\SQLEXPRESS",
                $@"{m}\MSSQLSERVER",
                $@"{m}\SQL2019",
                $@"{m}\SQL2022",
                m,
                @".\SQLEXPRESS",
                "(local)",
                "localhost"
            };
        }
    }
}
