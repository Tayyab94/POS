namespace POS_Shop.Views.Controllers.Supplier
{
    partial class PurchaseDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.pnlStatusBadge = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlMeta = new System.Windows.Forms.Panel();
            this.lblSupCap = new System.Windows.Forms.Label();
            this.lblSupVal = new System.Windows.Forms.Label();
            this.lblDateCap = new System.Windows.Forms.Label();
            this.lblDateVal = new System.Windows.Forms.Label();
            this.lblRefCap = new System.Windows.Forms.Label();
            this.lblRefVal = new System.Windows.Forms.Label();
            this.lblCreatedCap = new System.Windows.Forms.Label();
            this.lblCreatedVal = new System.Windows.Forms.Label();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.pnlPayStatus = new System.Windows.Forms.Panel();
            this.lblPayHeading = new System.Windows.Forms.Label();
            this.lblPaidCap = new System.Windows.Forms.Label();
            this.lblPaidVal = new System.Windows.Forms.Label();
            this.lblBalCap = new System.Windows.Forms.Label();
            this.lblBalVal = new System.Windows.Forms.Label();
            this.lblPayStatCap = new System.Windows.Forms.Label();
            this.lblPayStatVal = new System.Windows.Forms.Label();
            this.pnlTotals = new System.Windows.Forms.Panel();
            this.lblSubCap = new System.Windows.Forms.Label();
            this.lblSubVal = new System.Windows.Forms.Label();
            this.lblDiscCap = new System.Windows.Forms.Label();
            this.lblDiscVal = new System.Windows.Forms.Label();
            this.pnlSep = new System.Windows.Forms.Panel();
            this.lblNetCap = new System.Windows.Forms.Label();
            this.lblNetVal = new System.Windows.Forms.Label();
            this.lblNotesCap = new System.Windows.Forms.Label();
            this.lblNotesVal = new System.Windows.Forms.Label();
            this.pnlActionBar = new System.Windows.Forms.Panel();
            this.BtnPDF = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlStatusBadge.SuspendLayout();
            this.pnlMeta.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.pnlPayStatus.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.pnlActionBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblInvoiceNo);
            this.pnlHeader.Controls.Add(this.pnlStatusBadge);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1002, 64);
            this.pnlHeader.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Purchase Invoice";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
            this.lblInvoiceNo.Location = new System.Drawing.Point(270, 22);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(115, 28);
            this.lblInvoiceNo.TabIndex = 1;
            this.lblInvoiceNo.Text = "INV-00000";
            // 
            // pnlStatusBadge
            // 
            this.pnlStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.pnlStatusBadge.Controls.Add(this.lblStatus);
            this.pnlStatusBadge.Location = new System.Drawing.Point(1646, 16);
            this.pnlStatusBadge.Name = "pnlStatusBadge";
            this.pnlStatusBadge.Size = new System.Drawing.Size(154, 32);
            this.pnlStatusBadge.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(154, 32);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "⏳  PENDING";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMeta
            // 
            this.pnlMeta.BackColor = System.Drawing.Color.White;
            this.pnlMeta.Controls.Add(this.lblSupCap);
            this.pnlMeta.Controls.Add(this.lblSupVal);
            this.pnlMeta.Controls.Add(this.lblDateCap);
            this.pnlMeta.Controls.Add(this.lblDateVal);
            this.pnlMeta.Controls.Add(this.lblRefCap);
            this.pnlMeta.Controls.Add(this.lblRefVal);
            this.pnlMeta.Controls.Add(this.lblCreatedCap);
            this.pnlMeta.Controls.Add(this.lblCreatedVal);
            this.pnlMeta.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMeta.Location = new System.Drawing.Point(0, 64);
            this.pnlMeta.Name = "pnlMeta";
            this.pnlMeta.Size = new System.Drawing.Size(1002, 76);
            this.pnlMeta.TabIndex = 3;
            // 
            // lblSupCap
            // 
            this.lblSupCap.AutoSize = true;
            this.lblSupCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSupCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblSupCap.Location = new System.Drawing.Point(18, 10);
            this.lblSupCap.Name = "lblSupCap";
            this.lblSupCap.Size = new System.Drawing.Size(66, 17);
            this.lblSupCap.TabIndex = 0;
            this.lblSupCap.Text = "SUPPLIER";
            // 
            // lblSupVal
            // 
            this.lblSupVal.AutoSize = true;
            this.lblSupVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSupVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblSupVal.Location = new System.Drawing.Point(18, 27);
            this.lblSupVal.Name = "lblSupVal";
            this.lblSupVal.Size = new System.Drawing.Size(31, 25);
            this.lblSupVal.TabIndex = 1;
            this.lblSupVal.Text = "—";
            // 
            // lblDateCap
            // 
            this.lblDateCap.AutoSize = true;
            this.lblDateCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDateCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDateCap.Location = new System.Drawing.Point(380, 10);
            this.lblDateCap.Name = "lblDateCap";
            this.lblDateCap.Size = new System.Drawing.Size(111, 17);
            this.lblDateCap.TabIndex = 2;
            this.lblDateCap.Text = "PURCHASE DATE";
            // 
            // lblDateVal
            // 
            this.lblDateVal.AutoSize = true;
            this.lblDateVal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDateVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblDateVal.Location = new System.Drawing.Point(380, 27);
            this.lblDateVal.Name = "lblDateVal";
            this.lblDateVal.Size = new System.Drawing.Size(31, 25);
            this.lblDateVal.TabIndex = 3;
            this.lblDateVal.Text = "—";
            // 
            // lblRefCap
            // 
            this.lblRefCap.AutoSize = true;
            this.lblRefCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblRefCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblRefCap.Location = new System.Drawing.Point(570, 10);
            this.lblRefCap.Name = "lblRefCap";
            this.lblRefCap.Size = new System.Drawing.Size(156, 17);
            this.lblRefCap.TabIndex = 4;
            this.lblRefCap.Text = "SUPPLIER REF / BILL NO";
            // 
            // lblRefVal
            // 
            this.lblRefVal.AutoSize = true;
            this.lblRefVal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblRefVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblRefVal.Location = new System.Drawing.Point(570, 27);
            this.lblRefVal.Name = "lblRefVal";
            this.lblRefVal.Size = new System.Drawing.Size(31, 25);
            this.lblRefVal.TabIndex = 5;
            this.lblRefVal.Text = "—";
            // 
            // lblCreatedCap
            // 
            this.lblCreatedCap.AutoSize = true;
            this.lblCreatedCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblCreatedCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblCreatedCap.Location = new System.Drawing.Point(760, 10);
            this.lblCreatedCap.Name = "lblCreatedCap";
            this.lblCreatedCap.Size = new System.Drawing.Size(84, 17);
            this.lblCreatedCap.TabIndex = 6;
            this.lblCreatedCap.Text = "CREATED AT";
            // 
            // lblCreatedVal
            // 
            this.lblCreatedVal.AutoSize = true;
            this.lblCreatedVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCreatedVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblCreatedVal.Location = new System.Drawing.Point(760, 27);
            this.lblCreatedVal.Name = "lblCreatedVal";
            this.lblCreatedVal.Size = new System.Drawing.Size(27, 23);
            this.lblCreatedVal.TabIndex = 7;
            this.lblCreatedVal.Text = "—";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.pnlBody.Controls.Add(this.pnlGrid);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 140);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(14, 10, 14, 0);
            this.pnlBody.Size = new System.Drawing.Size(1002, 343);
            this.pnlBody.TabIndex = 0;
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvItems);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(14, 10);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(974, 333);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSrNo,
            this.colCode,
            this.colProduct,
            this.colUnit,
            this.colQty,
            this.colPrice,
            this.colTotal});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.dgvItems.Location = new System.Drawing.Point(0, 36);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.RowTemplate.Height = 38;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(974, 297);
            this.dgvItems.TabIndex = 0;
            // 
            // colSrNo
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.colSrNo.DefaultCellStyle = dataGridViewCellStyle3;
            this.colSrNo.FillWeight = 5F;
            this.colSrNo.HeaderText = "#";
            this.colSrNo.MinimumWidth = 6;
            this.colSrNo.Name = "colSrNo";
            this.colSrNo.ReadOnly = true;
            // 
            // colCode
            // 
            this.colCode.FillWeight = 10F;
            this.colCode.HeaderText = "Code";
            this.colCode.MinimumWidth = 6;
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colProduct
            // 
            this.colProduct.FillWeight = 38F;
            this.colProduct.HeaderText = "Product Name";
            this.colProduct.MinimumWidth = 6;
            this.colProduct.Name = "colProduct";
            this.colProduct.ReadOnly = true;
            // 
            // colUnit
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colUnit.DefaultCellStyle = dataGridViewCellStyle4;
            this.colUnit.FillWeight = 8F;
            this.colUnit.HeaderText = "Unit";
            this.colUnit.MinimumWidth = 6;
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            // 
            // colQty
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle5.Format = "N2";
            this.colQty.DefaultCellStyle = dataGridViewCellStyle5;
            this.colQty.FillWeight = 8F;
            this.colQty.HeaderText = "Qty";
            this.colQty.MinimumWidth = 6;
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.colPrice.FillWeight = 16F;
            this.colPrice.HeaderText = "Purchase Price (Rs.)";
            this.colPrice.MinimumWidth = 6;
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colTotal
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.Format = "N2";
            this.colTotal.DefaultCellStyle = dataGridViewCellStyle6;
            this.colTotal.FillWeight = 15F;
            this.colTotal.HeaderText = "Line Total (Rs.)";
            this.colTotal.MinimumWidth = 6;
            this.colTotal.Name = "colTotal";
            this.colTotal.ReadOnly = true;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.BackColor = System.Drawing.Color.SlateBlue;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.White;
            this.lblGridTitle.Location = new System.Drawing.Point(0, 0);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(974, 36);
            this.lblGridTitle.TabIndex = 1;
            this.lblGridTitle.Text = "  Items Purchased";
            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.pnlPayStatus);
            this.pnlFooter.Controls.Add(this.pnlTotals);
            this.pnlFooter.Controls.Add(this.lblNotesCap);
            this.pnlFooter.Controls.Add(this.lblNotesVal);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 483);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1002, 156);
            this.pnlFooter.TabIndex = 1;
            // 
            // pnlPayStatus
            // 
            this.pnlPayStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlPayStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.pnlPayStatus.Controls.Add(this.lblPayHeading);
            this.pnlPayStatus.Controls.Add(this.lblPaidCap);
            this.pnlPayStatus.Controls.Add(this.lblPaidVal);
            this.pnlPayStatus.Controls.Add(this.lblBalCap);
            this.pnlPayStatus.Controls.Add(this.lblBalVal);
            this.pnlPayStatus.Controls.Add(this.lblPayStatCap);
            this.pnlPayStatus.Controls.Add(this.lblPayStatVal);
            this.pnlPayStatus.Location = new System.Drawing.Point(14, 10);
            this.pnlPayStatus.Name = "pnlPayStatus";
            this.pnlPayStatus.Size = new System.Drawing.Size(330, 110);
            this.pnlPayStatus.TabIndex = 0;
            // 
            // lblPayHeading
            // 
            this.lblPayHeading.AutoSize = true;
            this.lblPayHeading.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblPayHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblPayHeading.Location = new System.Drawing.Point(10, 8);
            this.lblPayHeading.Name = "lblPayHeading";
            this.lblPayHeading.Size = new System.Drawing.Size(137, 17);
            this.lblPayHeading.TabIndex = 0;
            this.lblPayHeading.Text = "PAYMENT SUMMARY";
            // 
            // lblPaidCap
            // 
            this.lblPaidCap.AutoSize = true;
            this.lblPaidCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPaidCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPaidCap.Location = new System.Drawing.Point(10, 28);
            this.lblPaidCap.Name = "lblPaidCap";
            this.lblPaidCap.Size = new System.Drawing.Size(87, 23);
            this.lblPaidCap.TabIndex = 1;
            this.lblPaidCap.Text = "Total Paid:";
            // 
            // lblPaidVal
            // 
            this.lblPaidVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblPaidVal.Location = new System.Drawing.Point(178, 28);
            this.lblPaidVal.Name = "lblPaidVal";
            this.lblPaidVal.Size = new System.Drawing.Size(140, 22);
            this.lblPaidVal.TabIndex = 2;
            this.lblPaidVal.Text = "Rs. 0.00";
            this.lblPaidVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalCap
            // 
            this.lblBalCap.AutoSize = true;
            this.lblBalCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBalCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblBalCap.Location = new System.Drawing.Point(10, 56);
            this.lblBalCap.Name = "lblBalCap";
            this.lblBalCap.Size = new System.Drawing.Size(109, 23);
            this.lblBalCap.TabIndex = 3;
            this.lblBalCap.Text = "Balance Due:";
            // 
            // lblBalVal
            // 
            this.lblBalVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblBalVal.Location = new System.Drawing.Point(178, 56);
            this.lblBalVal.Name = "lblBalVal";
            this.lblBalVal.Size = new System.Drawing.Size(140, 22);
            this.lblBalVal.TabIndex = 4;
            this.lblBalVal.Text = "Rs. 0.00";
            this.lblBalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPayStatCap
            // 
            this.lblPayStatCap.AutoSize = true;
            this.lblPayStatCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPayStatCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblPayStatCap.Location = new System.Drawing.Point(10, 82);
            this.lblPayStatCap.Name = "lblPayStatCap";
            this.lblPayStatCap.Size = new System.Drawing.Size(60, 23);
            this.lblPayStatCap.TabIndex = 5;
            this.lblPayStatCap.Text = "Status:";
            // 
            // lblPayStatVal
            // 
            this.lblPayStatVal.AutoSize = true;
            this.lblPayStatVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPayStatVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
            this.lblPayStatVal.Location = new System.Drawing.Point(74, 82);
            this.lblPayStatVal.Name = "lblPayStatVal";
            this.lblPayStatVal.Size = new System.Drawing.Size(75, 23);
            this.lblPayStatVal.TabIndex = 6;
            this.lblPayStatVal.Text = "Pending";
            // 
            // pnlTotals
            // 
            this.pnlTotals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTotals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlTotals.Controls.Add(this.lblSubCap);
            this.pnlTotals.Controls.Add(this.lblSubVal);
            this.pnlTotals.Controls.Add(this.lblDiscCap);
            this.pnlTotals.Controls.Add(this.lblDiscVal);
            this.pnlTotals.Controls.Add(this.pnlSep);
            this.pnlTotals.Controls.Add(this.lblNetCap);
            this.pnlTotals.Controls.Add(this.lblNetVal);
            this.pnlTotals.Location = new System.Drawing.Point(540, 10);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Size = new System.Drawing.Size(447, 110);
            this.pnlTotals.TabIndex = 1;
            // 
            // lblSubCap
            // 
            this.lblSubCap.AutoSize = true;
            this.lblSubCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblSubCap.Location = new System.Drawing.Point(12, 10);
            this.lblSubCap.Name = "lblSubCap";
            this.lblSubCap.Size = new System.Drawing.Size(78, 23);
            this.lblSubCap.TabIndex = 0;
            this.lblSubCap.Text = "Subtotal:";
            // 
            // lblSubVal
            // 
            this.lblSubVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lblSubVal.Location = new System.Drawing.Point(216, 10);
            this.lblSubVal.Name = "lblSubVal";
            this.lblSubVal.Size = new System.Drawing.Size(170, 22);
            this.lblSubVal.TabIndex = 1;
            this.lblSubVal.Text = "Rs. 0.00";
            this.lblSubVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscCap
            // 
            this.lblDiscCap.AutoSize = true;
            this.lblDiscCap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblDiscCap.Location = new System.Drawing.Point(12, 38);
            this.lblDiscCap.Name = "lblDiscCap";
            this.lblDiscCap.Size = new System.Drawing.Size(81, 23);
            this.lblDiscCap.TabIndex = 2;
            this.lblDiscCap.Text = "Discount:";
            // 
            // lblDiscVal
            // 
            this.lblDiscVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblDiscVal.Location = new System.Drawing.Point(216, 38);
            this.lblDiscVal.Name = "lblDiscVal";
            this.lblDiscVal.Size = new System.Drawing.Size(170, 22);
            this.lblDiscVal.TabIndex = 3;
            this.lblDiscVal.Text = "Rs. 0.00";
            this.lblDiscVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSep
            // 
            this.pnlSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
            this.pnlSep.Location = new System.Drawing.Point(12, 66);
            this.pnlSep.Name = "pnlSep";
            this.pnlSep.Size = new System.Drawing.Size(376, 1);
            this.pnlSep.TabIndex = 4;
            // 
            // lblNetCap
            // 
            this.lblNetCap.AutoSize = true;
            this.lblNetCap.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNetCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblNetCap.Location = new System.Drawing.Point(12, 72);
            this.lblNetCap.Name = "lblNetCap";
            this.lblNetCap.Size = new System.Drawing.Size(146, 30);
            this.lblNetCap.TabIndex = 5;
            this.lblNetCap.Text = "Net Amount:";
            // 
            // lblNetVal
            // 
            this.lblNetVal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblNetVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblNetVal.Location = new System.Drawing.Point(216, 72);
            this.lblNetVal.Name = "lblNetVal";
            this.lblNetVal.Size = new System.Drawing.Size(170, 28);
            this.lblNetVal.TabIndex = 6;
            this.lblNetVal.Text = "Rs. 0.00";
            this.lblNetVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNotesCap
            // 
            this.lblNotesCap.AutoSize = true;
            this.lblNotesCap.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblNotesCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblNotesCap.Location = new System.Drawing.Point(14, 128);
            this.lblNotesCap.Name = "lblNotesCap";
            this.lblNotesCap.Size = new System.Drawing.Size(55, 20);
            this.lblNotesCap.TabIndex = 2;
            this.lblNotesCap.Text = "Notes:";
            // 
            // lblNotesVal
            // 
            this.lblNotesVal.AutoSize = true;
            this.lblNotesVal.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblNotesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblNotesVal.Location = new System.Drawing.Point(72, 128);
            this.lblNotesVal.Name = "lblNotesVal";
            this.lblNotesVal.Size = new System.Drawing.Size(24, 20);
            this.lblNotesVal.TabIndex = 3;
            this.lblNotesVal.Text = "—";
            // 
            // pnlActionBar
            // 
            this.pnlActionBar.BackColor = System.Drawing.Color.White;
            this.pnlActionBar.Controls.Add(this.BtnPDF);
            this.pnlActionBar.Controls.Add(this.btnPrint);
            this.pnlActionBar.Controls.Add(this.btnClose);
            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionBar.Location = new System.Drawing.Point(0, 639);
            this.pnlActionBar.Name = "pnlActionBar";
            this.pnlActionBar.Size = new System.Drawing.Size(1002, 54);
            this.pnlActionBar.TabIndex = 2;
            // 
            // BtnPDF
            // 
            this.BtnPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPDF.BackColor = System.Drawing.Color.SlateBlue;
            this.BtnPDF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnPDF.FlatAppearance.BorderSize = 0;
            this.BtnPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnPDF.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BtnPDF.ForeColor = System.Drawing.Color.White;
            this.BtnPDF.Location = new System.Drawing.Point(421, 9);
            this.BtnPDF.Name = "BtnPDF";
            this.BtnPDF.Size = new System.Drawing.Size(160, 36);
            this.BtnPDF.TabIndex = 2;
            this.BtnPDF.Text = "🖨  PDF / Export";
            this.BtnPDF.UseVisualStyleBackColor = false;
            this.BtnPDF.Click += new System.EventHandler(this.BtnPDF_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.SlateBlue;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderSize = 0;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(14, 9);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(160, 36);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "🖨  Print / Export";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.btnClose.Location = new System.Drawing.Point(180, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 36);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // PurchaseDetailForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1002, 693);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlActionBar);
            this.Controls.Add(this.pnlMeta);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(860, 640);
            this.Name = "PurchaseDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Purchase Invoice Detail";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlStatusBadge.ResumeLayout(false);
            this.pnlMeta.ResumeLayout(false);
            this.pnlMeta.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlPayStatus.ResumeLayout(false);
            this.pnlPayStatus.PerformLayout();
            this.pnlTotals.ResumeLayout(false);
            this.pnlTotals.PerformLayout();
            this.pnlActionBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // ── Header ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle, lblInvoiceNo;
        private System.Windows.Forms.Panel pnlStatusBadge;
        private System.Windows.Forms.Label lblStatus;

        // ── Meta ──────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlMeta;
        private System.Windows.Forms.Label lblSupCap, lblSupVal;
        private System.Windows.Forms.Label lblDateCap, lblDateVal;
        private System.Windows.Forms.Label lblRefCap, lblRefVal;
        private System.Windows.Forms.Label lblCreatedCap, lblCreatedVal;

        // ── Body / Grid ───────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlBody, pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSrNo, colCode, colProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit, colQty, colPrice, colTotal;

        // ── Footer ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Panel pnlPayStatus;
        private System.Windows.Forms.Label lblPayHeading;
        private System.Windows.Forms.Label lblPaidCap, lblPaidVal;
        private System.Windows.Forms.Label lblBalCap, lblBalVal;
        private System.Windows.Forms.Label lblPayStatCap, lblPayStatVal;
        private System.Windows.Forms.Panel pnlTotals;
        private System.Windows.Forms.Label lblSubCap, lblSubVal;
        private System.Windows.Forms.Label lblDiscCap, lblDiscVal;
        private System.Windows.Forms.Panel pnlSep;
        private System.Windows.Forms.Label lblNetCap, lblNetVal;
        private System.Windows.Forms.Label lblNotesCap, lblNotesVal;

        // ── Action bar ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlActionBar;
        private System.Windows.Forms.Button btnPrint, btnClose;
        private System.Windows.Forms.Button BtnPDF;
    }
}





//namespace POS_Shop.Views.Controllers.Supplier
//{
//    partial class PurchaseDetailForm
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && components != null) components.Dispose();
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        private void InitializeComponent()
//        {
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
//            this.pnlHeader = new System.Windows.Forms.Panel();
//            this.lblTitle = new System.Windows.Forms.Label();
//            this.lblInvoiceNo = new System.Windows.Forms.Label();
//            this.pnlStatusBadge = new System.Windows.Forms.Panel();
//            this.lblStatus = new System.Windows.Forms.Label();
//            this.pnlMeta = new System.Windows.Forms.Panel();
//            this.lblSupCap = new System.Windows.Forms.Label();
//            this.lblSupVal = new System.Windows.Forms.Label();
//            this.lblDateCap = new System.Windows.Forms.Label();
//            this.lblDateVal = new System.Windows.Forms.Label();
//            this.lblRefCap = new System.Windows.Forms.Label();
//            this.lblRefVal = new System.Windows.Forms.Label();
//            this.lblCreatedCap = new System.Windows.Forms.Label();
//            this.lblCreatedVal = new System.Windows.Forms.Label();
//            this.pnlBody = new System.Windows.Forms.Panel();
//            this.pnlGrid = new System.Windows.Forms.Panel();
//            this.dgvItems = new System.Windows.Forms.DataGridView();
//            this.colSrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.lblGridTitle = new System.Windows.Forms.Label();
//            this.pnlFooter = new System.Windows.Forms.Panel();
//            this.pnlPayStatus = new System.Windows.Forms.Panel();
//            this.lblPayHeading = new System.Windows.Forms.Label();
//            this.lblPaidCap = new System.Windows.Forms.Label();
//            this.lblPaidVal = new System.Windows.Forms.Label();
//            this.lblBalCap = new System.Windows.Forms.Label();
//            this.lblBalVal = new System.Windows.Forms.Label();
//            this.lblPayStatCap = new System.Windows.Forms.Label();
//            this.lblPayStatVal = new System.Windows.Forms.Label();
//            this.pnlTotals = new System.Windows.Forms.Panel();
//            this.lblSubCap = new System.Windows.Forms.Label();
//            this.lblSubVal = new System.Windows.Forms.Label();
//            this.lblDiscCap = new System.Windows.Forms.Label();
//            this.lblDiscVal = new System.Windows.Forms.Label();
//            this.pnlSep = new System.Windows.Forms.Panel();
//            this.lblNetCap = new System.Windows.Forms.Label();
//            this.lblNetVal = new System.Windows.Forms.Label();
//            this.lblNotesCap = new System.Windows.Forms.Label();
//            this.lblNotesVal = new System.Windows.Forms.Label();
//            this.pnlActionBar = new System.Windows.Forms.Panel();
//            this.btnPrint = new System.Windows.Forms.Button();
//            this.btnClose = new System.Windows.Forms.Button();
//            this.pnlHeader.SuspendLayout();
//            this.pnlStatusBadge.SuspendLayout();
//            this.pnlMeta.SuspendLayout();
//            this.pnlBody.SuspendLayout();
//            this.pnlGrid.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
//            this.pnlFooter.SuspendLayout();
//            this.pnlPayStatus.SuspendLayout();
//            this.pnlTotals.SuspendLayout();
//            this.pnlActionBar.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // pnlHeader
//            // 
//            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            this.pnlHeader.Controls.Add(this.lblTitle);
//            this.pnlHeader.Controls.Add(this.lblInvoiceNo);
//            this.pnlHeader.Controls.Add(this.pnlStatusBadge);
//            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
//            this.pnlHeader.Name = "pnlHeader";
//            this.pnlHeader.Size = new System.Drawing.Size(1002, 64);
//            this.pnlHeader.TabIndex = 4;
//            // 
//            // lblTitle
//            // 
//            this.lblTitle.AutoSize = true;
//            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
//            this.lblTitle.ForeColor = System.Drawing.Color.White;
//            this.lblTitle.Location = new System.Drawing.Point(18, 16);
//            this.lblTitle.Name = "lblTitle";
//            this.lblTitle.Size = new System.Drawing.Size(232, 37);
//            this.lblTitle.TabIndex = 0;
//            this.lblTitle.Text = "Purchase Invoice";
//            // 
//            // lblInvoiceNo
//            // 
//            this.lblInvoiceNo.AutoSize = true;
//            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
//            this.lblInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(144)))), ((int)(((byte)(202)))), ((int)(((byte)(249)))));
//            this.lblInvoiceNo.Location = new System.Drawing.Point(270, 22);
//            this.lblInvoiceNo.Name = "lblInvoiceNo";
//            this.lblInvoiceNo.Size = new System.Drawing.Size(115, 28);
//            this.lblInvoiceNo.TabIndex = 1;
//            this.lblInvoiceNo.Text = "INV-00000";
//            // 
//            // pnlStatusBadge
//            // 
//            this.pnlStatusBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.pnlStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
//            this.pnlStatusBadge.Controls.Add(this.lblStatus);
//            this.pnlStatusBadge.Location = new System.Drawing.Point(1646, 16);
//            this.pnlStatusBadge.Name = "pnlStatusBadge";
//            this.pnlStatusBadge.Size = new System.Drawing.Size(154, 32);
//            this.pnlStatusBadge.TabIndex = 2;
//            // 
//            // lblStatus
//            // 
//            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
//            this.lblStatus.ForeColor = System.Drawing.Color.White;
//            this.lblStatus.Location = new System.Drawing.Point(0, 0);
//            this.lblStatus.Name = "lblStatus";
//            this.lblStatus.Size = new System.Drawing.Size(154, 32);
//            this.lblStatus.TabIndex = 0;
//            this.lblStatus.Text = "⏳  PENDING";
//            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            // 
//            // pnlMeta
//            // 
//            this.pnlMeta.BackColor = System.Drawing.Color.White;
//            this.pnlMeta.Controls.Add(this.lblSupCap);
//            this.pnlMeta.Controls.Add(this.lblSupVal);
//            this.pnlMeta.Controls.Add(this.lblDateCap);
//            this.pnlMeta.Controls.Add(this.lblDateVal);
//            this.pnlMeta.Controls.Add(this.lblRefCap);
//            this.pnlMeta.Controls.Add(this.lblRefVal);
//            this.pnlMeta.Controls.Add(this.lblCreatedCap);
//            this.pnlMeta.Controls.Add(this.lblCreatedVal);
//            this.pnlMeta.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlMeta.Location = new System.Drawing.Point(0, 64);
//            this.pnlMeta.Name = "pnlMeta";
//            this.pnlMeta.Size = new System.Drawing.Size(1002, 76);
//            this.pnlMeta.TabIndex = 3;
//            // 
//            // lblSupCap
//            // 
//            this.lblSupCap.AutoSize = true;
//            this.lblSupCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblSupCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
//            this.lblSupCap.Location = new System.Drawing.Point(18, 10);
//            this.lblSupCap.Name = "lblSupCap";
//            this.lblSupCap.Size = new System.Drawing.Size(66, 17);
//            this.lblSupCap.TabIndex = 0;
//            this.lblSupCap.Text = "SUPPLIER";
//            // 
//            // lblSupVal
//            // 
//            this.lblSupVal.AutoSize = true;
//            this.lblSupVal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
//            this.lblSupVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            this.lblSupVal.Location = new System.Drawing.Point(18, 27);
//            this.lblSupVal.Name = "lblSupVal";
//            this.lblSupVal.Size = new System.Drawing.Size(31, 25);
//            this.lblSupVal.TabIndex = 1;
//            this.lblSupVal.Text = "—";
//            // 
//            // lblDateCap
//            // 
//            this.lblDateCap.AutoSize = true;
//            this.lblDateCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblDateCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
//            this.lblDateCap.Location = new System.Drawing.Point(380, 10);
//            this.lblDateCap.Name = "lblDateCap";
//            this.lblDateCap.Size = new System.Drawing.Size(111, 17);
//            this.lblDateCap.TabIndex = 2;
//            this.lblDateCap.Text = "PURCHASE DATE";
//            // 
//            // lblDateVal
//            // 
//            this.lblDateVal.AutoSize = true;
//            this.lblDateVal.Font = new System.Drawing.Font("Segoe UI", 11F);
//            this.lblDateVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
//            this.lblDateVal.Location = new System.Drawing.Point(380, 27);
//            this.lblDateVal.Name = "lblDateVal";
//            this.lblDateVal.Size = new System.Drawing.Size(31, 25);
//            this.lblDateVal.TabIndex = 3;
//            this.lblDateVal.Text = "—";
//            // 
//            // lblRefCap
//            // 
//            this.lblRefCap.AutoSize = true;
//            this.lblRefCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblRefCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
//            this.lblRefCap.Location = new System.Drawing.Point(570, 10);
//            this.lblRefCap.Name = "lblRefCap";
//            this.lblRefCap.Size = new System.Drawing.Size(156, 17);
//            this.lblRefCap.TabIndex = 4;
//            this.lblRefCap.Text = "SUPPLIER REF / BILL NO";
//            // 
//            // lblRefVal
//            // 
//            this.lblRefVal.AutoSize = true;
//            this.lblRefVal.Font = new System.Drawing.Font("Segoe UI", 11F);
//            this.lblRefVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
//            this.lblRefVal.Location = new System.Drawing.Point(570, 27);
//            this.lblRefVal.Name = "lblRefVal";
//            this.lblRefVal.Size = new System.Drawing.Size(31, 25);
//            this.lblRefVal.TabIndex = 5;
//            this.lblRefVal.Text = "—";
//            // 
//            // lblCreatedCap
//            // 
//            this.lblCreatedCap.AutoSize = true;
//            this.lblCreatedCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblCreatedCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
//            this.lblCreatedCap.Location = new System.Drawing.Point(760, 10);
//            this.lblCreatedCap.Name = "lblCreatedCap";
//            this.lblCreatedCap.Size = new System.Drawing.Size(84, 17);
//            this.lblCreatedCap.TabIndex = 6;
//            this.lblCreatedCap.Text = "CREATED AT";
//            // 
//            // lblCreatedVal
//            // 
//            this.lblCreatedVal.AutoSize = true;
//            this.lblCreatedVal.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblCreatedVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
//            this.lblCreatedVal.Location = new System.Drawing.Point(760, 27);
//            this.lblCreatedVal.Name = "lblCreatedVal";
//            this.lblCreatedVal.Size = new System.Drawing.Size(27, 23);
//            this.lblCreatedVal.TabIndex = 7;
//            this.lblCreatedVal.Text = "—";
//            // 
//            // pnlBody
//            // 
//            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
//            this.pnlBody.Controls.Add(this.pnlGrid);
//            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlBody.Location = new System.Drawing.Point(0, 140);
//            this.pnlBody.Name = "pnlBody";
//            this.pnlBody.Padding = new System.Windows.Forms.Padding(14, 10, 14, 0);
//            this.pnlBody.Size = new System.Drawing.Size(1002, 375);
//            this.pnlBody.TabIndex = 0;
//            // 
//            // pnlGrid
//            // 
//            this.pnlGrid.BackColor = System.Drawing.Color.White;
//            this.pnlGrid.Controls.Add(this.dgvItems);
//            this.pnlGrid.Controls.Add(this.lblGridTitle);
//            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlGrid.Location = new System.Drawing.Point(14, 10);
//            this.pnlGrid.Name = "pnlGrid";
//            this.pnlGrid.Size = new System.Drawing.Size(974, 365);
//            this.pnlGrid.TabIndex = 0;
//            // 
//            // dgvItems
//            // 
//            this.dgvItems.AllowUserToAddRows = false;
//            this.dgvItems.AllowUserToDeleteRows = false;
//            this.dgvItems.AllowUserToResizeRows = false;
//            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
//            this.dgvItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
//            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
//            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
//            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
//            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
//            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
//            this.dgvItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
//            this.dgvItems.ColumnHeadersHeight = 40;
//            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
//            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
//            this.colSrNo,
//            this.colCode,
//            this.colProduct,
//            this.colUnit,
//            this.colQty,
//            this.colPrice,
//            this.colTotal});
//            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
//            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
//            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
//            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
//            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
//            this.dgvItems.DefaultCellStyle = dataGridViewCellStyle14;
//            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.dgvItems.EnableHeadersVisualStyles = false;
//            this.dgvItems.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
//            this.dgvItems.Location = new System.Drawing.Point(0, 36);
//            this.dgvItems.MultiSelect = false;
//            this.dgvItems.Name = "dgvItems";
//            this.dgvItems.ReadOnly = true;
//            this.dgvItems.RowHeadersVisible = false;
//            this.dgvItems.RowHeadersWidth = 51;
//            this.dgvItems.RowTemplate.Height = 38;
//            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            this.dgvItems.Size = new System.Drawing.Size(974, 329);
//            this.dgvItems.TabIndex = 0;
//            // 
//            // colSrNo
//            // 
//            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
//            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
//            this.colSrNo.DefaultCellStyle = dataGridViewCellStyle10;
//            this.colSrNo.FillWeight = 5F;
//            this.colSrNo.HeaderText = "#";
//            this.colSrNo.MinimumWidth = 6;
//            this.colSrNo.Name = "colSrNo";
//            this.colSrNo.ReadOnly = true;
//            // 
//            // colCode
//            // 
//            this.colCode.FillWeight = 10F;
//            this.colCode.HeaderText = "Code";
//            this.colCode.MinimumWidth = 6;
//            this.colCode.Name = "colCode";
//            this.colCode.ReadOnly = true;
//            // 
//            // colProduct
//            // 
//            this.colProduct.FillWeight = 38F;
//            this.colProduct.HeaderText = "Product Name";
//            this.colProduct.MinimumWidth = 6;
//            this.colProduct.Name = "colProduct";
//            this.colProduct.ReadOnly = true;
//            // 
//            // colUnit
//            // 
//            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
//            this.colUnit.DefaultCellStyle = dataGridViewCellStyle11;
//            this.colUnit.FillWeight = 8F;
//            this.colUnit.HeaderText = "Unit";
//            this.colUnit.MinimumWidth = 6;
//            this.colUnit.Name = "colUnit";
//            this.colUnit.ReadOnly = true;
//            // 
//            // colQty
//            // 
//            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
//            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
//            dataGridViewCellStyle12.Format = "N2";
//            this.colQty.DefaultCellStyle = dataGridViewCellStyle12;
//            this.colQty.FillWeight = 8F;
//            this.colQty.HeaderText = "Qty";
//            this.colQty.MinimumWidth = 6;
//            this.colQty.Name = "colQty";
//            this.colQty.ReadOnly = true;
//            // 
//            // colPrice
//            // 
//            //this.colPrice.DefaultCellStyle = dataGridViewCellStyle5;
//            //this.colPrice.FillWeight = 16F;
//            //this.colPrice.HeaderText = "Purchase Price (Rs.)";
//            //this.colPrice.MinimumWidth = 6;
//            //this.colPrice.Name = "colPrice";
//            //this.colPrice.ReadOnly = true;
//            // 
//            // colTotal
//            // 
//            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
//            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
//            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            dataGridViewCellStyle13.Format = "N2";
//            this.colTotal.DefaultCellStyle = dataGridViewCellStyle13;
//            this.colTotal.FillWeight = 15F;
//            this.colTotal.HeaderText = "Line Total (Rs.)";
//            this.colTotal.MinimumWidth = 6;
//            this.colTotal.Name = "colTotal";
//            this.colTotal.ReadOnly = true;
//            // 
//            // lblGridTitle
//            // 
//            this.lblGridTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblGridTitle.ForeColor = System.Drawing.Color.White;
//            this.lblGridTitle.Location = new System.Drawing.Point(0, 0);
//            this.lblGridTitle.Name = "lblGridTitle";
//            this.lblGridTitle.Size = new System.Drawing.Size(974, 36);
//            this.lblGridTitle.TabIndex = 1;
//            this.lblGridTitle.Text = "  Items Purchased";
//            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            // 
//            // pnlFooter
//            // 
//            this.pnlFooter.BackColor = System.Drawing.Color.White;
//            this.pnlFooter.Controls.Add(this.pnlPayStatus);
//            this.pnlFooter.Controls.Add(this.pnlTotals);
//            this.pnlFooter.Controls.Add(this.lblNotesCap);
//            this.pnlFooter.Controls.Add(this.lblNotesVal);
//            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.pnlFooter.Location = new System.Drawing.Point(0, 515);
//            this.pnlFooter.Name = "pnlFooter";
//            this.pnlFooter.Size = new System.Drawing.Size(1002, 120);
//            this.pnlFooter.TabIndex = 1;
//            // 
//            // pnlPayStatus
//            // 
//            this.pnlPayStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.pnlPayStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
//            this.pnlPayStatus.Controls.Add(this.lblPayHeading);
//            this.pnlPayStatus.Controls.Add(this.lblPaidCap);
//            this.pnlPayStatus.Controls.Add(this.lblPaidVal);
//            this.pnlPayStatus.Controls.Add(this.lblBalCap);
//            this.pnlPayStatus.Controls.Add(this.lblBalVal);
//            this.pnlPayStatus.Controls.Add(this.lblPayStatCap);
//            this.pnlPayStatus.Controls.Add(this.lblPayStatVal);
//            this.pnlPayStatus.Location = new System.Drawing.Point(1312, 10);
//            this.pnlPayStatus.Name = "pnlPayStatus";
//            this.pnlPayStatus.Size = new System.Drawing.Size(480, 90);
//            this.pnlPayStatus.TabIndex = 0;
//            // 
//            // lblPayHeading
//            // 
//            this.lblPayHeading.AutoSize = true;
//            this.lblPayHeading.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblPayHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            this.lblPayHeading.Location = new System.Drawing.Point(14, 8);
//            this.lblPayHeading.Name = "lblPayHeading";
//            this.lblPayHeading.Size = new System.Drawing.Size(137, 17);
//            this.lblPayHeading.TabIndex = 0;
//            this.lblPayHeading.Text = "PAYMENT SUMMARY";
//            // 
//            // lblPaidCap
//            // 
//            this.lblPaidCap.AutoSize = true;
//            this.lblPaidCap.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblPaidCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
//            this.lblPaidCap.Location = new System.Drawing.Point(14, 25);
//            this.lblPaidCap.Name = "lblPaidCap";
//            this.lblPaidCap.Size = new System.Drawing.Size(87, 23);
//            this.lblPaidCap.TabIndex = 1;
//            this.lblPaidCap.Text = "Total Paid:";
//            // 
//            // lblPaidVal
//            // 
//            this.lblPaidVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblPaidVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
//            this.lblPaidVal.Location = new System.Drawing.Point(308, 25);
//            this.lblPaidVal.Name = "lblPaidVal";
//            this.lblPaidVal.Size = new System.Drawing.Size(160, 22);
//            this.lblPaidVal.TabIndex = 2;
//            this.lblPaidVal.Text = "Rs. 0.00";
//            this.lblPaidVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // lblBalCap
//            // 
//            this.lblBalCap.AutoSize = true;
//            this.lblBalCap.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblBalCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
//            this.lblBalCap.Location = new System.Drawing.Point(14, 50);
//            this.lblBalCap.Name = "lblBalCap";
//            this.lblBalCap.Size = new System.Drawing.Size(109, 23);
//            this.lblBalCap.TabIndex = 3;
//            this.lblBalCap.Text = "Balance Due:";
//            // 
//            // lblBalVal
//            // 
//            this.lblBalVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblBalVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
//            this.lblBalVal.Location = new System.Drawing.Point(308, 50);
//            this.lblBalVal.Name = "lblBalVal";
//            this.lblBalVal.Size = new System.Drawing.Size(160, 22);
//            this.lblBalVal.TabIndex = 4;
//            this.lblBalVal.Text = "Rs. 0.00";
//            this.lblBalVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // lblPayStatCap
//            // 
//            this.lblPayStatCap.AutoSize = true;
//            this.lblPayStatCap.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblPayStatCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
//            this.lblPayStatCap.Location = new System.Drawing.Point(14, 70);
//            this.lblPayStatCap.Name = "lblPayStatCap";
//            this.lblPayStatCap.Size = new System.Drawing.Size(60, 23);
//            this.lblPayStatCap.TabIndex = 5;
//            this.lblPayStatCap.Text = "Status:";
//            // 
//            // lblPayStatVal
//            // 
//            this.lblPayStatVal.AutoSize = true;
//            this.lblPayStatVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblPayStatVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(124)))), ((int)(((byte)(0)))));
//            this.lblPayStatVal.Location = new System.Drawing.Point(80, 70);
//            this.lblPayStatVal.Name = "lblPayStatVal";
//            this.lblPayStatVal.Size = new System.Drawing.Size(75, 23);
//            this.lblPayStatVal.TabIndex = 6;
//            this.lblPayStatVal.Text = "Pending";
//            // 
//            // pnlTotals
//            // 
//            this.pnlTotals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
//            this.pnlTotals.Controls.Add(this.lblSubCap);
//            this.pnlTotals.Controls.Add(this.lblSubVal);
//            this.pnlTotals.Controls.Add(this.lblDiscCap);
//            this.pnlTotals.Controls.Add(this.lblDiscVal);
//            this.pnlTotals.Controls.Add(this.pnlSep);
//            this.pnlTotals.Controls.Add(this.lblNetCap);
//            this.pnlTotals.Controls.Add(this.lblNetVal);
//            this.pnlTotals.Location = new System.Drawing.Point(14, 10);
//            this.pnlTotals.Name = "pnlTotals";
//            this.pnlTotals.Size = new System.Drawing.Size(480, 90);
//            this.pnlTotals.TabIndex = 1;
//            // 
//            // lblSubCap
//            // 
//            this.lblSubCap.AutoSize = true;
//            this.lblSubCap.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblSubCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
//            this.lblSubCap.Location = new System.Drawing.Point(14, 10);
//            this.lblSubCap.Name = "lblSubCap";
//            this.lblSubCap.Size = new System.Drawing.Size(77, 23);
//            this.lblSubCap.TabIndex = 0;
//            this.lblSubCap.Text = "Total Bill:";
//            // 
//            // lblSubVal
//            // 
//            this.lblSubVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblSubVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
//            this.lblSubVal.Location = new System.Drawing.Point(314, 10);
//            this.lblSubVal.Name = "lblSubVal";
//            this.lblSubVal.Size = new System.Drawing.Size(150, 22);
//            this.lblSubVal.TabIndex = 1;
//            this.lblSubVal.Text = "Rs. 0.00";
//            this.lblSubVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // lblDiscCap
//            // 
//            this.lblDiscCap.AutoSize = true;
//            this.lblDiscCap.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblDiscCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
//            this.lblDiscCap.Location = new System.Drawing.Point(14, 36);
//            this.lblDiscCap.Name = "lblDiscCap";
//            this.lblDiscCap.Size = new System.Drawing.Size(81, 23);
//            this.lblDiscCap.TabIndex = 2;
//            this.lblDiscCap.Text = "Discount:";
//            // 
//            // lblDiscVal
//            // 
//            this.lblDiscVal.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.lblDiscVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
//            this.lblDiscVal.Location = new System.Drawing.Point(314, 36);
//            this.lblDiscVal.Name = "lblDiscVal";
//            this.lblDiscVal.Size = new System.Drawing.Size(150, 22);
//            this.lblDiscVal.TabIndex = 3;
//            this.lblDiscVal.Text = "Rs. 0.00";
//            this.lblDiscVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // pnlSep
//            // 
//            this.pnlSep.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(216)))), ((int)(((byte)(220)))));
//            this.pnlSep.Location = new System.Drawing.Point(14, 62);
//            this.pnlSep.Name = "pnlSep";
//            this.pnlSep.Size = new System.Drawing.Size(452, 1);
//            this.pnlSep.TabIndex = 4;
//            // 
//            // lblNetCap
//            // 
//            this.lblNetCap.AutoSize = true;
//            this.lblNetCap.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
//            this.lblNetCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            this.lblNetCap.Location = new System.Drawing.Point(14, 66);
//            this.lblNetCap.Name = "lblNetCap";
//            this.lblNetCap.Size = new System.Drawing.Size(134, 28);
//            this.lblNetCap.TabIndex = 5;
//            this.lblNetCap.Text = "Net Amount:";
//            // 
//            // lblNetVal
//            // 
//            this.lblNetVal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
//            this.lblNetVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            this.lblNetVal.Location = new System.Drawing.Point(314, 64);
//            this.lblNetVal.Name = "lblNetVal";
//            this.lblNetVal.Size = new System.Drawing.Size(150, 26);
//            this.lblNetVal.TabIndex = 6;
//            this.lblNetVal.Text = "Rs. 0.00";
//            this.lblNetVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
//            // 
//            // lblNotesCap
//            // 
//            this.lblNotesCap.AutoSize = true;
//            this.lblNotesCap.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
//            this.lblNotesCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
//            this.lblNotesCap.Location = new System.Drawing.Point(14, 106);
//            this.lblNotesCap.Name = "lblNotesCap";
//            this.lblNotesCap.Size = new System.Drawing.Size(55, 20);
//            this.lblNotesCap.TabIndex = 2;
//            this.lblNotesCap.Text = "Notes:";
//            // 
//            // lblNotesVal
//            // 
//            this.lblNotesVal.AutoSize = true;
//            this.lblNotesVal.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
//            this.lblNotesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
//            this.lblNotesVal.Location = new System.Drawing.Point(72, 106);
//            this.lblNotesVal.Name = "lblNotesVal";
//            this.lblNotesVal.Size = new System.Drawing.Size(24, 20);
//            this.lblNotesVal.TabIndex = 3;
//            this.lblNotesVal.Text = "—";
//            // 
//            // pnlActionBar
//            // 
//            this.pnlActionBar.BackColor = System.Drawing.Color.White;
//            this.pnlActionBar.Controls.Add(this.btnPrint);
//            this.pnlActionBar.Controls.Add(this.btnClose);
//            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.pnlActionBar.Location = new System.Drawing.Point(0, 635);
//            this.pnlActionBar.Name = "pnlActionBar";
//            this.pnlActionBar.Size = new System.Drawing.Size(1002, 58);
//            this.pnlActionBar.TabIndex = 2;
//            // 
//            // btnPrint
//            // 
//            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
//            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnPrint.FlatAppearance.BorderSize = 0;
//            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.btnPrint.ForeColor = System.Drawing.Color.White;
//            this.btnPrint.Location = new System.Drawing.Point(14, 10);
//            this.btnPrint.Name = "btnPrint";
//            this.btnPrint.Size = new System.Drawing.Size(160, 36);
//            this.btnPrint.TabIndex = 0;
//            this.btnPrint.Text = "🖨  Print / Export";
//            this.btnPrint.UseVisualStyleBackColor = false;
//            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
//            // 
//            // btnClose
//            // 
//            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
//            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnClose.FlatAppearance.BorderSize = 0;
//            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
//            this.btnClose.Location = new System.Drawing.Point(190, 10);
//            this.btnClose.Name = "btnClose";
//            this.btnClose.Size = new System.Drawing.Size(120, 36);
//            this.btnClose.TabIndex = 1;
//            this.btnClose.Text = "Close";
//            this.btnClose.UseVisualStyleBackColor = false;
//            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
//            // 
//            // PurchaseDetailForm
//            // 
//            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
//            this.ClientSize = new System.Drawing.Size(1002, 693);
//            this.Controls.Add(this.pnlBody);
//            this.Controls.Add(this.pnlFooter);
//            this.Controls.Add(this.pnlActionBar);
//            this.Controls.Add(this.pnlMeta);
//            this.Controls.Add(this.pnlHeader);
//            this.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.MinimumSize = new System.Drawing.Size(860, 640);
//            this.Name = "PurchaseDetailForm";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
//            this.Text = "Purchase Invoice Detail";
//            this.pnlHeader.ResumeLayout(false);
//            this.pnlHeader.PerformLayout();
//            this.pnlStatusBadge.ResumeLayout(false);
//            this.pnlMeta.ResumeLayout(false);
//            this.pnlMeta.PerformLayout();
//            this.pnlBody.ResumeLayout(false);
//            this.pnlGrid.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
//            this.pnlFooter.ResumeLayout(false);
//            this.pnlFooter.PerformLayout();
//            this.pnlPayStatus.ResumeLayout(false);
//            this.pnlPayStatus.PerformLayout();
//            this.pnlTotals.ResumeLayout(false);
//            this.pnlTotals.PerformLayout();
//            this.pnlActionBar.ResumeLayout(false);
//            this.ResumeLayout(false);

//        }

//        #endregion

//        // ── Header ────────────────────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlHeader;
//        private System.Windows.Forms.Label lblTitle, lblInvoiceNo;
//        private System.Windows.Forms.Panel pnlStatusBadge;
//        private System.Windows.Forms.Label lblStatus;

//        // ── Meta ──────────────────────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlMeta;
//        private System.Windows.Forms.Label lblSupCap, lblSupVal;
//        private System.Windows.Forms.Label lblDateCap, lblDateVal;
//        private System.Windows.Forms.Label lblRefCap, lblRefVal;
//        private System.Windows.Forms.Label lblCreatedCap, lblCreatedVal;

//        // ── Body / Grid ───────────────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlBody, pnlGrid;
//        private System.Windows.Forms.Label lblGridTitle;
//        private System.Windows.Forms.DataGridView dgvItems;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colSrNo, colCode, colProduct;
//        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit, colQty, colPrice, colTotal;

//        // ── Footer ────────────────────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlFooter;
//        private System.Windows.Forms.Panel pnlPayStatus;
//        private System.Windows.Forms.Label lblPayHeading;
//        private System.Windows.Forms.Label lblPaidCap, lblPaidVal;
//        private System.Windows.Forms.Label lblBalCap, lblBalVal;
//        private System.Windows.Forms.Label lblPayStatCap, lblPayStatVal;
//        private System.Windows.Forms.Panel pnlTotals;
//        private System.Windows.Forms.Label lblSubCap, lblSubVal;
//        private System.Windows.Forms.Label lblDiscCap, lblDiscVal;
//        private System.Windows.Forms.Panel pnlSep;
//        private System.Windows.Forms.Label lblNetCap, lblNetVal;
//        private System.Windows.Forms.Label lblNotesCap, lblNotesVal;

//        // ── Action bar ────────────────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlActionBar;
//        private System.Windows.Forms.Button btnPrint, btnClose;
//    }
//}