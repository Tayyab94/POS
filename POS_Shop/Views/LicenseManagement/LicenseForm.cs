using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Interfaces;
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
        }

        private void LicenseForm_Load(object sender, EventArgs e)
        {
            LoadLicenseStatus();
        }

        private void LoadLicenseStatus()
        {
            try
            {
                // Check if license already exists and is valid
                if (_licenseService.IsLicenseValid())
                {
                    ShowLicenseInfo();
                }
                else
                {
                    ShowActivationForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading license information: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowLicenseInfo()
        {
            try
            {
                var licenseInfo = _licenseService.GetCurrentLicenseInfo();
                if (licenseInfo != null)
                {
                    txtLicenseInfo.Text = GenerateLicenseInfoText(licenseInfo);

                    panelLicenseInfo.Visible = true;
                    panelActivation.Visible = false;

                    // Update button based on license status
                    if (licenseInfo.LicenseType == LicenseType.Trial)
                    {
                        int remainingDays = _licenseService.GetRemainingDays();
                        btnContinue.Text = $"Continue (Trial: {remainingDays} days left)";

                        if (remainingDays <= 3)
                        {
                            btnContinue.BackColor = System.Drawing.Color.Orange;
                        }
                    }
                    else
                    {
                        btnContinue.Text = "Continue to Application";
                    }
                }
                else
                {
                    ShowActivationForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error showing license info: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowActivationForm();
            }
        }

        private string GenerateLicenseInfoText(LicenseInfo licenseInfo)
        {
            return $"User Name: {licenseInfo.UserName}\n" +
                   $"License Key: {licenseInfo.LicenseKey}\n" +
                   $"License Type: {licenseInfo.LicenseType}\n" +
                   $"Issue Date: {licenseInfo.IssueDate:dd/MM/yyyy HH:mm}\n" +
                   $"Expiry Date: {licenseInfo.ExpiryDate:dd/MM/yyyy HH:mm}\n" +
                   $"Status: {(licenseInfo.IsValid ? "VALID" : "INVALID")}\n" +
                   $"MAC Address: {licenseInfo.MacAddress}\n" +
                   $"Hardware ID: {licenseInfo.HardwareId}";
        }

        private void ShowActivationForm()
        {
            panelActivation.Visible = true;
            panelLicenseInfo.Visible = false;

            // Clear fields
            txtUserName.Text = "";
            txtLicenseKey.Text = "";
            lblStatus.Text = "";

            // Set focus
            txtUserName.Focus();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            ActivateLicense();
        }

        private void ActivateLicense()
        {
            try
            {
                string userName = txtUserName.Text.Trim();
                string licenseKey = txtLicenseKey.Text.Trim();

                // Validate inputs
                if (string.IsNullOrEmpty(userName))
                {
                    lblStatus.Text = "Please enter your user name!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    txtUserName.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(licenseKey))
                {
                    lblStatus.Text = "Please enter your license key!";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    txtLicenseKey.Focus();
                    return;
                }

                // Show processing
                lblStatus.Text = "Activating license, please wait...";
                lblStatus.ForeColor = System.Drawing.Color.Blue;
                this.Cursor = Cursors.WaitCursor;
                btnActivate.Enabled = false;
                Application.DoEvents();

                // Perform activation
                bool activated = _licenseService.ActivateLicense(userName, licenseKey);

                if (activated)
                {
                    lblStatus.Text = "License activated successfully!";
                    lblStatus.ForeColor = System.Drawing.Color.Green;

                    // Show success message
                    MessageBox.Show("License activated successfully!\n\nYou can now use the application.",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Update UI to show license info
                    //   LoadLicenseStatus();
                    this.Close();
                 
                }
                else
                {
                    lblStatus.Text = "Activation failed. Please check your license key.";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Activation error: {ex.Message}";
                lblStatus.ForeColor = System.Drawing.Color.Red;

                MessageBox.Show($"Activation failed: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnActivate.Enabled = true;

                this.Hide();
                var loginform = new LoginForm();
                loginform.ShowDialog();
            }
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
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
            MessageBox.Show("License Types:\n\n" +
                          "1. Trial License - 15 days free trial\n" +
                          "2. Yearly License - Valid for 1 year\n" +
                          "3. Lifetime License - No expiration\n\n" +
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
                MessageBox.Show("License information copied to clipboard.",
                    "Copy", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to deactivate the license on this computer?\n\n" +
                "You will need to reactivate to use the application.",
                "Confirm Deactivation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _licenseService.DeleteLicenseFile();
                    MessageBox.Show("License deactivated successfully.\n\n" +
                                  "Please restart the application to activate a new license.",
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

        private void lblStatus_TextChanged(object sender, EventArgs e)
        {
            // Auto-size the label to fit text
            using (var g = lblStatus.CreateGraphics())
            {
                var size = g.MeasureString(lblStatus.Text, lblStatus.Font);
                lblStatus.Height = (int)Math.Ceiling(size.Height);
            }
        }
    }
}
