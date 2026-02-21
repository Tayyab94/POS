using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    public partial class PriceVarianceReportForm : Form
    {
        private readonly POSDbContext _db;
        public PriceVarianceReportForm() { InitializeComponent(); _db = new POSDbContext(); dtpFrom.Value = DateTime.Today.AddMonths(-6); dtpTo.Value = DateTime.Today; WireEvents(); }
        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport(); btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this); btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(140, 20, 20), ReportBase.Red); ReportBase.Hover(btnPrint, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110)); ReportBase.Hover(btnClose, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110));
            KeyPreview = true; KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
        }
        private void RunReport()
        {
            try
            {
                DateTime from = dtpFrom.Value.Date, to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
                decimal minVar = nudMinVar.Value;
                var items = _db.PurchaseItems.Include("Purchase").Include("Product")
                    .Where(i => !i.IsDeleted && !i.Purchase.IsDeleted && i.Purchase.PurchaseDate >= from && i.Purchase.PurchaseDate <= to).ToList();
                var rows = items.GroupBy(i => i.ProductId)
                    .Where(g => g.Select(i => i.PurchasePrice).Distinct().Count() > 1)
                    .Select(g =>
                    {
                        var prices = g.Select(i => i.PurchasePrice).ToList();
                        decimal mn = prices.Min(), mx = prices.Max();
                        decimal vp = mx > 0 ? (mx - mn) / mx * 100m : 0m;
                        return new { Name = g.First().Product?.ProductEnglishName ?? $"#{g.Key}", Code = g.First().Product?.SearchByProductCode ?? "", Min = mn, Max = mx, Avg = prices.Average(), VarPct = vp, VarRs = mx - mn, Times = g.Count(), Last = g.Max(i => i.Purchase.PurchaseDate) };
                    })
                    .Where(x => x.VarPct >= minVar).OrderByDescending(x => x.VarPct).ToList();
                dgvReport.Rows.Clear();
                foreach (var r in rows)
                {
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colVProduct"].Value = r.Name; row.Cells["colVCode"].Value = r.Code;
                    row.Cells["colVMin"].Value = r.Min; row.Cells["colVMax"].Value = r.Max; row.Cells["colVAvg"].Value = r.Avg;
                    row.Cells["colVVarPct"].Value = $"{r.VarPct:F1}%"; row.Cells["colVVarRs"].Value = r.VarRs;
                    row.Cells["colVTimes"].Value = r.Times; row.Cells["colVLast"].Value = r.Last.ToString("dd MMM yyyy");
                    var vc = r.VarPct > 50 ? Color.FromArgb(183, 28, 28) : r.VarPct > 25 ? ReportBase.Orange : Color.FromArgb(245, 124, 0);
                    row.Cells["colVVarPct"].Style.ForeColor = vc; row.Cells["colVVarPct"].Style.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
                    row.Cells["colVMax"].Style.ForeColor = vc;
                }
                lblBar.Text = $"  Price Variance  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {rows.Count} products with ≥{minVar:F0}% variance";
                lblEmpty.Visible = rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}