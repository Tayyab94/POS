using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Report 8 — Top Unpaid Suppliers
    /// Real-time ranked list of suppliers you owe the most money to RIGHT NOW.
    /// </summary>
    public partial class TopUnpaidReportForm : Form
    {
        private readonly POSDbContext _db;

        public TopUnpaidReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            WireEvents();
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(140, 20, 20), ReportBase.Red);
            ReportBase.Hover(btnPrint, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110));
            ReportBase.Hover(btnClose, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110));
            KeyPreview = true;
            KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
        }

        private void RunReport()
        {
            try
            {
                DateTime today = DateTime.Today;

                var data = _db.Purchases
                    .Include("Supplier")
                    .Where(p => !p.IsDeleted && p.Balance > 0)
                    .ToList()
                    .GroupBy(p => p.SupplierId)
                    .Select(g =>
                    {
                        int oldest = g.Any() ? (today - g.Min(p => p.PurchaseDate.Date)).Days : 0;
                        return new
                        {
                            Supplier = g.First().Supplier?.SupplierName ?? $"#{g.Key}",
                            Shop = g.First().Supplier?.ShopName ?? "",
                            Contact = g.First().Supplier?.ContactNo ?? "",
                            OpenInvoices = g.Count(),
                            TotalOwed = g.Sum(p => p.Balance),
                            OldestDays = oldest,
                            PendingCount = g.Count(p => p.PaymentStatus == PurchasePaymentStatus.Pending),
                            PartialCount = g.Count(p => p.PaymentStatus == PurchasePaymentStatus.PartiallyPaid)
                        };
                    })
                    .OrderByDescending(x => x.TotalOwed)
                    .ToList();

                dgvReport.Rows.Clear();
                decimal grandOwed = 0;
                int rank = 1;

                foreach (var r in data)
                {
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colTURank"].Value = rank++;
                    row.Cells["colTUSupplier"].Value = $"{r.Supplier}  —  {r.Shop}";
                    row.Cells["colTUContact"].Value = r.Contact;
                    row.Cells["colTUOpenInv"].Value = r.OpenInvoices;
                    row.Cells["colTUOwed"].Value = r.TotalOwed;
                    row.Cells["colTUOldest"].Value = $"{r.OldestDays} days";
                    row.Cells["colTUPending"].Value = r.PendingCount;
                    row.Cells["colTUPartial"].Value = r.PartialCount;

                    Color ageColor = r.OldestDays > 90 ? Color.FromArgb(183, 28, 28)
                                   : r.OldestDays > 60 ? ReportBase.Orange
                                   : r.OldestDays > 30 ? Color.FromArgb(245, 124, 0)
                                   : ReportBase.Green;

                    row.Cells["colTUOldest"].Style.ForeColor = ageColor;
                    row.Cells["colTUOldest"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    row.Cells["colTUOwed"].Style.ForeColor = ReportBase.Red;
                    row.Cells["colTUOwed"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

                    grandOwed += r.TotalOwed;
                }

                if (data.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colTURank"].Value = "";
                    tr.Cells["colTUSupplier"].Value = $"TOTAL OUTSTANDING  ({data.Count} suppliers)";
                    tr.Cells["colTUOwed"].Value = grandOwed;
                    ReportBase.StyleTotalRow(tr, ReportBase.Red);
                }

                lblBar.Text = $"  Top Unpaid Suppliers  ·  As of {today:dd MMM yyyy}  ·  {data.Count} suppliers owe  ·  Total: Rs. {grandOwed:N2}";
                lblEmpty.Visible = data.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}