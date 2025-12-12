using POS_Shop.Helpers;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.DB_Screens
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        {
            txtCurrentPath.Text = DatabasePathManager.GetCurrentDatabaseInfo();

            string path = DatabasePathManager.GetDatabasePath();
            if (File.Exists(path))
            {
                var info = new FileInfo(path);
                lblFileInfo.Text = $"Size: {info.Length / 1024 / 1024} MB | " +
                                   $"Modified: {info.LastWriteTime:yyyy-MM-dd HH:mm}";
            }
        }

        private void btnChangeLocation_Click(object sender, EventArgs e)
        {
            if (DatabasePathManager.ChangeDatabaseLocation())
            {
                MessageBox.Show("Database location changed successfully!\n" +
                              "Please restart the application.",
                              "Success",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);


              //  LoadCurrentSettings();
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {
                string backupPath = DatabasePathManager.BackupDatabase();

                MessageBox.Show($"Backup created:\n{backupPath}",
                              "Backup Complete",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Backup failed: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            using (var db = new POSDbContext())
            {
                if (db.TestConnection())
                {
                    MessageBox.Show("✓ Database connection successful!\n\n" +
                                  db.GetDatabaseInfo(),
                                  "Connection Test",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("✗ Database connection failed!",
                                  "Connection Test",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
        }

        private void btnShowInExplorer_Click(object sender, EventArgs e)
        {

            string path = DatabasePathManager.GetDatabasePath();

            if (File.Exists(path))
            {
                string argument = $"/select, \"{path}\"";
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }
    }
}
