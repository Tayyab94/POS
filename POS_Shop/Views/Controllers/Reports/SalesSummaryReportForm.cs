using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 11 — Sales Summary
    /// Total orders, revenue, and average order value grouped by Daily / Weekly / Monthly
    /// </summary>
    public partial class SalesSummaryReportForm : Form
    {
        private readonly POSDbContext _db;

        public SalesSummaryReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
            cmbGroup.SelectedIndex = 0; // Monthly
            ApplyGridStyles();
            WireEvents();
        }

        private void ApplyGridStyles()
        {
            ReportBase.StyleGrid(dgvReport, ReportBase.Green);
            colSalPeriod.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colSalOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSalRevenue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalRevenue.DefaultCellStyle.Format = "N2";
            colSalRevenue.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colSalRevenue.DefaultCellStyle.ForeColor = ReportBase.Green;
            colSalCash.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalCash.DefaultCellStyle.Format = "N2";
            colSalCredit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalCredit.DefaultCellStyle.Format = "N2";
            colSalAvg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalAvg.DefaultCellStyle.Format = "N2";
            btnRun.FlatAppearance.BorderSize = btnPrint.FlatAppearance.BorderSize = btnClose.FlatAppearance.BorderSize = 0;
            btnRun.Cursor = btnPrint.Cursor = btnClose.Cursor = Cursors.Hand;
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(27, 94, 32), ReportBase.Green);
            ReportBase.Hover(btnPrint, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110));
            ReportBase.Hover(btnClose, Color.FromArgb(140, 20, 20), ReportBase.Red);
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
        }

        private void RunReport()
        {
            try
            {
                DateTime from = dtpFrom.Value.Date;
                DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
                string grp = cmbGroup.SelectedItem?.ToString() ?? "Monthly";

                var orders = _db.Orders
                    .Where(o => o.CreatedDate >= from && o.CreatedDate <= to)
                    .ToList();

                dgvReport.Rows.Clear();
                decimal gRev = 0, gCash = 0, gCredit = 0; int gOrders = 0;

                if (grp == "Monthly")
                {
                    var groups = orders.GroupBy(o => o.CreatedDate.ToString("yyyy-MM"))
                                       .OrderBy(g => g.Key).ToList();
                    foreach (var g in groups)
                    {
                        decimal rev = (decimal)g.Sum(o => o.TotalBill);
                        decimal cash = (decimal)g.Where(o => o.paymentType == "Cash").Sum(o => o.TotalBill);
                        decimal credit = (decimal)g.Where(o => o.paymentType != "Cash").Sum(o => o.TotalBill);
                        int cnt = g.Count();
                        DateTime d = DateTime.ParseExact(g.Key, "yyyy-MM", null);
                        AddRow(d.ToString("MMM yyyy"), cnt, rev, cash, credit);
                        gRev += rev; gCash += cash; gCredit += credit; gOrders += cnt;
                    }
                }
                else if (grp == "Weekly")
                {
                    var groups = orders.GroupBy(o =>
                    {
                        var mon = o.CreatedDate.Date.AddDays(-(int)o.CreatedDate.DayOfWeek + (int)DayOfWeek.Monday);
                        return mon.ToString("yyyy-MM-dd");
                    }).OrderBy(g => g.Key).ToList();
                    foreach (var g in groups)
                    {
                        decimal rev = (decimal)g.Sum(o => o.TotalBill);
                        decimal cash = (decimal)g.Where(o => o.paymentType == "Cash").Sum(o => o.TotalBill);
                        decimal credit = (decimal)g.Where(o => o.paymentType != "Cash").Sum(o => o.TotalBill);
                        int cnt = g.Count();
                        DateTime d = DateTime.Parse(g.Key);
                        AddRow($"Wk {d:dd MMM} – {d.AddDays(6):dd MMM}", cnt, rev, cash, credit);
                        gRev += rev; gCash += cash; gCredit += credit; gOrders += cnt;
                    }
                }
                else // Daily
                {
                    var groups = orders.GroupBy(o => o.CreatedDate.Date)
                                       .OrderBy(g => g.Key).ToList();
                    foreach (var g in groups)
                    {
                        decimal rev = (decimal)g.Sum(o => o.TotalBill);
                        decimal cash = (decimal)g.Where(o => o.paymentType == "Cash").Sum(o => o.TotalBill);
                        decimal credit = (decimal)g.Where(o => o.paymentType != "Cash").Sum(o => o.TotalBill);
                        int cnt = g.Count();
                        AddRow(g.Key.ToString("dd MMM yyyy"), cnt, rev, cash, credit);
                        gRev += rev; gCash += cash; gCredit += credit; gOrders += cnt;
                    }
                }

                if (dgvReport.Rows.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colSalPeriod"].Value = $"TOTAL  ({dgvReport.Rows.Count - 1} {grp.ToLower()} periods)";
                    tr.Cells["colSalOrders"].Value = gOrders;
                    tr.Cells["colSalRevenue"].Value = gRev;
                    tr.Cells["colSalCash"].Value = gCash;
                    tr.Cells["colSalCredit"].Value = gCredit;
                    tr.Cells["colSalAvg"].Value = gOrders > 0 ? gRev / gOrders : 0;
                    ReportBase.StyleTotalRow(tr, ReportBase.Green);
                }

                lblBar.Text = $"  Sales Summary  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {gOrders} orders  ·  Revenue: Rs. {gRev:N2}";
                lblEmpty.Visible = dgvReport.Rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void AddRow(string period, int orders, decimal rev, decimal cash, decimal credit)
        {
            var row = dgvReport.Rows[dgvReport.Rows.Add()];
            row.Cells["colSalPeriod"].Value = period;
            row.Cells["colSalOrders"].Value = orders;
            row.Cells["colSalRevenue"].Value = rev;
            row.Cells["colSalCash"].Value = cash;
            row.Cells["colSalCredit"].Value = credit;
            row.Cells["colSalAvg"].Value = orders > 0 ? rev / orders : 0;
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}


//// ════════════════════════════════════════════════════════════════════════════
//// SalesSummaryReportForm.cs  —  Report 11: Sales Summary (with Profit)
//// ════════════════════════════════════════════════════════════════════════════
//using POS_Shop.Models;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Controllers.Reports
//{
//    /// <summary>
//    /// Report 11 — Sales Summary
//    /// Total orders, revenue, actual bill, profit, cash vs credit,
//    /// and average order value — grouped by Daily / Weekly / Monthly.
//    /// </summary>
//    public partial class SalesSummaryReportForm : Form
//    {
//        private readonly POSDbContext _db;

//        // ────────────────────────────────────────────────────────────────────
//        #region Constructor
//        // ────────────────────────────────────────────────────────────────────

//        public SalesSummaryReportForm()
//        {
//            InitializeComponent();
//            _db = new POSDbContext();

//            dtpFrom.Value = DateTime.Today.AddMonths(-3);
//            dtpTo.Value = DateTime.Today;
//            cmbGroup.SelectedIndex = 0; // Monthly by default

//            ApplyGridStyles();
//            WireEvents();
//        }

//        #endregion

//        // ────────────────────────────────────────────────────────────────────
//        #region Grid Styles
//        // ────────────────────────────────────────────────────────────────────

//        private void ApplyGridStyles()
//        {
//            ReportBase.StyleGrid(dgvReport, ReportBase.Green);

//            // Period column — bold so period labels stand out
//            colSalPeriod.DefaultCellStyle.Font =
//                new Font("Segoe UI", 9.5f, FontStyle.Bold);

//            // Orders — centred
//            colSalOrders.DefaultCellStyle.Alignment =
//                DataGridViewContentAlignment.MiddleCenter;

//            // Revenue — green bold (primary KPI)
//            StyleMoneyColumn(colSalRevenue, foreColor: ReportBase.Green, bold: true);

//            // Cash & Credit
//            StyleMoneyColumn(colSalCash);
//            StyleMoneyColumn(colSalCredit);

//            // Actual Bill
//            StyleMoneyColumn(colSalActual);

//            // Profit — teal bold so it pops as the headline metric
//            StyleMoneyColumn(colSalProfit,
//                foreColor: Color.FromArgb(0, 150, 136), bold: true);

//            // Avg Order
//            StyleMoneyColumn(colSalAvg);

//            // Buttons
//            btnRun.FlatAppearance.BorderSize =
//            btnPrint.FlatAppearance.BorderSize =
//            btnClose.FlatAppearance.BorderSize = 0;

//            btnRun.Cursor = btnPrint.Cursor = btnClose.Cursor = Cursors.Hand;
//        }

//        /// <summary>
//        /// Applies right-aligned N2 money formatting to a column,
//        /// with optional foreground colour and bold font.
//        /// </summary>
//        private static void StyleMoneyColumn(
//            DataGridViewTextBoxColumn col,
//            Color? foreColor = null,
//            bool bold = false)
//        {
//            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
//            col.DefaultCellStyle.Format = "N2";

//            if (foreColor.HasValue)
//                col.DefaultCellStyle.ForeColor = foreColor.Value;

//            if (bold)
//                col.DefaultCellStyle.Font =
//                    new Font("Segoe UI", 9.5f, FontStyle.Bold);
//        }

//        #endregion

//        // ────────────────────────────────────────────────────────────────────
//        #region Event Wiring
//        // ────────────────────────────────────────────────────────────────────

//        private void WireEvents()
//        {
//            btnRun.Click += (s, e) => RunReport();
//            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
//            btnClose.Click += (s, e) => Close();

//            ReportBase.Hover(btnRun, Color.FromArgb(27, 94, 32), ReportBase.Green);
//            ReportBase.Hover(btnPrint, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110));
//            ReportBase.Hover(btnClose, Color.FromArgb(140, 20, 20), ReportBase.Red);

//            KeyPreview = true;
//            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
//        }

//        #endregion

//        // ────────────────────────────────────────────────────────────────────
//        #region Report Engine
//        // ────────────────────────────────────────────────────────────────────

//        private void RunReport()
//        {
//            try
//            {
//                // ── Date range ───────────────────────────────────────────────
//                DateTime from = dtpFrom.Value.Date;
//                DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1); // end of day
//                string grp = cmbGroup.SelectedItem?.ToString() ?? "Monthly";

//                // ── Load data ────────────────────────────────────────────────
//                var orders = _db.Orders
//                    .Where(o => o.CreatedDate >= from && o.CreatedDate <= to)
//                    .ToList();

//                dgvReport.Rows.Clear();

//                // Grand totals
//                var grand = new PeriodSums();

//                // ── Group & render rows ──────────────────────────────────────
//                switch (grp)
//                {
//                    case "Monthly":
//                        RenderGroups(
//                            orders.GroupBy(o => o.CreatedDate.ToString("yyyy-MM"))
//                                  .OrderBy(g => g.Key),
//                            key =>
//                            {
//                                DateTime d = DateTime.ParseExact(key, "yyyy-MM", null);
//                                return d.ToString("MMM yyyy");
//                            },
//                            ref grand);
//                        break;

//                    case "Weekly":
//                        RenderGroups(
//                            orders.GroupBy(o =>
//                            {
//                                // Week anchor = Monday of that week
//                                var mon = o.CreatedDate.Date.AddDays(
//                                    -(int)o.CreatedDate.DayOfWeek + (int)DayOfWeek.Monday);
//                                return mon.ToString("yyyy-MM-dd");
//                            }).OrderBy(g => g.Key),
//                            key =>
//                            {
//                                DateTime d = DateTime.Parse(key);
//                                return $"Wk {d:dd MMM} – {d.AddDays(6):dd MMM}";
//                            },
//                            ref grand);
//                        break;

//                    default: // Daily
//                        RenderGroups(
//                            orders.GroupBy(o => o.CreatedDate.Date.ToString("yyyy-MM-dd"))
//                                  .OrderBy(g => g.Key),
//                            key =>
//                            {
//                                DateTime d = DateTime.Parse(key);
//                                return d.ToString("dd MMM yyyy");
//                            },
//                            ref grand);
//                        break;
//                }

//                // ── Grand total row ──────────────────────────────────────────
//                if (dgvReport.Rows.Count > 0)
//                    AddTotalRow(grp, grand);

//                // ── Status bar ───────────────────────────────────────────────
//                lblBar.Text =
//                    $"  Sales Summary  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}" +
//                    $"  ·  {grand.Orders} orders" +
//                    $"  ·  Revenue: Rs. {grand.Revenue:N2}" +
//                    $"  ·  Profit: Rs. {grand.Profit:N2}";

//                lblEmpty.Visible = dgvReport.Rows.Count == 0;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        /// <summary>
//        /// Generic renderer: accepts any IGrouping of Orders keyed by string,
//        /// formats the period label via <paramref name="labelFn"/>,
//        /// and accumulates into <paramref name="grand"/>.
//        /// </summary>
//        private void RenderGroups(
//            IEnumerable<IGrouping<string, POS_Shop.Models.Order>> groups,
//            Func<string, string> labelFn,
//            ref PeriodSums grand)
//        {
//            foreach (var g in groups)
//            {
//                var sums = CalcSums(g);
//                AddDataRow(labelFn(g.Key), sums);
//                grand.Add(sums);
//            }
//        }

//        #endregion

//        // ────────────────────────────────────────────────────────────────────
//        #region Grid Row Builders
//        // ────────────────────────────────────────────────────────────────────

//        /// <summary>Appends one data row to the grid.</summary>
//        private void AddDataRow(string period, PeriodSums s)
//        {
//            var row = dgvReport.Rows[dgvReport.Rows.Add()];
//            PopulateRow(row, period, s);
//        }

//        /// <summary>Appends the grand-total summary row to the grid.</summary>
//        private void AddTotalRow(string grp, PeriodSums grand)
//        {
//            int periodCount = dgvReport.Rows.Count; // all data rows already added
//            var tr = dgvReport.Rows[dgvReport.Rows.Add()];

//            tr.Cells["colSalPeriod"].Value =
//                $"TOTAL  ({periodCount} {grp.ToLower()} periods)";

//            PopulateRow(tr, label: null, grand); // label already set above

//            ReportBase.StyleTotalRow(tr, ReportBase.Green);
//        }

//        /// <summary>
//        /// Fills the numeric cells of a row from a <see cref="PeriodSums"/>.
//        /// Pass <paramref name="label"/> = null to skip setting the Period cell
//        /// (used by the total row which sets its own label first).
//        /// </summary>
//        private static void PopulateRow(DataGridViewRow row, string label, PeriodSums s)
//        {
//            if (label != null)
//                row.Cells["colSalPeriod"].Value = label;

//            row.Cells["colSalOrders"].Value = s.Orders;
//            row.Cells["colSalRevenue"].Value = s.Revenue;
//            row.Cells["colSalCash"].Value = s.Cash;
//            row.Cells["colSalCredit"].Value = s.Credit;
//            row.Cells["colSalActual"].Value = s.Actual;
//            row.Cells["colSalProfit"].Value = s.Profit;
//            row.Cells["colSalAvg"].Value = s.Orders > 0
//                                                ? Math.Round(s.Revenue / s.Orders, 2)
//                                                : 0m;
//        }

//        #endregion

//        // ────────────────────────────────────────────────────────────────────
//        #region Aggregation
//        // ────────────────────────────────────────────────────────────────────

//        /// <summary>
//        /// Calculates all sums for a group of orders in one pass.
//        /// </summary>
//        private static PeriodSums CalcSums(IEnumerable<POS_Shop.Models.Order> source)
//        {
//            // Materialise once — avoid re-enumerating the LINQ group multiple times
//            var list = source as IList<POS_Shop.Models.Order> ?? source.ToList();

//            return new PeriodSums
//            {
//                Orders = list.Count,
//                Revenue = (decimal)list.Sum(o => o.TotalBill),
//                Cash = (decimal)list.Where(o => o.paymentType == "Cash")
//                                       .Sum(o => o.TotalBill),
//                Credit = (decimal)list.Where(o => o.paymentType != "Cash")
//                                       .Sum(o => o.TotalBill),
//                Actual = list.Sum(o => o.TotalActualBill),  // already decimal
//                Profit = list.Sum(o => o.TotalProfit),       // already decimal
//            };
//        }

//        #endregion

//        // ────────────────────────────────────────────────────────────────────
//        #region PeriodSums (Value Object)
//        // ────────────────────────────────────────────────────────────────────

//        /// <summary>
//        /// Immutable-ish value object that carries all aggregated figures
//        /// for one time-period group or a running grand total.
//        /// </summary>
//        private struct PeriodSums
//        {
//            public int Orders;
//            public decimal Revenue;
//            public decimal Cash;
//            public decimal Credit;
//            public decimal Actual;
//            public decimal Profit;

//            /// <summary>Adds another period's sums into this instance (for grand totals).</summary>
//            public void Add(PeriodSums other)
//            {
//                Orders += other.Orders;
//                Revenue += other.Revenue;
//                Cash += other.Cash;
//                Credit += other.Credit;
//                Actual += other.Actual;
//                Profit += other.Profit;
//            }
//        }

//        #endregion

//        // ────────────────────────────────────────────────────────────────────
//        #region Cleanup
//        // ────────────────────────────────────────────────────────────────────

//        protected override void OnFormClosed(FormClosedEventArgs e)
//        {
//            base.OnFormClosed(e);
//            _db?.Dispose();
//        }

//        #endregion
//    }
//}