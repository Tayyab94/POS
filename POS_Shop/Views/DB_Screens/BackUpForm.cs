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
    public partial class BackUpForm : Form
    {
        public BackUpForm()
        {
            InitializeComponent();
        }

        private void BrowsFileBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog  fbd= new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                BrowsFilePathTxt.Text = fbd.SelectedPath;
                BackupBtn.Enabled = true;
            }
        }

        private void BackupBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BrowsFilePathTxt.Text.Trim()))
            {
                try
                {
                    using(SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["POSDbConnectionstring"].ConnectionString))
                    {
                        conn.Open();
                        string backupFileName = $"DatabaseBackup_{DateTime.Now:yyyyMMddHHmmss}.bak";
                        string backupFilePath = System.IO.Path.Combine(BrowsFilePathTxt.Text, backupFileName);
                        string sqlQueryString = $"BACKUP DATABASE POSDB TO DISK = '{backupFilePath}'";
                        using (SqlCommand cmd = new SqlCommand(sqlQueryString, conn))
                        {
                            if(conn.State != ConnectionState.Open)
                            {
                                conn.Open();
                            }
                            cmd.CommandTimeout = 0;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    // Assuming you have a method to perform the backup
                    MessageBox.Show("Database backup completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a valid folder path.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BackScreenBtn_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

      
    }
}
