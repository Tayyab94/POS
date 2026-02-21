namespace POS_Shop.Views.Controllers.Supplier
{

    partial class SupplierPaymentForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPayNo = new System.Windows.Forms.Label();
            this.lblHeaderDate = new System.Windows.Forms.Label();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblSupCaption = new System.Windows.Forms.Label();
            this.txtSupSearch = new System.Windows.Forms.TextBox();
            this.pnlSupBadge = new System.Windows.Forms.Panel();
            this.lblSelSup = new System.Windows.Forms.Label();
            this.btnClrSup = new System.Windows.Forms.Button();
            this.lblDateCaption = new System.Windows.Forms.Label();
            this.dtpPayDate = new System.Windows.Forms.DateTimePicker();
            this.lblMethodCap = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblRefCaption = new System.Windows.Forms.Label();
            this.txtTxnRef = new System.Windows.Forms.TextBox();
            this.lblAmtCaption = new System.Windows.Forms.Label();
            this.txtTotalAmt = new System.Windows.Forms.TextBox();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.colInvNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colInvDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNetAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBalance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAllocate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblNotesCaption = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblTotalDueCaption = new System.Windows.Forms.Label();
            this.lblTotalDueVal = new System.Windows.Forms.Label();
            this.lblTotalAllocCaption = new System.Windows.Forms.Label();
            this.lblTotalAllocVal = new System.Windows.Forms.Label();
            this.lblRemainingCaption = new System.Windows.Forms.Label();
            this.lblRemainingVal = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.pnlActionBar = new System.Windows.Forms.Panel();
            this.btnAutoAllocate = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstSupSugg = new System.Windows.Forms.ListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHeader.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlSupBadge.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.pnlActionBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblPayNo);
            this.pnlHeader.Controls.Add(this.lblHeaderDate);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1082, 60);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(223, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Supplier Payment";
            // 
            // lblPayNo
            // 
            this.lblPayNo.AutoSize = true;
            this.lblPayNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPayNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(214)))), ((int)(((byte)(167)))));
            this.lblPayNo.Location = new System.Drawing.Point(300, 21);
            this.lblPayNo.Name = "lblPayNo";
            this.lblPayNo.Size = new System.Drawing.Size(97, 23);
            this.lblPayNo.TabIndex = 1;
            this.lblPayNo.Text = "PAY-00001";
            // 
            // lblHeaderDate
            // 
            this.lblHeaderDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeaderDate.AutoSize = true;
            this.lblHeaderDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHeaderDate.ForeColor = System.Drawing.Color.White;
            this.lblHeaderDate.Location = new System.Drawing.Point(1762, 21);
            this.lblHeaderDate.Name = "lblHeaderDate";
            this.lblHeaderDate.Size = new System.Drawing.Size(0, 23);
            this.lblHeaderDate.TabIndex = 2;
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.lblSupCaption);
            this.pnlTop.Controls.Add(this.txtSupSearch);
            this.pnlTop.Controls.Add(this.pnlSupBadge);
            this.pnlTop.Controls.Add(this.lblDateCaption);
            this.pnlTop.Controls.Add(this.dtpPayDate);
            this.pnlTop.Controls.Add(this.lblMethodCap);
            this.pnlTop.Controls.Add(this.cmbMethod);
            this.pnlTop.Controls.Add(this.lblRefCaption);
            this.pnlTop.Controls.Add(this.txtTxnRef);
            this.pnlTop.Controls.Add(this.lblAmtCaption);
            this.pnlTop.Controls.Add(this.txtTotalAmt);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 60);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1082, 110);
            this.pnlTop.TabIndex = 1;
            // 
            // lblSupCaption
            // 
            this.lblSupCaption.AutoSize = true;
            this.lblSupCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblSupCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblSupCaption.Location = new System.Drawing.Point(14, 12);
            this.lblSupCaption.Name = "lblSupCaption";
            this.lblSupCaption.Size = new System.Drawing.Size(72, 19);
            this.lblSupCaption.TabIndex = 0;
            this.lblSupCaption.Text = "SUPPLIER";
            // 
            // txtSupSearch
            // 
            this.txtSupSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSupSearch.Location = new System.Drawing.Point(14, 36);
            this.txtSupSearch.Name = "txtSupSearch";
            this.txtSupSearch.Size = new System.Drawing.Size(280, 30);
            this.txtSupSearch.TabIndex = 0;
            this.txtSupSearch.TextChanged += new System.EventHandler(this.TxtSupSearch_TextChanged);
            this.txtSupSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSupSearch_KeyDown);
            this.txtSupSearch.Leave += new System.EventHandler(this.TxtSupSearch_Leave);
            // 
            // pnlSupBadge
            // 
            this.pnlSupBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            this.pnlSupBadge.Controls.Add(this.lblSelSup);
            this.pnlSupBadge.Controls.Add(this.btnClrSup);
            this.pnlSupBadge.Location = new System.Drawing.Point(14, 72);
            this.pnlSupBadge.Name = "pnlSupBadge";
            this.pnlSupBadge.Size = new System.Drawing.Size(280, 26);
            this.pnlSupBadge.TabIndex = 1;
            this.pnlSupBadge.Visible = false;
            // 
            // lblSelSup
            // 
            this.lblSelSup.AutoEllipsis = true;
            this.lblSelSup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelSup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblSelSup.Location = new System.Drawing.Point(4, 4);
            this.lblSelSup.Name = "lblSelSup";
            this.lblSelSup.Size = new System.Drawing.Size(244, 18);
            this.lblSelSup.TabIndex = 0;
            // 
            // btnClrSup
            // 
            this.btnClrSup.BackColor = System.Drawing.Color.Transparent;
            this.btnClrSup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClrSup.FlatAppearance.BorderSize = 0;
            this.btnClrSup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClrSup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnClrSup.Location = new System.Drawing.Point(254, 1);
            this.btnClrSup.Name = "btnClrSup";
            this.btnClrSup.Size = new System.Drawing.Size(24, 22);
            this.btnClrSup.TabIndex = 1;
            this.btnClrSup.Text = "X";
            this.btnClrSup.UseVisualStyleBackColor = false;
            this.btnClrSup.Click += new System.EventHandler(this.BtnClrSup_Click);
            // 
            // lblDateCaption
            // 
            this.lblDateCaption.AutoSize = true;
            this.lblDateCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblDateCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblDateCaption.Location = new System.Drawing.Point(316, 12);
            this.lblDateCaption.Name = "lblDateCaption";
            this.lblDateCaption.Size = new System.Drawing.Size(112, 19);
            this.lblDateCaption.TabIndex = 2;
            this.lblDateCaption.Text = "PAYMENT DATE";
            // 
            // dtpPayDate
            // 
            this.dtpPayDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpPayDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPayDate.Location = new System.Drawing.Point(316, 36);
            this.dtpPayDate.Name = "dtpPayDate";
            this.dtpPayDate.Size = new System.Drawing.Size(160, 30);
            this.dtpPayDate.TabIndex = 1;
            // 
            // lblMethodCap
            // 
            this.lblMethodCap.AutoSize = true;
            this.lblMethodCap.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblMethodCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblMethodCap.Location = new System.Drawing.Point(496, 12);
            this.lblMethodCap.Name = "lblMethodCap";
            this.lblMethodCap.Size = new System.Drawing.Size(138, 19);
            this.lblMethodCap.TabIndex = 3;
            this.lblMethodCap.Text = "PAYMENT METHOD";
            // 
            // cmbMethod
            // 
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMethod.Items.AddRange(new object[] {
            "Cash",
            "Bank Transfer",
            "Cheque",
            "Online Transfer"});
            this.cmbMethod.Location = new System.Drawing.Point(496, 36);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(150, 31);
            this.cmbMethod.TabIndex = 2;
            // 
            // lblRefCaption
            // 
            this.lblRefCaption.AutoSize = true;
            this.lblRefCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblRefCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblRefCaption.Location = new System.Drawing.Point(664, 12);
            this.lblRefCaption.Name = "lblRefCaption";
            this.lblRefCaption.Size = new System.Drawing.Size(209, 19);
            this.lblRefCaption.TabIndex = 4;
            this.lblRefCaption.Text = "TXN REFERENCE / CHEQUE NO";
            // 
            // txtTxnRef
            // 
            this.txtTxnRef.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTxnRef.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTxnRef.Location = new System.Drawing.Point(664, 36);
            this.txtTxnRef.Name = "txtTxnRef";
            this.txtTxnRef.Size = new System.Drawing.Size(200, 30);
            this.txtTxnRef.TabIndex = 3;
            // 
            // lblAmtCaption
            // 
            this.lblAmtCaption.AutoSize = true;
            this.lblAmtCaption.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblAmtCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblAmtCaption.Location = new System.Drawing.Point(882, 12);
            this.lblAmtCaption.Name = "lblAmtCaption";
            this.lblAmtCaption.Size = new System.Drawing.Size(186, 19);
            this.lblAmtCaption.TabIndex = 5;
            this.lblAmtCaption.Text = "TOTAL AMOUNT PAID (Rs.)";
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalAmt.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.txtTotalAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.txtTotalAmt.Location = new System.Drawing.Point(882, 36);
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.Size = new System.Drawing.Size(160, 36);
            this.txtTotalAmt.TabIndex = 4;
            this.txtTotalAmt.Text = "0.00";
            this.txtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotalAmt.TextChanged += new System.EventHandler(this.TxtTotalAmt_TextChanged);
            this.txtTotalAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DecimalOnly);
            // 
            // pnlGrid
            // 
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvInvoices);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Location = new System.Drawing.Point(0, 170);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.pnlGrid.Size = new System.Drawing.Size(1082, 369);
            this.pnlGrid.TabIndex = 0;
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.AllowUserToAddRows = false;
            this.dgvInvoices.AllowUserToDeleteRows = false;
            this.dgvInvoices.AllowUserToResizeRows = false;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.dgvInvoices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle33;
            this.dgvInvoices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoices.BackgroundColor = System.Drawing.Color.White;
            this.dgvInvoices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.SlateBlue;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInvoices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
            this.dgvInvoices.ColumnHeadersHeight = 38;
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvInvoices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colInvNo,
            this.colInvDate,
            this.colNetAmt,
            this.colPaid,
            this.colBalance,
            this.colStatus,
            this.colAllocate});
            dataGridViewCellStyle40.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle40.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle40.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle40.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle40.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(201)))));
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle40.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvInvoices.DefaultCellStyle = dataGridViewCellStyle40;
            this.dgvInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInvoices.EnableHeadersVisualStyles = false;
            this.dgvInvoices.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvInvoices.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.dgvInvoices.Location = new System.Drawing.Point(14, 34);
            this.dgvInvoices.MultiSelect = false;
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.RowHeadersVisible = false;
            this.dgvInvoices.RowHeadersWidth = 51;
            this.dgvInvoices.RowTemplate.Height = 36;
            this.dgvInvoices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoices.Size = new System.Drawing.Size(1054, 335);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvInvoices_CellEndEdit);
            this.dgvInvoices.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvInvoices_CellFormatting);
            this.dgvInvoices.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgvInvoices_EditingControlShowing);
            // 
            // colInvNo
            // 
            this.colInvNo.FillWeight = 14F;
            this.colInvNo.HeaderText = "Invoice No";
            this.colInvNo.MinimumWidth = 6;
            this.colInvNo.Name = "colInvNo";
            this.colInvNo.ReadOnly = true;
            // 
            // colInvDate
            // 
            this.colInvDate.FillWeight = 11F;
            this.colInvDate.HeaderText = "Date";
            this.colInvDate.MinimumWidth = 6;
            this.colInvDate.Name = "colInvDate";
            this.colInvDate.ReadOnly = true;
            // 
            // colNetAmt
            // 
            dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle35.Format = "N2";
            this.colNetAmt.DefaultCellStyle = dataGridViewCellStyle35;
            this.colNetAmt.FillWeight = 14F;
            this.colNetAmt.HeaderText = "Net Amount";
            this.colNetAmt.MinimumWidth = 6;
            this.colNetAmt.Name = "colNetAmt";
            this.colNetAmt.ReadOnly = true;
            // 
            // colPaid
            // 
            dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle36.Format = "N2";
            this.colPaid.DefaultCellStyle = dataGridViewCellStyle36;
            this.colPaid.FillWeight = 14F;
            this.colPaid.HeaderText = "Already Paid";
            this.colPaid.MinimumWidth = 6;
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            // 
            // colBalance
            // 
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle37.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle37.Format = "N2";
            this.colBalance.DefaultCellStyle = dataGridViewCellStyle37;
            this.colBalance.FillWeight = 14F;
            this.colBalance.HeaderText = "Balance";
            this.colBalance.MinimumWidth = 6;
            this.colBalance.Name = "colBalance";
            this.colBalance.ReadOnly = true;
            // 
            // colStatus
            // 
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colStatus.DefaultCellStyle = dataGridViewCellStyle38;
            this.colStatus.FillWeight = 14F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colAllocate
            // 
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(233)))));
            dataGridViewCellStyle39.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(94)))), ((int)(((byte)(32)))));
            this.colAllocate.DefaultCellStyle = dataGridViewCellStyle39;
            this.colAllocate.FillWeight = 18F;
            this.colAllocate.HeaderText = "Allocate (Rs.)";
            this.colAllocate.MinimumWidth = 6;
            this.colAllocate.Name = "colAllocate";
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.BackColor = System.Drawing.Color.SlateBlue;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.White;
            this.lblGridTitle.Location = new System.Drawing.Point(14, 0);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(1054, 34);
            this.lblGridTitle.TabIndex = 1;
            this.lblGridTitle.Text = "  Pending / Partially Paid Invoices  -  Enter amount to allocate in Allocate colu" +
    "mn";
            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.White;
            this.pnlSummary.Controls.Add(this.lblNotesCaption);
            this.pnlSummary.Controls.Add(this.txtNotes);
            this.pnlSummary.Controls.Add(this.lblTotalDueCaption);
            this.pnlSummary.Controls.Add(this.lblTotalDueVal);
            this.pnlSummary.Controls.Add(this.lblTotalAllocCaption);
            this.pnlSummary.Controls.Add(this.lblTotalAllocVal);
            this.pnlSummary.Controls.Add(this.lblRemainingCaption);
            this.pnlSummary.Controls.Add(this.lblRemainingVal);
            this.pnlSummary.Controls.Add(this.lblHint);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Location = new System.Drawing.Point(0, 593);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlSummary.Size = new System.Drawing.Size(1082, 110);
            this.pnlSummary.TabIndex = 4;
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
            this.txtNotes.Location = new System.Drawing.Point(14, 28);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(380, 66);
            this.txtNotes.TabIndex = 10;
            // 
            // lblTotalDueCaption
            // 
            this.lblTotalDueCaption.AutoSize = true;
            this.lblTotalDueCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalDueCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblTotalDueCaption.Location = new System.Drawing.Point(420, 8);
            this.lblTotalDueCaption.Name = "lblTotalDueCaption";
            this.lblTotalDueCaption.Size = new System.Drawing.Size(257, 23);
            this.lblTotalDueCaption.TabIndex = 11;
            this.lblTotalDueCaption.Text = "Total Outstanding (this supplier):";
            // 
            // lblTotalDueVal
            // 
            this.lblTotalDueVal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalDueVal.Location = new System.Drawing.Point(920, 8);
            this.lblTotalDueVal.Name = "lblTotalDueVal";
            this.lblTotalDueVal.Size = new System.Drawing.Size(140, 22);
            this.lblTotalDueVal.TabIndex = 12;
            this.lblTotalDueVal.Text = "0.00";
            this.lblTotalDueVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAllocCaption
            // 
            this.lblTotalAllocCaption.AutoSize = true;
            this.lblTotalAllocCaption.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalAllocCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblTotalAllocCaption.Location = new System.Drawing.Point(420, 34);
            this.lblTotalAllocCaption.Name = "lblTotalAllocCaption";
            this.lblTotalAllocCaption.Size = new System.Drawing.Size(249, 23);
            this.lblTotalAllocCaption.TabIndex = 13;
            this.lblTotalAllocCaption.Text = "Total Allocated in this payment:";
            // 
            // lblTotalAllocVal
            // 
            this.lblTotalAllocVal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalAllocVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblTotalAllocVal.Location = new System.Drawing.Point(920, 34);
            this.lblTotalAllocVal.Name = "lblTotalAllocVal";
            this.lblTotalAllocVal.Size = new System.Drawing.Size(140, 22);
            this.lblTotalAllocVal.TabIndex = 14;
            this.lblTotalAllocVal.Text = "0.00";
            this.lblTotalAllocVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRemainingCaption
            // 
            this.lblRemainingCaption.AutoSize = true;
            this.lblRemainingCaption.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRemainingCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblRemainingCaption.Location = new System.Drawing.Point(420, 60);
            this.lblRemainingCaption.Name = "lblRemainingCaption";
            this.lblRemainingCaption.Size = new System.Drawing.Size(268, 23);
            this.lblRemainingCaption.TabIndex = 15;
            this.lblRemainingCaption.Text = "Unallocated (must be 0 to save):";
            // 
            // lblRemainingVal
            // 
            this.lblRemainingVal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRemainingVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblRemainingVal.Location = new System.Drawing.Point(920, 60);
            this.lblRemainingVal.Name = "lblRemainingVal";
            this.lblRemainingVal.Size = new System.Drawing.Size(140, 24);
            this.lblRemainingVal.TabIndex = 16;
            this.lblRemainingVal.Text = "0.00";
            this.lblRemainingVal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(144)))), ((int)(((byte)(156)))));
            this.lblHint.Location = new System.Drawing.Point(420, 86);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(498, 20);
            this.lblHint.TabIndex = 17;
            this.lblHint.Text = "Tip: Click Auto Allocate to fill oldest invoices first, or type amounts manually." +
    "";
            // 
            // pnlActionBar
            // 
            this.pnlActionBar.BackColor = System.Drawing.Color.White;
            this.pnlActionBar.Controls.Add(this.btnAutoAllocate);
            this.pnlActionBar.Controls.Add(this.btnCancel);
            this.pnlActionBar.Controls.Add(this.btnSave);
            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionBar.Location = new System.Drawing.Point(0, 539);
            this.pnlActionBar.Name = "pnlActionBar";
            this.pnlActionBar.Padding = new System.Windows.Forms.Padding(0, 8, 16, 8);
            this.pnlActionBar.Size = new System.Drawing.Size(1082, 54);
            this.pnlActionBar.TabIndex = 3;
            // 
            // btnAutoAllocate
            // 
            this.btnAutoAllocate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.btnAutoAllocate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutoAllocate.FlatAppearance.BorderSize = 0;
            this.btnAutoAllocate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoAllocate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAutoAllocate.ForeColor = System.Drawing.Color.White;
            this.btnAutoAllocate.Location = new System.Drawing.Point(468, 6);
            this.btnAutoAllocate.Name = "btnAutoAllocate";
            this.btnAutoAllocate.Size = new System.Drawing.Size(150, 38);
            this.btnAutoAllocate.TabIndex = 11;
            this.btnAutoAllocate.Text = "Auto Allocate";
            this.btnAutoAllocate.UseVisualStyleBackColor = false;
            this.btnAutoAllocate.Click += new System.EventHandler(this.BtnAutoAllocate_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(239)))), ((int)(((byte)(241)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.btnCancel.Location = new System.Drawing.Point(339, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(110, 38);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.SlateBlue;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(170, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 38);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save Payment";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // lstSupSugg
            // 
            this.lstSupSugg.BackColor = System.Drawing.Color.White;
            this.lstSupSugg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstSupSugg.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstSupSugg.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstSupSugg.ItemHeight = 44;
            this.lstSupSugg.Location = new System.Drawing.Point(0, 0);
            this.lstSupSugg.Name = "lstSupSugg";
            this.lstSupSugg.Size = new System.Drawing.Size(300, 2);
            this.lstSupSugg.TabIndex = 5;
            this.lstSupSugg.TabStop = false;
            this.lstSupSugg.Visible = false;
            this.lstSupSugg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LstSupSugg_MouseClick);
            this.lstSupSugg.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LstSupSugg_DrawItem);
            this.lstSupSugg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstSupSugg_KeyDown);
            // 
            // SupplierPaymentForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1082, 703);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlActionBar);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.lstSupSugg);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "SupplierPaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier Payment";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlSupBadge.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.pnlActionBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader, pnlTop, pnlGrid, pnlSummary, pnlActionBar;
        private System.Windows.Forms.Panel pnlSupBadge;
        private System.Windows.Forms.Label lblTitle, lblPayNo, lblHeaderDate;
        private System.Windows.Forms.Label lblSupCaption;
        private System.Windows.Forms.TextBox txtSupSearch;
        private System.Windows.Forms.Label lblSelSup;
        private System.Windows.Forms.Button btnClrSup;
        private System.Windows.Forms.Label lblDateCaption;
        private System.Windows.Forms.DateTimePicker dtpPayDate;
        private System.Windows.Forms.Label lblMethodCap;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblRefCaption;
        private System.Windows.Forms.TextBox txtTxnRef;
        private System.Windows.Forms.Label lblAmtCaption;
        private System.Windows.Forms.TextBox txtTotalAmt;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInvNo, colInvDate, colNetAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid, colBalance, colStatus, colAllocate;
        private System.Windows.Forms.Label lblNotesCaption;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblTotalDueCaption, lblTotalDueVal;
        private System.Windows.Forms.Label lblTotalAllocCaption, lblTotalAllocVal;
        private System.Windows.Forms.Label lblRemainingCaption, lblRemainingVal;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Button btnAutoAllocate, btnSave, btnCancel;
        private System.Windows.Forms.ListBox lstSupSugg;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}