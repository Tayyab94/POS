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
    /// Report 6 — Supplier Performance Summary
    /// One row per supplier: total purchased, paid, outstanding, invoices, last date, status
    /// </summary>
    public partial class SupplierPerformanceReportForm : Form
    {
        private readonly POSDbContext _db;

        public SupplierPerformanceReportForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            dtpFrom.Value = DateTime.Today.AddYears(-1);
            dtpTo.Value = DateTime.Today;
            WireEvents();
        }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            ReportBase.Hover(btnRun, Color.FromArgb(80, 0, 120), ReportBase.Purple);
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

                var suppliers = _db.Suppliers.Where(s => !s.IsDeleted).ToList();
                var purchases = _db.Purchases
                    .Where(p => !p.IsDeleted && p.PurchaseDate >= from && p.PurchaseDate <= to)
                    .ToList();

                var rows = suppliers
                    .Select(s =>
                    {
                        var sp = purchases.Where(p => p.SupplierId == s.Id).ToList();
                        return new
                        {
                            Id = s.Id,
                            Name = s.SupplierName,
                            Shop = s.ShopName,
                            Contact = s.ContactNo,
                            Invoices = sp.Count,
                            NetSpend = sp.Sum(p => p.NetAmount),
                            Paid = sp.Sum(p => p.TotalPaid),
                            Outstanding = sp.Sum(p => p.Balance),
                            LastPurchase = sp.Any() ? sp.Max(p => p.PurchaseDate) : (DateTime?)null,
                            Status = sp.Any(p => p.Balance > 0) ? "Has Balance"
                                         : sp.Any() ? "Settled"
                                                                       : "No Activity"
                        };
                    })
                    .Where(x => x.Invoices > 0)
                    .OrderByDescending(x => x.NetSpend)
                    .ToList();

                dgvReport.Rows.Clear();
                decimal gNet = 0, gPaid = 0, gOut = 0;

                foreach (var r in rows)
                {
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Cells["colPSName"].Value = $"{r.Name}  —  {r.Shop}";
                    row.Cells["colPSContact"].Value = r.Contact;
                    row.Cells["colPSInv"].Value = r.Invoices;
                    row.Cells["colPSNet"].Value = r.NetSpend;
                    row.Cells["colPSPaid"].Value = r.Paid;
                    row.Cells["colPSOut"].Value = r.Outstanding;
                    row.Cells["colPSLast"].Value = r.LastPurchase?.ToString("dd MMM yyyy") ?? "—";
                    row.Cells["colPSStatus"].Value = r.Status;

                    if (r.Outstanding > 0)
                        row.Cells["colPSOut"].Style.ForeColor = ReportBase.Red;

                    row.Cells["colPSStatus"].Style.ForeColor =
                        r.Status == "Settled" ? ReportBase.Green :
                        r.Status == "Has Balance" ? ReportBase.Orange :
                        Color.Gray;
                    row.Cells["colPSStatus"].Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

                    gNet += r.NetSpend; gPaid += r.Paid; gOut += r.Outstanding;
                }

                if (rows.Count > 0)
                {
                    var tr = dgvReport.Rows[dgvReport.Rows.Add()];
                    tr.Cells["colPSName"].Value = $"TOTAL  ({rows.Count} active suppliers)";
                    tr.Cells["colPSNet"].Value = gNet;
                    tr.Cells["colPSPaid"].Value = gPaid;
                    tr.Cells["colPSOut"].Value = gOut;
                    ReportBase.StyleTotalRow(tr, ReportBase.Purple);
                }

                lblBar.Text = $"  Supplier Performance  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  {rows.Count} active suppliers  ·  Outstanding: Rs. {gOut:N2}";
                lblEmpty.Visible = rows.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _db.Dispose(); }
    }
}