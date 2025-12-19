namespace POS_Shop.Views.Settings
{
    partial class PirntersAndScanners
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PrinterScannersDDL = new Bunifu.UI.WinForms.BunifuDropdown();
            this.label1 = new System.Windows.Forms.Label();
            this.SetDefaultPrinterBtn = new System.Windows.Forms.Button();
            this.BackScreenBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.PrinterScannersDDL);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.SetDefaultPrinterBtn);
            this.groupBox1.Location = new System.Drawing.Point(4, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 164);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Printers & Scanners";
            // 
            // PrinterScannersDDL
            // 
            this.PrinterScannersDDL.BackColor = System.Drawing.Color.Transparent;
            this.PrinterScannersDDL.BackgroundColor = System.Drawing.Color.White;
            this.PrinterScannersDDL.BorderColor = System.Drawing.Color.Silver;
            this.PrinterScannersDDL.BorderRadius = 1;
            this.PrinterScannersDDL.Color = System.Drawing.Color.Silver;
            this.PrinterScannersDDL.Direction = Bunifu.UI.WinForms.BunifuDropdown.Directions.Down;
            this.PrinterScannersDDL.DisabledBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.PrinterScannersDDL.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.PrinterScannersDDL.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.PrinterScannersDDL.DisabledForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.PrinterScannersDDL.DisabledIndicatorColor = System.Drawing.Color.DarkGray;
            this.PrinterScannersDDL.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.PrinterScannersDDL.DropdownBorderThickness = Bunifu.UI.WinForms.BunifuDropdown.BorderThickness.Thin;
            this.PrinterScannersDDL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrinterScannersDDL.DropDownTextAlign = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.PrinterScannersDDL.FillDropDown = true;
            this.PrinterScannersDDL.FillIndicator = false;
            this.PrinterScannersDDL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrinterScannersDDL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.PrinterScannersDDL.ForeColor = System.Drawing.Color.Black;
            this.PrinterScannersDDL.FormattingEnabled = true;
            this.PrinterScannersDDL.Icon = null;
            this.PrinterScannersDDL.IndicatorAlignment = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.PrinterScannersDDL.IndicatorColor = System.Drawing.Color.Gray;
            this.PrinterScannersDDL.IndicatorLocation = Bunifu.UI.WinForms.BunifuDropdown.Indicator.Right;
            this.PrinterScannersDDL.ItemBackColor = System.Drawing.Color.White;
            this.PrinterScannersDDL.ItemBorderColor = System.Drawing.Color.White;
            this.PrinterScannersDDL.ItemForeColor = System.Drawing.Color.Black;
            this.PrinterScannersDDL.ItemHeight = 26;
            this.PrinterScannersDDL.ItemHighLightColor = System.Drawing.Color.DodgerBlue;
            this.PrinterScannersDDL.ItemHighLightForeColor = System.Drawing.Color.White;
            this.PrinterScannersDDL.ItemTopMargin = 3;
            this.PrinterScannersDDL.Location = new System.Drawing.Point(12, 56);
            this.PrinterScannersDDL.Name = "PrinterScannersDDL";
            this.PrinterScannersDDL.Size = new System.Drawing.Size(728, 32);
            this.PrinterScannersDDL.TabIndex = 6;
            this.PrinterScannersDDL.Text = null;
            this.PrinterScannersDDL.TextAlignment = Bunifu.UI.WinForms.BunifuDropdown.TextAlign.Left;
            this.PrinterScannersDDL.TextLeftMargin = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(8, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "List of Printers & Scanners";
            // 
            // SetDefaultPrinterBtn
            // 
            this.SetDefaultPrinterBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SetDefaultPrinterBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SetDefaultPrinterBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SetDefaultPrinterBtn.Location = new System.Drawing.Point(617, 117);
            this.SetDefaultPrinterBtn.Name = "SetDefaultPrinterBtn";
            this.SetDefaultPrinterBtn.Size = new System.Drawing.Size(124, 37);
            this.SetDefaultPrinterBtn.TabIndex = 3;
            this.SetDefaultPrinterBtn.Text = "Set Default";
            this.SetDefaultPrinterBtn.UseVisualStyleBackColor = true;
            this.SetDefaultPrinterBtn.Click += new System.EventHandler(this.SetDefaultPrinterBtn_Click);
            // 
            // BackScreenBtn
            // 
            this.BackScreenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BackScreenBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackScreenBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BackScreenBtn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BackScreenBtn.Location = new System.Drawing.Point(645, 212);
            this.BackScreenBtn.Name = "BackScreenBtn";
            this.BackScreenBtn.Size = new System.Drawing.Size(104, 33);
            this.BackScreenBtn.TabIndex = 7;
            this.BackScreenBtn.Text = "Back Screen";
            this.BackScreenBtn.UseVisualStyleBackColor = true;
            // 
            // PirntersAndScanners
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(765, 202);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BackScreenBtn);
            this.Name = "PirntersAndScanners";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Printer & Scanner Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SetDefaultPrinterBtn;
        private System.Windows.Forms.Button BackScreenBtn;
        private Bunifu.UI.WinForms.BunifuDropdown PrinterScannersDDL;
    }
}
