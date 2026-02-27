using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 15 — Sales vs Purchase Comparison
    /// Side-by-side monthly view: Sales revenue vs Purchase cost, ratio, and trend.
    /// Quick answer: "Did we buy more than we sold this month?"
    /// </summary>
    public partial class SalesPurchaseComparisonReportForm : Form
    {
        private readonly POSDbContext _db;

        public SalesPurchaseComparisonReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddMonths(-12);
            dtpTo.Value = DateTime.Today;
            ApplyGridStyles();
            WireEvents();
        }

        private void ApplyGridStyles()
        {
            ReportBase.StyleGrid(dgvReport, ReportBase.Blue);
            colSPMonth.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colSPSOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSPSales.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSPSales.DefaultCellStyle.Format = "N2";
            colSPSales.DefaultCellStyle.ForeColor = ReportBase.Green;
            colSPSales.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colSPPInv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSPPurchase.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSPPurchase.DefaultCellStyle.Format = "N2";
            colSPPurchase.DefaultCellStyle.ForeColor = ReportBase.Red;
            colSPDiff.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSPDiff.DefaultCellStyle.Format = "N2";
            colSPDiff.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colSPRatio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSPRatio.DefaultCellStyle.Format = "N2";
            btnRun.FlatAppearance.BorderSize = btnPrint.FlatAppearance.BorderSize = btnClose.FlatAppearance.BorderSize = 0;
            btnRun.Cursor = btnPrint.Cursor = btnClose.Cursor = Cursors.Hand;
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(13, 71, 161), ReportBase.Blue);
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

                var salesByMonth = _db.Orders
                    .Where(o => o.CreatedDate >= from && o.CreatedDate <= to)
                    .ToList()
                    .GroupBy(o => o.CreatedDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => new {
                        Sales = (decimal)g.Sum(o => o.TotalBill),
                        Orders = g.Count()
                    });

                var purchByMonth = _db.Purchases
                    .Where(p => !p.IsDeleted && p.PurchaseDate >= from && p.PurchaseDate <= to)
                    .ToList()
                    .GroupBy(p => p.PurchaseDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => new {
                        Cost = g.Sum(p => p.NetAmount),
                        Inv = g.Count()
                    });

                var allMonths = salesByMonth.Keys.Union(purchByMonth.Keys)
                    .OrderBy(k => k).ToList();

                dgvReport.Rows.Clear();
                decimal gSales = 0, gCost = 0; int gOrders = 0, gInv = 0;

                foreach (var month in allMonths)
                {
                    decimal sales = salesByMonth.ContainsKey(month) ? salesByMonth[month].Sales : 0;
                    decimal cost = purchByMonth.ContainsKey(month) ? purchByMonth[month].Cost : 0;
                    decimal diff = sales - cost;
                    decimal ratio = cost > 0 ? sales / cost : 0;
                    int orders = salesByMonth.ContainsKey(month) ? salesByMonth[month].Orders : 0;
                    int inv = purchByMonth.ContainsKey(month) ? purchByMonth[month].Inv : 0;

                    DateTime d = DateTime.ParseExact(month, "yyyy-MM", null);
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colSPMonth"].Value = d.ToString("MMM yyyy");
                    row.Cells["colSPSOrders"].Value = orders;
                    row.Cells["colSPSales"].Value = sales;
                    row.Cells["colSPPInv"].Value = inv;
                    row.Cells["colSPPurchase"].Value = cost;
                    row.Cells["colSPDiff"].Value = diff;
                    row.Cells["colSPRatio"].Value = ratio;

                    // Colour the diff cell
                    row.Cells["colSPDiff"].Style.ForeColor = diff >= 0 ? ReportBase.Green : ReportBase.Red;
                    // Colour ratio: good if sales > 1.2× cost
                    row.Cells["colSPRatio"].Style.ForeColor = ratio >= 1.2m ? ReportBase.Green
                                                             : ratio >= 1.0m ? ReportBase.Orange
                                                             : ReportBase.Red;

                    gSales += sales; gCost += cost; gOrders += orders; gInv += inv;
                }

                if (allMonths.Count > 0)
                {
                    decimal gDiff = gSales - gCost;
                    decimal gRatio = gCost > 0 ? gSales / gCost : 0;
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colSPMonth"].Value = $"TOTAL  ({allMonths.Count} months)";
                    tr.Cells["colSPSOrders"].Value = gOrders;
                    tr.Cells["colSPSales"].Value = gSales;
                    tr.Cells["colSPPInv"].Value = gInv;
                    tr.Cells["colSPPurchase"].Value = gCost;
                    tr.Cells["colSPDiff"].Value = gDiff;
                    tr.Cells["colSPRatio"].Value = gRatio;
                    ReportBase.StyleTotalRow(tr, ReportBase.Blue);
                }

                decimal totalDiff = gSales - gCost;
                string status = totalDiff >= 0 ? $"Surplus Rs. {totalDiff:N2}" : $"DEFICIT Rs. {Math.Abs(totalDiff):N2}";
                lblBar.Text = $"  Sales vs Purchase  ·  {from:MMM yyyy} → {dtpTo.Value.Date:MMM yyyy}" +
                              $"  ·  Sales: Rs. {gSales:N2}  ·  Purchases: Rs. {gCost:N2}  ·  {status}";
                lblEmpty.Visible = allMonths.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}