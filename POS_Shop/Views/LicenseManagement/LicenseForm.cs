using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Models.LicenseModels;
using POS_Shop.Models.LicenseModels.DTO;
using POS_Shop.Repositories;
using POS_Shop.Views.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.LicenseManagement
{
    public partial class LicenseForm : Form
    {
        private readonly ILicenseService _licenseService;

        public LicenseForm()
        {
            InitializeComponent();
            _licenseService = new LicenseService();
            this.Load += LicenseForm_Load;
        }

        private void LicenseForm_Load(object sender, EventArgs e)
        {
            // Check if we have a valid license already
            if (_licenseService.IsLicenseValid())
            {
                ShowLicenseInfo();
            }
            else
            {
                ShowActivationForm();
            }
        }

        private void ShowLicenseInfo()
        {
            var licenseInfo = _licenseService.GetCurrentLicenseInfo();
            if (licenseInfo != null)
            {
                txtLicenseInfo.Text = GenerateLicenseInfoText(licenseInfo);
                panelLicenseInfo.Visible = true;
                panelActivation.Visible = false;

                // Update button text based on license type
                if (licenseInfo.LicenseType == LicenseType.Trial)
                {
                    int remainingDays = _licenseService.GetRemainingDays();
                    btnContinue.Text = $"Continue ({remainingDays} days left)";

                    if (remainingDays <= 3)
                    {
                        btnContinue.BackColor = Color.Orange;
                    }
                }
            }
        }

        private string GenerateLicenseInfoText(LicenseInfo licenseInfo)
        {
            return $"👤 User Name: {licenseInfo.UserName}\n" +
                //  $"🔑 License Key: {licenseInfo.LicenseKey}\n" +
                   $"📋 License Type: {licenseInfo.LicenseType}\n" +
                   $"📅 Issue Date: {licenseInfo.IssueDate:dd/MM/yyyy HH:mm}\n" +
                   $"⏰ Expiry Date: {licenseInfo.ExpiryDate:dd/MM/yyyy HH:mm}\n" +
                   $"✅ Status: {(licenseInfo.IsValid ? "VALID" : "INVALID")}\n";
                   //+ $"🔗 MAC Address: {licenseInfo.MacAddress}\n" +
                   //$"🖥️ Hardware ID: {licenseInfo.HardwareId}";
        }

        private void ShowActivationForm()
        {
            panelActivation.Visible = true;
            panelLicenseInfo.Visible = false;
            txtUserName.Clear();
            txtLicenseKey.Clear();
            lblStatus.Text = "";
            txtUserName.Focus();

            if (_licenseService.IsSoftwareNewOrNot())
            {
                label1.Text = "Update key";
                label1.ForeColor = Color.Red;
            }
            else
            {
                label1.Text = "POS Software";
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            ActivateLicense();
        }

        private bool ActivateLicense()
        {
            string userName = txtUserName.Text.Trim();
            string licenseKey = txtLicenseKey.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(userName))
            {
                lblStatus.Text = "Please enter your name!";
                lblStatus.ForeColor = Color.Red;
                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(licenseKey))
            {
                lblStatus.Text = "Please enter license key!";
                lblStatus.ForeColor = Color.Red;
                txtLicenseKey.Focus();
                return false;
            }

            // Show processing
            lblStatus.Text = "Activating license...";
            lblStatus.ForeColor = Color.Blue;
            this.Cursor = Cursors.WaitCursor;
            btnActivate.Enabled = false;
            Application.DoEvents();

            try
            {
                // Perform activation
                bool activated = _licenseService.ActivateLicense(userName, licenseKey);

                if (activated)
                {
                    // Activation successful - close form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return true;
                }
                else
                {
                    lblStatus.Text = "Activation failed. Check key and try again.";
                    lblStatus.ForeColor = Color.Red;
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnActivate.Enabled = true;
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            // Continue to application
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnActivateNew_Click(object sender, EventArgs e)
        {
            ShowActivationForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtLicenseKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                ActivateLicense();
                e.Handled = true;
            }
        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtLicenseKey.Focus();
                e.Handled = true;
            }
        }

        private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Available License Types:\n\n" +
                "1. 🔓 TRIAL-XXXX-XXXX-XXXX - 15 days free trial\n" +
                "2. 📅 YEARLY-XXXX-XXXX-XXXX - 1 year license\n" +
                "3. ⭐ LIFETIME-XXXX-XXXX-XXXX - Permanent license\n\n" +
                "Contact support for license keys.",
                "License Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnCopyLicenseInfo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtLicenseInfo.Text))
            {
                Clipboard.SetText(txtLicenseInfo.Text);
                MessageBox.Show("License info copied to clipboard.",
                    "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to deactivate the license?\n\n" +
                "You will need to reactivate to use the application.",
                "Confirm Deactivation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _licenseService.DeleteLicenseFile();
                    MessageBox.Show("License deactivated. Restart to activate new license.",
                        "Deactivated",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Deactivation failed: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
