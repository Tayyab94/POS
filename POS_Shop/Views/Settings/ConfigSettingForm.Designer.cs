//using POS_Shop.Helpers;
//using System.Drawing;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Settings
//{
//    partial class ConfigSettingForm
//    {
//        private System.ComponentModel.IContainer components = null;
//        private TabControl tabControl;
//        private Panel panelButtons;
//        private CheckBox chkEnableUpdateQty;
//        private TextBox txtShopName;
//        private TextBox txtShopAddress;
//        private TextBox txtContactNumber;
//        private TextBox txtEmail;
//        private TextBox txtTaxNumber;
//        private TextBox txtFooterMessage;
//        private Button btnSave;
//        private Button btnCancel;
//        private Button btnReset;
//        private Label lblConfigPath;

//        private CheckBox chkShowHideShopName;


//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            this.Text = "POS Settings";
//            this.Size = new Size(700, 550);
//            this.StartPosition = FormStartPosition.CenterParent;
//            this.MinimumSize = new Size(700, 550);

//            // Tab Control
//            this.tabControl = new TabControl();
//            this.tabControl.Dock = DockStyle.Fill;
//            this.tabControl.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);

//            // Features Tab
//            var tabFeatures = new TabPage("Features");
//            tabFeatures.Padding = new Padding(10);
//            InitializeFeaturesTab(tabFeatures);

//            //// Invoice Tab
//            //var tabInvoice = new TabPage("Invoice Settings");
//            //tabInvoice.Padding = new Padding(10);
//            //InitializeInvoiceTab(tabInvoice);

//            //// About Tab
//            //var tabAbout = new TabPage("About");
//            //tabAbout.Padding = new Padding(10);
//            //InitializeAboutTab(tabAbout);

//            this.tabControl.TabPages.Add(tabFeatures);
//            //this.tabControl.TabPages.Add(tabInvoice);
//            //this.tabControl.TabPages.Add(tabAbout);

//            // Buttons Panel
//            this.panelButtons = new Panel();
//            this.panelButtons.Dock = DockStyle.Bottom;
//            this.panelButtons.Height = 60;
//            this.panelButtons.BackColor = SystemColors.Control;

//            // Button styling
//            var buttonSize = new Size(100, 35);
//            var buttonFont = new Font("Segoe UI", 9F, FontStyle.Regular);

//            // Reset Button
//            this.btnReset = new Button();
//            this.btnReset.Text = "Reset to Default";
//            this.btnReset.Size = buttonSize;
//            this.btnReset.Font = buttonFont;
//            this.btnReset.Location = new Point(20, 15);
//            this.btnReset.Click += BtnReset_Click;
//            this.btnReset.BackColor = Color.Orange;
//            this.btnReset.ForeColor = Color.White;

//            // Save Button
//            this.btnSave = new Button();
//            this.btnSave.Text = "Save";
//            this.btnSave.Size = buttonSize;
//            this.btnSave.Font = buttonFont;
//            this.btnSave.Location = new Point(450, 15);
//            this.btnSave.Click += BtnSave_Click;
//            this.btnSave.BackColor = Color.SteelBlue;
//            this.btnSave.ForeColor = Color.White;

//            // Cancel Button
//            this.btnCancel = new Button();
//            this.btnCancel.Text = "Cancel";
//            this.btnCancel.Size = buttonSize;
//            this.btnCancel.Font = buttonFont;
//            this.btnCancel.Location = new Point(560, 15);
//            this.btnCancel.Click += BtnCancel_Click;
//            this.btnCancel.BackColor = SystemColors.ControlDark;
//            this.btnCancel.ForeColor = Color.White;

//            this.panelButtons.Controls.Add(this.btnReset);
//            this.panelButtons.Controls.Add(this.btnSave);
//            this.panelButtons.Controls.Add(this.btnCancel);

//            // Add status label
//            //this.lblConfigPath = new Label();
//            //this.lblConfigPath.Text = $"Config file: {ConfigurationManager.GetConfigFilePath()}";
//            //this.lblConfigPath.Location = new Point(20, 30);
//            //this.lblConfigPath.Size = new Size(650, 20);
//            //this.lblConfigPath.ForeColor = Color.Gray;
//            //this.lblConfigPath.Font = new Font("Segoe UI", 7F, FontStyle.Italic);

//            //this.panelButtons.Controls.Add(this.lblConfigPath);

//            this.Controls.Add(this.tabControl);
//            this.Controls.Add(this.panelButtons);
//        }

//        private void InitializeFeaturesTab(TabPage tab)
//        {
//            tab.BackColor = SystemColors.Window;

//            // Header
//            var lblHeader = new Label();
//            lblHeader.Text = "Feature Settings";
//            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
//            lblHeader.Location = new Point(10, 10);
//            lblHeader.Size = new Size(300, 30);

//            // Update Quantity checkbox
//            this.chkEnableUpdateQty = new CheckBox();
//            this.chkEnableUpdateQty.Name = "chkEnableUpdateQty";
//            this.chkEnableUpdateQty.Text = "Enable Quantity Update in Sales";
//            this.chkEnableUpdateQty.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
//            this.chkEnableUpdateQty.Location = new Point(20, 50);
//            this.chkEnableUpdateQty.Size = new Size(350, 25);

//            // Description - COMES RIGHT AFTER chkEnableUpdateQty
//            var lblFeatureDescription = new Label();
//            lblFeatureDescription.Text = "When enabled, cashiers can modify product quantities during sales transactions.";
//            lblFeatureDescription.Location = new Point(40, 80); // Positioned right below the checkbox
//            lblFeatureDescription.Size = new Size(600, 40);
//            lblFeatureDescription.ForeColor = SystemColors.ControlDarkDark;
//            lblFeatureDescription.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

//            // Show/Hide Shop Name checkbox - COMES AFTER THE DESCRIPTION
//            this.chkShowHideShopName = new CheckBox();
//            this.chkShowHideShopName.Name = "chkShowHideShopName";
//            this.chkShowHideShopName.Text = "Show Shop Name on Receipt";
//            this.chkShowHideShopName.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
//            this.chkShowHideShopName.Location = new Point(20, 130); // Below description (80 + 40 + 10 = 130)
//            this.chkShowHideShopName.Size = new Size(350, 25);

//            tab.Controls.AddRange(new Control[] {
//    lblHeader,
//    this.chkEnableUpdateQty,
//    lblFeatureDescription,     // Description added here
//    this.chkShowHideShopName   // Shop name checkbox added last
//});

//        }

//        //private void InitializeInvoiceTab(TabPage tab)
//        //{
//        //    tab.BackColor = SystemColors.Window;

//        //    // Header
//        //    var lblHeader = new Label();
//        //    lblHeader.Text = "Invoice/Receipt Settings";
//        //    lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
//        //    lblHeader.Location = new Point(10, 10);
//        //    lblHeader.Size = new Size(300, 30);

//        //    // Create form fields
//        //    int yPos = 60;
//        //    int labelWidth = 130;
//        //    int textBoxWidth = 400;
//        //    int spacing = 35;

//        //    var labelFont = new Font("Segoe UI", 9.75F, FontStyle.Regular);
//        //    var textBoxFont = new Font("Segoe UI", 9.75F, FontStyle.Regular);

//        //    // Shop Name
//        //    var lblShopName = new Label();
//        //    lblShopName.Text = "Shop Name:";
//        //    lblShopName.Font = labelFont;
//        //    lblShopName.Location = new Point(20, yPos);
//        //    lblShopName.Size = new Size(labelWidth, 25);

//        //    this.txtShopName = new TextBox();
//        //    this.txtShopName.Name = "txtShopName";
//        //    this.txtShopName.Font = textBoxFont;
//        //    this.txtShopName.Location = new Point(150, yPos);
//        //    this.txtShopName.Size = new Size(textBoxWidth, 28);

//        //    yPos += spacing;

//        //    // Address
//        //    var lblAddress = new Label();
//        //    lblAddress.Text = "Address:";
//        //    lblAddress.Font = labelFont;
//        //    lblAddress.Location = new Point(20, yPos);
//        //    lblAddress.Size = new Size(labelWidth, 25);

//        //    this.txtShopAddress = new TextBox();
//        //    this.txtShopAddress.Name = "txtShopAddress";
//        //    this.txtShopAddress.Font = textBoxFont;
//        //    this.txtShopAddress.Location = new Point(150, yPos);
//        //    this.txtShopAddress.Size = new Size(textBoxWidth, 28);
//        //    this.txtShopAddress.Multiline = true;
//        //    this.txtShopAddress.Height = 56;

//        //    yPos += 70;

//        //    // Contact Number
//        //    var lblContact = new Label();
//        //    lblContact.Text = "Contact Number:";
//        //    lblContact.Font = labelFont;
//        //    lblContact.Location = new Point(20, yPos);
//        //    lblContact.Size = new Size(labelWidth, 25);

//        //    this.txtContactNumber = new TextBox();
//        //    this.txtContactNumber.Name = "txtContactNumber";
//        //    this.txtContactNumber.Font = textBoxFont;
//        //    this.txtContactNumber.Location = new Point(150, yPos);
//        //    this.txtContactNumber.Size = new Size(textBoxWidth, 28);

//        //    yPos += spacing;

//        //    // Email
//        //    var lblEmail = new Label();
//        //    lblEmail.Text = "Email:";
//        //    lblEmail.Font = labelFont;
//        //    lblEmail.Location = new Point(20, yPos);
//        //    lblEmail.Size = new Size(labelWidth, 25);

//        //    this.txtEmail = new TextBox();
//        //    this.txtEmail.Name = "txtEmail";
//        //    this.txtEmail.Font = textBoxFont;
//        //    this.txtEmail.Location = new Point(150, yPos);
//        //    this.txtEmail.Size = new Size(textBoxWidth, 28);

//        //    yPos += spacing;

//        //    // Tax Number
//        //    var lblTax = new Label();
//        //    lblTax.Text = "Tax Number:";
//        //    lblTax.Font = labelFont;
//        //    lblTax.Location = new Point(20, yPos);
//        //    lblTax.Size = new Size(labelWidth, 25);

//        //    this.txtTaxNumber = new TextBox();
//        //    this.txtTaxNumber.Name = "txtTaxNumber";
//        //    this.txtTaxNumber.Font = textBoxFont;
//        //    this.txtTaxNumber.Location = new Point(150, yPos);
//        //    this.txtTaxNumber.Size = new Size(textBoxWidth, 28);

//        //    yPos += spacing;

//        //    // Footer Message
//        //    var lblFooter = new Label();
//        //    lblFooter.Text = "Footer Message:";
//        //    lblFooter.Font = labelFont;
//        //    lblFooter.Location = new Point(20, yPos);
//        //    lblFooter.Size = new Size(labelWidth, 25);

//        //    this.txtFooterMessage = new TextBox();
//        //    this.txtFooterMessage.Name = "txtFooterMessage";
//        //    this.txtFooterMessage.Font = textBoxFont;
//        //    this.txtFooterMessage.Location = new Point(150, yPos);
//        //    this.txtFooterMessage.Size = new Size(textBoxWidth, 28);
//        //    this.txtFooterMessage.Multiline = true;
//        //    this.txtFooterMessage.Height = 56;

//        //    tab.Controls.AddRange(new Control[] {
//        //        lblHeader,
//        //        lblShopName, this.txtShopName,
//        //        lblAddress, this.txtShopAddress,
//        //        lblContact, this.txtContactNumber,
//        //        lblEmail, this.txtEmail,
//        //        lblTax, this.txtTaxNumber,
//        //        lblFooter, this.txtFooterMessage
//        //    });
//        //}

//        //private void InitializeAboutTab(TabPage tab)
//        //{
//        //    tab.BackColor = SystemColors.Window;

//        //    var lblTitle = new Label();
//        //    lblTitle.Text = "POS System Configuration";
//        //    lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
//        //    lblTitle.Location = new Point(20, 20);
//        //    lblTitle.Size = new Size(400, 30);

//        //    var lblVersion = new Label();
//        //    lblVersion.Text = $"Version: {Application.ProductVersion}";
//        //    lblVersion.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
//        //    lblVersion.Location = new Point(20, 70);
//        //    lblVersion.Size = new Size(300, 25);

//        //    var lblConfigInfo = new Label();
//        //    lblConfigInfo.Text = "Configuration Information:";
//        //    lblConfigInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
//        //    lblConfigInfo.Location = new Point(20, 120);
//        //    lblConfigInfo.Size = new Size(300, 25);

//        //    var lblConfigPath = new Label();
//        //    lblConfigPath.Text = $"Config file location:\n{ConfigurationManager.GetConfigFilePath()}";
//        //    lblConfigPath.Font = new Font("Consolas", 9F, FontStyle.Regular);
//        //    lblConfigPath.Location = new Point(20, 160);
//        //    lblConfigPath.Size = new Size(600, 60);
//        //    lblConfigPath.BorderStyle = BorderStyle.FixedSingle;
//        //    lblConfigPath.BackColor = SystemColors.Info;

//        //    var btnOpenConfigFolder = new Button();
//        //    btnOpenConfigFolder.Text = "Open Config Folder";
//        //    btnOpenConfigFolder.Location = new Point(20, 240);
//        //    btnOpenConfigFolder.Size = new Size(150, 35);
//        //    btnOpenConfigFolder.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
//        //    btnOpenConfigFolder.Click += BtnOpenConfigFolder_Click;

//        //    var btnViewConfig = new Button();
//        //    btnViewConfig.Text = "View Config File";
//        //    btnViewConfig.Location = new Point(180, 240);
//        //    btnViewConfig.Size = new Size(150, 35);
//        //    btnViewConfig.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
//        //    btnViewConfig.Click += BtnViewConfig_Click;

//        //    tab.Controls.AddRange(new Control[] {
//        //        lblTitle,
//        //        lblVersion,
//        //        lblConfigInfo,
//        //        lblConfigPath,
//        //        btnOpenConfigFolder,
//        //        btnViewConfig
//        //    });
//        //}
//    }
//}


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
        private CheckBox chkShowHideShopName;

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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFeatures = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStockMinQty = new System.Windows.Forms.TextBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.chkEnableUpdateQty = new System.Windows.Forms.CheckBox();
            this.lblFeatureDescription = new System.Windows.Forms.Label();
            this.chkShowHideShopName = new System.Windows.Forms.CheckBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabFeatures.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabFeatures);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(682, 443);
            this.tabControl.TabIndex = 0;
            // 
            // tabFeatures
            // 
            this.tabFeatures.BackColor = System.Drawing.SystemColors.Window;
            this.tabFeatures.Controls.Add(this.label1);
            this.tabFeatures.Controls.Add(this.txtStockMinQty);
            this.tabFeatures.Controls.Add(this.lblHeader);
            this.tabFeatures.Controls.Add(this.chkEnableUpdateQty);
            this.tabFeatures.Controls.Add(this.lblFeatureDescription);
            this.tabFeatures.Controls.Add(this.chkShowHideShopName);
            this.tabFeatures.Location = new System.Drawing.Point(4, 30);
            this.tabFeatures.Name = "tabFeatures";
            this.tabFeatures.Padding = new System.Windows.Forms.Padding(10);
            this.tabFeatures.Size = new System.Drawing.Size(674, 409);
            this.tabFeatures.TabIndex = 0;
            this.tabFeatures.Text = "Features";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Set Default Stock Mininum Quantity";
            // 
            // txtStockMinQty
            // 
            this.txtStockMinQty.Location = new System.Drawing.Point(20, 123);
            this.txtStockMinQty.Name = "txtStockMinQty";
            this.txtStockMinQty.Size = new System.Drawing.Size(82, 29);
            this.txtStockMinQty.TabIndex = 4;
            // 
            // lblHeader
            // 
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(10, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(300, 30);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Feature Settings";
            // 
            // chkEnableUpdateQty
            // 
            this.chkEnableUpdateQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkEnableUpdateQty.Location = new System.Drawing.Point(20, 50);
            this.chkEnableUpdateQty.Name = "chkEnableUpdateQty";
            this.chkEnableUpdateQty.Size = new System.Drawing.Size(350, 25);
            this.chkEnableUpdateQty.TabIndex = 1;
            this.chkEnableUpdateQty.Text = "Enable Quantity Update in Sales";
            // 
            // lblFeatureDescription
            // 
            this.lblFeatureDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFeatureDescription.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblFeatureDescription.Location = new System.Drawing.Point(40, 80);
            this.lblFeatureDescription.Name = "lblFeatureDescription";
            this.lblFeatureDescription.Size = new System.Drawing.Size(600, 40);
            this.lblFeatureDescription.TabIndex = 2;
            this.lblFeatureDescription.Text = "When enabled, cashiers can modify product quantities during sales transactions.";
            // 
            // chkShowHideShopName
            // 
            this.chkShowHideShopName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkShowHideShopName.Location = new System.Drawing.Point(20, 182);
            this.chkShowHideShopName.Name = "chkShowHideShopName";
            this.chkShowHideShopName.Size = new System.Drawing.Size(350, 25);
            this.chkShowHideShopName.TabIndex = 3;
            this.chkShowHideShopName.Text = "Show Shop Name on Receipt";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.SystemColors.Control;
            this.panelButtons.Controls.Add(this.btnReset);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 443);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(682, 60);
            this.panelButtons.TabIndex = 1;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.Orange;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(20, 15);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 35);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset to Default";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SteelBlue;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(450, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(560, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // ConfigSettingForm
            // 
            this.ClientSize = new System.Drawing.Size(682, 503);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelButtons);
            this.MinimumSize = new System.Drawing.Size(700, 550);
            this.Name = "ConfigSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "POS Settings";
            this.tabControl.ResumeLayout(false);
            this.tabFeatures.ResumeLayout(false);
            this.tabFeatures.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TabPage tabFeatures;
        private Label label1;
        private TextBox txtStockMinQty;
        private Label lblHeader;
        private Label lblFeatureDescription;
    }
}