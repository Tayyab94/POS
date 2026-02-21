using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Reports Hub — Central launcher with 10 colour-coded report tiles.
    /// Open via:  new ReportsMenuForm().Show();
    /// </summary>
    public partial class ReportsMenuForm : Form
    {
        public ReportsMenuForm()
        {
            InitializeComponent();
            InitializeTiles();   // Custom tile setup (after designer init)
            WireButtons();       // Event handlers
        }

        private void InitializeTiles()
        {
            int tw = 440, th = 92, gx = 20, gy = 14;

            MakeTile(this.btnR1, "01",
                "Purchase Summary by Period",
                "Daily / Monthly / Weekly spend breakdown",
                Color.FromArgb(21, 101, 192),
                new Point(0, 0), tw, th);

            MakeTile(this.btnR2, "02",
                "Purchase by Supplier  —  Ranked",
                "Which suppliers you spend the most with",
                Color.FromArgb(0, 131, 143),
                new Point(tw + gx, 0), tw, th);

            MakeTile(this.btnR3, "03",
                "⭐  Supplier Aging Report",
                "0–30  ·  31–60  ·  61–90  ·  90+ days overdue",
                Color.FromArgb(198, 40, 40),
                new Point(0, th + gy), tw, th);

            MakeTile(this.btnR4, "04",
                "Supplier Ledger",
                "Invoices + Payments + Running balance per supplier",
                Color.FromArgb(46, 125, 50),
                new Point(tw + gx, th + gy), tw, th);

            MakeTile(this.btnR5, "05",
                "Payment Method Analysis",
                "Cash  ·  Bank  ·  Cheque  ·  Online — share & totals",
                Color.FromArgb(21, 101, 192),
                new Point(0, (th + gy) * 2), tw, th);

            MakeTile(this.btnR6, "06",
                "Supplier Performance Summary",
                "One-page overview of all supplier relationships",
                Color.FromArgb(106, 27, 154),
                new Point(tw + gx, (th + gy) * 2), tw, th);

            MakeTile(this.btnR7, "07",
                "Monthly Cash Flow",
                "Obligations vs Payments made — are you keeping up?",
                Color.FromArgb(0, 105, 92),
                new Point(0, (th + gy) * 3), tw, th);

            MakeTile(this.btnR8, "08",
                "⭐  Top Unpaid Suppliers",
                "Who you owe the most — real-time ranked list",
                Color.FromArgb(183, 28, 28),
                new Point(tw + gx, (th + gy) * 3), tw, th);

            MakeTile(this.btnR9, "09",
                "Product Purchase History",
                "What you bought  ·  How often  ·  At what prices",
                Color.FromArgb(109, 76, 65),
                new Point(0, (th + gy) * 4), tw, th);

            MakeTile(this.btnR10, "10",
                "Purchase Price Variance",
                "Same product — different prices — catches price creep",
                Color.FromArgb(198, 40, 40),
                new Point(tw + gx, (th + gy) * 4), tw, th);

            this.pnlTiles.Controls.AddRange(new Control[] {
                this.btnR1, this.btnR2, this.btnR3, this.btnR4, this.btnR5,
                this.btnR6, this.btnR7, this.btnR8, this.btnR9, this.btnR10 });
        }

        private void WireButtons()
        {
            btnR1.Click += (s, e) => OpenReport(new PurchaseSummaryReportForm());
            btnR2.Click += (s, e) => OpenReport(new PurchaseBySupplierReportForm());
            btnR3.Click += (s, e) => OpenReport(new AgingReportForm());
            btnR4.Click += (s, e) => OpenReport(new SupplierLedgerReportForm());
            btnR5.Click += (s, e) => OpenReport(new PaymentMethodReportForm());
            btnR6.Click += (s, e) => OpenReport(new SupplierPerformanceReportForm());
            btnR7.Click += (s, e) => OpenReport(new MonthlyCashFlowReportForm());
            btnR8.Click += (s, e) => OpenReport(new TopUnpaidReportForm());
            btnR9.Click += (s, e) => OpenReport(new ProductPurchaseHistoryReportForm());
            btnR10.Click += (s, e) => OpenReport(new PriceVarianceReportForm());
            btnClose.Click += (s, e) => Close();

            this.KeyPreview = true;
            this.KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
        }

        private void OpenReport(Form reportForm)
        {
            reportForm.Owner = this;
            reportForm.Show();
            // Optional: minimize owner to keep focus on report
            // this.WindowState = FormWindowState.Minimized;
        }

        private static void MakeTile(Button b, string num, string title, string subtitle, Color color, Point loc, int w, int h)
        {
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Max(0, color.R - 20),
                Math.Max(0, color.G - 20),
                Math.Max(0, color.B - 20));
            b.BackColor = color;
            b.ForeColor = Color.White;
            b.TextAlign = ContentAlignment.MiddleLeft;
            b.Cursor = Cursors.Hand;
            b.Size = new Size(w, h);
            b.Location = loc;
            b.Font = new Font("Segoe UI", 9f);
            b.UseVisualStyleBackColor = false; // Important for custom colors to render

            b.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(color);

                // Number badge (circle)
                var badgeRect = new Rectangle(12, 12, 44, 44);
                using (var badgeBrush = new SolidBrush(Color.FromArgb(60, Color.White)))
                    g.FillEllipse(badgeBrush, badgeRect);
                using (var nf = new Font("Segoe UI", 11f, FontStyle.Bold))
                    g.DrawString(num, nf, Brushes.White,
                        badgeRect, new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        });

                // Title
                using (var tf = new Font("Segoe UI", 11.5f, FontStyle.Bold))
                    g.DrawString(title, tf, Brushes.White,
                        new RectangleF(68, 14, w - 76, 34));

                // Subtitle
                using (var sf = new Font("Segoe UI", 9f))
                using (var sb = new SolidBrush(Color.FromArgb(200, Color.White)))
                    g.DrawString(subtitle, sf, sb,
                        new RectangleF(68, 50, w - 76, 30));

                // Right arrow
                using (var af = new Font("Segoe UI", 14f))
                using (var ab = new SolidBrush(Color.FromArgb(120, Color.White)))
                    g.DrawString("›", af, ab, w - 28, 30);
            };

            // Optional: subtle hover elevation effect
            b.MouseEnter += (s, e) => b.Invalidate();
            b.MouseLeave += (s, e) => b.Invalidate();
        }
    }
}