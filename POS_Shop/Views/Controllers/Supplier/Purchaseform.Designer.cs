namespace POS_Shop.Views.Controllers.Supplier
{
    partial class PurchaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.lblHeaderDate = new System.Windows.Forms.Label();
            this.pnlTopInfo = new System.Windows.Forms.Panel();
            this.lblSupplierCaption = new System.Windows.Forms.Label();
            this.txtSupplierSearch = new System.Windows.Forms.TextBox();
            this.pnlSupplierBadge = new System.Windows.Forms.Panel();
            this.lblSelectedSupplier = new System.Windows.Forms.Label();
            this.btnClearSupplier = new System.Windows.Forms.Button();
            this.lblRefCaption = new System.Windows.Forms.Label();
            this.txtReferenceNo = new System.Windows.Forms.TextBox();
            this.lblDateCaption = new System.Windows.Forms.Label();
            this.dtpPurchaseDate = new System.Windows.Forms.DateTimePicker();
            this.pnlAddProduct = new System.Windows.Forms.Panel();
            this.lblProductCaption = new System.Windows.Forms.Label();
            this.txtProductSearch = new System.Windows.Forms.TextBox();
            this.lblUnitCaption = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.lblQtyCaption = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblPriceCaption = new System.Windows.Forms.Label();
            this.txtItemPrice = new System.Windows.Forms.TextBox();
            this.lblItemTotalCaption = new System.Windows.Forms.Label();
            this.txtItemTotal = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblNotesCaption = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblSubtotalCaption = new System.Windows.Forms.Label();
            this.lblSubtotalVal = new System.Windows.Forms.Label();
            this.lblDiscountCaption = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblNetCaption = new System.Windows.Forms.Label();
            this.lblNetVal = new System.Windows.Forms.Label();
            this.lblStatusInfoCaption = new System.Windows.Forms.Label();
            this.lblStatusInfo = new System.Windows.Forms.Label();
            this.pnlActionBar = new System.Windows.Forms.Panel();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstSupplierSugg = new System.Windows.Forms.ListBox();
            this.lstProductSugg = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlTopInfo.SuspendLayout();
            this.pnlSupplierBadge.SuspendLayout();
            this.pnlAddProduct.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.pnlActionBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblInvoiceNo);
            this.pnlHeader.Controls.Add(this.lblHeaderDate);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1182, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(189, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Purchase Entry";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            this.lblInvoiceNo.Location = new System.Drawing.Point(270, 21);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(96, 23);
            this.lblInvoiceNo.TabIndex = 0;
            this.lblInvoiceNo.Text = "INV-00001";
            // 
            // lblHeaderDate
            // 
            this.lblHeaderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderDate.AutoSize = true;
            this.lblHeaderDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHeaderDate.ForeColor = System.Drawing.Color.White;
            this.lblHeaderDate.Location = new System.Drawing.Point(1942, 21);
            this.lblHeaderDate.Name = "lblHeaderDate";
            this.lblHeaderDate.Size = new System.Drawing.Size(0, 23);
            this.lblHeaderDate.TabIndex = 2;
            // 
            // pnlTopInfo
            // 
            this.pnlTopInfo.BackColor = System.Drawing.Color.White;
            this.pnlTopInfo.Controls.Add(this.lblSupplierCaption);
            this.pnlTopInfo.Controls.Add(this.txtSupplierSearch);
            this.pnlTopInfo.Controls.Add(this.pnlSupplierBadge);
            this.pnlTopInfo.Controls.Add(this.lblRefCaption);
            this.pnlTopInfo.Controls.Add(this.txtReferenceNo);
            this.pnlTopInfo.Controls.Add(this.lblDateCaption);
            this.pnlTopInfo.Controls.Add(this.dtpPurchaseDate);
            this.pnlTopInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopInfo.Location = new System.Drawing.Point(0, 60);
            this.pnlTopInfo.Name = "pnlTopInfo";
            this.pnlTopInfo.Size = new System.Drawing.Size(1182, 106);
            this.pnlTopInfo.TabIndex = 2;
            // 
            // lblSupplierCaption
            // 
            this.lblSupplierCaption.AutoSize = true;
            this.lblSupplierCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSupplierCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblSupplierCaption.Location = new System.Drawing.Point(14, 12);
            this.lblSupplierCaption.Name = "lblSupplierCaption";
            this.lblSupplierCaption.Size = new System.Drawing.Size(72, 19);
            this.lblSupplierCaption.TabIndex = 0;
            this.lblSupplierCaption.Text = "SUPPLIER";
            // 
            // txtSupplierSearch
            // 
            this.txtSupplierSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupplierSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSupplierSearch.Location = new System.Drawing.Point(14, 30);
            this.txtSupplierSearch.Name = "txtSupplierSearch";
            this.txtSupplierSearch.Size = new System.Drawing.Size(360, 30);
            this.txtSupplierSearch.TabIndex = 1;
            this.txtSupplierSearch.TextChanged += new System.EventHandler(this.TxtSupplierSearch_TextChanged);
            this.txtSupplierSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSupplierSearch_KeyDown);
            this.txtSupplierSearch.Leave += new System.EventHandler(this.TxtSupplierSearch_Leave);
            // 
            // pnlSupplierBadge
            // 
            this.pnlSupplierBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.pnlSupplierBadge.Controls.Add(this.lblSelectedSupplier);
            this.pnlSupplierBadge.Controls.Add(this.btnClearSupplier);
            this.pnlSupplierBadge.Location = new System.Drawing.Point(14, 66);
            this.pnlSupplierBadge.Name = "pnlSupplierBadge";
            this.pnlSupplierBadge.Size = new System.Drawing.Size(360, 28);
            this.pnlSupplierBadge.TabIndex = 1;
            this.pnlSupplierBadge.Visible = false;
            // 
            // lblSelectedSupplier
            // 
            this.lblSelectedSupplier.AutoEllipsis = true;
            this.lblSelectedSupplier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblSelectedSupplier.Location = new System.Drawing.Point(4, 5);
            this.lblSelectedSupplier.Name = "lblSelectedSupplier";
            this.lblSelectedSupplier.Size = new System.Drawing.Size(310, 18);
            this.lblSelectedSupplier.TabIndex = 0;
            // 
            // btnClearSupplier
            // 
            this.btnClearSupplier.BackColor = System.Drawing.Color.Transparent;
            this.btnClearSupplier.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearSupplier.FlatAppearance.BorderSize = 0;
            this.btnClearSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearSupplier.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnClearSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnClearSupplier.Location = new System.Drawing.Point(330, 2);
            this.btnClearSupplier.Name = "btnClearSupplier";
            this.btnClearSupplier.Size = new System.Drawing.Size(26, 24);
            this.btnClearSupplier.TabIndex = 1;
            this.btnClearSupplier.Text = "X";
            this.btnClearSupplier.UseVisualStyleBackColor = false;
            this.btnClearSupplier.Click += new System.EventHandler(this.BtnClearSupplier_Click);
            // 
            // lblRefCaption
            // 
            this.lblRefCaption.AutoSize = true;
            this.lblRefCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblRefCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblRefCaption.Location = new System.Drawing.Point(400, 12);
            this.lblRefCaption.Name = "lblRefCaption";
            this.lblRefCaption.Size = new System.Drawing.Size(149, 19);
            this.lblRefCaption.TabIndex = 0;
            this.lblRefCaption.Text = "REFERENCE / BILL NO";
            // 
            // txtReferenceNo
            // 
            this.txtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferenceNo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtReferenceNo.Location = new System.Drawing.Point(400, 30);
            this.txtReferenceNo.Name = "txtReferenceNo";
            this.txtReferenceNo.Size = new System.Drawing.Size(280, 30);
            this.txtReferenceNo.TabIndex = 2;
            // 
            // lblDateCaption
            // 
            this.lblDateCaption.AutoSize = true;
            this.lblDateCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDateCaption.Location = new System.Drawing.Point(710, 12);
            this.lblDateCaption.Name = "lblDateCaption";
            this.lblDateCaption.Size = new System.Drawing.Size(120, 19);
            this.lblDateCaption.TabIndex = 0;
            this.lblDateCaption.Text = "PURCHASE DATE";
            // 
            // dtpPurchaseDate
            // 
            this.dtpPurchaseDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPurchaseDate.Location = new System.Drawing.Point(710, 30);
            this.dtpPurchaseDate.Name = "dtpPurchaseDate";
            this.dtpPurchaseDate.Size = new System.Drawing.Size(180, 30);
            this.dtpPurchaseDate.TabIndex = 3;
            this.dtpPurchaseDate.Value = new System.DateTime(2026, 2, 19, 6, 26, 35, 208);
            // 
            // pnlAddProduct
            // 
            this.pnlAddProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(253)))), ((int)(((byte)(255)))));
            this.pnlAddProduct.Controls.Add(this.lblProductCaption);
            this.pnlAddProduct.Controls.Add(this.txtProductSearch);
            this.pnlAddProduct.Controls.Add(this.lblUnitCaption);
            this.pnlAddProduct.Controls.Add(this.cmbUnit);
            this.pnlAddProduct.Controls.Add(this.lblQtyCaption);
            this.pnlAddProduct.Controls.Add(this.txtQty);
            this.pnlAddProduct.Controls.Add(this.lblPriceCaption);
            this.pnlAddProduct.Controls.Add(this.txtItemPrice);
            this.pnlAddProduct.Controls.Add(this.lblItemTotalCaption);
            this.pnlAddProduct.Controls.Add(this.txtItemTotal);
            this.pnlAddProduct.Controls.Add(this.btnAddItem);
            this.pnlAddProduct.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAddProduct.Location = new System.Drawing.Point(0, 166);
            this.pnlAddProduct.Name = "pnlAddProduct";
            this.pnlAddProduct.Size = new System.Drawing.Size(1182, 86);
            this.pnlAddProduct.TabIndex = 0;
            // 
            // lblProductCaption
            // 
            this.lblProductCaption.AutoSize = true;
            this.lblProductCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblProductCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblProductCaption.Location = new System.Drawing.Point(14, 12);
            this.lblProductCaption.Name = "lblProductCaption";
            this.lblProductCaption.Size = new System.Drawing.Size(75, 19);
            this.lblProductCaption.TabIndex = 0;
            this.lblProductCaption.Text = "PRODUCT";
            // 
            // txtProductSearch
            // 
            this.txtProductSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProductSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtProductSearch.Location = new System.Drawing.Point(14, 36);
            this.txtProductSearch.Name = "txtProductSearch";
            this.txtProductSearch.Size = new System.Drawing.Size(310, 30);
            this.txtProductSearch.TabIndex = 4;
            this.txtProductSearch.TextChanged += new System.EventHandler(this.TxtProductSearch_TextChanged);
            this.txtProductSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtProductSearch_KeyDown);
            this.txtProductSearch.Leave += new System.EventHandler(this.TxtProductSearch_Leave);
            // 
            // lblUnitCaption
            // 
            this.lblUnitCaption.AutoSize = true;
            this.lblUnitCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblUnitCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblUnitCaption.Location = new System.Drawing.Point(342, 8);
            this.lblUnitCaption.Name = "lblUnitCaption";
            this.lblUnitCaption.Size = new System.Drawing.Size(42, 19);
            this.lblUnitCaption.TabIndex = 0;
            this.lblUnitCaption.Text = "UNIT";
            // 
            // cmbUnit
            // 
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUnit.Location = new System.Drawing.Point(342, 36);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(120, 31);
            this.cmbUnit.TabIndex = 5;
            this.cmbUnit.SelectedIndexChanged += new System.EventHandler(this.cmbUnit_SelectedIndexChanged);
            // 
            // lblQtyCaption
            // 
            this.lblQtyCaption.AutoSize = true;
            this.lblQtyCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblQtyCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblQtyCaption.Location = new System.Drawing.Point(480, 8);
            this.lblQtyCaption.Name = "lblQtyCaption";
            this.lblQtyCaption.Size = new System.Drawing.Size(36, 19);
            this.lblQtyCaption.TabIndex = 0;
            this.lblQtyCaption.Text = "QTY";
            // 
            // txtQty
            // 
            this.txtQty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQty.Location = new System.Drawing.Point(480, 36);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(90, 30);
            this.txtQty.TabIndex = 6;
            this.txtQty.Text = "1";
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtQty.TextChanged += new System.EventHandler(this.TxtCalc_TextChanged);
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericOnly);
            // 
            // lblPriceCaption
            // 
            this.lblPriceCaption.AutoSize = true;
            this.lblPriceCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblPriceCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblPriceCaption.Location = new System.Drawing.Point(586, 8);
            this.lblPriceCaption.Name = "lblPriceCaption";
            this.lblPriceCaption.Size = new System.Drawing.Size(124, 19);
            this.lblPriceCaption.TabIndex = 0;
            this.lblPriceCaption.Text = "PURCHASE PRICE";
            // 
            // txtItemPrice
            // 
            this.txtItemPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemPrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemPrice.Location = new System.Drawing.Point(590, 39);
            this.txtItemPrice.Name = "txtItemPrice";
            this.txtItemPrice.Size = new System.Drawing.Size(120, 30);
            this.txtItemPrice.TabIndex = 7;
            this.txtItemPrice.Text = "0.00";
            this.txtItemPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtItemPrice.TextChanged += new System.EventHandler(this.TxtCalc_TextChanged);
            this.txtItemPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // lblItemTotalCaption
            // 
            this.lblItemTotalCaption.AutoSize = true;
            this.lblItemTotalCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblItemTotalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblItemTotalCaption.Location = new System.Drawing.Point(724, 8);
            this.lblItemTotalCaption.Name = "lblItemTotalCaption";
            this.lblItemTotalCaption.Size = new System.Drawing.Size(86, 19);
            this.lblItemTotalCaption.TabIndex = 0;
            this.lblItemTotalCaption.Text = "ITEM TOTAL";
            // 
            // txtItemTotal
            // 
            this.txtItemTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.txtItemTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItemTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.txtItemTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.txtItemTotal.Location = new System.Drawing.Point(724, 36);
            this.txtItemTotal.Name = "txtItemTotal";
            this.txtItemTotal.ReadOnly = true;
            this.txtItemTotal.Size = new System.Drawing.Size(120, 30);
            this.txtItemTotal.TabIndex = 8;
            this.txtItemTotal.TabStop = false;
            this.txtItemTotal.Text = "0.00";
            this.txtItemTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.SlateBlue;
            this.btnAddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddItem.FlatAppearance.BorderSize = 0;
            this.btnAddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAddItem.ForeColor = System.Drawing.Color.White;
            this.btnAddItem.Location = new System.Drawing.Point(862, 33);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(120, 36);
            this.btnAddItem.TabIndex = 9;
            this.btnAddItem.Text = "+ Add Item";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.BtnAddItem_Click);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvItems);
            this.pnlGrid.Controls.Add(this.lblItemCount);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 252);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.pnlGrid.Size = new System.Drawing.Size(1182, 235);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvItems.ColumnHeadersHeight = 38;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSrNo,
            this.colProductCode,
            this.colProductName,
            this.colUnit,
            this.colQty,
            this.colPrice,
            this.colTotal,
            this.colDelete});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvItems.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.dgvItems.Location = new System.Drawing.Point(14, 57);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.RowTemplate.Height = 36;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(1154, 178);
            this.dgvItems.TabIndex = 0;
            this.dgvItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvItems_CellClick);
            this.dgvItems.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvItems_CellEndEdit);
            this.dgvItems.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgvItems_EditingControlShowing);
            // 
            // colSrNo
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSrNo.DefaultCellStyle = dataGridViewCellStyle12;
            this.colSrNo.FillWeight = 5F;
            this.colSrNo.HeaderText = "Sr#";
            this.colSrNo.MinimumWidth = 6;
            this.colSrNo.Name = "colSrNo";
            this.colSrNo.ReadOnly = true;
            // 
            // colProductCode
            // 
            this.colProductCode.FillWeight = 11F;
            this.colProductCode.HeaderText = "Code";
            this.colProductCode.MinimumWidth = 6;
            this.colProductCode.Name = "colProductCode";
            this.colProductCode.ReadOnly = true;
            // 
            // colProductName
            // 
            this.colProductName.FillWeight = 30F;
            this.colProductName.HeaderText = "Product Name";
            this.colProductName.MinimumWidth = 6;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colUnit
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colUnit.DefaultCellStyle = dataGridViewCellStyle13;
            this.colUnit.FillWeight = 8F;
            this.colUnit.HeaderText = "Unit";
            this.colUnit.MinimumWidth = 6;
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            // 
            // colQty
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colQty.DefaultCellStyle = dataGridViewCellStyle14;
            this.colQty.FillWeight = 8F;
            this.colQty.HeaderText = "Qty";
            this.colQty.MinimumWidth = 6;
            this.colQty.Name = "colQty";
            // 
            // colPrice
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "N2";
            this.colPrice.DefaultCellStyle = dataGridViewCellStyle15;
            this.colPrice.FillWeight = 14F;
            this.colPrice.HeaderText = "Purchase Price";
            this.colPrice.MinimumWidth = 6;
            this.colPrice.Name = "colPrice";
            // 
            // colTotal
            // 
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle16.Format = "N2";
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle16;
            this.colTotal.FillWeight = 14F;
            this.colTotal.HeaderText = "Total";
            this.colTotal.MinimumWidth = 6;
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // colDelete
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.colDelete.DefaultCellStyle = dataGridViewCellStyle17;
            this.colDelete.FillWeight = 6F;
            this.colDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colDelete.HeaderText = "";
            this.colDelete.MinimumWidth = 6;
            this.colDelete.Name = "colDelete";
            this.colDelete.Text = "X";
            this.colDelete.UseColumnTextForButtonValue = true;
            // 
            // lblItemCount
            // 
            this.lblItemCount.BackColor = System.Drawing.Color.SlateBlue;
            this.lblItemCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblItemCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblItemCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            this.lblItemCount.Location = new System.Drawing.Point(14, 34);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Padding = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.lblItemCount.Size = new System.Drawing.Size(1154, 23);
            this.lblItemCount.TabIndex = 9;
            this.lblItemCount.Text = "0 items";
            this.lblItemCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.BackColor = System.Drawing.Color.SlateBlue;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.White;
            this.lblGridTitle.Location = new System.Drawing.Point(14, 0);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(1154, 34);
            this.lblGridTitle.TabIndex = 10;
            this.lblGridTitle.Text = "  Purchase Items";
            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.lblNotesCaption);
            this.pnlFooter.Controls.Add(this.txtNotes);
            this.pnlFooter.Controls.Add(this.pnlTotals);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 543);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlFooter.Size = new System.Drawing.Size(1182, 118);
            this.pnlFooter.TabIndex = 5;
            // 
            // lblNotesCaption
            // 
            this.lblNotesCaption.AutoSize = true;
            this.lblNotesCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblNotesCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblNotesCaption.Location = new System.Drawing.Point(14, 10);
            this.lblNotesCaption.Name = "lblNotesCaption";
            this.lblNotesCaption.Size = new System.Drawing.Size(53, 19);
            this.lblNotesCaption.TabIndex = 0;
            this.lblNotesCaption.Text = "NOTES";
            // 
            // txtNotes
            // 
            this.txtNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNotes.Location = new System.Drawing.Point(12, 10);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotes.Size = new System.Drawing.Size(460, 90);
            this.txtNotes.TabIndex = 0;
            // 
            // pnlTotals
            // 
            this.pnlTotals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTotals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlTotals.Controls.Add(this.lblSubtotalCaption);
            this.pnlTotals.Controls.Add(this.lblSubtotalVal);
            this.pnlTotals.Controls.Add(this.lblDiscountCaption);
            this.pnlTotals.Controls.Add(this.txtDiscount);
            this.pnlTotals.Controls.Add(this.lblSeparator);
            this.pnlTotals.Controls.Add(this.lblNetCaption);
            this.pnlTotals.Controls.Add(this.lblNetVal);
            this.pnlTotals.Controls.Add(this.lblStatusInfoCaption);
            this.pnlTotals.Controls.Add(this.lblStatusInfo);
            this.pnlTotals.Location = new System.Drawing.Point(1472, 4);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(680, 104);
            this.pnlTotals.TabIndex = 11;
            // 
            // lblSubtotalCaption
            // 
            this.lblSubtotalCaption.AutoSize = true;
            this.lblSubtotalCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtotalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblSubtotalCaption.Location = new System.Drawing.Point(10, 6);
            this.lblSubtotalCaption.Name = "lblSubtotalCaption";
            this.lblSubtotalCaption.Size = new System.Drawing.Size(78, 23);
            this.lblSubtotalCaption.TabIndex = 0;
            this.lblSubtotalCaption.Text = "Subtotal:";
            // 
            // lblSubtotalVal
            // 
            this.lblSubtotalVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtotalVal.Location = new System.Drawing.Point(500, 6);
            this.lblSubtotalVal.Name = "lblSubtotalVal";
            this.lblSubtotalVal.Size = new System.Drawing.Size(170, 20);
            this.lblSubtotalVal.TabIndex = 1;
            this.lblSubtotalVal.Text = "0.00";
            this.lblSubtotalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountCaption
            // 
            this.lblDiscountCaption.AutoSize = true;
            this.lblDiscountCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscountCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDiscountCaption.Location = new System.Drawing.Point(10, 30);
            this.lblDiscountCaption.Name = "lblDiscountCaption";
            this.lblDiscountCaption.Size = new System.Drawing.Size(81, 23);
            this.lblDiscountCaption.TabIndex = 2;
            this.lblDiscountCaption.Text = "Discount:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiscount.Location = new System.Drawing.Point(500, 28);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(170, 27);
            this.txtDiscount.TabIndex = 11;
            this.txtDiscount.Text = "0.00";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDiscount.TextChanged += new System.EventHandler(this.TxtDiscount_TextChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // lblSeparator
            // 
            this.lblSeparator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.lblSeparator.Location = new System.Drawing.Point(10, 54);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(660, 1);
            this.lblSeparator.TabIndex = 12;
            // 
            // lblNetCaption
            // 
            this.lblNetCaption.AutoSize = true;
            this.lblNetCaption.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNetCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblNetCaption.Location = new System.Drawing.Point(10, 58);
            this.lblNetCaption.Name = "lblNetCaption";
            this.lblNetCaption.Size = new System.Drawing.Size(146, 30);
            this.lblNetCaption.TabIndex = 13;
            this.lblNetCaption.Text = "Net Amount:";
            // 
            // lblNetVal
            // 
            this.lblNetVal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNetVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblNetVal.Location = new System.Drawing.Point(500, 58);
            this.lblNetVal.Name = "lblNetVal";
            this.lblNetVal.Size = new System.Drawing.Size(170, 26);
            this.lblNetVal.TabIndex = 14;
            this.lblNetVal.Text = "0.00";
            this.lblNetVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusInfoCaption
            // 
            this.lblStatusInfoCaption.AutoSize = true;
            this.lblStatusInfoCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatusInfoCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblStatusInfoCaption.Location = new System.Drawing.Point(10, 88);
            this.lblStatusInfoCaption.Name = "lblStatusInfoCaption";
            this.lblStatusInfoCaption.Size = new System.Drawing.Size(131, 23);
            this.lblStatusInfoCaption.TabIndex = 15;
            this.lblStatusInfoCaption.Text = "Payment Status:";
            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.lblStatusInfo.Location = new System.Drawing.Point(300, 88);
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.Size = new System.Drawing.Size(370, 20);
            this.lblStatusInfo.TabIndex = 16;
            this.lblStatusInfo.Text = "PENDING - Pay later via Supplier Payment";
            this.lblStatusInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlActionBar
            // 
            this.pnlActionBar.BackColor = System.Drawing.Color.White;
            this.pnlActionBar.Controls.Add(this.btnClearAll);
            this.pnlActionBar.Controls.Add(this.btnSave);
            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionBar.Location = new System.Drawing.Point(0, 487);
            this.pnlActionBar.Name = "pnlActionBar";
            this.pnlActionBar.Padding = new System.Windows.Forms.Padding(0, 9, 16, 9);
            this.pnlActionBar.Size = new System.Drawing.Size(1182, 56);
            this.pnlActionBar.TabIndex = 0;
            // 
            // btnClearAll
            // 
            this.btnClearAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnClearAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearAll.FlatAppearance.BorderSize = 0;
            this.btnClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClearAll.ForeColor = System.Drawing.Color.White;
            this.btnClearAll.Location = new System.Drawing.Point(237, 9);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(188, 38);
            this.btnClearAll.TabIndex = 0;
            this.btnClearAll.Text = "Clear All (Ctrl+N)";
            this.btnClearAll.UseVisualStyleBackColor = false;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(12, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(219, 38);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save Purchase(Ctrl+S)";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lstSupplierSugg
            // 
            this.lstSupplierSugg.BackColor = System.Drawing.Color.White;
            this.lstSupplierSugg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSupplierSugg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSupplierSugg.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstSupplierSugg.ItemHeight = 44;
            this.lstSupplierSugg.Location = new System.Drawing.Point(0, 0);
            this.lstSupplierSugg.Name = "lstSupplierSugg";
            this.lstSupplierSugg.Size = new System.Drawing.Size(380, 2);
            this.lstSupplierSugg.TabIndex = 6;
            this.lstSupplierSugg.TabStop = false;
            this.lstSupplierSugg.Visible = false;
            this.lstSupplierSugg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LstSupplierSugg_MouseClick);
            this.lstSupplierSugg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstSugg_DrawItem);
            this.lstSupplierSugg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstSupplierSugg_KeyDown);
            // 
            // lstProductSugg
            // 
            this.lstProductSugg.BackColor = System.Drawing.Color.White;
            this.lstProductSugg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstProductSugg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstProductSugg.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstProductSugg.ItemHeight = 44;
            this.lstProductSugg.Location = new System.Drawing.Point(0, 0);
            this.lstProductSugg.Name = "lstProductSugg";
            this.lstProductSugg.Size = new System.Drawing.Size(380, 2);
            this.lstProductSugg.TabIndex = 7;
            this.lstProductSugg.TabStop = false;
            this.lstProductSugg.Visible = false;
            this.lstProductSugg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LstProductSugg_MouseClick);
            this.lstProductSugg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstSugg_DrawItem);
            this.lstProductSugg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstProductSugg_KeyDown);
            // 
            // PurchaseForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1182, 661);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlAddProduct);
            this.Controls.Add(this.pnlTopInfo);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlActionBar);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.lstSupplierSugg);
            this.Controls.Add(this.lstProductSugg);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "PurchaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Entry";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTopInfo.ResumeLayout(false);
            this.pnlTopInfo.PerformLayout();
            this.pnlSupplierBadge.ResumeLayout(false);
            this.pnlAddProduct.ResumeLayout(false);
            this.pnlAddProduct.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotals.PerformLayout();
            this.pnlActionBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader, pnlTopInfo, pnlAddProduct;
        private System.Windows.Forms.Panel pnlGrid, pnlFooter, pnlActionBar, pnlTotals;
        private System.Windows.Forms.Panel pnlSupplierBadge;
        private System.Windows.Forms.Label lblTitle, lblInvoiceNo, lblHeaderDate;
        private System.Windows.Forms.Label lblSupplierCaption;
        private System.Windows.Forms.TextBox txtSupplierSearch;
        private System.Windows.Forms.Label lblSelectedSupplier;
        private System.Windows.Forms.Button btnClearSupplier;
        private System.Windows.Forms.Label lblRefCaption;
        private System.Windows.Forms.TextBox txtReferenceNo;
        private System.Windows.Forms.Label lblDateCaption;
        private System.Windows.Forms.DateTimePicker dtpPurchaseDate;
        private System.Windows.Forms.Label lblProductCaption;
        private System.Windows.Forms.TextBox txtProductSearch;
        private System.Windows.Forms.Label lblUnitCaption;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label lblQtyCaption;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblPriceCaption;
        private System.Windows.Forms.TextBox txtItemPrice;
        private System.Windows.Forms.Label lblItemTotalCaption;
        private System.Windows.Forms.TextBox txtItemTotal;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Label lblGridTitle, lblItemCount;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSrNo, colProductCode, colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit, colQty, colPrice, colTotal;
        private System.Windows.Forms.DataGridViewButtonColumn colDelete;
        private System.Windows.Forms.Label lblSubtotalCaption, lblSubtotalVal;
        private System.Windows.Forms.Label lblDiscountCaption;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Label lblNetCaption, lblNetVal;
        private System.Windows.Forms.Label lblStatusInfoCaption, lblStatusInfo;
        private System.Windows.Forms.Label lblNotesCaption;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnSave, btnClearAll;
        private System.Windows.Forms.ListBox lstSupplierSugg, lstProductSugg;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}