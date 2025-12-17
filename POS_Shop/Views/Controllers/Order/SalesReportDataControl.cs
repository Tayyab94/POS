using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_Shop.Views.Controllers.Order
{
    public partial class SalesReportDataControl : UserControl
    {
        public SalesReportDataControl()
        {
            InitializeComponent();
            SetupChart();
        }

        private void SalesChartForm_Load(object sender, EventArgs e)
        {
            // Initialize date pickers with default values
            dtpFromDate.Value = DateTime.Today.AddDays(-7);
            dtpToDate.Value = DateTime.Today;

            // Initialize statistics labels
            lblTotalRevenueValue.Text = "0.00";
            lblAvgSalesValue.Text = "0.00";
            lblTotalOrdersValue.Text = "0";

            // Generate initial report
            GenerateReport();
        }

        private void SetupChart()
        {
            // DON'T clear the series here - the designer already created it!
            // WeeklySaleChart.Series.Clear(); // REMOVE THIS LINE
            WeeklySaleChart.ChartAreas.Clear();

            // Configure chart area
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.LabelStyle.Format = "N0"; // Remove $ sign, use number format
            WeeklySaleChart.ChartAreas.Add(chartArea);

            // Configure the existing series (named "Series1" from designer)
            Series series = WeeklySaleChart.Series["Series1"];
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.SteelBlue;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "N0"; // Remove $ sign, use number format
            series.Font = new Font("Arial", 9, FontStyle.Bold);
            series.ToolTip = "Date: #AXISLABEL\nAmount: #VALY{N0}";

            // Configure 3D effect
            chartArea.Area3DStyle.Enable3D = true;
            chartArea.Area3DStyle.IsClustered = true;

            // Configure axes
            chartArea.AxisX.Title = "Date";
            chartArea.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisY.Title = "Sales Amount";
            chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            DateTime fromDate = dtpFromDate.Value.Date;
            DateTime toDate = dtpToDate.Value.Date;

            // Validate date range
            if (fromDate > toDate)
            {
                MessageBox.Show("From Date cannot be greater than To Date",
                    "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Update status
            lblStatus.Text = "Generating report...";
            Application.DoEvents();

            try
            {
                // Get sales data
                var salesData = GetSalesData(fromDate, toDate);

                // Update chart
                UpdateChart(salesData, fromDate, toDate);

                // Update statistics
                UpdateStatistics(salesData);

                // Update status
                lblStatus.Text = $"Report generated: {fromDate:dd-MMM-yyyy} to {toDate:dd-MMM-yyyy}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Error generating report";
            }
        }

        private List<DailySalesData> GetSalesData(DateTime fromDate, DateTime toDate)
        {
            //using (var context = new POSDbContext())
            //{
            //    DateTime adjustedToDate = toDate.AddDays(1);

            //    // Get orders in date range
            //    var orders = context.Orders
            //        .AsNoTracking()
            //        .Where(o => o.CreatedDate >= fromDate && o.CreatedDate < adjustedToDate)
            //        .Select(o => new {
            //            Date = DbFunctions.TruncateTime(o.CreatedDate),
            //            o.TotalBill,
            //            OrderId = o.Id
            //        })
            //        .ToList();

            //    // Group by date
            //    var groupedData = orders
            //        .GroupBy(o => o.Date)
            //        .Select(g => new DailySalesData
            //        {
            //            Date = g.Key ?? fromDate,
            //            Sales = (decimal)g.Sum(o => o.TotalBill),
            //            OrderCount = g.Count()
            //        })
            //        .OrderBy(x => x.Date)
            //        .ToList();

            //    // Fill missing dates
            //    var allDates = new List<DailySalesData>();
            //    DateTime currentDate = fromDate;

            //    while (currentDate <= toDate)
            //    {
            //        var existingData = groupedData.FirstOrDefault(x => x.Date.Date == currentDate.Date);
            //        allDates.Add(existingData ?? new DailySalesData
            //        {
            //            Date = currentDate,
            //            Sales = 0m,
            //            OrderCount = 0
            //        });
            //        currentDate = currentDate.AddDays(1);
            //    }

            //    return allDates;
            //}

            using (var context = new POSDbContext())
            {
                DateTime adjustedToDate = toDate.Date.AddDays(1);
                DateTime adjustedFromDate = fromDate.Date;

                // Single query with grouping done in database
                var groupedData = context.Orders
                    .AsNoTracking()
                    .Where(o => o.CreatedDate >= adjustedFromDate && o.CreatedDate < adjustedToDate)
                    .GroupBy(o => DbFunctions.TruncateTime(o.CreatedDate))
                    .Select(g => new DailySalesData
                    {
                        Date = g.Key ?? adjustedFromDate,
                        Sales = g.Sum(o => o.TotalBill),
                        OrderCount = g.Count()
                    })
                    .ToList();

                // Convert to dictionary for O(1) lookup
                var dataByDate = groupedData.ToDictionary(x => x.Date.Date);

                // Fill missing dates with O(1) lookups
                var allDates = new List<DailySalesData>();
                for (DateTime currentDate = adjustedFromDate; currentDate <= toDate.Date; currentDate = currentDate.AddDays(1))
                {
                    if (dataByDate.TryGetValue(currentDate, out var existingData))
                    {
                        allDates.Add(existingData);
                    }
                    else
                    {
                        allDates.Add(new DailySalesData
                        {
                            Date = currentDate,
                            Sales = 0f,
                            OrderCount = 0
                        });
                    }
                }

                return allDates;
            }
        }

        private void UpdateChart(List<DailySalesData> salesData, DateTime fromDate, DateTime toDate)
        {
            // Clear existing points - Use the correct series name "Series1"
            WeeklySaleChart.Series["Series1"].Points.Clear();

            // Update chart title
            WeeklySaleChart.Titles[0].Text = $"Sales Report: {fromDate:dd-MMM} to {toDate:dd-MMM-yyyy}";

            // Add data points
            foreach (var data in salesData)
            {
                DataPoint point = new DataPoint();
                point.AxisLabel = data.Date.ToString("dd-MMM");
                point.YValues = new double[] { (double)data.Sales };
                point.Label = data.Sales.ToString("N0"); // No $ sign
                point.Color = GetColorForDate(data.Date, data.Sales);
                point.Tag = $"Orders: {data.OrderCount:N0}"; // Store additional info

                WeeklySaleChart.Series["Series1"].Points.Add(point);
            }
        }

        private void UpdateStatistics(List<DailySalesData> salesData)
        {
            if (!salesData.Any())
            {
                lblTotalRevenueValue.Text = "0.00";
                lblAvgSalesValue.Text = "0.00";
                lblTotalOrdersValue.Text = "0";
                return;
            }

            float totalSales = salesData.Sum(x => x.Sales);
            int totalOrders = salesData.Sum(x => x.OrderCount);
            int daysCount = salesData.Count;
            float avgSales = daysCount > 0 ? totalSales / daysCount : 0;

            // Update labels without $ sign
            lblTotalRevenueValue.Text = totalSales.ToString("N0");
            lblAvgSalesValue.Text = avgSales.ToString("N2");
            lblTotalOrdersValue.Text = totalOrders.ToString("N0");
        }

        private Color GetColorForDate(DateTime date, float sales)
        {
            // Different colors based on sales amount
            if (sales == 0)
                return Color.LightGray;
            else if (sales < 1000)
                return Color.LightSkyBlue;
            else if (sales < 5000)
                return Color.SteelBlue;
            else if (sales < 10000)
                return Color.MediumSeaGreen;
            else
                return Color.Goldenrod;
        }

        // Quick filter buttons
        private void btnLast7Days_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today.AddDays(-7);
            dtpToDate.Value = DateTime.Today;
            GenerateReport();
        }

        private void btnThisMonth_Click(object sender, EventArgs e)
        {
            dtpFromDate.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpToDate.Value = DateTime.Today;
            GenerateReport();
        }

        private void btnLastMonth_Click(object sender, EventArgs e)
        {
            DateTime firstDayOfLastMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
            DateTime lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);

            dtpFromDate.Value = firstDayOfLastMonth;
            dtpToDate.Value = lastDayOfLastMonth;
            GenerateReport();
        }

        // Data class
        private class DailySalesData
        {
            public DateTime Date { get; set; }
            public float Sales { get; set; }
            public int OrderCount { get; set; }
        }
    }
}
