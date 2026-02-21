namespace POS_Shop.Views.Controllers.Supplier
{
    partial class PaidPurchasesForm
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
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSupCap = new System.Windows.Forms.Label();
            this.txtSupSearch = new System.Windows.Forms.TextBox();
            this.pnlSupBadge = new System.Windows.Forms.Panel();
            this.lblSelSup = new System.Windows.Forms.Label();
            this.btnClrSup = new System.Windows.Forms.Button();
            this.lblDateFromCap = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateToCap = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblResultInfo = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvPurchases = new System.Windows.Forms.DataGridView();
            this.colInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNetAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaidAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlPager = new System.Windows.Forms.Panel();
            this.btnPrev = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageSize = new System.Windows.Forms.Label();
            this.cmbPageSize = new System.Windows.Forms.ComboBox();
            this.pnlActionBar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstSupSugg = new System.Windows.Forms.ListBox();
            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlSupBadge.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).BeginInit();
            this.pnlPager.SuspendLayout();
            this.pnlActionBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1182, 64);
            this.pnlHeader.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(298, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Paid Purchase History";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(214)))), ((int)(((byte)(167)))));
            this.lblSubtitle.Location = new System.Drawing.Point(21, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(318, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Fully settled invoices  —  PaymentStatus = Paid";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.lblSupCap);
            this.pnlSearch.Controls.Add(this.txtSupSearch);
            this.pnlSearch.Controls.Add(this.pnlSupBadge);
            this.pnlSearch.Controls.Add(this.lblDateFromCap);
            this.pnlSearch.Controls.Add(this.dtpFrom);
            this.pnlSearch.Controls.Add(this.lblDateToCap);
            this.pnlSearch.Controls.Add(this.dtpTo);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.lblResultInfo);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 64);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(14, 10, 14, 10);
            this.pnlSearch.Size = new System.Drawing.Size(1182, 100);
            this.pnlSearch.TabIndex = 3;
            // 
            // lblSupCap
            // 
            this.lblSupCap.AutoSize = true;
            this.lblSupCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSupCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblSupCap.Location = new System.Drawing.Point(14, 12);
            this.lblSupCap.Name = "lblSupCap";
            this.lblSupCap.Size = new System.Drawing.Size(268, 17);
            this.lblSupCap.TabIndex = 0;
            this.lblSupCap.Text = "SUPPLIER  (optional — leave blank for all)";
            // 
            // txtSupSearch
            // 
            this.txtSupSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSupSearch.Location = new System.Drawing.Point(14, 28);
            this.txtSupSearch.Name = "txtSupSearch";
            this.txtSupSearch.Size = new System.Drawing.Size(240, 30);
            this.txtSupSearch.TabIndex = 1;
            this.txtSupSearch.TextChanged += new System.EventHandler(this.TxtSupSearch_TextChanged);
            this.txtSupSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSupSearch_KeyDown);
            this.txtSupSearch.Leave += new System.EventHandler(this.TxtSupSearch_Leave);
            // 
            // pnlSupBadge
            // 
            this.pnlSupBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.pnlSupBadge.Controls.Add(this.lblSelSup);
            this.pnlSupBadge.Controls.Add(this.btnClrSup);
            this.pnlSupBadge.Location = new System.Drawing.Point(14, 62);
            this.pnlSupBadge.Name = "pnlSupBadge";
            this.pnlSupBadge.Size = new System.Drawing.Size(260, 26);
            this.pnlSupBadge.TabIndex = 2;
            this.pnlSupBadge.Visible = false;
            // 
            // lblSelSup
            // 
            this.lblSelSup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelSup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.lblSelSup.Location = new System.Drawing.Point(6, 0);
            this.lblSelSup.Name = "lblSelSup";
            this.lblSelSup.Size = new System.Drawing.Size(228, 26);
            this.lblSelSup.TabIndex = 0;
            this.lblSelSup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnClrSup
            // 
            this.btnClrSup.BackColor = System.Drawing.Color.Transparent;
            this.btnClrSup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClrSup.FlatAppearance.BorderSize = 0;
            this.btnClrSup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClrSup.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnClrSup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.btnClrSup.Location = new System.Drawing.Point(234, 0);
            this.btnClrSup.Name = "btnClrSup";
            this.btnClrSup.Size = new System.Drawing.Size(26, 26);
            this.btnClrSup.TabIndex = 1;
            this.btnClrSup.Text = "✕";
            this.btnClrSup.UseVisualStyleBackColor = false;
            this.btnClrSup.Click += new System.EventHandler(this.BtnClrSup_Click);
            // 
            // lblDateFromCap
            // 
            this.lblDateFromCap.AutoSize = true;
            this.lblDateFromCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDateFromCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDateFromCap.Location = new System.Drawing.Point(278, 12);
            this.lblDateFromCap.Name = "lblDateFromCap";
            this.lblDateFromCap.Size = new System.Drawing.Size(82, 17);
            this.lblDateFromCap.TabIndex = 3;
            this.lblDateFromCap.Text = "FROM DATE";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(278, 28);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(170, 30);
            this.dtpFrom.TabIndex = 4;
            // 
            // lblDateToCap
            // 
            this.lblDateToCap.AutoSize = true;
            this.lblDateToCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDateToCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDateToCap.Location = new System.Drawing.Point(462, 12);
            this.lblDateToCap.Name = "lblDateToCap";
            this.lblDateToCap.Size = new System.Drawing.Size(63, 17);
            this.lblDateToCap.TabIndex = 5;
            this.lblDateToCap.Text = "TO DATE";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(462, 28);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(170, 30);
            this.dtpTo.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(650, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(130, 34);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "🔍  Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblResultInfo
            // 
            this.lblResultInfo.AutoSize = true;
            this.lblResultInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblResultInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblResultInfo.Location = new System.Drawing.Point(800, 34);
            this.lblResultInfo.Name = "lblResultInfo";
            this.lblResultInfo.Size = new System.Drawing.Size(245, 20);
            this.lblResultInfo.TabIndex = 8;
            this.lblResultInfo.Text = "Enter search criteria and click Search.";
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvPurchases);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 164);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.pnlGrid.Size = new System.Drawing.Size(1182, 453);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvPurchases
            // 
            this.dgvPurchases.AllowUserToAddRows = false;
            this.dgvPurchases.AllowUserToDeleteRows = false;
            this.dgvPurchases.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(245)))));
            this.dgvPurchases.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPurchases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchases.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchases.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchases.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvPurchases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPurchases.ColumnHeadersHeight = 40;
            this.dgvPurchases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPurchases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInvNo,
            this.colSupplier,
            this.colDate,
            this.colItems,
            this.colTotalBill,
            this.colDiscount,
            this.colNetAmt,
            this.colPaidAmt,
            this.colStatus});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchases.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPurchases.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPurchases.EnableHeadersVisualStyles = false;
            this.dgvPurchases.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.dgvPurchases.Location = new System.Drawing.Point(14, 36);
            this.dgvPurchases.MultiSelect = false;
            this.dgvPurchases.Name = "dgvPurchases";
            this.dgvPurchases.ReadOnly = true;
            this.dgvPurchases.RowHeadersVisible = false;
            this.dgvPurchases.RowHeadersWidth = 51;
            this.dgvPurchases.RowTemplate.Height = 38;
            this.dgvPurchases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchases.Size = new System.Drawing.Size(1154, 417);
            this.dgvPurchases.TabIndex = 0;
            this.dgvPurchases.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellClick);
            this.dgvPurchases.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvPurchases_CellFormatting);
            this.dgvPurchases.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellMouseEnter);
            this.dgvPurchases.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellMouseLeave);
            // 
            // colInvNo
            // 
            this.colInvNo.FillWeight = 11F;
            this.colInvNo.HeaderText = "Invoice No";
            this.colInvNo.MinimumWidth = 6;
            this.colInvNo.Name = "colInvNo";
            this.colInvNo.ReadOnly = true;
            // 
            // colSupplier
            // 
            this.colSupplier.FillWeight = 22F;
            this.colSupplier.HeaderText = "Supplier";
            this.colSupplier.MinimumWidth = 6;
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.FillWeight = 12F;
            this.colDate.HeaderText = "Purchase Date";
            this.colDate.MinimumWidth = 6;
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colItems
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colItems.DefaultCellStyle = dataGridViewCellStyle3;
            this.colItems.FillWeight = 6F;
            this.colItems.HeaderText = "Items";
            this.colItems.MinimumWidth = 6;
            this.colItems.Name = "colItems";
            this.colItems.ReadOnly = true;
            // 
            // colTotalBill
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.Format = "N2";
            this.colTotalBill.DefaultCellStyle = dataGridViewCellStyle4;
            this.colTotalBill.FillWeight = 12F;
            this.colTotalBill.HeaderText = "Total Bill";
            this.colTotalBill.MinimumWidth = 6;
            this.colTotalBill.Name = "colTotalBill";
            this.colTotalBill.ReadOnly = true;
            // 
            // colDiscount
            // 
            this.colDiscount.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDiscount.FillWeight = 9F;
            this.colDiscount.HeaderText = "Discount";
            this.colDiscount.MinimumWidth = 6;
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            // 
            // colNetAmt
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle5.Format = "N2";
            this.colNetAmt.DefaultCellStyle = dataGridViewCellStyle5;
            this.colNetAmt.FillWeight = 12F;
            this.colNetAmt.HeaderText = "Net Amount";
            this.colNetAmt.MinimumWidth = 6;
            this.colNetAmt.Name = "colNetAmt";
            this.colNetAmt.ReadOnly = true;
            // 
            // colPaidAmt
            // 
            this.colPaidAmt.DefaultCellStyle = dataGridViewCellStyle4;
            this.colPaidAmt.FillWeight = 10F;
            this.colPaidAmt.HeaderText = "Paid";
            this.colPaidAmt.MinimumWidth = 6;
            this.colPaidAmt.Name = "colPaidAmt";
            this.colPaidAmt.ReadOnly = true;
            // 
            // colStatus
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle6;
            this.colStatus.FillWeight = 8F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.White;
            this.lblGridTitle.Location = new System.Drawing.Point(14, 0);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(1154, 36);
            this.lblGridTitle.TabIndex = 1;
            this.lblGridTitle.Text = "  Paid Invoices";
            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPager
            // 
            this.pnlPager.BackColor = System.Drawing.Color.White;
            this.pnlPager.Controls.Add(this.btnPrev);
            this.pnlPager.Controls.Add(this.lblPageInfo);
            this.pnlPager.Controls.Add(this.btnNext);
            this.pnlPager.Controls.Add(this.lblPageSize);
            this.pnlPager.Controls.Add(this.cmbPageSize);
            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPager.Location = new System.Drawing.Point(0, 617);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(1182, 48);
            this.pnlPager.TabIndex = 1;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrev.Enabled = false;
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnPrev.ForeColor = System.Drawing.Color.White;
            this.btnPrev.Location = new System.Drawing.Point(14, 8);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(120, 32);
            this.btnPrev.TabIndex = 0;
            this.btnPrev.Text = "◀  Previous";
            this.btnPrev.UseVisualStyleBackColor = false;
            this.btnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblPageInfo.Location = new System.Drawing.Point(142, 8);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(260, 32);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Page 1";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Enabled = false;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(410, 8);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(120, 32);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next  ▶";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
            // 
            // lblPageSize
            // 
            this.lblPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPageSize.AutoSize = true;
            this.lblPageSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPageSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblPageSize.Location = new System.Drawing.Point(1922, 16);
            this.lblPageSize.Name = "lblPageSize";
            this.lblPageSize.Size = new System.Drawing.Size(111, 20);
            this.lblPageSize.TabIndex = 3;
            this.lblPageSize.Text = "Rows per page:";
            // 
            // cmbPageSize
            // 
            this.cmbPageSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPageSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbPageSize.Items.AddRange(new object[] {
            "10",
            "20",
            "50",
            "100"});
            this.cmbPageSize.Location = new System.Drawing.Point(2042, 12);
            this.cmbPageSize.Name = "cmbPageSize";
            this.cmbPageSize.Size = new System.Drawing.Size(60, 28);
            this.cmbPageSize.TabIndex = 4;
            this.cmbPageSize.SelectedIndexChanged += new System.EventHandler(this.CmbPageSize_SelectedIndexChanged);
            // 
            // pnlActionBar
            // 
            this.pnlActionBar.BackColor = System.Drawing.Color.White;
            this.pnlActionBar.Controls.Add(this.btnClose);
            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionBar.Location = new System.Drawing.Point(0, 665);
            this.pnlActionBar.Name = "pnlActionBar";
            this.pnlActionBar.Size = new System.Drawing.Size(1182, 48);
            this.pnlActionBar.TabIndex = 2;
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
            this.btnClose.Location = new System.Drawing.Point(2042, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 34);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lstSupSugg
            // 
            this.lstSupSugg.BackColor = System.Drawing.Color.White;
            this.lstSupSugg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSupSugg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSupSugg.ItemHeight = 42;
            this.lstSupSugg.Location = new System.Drawing.Point(0, 0);
            this.lstSupSugg.Name = "lstSupSugg";
            this.lstSupSugg.Size = new System.Drawing.Size(120, 86);
            this.lstSupSugg.TabIndex = 5;
            this.lstSupSugg.Visible = false;
            this.lstSupSugg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LstSupSugg_MouseClick);
            this.lstSupSugg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstSupSugg_DrawItem);
            this.lstSupSugg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstSupSugg_KeyDown);
            // 
            // PaidPurchasesForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1182, 713);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlPager);
            this.Controls.Add(this.pnlActionBar);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lstSupSugg);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 620);
            this.Name = "PaidPurchasesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paid Purchase History";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.pnlSupBadge.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).EndInit();
            this.pnlPager.ResumeLayout(false);
            this.pnlPager.PerformLayout();
            this.pnlActionBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // ── Header ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle, lblSubtitle;

        // ── Search ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSupCap;
        private System.Windows.Forms.TextBox txtSupSearch;
        private System.Windows.Forms.Panel pnlSupBadge;
        private System.Windows.Forms.Label lblSelSup;
        private System.Windows.Forms.Button btnClrSup;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblDateFromCap, lblDateToCap;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.Label lblResultInfo;

        // ── Grid ──────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvPurchases;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvNo, colSupplier, colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItems, colTotalBill, colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetAmt, colPaidAmt, colStatus;

        // ── Pagination ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlPager;
        private System.Windows.Forms.Button btnPrev, btnNext;
        private System.Windows.Forms.Label lblPageInfo, lblPageSize;
        private System.Windows.Forms.ComboBox cmbPageSize;

        // ── Action bar ────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlActionBar;
        private System.Windows.Forms.Button btnClose;

        // ── Suggestion dropdown ───────────────────────────────────────────────
        private System.Windows.Forms.ListBox lstSupSugg;
    }
}