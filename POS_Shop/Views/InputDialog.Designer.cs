namespace POS_Shop.Views
{
    partial class InputDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDialog));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.LabelMessage = new System.Windows.Forms.Label();
            this.InputTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.OkBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelMessage
            // 
            this.LabelMessage.AutoSize = true;
            this.LabelMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelMessage.Location = new System.Drawing.Point(205, 35);
            this.LabelMessage.Name = "LabelMessage";
            this.LabelMessage.Size = new System.Drawing.Size(224, 25);
            this.LabelMessage.TabIndex = 0;
            this.LabelMessage.Text = "Enter Customer Name";
            // 
            // InputTxt
            // 
            this.InputTxt.AcceptsReturn = false;
            this.InputTxt.AcceptsTab = false;
            this.InputTxt.AnimationSpeed = 200;
            this.InputTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.InputTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.InputTxt.BackColor = System.Drawing.Color.Transparent;
            this.InputTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("InputTxt.BackgroundImage")));
            this.InputTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.InputTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.InputTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.InputTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.InputTxt.BorderRadius = 1;
            this.InputTxt.BorderThickness = 1;
            this.InputTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.InputTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.InputTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.InputTxt.DefaultText = "";
            this.InputTxt.FillColor = System.Drawing.Color.White;
            this.InputTxt.HideSelection = true;
            this.InputTxt.IconLeft = null;
            this.InputTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.InputTxt.IconPadding = 10;
            this.InputTxt.IconRight = null;
            this.InputTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.InputTxt.Lines = new string[0];
            this.InputTxt.Location = new System.Drawing.Point(89, 75);
            this.InputTxt.MaxLength = 32767;
            this.InputTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.InputTxt.Modified = false;
            this.InputTxt.Multiline = false;
            this.InputTxt.Name = "InputTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.InputTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.InputTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.InputTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.InputTxt.OnIdleState = stateProperties4;
            this.InputTxt.Padding = new System.Windows.Forms.Padding(3);
            this.InputTxt.PasswordChar = '\0';
            this.InputTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.InputTxt.PlaceholderText = "Enter text";
            this.InputTxt.ReadOnly = false;
            this.InputTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.InputTxt.SelectedText = "";
            this.InputTxt.SelectionLength = 0;
            this.InputTxt.SelectionStart = 0;
            this.InputTxt.ShortcutsEnabled = true;
            this.InputTxt.Size = new System.Drawing.Size(466, 47);
            this.InputTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.InputTxt.TabIndex = 1;
            this.InputTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.InputTxt.TextMarginBottom = 0;
            this.InputTxt.TextMarginLeft = 3;
            this.InputTxt.TextMarginTop = 0;
            this.InputTxt.TextPlaceholder = "Enter text";
            this.InputTxt.UseSystemPasswordChar = false;
            this.InputTxt.WordWrap = true;
            this.InputTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.InputTxt_KeyPress);
            // 
            // CancelBtn
            // 
            this.CancelBtn.BackColor = System.Drawing.Color.Red;
            this.CancelBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.CancelBtn.Location = new System.Drawing.Point(450, 142);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(105, 41);
            this.CancelBtn.TabIndex = 3;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = false;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // OkBtn
            // 
            this.OkBtn.BackColor = System.Drawing.Color.SlateBlue;
            this.OkBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.OkBtn.Location = new System.Drawing.Point(340, 142);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(104, 41);
            this.OkBtn.TabIndex = 2;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = false;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // InputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(629, 226);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.InputTxt);
            this.Controls.Add(this.LabelMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InputDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InputDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelMessage;
        private Bunifu.UI.WinForms.BunifuTextBox InputTxt;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button OkBtn;
    }
}