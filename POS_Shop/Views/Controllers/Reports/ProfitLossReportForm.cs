using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 12 — Profit & Loss
    /// Month-by-month: Sales Revenue vs Purchase Cost vs Gross Profit
    /// Note: "Cost" = purchase NetAmount paid in the same period (cash-basis)
    /// </summary>
    public partial class ProfitLossReportForm : Form
    {
        private readonly POSDbContext _db;

        public ProfitLossReportForm()
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
            ReportBase.StyleGrid(dgvReport, ReportBase.Purple);
            colPLPeriod.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            // Revenue column
            colPLRevenue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPLRevenue.DefaultCellStyle.Format = "N2";
            colPLRevenue.DefaultCellStyle.ForeColor = ReportBase.Green;
            colPLRevenue.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            // Cost column
            colPLCost.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPLCost.DefaultCellStyle.Format = "N2";
            colPLCost.DefaultCellStyle.ForeColor = ReportBase.Red;
            // Gross profit
            colPLGross.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPLGross.DefaultCellStyle.Format = "N2";
            colPLGross.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            // Margin
            colPLMargin.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colPLMargin.DefaultCellStyle.Format = "N1";
            // Orders / Invoices
            colPLOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPLInv.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            btnRun.FlatAppearance.BorderSize = btnPrint.FlatAppearance.BorderSize = btnClose.FlatAppearance.BorderSize = 0;
            btnRun.Cursor = btnPrint.Cursor = btnClose.Cursor = Cursors.Hand;
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(74, 20, 140), ReportBase.Purple);
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

                // Sales grouped by month
                var salesByMonth = _db.Orders
                    .Where(o => o.CreatedDate >= from && o.CreatedDate <= to)
                    .ToList()
                    .GroupBy(o => o.CreatedDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => new {
                        Revenue = (decimal)g.Sum(o => o.TotalBill),
                        Orders = g.Count()
                    });

                // Purchases (cost) grouped by month — uses NetAmount (what we owe the supplier)
                var costByMonth = _db.Purchases
                    .Where(p => !p.IsDeleted && p.PurchaseDate >= from && p.PurchaseDate <= to)
                    .ToList()
                    .GroupBy(p => p.PurchaseDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => new {
                        Cost = g.Sum(p => p.NetAmount),
                        Inv = g.Count()
                    });

                var allMonths = salesByMonth.Keys.Union(costByMonth.Keys)
                    .OrderBy(k => k).ToList();

                dgvReport.Rows.Clear();
                decimal gRev = 0, gCost = 0; int gOrders = 0, gInv = 0;

                foreach (var month in allMonths)
                {
                    decimal rev = salesByMonth.ContainsKey(month) ? salesByMonth[month].Revenue : 0;
                    decimal cost = costByMonth.ContainsKey(month) ? costByMonth[month].Cost : 0;
                    decimal gross = rev - cost;
                    decimal margin = rev > 0 ? (gross / rev) * 100m : 0m;
                    int orders = salesByMonth.ContainsKey(month) ? salesByMonth[month].Orders : 0;
                    int inv = costByMonth.ContainsKey(month) ? costByMonth[month].Inv : 0;

                    DateTime d = DateTime.ParseExact(month, "yyyy-MM", null);
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colPLPeriod"].Value = d.ToString("MMM yyyy");
                    row.Cells["colPLOrders"].Value = orders;
                    row.Cells["colPLRevenue"].Value = rev;
                    row.Cells["colPLInv"].Value = inv;
                    row.Cells["colPLCost"].Value = cost;
                    row.Cells["colPLGross"].Value = gross;
                    row.Cells["colPLMargin"].Value = margin;

                    // Colour gross profit cell
                    row.Cells["colPLGross"].Style.ForeColor = gross >= 0 ? ReportBase.Green : ReportBase.Red;
                    row.Cells["colPLGross"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    // Colour margin
                    row.Cells["colPLMargin"].Style.ForeColor = margin >= 20 ? ReportBase.Green
                                                              : margin >= 0 ? ReportBase.Orange
                                                              : ReportBase.Red;

                    gRev += rev; gCost += cost; gOrders += orders; gInv += inv;
                }

                if (allMonths.Count > 0)
                {
                    decimal gGross = gRev - gCost;
                    decimal gMargin = gRev > 0 ? (gGross / gRev) * 100m : 0m;
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colPLPeriod"].Value = $"TOTAL  ({allMonths.Count} months)";
                    tr.Cells["colPLOrders"].Value = gOrders;
                    tr.Cells["colPLRevenue"].Value = gRev;
                    tr.Cells["colPLInv"].Value = gInv;
                    tr.Cells["colPLCost"].Value = gCost;
                    tr.Cells["colPLGross"].Value = gGross;
                    tr.Cells["colPLMargin"].Value = gMargin;
                    ReportBase.StyleTotalRow(tr, ReportBase.Purple);
                }

                decimal totalGross = gRev - gCost;
                decimal totalMargin = gRev > 0 ? (totalGross / gRev) * 100m : 0m;
                lblBar.Text = $"  Profit & Loss  ·  {from:MMM yyyy} → {dtpTo.Value.Date:MMM yyyy}" +
                              $"  ·  Revenue: Rs. {gRev:N2}  ·  Cost: Rs. {gCost:N2}" +
                              $"  ·  Gross Profit: Rs. {totalGross:N2}  ·  Margin: {totalMargin:N1}%";
                lblEmpty.Visible = allMonths.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }

      
    }
}