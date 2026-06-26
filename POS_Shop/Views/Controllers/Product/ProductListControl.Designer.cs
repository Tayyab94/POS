namespace POS_Shop.Views.Controllers.Product
{
    partial class ProductListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductListControl));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ClearAllSelectionBtn = new System.Windows.Forms.Button();
            this.SelectAllBtn = new System.Windows.Forms.Button();
            this.ImportFilBtn = new System.Windows.Forms.Button();
            this.selectedProdLbl = new System.Windows.Forms.Label();
            this.ProductListGridGrp = new Bunifu.UI.WinForms.BunifuGroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ProductListGrid = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.AddNewProductFormBtn = new System.Windows.Forms.Button();
            this.btnManagePrices = new System.Windows.Forms.Button();
            this.btnDeleteProduct = new System.Windows.Forms.Button();
            this.ProductFormLbl = new System.Windows.Forms.Label();
            this.PreviousPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.NextPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.ProdSearchTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.ExportProdBtn = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.ProductListGridGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductListGrid)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.ProdSearchTxt);
            this.flowLayoutPanel1.Controls.Add(this.ClearAllSelectionBtn);
            this.flowLayoutPanel1.Controls.Add(this.SelectAllBtn);
            this.flowLayoutPanel1.Controls.Add(this.ExportProdBtn);
            this.flowLayoutPanel1.Controls.Add(this.ImportFilBtn);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(21, 54);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1172, 59);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // ClearAllSelectionBtn
            // 
            this.ClearAllSelectionBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearAllSelectionBtn.BackColor = System.Drawing.Color.Violet;
            this.ClearAllSelectionBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ClearAllSelectionBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearAllSelectionBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClearAllSelectionBtn.Location = new System.Drawing.Point(528, 12);
            this.ClearAllSelectionBtn.Name = "ClearAllSelectionBtn";
            this.ClearAllSelectionBtn.Size = new System.Drawing.Size(103, 42);
            this.ClearAllSelectionBtn.TabIndex = 20;
            this.ClearAllSelectionBtn.Text = "Clear All";
            this.ClearAllSelectionBtn.UseVisualStyleBackColor = false;
            this.ClearAllSelectionBtn.Visible = false;
            this.ClearAllSelectionBtn.Click += new System.EventHandler(this.ClearAllSelectionBtn_Click);
            // 
            // SelectAllBtn
            // 
            this.SelectAllBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectAllBtn.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.SelectAllBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SelectAllBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectAllBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SelectAllBtn.Location = new System.Drawing.Point(637, 12);
            this.SelectAllBtn.Name = "SelectAllBtn";
            this.SelectAllBtn.Size = new System.Drawing.Size(103, 42);
            this.SelectAllBtn.TabIndex = 21;
            this.SelectAllBtn.Text = "Select All";
            this.SelectAllBtn.UseVisualStyleBackColor = false;
            this.SelectAllBtn.Click += new System.EventHandler(this.SelectAllBtn_Click);
            // 
            // ImportFilBtn
            // 
            this.ImportFilBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportFilBtn.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ImportFilBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ImportFilBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFilBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ImportFilBtn.Location = new System.Drawing.Point(866, 12);
            this.ImportFilBtn.Name = "ImportFilBtn";
            this.ImportFilBtn.Size = new System.Drawing.Size(158, 42);
            this.ImportFilBtn.TabIndex = 23;
            this.ImportFilBtn.Text = "Import File Form";
            this.ImportFilBtn.UseVisualStyleBackColor = false;
            this.ImportFilBtn.Click += new System.EventHandler(this.ImportFilBtn_Click);
            // 
            // selectedProdLbl
            // 
            this.selectedProdLbl.AutoSize = true;
            this.selectedProdLbl.Location = new System.Drawing.Point(113, 141);
            this.selectedProdLbl.Name = "selectedProdLbl";
            this.selectedProdLbl.Size = new System.Drawing.Size(45, 16);
            this.selectedProdLbl.TabIndex = 23;
            this.selectedProdLbl.Text = "Select";
            // 
            // ProductListGridGrp
            // 
            this.ProductListGridGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductListGridGrp.BorderColor = System.Drawing.Color.LightGray;
            this.ProductListGridGrp.BorderRadius = 1;
            this.ProductListGridGrp.BorderThickness = 2;
            this.ProductListGridGrp.Controls.Add(this.lblStatus);
            this.ProductListGridGrp.Controls.Add(this.ProductListGrid);
            this.ProductListGridGrp.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ProductListGridGrp.LabelAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ProductListGridGrp.LabelIndent = 10;
            this.ProductListGridGrp.LineStyle = Bunifu.UI.WinForms.BunifuGroupBox.LineStyles.Solid;
            this.ProductListGridGrp.Location = new System.Drawing.Point(21, 160);
            this.ProductListGridGrp.Name = "ProductListGridGrp";
            this.ProductListGridGrp.Size = new System.Drawing.Size(1171, 388);
            this.ProductListGridGrp.TabIndex = 3;
            this.ProductListGridGrp.TabStop = false;
            this.ProductListGridGrp.Text = "Product List";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(849, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(49, 20);
            this.lblStatus.TabIndex = 18;
            this.lblStatus.Text = "Status";
            // 
            // ProductListGrid
            // 
            this.ProductListGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductListGrid.Location = new System.Drawing.Point(13, 23);
            this.ProductListGrid.Name = "ProductListGrid";
            this.ProductListGrid.RowHeadersWidth = 51;
            this.ProductListGrid.RowTemplate.Height = 24;
            this.ProductListGrid.Size = new System.Drawing.Size(1152, 359);
            this.ProductListGrid.TabIndex = 0;
            this.ProductListGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductListGrid_CellClick);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.AddNewProductFormBtn);
            this.flowLayoutPanel2.Controls.Add(this.btnManagePrices);
            this.flowLayoutPanel2.Controls.Add(this.btnDeleteProduct);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(21, 554);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(1163, 58);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // AddNewProductFormBtn
            // 
            this.AddNewProductFormBtn.BackColor = System.Drawing.Color.Maroon;
            this.AddNewProductFormBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddNewProductFormBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewProductFormBtn.ForeColor = System.Drawing.Color.White;
            this.AddNewProductFormBtn.Location = new System.Drawing.Point(4, 4);
            this.AddNewProductFormBtn.Margin = new System.Windows.Forms.Padding(4);
            this.AddNewProductFormBtn.Name = "AddNewProductFormBtn";
            this.AddNewProductFormBtn.Size = new System.Drawing.Size(133, 49);
            this.AddNewProductFormBtn.TabIndex = 11;
            this.AddNewProductFormBtn.Text = "🗑️ Add New";
            this.AddNewProductFormBtn.UseVisualStyleBackColor = true;
            this.AddNewProductFormBtn.Click += new System.EventHandler(this.AddNewProductFormBtn_Click);
            // 
            // btnManagePrices
            // 
            this.btnManagePrices.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnManagePrices.Enabled = false;
            this.btnManagePrices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManagePrices.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagePrices.ForeColor = System.Drawing.Color.White;
            this.btnManagePrices.Location = new System.Drawing.Point(145, 4);
            this.btnManagePrices.Margin = new System.Windows.Forms.Padding(4);
            this.btnManagePrices.Name = "btnManagePrices";
            this.btnManagePrices.Size = new System.Drawing.Size(187, 49);
            this.btnManagePrices.TabIndex = 9;
            this.btnManagePrices.Text = "💰 Manage Prices";
            this.btnManagePrices.UseVisualStyleBackColor = false;
            this.btnManagePrices.Click += new System.EventHandler(this.btnManagePrices_Click);
            // 
            // btnDeleteProduct
            // 
            this.btnDeleteProduct.BackColor = System.Drawing.Color.IndianRed;
            this.btnDeleteProduct.Enabled = false;
            this.btnDeleteProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteProduct.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteProduct.ForeColor = System.Drawing.Color.White;
            this.btnDeleteProduct.Location = new System.Drawing.Point(340, 4);
            this.btnDeleteProduct.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteProduct.Name = "btnDeleteProduct";
            this.btnDeleteProduct.Size = new System.Drawing.Size(133, 49);
            this.btnDeleteProduct.TabIndex = 10;
            this.btnDeleteProduct.Text = "🗑️ Delete";
            this.btnDeleteProduct.UseVisualStyleBackColor = false;
            this.btnDeleteProduct.Click += new System.EventHandler(this.btnDeleteProduct_Click);

            // 
            // ProductFormLbl
            // 
            this.ProductFormLbl.AutoSize = true;
            this.ProductFormLbl.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ProductFormLbl.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductFormLbl.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ProductFormLbl.Location = new System.Drawing.Point(502, 12);
            this.ProductFormLbl.Name = "ProductFormLbl";
            this.ProductFormLbl.Size = new System.Drawing.Size(202, 26);
            this.ProductFormLbl.TabIndex = 21;
            this.ProductFormLbl.Text = "Product List Page";
            // 
            // PreviousPageBtn
            // 
            this.PreviousPageBtn.ActiveImage = null;
            this.PreviousPageBtn.AllowAnimations = true;
            this.PreviousPageBtn.AllowBuffering = false;
            this.PreviousPageBtn.AllowToggling = false;
            this.PreviousPageBtn.AllowZooming = false;
            this.PreviousPageBtn.AllowZoomingOnFocus = false;
            this.PreviousPageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviousPageBtn.BackColor = System.Drawing.Color.Transparent;
            this.PreviousPageBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PreviousPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.PreviousPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.ErrorImage")));
            this.PreviousPageBtn.FadeWhenInactive = false;
            this.PreviousPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.PreviousPageBtn.Image = global::POS_Shop.Properties.Resources.iconPrev;
            this.PreviousPageBtn.ImageActive = null;
            this.PreviousPageBtn.ImageLocation = null;
            this.PreviousPageBtn.ImageMargin = 2;
            this.PreviousPageBtn.ImageSize = new System.Drawing.Size(33, 36);
            this.PreviousPageBtn.ImageZoomSize = new System.Drawing.Size(35, 38);
            this.PreviousPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.InitialImage")));
            this.PreviousPageBtn.Location = new System.Drawing.Point(1105, 119);
            this.PreviousPageBtn.Name = "PreviousPageBtn";
            this.PreviousPageBtn.Rotation = 0;
            this.PreviousPageBtn.ShowActiveImage = true;
            this.PreviousPageBtn.ShowCursorChanges = true;
            this.PreviousPageBtn.ShowImageBorders = true;
            this.PreviousPageBtn.ShowSizeMarkers = false;
            this.PreviousPageBtn.Size = new System.Drawing.Size(35, 38);
            this.PreviousPageBtn.TabIndex = 4;
            this.PreviousPageBtn.ToolTipText = "";
            this.PreviousPageBtn.WaitOnLoad = false;
            this.PreviousPageBtn.Zoom = 2;
            this.PreviousPageBtn.ZoomSpeed = 10;
            this.PreviousPageBtn.Click += new System.EventHandler(this.PreviousPageBtn_Click);
            // 
            // NextPageBtn
            // 
            this.NextPageBtn.ActiveImage = null;
            this.NextPageBtn.AllowAnimations = true;
            this.NextPageBtn.AllowBuffering = false;
            this.NextPageBtn.AllowToggling = false;
            this.NextPageBtn.AllowZooming = false;
            this.NextPageBtn.AllowZoomingOnFocus = false;
            this.NextPageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NextPageBtn.BackColor = System.Drawing.Color.Transparent;
            this.NextPageBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NextPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.NextPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.ErrorImage")));
            this.NextPageBtn.FadeWhenInactive = false;
            this.NextPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.NextPageBtn.Image = global::POS_Shop.Properties.Resources.iconNext;
            this.NextPageBtn.ImageActive = null;
            this.NextPageBtn.ImageLocation = null;
            this.NextPageBtn.ImageMargin = 2;
            this.NextPageBtn.ImageSize = new System.Drawing.Size(33, 36);
            this.NextPageBtn.ImageZoomSize = new System.Drawing.Size(35, 38);
            this.NextPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.InitialImage")));
            this.NextPageBtn.Location = new System.Drawing.Point(1145, 119);
            this.NextPageBtn.Name = "NextPageBtn";
            this.NextPageBtn.Rotation = 0;
            this.NextPageBtn.ShowActiveImage = true;
            this.NextPageBtn.ShowCursorChanges = true;
            this.NextPageBtn.ShowImageBorders = true;
            this.NextPageBtn.ShowSizeMarkers = false;
            this.NextPageBtn.Size = new System.Drawing.Size(35, 38);
            this.NextPageBtn.TabIndex = 5;
            this.NextPageBtn.ToolTipText = "";
            this.NextPageBtn.WaitOnLoad = false;
            this.NextPageBtn.Zoom = 2;
            this.NextPageBtn.ZoomSpeed = 10;
            this.NextPageBtn.Click += new System.EventHandler(this.NextPageBtn_Click);
            // 
            // ProdSearchTxt
            // 
            this.ProdSearchTxt.AcceptsReturn = false;
            this.ProdSearchTxt.AcceptsTab = false;
            this.ProdSearchTxt.AnimationSpeed = 200;
            this.ProdSearchTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.ProdSearchTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.ProdSearchTxt.BackColor = System.Drawing.Color.Transparent;
            this.ProdSearchTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ProdSearchTxt.BackgroundImage")));
            this.ProdSearchTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.ProdSearchTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ProdSearchTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.ProdSearchTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.ProdSearchTxt.BorderRadius = 1;
            this.ProdSearchTxt.BorderThickness = 1;
            this.ProdSearchTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.ProdSearchTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdSearchTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.ProdSearchTxt.DefaultText = "";
            this.ProdSearchTxt.FillColor = System.Drawing.Color.White;
            this.ProdSearchTxt.HideSelection = true;
            this.ProdSearchTxt.IconLeft = null;
            this.ProdSearchTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdSearchTxt.IconPadding = 10;
            this.ProdSearchTxt.IconRight = null;
            this.ProdSearchTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdSearchTxt.Lines = new string[0];
            this.ProdSearchTxt.Location = new System.Drawing.Point(3, 13);
            this.ProdSearchTxt.MaxLength = 32767;
            this.ProdSearchTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.ProdSearchTxt.Modified = false;
            this.ProdSearchTxt.Multiline = false;
            this.ProdSearchTxt.Name = "ProdSearchTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdSearchTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.ProdSearchTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdSearchTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdSearchTxt.OnIdleState = stateProperties4;
            this.ProdSearchTxt.Padding = new System.Windows.Forms.Padding(3);
            this.ProdSearchTxt.PasswordChar = '\0';
            this.ProdSearchTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.ProdSearchTxt.PlaceholderText = "Search...";
            this.ProdSearchTxt.ReadOnly = false;
            this.ProdSearchTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ProdSearchTxt.SelectedText = "";
            this.ProdSearchTxt.SelectionLength = 0;
            this.ProdSearchTxt.SelectionStart = 0;
            this.ProdSearchTxt.ShortcutsEnabled = true;
            this.ProdSearchTxt.Size = new System.Drawing.Size(519, 41);
            this.ProdSearchTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.ProdSearchTxt.TabIndex = 22;
            this.ProdSearchTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ProdSearchTxt.TextMarginBottom = 0;
            this.ProdSearchTxt.TextMarginLeft = 3;
            this.ProdSearchTxt.TextMarginTop = 0;
            this.ProdSearchTxt.TextPlaceholder = "Search...";
            this.ProdSearchTxt.UseSystemPasswordChar = false;
            this.ProdSearchTxt.WordWrap = true;
            this.ProdSearchTxt.TextChange += new System.EventHandler(this.ProdSearchTxt_TextChanged);
            // 
            // ExportProdBtn
            // 
            this.ExportProdBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExportProdBtn.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ExportProdBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExportProdBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportProdBtn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ExportProdBtn.Image = global::POS_Shop.Properties.Resources.iconExcel;
            this.ExportProdBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ExportProdBtn.Location = new System.Drawing.Point(746, 11);
            this.ExportProdBtn.Name = "ExportProdBtn";
            this.ExportProdBtn.Size = new System.Drawing.Size(114, 43);
            this.ExportProdBtn.TabIndex = 19;
            this.ExportProdBtn.Text = "Export";
            this.ExportProdBtn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ExportProdBtn.UseVisualStyleBackColor = false;
            this.ExportProdBtn.Click += new System.EventHandler(this.ExportProdBtn_Click);
            // 
            // ProductListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProductFormLbl);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.PreviousPageBtn);
            this.Controls.Add(this.NextPageBtn);
            this.Controls.Add(this.ProductListGridGrp);
            this.Controls.Add(this.selectedProdLbl);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ProductListControl";
            this.Size = new System.Drawing.Size(1214, 629);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ProductListGridGrp.ResumeLayout(false);
            this.ProductListGridGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductListGrid)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Bunifu.UI.WinForms.BunifuTextBox ProdSearchTxt;
        private System.Windows.Forms.Button ClearAllSelectionBtn;
        private System.Windows.Forms.Button SelectAllBtn;
        private System.Windows.Forms.Button ExportProdBtn;
        private System.Windows.Forms.Label selectedProdLbl;
        private Bunifu.UI.WinForms.BunifuGroupBox ProductListGridGrp;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView ProductListGrid;
        private Bunifu.UI.WinForms.BunifuImageButton PreviousPageBtn;
        private Bunifu.UI.WinForms.BunifuImageButton NextPageBtn;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button AddNewProductFormBtn;
        private System.Windows.Forms.Button btnManagePrices;
        private System.Windows.Forms.Button btnDeleteProduct;
        private System.Windows.Forms.Label ProductFormLbl;
        private System.Windows.Forms.Button ImportFilBtn;
    }
}
