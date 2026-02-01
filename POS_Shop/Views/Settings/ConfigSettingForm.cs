using POS_Shop.Helpers;
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

namespace POS_Shop.Views.Settings
{
    public partial class ConfigSettingForm : Form
    {

        public ConfigSettingForm()
        {
            InitializeComponent(); // This calls the designer-generated method
           // SetConfigPathLabel();
           LoadSettings();
        }
        private void LoadSettings()
        {
            try
            {
                var config = ConfigurationManager.Configuration;
                // Load feature settings
                chkEnableUpdateQty.Checked = config.Features.EnableUpdateQty;
                chkShowHideShopName.Checked = config.Features.ShowHideShopName;
                // Load invoice settings
                //txtShopName.Text = config.InvoiceSettings.ShopName ?? "";
                //txtShopAddress.Text = config.InvoiceSettings.ShopAddress ?? "";
                //txtContactNumber.Text = config.InvoiceSettings.ContactNumber ?? "";
                //txtEmail.Text = config.InvoiceSettings.Email ?? "";
                //txtTaxNumber.Text = config.InvoiceSettings.TaxNumber ?? "";
                //txtFooterMessage.Text = config.InvoiceSettings.FooterMessage ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                //if (string.IsNullOrWhiteSpace(txtShopName.Text))
                //{
                //    MessageBox.Show("Shop name is required.",
                //        "Validation Error",
                //        MessageBoxButtons.OK,
                //        MessageBoxIcon.Warning);
                //    txtShopName.Focus();
                //    return;
                //}

                var config = ConfigurationManager.Configuration;

                // Save features
                config.Features.EnableUpdateQty = chkEnableUpdateQty.Checked;
                config.Features.ShowHideShopName = chkShowHideShopName.Checked;

                //// Save invoice settings
                //config.InvoiceSettings.ShopName = txtShopName.Text.Trim();
                //config.InvoiceSettings.ShopAddress = txtShopAddress.Text.Trim();
                //config.InvoiceSettings.ContactNumber = txtContactNumber.Text.Trim();
                //config.InvoiceSettings.Email = txtEmail.Text.Trim();
                //config.InvoiceSettings.TaxNumber = txtTaxNumber.Text.Trim();
                //config.InvoiceSettings.FooterMessage = txtFooterMessage.Text.Trim();

                // Save to file
                ConfigurationManager.SaveConfiguration();

                MessageBox.Show("Settings saved successfully",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to reset all settings to default values?",
                "Reset Settings",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                ConfigurationManager.ResetToDefault();
                LoadSettings();
                MessageBox.Show("Settings have been reset to default values.",
                    "Reset Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void BtnOpenConfigFolder_Click(object sender, EventArgs e)
        {
            try
            {
                var configPath = ConfigurationManager.GetConfigFilePath();
                var folderPath = Path.GetDirectoryName(configPath);

                if (Directory.Exists(folderPath))
                {
                    System.Diagnostics.Process.Start("explorer.exe", folderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening folder: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnViewConfig_Click(object sender, EventArgs e)
        {
            try
            {
                var configPath = ConfigurationManager.GetConfigFilePath();

                if (File.Exists(configPath))
                {
                    System.Diagnostics.Process.Start("notepad.exe", configPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
