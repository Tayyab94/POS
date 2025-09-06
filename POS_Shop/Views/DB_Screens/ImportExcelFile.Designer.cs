namespace POS_Shop.Views.DB_Screens
{
    partial class ImportExcelFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportExcelFile));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.ImportExcelFiuleGroup = new System.Windows.Forms.GroupBox();
            this.loadDataBtn = new System.Windows.Forms.Button();
            this.BrowsFileBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.peoductGirdGroup = new System.Windows.Forms.GroupBox();
            this.ProductDataGrid = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.ImportFileTabComtrol = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ImportFilePathTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.updatePriceGroup = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.bunifuTextBox1 = new Bunifu.UI.WinForms.BunifuTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ImportToDbBtn = new System.Windows.Forms.Button();
            this.ImportExcelFiuleGroup.SuspendLayout();
            this.peoductGirdGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductDataGrid)).BeginInit();
            this.ImportFileTabComtrol.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.updatePriceGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImportExcelFiuleGroup
            // 
            this.ImportExcelFiuleGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportExcelFiuleGroup.Controls.Add(this.ImportToDbBtn);
            this.ImportExcelFiuleGroup.Controls.Add(this.loadDataBtn);
            this.ImportExcelFiuleGroup.Controls.Add(this.ImportFilePathTxt);
            this.ImportExcelFiuleGroup.Controls.Add(this.BrowsFileBtn);
            this.ImportExcelFiuleGroup.Controls.Add(this.label1);
            this.ImportExcelFiuleGroup.Location = new System.Drawing.Point(7, 18);
            this.ImportExcelFiuleGroup.Name = "ImportExcelFiuleGroup";
            this.ImportExcelFiuleGroup.Size = new System.Drawing.Size(1270, 111);
            this.ImportExcelFiuleGroup.TabIndex = 0;
            this.ImportExcelFiuleGroup.TabStop = false;
            this.ImportExcelFiuleGroup.Text = "Import Products";
            // 
            // loadDataBtn
            // 
            this.loadDataBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadDataBtn.BackColor = System.Drawing.Color.BurlyWood;
            this.loadDataBtn.Enabled = false;
            this.loadDataBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadDataBtn.Location = new System.Drawing.Point(884, 49);
            this.loadDataBtn.Name = "loadDataBtn";
            this.loadDataBtn.Size = new System.Drawing.Size(124, 37);
            this.loadDataBtn.TabIndex = 9;
            this.loadDataBtn.Text = "Load Data";
            this.loadDataBtn.UseVisualStyleBackColor = false;
            this.loadDataBtn.Click += new System.EventHandler(this.loadDataBtn_Click);
            // 
            // BrowsFileBtn
            // 
            this.BrowsFileBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowsFileBtn.BackColor = System.Drawing.Color.SlateBlue;
            this.BrowsFileBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrowsFileBtn.ForeColor = System.Drawing.SystemColors.Control;
            this.BrowsFileBtn.Location = new System.Drawing.Point(754, 50);
            this.BrowsFileBtn.Name = "BrowsFileBtn";
            this.BrowsFileBtn.Size = new System.Drawing.Size(124, 37);
            this.BrowsFileBtn.TabIndex = 7;
            this.BrowsFileBtn.Text = "Brows File";
            this.BrowsFileBtn.UseVisualStyleBackColor = false;
            this.BrowsFileBtn.Click += new System.EventHandler(this.BrowsFileBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Brows file to Load the data into Grid";
            // 
            // peoductGirdGroup
            // 
            this.peoductGirdGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.peoductGirdGroup.Controls.Add(this.ProductDataGrid);
            this.peoductGirdGroup.Location = new System.Drawing.Point(7, 144);
            this.peoductGirdGroup.Name = "peoductGirdGroup";
            this.peoductGirdGroup.Size = new System.Drawing.Size(1270, 492);
            this.peoductGirdGroup.TabIndex = 1;
            this.peoductGirdGroup.TabStop = false;
            this.peoductGirdGroup.Text = "Product List";
            // 
            // ProductDataGrid
            // 
            this.ProductDataGrid.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.ProductDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductDataGrid.ColumnHeadersHeight = 40;
            this.ProductDataGrid.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.ProductDataGrid.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.ProductDataGrid.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.ProductDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.ProductDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.ProductDataGrid.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.ProductDataGrid.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.ProductDataGrid.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.ProductDataGrid.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.ProductDataGrid.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.ProductDataGrid.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.ProductDataGrid.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.ProductDataGrid.CurrentTheme.Name = null;
            this.ProductDataGrid.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.ProductDataGrid.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.ProductDataGrid.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.ProductDataGrid.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.ProductDataGrid.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductDataGrid.EnableHeadersVisualStyles = false;
            this.ProductDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.ProductDataGrid.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.ProductDataGrid.HeaderBgColor = System.Drawing.Color.Empty;
            this.ProductDataGrid.HeaderForeColor = System.Drawing.Color.White;
            this.ProductDataGrid.Location = new System.Drawing.Point(6, 32);
            this.ProductDataGrid.Name = "ProductDataGrid";
            this.ProductDataGrid.RowHeadersVisible = false;
            this.ProductDataGrid.RowHeadersWidth = 51;
            this.ProductDataGrid.RowTemplate.Height = 40;
            this.ProductDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductDataGrid.Size = new System.Drawing.Size(1258, 450);
            this.ProductDataGrid.TabIndex = 0;
            this.ProductDataGrid.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // ImportFileTabComtrol
            // 
            this.ImportFileTabComtrol.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportFileTabComtrol.Controls.Add(this.tabPage1);
            this.ImportFileTabComtrol.Controls.Add(this.tabPage2);
            this.ImportFileTabComtrol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportFileTabComtrol.Location = new System.Drawing.Point(12, 12);
            this.ImportFileTabComtrol.Name = "ImportFileTabComtrol";
            this.ImportFileTabComtrol.SelectedIndex = 0;
            this.ImportFileTabComtrol.Size = new System.Drawing.Size(1298, 673);
            this.ImportFileTabComtrol.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ImportExcelFiuleGroup);
            this.tabPage1.Controls.Add(this.peoductGirdGroup);
            this.tabPage1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1290, 642);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Import Excel Initial";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.updatePriceGroup);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1290, 642);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Import Updated Price Excel";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ImportFilePathTxt
            // 
            this.ImportFilePathTxt.AcceptsReturn = false;
            this.ImportFilePathTxt.AcceptsTab = false;
            this.ImportFilePathTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportFilePathTxt.AnimationSpeed = 200;
            this.ImportFilePathTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.ImportFilePathTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.ImportFilePathTxt.BackColor = System.Drawing.Color.Transparent;
            this.ImportFilePathTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ImportFilePathTxt.BackgroundImage")));
            this.ImportFilePathTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.ImportFilePathTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ImportFilePathTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.ImportFilePathTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.ImportFilePathTxt.BorderRadius = 1;
            this.ImportFilePathTxt.BorderThickness = 1;
            this.ImportFilePathTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.ImportFilePathTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ImportFilePathTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.ImportFilePathTxt.DefaultText = "";
            this.ImportFilePathTxt.FillColor = System.Drawing.Color.White;
            this.ImportFilePathTxt.HideSelection = true;
            this.ImportFilePathTxt.IconLeft = null;
            this.ImportFilePathTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.ImportFilePathTxt.IconPadding = 10;
            this.ImportFilePathTxt.IconRight = null;
            this.ImportFilePathTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.ImportFilePathTxt.Lines = new string[0];
            this.ImportFilePathTxt.Location = new System.Drawing.Point(18, 50);
            this.ImportFilePathTxt.MaxLength = 32767;
            this.ImportFilePathTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.ImportFilePathTxt.Modified = false;
            this.ImportFilePathTxt.Multiline = false;
            this.ImportFilePathTxt.Name = "ImportFilePathTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportFilePathTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.ImportFilePathTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportFilePathTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ImportFilePathTxt.OnIdleState = stateProperties4;
            this.ImportFilePathTxt.Padding = new System.Windows.Forms.Padding(3);
            this.ImportFilePathTxt.PasswordChar = '\0';
            this.ImportFilePathTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.ImportFilePathTxt.PlaceholderText = "Enter text";
            this.ImportFilePathTxt.ReadOnly = false;
            this.ImportFilePathTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ImportFilePathTxt.SelectedText = "";
            this.ImportFilePathTxt.SelectionLength = 0;
            this.ImportFilePathTxt.SelectionStart = 0;
            this.ImportFilePathTxt.ShortcutsEnabled = true;
            this.ImportFilePathTxt.Size = new System.Drawing.Size(711, 41);
            this.ImportFilePathTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.ImportFilePathTxt.TabIndex = 8;
            this.ImportFilePathTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ImportFilePathTxt.TextMarginBottom = 0;
            this.ImportFilePathTxt.TextMarginLeft = 3;
            this.ImportFilePathTxt.TextMarginTop = 0;
            this.ImportFilePathTxt.TextPlaceholder = "Enter text";
            this.ImportFilePathTxt.UseSystemPasswordChar = false;
            this.ImportFilePathTxt.WordWrap = true;
            // 
            // updatePriceGroup
            // 
            this.updatePriceGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updatePriceGroup.Controls.Add(this.button1);
            this.updatePriceGroup.Controls.Add(this.bunifuTextBox1);
            this.updatePriceGroup.Controls.Add(this.button2);
            this.updatePriceGroup.Controls.Add(this.label2);
            this.updatePriceGroup.Location = new System.Drawing.Point(6, 22);
            this.updatePriceGroup.Name = "updatePriceGroup";
            this.updatePriceGroup.Size = new System.Drawing.Size(1270, 111);
            this.updatePriceGroup.TabIndex = 1;
            this.updatePriceGroup.TabStop = false;
            this.updatePriceGroup.Text = "Import Excel";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(884, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 37);
            this.button1.TabIndex = 9;
            this.button1.Text = "Load Data";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // bunifuTextBox1
            // 
            this.bunifuTextBox1.AcceptsReturn = false;
            this.bunifuTextBox1.AcceptsTab = false;
            this.bunifuTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bunifuTextBox1.AnimationSpeed = 200;
            this.bunifuTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.bunifuTextBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.bunifuTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuTextBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuTextBox1.BackgroundImage")));
            this.bunifuTextBox1.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.bunifuTextBox1.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.bunifuTextBox1.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.bunifuTextBox1.BorderColorIdle = System.Drawing.Color.Silver;
            this.bunifuTextBox1.BorderRadius = 1;
            this.bunifuTextBox1.BorderThickness = 1;
            this.bunifuTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.bunifuTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.bunifuTextBox1.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.bunifuTextBox1.DefaultText = "";
            this.bunifuTextBox1.FillColor = System.Drawing.Color.White;
            this.bunifuTextBox1.HideSelection = true;
            this.bunifuTextBox1.IconLeft = null;
            this.bunifuTextBox1.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.bunifuTextBox1.IconPadding = 10;
            this.bunifuTextBox1.IconRight = null;
            this.bunifuTextBox1.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.bunifuTextBox1.Lines = new string[0];
            this.bunifuTextBox1.Location = new System.Drawing.Point(18, 50);
            this.bunifuTextBox1.MaxLength = 32767;
            this.bunifuTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.bunifuTextBox1.Modified = false;
            this.bunifuTextBox1.Multiline = false;
            this.bunifuTextBox1.Name = "bunifuTextBox1";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.bunifuTextBox1.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.bunifuTextBox1.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.bunifuTextBox1.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.White;
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.bunifuTextBox1.OnIdleState = stateProperties8;
            this.bunifuTextBox1.Padding = new System.Windows.Forms.Padding(3);
            this.bunifuTextBox1.PasswordChar = '\0';
            this.bunifuTextBox1.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.bunifuTextBox1.PlaceholderText = "Enter text";
            this.bunifuTextBox1.ReadOnly = false;
            this.bunifuTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.bunifuTextBox1.SelectedText = "";
            this.bunifuTextBox1.SelectionLength = 0;
            this.bunifuTextBox1.SelectionStart = 0;
            this.bunifuTextBox1.ShortcutsEnabled = true;
            this.bunifuTextBox1.Size = new System.Drawing.Size(711, 41);
            this.bunifuTextBox1.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.bunifuTextBox1.TabIndex = 8;
            this.bunifuTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.bunifuTextBox1.TextMarginBottom = 0;
            this.bunifuTextBox1.TextMarginLeft = 3;
            this.bunifuTextBox1.TextMarginTop = 0;
            this.bunifuTextBox1.TextPlaceholder = "Enter text";
            this.bunifuTextBox1.UseSystemPasswordChar = false;
            this.bunifuTextBox1.WordWrap = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(754, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 37);
            this.button2.TabIndex = 7;
            this.button2.Text = "Brows File";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(340, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "Brows Updated Price file to Load the data into Grid";
            // 
            // ImportToDbBtn
            // 
            this.ImportToDbBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportToDbBtn.BackColor = System.Drawing.Color.DarkKhaki;
            this.ImportToDbBtn.Enabled = false;
            this.ImportToDbBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImportToDbBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ImportToDbBtn.Location = new System.Drawing.Point(1012, 49);
            this.ImportToDbBtn.Name = "ImportToDbBtn";
            this.ImportToDbBtn.Size = new System.Drawing.Size(124, 37);
            this.ImportToDbBtn.TabIndex = 10;
            this.ImportToDbBtn.Text = "Import to DB";
            this.ImportToDbBtn.UseVisualStyleBackColor = false;
            this.ImportToDbBtn.Click += new System.EventHandler(this.ImportToDbBtn_Click);
            // 
            // ImportExcelFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1322, 697);
            this.Controls.Add(this.ImportFileTabComtrol);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImportExcelFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import Excel File";
            this.ImportExcelFiuleGroup.ResumeLayout(false);
            this.ImportExcelFiuleGroup.PerformLayout();
            this.peoductGirdGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductDataGrid)).EndInit();
            this.ImportFileTabComtrol.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.updatePriceGroup.ResumeLayout(false);
            this.updatePriceGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ImportExcelFiuleGroup;
        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuTextBox ImportFilePathTxt;
        private System.Windows.Forms.Button BrowsFileBtn;
        private System.Windows.Forms.Button loadDataBtn;
        private System.Windows.Forms.GroupBox peoductGirdGroup;
        private Bunifu.UI.WinForms.BunifuDataGridView ProductDataGrid;
        private System.Windows.Forms.TabControl ImportFileTabComtrol;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox updatePriceGroup;
        private System.Windows.Forms.Button button1;
        private Bunifu.UI.WinForms.BunifuTextBox bunifuTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ImportToDbBtn;
    }
}