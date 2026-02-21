using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 7 — Monthly Cash Flow
    /// Side-by-side: obligations (invoices) vs payments made each month + running cumulative
    /// </summary>
    public partial class MonthlyCashFlowReportForm : Form
    {
        private readonly POSDbContext _db;

        public MonthlyCashFlowReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddMonths(-12);
            dtpTo.Value = DateTime.Today;
            WireEvents();
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

                var obligByMonth = _db.Purchases
                    .Where(p => !p.IsDeleted && p.PurchaseDate >= from && p.PurchaseDate <= to)
                    .ToList()
                    .GroupBy(p => p.PurchaseDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => g.Sum(p => p.NetAmount));

                var paidByMonth = _db.SupplierPayments
                    .Where(p => !p.IsDeleted && p.PaymentDate >= from && p.PaymentDate <= to)
                    .ToList()
                    .GroupBy(p => p.PaymentDate.ToString("yyyy-MM"))
                    .ToDictionary(g => g.Key, g => g.Sum(p => p.TotalAmountPaid));

                var allMonths = obligByMonth.Keys.Union(paidByMonth.Keys)
                    .OrderBy(k => k).ToList();

                dgvReport.Rows.Clear();
                // FIX: single pair of accumulators — cumOblig/cumPaid serve as
                // both running row cumulative AND grand total at loop end.
                // Old code had separate gOblig/gPaid that added a second time at the
                // bottom of the loop, making every grand total exactly 2× the real value.
                decimal cumOblig = 0, cumPaid = 0;

                foreach (var month in allMonths)
                {
                    decimal oblig = obligByMonth.ContainsKey(month) ? obligByMonth[month] : 0;
                    decimal paid = paidByMonth.ContainsKey(month) ? paidByMonth[month] : 0;
                    decimal diff = paid - oblig;

                    cumOblig += oblig;  // accumulate exactly once per month
                    cumPaid += paid;

                    DateTime d = DateTime.ParseExact(month, "yyyy-MM", null);
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colCFMonth"].Value = d.ToString("MMM yyyy");
                    row.Cells["colCFOblig"].Value = oblig;
                    row.Cells["colCFPaid"].Value = paid;
                    row.Cells["colCFDiff"].Value = diff;
                    row.Cells["colCFCumOblig"].Value = cumOblig;
                    row.Cells["colCFCumPaid"].Value = cumPaid;

                    if (diff < 0) row.Cells["colCFDiff"].Style.ForeColor = ReportBase.Red;
                    else if (diff > 0) row.Cells["colCFDiff"].Style.ForeColor = ReportBase.Green;
                    row.Cells["colCFDiff"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    // REMOVED: "gOblig += oblig; gPaid += paid;" second accumulation
                }

                if (allMonths.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colCFMonth"].Value = $"TOTAL  ({allMonths.Count} months)";
                    tr.Cells["colCFOblig"].Value = cumOblig;           // correct grand total
                    tr.Cells["colCFPaid"].Value = cumPaid;            // correct grand total
                    tr.Cells["colCFDiff"].Value = cumPaid - cumOblig;
                    ReportBase.StyleTotalRow(tr, ReportBase.Teal);
                }

                lblBar.Text = $"  Monthly Cash Flow  ·  {from:MMM yyyy} → {dtpTo.Value.Date:MMM yyyy}" +
                                   $"  ·  Obligations: Rs. {cumOblig:N2}" +
                                   $"  ·  Paid: Rs. {cumPaid:N2}" +
                                   $"  ·  Net: Rs. {cumPaid - cumOblig:N2}";
                lblEmpty.Visible = allMonths.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}