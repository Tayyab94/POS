namespace POS_Shop.Views.BillScreen
{
    partial class SearchCustomerUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchCustomerUI));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.FormCloseLbl = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.NextPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.PreviousPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.SearchCustomerTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.CustomerListGroup = new System.Windows.Forms.GroupBox();
            this.CustomerListDataGrid = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.AddNewCustomerLink = new System.Windows.Forms.LinkLabel();
            this.CustomerIdLbl = new System.Windows.Forms.Label();
            this.CustomerName = new System.Windows.Forms.Label();
            this.CustomerListGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerListDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // FormCloseLbl
            // 
            this.FormCloseLbl.AutoSize = true;
            this.FormCloseLbl.Location = new System.Drawing.Point(931, 21);
            this.FormCloseLbl.Name = "FormCloseLbl";
            this.FormCloseLbl.Size = new System.Drawing.Size(76, 16);
            this.FormCloseLbl.TabIndex = 0;
            this.FormCloseLbl.Text = "Form Close";
            this.FormCloseLbl.Visible = false;
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.Red;
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.CloseBtn.Location = new System.Drawing.Point(1055, 6);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(76, 38);
            this.CloseBtn.TabIndex = 0;
            this.CloseBtn.Text = "X";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // NextPageBtn
            // 
            this.NextPageBtn.ActiveImage = null;
            this.NextPageBtn.AllowAnimations = true;
            this.NextPageBtn.AllowBuffering = false;
            this.NextPageBtn.AllowToggling = false;
            this.NextPageBtn.AllowZooming = false;
            this.NextPageBtn.AllowZoomingOnFocus = false;
            this.NextPageBtn.BackColor = System.Drawing.Color.Transparent;
            this.NextPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.NextPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.ErrorImage")));
            this.NextPageBtn.FadeWhenInactive = false;
            this.NextPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.NextPageBtn.Image = global::POS_Shop.Properties.Resources.iconNext;
            this.NextPageBtn.ImageActive = null;
            this.NextPageBtn.ImageLocation = null;
            this.NextPageBtn.ImageMargin = 2;
            this.NextPageBtn.ImageSize = new System.Drawing.Size(49, 39);
            this.NextPageBtn.ImageZoomSize = new System.Drawing.Size(51, 41);
            this.NextPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.InitialImage")));
            this.NextPageBtn.Location = new System.Drawing.Point(1075, 94);
            this.NextPageBtn.Name = "NextPageBtn";
            this.NextPageBtn.Rotation = 0;
            this.NextPageBtn.ShowActiveImage = true;
            this.NextPageBtn.ShowCursorChanges = true;
            this.NextPageBtn.ShowImageBorders = true;
            this.NextPageBtn.ShowSizeMarkers = false;
            this.NextPageBtn.Size = new System.Drawing.Size(51, 41);
            this.NextPageBtn.TabIndex = 0;
            this.NextPageBtn.ToolTipText = "";
            this.NextPageBtn.WaitOnLoad = false;
            this.NextPageBtn.Zoom = 2;
            this.NextPageBtn.ZoomSpeed = 10;
            this.NextPageBtn.Click += new System.EventHandler(this.NextPageBtn_Click);
            // 
            // PreviousPageBtn
            // 
            this.PreviousPageBtn.ActiveImage = null;
            this.PreviousPageBtn.AllowAnimations = true;
            this.PreviousPageBtn.AllowBuffering = false;
            this.PreviousPageBtn.AllowToggling = false;
            this.PreviousPageBtn.AllowZooming = false;
            this.PreviousPageBtn.AllowZoomingOnFocus = false;
            this.PreviousPageBtn.BackColor = System.Drawing.Color.Transparent;
            this.PreviousPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.PreviousPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.ErrorImage")));
            this.PreviousPageBtn.FadeWhenInactive = false;
            this.PreviousPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.PreviousPageBtn.Image = global::POS_Shop.Properties.Resources.iconPrev;
            this.PreviousPageBtn.ImageActive = null;
            this.PreviousPageBtn.ImageLocation = null;
            this.PreviousPageBtn.ImageMargin = 2;
            this.PreviousPageBtn.ImageSize = new System.Drawing.Size(41, 38);
            this.PreviousPageBtn.ImageZoomSize = new System.Drawing.Size(43, 40);
            this.PreviousPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.InitialImage")));
            this.PreviousPageBtn.Location = new System.Drawing.Point(1023, 95);
            this.PreviousPageBtn.Name = "PreviousPageBtn";
            this.PreviousPageBtn.Rotation = 0;
            this.PreviousPageBtn.ShowActiveImage = true;
            this.PreviousPageBtn.ShowCursorChanges = true;
            this.PreviousPageBtn.ShowImageBorders = true;
            this.PreviousPageBtn.ShowSizeMarkers = false;
            this.PreviousPageBtn.Size = new System.Drawing.Size(43, 40);
            this.PreviousPageBtn.TabIndex = 0;
            this.PreviousPageBtn.ToolTipText = "";
            this.PreviousPageBtn.WaitOnLoad = false;
            this.PreviousPageBtn.Zoom = 2;
            this.PreviousPageBtn.ZoomSpeed = 10;
            this.PreviousPageBtn.Click += new System.EventHandler(this.PreviousPageBtn_Click);
            // 
            // SearchCustomerTxt
            // 
            this.SearchCustomerTxt.AcceptsReturn = false;
            this.SearchCustomerTxt.AcceptsTab = false;
            this.SearchCustomerTxt.AnimationSpeed = 200;
            this.SearchCustomerTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.SearchCustomerTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.SearchCustomerTxt.BackColor = System.Drawing.Color.Transparent;
            this.SearchCustomerTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchCustomerTxt.BackgroundImage")));
            this.SearchCustomerTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.SearchCustomerTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SearchCustomerTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SearchCustomerTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.SearchCustomerTxt.BorderRadius = 1;
            this.SearchCustomerTxt.BorderThickness = 1;
            this.SearchCustomerTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.SearchCustomerTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchCustomerTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.SearchCustomerTxt.DefaultText = "";
            this.SearchCustomerTxt.FillColor = System.Drawing.Color.White;
            this.SearchCustomerTxt.HideSelection = true;
            this.SearchCustomerTxt.IconLeft = null;
            this.SearchCustomerTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchCustomerTxt.IconPadding = 10;
            this.SearchCustomerTxt.IconRight = null;
            this.SearchCustomerTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchCustomerTxt.Lines = new string[0];
            this.SearchCustomerTxt.Location = new System.Drawing.Point(359, 79);
            this.SearchCustomerTxt.MaxLength = 32767;
            this.SearchCustomerTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.SearchCustomerTxt.Modified = false;
            this.SearchCustomerTxt.Multiline = false;
            this.SearchCustomerTxt.Name = "SearchCustomerTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchCustomerTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.SearchCustomerTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchCustomerTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchCustomerTxt.OnIdleState = stateProperties4;
            this.SearchCustomerTxt.Padding = new System.Windows.Forms.Padding(3);
            this.SearchCustomerTxt.PasswordChar = '\0';
            this.SearchCustomerTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.SearchCustomerTxt.PlaceholderText = "Search Customer";
            this.SearchCustomerTxt.ReadOnly = false;
            this.SearchCustomerTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SearchCustomerTxt.SelectedText = "";
            this.SearchCustomerTxt.SelectionLength = 0;
            this.SearchCustomerTxt.SelectionStart = 0;
            this.SearchCustomerTxt.ShortcutsEnabled = true;
            this.SearchCustomerTxt.Size = new System.Drawing.Size(382, 39);
            this.SearchCustomerTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.SearchCustomerTxt.TabIndex = 1;
            this.SearchCustomerTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SearchCustomerTxt.TextMarginBottom = 0;
            this.SearchCustomerTxt.TextMarginLeft = 3;
            this.SearchCustomerTxt.TextMarginTop = 0;
            this.SearchCustomerTxt.TextPlaceholder = "Search Customer";
            this.SearchCustomerTxt.UseSystemPasswordChar = false;
            this.SearchCustomerTxt.WordWrap = true;
            this.SearchCustomerTxt.TextChange += new System.EventHandler(this.SearchCustomerTxt_TextChange);
            this.SearchCustomerTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchCustomerTxt_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SlateBlue;
            this.label1.Location = new System.Drawing.Point(363, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(378, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Customer";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(825, -3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // CustomerListGroup
            // 
            this.CustomerListGroup.Controls.Add(this.CustomerListDataGrid);
            this.CustomerListGroup.Controls.Add(this.lblStatus);
            this.CustomerListGroup.Location = new System.Drawing.Point(1, 141);
            this.CustomerListGroup.Name = "CustomerListGroup";
            this.CustomerListGroup.Size = new System.Drawing.Size(1138, 414);
            this.CustomerListGroup.TabIndex = 33;
            this.CustomerListGroup.TabStop = false;
            this.CustomerListGroup.Text = "Customer List";
            // 
            // CustomerListDataGrid
            // 
            this.CustomerListDataGrid.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.CustomerListDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CustomerListDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CustomerListDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CustomerListDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.CustomerListDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CustomerListDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CustomerListDataGrid.ColumnHeadersHeight = 40;
            this.CustomerListDataGrid.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.CustomerListDataGrid.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.CustomerListDataGrid.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.CustomerListDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.CustomerListDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.CustomerListDataGrid.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.CustomerListDataGrid.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.CustomerListDataGrid.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.CustomerListDataGrid.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.CustomerListDataGrid.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.CustomerListDataGrid.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.CustomerListDataGrid.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.CustomerListDataGrid.CurrentTheme.Name = null;
            this.CustomerListDataGrid.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.CustomerListDataGrid.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.CustomerListDataGrid.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.CustomerListDataGrid.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.CustomerListDataGrid.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CustomerListDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.CustomerListDataGrid.EnableHeadersVisualStyles = false;
            this.CustomerListDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.CustomerListDataGrid.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.CustomerListDataGrid.HeaderBgColor = System.Drawing.Color.Empty;
            this.CustomerListDataGrid.HeaderForeColor = System.Drawing.Color.White;
            this.CustomerListDataGrid.Location = new System.Drawing.Point(11, 21);
            this.CustomerListDataGrid.Name = "CustomerListDataGrid";
            this.CustomerListDataGrid.RowHeadersVisible = false;
            this.CustomerListDataGrid.RowHeadersWidth = 51;
            this.CustomerListDataGrid.RowTemplate.Height = 40;
            this.CustomerListDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CustomerListDataGrid.Size = new System.Drawing.Size(1126, 387);
            this.CustomerListDataGrid.TabIndex = 2;
            this.CustomerListDataGrid.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.CustomerListDataGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.CustomerListDataGrid_CellMouseClick);
            this.CustomerListDataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CustomerListDataGrid_KeyDown);
            // 
            // AddNewCustomerLink
            // 
            this.AddNewCustomerLink.AutoSize = true;
            this.AddNewCustomerLink.Location = new System.Drawing.Point(757, 98);
            this.AddNewCustomerLink.Name = "AddNewCustomerLink";
            this.AddNewCustomerLink.Size = new System.Drawing.Size(122, 16);
            this.AddNewCustomerLink.TabIndex = 0;
            this.AddNewCustomerLink.TabStop = true;
            this.AddNewCustomerLink.Text = "Add New Customer";
            this.AddNewCustomerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AddNewCustomerLink_LinkClicked);
            // 
            // CustomerIdLbl
            // 
            this.CustomerIdLbl.AutoSize = true;
            this.CustomerIdLbl.Location = new System.Drawing.Point(278, 60);
            this.CustomerIdLbl.Name = "CustomerIdLbl";
            this.CustomerIdLbl.Size = new System.Drawing.Size(78, 16);
            this.CustomerIdLbl.TabIndex = 0;
            this.CustomerIdLbl.Text = "Customer Id";
            this.CustomerIdLbl.Visible = false;
            // 
            // CustomerName
            // 
            this.CustomerName.AutoSize = true;
            this.CustomerName.Location = new System.Drawing.Point(275, 94);
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Size = new System.Drawing.Size(53, 16);
            this.CustomerName.TabIndex = 0;
            this.CustomerName.Text = "CName";
            this.CustomerName.Visible = false;
            // 
            // SearchCustomerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1151, 558);
            this.Controls.Add(this.CustomerName);
            this.Controls.Add(this.CustomerIdLbl);
            this.Controls.Add(this.AddNewCustomerLink);
            this.Controls.Add(this.FormCloseLbl);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.NextPageBtn);
            this.Controls.Add(this.PreviousPageBtn);
            this.Controls.Add(this.SearchCustomerTxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CustomerListGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchCustomerUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchCustomerUI";
            this.CustomerListGroup.ResumeLayout(false);
            this.CustomerListGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomerListDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label FormCloseLbl;
        private System.Windows.Forms.Button CloseBtn;
        private Bunifu.UI.WinForms.BunifuImageButton NextPageBtn;
        private Bunifu.UI.WinForms.BunifuImageButton PreviousPageBtn;
        private Bunifu.UI.WinForms.BunifuTextBox SearchCustomerTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.GroupBox CustomerListGroup;
        private Bunifu.UI.WinForms.BunifuDataGridView CustomerListDataGrid;
        private System.Windows.Forms.LinkLabel AddNewCustomerLink;
        public System.Windows.Forms.Label CustomerIdLbl;
        public System.Windows.Forms.Label CustomerName;
    }
}