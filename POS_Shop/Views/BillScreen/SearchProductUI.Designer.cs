namespace POS_Shop.Views.BillScreen
{
    partial class SearchProductUI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchProductUI));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.ProductListGrid = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ProdicListGroup = new System.Windows.Forms.GroupBox();
            this.ProdIdLbl = new System.Windows.Forms.Label();
            this.PNameLbl = new System.Windows.Forms.Label();
            this.PUNameLbl = new System.Windows.Forms.Label();
            this.PTypeLbl = new System.Windows.Forms.Label();
            this.ProdSalePriceLbl = new System.Windows.Forms.Label();
            this.FormCloseLbl = new System.Windows.Forms.Label();
            this.NextPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.PreviousPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.SearchProductTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProductListGrid)).BeginInit();
            this.ProdicListGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SlateBlue;
            this.label1.Location = new System.Drawing.Point(386, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(344, 55);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Product";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(859, -3);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.Red;
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.CloseBtn.Location = new System.Drawing.Point(1101, 8);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(76, 38);
            this.CloseBtn.TabIndex = 0;
            this.CloseBtn.Text = "X";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // ProductListGrid
            // 
            this.ProductListGrid.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.ProductListGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductListGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductListGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductListGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductListGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductListGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductListGrid.ColumnHeadersHeight = 40;
            this.ProductListGrid.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.ProductListGrid.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.ProductListGrid.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.ProductListGrid.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.ProductListGrid.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.ProductListGrid.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.ProductListGrid.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.ProductListGrid.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.ProductListGrid.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.ProductListGrid.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.ProductListGrid.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.ProductListGrid.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.ProductListGrid.CurrentTheme.Name = null;
            this.ProductListGrid.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.ProductListGrid.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.ProductListGrid.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.ProductListGrid.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.ProductListGrid.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductListGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductListGrid.EnableHeadersVisualStyles = false;
            this.ProductListGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.ProductListGrid.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.ProductListGrid.HeaderBgColor = System.Drawing.Color.Empty;
            this.ProductListGrid.HeaderForeColor = System.Drawing.Color.White;
            this.ProductListGrid.Location = new System.Drawing.Point(12, 31);
            this.ProductListGrid.Name = "ProductListGrid";
            this.ProductListGrid.RowHeadersVisible = false;
            this.ProductListGrid.RowHeadersWidth = 51;
            this.ProductListGrid.RowTemplate.Height = 40;
            this.ProductListGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductListGrid.Size = new System.Drawing.Size(1153, 392);
            this.ProductListGrid.TabIndex = 2;
            this.ProductListGrid.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.ProductListGrid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ProductListGrid_CellMouseClick);
            this.ProductListGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductListGrid_KeyDown);
            // 
            // ProdicListGroup
            // 
            this.ProdicListGroup.Controls.Add(this.ProductListGrid);
            this.ProdicListGroup.Controls.Add(this.lblStatus);
            this.ProdicListGroup.Location = new System.Drawing.Point(12, 159);
            this.ProdicListGroup.Name = "ProdicListGroup";
            this.ProdicListGroup.Size = new System.Drawing.Size(1172, 434);
            this.ProdicListGroup.TabIndex = 0;
            this.ProdicListGroup.TabStop = false;
            this.ProdicListGroup.Text = "Product List";
            // 
            // ProdIdLbl
            // 
            this.ProdIdLbl.AutoSize = true;
            this.ProdIdLbl.Location = new System.Drawing.Point(51, 26);
            this.ProdIdLbl.Name = "ProdIdLbl";
            this.ProdIdLbl.Size = new System.Drawing.Size(50, 16);
            this.ProdIdLbl.TabIndex = 0;
            this.ProdIdLbl.Text = "Prod Id";
            this.ProdIdLbl.Visible = false;
            // 
            // PNameLbl
            // 
            this.PNameLbl.AutoSize = true;
            this.PNameLbl.Location = new System.Drawing.Point(51, 62);
            this.PNameLbl.Name = "PNameLbl";
            this.PNameLbl.Size = new System.Drawing.Size(79, 16);
            this.PNameLbl.TabIndex = 0;
            this.PNameLbl.Text = "Prodt Name";
            this.PNameLbl.Visible = false;
            // 
            // PUNameLbl
            // 
            this.PUNameLbl.AutoSize = true;
            this.PUNameLbl.Location = new System.Drawing.Point(53, 104);
            this.PUNameLbl.Name = "PUNameLbl";
            this.PUNameLbl.Size = new System.Drawing.Size(108, 16);
            this.PUNameLbl.TabIndex = 0;
            this.PUNameLbl.Text = "Prod Urdu Name";
            this.PUNameLbl.Visible = false;
            // 
            // PTypeLbl
            // 
            this.PTypeLbl.AutoSize = true;
            this.PTypeLbl.Location = new System.Drawing.Point(149, 26);
            this.PTypeLbl.Name = "PTypeLbl";
            this.PTypeLbl.Size = new System.Drawing.Size(71, 16);
            this.PTypeLbl.TabIndex = 0;
            this.PTypeLbl.Text = "Prod Type";
            this.PTypeLbl.Visible = false;
            // 
            // ProdSalePriceLbl
            // 
            this.ProdSalePriceLbl.AutoSize = true;
            this.ProdSalePriceLbl.Location = new System.Drawing.Point(149, 74);
            this.ProdSalePriceLbl.Name = "ProdSalePriceLbl";
            this.ProdSalePriceLbl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ProdSalePriceLbl.Size = new System.Drawing.Size(101, 16);
            this.ProdSalePriceLbl.TabIndex = 0;
            this.ProdSalePriceLbl.Text = "Prod Sale Price";
            this.ProdSalePriceLbl.Visible = false;
            // 
            // FormCloseLbl
            // 
            this.FormCloseLbl.AutoSize = true;
            this.FormCloseLbl.Location = new System.Drawing.Point(977, 23);
            this.FormCloseLbl.Name = "FormCloseLbl";
            this.FormCloseLbl.Size = new System.Drawing.Size(76, 16);
            this.FormCloseLbl.TabIndex = 0;
            this.FormCloseLbl.Text = "Form Close";
            this.FormCloseLbl.Visible = false;
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
            this.NextPageBtn.Location = new System.Drawing.Point(1098, 115);
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
            this.PreviousPageBtn.Location = new System.Drawing.Point(1046, 116);
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
            // SearchProductTxt
            // 
            this.SearchProductTxt.AcceptsReturn = false;
            this.SearchProductTxt.AcceptsTab = false;
            this.SearchProductTxt.AnimationSpeed = 200;
            this.SearchProductTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.SearchProductTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.SearchProductTxt.BackColor = System.Drawing.Color.Transparent;
            this.SearchProductTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchProductTxt.BackgroundImage")));
            this.SearchProductTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.SearchProductTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SearchProductTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SearchProductTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.SearchProductTxt.BorderRadius = 1;
            this.SearchProductTxt.BorderThickness = 1;
            this.SearchProductTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.SearchProductTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchProductTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.SearchProductTxt.DefaultText = "";
            this.SearchProductTxt.FillColor = System.Drawing.Color.White;
            this.SearchProductTxt.HideSelection = true;
            this.SearchProductTxt.IconLeft = null;
            this.SearchProductTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchProductTxt.IconPadding = 10;
            this.SearchProductTxt.IconRight = null;
            this.SearchProductTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchProductTxt.Lines = new string[0];
            this.SearchProductTxt.Location = new System.Drawing.Point(382, 100);
            this.SearchProductTxt.MaxLength = 32767;
            this.SearchProductTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.SearchProductTxt.Modified = false;
            this.SearchProductTxt.Multiline = false;
            this.SearchProductTxt.Name = "SearchProductTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchProductTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.SearchProductTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchProductTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchProductTxt.OnIdleState = stateProperties4;
            this.SearchProductTxt.Padding = new System.Windows.Forms.Padding(3);
            this.SearchProductTxt.PasswordChar = '\0';
            this.SearchProductTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.SearchProductTxt.PlaceholderText = "Search Product";
            this.SearchProductTxt.ReadOnly = false;
            this.SearchProductTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SearchProductTxt.SelectedText = "";
            this.SearchProductTxt.SelectionLength = 0;
            this.SearchProductTxt.SelectionStart = 0;
            this.SearchProductTxt.ShortcutsEnabled = true;
            this.SearchProductTxt.Size = new System.Drawing.Size(362, 39);
            this.SearchProductTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.SearchProductTxt.TabIndex = 1;
            this.SearchProductTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SearchProductTxt.TextMarginBottom = 0;
            this.SearchProductTxt.TextMarginLeft = 3;
            this.SearchProductTxt.TextMarginTop = 0;
            this.SearchProductTxt.TextPlaceholder = "Search Product";
            this.SearchProductTxt.UseSystemPasswordChar = false;
            this.SearchProductTxt.WordWrap = true;
            this.SearchProductTxt.TextChange += new System.EventHandler(this.SearchProductTxt_TextChanged);
            this.SearchProductTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchProductTxt_KeyDown);
            // 
            // SearchProductUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1201, 605);
            this.Controls.Add(this.FormCloseLbl);
            this.Controls.Add(this.ProdSalePriceLbl);
            this.Controls.Add(this.PTypeLbl);
            this.Controls.Add(this.PUNameLbl);
            this.Controls.Add(this.PNameLbl);
            this.Controls.Add(this.ProdIdLbl);
            this.Controls.Add(this.ProdicListGroup);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.NextPageBtn);
            this.Controls.Add(this.PreviousPageBtn);
            this.Controls.Add(this.SearchProductTxt);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SearchProductUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SearchProductUI";
            ((System.ComponentModel.ISupportInitialize)(this.ProductListGrid)).EndInit();
            this.ProdicListGroup.ResumeLayout(false);
            this.ProdicListGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuTextBox SearchProductTxt;
        private Bunifu.UI.WinForms.BunifuImageButton PreviousPageBtn;
        private Bunifu.UI.WinForms.BunifuImageButton NextPageBtn;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button CloseBtn;
        private Bunifu.UI.WinForms.BunifuDataGridView ProductListGrid;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox ProdicListGroup;
        public System.Windows.Forms.Label ProdIdLbl;
        public System.Windows.Forms.Label PNameLbl;
        public System.Windows.Forms.Label PUNameLbl;
        public System.Windows.Forms.Label PTypeLbl;
        public System.Windows.Forms.Label ProdSalePriceLbl;
        public System.Windows.Forms.Label FormCloseLbl;
    }
}