using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Reports Hub — Launches all 15 reports.
    /// Two sections: Purchase Reports (10) + Sales / P&amp;L Reports (5).
    /// Open via:  new ReportsMenuForm().Show();
    /// </summary>
    public partial class FormReportsMenu : Form
    {
        public FormReportsMenu()
        {
            InitializeComponent();
            BuildTiles();
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
        }

        // ── Tile definition ───────────────────────────────────────────────────
        private struct TileInfo
        {
            public string Icon, Title, Sub;
            public Color BgColor;
            public Action OnClick;
        }

        private void BuildTiles()
        {
            var tiles = new TileInfo[]
            {
                // ── Section A: Purchase Reports ───────────────────────────────
                new TileInfo { Icon="📋", Title="Purchase Summary",         Sub="Daily / Weekly / Monthly",          BgColor=Color.FromArgb(21, 101,192), OnClick=()=>new PurchaseSummaryReportForm().Show(this) },
                new TileInfo { Icon="🏢", Title="By Supplier",              Sub="Ranked by net spend",               BgColor=Color.FromArgb(0,  131,143), OnClick=()=>new PurchaseBySupplierReportForm().Show(this) },
                new TileInfo { Icon="⏳", Title="Supplier Aging",           Sub="Outstanding by age bucket",         BgColor=Color.FromArgb(198, 40, 40), OnClick=()=>new AgingReportForm().Show(this) },
                new TileInfo { Icon="📒", Title="Supplier Ledger",          Sub="Full transaction history",          BgColor=Color.FromArgb(46, 125, 50), OnClick=()=>new SupplierLedgerReportForm().Show(this) },
                new TileInfo { Icon="💳", Title="Payment Methods",          Sub="Cash / Bank / Cheque / Online",     BgColor=Color.FromArgb(21, 101,192), OnClick=()=>new PaymentMethodReportForm().Show(this) },
                new TileInfo { Icon="📊", Title="Supplier Performance",     Sub="Overview of all suppliers",         BgColor=Color.FromArgb(106, 27,154), OnClick=()=>new SupplierPerformanceReportForm().Show(this) },
                new TileInfo { Icon="📅", Title="Monthly Cash Flow",        Sub="Obligations vs payments",           BgColor=Color.FromArgb(0,  105, 92), OnClick=()=>new MonthlyCashFlowReportForm().Show(this) },
                new TileInfo { Icon="🚨", Title="Top Unpaid",               Sub="Highest outstanding balances",      BgColor=Color.FromArgb(198, 40, 40), OnClick=()=>new TopUnpaidReportForm().Show(this) },
                new TileInfo { Icon="📦", Title="Product Purchase History", Sub="What was bought & at what price",   BgColor=Color.FromArgb(109, 76, 65), OnClick=()=>new ProductPurchaseHistoryReportForm().Show(this) },
                new TileInfo { Icon="📈", Title="Price Variance",           Sub="Supplier price creep detector",     BgColor=Color.FromArgb(198, 40, 40), OnClick=()=>new PriceVarianceReportForm().Show(this) },
                // ── Section B: Sales / P&L Reports ───────────────────────────
                new TileInfo { Icon="🛒", Title="Sales Summary",            Sub="Revenue by period",                 BgColor=Color.FromArgb(27, 94,  32), OnClick=()=>new SalesSummaryReportForm().Show(this) },
                new TileInfo { Icon="💰", Title="Profit & Loss",            Sub="Revenue vs Cost, Gross Margin",     BgColor=Color.FromArgb(74, 20, 140), OnClick=()=>new ProfitLossReportForm().Show(this) },
                new TileInfo { Icon="👥", Title="Top Customers",            Sub="Ranked by total spend",             BgColor=Color.FromArgb(230, 81,  0), OnClick=()=>new TopCustomersReportForm().Show(this) },
                new TileInfo { Icon="🏆", Title="Top Selling Products",     Sub="By revenue or quantity",            BgColor=Color.FromArgb(0,  105, 92), OnClick=()=>new TopSellingProductsReportForm().Show(this) },
                new TileInfo { Icon="⚖️", Title="Sales vs Purchases",       Sub="Surplus / Deficit per month",       BgColor=Color.FromArgb(21, 101,192), OnClick=()=>new SalesPurchaseComparisonReportForm().Show(this) },
            };

            // Layout constants
            const int cols = 5;
            const int tileW = 186;
            const int tileH = 110;
            const int gapX = 14;
            const int gapY = 14;
            const int startX = 16;
            const int startY = 16;
            const int secGap = 38; // extra gap before Section B row

            pnlTiles.Controls.Clear();

            for (int i = 0; i < tiles.Length; i++)
            {
                int row = i / cols;
                int col = i % cols;

                // Add section header label before tile row 0 and row 2
                if (i == 0 || i == 10)
                {
                    int labelY = startY + row * (tileH + gapY) + (row > 0 ? secGap : 0);
                    if (i == 10) labelY += secGap / 2;

                    var lbl = new Label
                    {
                        Text = i == 0 ? "🏗  PURCHASE REPORTS" : "💹  SALES & PROFIT / LOSS REPORTS",
                        Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                        ForeColor = Color.FromArgb(80, 80, 80),
                        AutoSize = true,
                        Location = new Point(startX, labelY - 20)
                    };
                    pnlTiles.Controls.Add(lbl);
                }

                int extraY = row >= 2 ? secGap : 0; // push rows 2+ down for section label
                int x = startX + col * (tileW + gapX);
                int y = startY + row * (tileH + gapY) + extraY + 4;

                var t = tiles[i];
                var panel = new Panel
                {
                    Size = new Size(tileW, tileH),
                    Location = new Point(x, y),
                    BackColor = t.BgColor,
                    Cursor = Cursors.Hand
                };

                var icon = new Label
                {
                    Text = t.Icon,
                    Font = new Font("Segoe UI", 22f),
                    ForeColor = Color.FromArgb(255, 255, 255, 120),
                    AutoSize = false,
                    Size = new Size(50, 50),
                    Location = new Point(tileW - 56, 4),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                var title = new Label
                {
                    Text = t.Title,
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    ForeColor = Color.White,
                    AutoSize = false,
                    Size = new Size(tileW - 8, 38),
                    Location = new Point(6, tileH - 56),
                    TextAlign = ContentAlignment.BottomLeft
                };

                var sub = new Label
                {
                    Text = t.Sub,
                    Font = new Font("Segoe UI", 7.5f),
                    ForeColor = Color.FromArgb(220, 255, 255, 255),
                    AutoSize = false,
                    Size = new Size(tileW - 8, 20),
                    Location = new Point(6, tileH - 20),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                // Number badge
                var badge = new Label
                {
                    Text = $"{i + 1:D2}",
                    Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(160, 255, 255, 255),
                    AutoSize = false,
                    Size = new Size(24, 20),
                    Location = new Point(6, 6),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                panel.Controls.AddRange(new Control[] { icon, title, sub, badge });

                // Hover effect
                Color normal = t.BgColor;
                Color dark = ControlPaint.Dark(normal, 0.15f);
                var action = t.OnClick;

                EventHandler enterH = (s, e2) => { panel.BackColor = dark; };
                EventHandler leaveH = (s, e2) => { panel.BackColor = normal; };
                EventHandler clickH = (s, e2) => action?.Invoke();

                panel.MouseEnter += enterH; panel.MouseLeave += leaveH; panel.Click += clickH;
                foreach (Control c in panel.Controls)
                {
                    c.MouseEnter += enterH; c.MouseLeave += leaveH; c.Click += clickH;
                }

                pnlTiles.Controls.Add(panel);
            }
        }
    }
}