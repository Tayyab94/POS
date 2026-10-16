//using System.Drawing;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Setup
//{
//    partial class DatabaseSetupForm
//    {
//        private System.ComponentModel.IContainer components = null;

//        // ══════════════════════════════════════════════════════════
//        //  CONTROL DECLARATIONS
//        // ══════════════════════════════════════════════════════════

//        // Header
//        private Panel pnlHeader;
//        private Label lblAppName;
//        private Label lblHeaderSub;

//        // DB Type Group
//        private GroupBox grpDbType;
//        private RadioButton rdoSqlServer;
//        private RadioButton rdoLocalDb;

//        // ── SQL Server Panel ──────────────────────────────────────
//        private Panel pnlSqlServer;

//        //   Row 1: Server
//        private Label lblServerLabel;
//        private ComboBox cboServer;
//        private Button btnRefresh;
//        private Label lblServerLoading;

//        //   Row 2: Auth Type
//        private Label lblAuthType;
//        private RadioButton rdoWinAuth;
//        private RadioButton rdoSqlAuth;

//        //   Row 3: SQL Auth credentials (hide/show)
//        private Panel pnlSqlCreds;
//        private Label lblSqlUser;
//        private TextBox txtSqlUser;
//        private Label lblSqlPass;
//        private TextBox txtSqlPass;
//        private Button btnLoadDbs;

//        //   Row 4: Database dropdown
//        private Label lblDatabaseLabel;
//        private ComboBox cboDatabases;
//        private Label lblDbLoading;

//        // ── LocalDB Panel ─────────────────────────────────────────
//        private Panel pnlLocalDb;
//        private Label lblMdfLabel;
//        private TextBox txtMdfPath;
//        private Button btnBrowse;
//        private Label lblLocalDbNote;

//        // Status bar
//        private Panel pnlStatusBar;
//        private Label lblStatusIcon;
//        private Label lblStatusText;

//        // Footer
//        private Panel pnlFooter;
//        private Button btnTestConn;
//        private Button btnSave;
//        private Button btnCancel;

//        // ══════════════════════════════════════════════════════════
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && components != null) components.Dispose();
//            base.Dispose(disposing);
//        }

//        // ══════════════════════════════════════════════════════════
//        //  InitializeComponent
//        // ══════════════════════════════════════════════════════════
//        private void InitializeComponent()
//        {
//            // Instantiate
//            pnlHeader = new Panel();
//            lblAppName = new Label();
//            lblHeaderSub = new Label();

//            grpDbType = new GroupBox();
//            rdoSqlServer = new RadioButton();
//            rdoLocalDb = new RadioButton();

//            pnlSqlServer = new Panel();
//            lblServerLabel = new Label();
//            cboServer = new ComboBox();
//            btnRefresh = new Button();
//            lblServerLoading = new Label();

//            lblAuthType = new Label();
//            rdoWinAuth = new RadioButton();
//            rdoSqlAuth = new RadioButton();

//            pnlSqlCreds = new Panel();
//            lblSqlUser = new Label();
//            txtSqlUser = new TextBox();
//            lblSqlPass = new Label();
//            txtSqlPass = new TextBox();
//            btnLoadDbs = new Button();

//            lblDatabaseLabel = new Label();
//            cboDatabases = new ComboBox();
//            lblDbLoading = new Label();

//            pnlLocalDb = new Panel();
//            lblMdfLabel = new Label();
//            txtMdfPath = new TextBox();
//            btnBrowse = new Button();
//            lblLocalDbNote = new Label();

//            pnlStatusBar = new Panel();
//            lblStatusIcon = new Label();
//            lblStatusText = new Label();

//            pnlFooter = new Panel();
//            btnTestConn = new Button();
//            btnSave = new Button();
//            btnCancel = new Button();

//            this.SuspendLayout();

//            // ══════════════════════════════════════════════════════
//            //  FORM
//            // ══════════════════════════════════════════════════════
//            this.Text = "POS Shop — Database Setup";
//            this.ClientSize = new Size(580, 610);
//            this.FormBorderStyle = FormBorderStyle.FixedDialog;
//            this.StartPosition = FormStartPosition.CenterScreen;
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.BackColor = Color.FromArgb(248, 249, 252);
//            this.Font = new Font("Segoe UI", 9f);

//            // ══════════════════════════════════════════════════════
//            //  HEADER
//            // ══════════════════════════════════════════════════════
//            pnlHeader.Dock = DockStyle.Top;
//            pnlHeader.Height = 72;
//            pnlHeader.BackColor = Color.FromArgb(21, 82, 163);

//            lblAppName.Text = "POS Shop — Database Setup";
//            lblAppName.Font = new Font("Segoe UI", 13f, FontStyle.Bold);
//            lblAppName.ForeColor = Color.White;
//            lblAppName.AutoSize = true;
//            lblAppName.Location = new Point(20, 10);

//            lblHeaderSub.Text = "Pehli baar setup — ye settings save hongi aur dobara nahi puchhi jayengi.";
//            lblHeaderSub.Font = new Font("Segoe UI", 8.5f);
//            lblHeaderSub.ForeColor = Color.FromArgb(180, 210, 255);
//            lblHeaderSub.AutoSize = true;
//            lblHeaderSub.Location = new Point(22, 42);

//            pnlHeader.Controls.Add(lblAppName);
//            pnlHeader.Controls.Add(lblHeaderSub);

//            // ══════════════════════════════════════════════════════
//            //  DB TYPE GROUP
//            // ══════════════════════════════════════════════════════
//            grpDbType.Text = "  Database Engine";
//            grpDbType.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
//            grpDbType.Location = new Point(20, 88);
//            grpDbType.Size = new Size(540, 56);
//            grpDbType.BackColor = Color.White;

//            rdoSqlServer.Text = "SQL Server  (full install / named instance)";
//            rdoSqlServer.Font = new Font("Segoe UI", 9.5f);
//            rdoSqlServer.Location = new Point(18, 22);
//            rdoSqlServer.AutoSize = true;
//            rdoSqlServer.Checked = true;

//            rdoLocalDb.Text = "LocalDB  (.mdf file)";
//            rdoLocalDb.Font = new Font("Segoe UI", 9.5f);
//            rdoLocalDb.Location = new Point(330, 22);
//            rdoLocalDb.AutoSize = true;

//            grpDbType.Controls.Add(rdoSqlServer);
//            grpDbType.Controls.Add(rdoLocalDb);

//            // ══════════════════════════════════════════════════════
//            //  SQL SERVER PANEL
//            //  Height dynamically controlled by pnlSqlCreds visibility
//            // ══════════════════════════════════════════════════════
//            pnlSqlServer.Location = new Point(20, 158);
//            pnlSqlServer.Size = new Size(540, 370);
//            pnlSqlServer.BackColor = Color.White;
//            pnlSqlServer.BorderStyle = BorderStyle.FixedSingle;
//            pnlSqlServer.Visible = true;

//            // ── Row 1: Server Instance ────────────────────────────
//            lblServerLabel.Text = "SQL Server Instance:";
//            lblServerLabel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
//            lblServerLabel.Location = new Point(14, 14);
//            lblServerLabel.AutoSize = true;

//            cboServer.Location = new Point(14, 34);
//            cboServer.Size = new Size(396, 26);
//            cboServer.DropDownStyle = ComboBoxStyle.DropDown;
//            cboServer.Font = new Font("Segoe UI", 9.5f);

//            btnRefresh.Text = "↺  Refresh";
//            btnRefresh.Location = new Point(418, 33);
//            btnRefresh.Size = new Size(104, 27);
//            btnRefresh.FlatStyle = FlatStyle.Flat;
//            btnRefresh.BackColor = Color.FromArgb(21, 82, 163);
//            btnRefresh.ForeColor = Color.White;
//            btnRefresh.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
//            btnRefresh.FlatAppearance.BorderSize = 0;
//            btnRefresh.Cursor = Cursors.Hand;

//            lblServerLoading.Text = "⏳  Servers dhundhe ja rahe hain...";
//            lblServerLoading.Font = new Font("Segoe UI", 8f, FontStyle.Italic);
//            lblServerLoading.ForeColor = Color.Gray;
//            lblServerLoading.Location = new Point(14, 64);
//            lblServerLoading.AutoSize = true;
//            lblServerLoading.Visible = false;

//            // ── Row 2: Auth Type ──────────────────────────────────
//            // Separator line
//            var sep1 = new Label
//            {
//                Location = new Point(14, 74),
//                Size = new Size(508, 1),
//                BackColor = Color.FromArgb(220, 225, 240)
//            };

//            lblAuthType.Text = "Authentication:";
//            lblAuthType.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
//            lblAuthType.Location = new Point(14, 84);
//            lblAuthType.AutoSize = true;

//            rdoWinAuth.Text = "Windows Authentication";
//            rdoWinAuth.Font = new Font("Segoe UI", 9.5f);
//            rdoWinAuth.Location = new Point(14, 104);
//            rdoWinAuth.AutoSize = true;
//            rdoWinAuth.Checked = true;

//            rdoSqlAuth.Text = "SQL Server Authentication";
//            rdoSqlAuth.Font = new Font("Segoe UI", 9.5f);
//            rdoSqlAuth.Location = new Point(220, 104);
//            rdoSqlAuth.AutoSize = true;

//            // ── Row 3: SQL Auth Credentials Panel (hidden by default)
//            pnlSqlCreds.Location = new Point(14, 136);
//            pnlSqlCreds.Size = new Size(508, 80);
//            pnlSqlCreds.BackColor = Color.FromArgb(246, 248, 255);
//            pnlSqlCreds.BorderStyle = BorderStyle.FixedSingle;
//            pnlSqlCreds.Visible = false;

//            lblSqlUser.Text = "Username:";
//            lblSqlUser.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
//            lblSqlUser.Location = new Point(12, 10);
//            lblSqlUser.AutoSize = true;

//            txtSqlUser.Location = new Point(80, 8);
//            txtSqlUser.Size = new Size(160, 24);
//            txtSqlUser.Font = new Font("Segoe UI", 9f);

//            lblSqlPass.Text = "Password:";
//            lblSqlPass.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
//            lblSqlPass.Location = new Point(260, 10);
//            lblSqlPass.AutoSize = true;

//            txtSqlPass.Location = new Point(326, 8);
//            txtSqlPass.Size = new Size(160, 24);
//            txtSqlPass.Font = new Font("Segoe UI", 9f);
//            txtSqlPass.UseSystemPasswordChar = true;

//            btnLoadDbs.Text = "Load Databases  ▼";
//            btnLoadDbs.Location = new Point(12, 44);
//            btnLoadDbs.Size = new Size(474, 28);
//            btnLoadDbs.FlatStyle = FlatStyle.Flat;
//            btnLoadDbs.BackColor = Color.FromArgb(240, 245, 255);
//            btnLoadDbs.ForeColor = Color.FromArgb(21, 82, 163);
//            btnLoadDbs.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
//            btnLoadDbs.FlatAppearance.BorderColor = Color.FromArgb(21, 82, 163);
//            btnLoadDbs.Cursor = Cursors.Hand;

//            pnlSqlCreds.Controls.Add(lblSqlUser);
//            pnlSqlCreds.Controls.Add(txtSqlUser);
//            pnlSqlCreds.Controls.Add(lblSqlPass);
//            pnlSqlCreds.Controls.Add(txtSqlPass);
//            pnlSqlCreds.Controls.Add(btnLoadDbs);

//            // ── Row 4: Database dropdown ──────────────────────────
//            // (Y position is set dynamically in code — UpdateDatabaseRowPosition())
//            var sep2 = new Label
//            {
//                Name = "sep2",
//                Location = new Point(14, 228),
//                Size = new Size(508, 1),
//                BackColor = Color.FromArgb(220, 225, 240)
//            };

//            lblDatabaseLabel.Name = "lblDatabaseLabel";
//            lblDatabaseLabel.Text = "Select Database:";
//            lblDatabaseLabel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
//            lblDatabaseLabel.Location = new Point(14, 238);
//            lblDatabaseLabel.AutoSize = true;

//            cboDatabases.Name = "cboDatabases";
//            cboDatabases.Location = new Point(14, 258);
//            cboDatabases.Size = new Size(508, 26);
//            cboDatabases.DropDownStyle = ComboBoxStyle.DropDownList;
//            cboDatabases.Font = new Font("Segoe UI", 9.5f);
//            cboDatabases.Enabled = false;

//            lblDbLoading.Text = "⏳  Databases load ho rahi hain...";
//            lblDbLoading.Font = new Font("Segoe UI", 8f, FontStyle.Italic);
//            lblDbLoading.ForeColor = Color.Gray;
//            lblDbLoading.Location = new Point(14, 290);
//            lblDbLoading.AutoSize = true;
//            lblDbLoading.Visible = false;

//            // Add all controls to pnlSqlServer
//            pnlSqlServer.Controls.Add(lblServerLabel);
//            pnlSqlServer.Controls.Add(cboServer);
//            pnlSqlServer.Controls.Add(btnRefresh);
//            pnlSqlServer.Controls.Add(lblServerLoading);
//            pnlSqlServer.Controls.Add(sep1);
//            pnlSqlServer.Controls.Add(lblAuthType);
//            pnlSqlServer.Controls.Add(rdoWinAuth);
//            pnlSqlServer.Controls.Add(rdoSqlAuth);
//            pnlSqlServer.Controls.Add(pnlSqlCreds);
//            pnlSqlServer.Controls.Add(sep2);
//            pnlSqlServer.Controls.Add(lblDatabaseLabel);
//            pnlSqlServer.Controls.Add(cboDatabases);
//            pnlSqlServer.Controls.Add(lblDbLoading);

//            // ══════════════════════════════════════════════════════
//            //  LOCALDB PANEL
//            // ══════════════════════════════════════════════════════
//            pnlLocalDb.Location = new Point(20, 158);
//            pnlLocalDb.Size = new Size(540, 118);
//            pnlLocalDb.BackColor = Color.White;
//            pnlLocalDb.BorderStyle = BorderStyle.FixedSingle;
//            pnlLocalDb.Visible = false;

//            lblMdfLabel.Text = "Database File (.mdf):";
//            lblMdfLabel.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
//            lblMdfLabel.Location = new Point(14, 14);
//            lblMdfLabel.AutoSize = true;

//            txtMdfPath.Location = new Point(14, 34);
//            txtMdfPath.Size = new Size(396, 26);
//            txtMdfPath.Font = new Font("Segoe UI", 9f);
//            txtMdfPath.ReadOnly = true;
//            txtMdfPath.BackColor = Color.FromArgb(245, 247, 250);

//            btnBrowse.Text = "📂  Browse";
//            btnBrowse.Location = new Point(418, 33);
//            btnBrowse.Size = new Size(104, 27);
//            btnBrowse.FlatStyle = FlatStyle.Flat;
//            btnBrowse.BackColor = Color.FromArgb(21, 82, 163);
//            btnBrowse.ForeColor = Color.White;
//            btnBrowse.Font = new Font("Segoe UI", 8.5f, FontStyle.Bold);
//            btnBrowse.FlatAppearance.BorderSize = 0;
//            btnBrowse.Cursor = Cursors.Hand;

//            lblLocalDbNote.Text = "ℹ  Sirf .mdf file ka path select karein — database naam automatic detect hoga.";
//            lblLocalDbNote.Font = new Font("Segoe UI", 8f, FontStyle.Italic);
//            lblLocalDbNote.ForeColor = Color.FromArgb(100, 120, 160);
//            lblLocalDbNote.Location = new Point(14, 70);
//            lblLocalDbNote.Size = new Size(508, 32);

//            pnlLocalDb.Controls.Add(lblMdfLabel);
//            pnlLocalDb.Controls.Add(txtMdfPath);
//            pnlLocalDb.Controls.Add(btnBrowse);
//            pnlLocalDb.Controls.Add(lblLocalDbNote);

//            // ══════════════════════════════════════════════════════
//            //  STATUS BAR
//            // ══════════════════════════════════════════════════════
//            pnlStatusBar.Location = new Point(20, 540);
//            pnlStatusBar.Size = new Size(540, 30);
//            pnlStatusBar.BorderStyle = BorderStyle.FixedSingle;
//            pnlStatusBar.Visible = false;

//            lblStatusIcon.Text = "●";
//            lblStatusIcon.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
//            lblStatusIcon.Location = new Point(8, 5);
//            lblStatusIcon.AutoSize = true;

//            lblStatusText.Text = string.Empty;
//            lblStatusText.Font = new Font("Segoe UI", 8.5f);
//            lblStatusText.Location = new Point(28, 7);
//            lblStatusText.AutoSize = true;

//            pnlStatusBar.Controls.Add(lblStatusIcon);
//            pnlStatusBar.Controls.Add(lblStatusText);

//            // ══════════════════════════════════════════════════════
//            //  FOOTER
//            // ══════════════════════════════════════════════════════
//            pnlFooter.Dock = DockStyle.Bottom;
//            pnlFooter.Height = 58;
//            pnlFooter.BackColor = Color.FromArgb(235, 239, 248);

//            btnCancel.Text = "Cancel";
//            btnCancel.Size = new Size(88, 34);
//            btnCancel.Location = new Point(18, 12);
//            btnCancel.FlatStyle = FlatStyle.Flat;
//            btnCancel.BackColor = Color.White;
//            btnCancel.ForeColor = Color.FromArgb(80, 80, 80);
//            btnCancel.Font = new Font("Segoe UI", 9f);
//            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 210);
//            btnCancel.Cursor = Cursors.Hand;

//            btnTestConn.Text = "🔌  Test Connection";
//            btnTestConn.Size = new Size(150, 34);
//            btnTestConn.Location = new Point(222, 12);
//            btnTestConn.FlatStyle = FlatStyle.Flat;
//            btnTestConn.BackColor = Color.White;
//            btnTestConn.ForeColor = Color.FromArgb(21, 82, 163);
//            btnTestConn.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
//            btnTestConn.FlatAppearance.BorderColor = Color.FromArgb(21, 82, 163);
//            btnTestConn.Cursor = Cursors.Hand;

//            btnSave.Text = "✔  Save & Continue";
//            btnSave.Size = new Size(154, 34);
//            btnSave.Location = new Point(380, 12);
//            btnSave.FlatStyle = FlatStyle.Flat;
//            btnSave.BackColor = Color.FromArgb(22, 142, 60);
//            btnSave.ForeColor = Color.White;
//            btnSave.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
//            btnSave.FlatAppearance.BorderSize = 0;
//            btnSave.Cursor = Cursors.Hand;

//            pnlFooter.Controls.Add(btnCancel);
//            pnlFooter.Controls.Add(btnTestConn);
//            pnlFooter.Controls.Add(btnSave);

//            // ══════════════════════════════════════════════════════
//            //  ADD TO FORM
//            // ══════════════════════════════════════════════════════
//            this.Controls.Add(pnlHeader);
//            this.Controls.Add(grpDbType);
//            this.Controls.Add(pnlSqlServer);
//            this.Controls.Add(pnlLocalDb);
//            this.Controls.Add(pnlStatusBar);
//            this.Controls.Add(pnlFooter);

//            this.ResumeLayout(false);
//        }
//    }
//}



using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Setup
{
    partial class DatabaseSetupForm
    {
        private System.ComponentModel.IContainer components = null;

        // ══════════════════════════════════════════════════════════
        //  CONTROL DECLARATIONS
        // ══════════════════════════════════════════════════════════

        // Header
        private Panel pnlHeader;
        private Label lblAppName;
        private Label lblHeaderSub;

        // DB Type Group
        private GroupBox grpDbType;
        private RadioButton rdoSqlServer;
        private RadioButton rdoLocalDb;

        // ── SQL Server Panel ──────────────────────────────────────
        private Panel pnlSqlServer;

        //   Row 1: Server
        private Label lblServerLabel;
        private ComboBox cboServer;
        private Button btnRefresh;
        private Label lblServerLoading;

        //   Row 2: Auth Type
        private Label lblAuthType;
        private RadioButton rdoWinAuth;
        private RadioButton rdoSqlAuth;

        //   Separators
        private Label sep1;
        private Label sep2;

        //   Row 3: SQL Auth credentials (hide/show)
        private Panel pnlSqlCreds;
        private Label lblSqlUser;
        private TextBox txtSqlUser;
        private Label lblSqlPass;
        private TextBox txtSqlPass;
        private Button btnLoadDbs;

        //   Row 4: Database dropdown
        private Label lblDatabaseLabel;
        private ComboBox cboDatabases;
        private Label lblDbLoading;

        // ── LocalDB Panel ─────────────────────────────────────────
        private Panel pnlLocalDb;
        private Label lblMdfLabel;
        private TextBox txtMdfPath;
        private Button btnBrowse;
        private Label lblLocalDbNote;

        // Status bar
        private Panel pnlStatusBar;
        private Label lblStatusIcon;
        private Label lblStatusText;

        // Footer
        private Panel pnlFooter;
        private Button btnTestConn;
        private Button btnSave;
        private Button btnCancel;

        // ══════════════════════════════════════════════════════════
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        // ══════════════════════════════════════════════════════════
        //  InitializeComponent
        // ══════════════════════════════════════════════════════════
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblAppName = new System.Windows.Forms.Label();
            this.lblHeaderSub = new System.Windows.Forms.Label();
            this.grpDbType = new System.Windows.Forms.GroupBox();
            this.rdoSqlServer = new System.Windows.Forms.RadioButton();
            this.rdoLocalDb = new System.Windows.Forms.RadioButton();
            this.pnlSqlServer = new System.Windows.Forms.Panel();
            this.lblServerLabel = new System.Windows.Forms.Label();
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblServerLoading = new System.Windows.Forms.Label();
            this.sep1 = new System.Windows.Forms.Label();
            this.lblAuthType = new System.Windows.Forms.Label();
            this.rdoWinAuth = new System.Windows.Forms.RadioButton();
            this.rdoSqlAuth = new System.Windows.Forms.RadioButton();
            this.pnlSqlCreds = new System.Windows.Forms.Panel();
            this.lblSqlUser = new System.Windows.Forms.Label();
            this.txtSqlUser = new System.Windows.Forms.TextBox();
            this.lblSqlPass = new System.Windows.Forms.Label();
            this.txtSqlPass = new System.Windows.Forms.TextBox();
            this.btnLoadDbs = new System.Windows.Forms.Button();
            this.sep2 = new System.Windows.Forms.Label();
            this.lblDatabaseLabel = new System.Windows.Forms.Label();
            this.cboDatabases = new System.Windows.Forms.ComboBox();
            this.lblDbLoading = new System.Windows.Forms.Label();
            this.pnlLocalDb = new System.Windows.Forms.Panel();
            this.lblMdfLabel = new System.Windows.Forms.Label();
            this.txtMdfPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblLocalDbNote = new System.Windows.Forms.Label();
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusIcon = new System.Windows.Forms.Label();
            this.lblStatusText = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTestConn = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.grpDbType.SuspendLayout();
            this.pnlSqlServer.SuspendLayout();
            this.pnlSqlCreds.SuspendLayout();
            this.pnlLocalDb.SuspendLayout();
            this.pnlStatusBar.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(27)))), ((int)(((byte)(75)))));
            this.pnlHeader.Controls.Add(this.lblAppName);
            this.pnlHeader.Controls.Add(this.lblHeaderSub);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(580, 72);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblAppName.ForeColor = System.Drawing.Color.White;
            this.lblAppName.Location = new System.Drawing.Point(20, 10);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(253, 30);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "POS — Database Setup";
            // 
            // lblHeaderSub
            // 
            this.lblHeaderSub.AutoSize = true;
            this.lblHeaderSub.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblHeaderSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.lblHeaderSub.Location = new System.Drawing.Point(22, 42);
            this.lblHeaderSub.Name = "lblHeaderSub";
            this.lblHeaderSub.Size = new System.Drawing.Size(345, 20);
            this.lblHeaderSub.TabIndex = 1;
            this.lblHeaderSub.Text = "First time setup — This settings will save once only.\r\n";
            // 
            // grpDbType
            // 
            this.grpDbType.BackColor = System.Drawing.Color.White;
            this.grpDbType.Controls.Add(this.rdoSqlServer);
            this.grpDbType.Controls.Add(this.rdoLocalDb);
            this.grpDbType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpDbType.Location = new System.Drawing.Point(20, 88);
            this.grpDbType.Name = "grpDbType";
            this.grpDbType.Size = new System.Drawing.Size(540, 56);
            this.grpDbType.TabIndex = 1;
            this.grpDbType.TabStop = false;
            this.grpDbType.Text = "  Database Engine";
            // 
            // rdoSqlServer
            // 
            this.rdoSqlServer.AutoSize = true;
            this.rdoSqlServer.Checked = true;
            this.rdoSqlServer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rdoSqlServer.Location = new System.Drawing.Point(18, 22);
            this.rdoSqlServer.Name = "rdoSqlServer";
            this.rdoSqlServer.Size = new System.Drawing.Size(317, 25);
            this.rdoSqlServer.TabIndex = 0;
            this.rdoSqlServer.TabStop = true;
            this.rdoSqlServer.Text = "SQL Server  (full install / named instance)";
            // 
            // rdoLocalDb
            // 
            this.rdoLocalDb.AutoSize = true;
            this.rdoLocalDb.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rdoLocalDb.Location = new System.Drawing.Point(346, 22);
            this.rdoLocalDb.Name = "rdoLocalDb";
            this.rdoLocalDb.Size = new System.Drawing.Size(161, 25);
            this.rdoLocalDb.TabIndex = 1;
            this.rdoLocalDb.Text = "LocalDB  (.mdf file)";
            // 
            // pnlSqlServer
            // 
            this.pnlSqlServer.BackColor = System.Drawing.Color.White;
            this.pnlSqlServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSqlServer.Controls.Add(this.lblServerLabel);
            this.pnlSqlServer.Controls.Add(this.cboServer);
            this.pnlSqlServer.Controls.Add(this.btnRefresh);
            this.pnlSqlServer.Controls.Add(this.lblServerLoading);
            this.pnlSqlServer.Controls.Add(this.sep1);
            this.pnlSqlServer.Controls.Add(this.lblAuthType);
            this.pnlSqlServer.Controls.Add(this.rdoWinAuth);
            this.pnlSqlServer.Controls.Add(this.rdoSqlAuth);
            this.pnlSqlServer.Controls.Add(this.pnlSqlCreds);
            this.pnlSqlServer.Controls.Add(this.sep2);
            this.pnlSqlServer.Controls.Add(this.lblDatabaseLabel);
            this.pnlSqlServer.Controls.Add(this.cboDatabases);
            this.pnlSqlServer.Controls.Add(this.lblDbLoading);
            this.pnlSqlServer.Location = new System.Drawing.Point(20, 158);
            this.pnlSqlServer.Name = "pnlSqlServer";
            this.pnlSqlServer.Size = new System.Drawing.Size(540, 370);
            this.pnlSqlServer.TabIndex = 2;
            // 
            // lblServerLabel
            // 
            this.lblServerLabel.AutoSize = true;
            this.lblServerLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblServerLabel.Location = new System.Drawing.Point(14, 14);
            this.lblServerLabel.Name = "lblServerLabel";
            this.lblServerLabel.Size = new System.Drawing.Size(152, 20);
            this.lblServerLabel.TabIndex = 0;
            this.lblServerLabel.Text = "SQL Server Instance:";
            // 
            // cboServer
            // 
            this.cboServer.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboServer.Location = new System.Drawing.Point(14, 34);
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size(396, 29);
            this.cboServer.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(27)))), ((int)(((byte)(75)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(418, 33);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(104, 27);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "↺  Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // lblServerLoading
            // 
            this.lblServerLoading.AutoSize = true;
            this.lblServerLoading.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblServerLoading.ForeColor = System.Drawing.Color.Gray;
            this.lblServerLoading.Location = new System.Drawing.Point(14, 64);
            this.lblServerLoading.Name = "lblServerLoading";
            this.lblServerLoading.Size = new System.Drawing.Size(170, 19);
            this.lblServerLoading.TabIndex = 3;
            this.lblServerLoading.Text = "⏳  Trying to find server...";
            this.lblServerLoading.Visible = false;
            // 
            // sep1
            // 
            this.sep1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(240)))));
            this.sep1.Location = new System.Drawing.Point(14, 74);
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(508, 1);
            this.sep1.TabIndex = 4;
            // 
            // lblAuthType
            // 
            this.lblAuthType.AutoSize = true;
            this.lblAuthType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAuthType.Location = new System.Drawing.Point(14, 84);
            this.lblAuthType.Name = "lblAuthType";
            this.lblAuthType.Size = new System.Drawing.Size(118, 20);
            this.lblAuthType.TabIndex = 5;
            this.lblAuthType.Text = "Authentication:";
            // 
            // rdoWinAuth
            // 
            this.rdoWinAuth.AutoSize = true;
            this.rdoWinAuth.Checked = true;
            this.rdoWinAuth.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rdoWinAuth.Location = new System.Drawing.Point(14, 104);
            this.rdoWinAuth.Name = "rdoWinAuth";
            this.rdoWinAuth.Size = new System.Drawing.Size(201, 25);
            this.rdoWinAuth.TabIndex = 6;
            this.rdoWinAuth.TabStop = true;
            this.rdoWinAuth.Text = "Windows Authentication";
            // 
            // rdoSqlAuth
            // 
            this.rdoSqlAuth.AutoSize = true;
            this.rdoSqlAuth.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.rdoSqlAuth.Location = new System.Drawing.Point(220, 104);
            this.rdoSqlAuth.Name = "rdoSqlAuth";
            this.rdoSqlAuth.Size = new System.Drawing.Size(214, 25);
            this.rdoSqlAuth.TabIndex = 7;
            this.rdoSqlAuth.Text = "SQL Server Authentication";
            // 
            // pnlSqlCreds
            // 
            this.pnlSqlCreds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlSqlCreds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSqlCreds.Controls.Add(this.lblSqlUser);
            this.pnlSqlCreds.Controls.Add(this.txtSqlUser);
            this.pnlSqlCreds.Controls.Add(this.lblSqlPass);
            this.pnlSqlCreds.Controls.Add(this.txtSqlPass);
            this.pnlSqlCreds.Controls.Add(this.btnLoadDbs);
            this.pnlSqlCreds.Location = new System.Drawing.Point(14, 136);
            this.pnlSqlCreds.Name = "pnlSqlCreds";
            this.pnlSqlCreds.Size = new System.Drawing.Size(508, 80);
            this.pnlSqlCreds.TabIndex = 8;
            this.pnlSqlCreds.Visible = false;
            // 
            // lblSqlUser
            // 
            this.lblSqlUser.AutoSize = true;
            this.lblSqlUser.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSqlUser.Location = new System.Drawing.Point(12, 10);
            this.lblSqlUser.Name = "lblSqlUser";
            this.lblSqlUser.Size = new System.Drawing.Size(84, 20);
            this.lblSqlUser.TabIndex = 0;
            this.lblSqlUser.Text = "Username:";
            // 
            // txtSqlUser
            // 
            this.txtSqlUser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSqlUser.Location = new System.Drawing.Point(80, 8);
            this.txtSqlUser.Name = "txtSqlUser";
            this.txtSqlUser.Size = new System.Drawing.Size(160, 27);
            this.txtSqlUser.TabIndex = 1;
            // 
            // lblSqlPass
            // 
            this.lblSqlPass.AutoSize = true;
            this.lblSqlPass.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSqlPass.Location = new System.Drawing.Point(260, 10);
            this.lblSqlPass.Name = "lblSqlPass";
            this.lblSqlPass.Size = new System.Drawing.Size(80, 20);
            this.lblSqlPass.TabIndex = 2;
            this.lblSqlPass.Text = "Password:";
            // 
            // txtSqlPass
            // 
            this.txtSqlPass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSqlPass.Location = new System.Drawing.Point(326, 8);
            this.txtSqlPass.Name = "txtSqlPass";
            this.txtSqlPass.Size = new System.Drawing.Size(160, 27);
            this.txtSqlPass.TabIndex = 3;
            this.txtSqlPass.UseSystemPasswordChar = true;
            // 
            // btnLoadDbs
            // 
            this.btnLoadDbs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.btnLoadDbs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadDbs.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(82)))), ((int)(((byte)(163)))));
            this.btnLoadDbs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadDbs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadDbs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(82)))), ((int)(((byte)(163)))));
            this.btnLoadDbs.Location = new System.Drawing.Point(12, 44);
            this.btnLoadDbs.Name = "btnLoadDbs";
            this.btnLoadDbs.Size = new System.Drawing.Size(474, 28);
            this.btnLoadDbs.TabIndex = 4;
            this.btnLoadDbs.Text = "Load Databases  ▼";
            this.btnLoadDbs.UseVisualStyleBackColor = false;
            // 
            // sep2
            // 
            this.sep2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(240)))));
            this.sep2.Location = new System.Drawing.Point(14, 228);
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(508, 1);
            this.sep2.TabIndex = 9;
            // 
            // lblDatabaseLabel
            // 
            this.lblDatabaseLabel.AutoSize = true;
            this.lblDatabaseLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDatabaseLabel.Location = new System.Drawing.Point(14, 234);
            this.lblDatabaseLabel.Name = "lblDatabaseLabel";
            this.lblDatabaseLabel.Size = new System.Drawing.Size(123, 20);
            this.lblDatabaseLabel.TabIndex = 10;
            this.lblDatabaseLabel.Text = "Select Database:";
            // 
            // cboDatabases
            // 
            this.cboDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatabases.Enabled = false;
            this.cboDatabases.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboDatabases.Location = new System.Drawing.Point(14, 256);
            this.cboDatabases.Name = "cboDatabases";
            this.cboDatabases.Size = new System.Drawing.Size(508, 29);
            this.cboDatabases.TabIndex = 11;
            // 
            // lblDbLoading
            // 
            this.lblDbLoading.AutoSize = true;
            this.lblDbLoading.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblDbLoading.ForeColor = System.Drawing.Color.Gray;
            this.lblDbLoading.Location = new System.Drawing.Point(14, 295);
            this.lblDbLoading.Name = "lblDbLoading";
            this.lblDbLoading.Size = new System.Drawing.Size(190, 19);
            this.lblDbLoading.TabIndex = 12;
            this.lblDbLoading.Text = "⏳  Databases are loading....";
            this.lblDbLoading.Visible = false;
            // 
            // pnlLocalDb
            // 
            this.pnlLocalDb.BackColor = System.Drawing.Color.White;
            this.pnlLocalDb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocalDb.Controls.Add(this.lblMdfLabel);
            this.pnlLocalDb.Controls.Add(this.txtMdfPath);
            this.pnlLocalDb.Controls.Add(this.btnBrowse);
            this.pnlLocalDb.Controls.Add(this.lblLocalDbNote);
            this.pnlLocalDb.Location = new System.Drawing.Point(20, 158);
            this.pnlLocalDb.Name = "pnlLocalDb";
            this.pnlLocalDb.Size = new System.Drawing.Size(540, 118);
            this.pnlLocalDb.TabIndex = 3;
            this.pnlLocalDb.Visible = false;
            // 
            // lblMdfLabel
            // 
            this.lblMdfLabel.AutoSize = true;
            this.lblMdfLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMdfLabel.Location = new System.Drawing.Point(14, 14);
            this.lblMdfLabel.Name = "lblMdfLabel";
            this.lblMdfLabel.Size = new System.Drawing.Size(156, 20);
            this.lblMdfLabel.TabIndex = 0;
            this.lblMdfLabel.Text = "Database File (.mdf):";
            // 
            // txtMdfPath
            // 
            this.txtMdfPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.txtMdfPath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMdfPath.Location = new System.Drawing.Point(14, 34);
            this.txtMdfPath.Name = "txtMdfPath";
            this.txtMdfPath.ReadOnly = true;
            this.txtMdfPath.Size = new System.Drawing.Size(396, 27);
            this.txtMdfPath.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(82)))), ((int)(((byte)(163)))));
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnBrowse.ForeColor = System.Drawing.Color.White;
            this.btnBrowse.Location = new System.Drawing.Point(418, 33);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(104, 27);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "📂  Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            // 
            // lblLocalDbNote
            // 
            this.lblLocalDbNote.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblLocalDbNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(120)))), ((int)(((byte)(160)))));
            this.lblLocalDbNote.Location = new System.Drawing.Point(14, 70);
            this.lblLocalDbNote.Name = "lblLocalDbNote";
            this.lblLocalDbNote.Size = new System.Drawing.Size(508, 32);
            this.lblLocalDbNote.TabIndex = 3;
            this.lblLocalDbNote.Text = "ℹ  Sirf .mdf file ka path select karein — database naam automatic detect hoga.";
            // 
            // pnlStatusBar
            // 
            this.pnlStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatusBar.Controls.Add(this.lblStatusIcon);
            this.pnlStatusBar.Controls.Add(this.lblStatusText);
            this.pnlStatusBar.Location = new System.Drawing.Point(20, 529);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(540, 30);
            this.pnlStatusBar.TabIndex = 4;
            this.pnlStatusBar.Visible = false;
            // 
            // lblStatusIcon
            // 
            this.lblStatusIcon.AutoSize = true;
            this.lblStatusIcon.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblStatusIcon.Location = new System.Drawing.Point(8, 5);
            this.lblStatusIcon.Name = "lblStatusIcon";
            this.lblStatusIcon.Size = new System.Drawing.Size(23, 25);
            this.lblStatusIcon.TabIndex = 0;
            this.lblStatusIcon.Text = "●";
            // 
            // lblStatusText
            // 
            this.lblStatusText.AutoSize = true;
            this.lblStatusText.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatusText.Location = new System.Drawing.Point(28, 7);
            this.lblStatusText.Name = "lblStatusText";
            this.lblStatusText.Size = new System.Drawing.Size(0, 20);
            this.lblStatusText.TabIndex = 1;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(248)))));
            this.pnlFooter.Controls.Add(this.btnCancel);
            this.pnlFooter.Controls.Add(this.btnTestConn);
            this.pnlFooter.Controls.Add(this.btnSave);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 560);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(580, 50);
            this.pnlFooter.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(210)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.Location = new System.Drawing.Point(18, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 34);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnTestConn
            // 
            this.btnTestConn.BackColor = System.Drawing.Color.White;
            this.btnTestConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTestConn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(82)))), ((int)(((byte)(163)))));
            this.btnTestConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestConn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(82)))), ((int)(((byte)(163)))));
            this.btnTestConn.Location = new System.Drawing.Point(236, 7);
            this.btnTestConn.Name = "btnTestConn";
            this.btnTestConn.Size = new System.Drawing.Size(150, 34);
            this.btnTestConn.TabIndex = 1;
            this.btnTestConn.Text = "🔌  Test Connection";
            this.btnTestConn.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(400, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(154, 34);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "✔  Save & Continue";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // DatabaseSetupForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(580, 610);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.grpDbType);
            this.Controls.Add(this.pnlSqlServer);
            this.Controls.Add(this.pnlLocalDb);
            this.Controls.Add(this.pnlStatusBar);
            this.Controls.Add(this.pnlFooter);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseSetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Shop — Database Setup";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.grpDbType.ResumeLayout(false);
            this.grpDbType.PerformLayout();
            this.pnlSqlServer.ResumeLayout(false);
            this.pnlSqlServer.PerformLayout();
            this.pnlSqlCreds.ResumeLayout(false);
            this.pnlSqlCreds.PerformLayout();
            this.pnlLocalDb.ResumeLayout(false);
            this.pnlLocalDb.PerformLayout();
            this.pnlStatusBar.ResumeLayout(false);
            this.pnlStatusBar.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}