using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Reports
{
    public partial class frmPurchaseSummaryByPeriod : Form
    {
        public frmPurchaseSummaryByPeriod()
        {
            InitializeComponent();
        }

        private void frmPurchaseSummaryByPeriod_Load(object sender, EventArgs e)
        {
            dtpStart.Value = DateTime.Today.AddMonths(-3);
            dtpEnd.Value = DateTime.Today;
            cmbPeriod.SelectedIndex = 2; // default to Monthly
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (dtpStart.Value > dtpEnd.Value)
            {
                MessageBox.Show("Start date cannot be after end date.", "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string period = cmbPeriod.SelectedItem?.ToString() ?? "Monthly";

            try
            {
                var reportData = GetPurchaseSummaryByPeriod(dtpStart.Value, dtpEnd.Value, period);

                dgvReport.DataSource = reportData;

                // Optional: format columns
                if (dgvReport.Columns["TotalSpending"] != null)
                {
                    dgvReport.Columns["TotalSpending"].DefaultCellStyle.Format = "N2";
                    dgvReport.Columns["AverageInvoiceValue"].DefaultCellStyle.Format = "N2";
                }

                lblRecordCount.Text = $"Records: {reportData.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<PurchaseSummaryDto> GetPurchaseSummaryByPeriod(DateTime start, DateTime end, string period)
        {
            using (var db = new POSDbContext())
            {
                var purchases = db.Purchases
                    .Where(p => !p.IsDeleted &&
                                p.PurchaseDate >= start &&
                                p.PurchaseDate <= end)
                    .ToList(); // materialize because of date grouping limitations in EF6

                switch (period.ToLower())
                {
                    case "daily":
                        return (from p in purchases
                                group p by p.PurchaseDate.Date into g
                                orderby g.Key
                                select new PurchaseSummaryDto
                                {
                                    Period = g.Key.ToString("yyyy-MM-dd"),
                                    TotalSpending = g.Sum(x => x.NetAmount),
                                    InvoiceCount = g.Count(),
                                    AverageInvoiceValue = g.Average(x => x.NetAmount)
                                }).ToList();

                    case "weekly":
                        return (from p in purchases
                                let week = System.Globalization.CultureInfo.InvariantCulture.Calendar
                                    .GetWeekOfYear(p.PurchaseDate, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                let year = p.PurchaseDate.Year
                                group p by new { Year = year, Week = week } into g
                                orderby g.Key.Year, g.Key.Week
                                select new PurchaseSummaryDto
                                {
                                    Period = $"Week {g.Key.Week:D2}, {g.Key.Year}",
                                    TotalSpending = g.Sum(x => x.NetAmount),
                                    InvoiceCount = g.Count(),
                                    AverageInvoiceValue = g.Average(x => x.NetAmount)
                                }).ToList();

                    case "monthly":
                    default:
                        return (from p in purchases
                                group p by new { p.PurchaseDate.Year, p.PurchaseDate.Month } into g
                                orderby g.Key.Year, g.Key.Month
                                select new PurchaseSummaryDto
                                {
                                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                                    TotalSpending = g.Sum(x => x.NetAmount),
                                    InvoiceCount = g.Count(),
                                    AverageInvoiceValue = g.Average(x => x.NetAmount)
                                }).ToList();
                }
            }
        }
    }

    // DTO - put this in a shared folder (e.g. Models/Reports)
    public class PurchaseSummaryDto
    {
        public string Period { get; set; }
        public decimal TotalSpending { get; set; }
        public int InvoiceCount { get; set; }
        public decimal AverageInvoiceValue { get; set; }
    }
}
