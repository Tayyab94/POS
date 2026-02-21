using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    public partial class AgingReportForm : Form
    {
        private readonly POSDbContext _db;
        public AgingReportForm() { InitializeComponent(); _db = new POSDbContext(); WireEvents(); }
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
                DateTime today = DateTime.Today;
                var rows = _db.Purchases.Include("Supplier").Where(p => !p.IsDeleted && p.Balance > 0).ToList()
                    .GroupBy(p => p.SupplierId)
                    .Select(g =>
                    {
                        decimal cur = 0, d30 = 0, d60 = 0, d90 = 0;
                        foreach (var inv in g) { int age = (today - inv.PurchaseDate.Date).Days; if (age <= 30) cur += inv.Balance; else if (age <= 60) d30 += inv.Balance; else if (age <= 90) d60 += inv.Balance; else d90 += inv.Balance; }
                        return new { Sup = g.First().Supplier?.SupplierName ?? $"#{g.Key}", Shop = g.First().Supplier?.ShopName ?? "", Inv = g.Count(), Cur = cur, D30 = d30, D60 = d60, D90 = d90, Tot = cur + d30 + d60 + d90 };
                    })
                    .OrderByDescending(x => x.Tot).ToList();
                dgvReport.Rows.Clear();
                decimal gCur = 0, g30 = 0, g60 = 0, g90 = 0, gTot = 0;
                foreach (var r in rows)
                {
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colAGSup"].Value = $"{r.Sup}  —  {r.Shop}"; row.Cells["colAGInv"].Value = r.Inv;
                    row.Cells["colAGCur"].Value = r.Cur; row.Cells["colAG30"].Value = r.D30; row.Cells["colAG60"].Value = r.D60; row.Cells["colAG90"].Value = r.D90; row.Cells["colAGTot"].Value = r.Tot;
                    if (r.D90 > 0) row.Cells["colAG90"].Style.ForeColor = Color.FromArgb(183, 28, 28);
                    if (r.D60 > 0) row.Cells["colAG60"].Style.ForeColor = ReportBase.Orange;
                    if (r.D30 > 0) row.Cells["colAG30"].Style.ForeColor = Color.FromArgb(245, 124, 0);
                    gCur += r.Cur; g30 += r.D30; g60 += r.D60; g90 += r.D90; gTot += r.Tot;
                }
                if (rows.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colAGSup"].Value = $"GRAND TOTAL  ({rows.Count} suppliers)";
                    tr.Cells["colAGCur"].Value = gCur; tr.Cells["colAG30"].Value = g30; tr.Cells["colAG60"].Value = g60; tr.Cells["colAG90"].Value = g90; tr.Cells["colAGTot"].Value = gTot;
                    ReportBase.StyleTotalRow(tr, ReportBase.Red);
                }
                lblBar.Text = $"  Supplier Aging Report  ·  As of {today:dd MMM yyyy}  ·  {rows.Count} suppliers  ·  Total Outstanding: Rs. {gTot:N2}";
                lblEmpty.Visible = rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}