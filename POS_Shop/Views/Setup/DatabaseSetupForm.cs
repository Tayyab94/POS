using POS_Shop.Config;
using POS_Shop.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Setup
{
      /// <summary>
    /// Database Setup Form — sirf pehli baar open hota hai.
    ///
    /// Flow:
    ///   1. DB Engine choose karo  (SQL Server / LocalDB)
    ///   2. SQL Server:
    ///      a. Server instance select karo (auto-loaded)
    ///      b. Auth type choose karo (Windows / SQL Server)
    ///      c. SQL Auth: username + password dalo → "Load Databases" click
    ///         Windows Auth: server select ke baad databases auto-load
    ///      d. Database dropdown se select karo
    ///   3. LocalDB: .mdf file browse karo
    ///   4. Test Connection (optional) → Save & Continue
    ///
    /// DialogResult.OK  = saved, app proceed kare
    /// DialogResult.Cancel = app band kare
    /// </summary>
    public partial class DatabaseSetupForm : Form
    {
        // ── State ─────────────────────────────────────────────────
        private bool _serverListLoaded = false;

        // ── Constructor ───────────────────────────────────────────
        public DatabaseSetupForm()
        {
            InitializeComponent();
            AttachEvents();
        }

        // ══════════════════════════════════════════════════════════
        //  EVENT WIRING
        // ══════════════════════════════════════════════════════════
        private void AttachEvents()
        {
            this.Load += Form_Load;

            // DB engine toggle
            rdoSqlServer.CheckedChanged += DbEngine_Changed;
            rdoLocalDb.CheckedChanged   += DbEngine_Changed;

            // Server
            btnRefresh.Click               += BtnRefresh_Click;
            cboServer.SelectedIndexChanged += CboServer_SelectedIndexChanged;
            cboServer.Leave                += CboServer_Leave;

            // Auth toggle
            rdoWinAuth.CheckedChanged  += AuthType_Changed;
            rdoSqlAuth.CheckedChanged  += AuthType_Changed;

            // Load DBs button (SQL Auth only — Windows Auth auto-loads on server select)
            btnLoadDbs.Click += BtnLoadDbs_Click;

            // LocalDB
            btnBrowse.Click += BtnBrowse_Click;

            // Footer
            btnTestConn.Click += BtnTestConn_Click;
            btnSave.Click     += BtnSave_Click;
            btnCancel.Click   += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        // ══════════════════════════════════════════════════════════
        //  FORM LOAD
        // ══════════════════════════════════════════════════════════
        private async void Form_Load(object sender, EventArgs e)
        {
            await LoadServersAsync();
        }

        // ══════════════════════════════════════════════════════════
        //  DB ENGINE TOGGLE  (SQL Server ↔ LocalDB)
        // ══════════════════════════════════════════════════════════
        private async void DbEngine_Changed(object sender, EventArgs e)
        {
            HideStatus();

            if (rdoSqlServer.Checked)
            {
                pnlSqlServer.Visible = true;
                pnlLocalDb.Visible   = false;

                if (!_serverListLoaded)
                    await LoadServersAsync();
            }
            else
            {
                pnlSqlServer.Visible = false;
                pnlLocalDb.Visible   = true;
            }
        }

        // ══════════════════════════════════════════════════════════
        //  SERVER LIST LOADING
        // ══════════════════════════════════════════════════════════
        private async Task LoadServersAsync()
        {
            SetServerLoadingState(true);
            cboServer.Items.Clear();
            cboDatabases.Items.Clear();
            cboDatabases.Enabled = false;

            List<string> servers = null;
            try
            {
                servers = await Task.Run(() => SqlServerHelper.GetLocalSqlServerInstances());
                _serverListLoaded = true;
            }
            catch (Exception ex)
            {
                ShowStatus($"Server load error: {ex.Message}", StatusKind.Warning);
            }
            finally
            {
                SetServerLoadingState(false);
            }

            if (servers == null || servers.Count == 0)
            {
                ShowStatus("Koi instance detect nahi hua — manually naam type karein.", StatusKind.Warning);
                return;
            }

            foreach (var s in servers) cboServer.Items.Add(s);
            cboServer.SelectedIndex = 0;

            // Windows Auth mein databases auto-load karo
            if (rdoWinAuth.Checked)
                await LoadDatabasesAsync();
        }

        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            _serverListLoaded = false;
            await LoadServersAsync();
        }

        // Server select hone par (dropdown change)
        private async void CboServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideStatus();
            if (rdoWinAuth.Checked && !string.IsNullOrWhiteSpace(cboServer.Text))
                await LoadDatabasesAsync();
        }

        // Server manually type karne ke baad focus change hone par
        private async void CboServer_Leave(object sender, EventArgs e)
        {
            if (rdoWinAuth.Checked && !string.IsNullOrWhiteSpace(cboServer.Text))
                await LoadDatabasesAsync();
        }

        private void SetServerLoadingState(bool loading)
        {
            btnRefresh.Enabled       = !loading;
            cboServer.Enabled        = !loading;
            lblServerLoading.Visible = loading;
        }

        // ══════════════════════════════════════════════════════════
        //  AUTH TYPE TOGGLE  (Windows ↔ SQL Server Auth)
        // ══════════════════════════════════════════════════════════
        private async void AuthType_Changed(object sender, EventArgs e)
        {
            HideStatus();
            cboDatabases.Items.Clear();
            cboDatabases.Enabled = false;

            if (rdoWinAuth.Checked)
            {
                // Hide credentials panel — push DB row up
                pnlSqlCreds.Visible = false;
                UpdateDatabaseRowY(228);         // original Y

                // Auto-load databases if server is already selected
                if (!string.IsNullOrWhiteSpace(cboServer.Text))
                    await LoadDatabasesAsync();
            }
            else
            {
                // Show credentials panel — push DB row down
                pnlSqlCreds.Visible = true;
                UpdateDatabaseRowY(228 + 94);   // shift down by creds panel height + gap
            }
        }

        /// <summary>
        /// Database dropdown row Y position update karta hai
        /// (credentials panel show/hide hone par)
        /// </summary>
        private void UpdateDatabaseRowY(int y)
        {
            // sep2
            var sep2 = pnlSqlServer.Controls["sep2"];
            if (sep2 != null) sep2.Location = new Point(14, y);

            lblDatabaseLabel.Location = new Point(14, y + 10);
            cboDatabases.Location     = new Point(14, y + 30);
            lblDbLoading.Location     = new Point(14, y + 62);
        }

        // ══════════════════════════════════════════════════════════
        //  DATABASE LIST LOADING
        // ══════════════════════════════════════════════════════════

        // Windows Auth: auto-called on server select
        // SQL Auth: called when user clicks "Load Databases"
        private async void BtnLoadDbs_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboServer.Text))
            {
                ShowStatus("Select the Server First", StatusKind.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSqlUser.Text))
            {
                ShowStatus("Add UserName", StatusKind.Warning);
                txtSqlUser.Focus();
                return;
            }
            await LoadDatabasesAsync();
        }

        private async Task LoadDatabasesAsync()
        {
            string server   = cboServer.Text.Trim();
            bool   winAuth  = rdoWinAuth.Checked;
            string user     = txtSqlUser.Text.Trim();
            string pass     = txtSqlPass.Text;

            cboDatabases.Items.Clear();
            cboDatabases.Enabled   = false;
            lblDbLoading.Visible   = true;
            btnLoadDbs.Enabled     = false;

            (List<string> dbs, string err) result = (null, null);

            try
            {
                result = await Task.Run(() =>
                    SqlServerHelper.GetDatabases(server, winAuth, user, pass));
            }
            finally
            {
                lblDbLoading.Visible = false;
                btnLoadDbs.Enabled   = true;
            }

            if (!string.IsNullOrEmpty(result.err))
            {
                ShowStatus($"Databases loading error: {result.err}", StatusKind.Error);
                return;
            }

            if (result.dbs == null || result.dbs.Count == 0)
            {
                ShowStatus("Is server par koi user database nahi mili.", StatusKind.Warning);
                return;
            }

            foreach (var db in result.dbs)
                cboDatabases.Items.Add(db);

            cboDatabases.Enabled      = true;
            cboDatabases.SelectedIndex = 0;
            ShowStatus($"{result.dbs.Count} databases found — please select.", StatusKind.Info);
        }

        // ══════════════════════════════════════════════════════════
        //  BROWSE .MDF  (LocalDB)
        // ══════════════════════════════════════════════════════════
        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title  = ".mdf Database File Select Karein";
                dlg.Filter = "SQL Server Database|*.mdf|All Files|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                    txtMdfPath.Text = dlg.FileName;
            }
        }

        // ══════════════════════════════════════════════════════════
        //  TEST CONNECTION
        // ══════════════════════════════════════════════════════════
        private async void BtnTestConn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(showMessages: false))
            {
                ShowStatus("Please fill all the required fields.", StatusKind.Warning);
                return;
            }

            btnTestConn.Enabled = false;
            ShowStatus("Testing connection...", StatusKind.Info);

            string connStr = BuildTempConfig().BuildConnectionString();
            (bool ok, string err) result = (false, null);

            try
            {
                result = await Task.Run(() => SqlServerHelper.TestConnection(connStr));
            }
            finally
            {
                btnTestConn.Enabled = true;
            }

            if (result.ok)
                ShowStatus("✅  Connection Successful! Database connected.", StatusKind.Success);
            else
                ShowStatus($"❌  Fail: {result.err}", StatusKind.Error);
        }

        // ══════════════════════════════════════════════════════════
        //  SAVE & CONTINUE
        // ══════════════════════════════════════════════════════════
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(showMessages: true)) return;

            try
            {
                ConfigManager.Save(BuildTempConfig());

                MessageBox.Show(
                    "✅  Configuration saved!\n\n" +
                    $"File: {ConfigManager.GetConfigFilePath()}\n\n" +
                    "You can continue now.",
                    "Setup Complete",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save error:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════════════════
        //  HELPERS
        // ══════════════════════════════════════════════════════════

        /// <summary>UI se AppConfig build karta hai</summary>
        private AppConfig BuildTempConfig()
        {
            if (rdoSqlServer.Checked)
            {
                return new AppConfig
                {
                    DatabaseType = "SqlServer",
                    ServerName   = cboServer.Text.Trim(),
                    DatabaseName = cboDatabases.Text.Trim(),
                    AuthType     = rdoWinAuth.Checked ? "Windows" : "SqlAuth",
                    SqlUsername  = rdoSqlAuth.Checked ? txtSqlUser.Text.Trim() : null,
                    SqlPassword  = rdoSqlAuth.Checked ? txtSqlPass.Text : null
                };
            }

            return new AppConfig
            {
                DatabaseType = "LocalDb",
                LocalDbPath  = txtMdfPath.Text.Trim()
            };
        }

        /// <summary>Input validation</summary>
        private bool ValidateInputs(bool showMessages)
        {
            if (rdoSqlServer.Checked)
            {
                if (string.IsNullOrWhiteSpace(cboServer.Text))
                    return Fail("Server naam is mandatory.", cboServer, showMessages);

                if (rdoSqlAuth.Checked)
                {
                    if (string.IsNullOrWhiteSpace(txtSqlUser.Text))
                        return Fail("SQL Username is mandatory.", txtSqlUser, showMessages);
                    if (string.IsNullOrWhiteSpace(txtSqlPass.Text))
                        return Fail("Password is mandatory.", txtSqlPass, showMessages);
                }

                if (cboDatabases.Items.Count == 0 || string.IsNullOrWhiteSpace(cboDatabases.Text))
                    return Fail("Select a Database — first load the databases.", cboDatabases, showMessages);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtMdfPath.Text))
                    return Fail("Select an .mdf file.", btnBrowse, showMessages);

                if (!System.IO.File.Exists(txtMdfPath.Text))
                    return Fail("The selected .mdf file does not exist.", btnBrowse, showMessages);
            }

            return true;
        }

        private bool Fail(string msg, Control focus, bool showMsg)
        {
            if (showMsg)
            {
                MessageBox.Show(msg, "Incomplete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                focus?.Focus();
            }
            else
            {
                ShowStatus(msg, StatusKind.Warning);
            }
            return false;
        }

        // ── Status bar ────────────────────────────────────────────
        private enum StatusKind { Info, Success, Warning, Error }

        private void ShowStatus(string message, StatusKind kind)
        {
            pnlStatusBar.Visible = true;
            lblStatusText.Text   = message;

            Color bg, icon, text;
            switch (kind)
            {
                case StatusKind.Success:
                    bg = Color.FromArgb(235, 252, 240); icon = Color.FromArgb(22, 142, 60);  text = Color.FromArgb(20, 100, 40);  break;
                case StatusKind.Error:
                    bg = Color.FromArgb(255, 240, 240); icon = Color.FromArgb(180, 30, 30);  text = Color.FromArgb(150, 20, 20);  break;
                case StatusKind.Warning:
                    bg = Color.FromArgb(255, 250, 230); icon = Color.FromArgb(160, 110, 0);  text = Color.FromArgb(130, 90, 0);   break;
                default:
                    bg = Color.FromArgb(235, 242, 255); icon = Color.FromArgb(21, 82, 163);  text = Color.FromArgb(30, 70, 150);  break;
            }

            pnlStatusBar.BackColor  = bg;
            lblStatusIcon.ForeColor = icon;
            lblStatusText.ForeColor = text;
        }

        private void HideStatus() => pnlStatusBar.Visible = false;
    }
}
