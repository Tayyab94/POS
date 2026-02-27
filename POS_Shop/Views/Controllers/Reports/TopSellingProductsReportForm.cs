using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 14 — Top Selling Products
    /// Products ranked by quantity sold and revenue in the selected period.
    /// Also shows purchase cost vs sale price for margin estimation.
    /// </summary>
    public partial class TopSellingProductsReportForm : Form
    {
        private readonly POSDbContext _db;

        public TopSellingProductsReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
            cmbSort.SelectedIndex = 0; // By Revenue
            ApplyGridStyles();
            WireEvents();
        }

        private void ApplyGridStyles()
        {
            ReportBase.StyleGrid(dgvReport, ReportBase.Teal);
            colTPRank.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTPRank.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colTPQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTPQty.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colTPRevenue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTPRevenue.DefaultCellStyle.Format = "N2";
            colTPRevenue.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colTPRevenue.DefaultCellStyle.ForeColor = ReportBase.Teal;
            colTPAvgPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTPAvgPrice.DefaultCellStyle.Format = "N2";
            colTPOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTPShare.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTPShare.DefaultCellStyle.Format = "N1";
            btnRun.FlatAppearance.BorderSize = btnPrint.FlatAppearance.BorderSize = btnClose.FlatAppearance.BorderSize = 0;
            btnRun.Cursor = btnPrint.Cursor = btnClose.Cursor = Cursors.Hand;
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(0, 77, 64), ReportBase.Teal);
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
                string sort = cmbSort.SelectedItem?.ToString() ?? "By Revenue";

                var details = _db.OrderDetails
                    .Include("Order")
                    .Include("Product")
                    .Where(d => d.Order.CreatedDate >= from && d.Order.CreatedDate <= to)
                    .ToList();

                decimal totalRevenue = (decimal)details.Sum(d => d.Price * d.Quantity);

                var groups = details
                    .GroupBy(d => d.ProductId)
                    .Select(g =>
                    {
                        string name = g.First().Product?.ProductEnglishName
                                   ?? g.First().OtherProductName
                                   ?? "Unknown Product";
                        string code = g.First().Product?.SearchByProductCode ?? "-";
                        int qty = g.Sum(d => d.Quantity);
                        decimal rev = (decimal)g.Sum(d => d.Price * d.Quantity);
                        decimal avgPrc = qty > 0 ? rev / qty : 0;
                        int orders = g.Select(d => d.OrderId).Distinct().Count();
                        return new { Name = name, Code = code, Qty = qty, Revenue = rev, AvgPrice = avgPrc, Orders = orders };
                    });

                // Sort
                var sorted = sort == "By Quantity"
                    ? groups.OrderByDescending(x => x.Qty).ToList()
                    : groups.OrderByDescending(x => x.Revenue).ToList();

                dgvReport.Rows.Clear();
                int rank = 1; decimal gRev = 0; int gQty = 0;

                foreach (var r in sorted)
                {
                    decimal share = totalRevenue > 0 ? (r.Revenue / totalRevenue) * 100m : 0m;
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colTPRank"].Value = rank++;
                    row.Cells["colTPName"].Value = r.Name;
                    row.Cells["colTPCode"].Value = r.Code;
                    row.Cells["colTPQty"].Value = r.Qty;
                    row.Cells["colTPOrders"].Value = r.Orders;
                    row.Cells["colTPRevenue"].Value = r.Revenue;
                    row.Cells["colTPAvgPrice"].Value = r.AvgPrice;
                    row.Cells["colTPShare"].Value = share;
                    gRev += r.Revenue; gQty += r.Qty;
                }

                if (sorted.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colTPName"].Value = $"GRAND TOTAL  ({sorted.Count} products)";
                    tr.Cells["colTPQty"].Value = gQty;
                    tr.Cells["colTPRevenue"].Value = gRev;
                    tr.Cells["colTPShare"].Value = 100m;
                    ReportBase.StyleTotalRow(tr, ReportBase.Teal);
                }

                lblBar.Text = $"  Top Selling Products  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {sorted.Count} products  ·  Revenue: Rs. {gRev:N2}  ·  Units: {gQty:N0}";
                lblEmpty.Visible = sorted.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}