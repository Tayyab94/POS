//namespace POS_Shop.Views.Controllers.Supplier
//{
//    partial class AllPurchasesForm
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && components != null) components.Dispose();
//            base.Dispose(disposing);
//        }

//        private void InitializeComponent()
//        {
//            System.Windows.Forms.DataGridViewCellStyle hdrStyle = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle altStyle = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle numStyle = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();

//            // ── Declare all controls ──────────────────────────────────────────
//            this.pnlHeader = new System.Windows.Forms.Panel();
//            this.lblTitle = new System.Windows.Forms.Label();
//            this.lblSubtitle = new System.Windows.Forms.Label();

//            this.pnlSearch = new System.Windows.Forms.Panel();
//            // Row 1: Invoice search + Supplier search
//            this.lblInvCap = new System.Windows.Forms.Label();
//            this.txtInvSearch = new System.Windows.Forms.TextBox();
//            this.lblSupCap = new System.Windows.Forms.Label();
//            this.txtSupSearch = new System.Windows.Forms.TextBox();
//            this.pnlSupBadge = new System.Windows.Forms.Panel();
//            this.lblSelSup = new System.Windows.Forms.Label();
//            this.btnClrSup = new System.Windows.Forms.Button();
//            // Row 2: Status + Dates + Search button
//            this.lblStatusCap = new System.Windows.Forms.Label();
//            this.cmbStatus = new System.Windows.Forms.ComboBox();
//            this.lblDateFromCap = new System.Windows.Forms.Label();
//            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
//            this.lblDateToCap = new System.Windows.Forms.Label();
//            this.dtpTo = new System.Windows.Forms.DateTimePicker();
//            this.btnSearch = new System.Windows.Forms.Button();
//            this.lblResultInfo = new System.Windows.Forms.Label();

//            this.pnlGrid = new System.Windows.Forms.Panel();
//            this.lblGridTitle = new System.Windows.Forms.Label();
//            this.dgvPurchases = new System.Windows.Forms.DataGridView();
//            this.colAPInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPTotalBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPNetAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
//            this.colAPStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();

//            this.pnlPager = new System.Windows.Forms.Panel();
//            this.btnPrev = new System.Windows.Forms.Button();
//            this.lblPageInfo = new System.Windows.Forms.Label();
//            this.btnNext = new System.Windows.Forms.Button();
//            this.lblPageSize = new System.Windows.Forms.Label();
//            this.cmbPageSize = new System.Windows.Forms.ComboBox();

//            this.pnlActionBar = new System.Windows.Forms.Panel();
//            this.btnClose = new System.Windows.Forms.Button();

//            this.lstSupSugg = new System.Windows.Forms.ListBox();

//            this.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).BeginInit();

//            // ══════════════════════════════════════════════════════════════════
//            //  FORM
//            // ══════════════════════════════════════════════════════════════════
//            this.Text = "All Purchase History";
//            this.Size = new System.Drawing.Size(1280, 780);
//            this.MinimumSize = new System.Drawing.Size(1050, 620);
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
//            this.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

//            // ══════════════════════════════════════════════════════════════════
//            //  HEADER  (blue accent — distinguishes from Paid green)
//            // ══════════════════════════════════════════════════════════════════
//            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlHeader.Height = 64;
//            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.pnlHeader.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.lblTitle, this.lblSubtitle });

//            this.lblTitle.Text = "All Purchase History";
//            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
//            this.lblTitle.ForeColor = System.Drawing.Color.White;
//            this.lblTitle.AutoSize = true;
//            this.lblTitle.Location = new System.Drawing.Point(18, 14);

//            this.lblSubtitle.Text = "All invoices  —  Pending  ·  Partially Paid  ·  Paid";
//            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(187, 222, 251);
//            this.lblSubtitle.AutoSize = true;
//            this.lblSubtitle.Location = new System.Drawing.Point(21, 44);

//            // ══════════════════════════════════════════════════════════════════
//            //  SEARCH PANEL  (2 rows — taller to fit all filters)
//            // ══════════════════════════════════════════════════════════════════
//            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlSearch.Height = 118;
//            this.pnlSearch.BackColor = System.Drawing.Color.White;
//            this.pnlSearch.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
//            this.pnlSearch.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.lblInvCap,    this.txtInvSearch,
//                this.lblSupCap,    this.txtSupSearch,   this.pnlSupBadge,
//                this.lblStatusCap, this.cmbStatus,
//                this.lblDateFromCap, this.dtpFrom,
//                this.lblDateToCap,   this.dtpTo,
//                this.btnSearch,    this.lblResultInfo });

//            // ── Row 1: Invoice No search ──────────────────────────────────────
//            this.lblInvCap.Text = "INVOICE NO";
//            this.lblInvCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblInvCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
//            this.lblInvCap.AutoSize = true;
//            this.lblInvCap.Location = new System.Drawing.Point(14, 10);

//            this.txtInvSearch.Location = new System.Drawing.Point(14, 26);
//            this.txtInvSearch.Size = new System.Drawing.Size(190, 28);
//            this.txtInvSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtInvSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

//            // ── Row 1: Supplier search ────────────────────────────────────────
//            this.lblSupCap.Text = "SUPPLIER  (leave blank for all)";
//            this.lblSupCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblSupCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
//            this.lblSupCap.AutoSize = true;
//            this.lblSupCap.Location = new System.Drawing.Point(220, 10);

//            this.txtSupSearch.Location = new System.Drawing.Point(220, 26);
//            this.txtSupSearch.Size = new System.Drawing.Size(230, 28);
//            this.txtSupSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.txtSupSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

//            // Supplier badge
//            this.pnlSupBadge.BackColor = System.Drawing.Color.FromArgb(227, 242, 253);
//            this.pnlSupBadge.Location = new System.Drawing.Point(220, 60);
//            this.pnlSupBadge.Size = new System.Drawing.Size(250, 26);
//            this.pnlSupBadge.Visible = false;
//            this.pnlSupBadge.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.lblSelSup, this.btnClrSup });

//            this.lblSelSup.Text = "";
//            this.lblSelSup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            this.lblSelSup.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.lblSelSup.AutoSize = false;
//            this.lblSelSup.Size = new System.Drawing.Size(220, 26);
//            this.lblSelSup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            this.lblSelSup.Location = new System.Drawing.Point(6, 0);

//            this.btnClrSup.Text = "✕";
//            this.btnClrSup.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
//            this.btnClrSup.ForeColor = System.Drawing.Color.FromArgb(198, 40, 40);
//            this.btnClrSup.BackColor = System.Drawing.Color.Transparent;
//            this.btnClrSup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnClrSup.FlatAppearance.BorderSize = 0;
//            this.btnClrSup.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnClrSup.Size = new System.Drawing.Size(26, 26);
//            this.btnClrSup.Location = new System.Drawing.Point(224, 0);

//            // ── Row 2: Status filter ──────────────────────────────────────────
//            this.lblStatusCap.Text = "STATUS";
//            this.lblStatusCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblStatusCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
//            this.lblStatusCap.AutoSize = true;
//            this.lblStatusCap.Location = new System.Drawing.Point(14, 66);

//            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.cmbStatus.Items.AddRange(new object[] { "All Statuses", "Pending", "Partially Paid", "Paid" });
//            this.cmbStatus.SelectedIndex = 0;
//            this.cmbStatus.Location = new System.Drawing.Point(14, 82);
//            this.cmbStatus.Size = new System.Drawing.Size(190, 28);

//            // ── Row 2: Date From ──────────────────────────────────────────────
//            this.lblDateFromCap.Text = "FROM DATE";
//            this.lblDateFromCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblDateFromCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
//            this.lblDateFromCap.AutoSize = true;
//            this.lblDateFromCap.Location = new System.Drawing.Point(480, 10);

//            this.dtpFrom.Location = new System.Drawing.Point(480, 26);
//            this.dtpFrom.Size = new System.Drawing.Size(170, 28);
//            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;

//            // ── Row 2: Date To ────────────────────────────────────────────────
//            this.lblDateToCap.Text = "TO DATE";
//            this.lblDateToCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
//            this.lblDateToCap.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
//            this.lblDateToCap.AutoSize = true;
//            this.lblDateToCap.Location = new System.Drawing.Point(665, 10);

//            this.dtpTo.Location = new System.Drawing.Point(665, 26);
//            this.dtpTo.Size = new System.Drawing.Size(170, 28);
//            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;

//            // ── Search button ─────────────────────────────────────────────────
//            this.btnSearch.Text = "🔍  Search";
//            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.btnSearch.ForeColor = System.Drawing.Color.White;
//            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnSearch.FlatAppearance.BorderSize = 0;
//            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnSearch.Size = new System.Drawing.Size(130, 34);
//            this.btnSearch.Location = new System.Drawing.Point(850, 36);

//            // Result info
//            this.lblResultInfo.Text = "Set filters above and click Search.";
//            this.lblResultInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
//            this.lblResultInfo.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
//            this.lblResultInfo.AutoSize = true;
//            this.lblResultInfo.Location = new System.Drawing.Point(1000, 46);

//            // ══════════════════════════════════════════════════════════════════
//            //  GRID PANEL
//            // ══════════════════════════════════════════════════════════════════
//            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlGrid.BackColor = System.Drawing.Color.White;
//            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
//            this.pnlGrid.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.dgvPurchases, this.lblGridTitle });

//            this.lblGridTitle.Text = "  All Purchases";
//            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
//            this.lblGridTitle.Height = 36;
//            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblGridTitle.ForeColor = System.Drawing.Color.White;
//            this.lblGridTitle.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

//            // ── Grid styles ───────────────────────────────────────────────────
//            hdrStyle.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            hdrStyle.ForeColor = System.Drawing.Color.White;
//            hdrStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
//            hdrStyle.SelectionBackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            hdrStyle.SelectionForeColor = System.Drawing.Color.White;

//            cellStyle.BackColor = System.Drawing.Color.White;
//            cellStyle.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
//            cellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(187, 222, 251);
//            cellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(13, 71, 161);

//            altStyle.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);

//            numStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
//            numStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            numStyle.Format = "N2";

//            boldStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
//            boldStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
//            boldStyle.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            boldStyle.Format = "N2";

//            this.dgvPurchases.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.dgvPurchases.AllowUserToAddRows = false;
//            this.dgvPurchases.AllowUserToDeleteRows = false;
//            this.dgvPurchases.AllowUserToResizeRows = false;
//            this.dgvPurchases.ReadOnly = true;
//            this.dgvPurchases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.dgvPurchases.BackgroundColor = System.Drawing.Color.White;
//            this.dgvPurchases.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.dgvPurchases.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
//            this.dgvPurchases.GridColor = System.Drawing.Color.FromArgb(236, 239, 241);
//            this.dgvPurchases.ColumnHeadersDefaultCellStyle = hdrStyle;
//            this.dgvPurchases.ColumnHeadersHeight = 40;
//            this.dgvPurchases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
//            this.dgvPurchases.DefaultCellStyle = cellStyle;
//            this.dgvPurchases.AlternatingRowsDefaultCellStyle = altStyle;
//            this.dgvPurchases.EnableHeadersVisualStyles = false;
//            this.dgvPurchases.MultiSelect = false;
//            this.dgvPurchases.RowHeadersVisible = false;
//            this.dgvPurchases.RowTemplate.Height = 38;
//            this.dgvPurchases.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

//            // ── Columns ───────────────────────────────────────────────────────
//            this.colAPInvNo.Name = "colAPInvNo";
//            this.colAPInvNo.HeaderText = "Invoice No";
//            this.colAPInvNo.FillWeight = 10F;
//            this.colAPInvNo.ReadOnly = true;

//            this.colAPSupplier.Name = "colAPSupplier";
//            this.colAPSupplier.HeaderText = "Supplier";
//            this.colAPSupplier.FillWeight = 20F;
//            this.colAPSupplier.ReadOnly = true;

//            this.colAPDate.Name = "colAPDate";
//            this.colAPDate.HeaderText = "Purchase Date";
//            this.colAPDate.FillWeight = 11F;
//            this.colAPDate.ReadOnly = true;

//            this.colAPItems.Name = "colAPItems";
//            this.colAPItems.HeaderText = "Items";
//            this.colAPItems.FillWeight = 5F;
//            this.colAPItems.ReadOnly = true;
//            this.colAPItems.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

//            this.colAPTotalBill.Name = "colAPTotalBill";
//            this.colAPTotalBill.HeaderText = "Total Bill";
//            this.colAPTotalBill.FillWeight = 11F;
//            this.colAPTotalBill.ReadOnly = true;
//            this.colAPTotalBill.DefaultCellStyle = numStyle;

//            this.colAPDiscount.Name = "colAPDiscount";
//            this.colAPDiscount.HeaderText = "Discount";
//            this.colAPDiscount.FillWeight = 8F;
//            this.colAPDiscount.ReadOnly = true;
//            this.colAPDiscount.DefaultCellStyle = numStyle;

//            this.colAPNetAmt.Name = "colAPNetAmt";
//            this.colAPNetAmt.HeaderText = "Net Amount";
//            this.colAPNetAmt.FillWeight = 11F;
//            this.colAPNetAmt.ReadOnly = true;
//            this.colAPNetAmt.DefaultCellStyle = boldStyle;

//            this.colAPPaid.Name = "colAPPaid";
//            this.colAPPaid.HeaderText = "Paid";
//            this.colAPPaid.FillWeight = 10F;
//            this.colAPPaid.ReadOnly = true;
//            this.colAPPaid.DefaultCellStyle = numStyle;

//            this.colAPBalance.Name = "colAPBalance";
//            this.colAPBalance.HeaderText = "Balance Due";
//            this.colAPBalance.FillWeight = 10F;
//            this.colAPBalance.ReadOnly = true;
//            this.colAPBalance.DefaultCellStyle = numStyle;

//            this.colAPStatus.Name = "colAPStatus";
//            this.colAPStatus.HeaderText = "Status";
//            this.colAPStatus.FillWeight = 8F;
//            this.colAPStatus.ReadOnly = true;
//            this.colAPStatus.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

//            this.dgvPurchases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
//                this.colAPInvNo, this.colAPSupplier, this.colAPDate, this.colAPItems,
//                this.colAPTotalBill, this.colAPDiscount, this.colAPNetAmt,
//                this.colAPPaid, this.colAPBalance, this.colAPStatus });

//            // ══════════════════════════════════════════════════════════════════
//            //  PAGINATION BAR
//            // ══════════════════════════════════════════════════════════════════
//            this.pnlPager.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.pnlPager.Height = 48;
//            this.pnlPager.BackColor = System.Drawing.Color.White;
//            this.pnlPager.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.btnPrev, this.lblPageInfo, this.btnNext,
//                this.lblPageSize, this.cmbPageSize });

//            this.btnPrev.Text = "◀  Previous";
//            this.btnPrev.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            this.btnPrev.ForeColor = System.Drawing.Color.White;
//            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnPrev.FlatAppearance.BorderSize = 0;
//            this.btnPrev.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnPrev.Size = new System.Drawing.Size(120, 32);
//            this.btnPrev.Location = new System.Drawing.Point(14, 8);
//            this.btnPrev.Enabled = false;

//            this.lblPageInfo.Text = "Page 1";
//            this.lblPageInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
//            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.lblPageInfo.AutoSize = false;
//            this.lblPageInfo.Size = new System.Drawing.Size(260, 32);
//            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
//            this.lblPageInfo.Location = new System.Drawing.Point(142, 8);

//            this.btnNext.Text = "Next  ▶";
//            this.btnNext.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            this.btnNext.ForeColor = System.Drawing.Color.White;
//            this.btnNext.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnNext.FlatAppearance.BorderSize = 0;
//            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnNext.Size = new System.Drawing.Size(120, 32);
//            this.btnNext.Location = new System.Drawing.Point(410, 8);
//            this.btnNext.Enabled = false;

//            this.lblPageSize.Text = "Rows per page:";
//            this.lblPageSize.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.lblPageSize.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
//            this.lblPageSize.AutoSize = true;
//            this.lblPageSize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
//            this.lblPageSize.Location = new System.Drawing.Point(1000, 16);

//            this.cmbPageSize.Items.AddRange(new object[] { "10", "20", "50", "100" });
//            this.cmbPageSize.SelectedIndex = 1;
//            this.cmbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
//            this.cmbPageSize.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.cmbPageSize.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
//            this.cmbPageSize.Location = new System.Drawing.Point(1120, 12);
//            this.cmbPageSize.Size = new System.Drawing.Size(60, 26);

//            // ══════════════════════════════════════════════════════════════════
//            //  ACTION BAR
//            // ══════════════════════════════════════════════════════════════════
//            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.pnlActionBar.Height = 48;
//            this.pnlActionBar.BackColor = System.Drawing.Color.White;
//            this.pnlActionBar.Controls.Add(this.btnClose);

//            this.btnClose.Text = "Close";
//            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
//            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(55, 71, 79);
//            this.btnClose.BackColor = System.Drawing.Color.FromArgb(236, 239, 241);
//            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnClose.FlatAppearance.BorderSize = 0;
//            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnClose.Size = new System.Drawing.Size(120, 34);
//            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
//            this.btnClose.Location = new System.Drawing.Point(1130, 7);

//            // ══════════════════════════════════════════════════════════════════
//            //  SUGGESTION DROPDOWN  (floating, must be added last)
//            // ══════════════════════════════════════════════════════════════════
//            this.lstSupSugg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
//            this.lstSupSugg.ItemHeight = 42;
//            this.lstSupSugg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.lstSupSugg.BackColor = System.Drawing.Color.White;
//            this.lstSupSugg.Visible = false;

//            // ── Wire events ───────────────────────────────────────────────────
//            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
//            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
//            this.btnClrSup.Click += new System.EventHandler(this.BtnClrSup_Click);
//            this.btnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
//            this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
//            this.cmbPageSize.SelectedIndexChanged += new System.EventHandler(this.CmbPageSize_SelectedIndexChanged);
//            this.txtSupSearch.TextChanged += new System.EventHandler(this.TxtSupSearch_TextChanged);
//            this.txtSupSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSupSearch_KeyDown);
//            this.txtSupSearch.Leave += new System.EventHandler(this.TxtSupSearch_Leave);
//            this.lstSupSugg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LstSupSugg_MouseClick);
//            this.lstSupSugg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstSupSugg_KeyDown);
//            this.lstSupSugg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstSupSugg_DrawItem);
//            this.dgvPurchases.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellClick);
//            this.dgvPurchases.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvPurchases_CellFormatting);
//            this.dgvPurchases.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellMouseEnter);
//            this.dgvPurchases.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellMouseLeave);

//            // ── Add to form (lstSupSugg last so it floats on top) ─────────────
//            this.Controls.AddRange(new System.Windows.Forms.Control[] {
//                this.pnlGrid,
//                this.pnlPager,
//                this.pnlActionBar,
//                this.pnlSearch,
//                this.pnlHeader,
//                this.lstSupSugg
//            });

//            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).EndInit();
//            this.ResumeLayout(false);
//        }

//        // ── Fields ────────────────────────────────────────────────────────────
//        private System.Windows.Forms.Panel pnlHeader, pnlSearch, pnlGrid, pnlPager, pnlActionBar;
//        private System.Windows.Forms.Label lblTitle, lblSubtitle;
//        private System.Windows.Forms.Label lblInvCap, lblSupCap, lblStatusCap, lblDateFromCap, lblDateToCap;
//        private System.Windows.Forms.Label lblResultInfo, lblGridTitle, lblPageInfo, lblPageSize;
//        private System.Windows.Forms.TextBox txtInvSearch, txtSupSearch;
//        private System.Windows.Forms.Panel pnlSupBadge;
//        private System.Windows.Forms.Label lblSelSup;
//        private System.Windows.Forms.Button btnClrSup, btnSearch, btnPrev, btnNext, btnClose;
//        private System.Windows.Forms.ComboBox cmbStatus, cmbPageSize;
//        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
//        private System.Windows.Forms.DataGridView dgvPurchases;
//        private System.Windows.Forms.DataGridViewTextBoxColumn
//            colAPInvNo, colAPSupplier, colAPDate, colAPItems,
//            colAPTotalBill, colAPDiscount, colAPNetAmt,
//            colAPPaid, colAPBalance, colAPStatus;
//        private System.Windows.Forms.ListBox lstSupSugg;
//    }
//}




namespace POS_Shop.Views.Controllers.Supplier
{
    partial class AllPurchasesForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblInvCap = new System.Windows.Forms.Label();
            this.txtInvSearch = new System.Windows.Forms.TextBox();
            this.lblSupCap = new System.Windows.Forms.Label();
            this.txtSupSearch = new System.Windows.Forms.TextBox();
            this.pnlSupBadge = new System.Windows.Forms.Panel();
            this.lblSelSup = new System.Windows.Forms.Label();
            this.btnClrSup = new System.Windows.Forms.Button();
            this.lblStatusCap = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblDateFromCap = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateToCap = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblResultInfo = new System.Windows.Forms.Label();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvPurchases = new System.Windows.Forms.DataGridView();
            this.colAPInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPTotalBill = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPNetAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAPFlow = new System.Windows.Forms.DataGridViewButtonColumn();
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
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1262, 64);
            this.pnlHeader.TabIndex = 4;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(277, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "All Purchase History";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            this.lblSubtitle.Location = new System.Drawing.Point(21, 44);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(318, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "All invoices  —  Pending  ·  Partially Paid  ·  Paid";
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Controls.Add(this.lblInvCap);
            this.pnlSearch.Controls.Add(this.txtInvSearch);
            this.pnlSearch.Controls.Add(this.lblSupCap);
            this.pnlSearch.Controls.Add(this.txtSupSearch);
            this.pnlSearch.Controls.Add(this.pnlSupBadge);
            this.pnlSearch.Controls.Add(this.lblStatusCap);
            this.pnlSearch.Controls.Add(this.cmbStatus);
            this.pnlSearch.Controls.Add(this.lblDateFromCap);
            this.pnlSearch.Controls.Add(this.dtpFrom);
            this.pnlSearch.Controls.Add(this.lblDateToCap);
            this.pnlSearch.Controls.Add(this.dtpTo);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.lblResultInfo);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 64);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlSearch.Size = new System.Drawing.Size(1262, 118);
            this.pnlSearch.TabIndex = 3;
            // 
            // lblInvCap
            // 
            this.lblInvCap.AutoSize = true;
            this.lblInvCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblInvCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblInvCap.Location = new System.Drawing.Point(14, 10);
            this.lblInvCap.Name = "lblInvCap";
            this.lblInvCap.Size = new System.Drawing.Size(84, 17);
            this.lblInvCap.TabIndex = 0;
            this.lblInvCap.Text = "INVOICE NO";
            // 
            // txtInvSearch
            // 
            this.txtInvSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInvSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtInvSearch.Location = new System.Drawing.Point(14, 26);
            this.txtInvSearch.Name = "txtInvSearch";
            this.txtInvSearch.Size = new System.Drawing.Size(190, 30);
            this.txtInvSearch.TabIndex = 1;
            // 
            // lblSupCap
            // 
            this.lblSupCap.AutoSize = true;
            this.lblSupCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSupCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblSupCap.Location = new System.Drawing.Point(220, 10);
            this.lblSupCap.Name = "lblSupCap";
            this.lblSupCap.Size = new System.Drawing.Size(195, 17);
            this.lblSupCap.TabIndex = 2;
            this.lblSupCap.Text = "SUPPLIER  (leave blank for all)";
            // 
            // txtSupSearch
            // 
            this.txtSupSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSupSearch.Location = new System.Drawing.Point(220, 26);
            this.txtSupSearch.Name = "txtSupSearch";
            this.txtSupSearch.Size = new System.Drawing.Size(230, 30);
            this.txtSupSearch.TabIndex = 3;
            this.txtSupSearch.TextChanged += new System.EventHandler(this.TxtSupSearch_TextChanged);
            this.txtSupSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSupSearch_KeyDown);
            this.txtSupSearch.Leave += new System.EventHandler(this.TxtSupSearch_Leave);
            // 
            // pnlSupBadge
            // 
            this.pnlSupBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.pnlSupBadge.Controls.Add(this.lblSelSup);
            this.pnlSupBadge.Controls.Add(this.btnClrSup);
            this.pnlSupBadge.Location = new System.Drawing.Point(220, 60);
            this.pnlSupBadge.Name = "pnlSupBadge";
            this.pnlSupBadge.Size = new System.Drawing.Size(250, 26);
            this.pnlSupBadge.TabIndex = 4;
            this.pnlSupBadge.Visible = false;
            // 
            // lblSelSup
            // 
            this.lblSelSup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelSup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblSelSup.Location = new System.Drawing.Point(6, 0);
            this.lblSelSup.Name = "lblSelSup";
            this.lblSelSup.Size = new System.Drawing.Size(220, 26);
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
            this.btnClrSup.Location = new System.Drawing.Point(224, 0);
            this.btnClrSup.Name = "btnClrSup";
            this.btnClrSup.Size = new System.Drawing.Size(26, 26);
            this.btnClrSup.TabIndex = 1;
            this.btnClrSup.Text = "✕";
            this.btnClrSup.UseVisualStyleBackColor = false;
            this.btnClrSup.Click += new System.EventHandler(this.BtnClrSup_Click);
            // 
            // lblStatusCap
            // 
            this.lblStatusCap.AutoSize = true;
            this.lblStatusCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblStatusCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblStatusCap.Location = new System.Drawing.Point(14, 66);
            this.lblStatusCap.Name = "lblStatusCap";
            this.lblStatusCap.Size = new System.Drawing.Size(54, 17);
            this.lblStatusCap.TabIndex = 5;
            this.lblStatusCap.Text = "STATUS";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.Items.AddRange(new object[] {
            "All Statuses",
            "Pending",
            "Partially Paid",
            "Paid"});
            this.cmbStatus.Location = new System.Drawing.Point(14, 82);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(190, 31);
            this.cmbStatus.TabIndex = 6;
            // 
            // lblDateFromCap
            // 
            this.lblDateFromCap.AutoSize = true;
            this.lblDateFromCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDateFromCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDateFromCap.Location = new System.Drawing.Point(480, 10);
            this.lblDateFromCap.Name = "lblDateFromCap";
            this.lblDateFromCap.Size = new System.Drawing.Size(82, 17);
            this.lblDateFromCap.TabIndex = 7;
            this.lblDateFromCap.Text = "FROM DATE";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(480, 26);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(170, 30);
            this.dtpFrom.TabIndex = 8;
            // 
            // lblDateToCap
            // 
            this.lblDateToCap.AutoSize = true;
            this.lblDateToCap.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDateToCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDateToCap.Location = new System.Drawing.Point(665, 10);
            this.lblDateToCap.Name = "lblDateToCap";
            this.lblDateToCap.Size = new System.Drawing.Size(63, 17);
            this.lblDateToCap.TabIndex = 9;
            this.lblDateToCap.Text = "TO DATE";
            // 
            // dtpTo
            // 
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(665, 26);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(170, 30);
            this.dtpTo.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(850, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(130, 34);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.Text = "🔍  Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblResultInfo
            // 
            this.lblResultInfo.AutoSize = true;
            this.lblResultInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblResultInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblResultInfo.Location = new System.Drawing.Point(1000, 33);
            this.lblResultInfo.Name = "lblResultInfo";
            this.lblResultInfo.Size = new System.Drawing.Size(219, 20);
            this.lblResultInfo.TabIndex = 12;
            this.lblResultInfo.Text = "Set filters above and click Search.";
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvPurchases);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 182);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.pnlGrid.Size = new System.Drawing.Size(1262, 455);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvPurchases
            // 
            this.dgvPurchases.AllowUserToAddRows = false;
            this.dgvPurchases.AllowUserToDeleteRows = false;
            this.dgvPurchases.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.dgvPurchases.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvPurchases.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchases.BackgroundColor = System.Drawing.Color.White;
            this.dgvPurchases.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPurchases.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            this.dgvPurchases.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvPurchases.ColumnHeadersHeight = 40;
            this.dgvPurchases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvPurchases.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAPInvNo,
            this.colAPSupplier,
            this.colAPDate,
            this.colAPItems,
            this.colAPTotalBill,
            this.colAPDiscount,
            this.colAPNetAmt,
            this.colAPPaid,
            this.colAPBalance,
            this.colAPStatus,
            this.colAPFlow});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(222)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(71)))), ((int)(((byte)(161)))));
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchases.DefaultCellStyle = dataGridViewCellStyle16;
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
            this.dgvPurchases.Size = new System.Drawing.Size(1234, 419);
            this.dgvPurchases.TabIndex = 0;
            this.dgvPurchases.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellClick);
            this.dgvPurchases.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvPurchases_CellFormatting);
            this.dgvPurchases.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellMouseEnter);
            this.dgvPurchases.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvPurchases_CellMouseLeave);
            // 
            // colAPInvNo
            // 
            this.colAPInvNo.FillWeight = 10F;
            this.colAPInvNo.HeaderText = "Invoice No";
            this.colAPInvNo.MinimumWidth = 6;
            this.colAPInvNo.Name = "colAPInvNo";
            this.colAPInvNo.ReadOnly = true;
            // 
            // colAPSupplier
            // 
            this.colAPSupplier.FillWeight = 20F;
            this.colAPSupplier.HeaderText = "Supplier";
            this.colAPSupplier.MinimumWidth = 6;
            this.colAPSupplier.Name = "colAPSupplier";
            this.colAPSupplier.ReadOnly = true;
            // 
            // colAPDate
            // 
            this.colAPDate.FillWeight = 11F;
            this.colAPDate.HeaderText = "Purchase Date";
            this.colAPDate.MinimumWidth = 6;
            this.colAPDate.Name = "colAPDate";
            this.colAPDate.ReadOnly = true;
            // 
            // colAPItems
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colAPItems.DefaultCellStyle = dataGridViewCellStyle11;
            this.colAPItems.FillWeight = 5F;
            this.colAPItems.HeaderText = "Items";
            this.colAPItems.MinimumWidth = 6;
            this.colAPItems.Name = "colAPItems";
            this.colAPItems.ReadOnly = true;
            // 
            // colAPTotalBill
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle12.Format = "N2";
            this.colAPTotalBill.DefaultCellStyle = dataGridViewCellStyle12;
            this.colAPTotalBill.FillWeight = 11F;
            this.colAPTotalBill.HeaderText = "Total Bill";
            this.colAPTotalBill.MinimumWidth = 6;
            this.colAPTotalBill.Name = "colAPTotalBill";
            this.colAPTotalBill.ReadOnly = true;
            // 
            // colAPDiscount
            // 
           // this.colAPDiscount.DefaultCellStyle = dataGridViewCellStyle4;
            this.colAPDiscount.FillWeight = 8F;
            this.colAPDiscount.HeaderText = "Discount";
            this.colAPDiscount.MinimumWidth = 6;
            this.colAPDiscount.Name = "colAPDiscount";
            this.colAPDiscount.ReadOnly = true;
            // 
            // colAPNetAmt
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle13.Format = "N2";
            this.colAPNetAmt.DefaultCellStyle = dataGridViewCellStyle13;
            this.colAPNetAmt.FillWeight = 11F;
            this.colAPNetAmt.HeaderText = "Net Amount";
            this.colAPNetAmt.MinimumWidth = 6;
            this.colAPNetAmt.Name = "colAPNetAmt";
            this.colAPNetAmt.ReadOnly = true;
            // 
            // colAPPaid
            // 
         //   this.colAPPaid.DefaultCellStyle = dataGridViewCellStyle4;
            this.colAPPaid.FillWeight = 10F;
            this.colAPPaid.HeaderText = "Paid";
            this.colAPPaid.MinimumWidth = 6;
            this.colAPPaid.Name = "colAPPaid";
            this.colAPPaid.ReadOnly = true;
            // 
            // colAPBalance
            // 
          //  this.colAPBalance.DefaultCellStyle = dataGridViewCellStyle4;
            this.colAPBalance.FillWeight = 10F;
            this.colAPBalance.HeaderText = "Balance Due";
            this.colAPBalance.MinimumWidth = 6;
            this.colAPBalance.Name = "colAPBalance";
            this.colAPBalance.ReadOnly = true;
            // 
            // colAPStatus
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colAPStatus.DefaultCellStyle = dataGridViewCellStyle14;
            this.colAPStatus.FillWeight = 8F;
            this.colAPStatus.HeaderText = "Status";
            this.colAPStatus.MinimumWidth = 6;
            this.colAPStatus.Name = "colAPStatus";
            this.colAPStatus.ReadOnly = true;
            // 
            // colAPFlow
            // 
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            this.colAPFlow.DefaultCellStyle = dataGridViewCellStyle15;
            this.colAPFlow.FillWeight = 7F;
            this.colAPFlow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colAPFlow.HeaderText = "";
            this.colAPFlow.MinimumWidth = 6;
            this.colAPFlow.Name = "colAPFlow";
            this.colAPFlow.ReadOnly = true;
            this.colAPFlow.Text = "📋 Flow";
            this.colAPFlow.UseColumnTextForButtonValue = true;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.White;
            this.lblGridTitle.Location = new System.Drawing.Point(14, 0);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(1234, 36);
            this.lblGridTitle.TabIndex = 1;
            this.lblGridTitle.Text = "  All Purchases";
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
            this.pnlPager.Location = new System.Drawing.Point(0, 637);
            this.pnlPager.Name = "pnlPager";
            this.pnlPager.Size = new System.Drawing.Size(1262, 48);
            this.pnlPager.TabIndex = 1;
            // 
            // btnPrev
            // 
            this.btnPrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
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
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblPageInfo.Location = new System.Drawing.Point(142, 8);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(260, 32);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Page 1";
            this.lblPageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
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
            this.lblPageSize.Location = new System.Drawing.Point(2062, 16);
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
            this.cmbPageSize.Location = new System.Drawing.Point(2182, 12);
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
            this.pnlActionBar.Location = new System.Drawing.Point(0, 685);
            this.pnlActionBar.Name = "pnlActionBar";
            this.pnlActionBar.Size = new System.Drawing.Size(1262, 48);
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
            this.btnClose.Location = new System.Drawing.Point(2192, 7);
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
            // AllPurchasesForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1262, 733);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlPager);
            this.Controls.Add(this.pnlActionBar);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lstSupSugg);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1050, 620);
            this.Name = "AllPurchasesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "All Purchase History";
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

        // ── Fields ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader, pnlSearch, pnlGrid, pnlPager, pnlActionBar;
        private System.Windows.Forms.Label lblTitle, lblSubtitle;
        private System.Windows.Forms.Label lblInvCap, lblSupCap, lblStatusCap, lblDateFromCap, lblDateToCap;
        private System.Windows.Forms.Label lblResultInfo, lblGridTitle, lblPageInfo, lblPageSize;
        private System.Windows.Forms.TextBox txtInvSearch, txtSupSearch;
        private System.Windows.Forms.Panel pnlSupBadge;
        private System.Windows.Forms.Label lblSelSup;
        private System.Windows.Forms.Button btnClrSup, btnSearch, btnPrev, btnNext, btnClose;
        private System.Windows.Forms.ComboBox cmbStatus, cmbPageSize;
        private System.Windows.Forms.DateTimePicker dtpFrom, dtpTo;
        private System.Windows.Forms.DataGridView dgvPurchases;
        private System.Windows.Forms.DataGridViewTextBoxColumn
            colAPInvNo, colAPSupplier, colAPDate, colAPItems,
            colAPTotalBill, colAPDiscount, colAPNetAmt,
            colAPPaid, colAPBalance, colAPStatus;
        private System.Windows.Forms.DataGridViewButtonColumn colAPFlow;
        private System.Windows.Forms.ListBox lstSupSugg;
    }
}