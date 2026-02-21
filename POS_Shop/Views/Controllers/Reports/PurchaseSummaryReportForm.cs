using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 1 — Purchase Summary by Period
    /// Groups purchases by Daily / Weekly / Monthly and shows:
    /// Invoices · Total Bill · Discount · Net Spend · Avg Invoice
    /// </summary>
    public partial class PurchaseSummaryReportForm : Form
    {
        private readonly POSDbContext _db;

        public PurchaseSummaryReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
            cmbGroup.SelectedIndex = 1; // Monthly default
            WireEvents();
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
                string group = cmbGroup.SelectedItem?.ToString() ?? "Monthly";

                var list = _db.Purchases
                    .Where(p => !p.IsDeleted && p.PurchaseDate >= from && p.PurchaseDate <= to)
                    .ToList();

                var grouped = list.GroupBy(p =>
                    group == "Daily"
                        ? p.PurchaseDate.Date.ToString("dd MMM yyyy")
                    : group == "Weekly"
                        ? $"Week {GetWeek(p.PurchaseDate)},  {p.PurchaseDate.Year}"
                        : p.PurchaseDate.ToString("MMM yyyy"));

                dgvReport.Rows.Clear();
                decimal gBill = 0, gDisc = 0, gNet = 0; int gInv = 0;

                foreach (var g in grouped.OrderBy(x => x.Key))
                {
                    decimal bill = g.Sum(p => p.TotalAmount);
                    decimal disc = g.Sum(p => p.Discount);
                    decimal net = g.Sum(p => p.NetAmount);
                    int inv = g.Count();

                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colS1Period"].Value = g.Key;
                    row.Cells["colS1Inv"].Value = inv;
                    row.Cells["colS1Bill"].Value = bill;
                    row.Cells["colS1Disc"].Value = disc;
                    row.Cells["colS1Net"].Value = net;
                    row.Cells["colS1Avg"].Value = inv > 0 ? net / inv : 0;
                    gBill += bill; gDisc += disc; gNet += net; gInv += inv;
                }

                if (dgvReport.Rows.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colS1Period"].Value = $"GRAND TOTAL  ({grouped.Count()} periods)";
                    tr.Cells["colS1Inv"].Value = gInv;
                    tr.Cells["colS1Bill"].Value = gBill;
                    tr.Cells["colS1Disc"].Value = gDisc;
                    tr.Cells["colS1Net"].Value = gNet;
                    tr.Cells["colS1Avg"].Value = gInv > 0 ? gNet / gInv : 0;
                    ReportBase.StyleTotalRow(tr, ReportBase.Blue);
                }

                lblBar.Text = $"  Purchase Summary ({group})  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {gInv} invoices  ·  Net: Rs. {gNet:N2}";
                lblEmpty.Visible = dgvReport.Rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private static int GetWeek(DateTime d) =>
            CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(d,
                CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}