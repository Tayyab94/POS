using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 11 — Sales Summary
    /// Total orders, revenue, and average order value grouped by Daily / Weekly / Monthly
    /// </summary>
    public partial class SalesSummaryReportForm : Form
    {
        private readonly POSDbContext _db;

        public SalesSummaryReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
            cmbGroup.SelectedIndex = 0; // Monthly
            ApplyGridStyles();
            WireEvents();
        }

        private void ApplyGridStyles()
        {
            ReportBase.StyleGrid(dgvReport, ReportBase.Green);
            colSalPeriod.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colSalOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSalRevenue.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalRevenue.DefaultCellStyle.Format = "N2";
            colSalRevenue.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colSalRevenue.DefaultCellStyle.ForeColor = ReportBase.Green;
            colSalCash.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalCash.DefaultCellStyle.Format = "N2";
            colSalCredit.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalCredit.DefaultCellStyle.Format = "N2";
            colSalAvg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSalAvg.DefaultCellStyle.Format = "N2";
            btnRun.FlatAppearance.BorderSize = btnPrint.FlatAppearance.BorderSize = btnClose.FlatAppearance.BorderSize = 0;
            btnRun.Cursor = btnPrint.Cursor = btnClose.Cursor = Cursors.Hand;
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(27, 94, 32), ReportBase.Green);
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
                string grp = cmbGroup.SelectedItem?.ToString() ?? "Monthly";

                var orders = _db.Orders
                    .Where(o => o.CreatedDate >= from && o.CreatedDate <= to)
                    .ToList();

                dgvReport.Rows.Clear();
                decimal gRev = 0, gCash = 0, gCredit = 0; int gOrders = 0;

                if (grp == "Monthly")
                {
                    var groups = orders.GroupBy(o => o.CreatedDate.ToString("yyyy-MM"))
                                       .OrderBy(g => g.Key).ToList();
                    foreach (var g in groups)
                    {
                        decimal rev = (decimal)g.Sum(o => o.TotalBill);
                        decimal cash = (decimal)g.Where(o => o.paymentType == "Cash").Sum(o => o.TotalBill);
                        decimal credit = (decimal)g.Where(o => o.paymentType != "Cash").Sum(o => o.TotalBill);
                        int cnt = g.Count();
                        DateTime d = DateTime.ParseExact(g.Key, "yyyy-MM", null);
                        AddRow(d.ToString("MMM yyyy"), cnt, rev, cash, credit);
                        gRev += rev; gCash += cash; gCredit += credit; gOrders += cnt;
                    }
                }
                else if (grp == "Weekly")
                {
                    var groups = orders.GroupBy(o =>
                    {
                        var mon = o.CreatedDate.Date.AddDays(-(int)o.CreatedDate.DayOfWeek + (int)DayOfWeek.Monday);
                        return mon.ToString("yyyy-MM-dd");
                    }).OrderBy(g => g.Key).ToList();
                    foreach (var g in groups)
                    {
                        decimal rev = (decimal)g.Sum(o => o.TotalBill);
                        decimal cash = (decimal)g.Where(o => o.paymentType == "Cash").Sum(o => o.TotalBill);
                        decimal credit = (decimal)g.Where(o => o.paymentType != "Cash").Sum(o => o.TotalBill);
                        int cnt = g.Count();
                        DateTime d = DateTime.Parse(g.Key);
                        AddRow($"Wk {d:dd MMM} – {d.AddDays(6):dd MMM}", cnt, rev, cash, credit);
                        gRev += rev; gCash += cash; gCredit += credit; gOrders += cnt;
                    }
                }
                else // Daily
                {
                    var groups = orders.GroupBy(o => o.CreatedDate.Date)
                                       .OrderBy(g => g.Key).ToList();
                    foreach (var g in groups)
                    {
                        decimal rev = (decimal)g.Sum(o => o.TotalBill);
                        decimal cash = (decimal)g.Where(o => o.paymentType == "Cash").Sum(o => o.TotalBill);
                        decimal credit = (decimal)g.Where(o => o.paymentType != "Cash").Sum(o => o.TotalBill);
                        int cnt = g.Count();
                        AddRow(g.Key.ToString("dd MMM yyyy"), cnt, rev, cash, credit);
                        gRev += rev; gCash += cash; gCredit += credit; gOrders += cnt;
                    }
                }

                if (dgvReport.Rows.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colSalPeriod"].Value = $"TOTAL  ({dgvReport.Rows.Count - 1} {grp.ToLower()} periods)";
                    tr.Cells["colSalOrders"].Value = gOrders;
                    tr.Cells["colSalRevenue"].Value = gRev;
                    tr.Cells["colSalCash"].Value = gCash;
                    tr.Cells["colSalCredit"].Value = gCredit;
                    tr.Cells["colSalAvg"].Value = gOrders > 0 ? gRev / gOrders : 0;
                    ReportBase.StyleTotalRow(tr, ReportBase.Green);
                }

                lblBar.Text = $"  Sales Summary  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {gOrders} orders  ·  Revenue: Rs. {gRev:N2}";
                lblEmpty.Visible = dgvReport.Rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void AddRow(string period, int orders, decimal rev, decimal cash, decimal credit)
        {
            var row = dgvReport.Rows[dgvReport.Rows.Add()];
            row.Cells["colSalPeriod"].Value = period;
            row.Cells["colSalOrders"].Value = orders;
            row.Cells["colSalRevenue"].Value = rev;
            row.Cells["colSalCash"].Value = cash;
            row.Cells["colSalCredit"].Value = credit;
            row.Cells["colSalAvg"].Value = orders > 0 ? rev / orders : 0;
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}