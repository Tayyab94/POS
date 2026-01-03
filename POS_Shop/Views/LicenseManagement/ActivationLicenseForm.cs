using POS_Shop.Interfaces;
using POS_Shop.Models.LicenseModels.DTO;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.LicenseManagement
{
    public partial class ActivationLicenseForm : Form
    {
        private readonly ILicenseService _licenseService;

        public ActivationLicenseForm()
        {
            InitializeComponent();
            _licenseService = new LicenseService();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Check if license already exists and is valid
            if (_licenseService.IsLicenseValid())
            {
                var licenseInfo = _licenseService.GetCurrentLicenseInfo();
                ShowLicenseInfo(licenseInfo);
                return;
            }

            // Show activation form
          //  ShowActivationForm();
        }

        private void InitializeComponent()
        {
            this.Text = "ERP/POS System - License Activation";
            this.Size = new Size(500, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create controls
            var lblTitle = new Label
            {
                Text = "ERP/POS License Activation",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(100, 20),
                Size = new Size(300, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblUserName = new Label
            {
                Text = "User Name:",
                Location = new Point(50, 80),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10)
            };

            var txtUserName = new TextBox
            {
                Location = new Point(150, 80),
                Size = new Size(250, 25),
                Name = "txtUserName",
                Font = new Font("Segoe UI", 10)
            };

            var lblLicenseKey = new Label
            {
                Text = "License Key:",
                Location = new Point(50, 120),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10)
            };

            var txtLicenseKey = new TextBox
            {
                Location = new Point(150, 120),
                Size = new Size(250, 25),
                Name = "txtLicenseKey",
                Font = new Font("Segoe UI", 10)
            };

            var btnActivate = new Button
            {
                Text = "Activate License",
                Location = new Point(150, 180),
                Size = new Size(150, 40),
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            var btnExit = new Button
            {
                Text = "Exit",
                Location = new Point(320, 180),
                Size = new Size(80, 40),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };

            var lblStatus = new Label
            {
                Text = "",
                Location = new Point(50, 230),
                Size = new Size(400, 50),
                ForeColor = Color.Red,
                Name = "lblStatus",
                Font = new Font("Segoe UI", 9)
            };

            // Add event handlers
            btnActivate.Click += BtnActivate_Click;
            btnExit.Click += BtnExit_Click;

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                lblTitle, lblUserName, txtUserName,
                lblLicenseKey, txtLicenseKey, btnActivate,
                btnExit, lblStatus
            });
        }

        private void BtnActivate_Click(object sender, EventArgs e)
        {
            var txtUserName = this.Controls["txtUserName"] as TextBox;
            var txtLicenseKey = this.Controls["txtLicenseKey"] as TextBox;
            var lblStatus = this.Controls["lblStatus"] as Label;

            string userName = txtUserName.Text.Trim();
            string licenseKey = txtLicenseKey.Text.Trim();

            if (string.IsNullOrEmpty(userName))
            {
                lblStatus.Text = "Please enter your user name!";
                txtUserName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(licenseKey))
            {
                lblStatus.Text = "Please enter your license key!";
                txtLicenseKey.Focus();
                return;
            }

            lblStatus.Text = "Activating license...";
            lblStatus.ForeColor = Color.Blue;

            // Perform activation
            bool activated = _licenseService.ActivateLicense(userName, licenseKey);
            if (activated)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblStatus.Text = "Activation failed. Please check your license key and try again.";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowLicenseInfo(LicenseInfo licenseInfo)
        {
            this.Controls.Clear();

            var lblTitle = new Label
            {
                Text = "License Information",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(150, 20),
                Size = new Size(200, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            var lblInfo = new Label
            {
                Text = $"User: {licenseInfo.UserName}\n\n" +
                       $"License Type: {licenseInfo.LicenseType}\n\n" +
                       $"Issued: {licenseInfo.IssueDate:dd/MM/yyyy}\n\n" +
                       $"Expires: {licenseInfo.ExpiryDate:dd/MM/yyyy}\n\n" +
                       $"Status: {(licenseInfo.IsValid ? "VALID ✓" : "EXPIRED ✗")}",
                Location = new Point(50, 70),
                Size = new Size(400, 150),
                Font = new Font("Segoe UI", 11),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var btnContinue = new Button
            {
                Text = "Continue to Application",
                Location = new Point(150, 250),
                Size = new Size(200, 40),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            btnContinue.Click += (sender, e) =>
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.Controls.AddRange(new Control[] { lblTitle, lblInfo, btnContinue });
        }
    }
}
