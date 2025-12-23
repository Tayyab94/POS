using POS_Shop.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Settings
{
    partial class ConfigSettingForm
    {
        private System.ComponentModel.IContainer components = null;
        private TabControl tabControl;
        private Panel panelButtons;
        private CheckBox chkEnableUpdateQty;
        private TextBox txtShopName;
        private TextBox txtShopAddress;
        private TextBox txtContactNumber;
        private TextBox txtEmail;
        private TextBox txtTaxNumber;
        private TextBox txtFooterMessage;
        private Button btnSave;
        private Button btnCancel;
        private Button btnReset;
        private Label lblConfigPath;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "POS Settings";
            this.Size = new Size(700, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimumSize = new Size(700, 550);

            // Tab Control
            this.tabControl = new TabControl();
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);

            // Features Tab
            var tabFeatures = new TabPage("Features");
            tabFeatures.Padding = new Padding(10);
            InitializeFeaturesTab(tabFeatures);

            // Invoice Tab
            var tabInvoice = new TabPage("Invoice Settings");
            tabInvoice.Padding = new Padding(10);
            InitializeInvoiceTab(tabInvoice);

            // About Tab
            var tabAbout = new TabPage("About");
            tabAbout.Padding = new Padding(10);
            InitializeAboutTab(tabAbout);

            this.tabControl.TabPages.Add(tabFeatures);
            this.tabControl.TabPages.Add(tabInvoice);
            this.tabControl.TabPages.Add(tabAbout);

            // Buttons Panel
            this.panelButtons = new Panel();
            this.panelButtons.Dock = DockStyle.Bottom;
            this.panelButtons.Height = 60;
            this.panelButtons.BackColor = SystemColors.Control;

            // Button styling
            var buttonSize = new Size(100, 35);
            var buttonFont = new Font("Segoe UI", 9F, FontStyle.Regular);

            // Reset Button
            this.btnReset = new Button();
            this.btnReset.Text = "Reset to Default";
            this.btnReset.Size = buttonSize;
            this.btnReset.Font = buttonFont;
            this.btnReset.Location = new Point(20, 15);
            this.btnReset.Click += BtnReset_Click;
            this.btnReset.BackColor = Color.Orange;
            this.btnReset.ForeColor = Color.White;

            // Save Button
            this.btnSave = new Button();
            this.btnSave.Text = "Save";
            this.btnSave.Size = buttonSize;
            this.btnSave.Font = buttonFont;
            this.btnSave.Location = new Point(450, 15);
            this.btnSave.Click += BtnSave_Click;
            this.btnSave.BackColor = Color.SteelBlue;
            this.btnSave.ForeColor = Color.White;

            // Cancel Button
            this.btnCancel = new Button();
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Size = buttonSize;
            this.btnCancel.Font = buttonFont;
            this.btnCancel.Location = new Point(560, 15);
            this.btnCancel.Click += BtnCancel_Click;
            this.btnCancel.BackColor = SystemColors.ControlDark;
            this.btnCancel.ForeColor = Color.White;

            this.panelButtons.Controls.Add(this.btnReset);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnCancel);

            // Add status label
            this.lblConfigPath = new Label();
            this.lblConfigPath.Text = $"Config file: {ConfigurationManager.GetConfigFilePath()}";
            this.lblConfigPath.Location = new Point(20, 30);
            this.lblConfigPath.Size = new Size(650, 20);
            this.lblConfigPath.ForeColor = Color.Gray;
            this.lblConfigPath.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

            this.panelButtons.Controls.Add(this.lblConfigPath);

            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelButtons);
        }

        private void InitializeFeaturesTab(TabPage tab)
        {
            tab.BackColor = SystemColors.Window;

            // Header
            var lblHeader = new Label();
            lblHeader.Text = "Feature Settings";
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeader.Location = new Point(10, 10);
            lblHeader.Size = new Size(300, 30);

            // Update Quantity checkbox
            this.chkEnableUpdateQty = new CheckBox();
            this.chkEnableUpdateQty.Name = "chkEnableUpdateQty";
            this.chkEnableUpdateQty.Text = "Enable Quantity Update in Sales";
            this.chkEnableUpdateQty.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.chkEnableUpdateQty.Location = new Point(20, 60);
            this.chkEnableUpdateQty.Size = new Size(350, 25);

            // Description
            var lblFeatureDescription = new Label();
            lblFeatureDescription.Text = "When enabled, cashiers can modify product quantities during sales transactions.";
            lblFeatureDescription.Location = new Point(40, 90);
            lblFeatureDescription.Size = new Size(600, 40);
            lblFeatureDescription.ForeColor = SystemColors.ControlDarkDark;
            lblFeatureDescription.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            tab.Controls.AddRange(new Control[] {
                lblHeader,
                this.chkEnableUpdateQty,
                lblFeatureDescription
            });
        }

        private void InitializeInvoiceTab(TabPage tab)
        {
            tab.BackColor = SystemColors.Window;

            // Header
            var lblHeader = new Label();
            lblHeader.Text = "Invoice/Receipt Settings";
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeader.Location = new Point(10, 10);
            lblHeader.Size = new Size(300, 30);

            // Create form fields
            int yPos = 60;
            int labelWidth = 130;
            int textBoxWidth = 400;
            int spacing = 35;

            var labelFont = new Font("Segoe UI", 9.75F, FontStyle.Regular);
            var textBoxFont = new Font("Segoe UI", 9.75F, FontStyle.Regular);

            // Shop Name
            var lblShopName = new Label();
            lblShopName.Text = "Shop Name:";
            lblShopName.Font = labelFont;
            lblShopName.Location = new Point(20, yPos);
            lblShopName.Size = new Size(labelWidth, 25);

            this.txtShopName = new TextBox();
            this.txtShopName.Name = "txtShopName";
            this.txtShopName.Font = textBoxFont;
            this.txtShopName.Location = new Point(150, yPos);
            this.txtShopName.Size = new Size(textBoxWidth, 28);

            yPos += spacing;

            // Address
            var lblAddress = new Label();
            lblAddress.Text = "Address:";
            lblAddress.Font = labelFont;
            lblAddress.Location = new Point(20, yPos);
            lblAddress.Size = new Size(labelWidth, 25);

            this.txtShopAddress = new TextBox();
            this.txtShopAddress.Name = "txtShopAddress";
            this.txtShopAddress.Font = textBoxFont;
            this.txtShopAddress.Location = new Point(150, yPos);
            this.txtShopAddress.Size = new Size(textBoxWidth, 28);
            this.txtShopAddress.Multiline = true;
            this.txtShopAddress.Height = 56;

            yPos += 70;

            // Contact Number
            var lblContact = new Label();
            lblContact.Text = "Contact Number:";
            lblContact.Font = labelFont;
            lblContact.Location = new Point(20, yPos);
            lblContact.Size = new Size(labelWidth, 25);

            this.txtContactNumber = new TextBox();
            this.txtContactNumber.Name = "txtContactNumber";
            this.txtContactNumber.Font = textBoxFont;
            this.txtContactNumber.Location = new Point(150, yPos);
            this.txtContactNumber.Size = new Size(textBoxWidth, 28);

            yPos += spacing;

            // Email
            var lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Font = labelFont;
            lblEmail.Location = new Point(20, yPos);
            lblEmail.Size = new Size(labelWidth, 25);

            this.txtEmail = new TextBox();
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Font = textBoxFont;
            this.txtEmail.Location = new Point(150, yPos);
            this.txtEmail.Size = new Size(textBoxWidth, 28);

            yPos += spacing;

            // Tax Number
            var lblTax = new Label();
            lblTax.Text = "Tax Number:";
            lblTax.Font = labelFont;
            lblTax.Location = new Point(20, yPos);
            lblTax.Size = new Size(labelWidth, 25);

            this.txtTaxNumber = new TextBox();
            this.txtTaxNumber.Name = "txtTaxNumber";
            this.txtTaxNumber.Font = textBoxFont;
            this.txtTaxNumber.Location = new Point(150, yPos);
            this.txtTaxNumber.Size = new Size(textBoxWidth, 28);

            yPos += spacing;

            // Footer Message
            var lblFooter = new Label();
            lblFooter.Text = "Footer Message:";
            lblFooter.Font = labelFont;
            lblFooter.Location = new Point(20, yPos);
            lblFooter.Size = new Size(labelWidth, 25);

            this.txtFooterMessage = new TextBox();
            this.txtFooterMessage.Name = "txtFooterMessage";
            this.txtFooterMessage.Font = textBoxFont;
            this.txtFooterMessage.Location = new Point(150, yPos);
            this.txtFooterMessage.Size = new Size(textBoxWidth, 28);
            this.txtFooterMessage.Multiline = true;
            this.txtFooterMessage.Height = 56;

            tab.Controls.AddRange(new Control[] {
                lblHeader,
                lblShopName, this.txtShopName,
                lblAddress, this.txtShopAddress,
                lblContact, this.txtContactNumber,
                lblEmail, this.txtEmail,
                lblTax, this.txtTaxNumber,
                lblFooter, this.txtFooterMessage
            });
        }

        private void InitializeAboutTab(TabPage tab)
        {
            tab.BackColor = SystemColors.Window;

            var lblTitle = new Label();
            lblTitle.Text = "POS System Configuration";
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 20);
            lblTitle.Size = new Size(400, 30);

            var lblVersion = new Label();
            lblVersion.Text = $"Version: {Application.ProductVersion}";
            lblVersion.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            lblVersion.Location = new Point(20, 70);
            lblVersion.Size = new Size(300, 25);

            var lblConfigInfo = new Label();
            lblConfigInfo.Text = "Configuration Information:";
            lblConfigInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblConfigInfo.Location = new Point(20, 120);
            lblConfigInfo.Size = new Size(300, 25);

            var lblConfigPath = new Label();
            lblConfigPath.Text = $"Config file location:\n{ConfigurationManager.GetConfigFilePath()}";
            lblConfigPath.Font = new Font("Consolas", 9F, FontStyle.Regular);
            lblConfigPath.Location = new Point(20, 160);
            lblConfigPath.Size = new Size(600, 60);
            lblConfigPath.BorderStyle = BorderStyle.FixedSingle;
            lblConfigPath.BackColor = SystemColors.Info;

            var btnOpenConfigFolder = new Button();
            btnOpenConfigFolder.Text = "Open Config Folder";
            btnOpenConfigFolder.Location = new Point(20, 240);
            btnOpenConfigFolder.Size = new Size(150, 35);
            btnOpenConfigFolder.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnOpenConfigFolder.Click += BtnOpenConfigFolder_Click;

            var btnViewConfig = new Button();
            btnViewConfig.Text = "View Config File";
            btnViewConfig.Location = new Point(180, 240);
            btnViewConfig.Size = new Size(150, 35);
            btnViewConfig.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btnViewConfig.Click += BtnViewConfig_Click;

            tab.Controls.AddRange(new Control[] {
                lblTitle,
                lblVersion,
                lblConfigInfo,
                lblConfigPath,
                btnOpenConfigFolder,
                btnViewConfig
            });
        }
    }
}