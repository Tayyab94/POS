namespace POS_Shop.Views.DB_Screens
{
    partial class RestoreDbForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreDbForm));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties21 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties22 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties23 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties24 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.BackScreenBtn = new System.Windows.Forms.Button();
            this.RestoreDatabaseBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RestoreFilePathTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.BrowsFileBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BackScreenBtn
            // 
            this.BackScreenBtn.Location = new System.Drawing.Point(662, 263);
            this.BackScreenBtn.Name = "BackScreenBtn";
            this.BackScreenBtn.Size = new System.Drawing.Size(104, 33);
            this.BackScreenBtn.TabIndex = 5;
            this.BackScreenBtn.Text = "Back Screen";
            this.BackScreenBtn.UseVisualStyleBackColor = true;
            this.BackScreenBtn.Click += new System.EventHandler(this.BackScreenBtn_Click);
            // 
            // RestoreDatabaseBtn
            // 
            this.RestoreDatabaseBtn.Enabled = false;
            this.RestoreDatabaseBtn.Location = new System.Drawing.Point(475, 263);
            this.RestoreDatabaseBtn.Name = "RestoreDatabaseBtn";
            this.RestoreDatabaseBtn.Size = new System.Drawing.Size(158, 33);
            this.RestoreDatabaseBtn.TabIndex = 4;
            this.RestoreDatabaseBtn.Text = "Restore Database";
            this.RestoreDatabaseBtn.UseVisualStyleBackColor = true;
            this.RestoreDatabaseBtn.Click += new System.EventHandler(this.RestoreDatabaseBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RestoreFilePathTxt);
            this.groupBox1.Controls.Add(this.BrowsFileBtn);
            this.groupBox1.Location = new System.Drawing.Point(12, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 180);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Restore Database Backup Path";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Brows file to Restor your Database";
            // 
            // RestoreFilePathTxt
            // 
            this.RestoreFilePathTxt.AcceptsReturn = false;
            this.RestoreFilePathTxt.AcceptsTab = false;
            this.RestoreFilePathTxt.AnimationSpeed = 200;
            this.RestoreFilePathTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.RestoreFilePathTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.RestoreFilePathTxt.BackColor = System.Drawing.Color.Transparent;
            this.RestoreFilePathTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RestoreFilePathTxt.BackgroundImage")));
            this.RestoreFilePathTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.RestoreFilePathTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.RestoreFilePathTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.RestoreFilePathTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.RestoreFilePathTxt.BorderRadius = 1;
            this.RestoreFilePathTxt.BorderThickness = 1;
            this.RestoreFilePathTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.RestoreFilePathTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.RestoreFilePathTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.RestoreFilePathTxt.DefaultText = "";
            this.RestoreFilePathTxt.FillColor = System.Drawing.Color.White;
            this.RestoreFilePathTxt.HideSelection = true;
            this.RestoreFilePathTxt.IconLeft = null;
            this.RestoreFilePathTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.RestoreFilePathTxt.IconPadding = 10;
            this.RestoreFilePathTxt.IconRight = null;
            this.RestoreFilePathTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.RestoreFilePathTxt.Lines = new string[0];
            this.RestoreFilePathTxt.Location = new System.Drawing.Point(26, 65);
            this.RestoreFilePathTxt.MaxLength = 32767;
            this.RestoreFilePathTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.RestoreFilePathTxt.Modified = false;
            this.RestoreFilePathTxt.Multiline = false;
            this.RestoreFilePathTxt.Name = "RestoreFilePathTxt";
            stateProperties21.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties21.FillColor = System.Drawing.Color.Empty;
            stateProperties21.ForeColor = System.Drawing.Color.Empty;
            stateProperties21.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.RestoreFilePathTxt.OnActiveState = stateProperties21;
            stateProperties22.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties22.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties22.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.RestoreFilePathTxt.OnDisabledState = stateProperties22;
            stateProperties23.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties23.FillColor = System.Drawing.Color.Empty;
            stateProperties23.ForeColor = System.Drawing.Color.Empty;
            stateProperties23.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.RestoreFilePathTxt.OnHoverState = stateProperties23;
            stateProperties24.BorderColor = System.Drawing.Color.Silver;
            stateProperties24.FillColor = System.Drawing.Color.White;
            stateProperties24.ForeColor = System.Drawing.Color.Empty;
            stateProperties24.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.RestoreFilePathTxt.OnIdleState = stateProperties24;
            this.RestoreFilePathTxt.Padding = new System.Windows.Forms.Padding(3);
            this.RestoreFilePathTxt.PasswordChar = '\0';
            this.RestoreFilePathTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.RestoreFilePathTxt.PlaceholderText = "Enter text";
            this.RestoreFilePathTxt.ReadOnly = false;
            this.RestoreFilePathTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.RestoreFilePathTxt.SelectedText = "";
            this.RestoreFilePathTxt.SelectionLength = 0;
            this.RestoreFilePathTxt.SelectionStart = 0;
            this.RestoreFilePathTxt.ShortcutsEnabled = true;
            this.RestoreFilePathTxt.Size = new System.Drawing.Size(728, 41);
            this.RestoreFilePathTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.RestoreFilePathTxt.TabIndex = 4;
            this.RestoreFilePathTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.RestoreFilePathTxt.TextMarginBottom = 0;
            this.RestoreFilePathTxt.TextMarginLeft = 3;
            this.RestoreFilePathTxt.TextMarginTop = 0;
            this.RestoreFilePathTxt.TextPlaceholder = "Enter text";
            this.RestoreFilePathTxt.UseSystemPasswordChar = false;
            this.RestoreFilePathTxt.WordWrap = true;
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
            // RestoreDbForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Alert;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 340);
            this.ControlBox = false;
            this.Controls.Add(this.BackScreenBtn);
            this.Controls.Add(this.RestoreDatabaseBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "RestoreDbForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restore Database";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BackScreenBtn;
        private System.Windows.Forms.Button RestoreDatabaseBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuTextBox RestoreFilePathTxt;
        private System.Windows.Forms.Button BrowsFileBtn;
    }
}