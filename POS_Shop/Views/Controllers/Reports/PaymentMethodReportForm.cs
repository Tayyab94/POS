using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    public partial class PaymentMethodReportForm : Form
    {
        private readonly POSDbContext _db;
        public PaymentMethodReportForm() { InitializeComponent(); _db = new POSDbContext(); dtpFrom.Value = DateTime.Today.AddMonths(-3); dtpTo.Value = DateTime.Today; WireEvents(); }
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
                var rows = _db.SupplierPayments.Where(p => !p.IsDeleted && p.PaymentDate >= from && p.PaymentDate <= to).ToList()
                    .GroupBy(p => p.PaymentMethod)
                    .Select(g => new { Method = g.Key.ToString(), Runs = g.Count(), Total = g.Sum(p => p.TotalAmountPaid), Avg = g.Average(p => p.TotalAmountPaid), Last = g.Max(p => p.PaymentDate) })
                    .OrderByDescending(x => x.Total).ToList();
                var colours = new Dictionary<string, Color> { ["Cash"] = ReportBase.Green, ["BankTransfer"] = ReportBase.Blue, ["Cheque"] = ReportBase.Orange, ["OnlineTransfer"] = ReportBase.Purple };
                dgvReport.Rows.Clear();
                decimal grand = 0; int grandRuns = 0;
                foreach (var r in rows)
                {
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colPMMethod"].Value = r.Method; row.Cells["colPMRuns"].Value = r.Runs;
                    row.Cells["colPMTotal"].Value = r.Total; row.Cells["colPMAvg"].Value = r.Avg;
                    row.Cells["colPMShare"].Value = "—"; row.Cells["colPMLast"].Value = r.Last.ToString("dd MMM yyyy");
                    if (colours.TryGetValue(r.Method, out var c)) { row.Cells["colPMMethod"].Style.ForeColor = c; row.Cells["colPMMethod"].Style.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold); }
                    grand += r.Total; grandRuns += r.Runs;
                }
                foreach (DataGridViewRow row in dgvReport.Rows)
                    if (grand > 0 && row.Cells["colPMTotal"].Value is decimal v) row.Cells["colPMShare"].Value = $"{(v / grand * 100):F1}%";
                if (rows.Count > 0) { var tr = dgvReport.Rows[dgvReport.Rows.Add()]; tr.Cells["colPMMethod"].Value = $"TOTAL  ({grandRuns} payment runs)"; tr.Cells["colPMTotal"].Value = grand; tr.Cells["colPMShare"].Value = "100%"; ReportBase.StyleTotalRow(tr, ReportBase.Blue); }
                lblBar.Text = $"  Payment Method Analysis  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  Total Paid: Rs. {grand:N2}";
                lblEmpty.Visible = rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}