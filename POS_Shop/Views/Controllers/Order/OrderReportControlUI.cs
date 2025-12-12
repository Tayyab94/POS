using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_Shop.Views.Controllers.Order
{
    public partial class OrderReportControlUI : UserControl
    {
        public OrderReportControlUI()
        {
            InitializeComponent();
            this.Load += OrderReportControlUI_Load;
            SetupChart();
            LoadWeeklySalesData();
        }
        private async void OrderReportControlUI_Load(object sender, EventArgs e)
        {
            using (var context = new POSDbContext())
            {
                var today = DateTime.Today.Date;

                // Get tomorrow's date with time set to midnight
                var tomorrow = today.AddDays(1);
                var TodaySale = await Task.Run(() => context.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Sum(s => (float?)s.TotalBill) ?? 0f);
                // Update UI controls on the main thread
                this.Invoke(new Action(() =>
                {
                    TodayTotalOrderSaleLbl.Text = TodaySale.ToString();
                }));


            }
        }



        private void SetupChart()
        {
            // Clear any existing series and chart areas
            WeeklySaleChart.Series.Clear();
            WeeklySaleChart.ChartAreas.Clear();

            // Create and configure chart area
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            WeeklySaleChart.ChartAreas.Add(chartArea);

            // Create series for bar chart
            Series series = new Series("Weekly Sales");
            series.ChartType = SeriesChartType.Column; // Bar chart
            series.Color = Color.SteelBlue;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "C0"; // Currency format
            series.Font = new Font("Arial", 10, FontStyle.Bold);

            WeeklySaleChart.Series.Add(series);

            // Chart title
            WeeklySaleChart.Titles.Clear();
            Title title = new Title("Weekly Sales Summary", Docking.Top, new Font("Arial", 14, FontStyle.Bold), Color.Black);
            WeeklySaleChart.Titles.Add(title);

            // Set 3D effect for better appearance
            chartArea.Area3DStyle.Enable3D = true;
            chartArea.Area3DStyle.IsClustered = true;

            // Customize X-axis
            WeeklySaleChart.ChartAreas[0].AxisX.Title = "Days of Week";
            WeeklySaleChart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);

            // Customize Y-axis
            WeeklySaleChart.ChartAreas[0].AxisY.Title = "Sales Amount";
            WeeklySaleChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            //WeeklySaleChart.ChartAreas[0].AxisY.LabelStyle.Format = "C0";
        }

        private void LoadWeeklySalesData()
        {
            try
            {
                // Get weekly sales data
                var weeklySales = GetWeeklySalesData();

                // Clear existing data points
                WeeklySaleChart.Series["Weekly Sales"].Points.Clear();

                // Add data points to chart
                foreach (var day in weeklySales)
                {
                    DataPoint point = new DataPoint();
                    point.AxisLabel = day.Day;
                    point.YValues = new double[] { (double)day.Sales };
                    point.Label = day.Sales.ToString();
                    point.Color = GetColorForDay(day.Day);

                    WeeklySaleChart.Series["Weekly Sales"].Points.Add(point);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading sales data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //private List<DailySales> GetWeeklySalesData()
        //{
        //    // This method gets data from your database
        //    // Replace 'YourDbContext' with your actual DbContext class name

        //    List<DailySales> weeklySales = new List<DailySales>();

        //    try
        //    {
        //        using (var context = new POSDbContext()) // CHANGE THIS to your DbContext name
        //        {
        //            // Calculate start of current week (Sunday)
        //            DateTime startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        //            // Calculate end of current week (Saturday)
        //            DateTime endDate = startDate.AddDays(7);

        //            // Query to get sales data grouped by day of week
        //            var salesData = context.Orders
        //                .AsNoTracking()
        //                .Where(o => o.CreatedDate >= startDate && o.CreatedDate < endDate)
        //                .GroupBy(o => o.CreatedDate.DayOfWeek)
        //                .Select(g => new
        //                {
        //                    DayOfWeek = g.Key,
        //                    DayName = g.Key.ToString(),
        //                    TotalSales = g.Sum(o => o.TotalBill)
        //                })
        //                .OrderBy(x => x.DayOfWeek)
        //                .ToList();

        //            // Convert to DailySales list
        //            foreach (var item in salesData)
        //            {
        //                weeklySales.Add(new DailySales
        //                {
        //                    Day = item.DayName,
        //                    Sales = (decimal)item.TotalSales
        //                });
        //            }

        //            // If no data found, show sample data
        //            if (weeklySales.Count == 0)
        //            {
        //                weeklySales = GetSampleData();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // If there's an error (like database connection), show sample data
        //        MessageBox.Show("Using sample data. Database error: " + ex.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        weeklySales = GetSampleData();
        //    }

        //    return weeklySales;
        //}


        //private List<DailySales> GetWeeklySalesData()
        //{
        //    try
        //    {
        //        using (var context = new POSDbContext())
        //        {
        //            // Calculate start of current week (Sunday)
        //            DateTime startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
        //            DateTime endDate = startDate.AddDays(7);

        //            // Get all 7 days with SQL aggregation
        //            var salesByDate = context.Orders
        //                .AsNoTracking()
        //                .Where(o => o.CreatedDate >= startDate &&
        //                           o.CreatedDate < endDate)
        //                .GroupBy(o => EntityFunctions.TruncateTime(o.CreatedDate))
        //                .Select(g => new
        //                {
        //                    Date = g.Key,
        //                    TotalSales = g.Sum(o => o.TotalBill)  // Handle nulls in SQL
        //                })
        //                .ToList();  // Only ~7 rows returned!

        //            // Create full week structure (Sunday to Saturday)
        //            var weeklySales = new List<DailySales>();

        //            for (int i = 0; i < 7; i++)
        //            {
        //                DateTime currentDate = startDate.AddDays(i);
        //                var dayData = salesByDate.FirstOrDefault(x => x.Date == currentDate);

        //                weeklySales.Add(new DailySales
        //                {
        //                    Day = currentDate.DayOfWeek.ToString(),
        //                    Sales =(decimal)( dayData?.TotalSales ?? 0)  // Default to 0 if no sales
        //                });
        //            }

        //            // If no sales at all, show sample data
        //            if (weeklySales.All(x => x.Sales == 0))
        //            {
        //                return GetSampleData();
        //            }

        //            return weeklySales;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Using sample data. Database error: " + ex.Message,
        //                       "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return GetSampleData();
        //    }
        //}

        private List<DailySales> GetWeeklySalesData()
        {
            try
            {
                using (var context = new POSDbContext())
                {
                    DateTime startDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                    DateTime endDate = startDate.AddDays(7);

                    // Load only date and amount (minimal data)
                    var weekOrders = context.Orders
                        .AsNoTracking()
                        .Where(o => o.CreatedDate >= startDate && o.CreatedDate < endDate)
                        .Select(o => new { o.CreatedDate, o.ReceiveAmount })
                        .ToList();

                    // Group in memory
                    var salesByDate = weekOrders
                        .GroupBy(o => o.CreatedDate.Date)
                        .Select(g => new
                        {
                            Date = g.Key,
                            TotalSales =(decimal)g.Sum(o => o.ReceiveAmount)  // ✅ Handle nulls here
                        })
                        .ToList();

                    var weeklySales = new List<DailySales>();

                    for (int i = 0; i < 7; i++)
                    {
                        DateTime currentDate = startDate.AddDays(i);
                        var dayData = salesByDate.FirstOrDefault(x => x.Date == currentDate);

                        weeklySales.Add(new DailySales
                        {
                            Day = currentDate.DayOfWeek.ToString(),
                            Sales = dayData?.TotalSales ?? 0m  // ✅ FIXED: Default to 0m if null
                        });
                    }

                    if (weeklySales.All(x => x.Sales == 0))
                    {
                        return GetSampleData();
                    }

                    return weeklySales;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Using sample data. Database error: " + ex.Message,
                               "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return GetSampleData();
            }
        }

        // Sample data in case database is not available
        private List<DailySales> GetSampleData()
        {
            return new List<DailySales>
            {
                new DailySales { Day = "Sunday", Sales = 1200 },
                new DailySales { Day = "Monday", Sales = 1800 },
                new DailySales { Day = "Tuesday", Sales = 2200 },
                new DailySales { Day = "Wednesday", Sales = 1900 },
                new DailySales { Day = "Thursday", Sales = 2500 },
                new DailySales { Day = "Friday", Sales = 3200 },
                new DailySales { Day = "Saturday", Sales = 2800 }
            };
        }


        private Color GetColorForDay(string day)
        {
            // Different colors for each day
            switch (day.ToLower())
            {
                case "sunday": return Color.LightCoral;
                case "monday": return Color.LightSkyBlue;
                case "tuesday": return Color.LightGreen;
                case "wednesday": return Color.Khaki;
                case "thursday": return Color.Plum;
                case "friday": return Color.LightSalmon;
                case "saturday": return Color.MediumAquamarine;
                default: return Color.SteelBlue;
            }
        }


        public class DailySales
        {
            public string Day { get; set; }
            public decimal Sales { get; set; }
        }
    }
}
