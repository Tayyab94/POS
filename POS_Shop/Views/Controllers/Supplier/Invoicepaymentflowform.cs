using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    /// <summary>
    /// Invoice Payment Flow
    /// ─────────────────────────────────────────────────────────────────────
    /// Shows the complete payment history for ONE purchase invoice:
    ///
    ///   TOP PANEL  — Invoice summary card (amounts, status, balance)
    ///   TIMELINE   — Every payment ever made against this invoice,
    ///                chronological, with date / amount / method / ref / notes
    ///   ITEMS GRID — Line items that were purchased on this invoice
    ///
    /// Open via:  new InvoicePaymentFlowForm(purchaseId).ShowDialog();
    /// </summary>
    public partial class InvoicePaymentFlowForm : Form
    {
        private readonly int _purchaseId;
        private decimal _progressPct = 0m;
        private Color _progressColor = Color.FromArgb(46, 125, 50);

        public InvoicePaymentFlowForm(int purchaseId)
        {
            _purchaseId = purchaseId;
            InitializeComponent();

            // Re-apply fill width whenever the track panel resizes
            // (fires on first layout AND on window resize)
            pnlProgressTrack.Resize += (s, e) => ApplyProgressBar();

            LoadData();
        }

        /// <summary>
        /// Sets pnlProgressFill.Width as a fraction of pnlProgressTrack.Width.
        /// Must NOT be called until the track panel has a real width (post-layout).
        /// </summary>
        private void ApplyProgressBar()
        {
            if (pnlProgressTrack.Width <= 0) return;
            pnlProgressFill.Dock = DockStyle.None;
            pnlProgressFill.Location = new Point(0, 0);
            pnlProgressFill.Height = pnlProgressTrack.Height;
            pnlProgressFill.Width = (int)(pnlProgressTrack.Width
                                        * Math.Min(_progressPct / 100m, 1m));
            pnlProgressFill.BackColor = _progressColor;
        }

        // ══════════════════════════════════════════════════════════════════════
        //  LOAD DATA
        // ══════════════════════════════════════════════════════════════════════

        private void LoadData()
        {
            try
            {
                using (var db = new POSDbContext())
                {
                    // ── 1. Load purchase with supplier ────────────────────────
                    var purchase = db.Purchases
                        .Include("Supplier")
                        .AsNoTracking()
                        .FirstOrDefault(p => p.Id == _purchaseId);

                    if (purchase == null)
                    {
                        MessageBox.Show("Invoice not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                        return;
                    }

                    // ── 2. Load payment details (each allocation row)
                    //       joined with its parent SupplierPayment header
                    var payments = db.SupplierPaymentDetails
                        .Include("SupplierPayment")
                        .AsNoTracking()
                        .Where(d => d.PurchaseId == _purchaseId
                                 && !d.SupplierPayment.IsDeleted)
                        .OrderBy(d => d.SupplierPayment.PaymentDate)
                        .ThenBy(d => d.Id)
                        .ToList();

                    // ── 3. Load purchase items ─────────────────────────────────
                    var items = db.PurchaseItems
                        .Include("Product")
                        .AsNoTracking()
                        .Where(i => i.PurchaseId == _purchaseId && !i.IsDeleted)
                        .ToList();

                    // ── 4. Render ──────────────────────────────────────────────
                    RenderHeader(purchase);
                    RenderSummaryCard(purchase);
                    RenderPaymentTimeline(payments, purchase.NetAmount);
                    RenderItemsGrid(items);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  RENDER HEADER
        // ══════════════════════════════════════════════════════════════════════

        private void RenderHeader(Purchase p)
        {
            string supplierName = p.Supplier != null
                ? $"{p.Supplier.SupplierName}  —  {p.Supplier.ShopName}"
                : $"Supplier #{p.SupplierId}";

            lblTitle.Text = $"Payment Flow  —  {p.InvoiceNumber}";
            lblSubtitle.Text = $"{supplierName}  ·  Purchased: {p.PurchaseDate:dd MMM yyyy}";

            // Header accent by status
            Color accent = p.PaymentStatus == PurchasePaymentStatus.Paid ? Color.FromArgb(46, 125, 50)
                         : p.PaymentStatus == PurchasePaymentStatus.PartiallyPaid ? Color.FromArgb(230, 81, 0)
                         : Color.FromArgb(198, 40, 40);

            pnlHeader.BackColor = accent;
            lblSubtitle.ForeColor = p.PaymentStatus == PurchasePaymentStatus.Paid
                ? Color.FromArgb(165, 214, 167)
                : Color.FromArgb(255, 224, 178);

            this.Text = $"Payment Flow — {p.InvoiceNumber}";
        }

        // ══════════════════════════════════════════════════════════════════════
        //  RENDER SUMMARY CARD
        // ══════════════════════════════════════════════════════════════════════

        private void RenderSummaryCard(Purchase p)
        {
            decimal pct = p.NetAmount > 0 ? (p.TotalPaid / p.NetAmount) * 100m : 0m;

            string statusText = p.PaymentStatus == PurchasePaymentStatus.Paid ? "✔  Fully Paid"
                               : p.PaymentStatus == PurchasePaymentStatus.PartiallyPaid ? "◑  Partially Paid"
                               : "○  Pending";

            // Populate the 5 KPI boxes
            SetKpi(lblKpi1Title, lblKpi1Val, "Total Bill", $"Rs. {p.TotalAmount:N2}", Color.FromArgb(33, 33, 33));
            SetKpi(lblKpi2Title, lblKpi2Val, "Discount", $"Rs. {p.Discount:N2}", Color.FromArgb(198, 40, 40));
            SetKpi(lblKpi3Title, lblKpi3Val, "Net Amount", $"Rs. {p.NetAmount:N2}", Color.FromArgb(21, 101, 192));
            SetKpi(lblKpi4Title, lblKpi4Val, "Total Paid", $"Rs. {p.TotalPaid:N2}", Color.FromArgb(46, 125, 50));
            SetKpi(lblKpi5Title, lblKpi5Val, "Balance Due", $"Rs. {p.Balance:N2}",
                p.Balance > 0 ? Color.FromArgb(198, 40, 40) : Color.FromArgb(46, 125, 50));

            // Status badge
            lblStatus.Text = statusText;
            lblStatus.ForeColor = p.PaymentStatus == PurchasePaymentStatus.Paid ? Color.FromArgb(46, 125, 50)
                                 : p.PaymentStatus == PurchasePaymentStatus.PartiallyPaid ? Color.FromArgb(230, 81, 0)
                                 : Color.FromArgb(198, 40, 40);

            // Progress bar — store values and apply AFTER layout so track width is real
            _progressPct = pct;
            _progressColor = p.PaymentStatus == PurchasePaymentStatus.Paid
                ? Color.FromArgb(46, 125, 50)
                : Color.FromArgb(230, 81, 0);
            lblProgressPct.Text = $"{pct:N1}% paid";
            ApplyProgressBar();   // also fires again via pnlProgressTrack.Resize

            // Notes
            if (!string.IsNullOrWhiteSpace(p.Notes))
            {
                lblNotes.Text = $"📝  {p.Notes}";
                lblNotes.Visible = true;
            }
        }

        private static void SetKpi(Label title, Label val, string t, string v, Color valColor)
        {
            title.Text = t;
            val.Text = v;
            val.ForeColor = valColor;
        }

        // ══════════════════════════════════════════════════════════════════════
        //  RENDER PAYMENT TIMELINE
        // ══════════════════════════════════════════════════════════════════════

        private void RenderPaymentTimeline(List<SupplierPaymentDetail> payments, decimal netAmount)
        {
            pnlTimeline.Controls.Clear();

            if (payments.Count == 0)
            {
                var lbl = new Label
                {
                    Text = "No payments recorded yet for this invoice.",
                    Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(120, 144, 156),
                    AutoSize = false,
                    Dock = DockStyle.Top,
                    Height = 50,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlTimeline.Controls.Add(lbl);
                return;
            }

            // Build timeline entries bottom-to-top (Controls.Add inserts at top)
            decimal running = 0;
            var entries = new List<Control>();

            for (int i = 0; i < payments.Count; i++)
            {
                var d = payments[i];
                var hdr = d.SupplierPayment;
                running += d.AmountAllocated;

                bool isLast = i == payments.Count - 1;
                string method = hdr.PaymentMethod.ToString().Replace("Transfer", " Transfer");

                var entry = BuildTimelineEntry(
                    index: i + 1,
                    date: hdr.PaymentDate,
                    payNo: hdr.PaymentNumber,
                    amount: d.AmountAllocated,
                    runningTotal: running,
                    netAmount: netAmount,
                    method: method,
                    reference: hdr.TransactionReference,
                    notes: hdr.Notes,
                    isLast: isLast);

                entries.Add(entry);
            }

            // Add in reverse so newest is at top
            for (int i = entries.Count - 1; i >= 0; i--)
                pnlTimeline.Controls.Add(entries[i]);
        }

        private Control BuildTimelineEntry(
            int index, DateTime date, string payNo,
            decimal amount, decimal runningTotal, decimal netAmount,
            string method, string reference, string notes, bool isLast)
        {
            bool isFinalPayment = isLast && runningTotal >= netAmount;
            decimal pct = netAmount > 0 ? (runningTotal / netAmount) * 100m : 100m;

            var outer = new Panel
            {
                Dock = DockStyle.Top,
                Height = string.IsNullOrWhiteSpace(notes) && string.IsNullOrWhiteSpace(reference) ? 82 : 100,
                BackColor = Color.White,
                Padding = new Padding(0, 4, 0, 4)
            };

            // Left timeline bar (coloured dot + vertical line)
            var bar = new Panel
            {
                Width = 48,
                Dock = DockStyle.Left,
                BackColor = Color.White
            };

            // Dot
            var dot = new Label
            {
                Size = new Size(20, 20),
                Location = new Point(14, 14),
                BackColor = isFinalPayment ? Color.FromArgb(46, 125, 50) : Color.FromArgb(21, 101, 192),
                Text = ""
            };
            // Make it circular via paint
            dot.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(dot.BackColor),
                    0, 0, dot.Width - 1, dot.Height - 1);
                e.Graphics.DrawEllipse(Pens.White, 0, 0, dot.Width - 1, dot.Height - 1);
            };
            dot.BackColor = isFinalPayment ? Color.FromArgb(46, 125, 50) : Color.FromArgb(21, 101, 192);

            // Vertical connector line (not on last entry)
            if (!isLast)
            {
                var line = new Label
                {
                    Width = 2,
                    BackColor = Color.FromArgb(207, 216, 220),
                    Location = new Point(22, 34),
                    Height = outer.Height - 34
                };
                bar.Controls.Add(line);
            }
            bar.Controls.Add(dot);

            // Right content area
            var content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(8, 8, 16, 8)
            };

            // Row 1: Date + Payment No + Amount
            var lblDate = new Label
            {
                Text = date.ToString("dd MMM yyyy"),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33),
                AutoSize = true,
                Location = new Point(8, 8)
            };
            var lblPayNo = new Label
            {
                Text = $"({payNo})",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(120, 144, 156),
                AutoSize = true,
                Location = new Point(165, 10)
            };
            var lblAmt = new Label
            {
                Text = $"Rs. {amount:N2}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 125, 50),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            lblAmt.Location = new Point(content.Width - 150, 6);

            // Row 2: Method badge + running total
            var lblMethod = new Label
            {
                Text = $"  {method}  ",
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(21, 101, 192),
                BackColor = Color.FromArgb(227, 242, 253),
                AutoSize = true,
                Location = new Point(8, 30),
                Padding = new Padding(3, 2, 3, 2)
            };
            var lblRunning = new Label
            {
                Text = $"Cumulative: Rs. {runningTotal:N2}  ({pct:N0}% of invoice)",
                Font = new Font("Segoe UI", 8.5F),
                ForeColor = Color.FromArgb(90, 90, 90),
                AutoSize = true,
                Location = new Point(8, 52)
            };

            content.Controls.AddRange(new Control[] { lblDate, lblPayNo, lblAmt, lblMethod, lblRunning });

            // Optional: Reference + Notes
            int nextY = 68;
            if (!string.IsNullOrWhiteSpace(reference))
            {
                var lblRef = new Label
                {
                    Text = $"Ref: {reference}",
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(120, 144, 156),
                    AutoSize = true,
                    Location = new Point(8, nextY)
                };
                content.Controls.Add(lblRef);
                nextY += 16;
            }
            if (!string.IsNullOrWhiteSpace(notes))
            {
                var lblN = new Label
                {
                    Text = $"📝 {notes}",
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Italic),
                    ForeColor = Color.FromArgb(100, 100, 100),
                    AutoSize = true,
                    Location = new Point(8, nextY)
                };
                content.Controls.Add(lblN);
            }

            // Separator line
            var sep = new Label
            {
                Dock = DockStyle.Bottom,
                Height = 1,
                BackColor = Color.FromArgb(236, 239, 241)
            };

            outer.Controls.Add(content);
            outer.Controls.Add(bar);
            outer.Controls.Add(sep);
            return outer;
        }

        // ══════════════════════════════════════════════════════════════════════
        //  RENDER ITEMS GRID
        // ══════════════════════════════════════════════════════════════════════

        private void RenderItemsGrid(List<PurchaseItem> items)
        {
            dgvItems.Rows.Clear();

            decimal total = 0;
            foreach (var item in items)
            {
                string productName = item.Product?.ProductEnglishName ?? $"Product #{item.ProductId}";
                string unit = item.ProductUnit?.Name ?? "-";

                int idx = dgvItems.Rows.Add();
                var row = dgvItems.Rows[idx];
                row.Cells["colItemProduct"].Value = productName;
                row.Cells["colItemUnit"].Value = unit;
                row.Cells["colItemQty"].Value = item.Quantity;
                row.Cells["colItemPrice"].Value = item.PurchasePrice;
                row.Cells["colItemTotal"].Value = item.TotalPrice;
                total += item.TotalPrice;
            }

            // Total row
            if (items.Count > 0)
            {
                var tr = dgvItems.Rows[dgvItems.Rows.Add()];
                tr.Cells["colItemProduct"].Value = $"TOTAL  ({items.Count} items)";
                tr.Cells["colItemTotal"].Value = total;
                foreach (DataGridViewCell c in tr.Cells)
                {
                    c.Style.BackColor = Color.FromArgb(21, 101, 192);
                    c.Style.ForeColor = Color.White;
                    c.Style.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                }
            }
        }

        // ── Close button ───────────────────────────────────────────────────────
        private void BtnClose_Click(object sender, EventArgs e) => Close();
    }
}