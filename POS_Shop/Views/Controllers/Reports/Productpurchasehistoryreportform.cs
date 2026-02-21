using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    public partial class ProductPurchaseHistoryReportForm : Form
    {
        private readonly POSDbContext _db;
        public ProductPurchaseHistoryReportForm() { InitializeComponent(); _db = new POSDbContext(); dtpFrom.Value = DateTime.Today.AddMonths(-3); dtpTo.Value = DateTime.Today; WireEvents(); }
        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport(); btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this); btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(13, 71, 161), ReportBase.Blue); ReportBase.Hover(btnPrint, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110)); ReportBase.Hover(btnClose, Color.FromArgb(140, 20, 20), ReportBase.Red);
            KeyPreview = true; KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
        }
        private void RunReport()
        {
            try
            {
                DateTime from = dtpFrom.Value.Date, to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
                var items = _db.PurchaseItems.Include("Purchase").Include("Product")
                    .Where(i => !i.IsDeleted && !i.Purchase.IsDeleted && i.Purchase.PurchaseDate >= from && i.Purchase.PurchaseDate <= to).ToList();
                var rows = items.GroupBy(i => i.ProductId)
                    .Select(g =>
                    {
                        var prices = g.Select(i => i.PurchasePrice).ToList();
                        decimal mn = prices.Min(), mx = prices.Max();
                        return new { Name = g.First().Product?.ProductEnglishName ?? $"#{g.Key}", Code = g.First().Product?.SearchByProductCode ?? "", Times = g.Select(i => i.PurchaseId).Distinct().Count(), Qty = g.Sum(i => i.Quantity), Spend = g.Sum(i => i.TotalPrice), Min = mn, Max = mx, Avg = prices.Average(), VarPct = mx > 0 ? (mx - mn) / mx * 100m : 0m, Last = g.Max(i => i.Purchase.PurchaseDate) };
                    })
                    .OrderByDescending(x => x.Spend).ToList();
                dgvReport.Rows.Clear();
                decimal gSpend = 0;
                foreach (var r in rows)
                {
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colPHProduct"].Value = r.Name; row.Cells["colPHCode"].Value = r.Code;
                    row.Cells["colPHTimes"].Value = r.Times; row.Cells["colPHQty"].Value = r.Qty;
                    row.Cells["colPHSpend"].Value = r.Spend; row.Cells["colPHMin"].Value = r.Min;
                    row.Cells["colPHMax"].Value = r.Max; row.Cells["colPHAvg"].Value = r.Avg;
                    row.Cells["colPHLast"].Value = r.Last.ToString("dd MMM yyyy");
                    if (r.VarPct > 20) { row.Cells["colPHMax"].Style.ForeColor = r.VarPct > 50 ? ReportBase.Red : ReportBase.Orange; row.Cells["colPHMax"].Style.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold); }
                    gSpend += r.Spend;
                }
                if (rows.Count > 0) { var tr = dgvReport.Rows[dgvReport.Rows.Add()]; tr.Cells["colPHProduct"].Value = $"TOTAL  ({rows.Count} products)"; tr.Cells["colPHSpend"].Value = gSpend; ReportBase.StyleTotalRow(tr, ReportBase.Brown); }
                lblBar.Text = $"  Product Purchase History  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {rows.Count} products  ·  Total Spend: Rs. {gSpend:N2}";
                lblEmpty.Visible = rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}