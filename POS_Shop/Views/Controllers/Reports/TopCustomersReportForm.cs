using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 13 — Top Customers
    /// Ranks customers by total spend in the selected period.
    /// Walk-in (no customer) orders are grouped as "Walk-in / Anonymous".
    /// </summary>
    public partial class TopCustomersReportForm : Form
    {
        private readonly POSDbContext _db;

        public TopCustomersReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddMonths(-3);
            dtpTo.Value = DateTime.Today;
            ApplyGridStyles();
            WireEvents();
        }

        private void ApplyGridStyles()
        {
            ReportBase.StyleGrid(dgvReport, ReportBase.Orange);
            colTCRank.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTCRank.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colTCOrders.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colTCSpend.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTCSpend.DefaultCellStyle.Format = "N2";
            colTCSpend.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            colTCSpend.DefaultCellStyle.ForeColor = ReportBase.Orange;
            colTCAvg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTCAvg.DefaultCellStyle.Format = "N2";
            colTCShare.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colTCShare.DefaultCellStyle.Format = "N1";
            colTCLast.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            btnRun.FlatAppearance.BorderSize = btnPrint.FlatAppearance.BorderSize = btnClose.FlatAppearance.BorderSize = 0;
            btnRun.Cursor = btnPrint.Cursor = btnClose.Cursor = Cursors.Hand;
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(230, 81, 0), ReportBase.Orange);
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

                var orders = _db.Orders
                    .Include("Customer")
                    .Where(o => o.CreatedDate >= from && o.CreatedDate <= to)
                    .ToList();

                decimal totalRevenue = (decimal)orders.Sum(o => o.TotalBill);

                var groups = orders
                    .GroupBy(o => o.customerId)
                    .Select(g =>
                    {
                        string name = g.First().Customer != null
                            ? (g.First().Customer.CustomerName ?? $"Customer #{g.Key}")
                            : "Walk-in / Anonymous";
                        string phone = g.First().Customer?.ContactNo ?? "-";
                        decimal spend = (decimal)g.Sum(o => o.TotalBill);
                        int cnt = g.Count();
                        DateTime last = g.Max(o => o.CreatedDate);
                        return new { Name = name, Phone = phone, Orders = cnt, Spend = spend, Last = last };
                    })
                    .OrderByDescending(x => x.Spend)
                    .ToList();

                dgvReport.Rows.Clear();
                int rank = 1;
                decimal gSpend = 0;

                foreach (var r in groups)
                {
                    decimal share = totalRevenue > 0 ? (r.Spend / totalRevenue) * 100m : 0m;
                    decimal avg = r.Orders > 0 ? r.Spend / r.Orders : 0;
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colTCRank"].Value = rank++;
                    row.Cells["colTCName"].Value = r.Name;
                    row.Cells["colTCPhone"].Value = r.Phone;
                    row.Cells["colTCOrders"].Value = r.Orders;
                    row.Cells["colTCSpend"].Value = r.Spend;
                    row.Cells["colTCAvg"].Value = avg;
                    row.Cells["colTCShare"].Value = share;
                    row.Cells["colTCLast"].Value = r.Last.ToString("dd MMM yyyy");
                    // Top 3 get gold/silver/bronze tint
                    if (rank == 2) row.DefaultCellStyle.BackColor = Color.FromArgb(255, 253, 231); // gold
                    if (rank == 3) row.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250); // silver
                    gSpend += r.Spend;
                }

                if (groups.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colTCName"].Value = $"GRAND TOTAL  ({groups.Count} customers)";
                    tr.Cells["colTCOrders"].Value = orders.Count;
                    tr.Cells["colTCSpend"].Value = gSpend;
                    tr.Cells["colTCShare"].Value = 100m;
                    ReportBase.StyleTotalRow(tr, ReportBase.Orange);
                }

                lblBar.Text = $"  Top Customers  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {groups.Count} customers  ·  Total Revenue: Rs. {gSpend:N2}";
                lblEmpty.Visible = groups.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}