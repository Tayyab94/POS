namespace POS_Shop.Views.DB_Screens
{
    partial class BackUpForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackUpForm));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BrowsFileBtn = new System.Windows.Forms.Button();
            this.BackupBtn = new System.Windows.Forms.Button();
            this.BackScreenBtn = new System.Windows.Forms.Button();
            this.X_CloseBtn = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MinimizeBtn = new System.Windows.Forms.Button();
            this.ExpandBtn = new System.Windows.Forms.Button();
            this.BrowsFilePathTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BrowsFilePathTxt);
            this.groupBox1.Controls.Add(this.BrowsFileBtn);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // BrowsFileBtn
            // 
            resources.ApplyResources(this.BrowsFileBtn, "BrowsFileBtn");
            this.BrowsFileBtn.Name = "BrowsFileBtn";
            this.BrowsFileBtn.UseVisualStyleBackColor = true;
            this.BrowsFileBtn.Click += new System.EventHandler(this.BrowsFileBtn_Click);
            // 
            // BackupBtn
            // 
            resources.ApplyResources(this.BackupBtn, "BackupBtn");
            this.BackupBtn.Name = "BackupBtn";
            this.BackupBtn.UseVisualStyleBackColor = true;
            this.BackupBtn.Click += new System.EventHandler(this.BackupBtn_Click);
            // 
            // BackScreenBtn
            // 
            resources.ApplyResources(this.BackScreenBtn, "BackScreenBtn");
            this.BackScreenBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BackScreenBtn.Name = "BackScreenBtn";
            this.BackScreenBtn.UseVisualStyleBackColor = true;
            this.BackScreenBtn.Click += new System.EventHandler(this.BackScreenBtn_Click);
            // 
            // X_CloseBtn
            // 
            resources.ApplyResources(this.X_CloseBtn, "X_CloseBtn");
            this.X_CloseBtn.BackColor = System.Drawing.Color.Red;
            this.X_CloseBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.X_CloseBtn.Name = "X_CloseBtn";
            this.X_CloseBtn.Click += new System.EventHandler(this.X_CloseBtn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SlateBlue;
            this.panel1.Controls.Add(this.MinimizeBtn);
            this.panel1.Controls.Add(this.ExpandBtn);
            this.panel1.Controls.Add(this.X_CloseBtn);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // MinimizeBtn
            // 
            resources.ApplyResources(this.MinimizeBtn, "MinimizeBtn");
            this.MinimizeBtn.BackColor = System.Drawing.Color.Yellow;
            this.MinimizeBtn.BackgroundImage = global::POS_Shop.Properties.Resources.minimizeSign;
            this.MinimizeBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.MinimizeBtn.Name = "MinimizeBtn";
            this.MinimizeBtn.UseVisualStyleBackColor = false;
            this.MinimizeBtn.Click += new System.EventHandler(this.MinimizeBtn_Click);
            // 
            // ExpandBtn
            // 
            resources.ApplyResources(this.ExpandBtn, "ExpandBtn");
            this.ExpandBtn.BackColor = System.Drawing.Color.DodgerBlue;
            this.ExpandBtn.BackgroundImage = global::POS_Shop.Properties.Resources.Expand;
            this.ExpandBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExpandBtn.Name = "ExpandBtn";
            this.ExpandBtn.UseVisualStyleBackColor = false;
            this.ExpandBtn.Click += new System.EventHandler(this.ExpandBtn_Click);
            // 
            // BrowsFilePathTxt
            // 
            this.BrowsFilePathTxt.AcceptsReturn = false;
            this.BrowsFilePathTxt.AcceptsTab = false;
            resources.ApplyResources(this.BrowsFilePathTxt, "BrowsFilePathTxt");
            this.BrowsFilePathTxt.AnimationSpeed = 200;
            this.BrowsFilePathTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.BrowsFilePathTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.BrowsFilePathTxt.BackColor = System.Drawing.Color.Transparent;
            this.BrowsFilePathTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.BrowsFilePathTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BrowsFilePathTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.BrowsFilePathTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.BrowsFilePathTxt.BorderRadius = 1;
            this.BrowsFilePathTxt.BorderThickness = 1;
            this.BrowsFilePathTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.BrowsFilePathTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BrowsFilePathTxt.DefaultFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowsFilePathTxt.DefaultText = "";
            this.BrowsFilePathTxt.FillColor = System.Drawing.Color.White;
            this.BrowsFilePathTxt.HideSelection = true;
            this.BrowsFilePathTxt.IconLeft = null;
            this.BrowsFilePathTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.BrowsFilePathTxt.IconPadding = 10;
            this.BrowsFilePathTxt.IconRight = null;
            this.BrowsFilePathTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.BrowsFilePathTxt.Lines = new string[0];
            this.BrowsFilePathTxt.MaxLength = 32767;
            this.BrowsFilePathTxt.Modified = false;
            this.BrowsFilePathTxt.Multiline = false;
            this.BrowsFilePathTxt.Name = "BrowsFilePathTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.BrowsFilePathTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.BrowsFilePathTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.BrowsFilePathTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.BrowsFilePathTxt.OnIdleState = stateProperties4;
            this.BrowsFilePathTxt.PasswordChar = '\0';
            this.BrowsFilePathTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.BrowsFilePathTxt.PlaceholderText = "Enter text";
            this.BrowsFilePathTxt.ReadOnly = false;
            this.BrowsFilePathTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.BrowsFilePathTxt.SelectedText = "";
            this.BrowsFilePathTxt.SelectionLength = 0;
            this.BrowsFilePathTxt.SelectionStart = 0;
            this.BrowsFilePathTxt.ShortcutsEnabled = true;
            this.BrowsFilePathTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.BrowsFilePathTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.BrowsFilePathTxt.TextMarginBottom = 0;
            this.BrowsFilePathTxt.TextMarginLeft = 3;
            this.BrowsFilePathTxt.TextMarginTop = 0;
            this.BrowsFilePathTxt.TextPlaceholder = "Enter text";
            this.BrowsFilePathTxt.UseSystemPasswordChar = false;
            this.BrowsFilePathTxt.WordWrap = true;
            // 
            // BackUpForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GrayText;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BackScreenBtn);
            this.Controls.Add(this.BackupBtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BackUpForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Bunifu.UI.WinForms.BunifuTextBox BrowsFilePathTxt;
        private System.Windows.Forms.Button BrowsFileBtn;
        private System.Windows.Forms.Button BackupBtn;
        private System.Windows.Forms.Button BackScreenBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label X_CloseBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ExpandBtn;
        private System.Windows.Forms.Button MinimizeBtn;
    }
}