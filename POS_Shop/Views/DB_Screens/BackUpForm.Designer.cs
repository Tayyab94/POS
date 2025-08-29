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
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties17 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties18 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties19 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties20 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BackupBtn = new System.Windows.Forms.Button();
            this.BackScreenBtn = new System.Windows.Forms.Button();
            this.BrowsFileBtn = new System.Windows.Forms.Button();
            this.BrowsFilePathTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.BrowsFilePathTxt);
            this.groupBox1.Controls.Add(this.BrowsFileBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 180);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Backup Path";
            // 
            // BackupBtn
            // 
            this.BackupBtn.Enabled = false;
            this.BackupBtn.Location = new System.Drawing.Point(558, 259);
            this.BackupBtn.Name = "BackupBtn";
            this.BackupBtn.Size = new System.Drawing.Size(75, 33);
            this.BackupBtn.TabIndex = 1;
            this.BackupBtn.Text = "BackUp";
            this.BackupBtn.UseVisualStyleBackColor = true;
            this.BackupBtn.Click += new System.EventHandler(this.BackupBtn_Click);
            // 
            // BackScreenBtn
            // 
            this.BackScreenBtn.Location = new System.Drawing.Point(662, 259);
            this.BackScreenBtn.Name = "BackScreenBtn";
            this.BackScreenBtn.Size = new System.Drawing.Size(104, 33);
            this.BackScreenBtn.TabIndex = 2;
            this.BackScreenBtn.Text = "Back Screen";
            this.BackScreenBtn.UseVisualStyleBackColor = true;
            this.BackScreenBtn.Click += new System.EventHandler(this.BackScreenBtn_Click);
            // 
            // BrowsFileBtn
            // 
            this.BrowsFileBtn.Location = new System.Drawing.Point(630, 122);
            this.BrowsFileBtn.Name = "BrowsFileBtn";
            this.BrowsFileBtn.Size = new System.Drawing.Size(124, 37);
            this.BrowsFileBtn.TabIndex = 3;
            this.BrowsFileBtn.Text = "Brows File";
            this.BrowsFileBtn.UseVisualStyleBackColor = true;
            this.BrowsFileBtn.Click += new System.EventHandler(this.BrowsFileBtn_Click);
            // 
            // BrowsFilePathTxt
            // 
            this.BrowsFilePathTxt.AcceptsReturn = false;
            this.BrowsFilePathTxt.AcceptsTab = false;
            this.BrowsFilePathTxt.AnimationSpeed = 200;
            this.BrowsFilePathTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.BrowsFilePathTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.BrowsFilePathTxt.BackColor = System.Drawing.Color.Transparent;
            this.BrowsFilePathTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BrowsFilePathTxt.BackgroundImage")));
            this.BrowsFilePathTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.BrowsFilePathTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.BrowsFilePathTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.BrowsFilePathTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.BrowsFilePathTxt.BorderRadius = 1;
            this.BrowsFilePathTxt.BorderThickness = 1;
            this.BrowsFilePathTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.BrowsFilePathTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.BrowsFilePathTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.BrowsFilePathTxt.DefaultText = "";
            this.BrowsFilePathTxt.FillColor = System.Drawing.Color.White;
            this.BrowsFilePathTxt.HideSelection = true;
            this.BrowsFilePathTxt.IconLeft = null;
            this.BrowsFilePathTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.BrowsFilePathTxt.IconPadding = 10;
            this.BrowsFilePathTxt.IconRight = null;
            this.BrowsFilePathTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.BrowsFilePathTxt.Lines = new string[0];
            this.BrowsFilePathTxt.Location = new System.Drawing.Point(26, 65);
            this.BrowsFilePathTxt.MaxLength = 32767;
            this.BrowsFilePathTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.BrowsFilePathTxt.Modified = false;
            this.BrowsFilePathTxt.Multiline = false;
            this.BrowsFilePathTxt.Name = "BrowsFilePathTxt";
            stateProperties17.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties17.FillColor = System.Drawing.Color.Empty;
            stateProperties17.ForeColor = System.Drawing.Color.Empty;
            stateProperties17.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.BrowsFilePathTxt.OnActiveState = stateProperties17;
            stateProperties18.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties18.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties18.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.BrowsFilePathTxt.OnDisabledState = stateProperties18;
            stateProperties19.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties19.FillColor = System.Drawing.Color.Empty;
            stateProperties19.ForeColor = System.Drawing.Color.Empty;
            stateProperties19.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.BrowsFilePathTxt.OnHoverState = stateProperties19;
            stateProperties20.BorderColor = System.Drawing.Color.Silver;
            stateProperties20.FillColor = System.Drawing.Color.White;
            stateProperties20.ForeColor = System.Drawing.Color.Empty;
            stateProperties20.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.BrowsFilePathTxt.OnIdleState = stateProperties20;
            this.BrowsFilePathTxt.Padding = new System.Windows.Forms.Padding(3);
            this.BrowsFilePathTxt.PasswordChar = '\0';
            this.BrowsFilePathTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.BrowsFilePathTxt.PlaceholderText = "Enter text";
            this.BrowsFilePathTxt.ReadOnly = false;
            this.BrowsFilePathTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.BrowsFilePathTxt.SelectedText = "";
            this.BrowsFilePathTxt.SelectionLength = 0;
            this.BrowsFilePathTxt.SelectionStart = 0;
            this.BrowsFilePathTxt.ShortcutsEnabled = true;
            this.BrowsFilePathTxt.Size = new System.Drawing.Size(728, 41);
            this.BrowsFilePathTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.BrowsFilePathTxt.TabIndex = 4;
            this.BrowsFilePathTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.BrowsFilePathTxt.TextMarginBottom = 0;
            this.BrowsFilePathTxt.TextMarginLeft = 3;
            this.BrowsFilePathTxt.TextMarginTop = 0;
            this.BrowsFilePathTxt.TextPlaceholder = "Enter text";
            this.BrowsFilePathTxt.UseSystemPasswordChar = false;
            this.BrowsFilePathTxt.WordWrap = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(325, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Brows folder wher you want to save Database backup";
            // 
            // BackUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 340);
            this.ControlBox = false;
            this.Controls.Add(this.BackScreenBtn);
            this.Controls.Add(this.BackupBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "BackUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BackUp From";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Bunifu.UI.WinForms.BunifuTextBox BrowsFilePathTxt;
        private System.Windows.Forms.Button BrowsFileBtn;
        private System.Windows.Forms.Button BackupBtn;
        private System.Windows.Forms.Button BackScreenBtn;
        private System.Windows.Forms.Label label1;
    }
}