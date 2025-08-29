using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.DB_Screens
{
    public partial class RestoreDbForm : Form
    {
        public RestoreDbForm()
        {
            InitializeComponent();
        }

        private void BackScreenBtn_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void BrowsFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // Set the filter to show only .bak files
            ofd.Filter = "Backup Files (*.bak)|*.bak";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                RestoreFilePathTxt.Text = ofd.FileName;
                RestoreDatabaseBtn.Enabled = true;
            }
        }

        private void RestoreDatabaseBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(RestoreFilePathTxt.Text.Trim()))
            {
                try
                {
                    string sqlcommand = $"use master; ALTER Database POSDB SET Single_User With RollBack Immediate;";

                    sqlcommand += $"RESTORE DATABASE POSDB FROM DISK = '{RestoreFilePathTxt.Text}' WITH REPLACE;";
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POSDbConnectionstring"].ConnectionString))
                    {
                        conn.Open();
                        
                        using (SqlCommand cmd = new SqlCommand(sqlcommand, conn))
                        {
                            if (conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
                            cmd.CommandTimeout = 0;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    // Assuming you have a method to perform the backup
                    MessageBox.Show("Database Restore completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during Restore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid file path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
