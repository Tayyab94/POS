namespace POS_Shop.Views.Controllers.Supplier
{
    partial class InvoicePaymentFlowForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle hdrStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle altStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle numStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle boldStyle = new System.Windows.Forms.DataGridViewCellStyle();

            // ── Declare controls ──────────────────────────────────────────────
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();

            this.pnlSummaryCard = new System.Windows.Forms.Panel();
            // KPI boxes (5)
            this.pnlKpi1 = new System.Windows.Forms.Panel();
            this.lblKpi1Title = new System.Windows.Forms.Label();
            this.lblKpi1Val = new System.Windows.Forms.Label();
            this.pnlKpi2 = new System.Windows.Forms.Panel();
            this.lblKpi2Title = new System.Windows.Forms.Label();
            this.lblKpi2Val = new System.Windows.Forms.Label();
            this.pnlKpi3 = new System.Windows.Forms.Panel();
            this.lblKpi3Title = new System.Windows.Forms.Label();
            this.lblKpi3Val = new System.Windows.Forms.Label();
            this.pnlKpi4 = new System.Windows.Forms.Panel();
            this.lblKpi4Title = new System.Windows.Forms.Label();
            this.lblKpi4Val = new System.Windows.Forms.Label();
            this.pnlKpi5 = new System.Windows.Forms.Panel();
            this.lblKpi5Title = new System.Windows.Forms.Label();
            this.lblKpi5Val = new System.Windows.Forms.Label();
            // Status badge + progress bar
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlProgressTrack = new System.Windows.Forms.Panel();
            this.pnlProgressFill = new System.Windows.Forms.Panel();
            this.lblProgressPct = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();

            // ── Body: split into left (timeline) + right (items) ─────────────
            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblTimelineTitle = new System.Windows.Forms.Label();
            this.pnlTimeline = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblItemsTitle = new System.Windows.Forms.Label();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colItemProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colItemTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();

            this.pnlActionBar = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();

            this.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();

            // ══════════════════════════════════════════════════════════════════
            //  FORM
            // ══════════════════════════════════════════════════════════════════
            this.Text = "Invoice Payment Flow";
            this.Size = new System.Drawing.Size(1100, 780);
            this.MinimumSize = new System.Drawing.Size(920, 640);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;

            // ══════════════════════════════════════════════════════════════════
            //  HEADER  (colour set at runtime by status)
            // ══════════════════════════════════════════════════════════════════
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 68;
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.pnlHeader.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblSubtitle });

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(18, 12);
            this.lblTitle.Text = "Payment Flow";

            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(187, 222, 251);
            this.lblSubtitle.Location = new System.Drawing.Point(21, 44);
            this.lblSubtitle.Text = "Loading…";

            // ══════════════════════════════════════════════════════════════════
            //  SUMMARY CARD  (KPI boxes + progress bar)
            // ══════════════════════════════════════════════════════════════════
            this.pnlSummaryCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSummaryCard.Height = 110;
            this.pnlSummaryCard.BackColor = System.Drawing.Color.White;
            this.pnlSummaryCard.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);

            // Helper: build one KPI box
            void MakeKpi(System.Windows.Forms.Panel box,
                         System.Windows.Forms.Label title,
                         System.Windows.Forms.Label val,
                         int x, string titleText)
            {
                box.BackColor = System.Drawing.Color.FromArgb(248, 249, 252);
                box.Location = new System.Drawing.Point(x, 10);
                box.Size = new System.Drawing.Size(148, 56);
                box.Padding = new System.Windows.Forms.Padding(8, 6, 8, 4);

                title.Text = titleText;
                title.Font = new System.Drawing.Font("Segoe UI", 7.5F, System.Drawing.FontStyle.Bold);
                title.ForeColor = System.Drawing.Color.FromArgb(120, 144, 156);
                title.AutoSize = false;
                title.Size = new System.Drawing.Size(132, 16);
                title.Location = new System.Drawing.Point(8, 6);

                val.Text = "—";
                val.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
                val.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
                val.AutoSize = false;
                val.Size = new System.Drawing.Size(132, 26);
                val.Location = new System.Drawing.Point(8, 24);

                box.Controls.AddRange(new System.Windows.Forms.Control[] { title, val });
                this.pnlSummaryCard.Controls.Add(box);
            }

            MakeKpi(pnlKpi1, lblKpi1Title, lblKpi1Val, 14, "Total Bill");
            MakeKpi(pnlKpi2, lblKpi2Title, lblKpi2Val, 172, "Discount");
            MakeKpi(pnlKpi3, lblKpi3Title, lblKpi3Val, 330, "Net Amount");
            MakeKpi(pnlKpi4, lblKpi4Title, lblKpi4Val, 488, "Total Paid");
            MakeKpi(pnlKpi5, lblKpi5Title, lblKpi5Val, 646, "Balance Due");

            // Status badge (right side of card)
            this.lblStatus.AutoSize = false;
            this.lblStatus.Size = new System.Drawing.Size(200, 32);
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblStatus.Location = new System.Drawing.Point(860, 12);
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatus.Text = "";
            this.pnlSummaryCard.Controls.Add(this.lblStatus);

            // Progress track (bottom strip)
            this.pnlProgressTrack.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.pnlProgressTrack.BackColor = System.Drawing.Color.FromArgb(225, 230, 235);
            this.pnlProgressTrack.Location = new System.Drawing.Point(14, 76);
            this.pnlProgressTrack.Size = new System.Drawing.Size(900, 14);
            this.pnlProgressTrack.Controls.Add(this.pnlProgressFill);

            this.pnlProgressFill.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);
            this.pnlProgressFill.Location = new System.Drawing.Point(0, 0);
            this.pnlProgressFill.Size = new System.Drawing.Size(0, 14);
            // Width is set at runtime by ApplyProgressBar() — do NOT dock

            this.pnlSummaryCard.Controls.Add(this.pnlProgressTrack);

            this.lblProgressPct.AutoSize = true;
            this.lblProgressPct.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblProgressPct.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblProgressPct.Location = new System.Drawing.Point(14, 92);
            this.lblProgressPct.Text = "0% paid";
            this.pnlSummaryCard.Controls.Add(this.lblProgressPct);

            // Notes label (hidden until data loaded)
            this.lblNotes.AutoSize = false;
            this.lblNotes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            this.lblNotes.Size = new System.Drawing.Size(400, 20);
            this.lblNotes.Location = new System.Drawing.Point(660, 90);
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblNotes.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblNotes.Visible = false;
            this.pnlSummaryCard.Controls.Add(this.lblNotes);

            // ══════════════════════════════════════════════════════════════════
            //  BODY PANEL  (fills remaining space)
            // ══════════════════════════════════════════════════════════════════
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(240, 244, 248);

            // ── LEFT PANEL: Payment Timeline (40% width) ──────────────────────
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Width = 420;
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(0, 0, 1, 0); // right border

            this.lblTimelineTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTimelineTitle.Height = 38;
            this.lblTimelineTitle.BackColor = System.Drawing.Color.FromArgb(33, 33, 33);
            this.lblTimelineTitle.ForeColor = System.Drawing.Color.White;
            this.lblTimelineTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimelineTitle.Text = "   💳  Payment Timeline";
            this.lblTimelineTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.pnlTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimeline.AutoScroll = true;
            this.pnlTimeline.BackColor = System.Drawing.Color.White;
            this.pnlTimeline.Padding = new System.Windows.Forms.Padding(0, 4, 0, 4);

            this.pnlLeft.Controls.Add(this.pnlTimeline);
            this.pnlLeft.Controls.Add(this.lblTimelineTitle);

            // ── RIGHT PANEL: Items Grid (fills rest) ──────────────────────────
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);

            this.lblItemsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblItemsTitle.Height = 38;
            this.lblItemsTitle.BackColor = System.Drawing.Color.FromArgb(55, 71, 79);
            this.lblItemsTitle.ForeColor = System.Drawing.Color.White;
            this.lblItemsTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblItemsTitle.Text = "   📦  Purchased Items";
            this.lblItemsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // Items grid styles
            hdrStyle.BackColor = System.Drawing.Color.FromArgb(55, 71, 79);
            hdrStyle.ForeColor = System.Drawing.Color.White;
            hdrStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            hdrStyle.SelectionBackColor = System.Drawing.Color.FromArgb(55, 71, 79);
            hdrStyle.SelectionForeColor = System.Drawing.Color.White;

            cellStyle.BackColor = System.Drawing.Color.White;
            cellStyle.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
            cellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            cellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(207, 216, 220);
            cellStyle.SelectionForeColor = System.Drawing.Color.FromArgb(33, 33, 33);

            altStyle.BackColor = System.Drawing.Color.FromArgb(245, 247, 248);

            numStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            numStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            numStyle.Format = "N2";

            boldStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            boldStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            boldStyle.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
            boldStyle.Format = "N2";

            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.ReadOnly = true;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItems.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvItems.GridColor = System.Drawing.Color.FromArgb(236, 239, 241);
            this.dgvItems.ColumnHeadersDefaultCellStyle = hdrStyle;
            this.dgvItems.ColumnHeadersHeight = 40;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvItems.DefaultCellStyle = cellStyle;
            this.dgvItems.AlternatingRowsDefaultCellStyle = altStyle;
            this.dgvItems.EnableHeadersVisualStyles = false;
            this.dgvItems.MultiSelect = false;
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.RowTemplate.Height = 38;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            this.colItemProduct.Name = "colItemProduct";
            this.colItemProduct.HeaderText = "Product";
            this.colItemProduct.FillWeight = 38F;
            this.colItemProduct.ReadOnly = true;

            this.colItemUnit.Name = "colItemUnit";
            this.colItemUnit.HeaderText = "Unit";
            this.colItemUnit.FillWeight = 14F;
            this.colItemUnit.ReadOnly = true;
            this.colItemUnit.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            this.colItemQty.Name = "colItemQty";
            this.colItemQty.HeaderText = "Qty";
            this.colItemQty.FillWeight = 12F;
            this.colItemQty.ReadOnly = true;
            this.colItemQty.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colItemQty.DefaultCellStyle.Format = "N2";

            this.colItemPrice.Name = "colItemPrice";
            this.colItemPrice.HeaderText = "Unit Price";
            this.colItemPrice.FillWeight = 18F;
            this.colItemPrice.ReadOnly = true;
            this.colItemPrice.DefaultCellStyle = numStyle;

            this.colItemTotal.Name = "colItemTotal";
            this.colItemTotal.HeaderText = "Total (Rs.)";
            this.colItemTotal.FillWeight = 18F;
            this.colItemTotal.ReadOnly = true;
            this.colItemTotal.DefaultCellStyle = boldStyle;

            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colItemProduct, this.colItemUnit, this.colItemQty,
                this.colItemPrice, this.colItemTotal });

            this.pnlRight.Controls.Add(this.dgvItems);
            this.pnlRight.Controls.Add(this.lblItemsTitle);

            // Right goes into body first (Fill), then Left (Left docks on top of it)
            this.pnlBody.Controls.Add(this.pnlRight);
            this.pnlBody.Controls.Add(this.pnlLeft);

            // ══════════════════════════════════════════════════════════════════
            //  ACTION BAR
            // ══════════════════════════════════════════════════════════════════
            this.pnlActionBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionBar.Height = 48;
            this.pnlActionBar.BackColor = System.Drawing.Color.White;
            this.pnlActionBar.Controls.Add(this.btnClose);

            this.btnClose.Text = "Close";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(55, 71, 79);
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(236, 239, 241);
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Size = new System.Drawing.Size(120, 34);
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.Location = new System.Drawing.Point(960, 7);
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);

            // ── Add to form ───────────────────────────────────────────────────
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.pnlBody,
                this.pnlActionBar,
                this.pnlSummaryCard,
                this.pnlHeader
            });

            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
        }

        // ── Fields ────────────────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader, pnlSummaryCard, pnlBody;
        private System.Windows.Forms.Panel pnlLeft, pnlRight, pnlActionBar;
        private System.Windows.Forms.Label lblTitle, lblSubtitle;

        // KPI boxes
        private System.Windows.Forms.Panel pnlKpi1, pnlKpi2, pnlKpi3, pnlKpi4, pnlKpi5;
        private System.Windows.Forms.Label lblKpi1Title, lblKpi1Val;
        private System.Windows.Forms.Label lblKpi2Title, lblKpi2Val;
        private System.Windows.Forms.Label lblKpi3Title, lblKpi3Val;
        private System.Windows.Forms.Label lblKpi4Title, lblKpi4Val;
        private System.Windows.Forms.Label lblKpi5Title, lblKpi5Val;

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlProgressTrack, pnlProgressFill;
        private System.Windows.Forms.Label lblProgressPct, lblNotes;

        // Timeline
        private System.Windows.Forms.Label lblTimelineTitle;
        private System.Windows.Forms.Panel pnlTimeline;

        // Items grid
        private System.Windows.Forms.Label lblItemsTitle;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn
            colItemProduct, colItemUnit, colItemQty, colItemPrice, colItemTotal;

        // Action bar
        private System.Windows.Forms.Button btnClose;
    }
}