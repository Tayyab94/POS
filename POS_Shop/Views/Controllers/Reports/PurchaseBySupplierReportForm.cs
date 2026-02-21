using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    public partial class PurchaseBySupplierReportForm : Form
    {
        private readonly POSDbContext _db;
        public PurchaseBySupplierReportForm() { InitializeComponent(); 
            _db = new POSDbContext(); dtpFrom.Value = DateTime.Today.AddMonths(-3); dtpTo.Value = DateTime.Today; WireEvents(); }
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
                var rows = _db.Purchases.Include("Supplier").Where(p => !p.IsDeleted && p.PurchaseDate >= from && p.PurchaseDate <= to).ToList()
                    .GroupBy(p => p.SupplierId)
                    .Select(g => new { Name = g.First().Supplier?.SupplierName ?? $"#{g.Key}", Shop = g.First().Supplier?.ShopName ?? "", Inv = g.Count(), Bill = g.Sum(p => p.TotalAmount), Disc = g.Sum(p => p.Discount), Net = g.Sum(p => p.NetAmount), Paid = g.Sum(p => p.TotalPaid), Out = g.Sum(p => p.Balance), Last = g.Max(p => p.PurchaseDate) })
                    .OrderByDescending(x => x.Net).ToList();
                dgvReport.Rows.Clear();
                decimal gNet = 0, gOut = 0; int rank = 1;
                foreach (var r in rows)
                {
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colBSRank"].Value = rank++; row.Cells["colBSSup"].Value = $"{r.Name}  —  {r.Shop}";
                    row.Cells["colBSInv"].Value = r.Inv; row.Cells["colBSBill"].Value = r.Bill; row.Cells["colBSDisc"].Value = r.Disc;
                    row.Cells["colBSNet"].Value = r.Net; row.Cells["colBSPaid"].Value = r.Paid; row.Cells["colBSOut"].Value = r.Out;
                    row.Cells["colBSLast"].Value = r.Last.ToString("dd MMM yyyy");
                    if (r.Out > 0) row.Cells["colBSOut"].Style.ForeColor = ReportBase.Red;
                    gNet += r.Net; gOut += r.Out;
                }
                if (rows.Count > 0) { var tr = dgvReport.Rows[dgvReport.Rows.Add()]; tr.Cells["colBSSup"].Value = $"GRAND TOTAL  ({rows.Count} suppliers)"; tr.Cells["colBSNet"].Value = gNet; tr.Cells["colBSOut"].Value = gOut; ReportBase.StyleTotalRow(tr, ReportBase.Blue); }
                lblBar.Text = $"  Purchase by Supplier  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {rows.Count} suppliers  ·  Net: Rs. {gNet:N2}";
                lblEmpty.Visible = rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}