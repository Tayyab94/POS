////using POS_Shop.Models;
////using POS_Shop.Views.Controllers.Order;
////using POS_Shop.Views.Reports;
////using System;
////using System.Data;
////using System.Linq;
////using System.Threading.Tasks;
////using System.Windows.Forms;

////namespace POS_Shop.Views.Controllers
////{
////    public partial class HomeControlUI : UserControl
////    {
////        public HomeControlUI()
////        {
////            InitializeComponent();
////            this.Load += HomeControlUI_Load;

////        }

////        private async void HomeControlUI_Load(object sender, EventArgs e)
////        {
////            using (var context = new POSDbContext())
////            {
////                var today = DateTime.Today.Date;

////                // Get tomorrow's date with time set to midnight
////                var tomorrow = today.AddDays(1);
////                var TodayOrderCount = await Task.Run(() => context.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Count());
////                var TodaySale = await Task.Run(() => context.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Sum(s => (float?)s.TotalBill) ?? 0f);
////                var TodayTempOrderCount = await Task.Run(() => context.TempOrders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Count());
////                //// Update UI controls on the main thread
////                //this.Invoke(new Action(() =>
////                //{
////                //    TodayTotalOrderLbl.Text = TodayOrderCount.ToString();
////                //    TempTotalOrderLbl.Text = TodayTempOrderCount.ToString();
////                //}));

////                // Use BeginInvoke with IsHandleCreated check
////                if (this.IsHandleCreated)
////                {
////                    this.BeginInvoke(new Action(() =>
////                    {
////                        TodayTotalOrderLbl.Text = TodayOrderCount.ToString();
////                        TempTotalOrderLbl.Text = TodayTempOrderCount.ToString();
////                    }));
////                }
////            }
////        }



////        private void ReportAnalysisLblBtn_Click(object sender, EventArgs e)
////        {
////            bool showDialog = true;
////            while (showDialog)
////            {
////                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
////                {
////                    if (dialog.ShowDialog() != DialogResult.OK) return;

////                    string userInput = dialog.InputValue;
////                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
////                    {
////                        showDialog = false;
////                        Form ProductForm = new Form();
////                        ProductForm.Text = "Order Report Form";
////                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

////                        // Create an instance of your User Control
////                        // Replace 'YourUserControl' with the actual name of your User Control

////                        var FormCtrl = new SalesReportDataControl();
////                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

////                        // Add the User Control to the new Form's controls collection
////                        ProductForm.Controls.Add(FormCtrl);
////                        ProductForm.Width = 890; ProductForm.Height = 625;
////                        // Show the new form
////                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
////                    }
////                    else
////                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                }
////            }

////        }

////        private void SalesReportBtn_Click(object sender, EventArgs e)
////        {
////            bool showDialog = true;
////            while (showDialog)
////            {
////                using (var dialog = new InputDialog("Enter Password:", title: "Sales Chart Report", isTextBoxProtected: true))
////                {
////                    if (dialog.ShowDialog() != DialogResult.OK) return;

////                    string userInput = dialog.InputValue;
////                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
////                    {
////                        showDialog = false;
////                        var saleChartForm = new SalesChartForm();
////                        saleChartForm.ShowDialog();
////                        this.Show();
////                    }
////                    else
////                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                }
////            }

////        }

////        private void ProductSaleTrendBtn_Click(object sender, EventArgs e)
////        {
////            bool showDialog = true;
////            while (showDialog)
////            {
////                using (var dialog = new InputDialog("Enter Password:", title: "Sales Chart Report", isTextBoxProtected: true))
////                {
////                    if (dialog.ShowDialog() != DialogResult.OK) return;

////                    string userInput = dialog.InputValue;
////                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
////                    {
////                        showDialog = false;
////                        var saleChartForm = new ProductSalesTrendForm();
////                        saleChartForm.ShowDialog();
////                        this.Show();
////                    }
////                    else
////                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

////                }
////            }
////        }



////        private void WeeklyReportAnalysisBtn_Click(object sender, EventArgs e)
////        {
////            bool showDialog = true;
////            while (showDialog)
////            {
////                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
////                {
////                    if (dialog.ShowDialog() != DialogResult.OK) return;

////                    string userInput = dialog.InputValue;
////                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
////                    {
////                        showDialog = false;
////                        Form ProductForm = new Form();
////                        ProductForm.Text = "Order Report Form";
////                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

////                        // Create an instance of your User Control
////                        // Replace 'YourUserControl' with the actual name of your User Control
////                        var FormCtrl = new OrderReportControlUI();

////                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

////                        // Add the User Control to the new Form's controls collection
////                        ProductForm.Controls.Add(FormCtrl);
////                        ProductForm.Width = 750; ProductForm.Height = 525;
////                        // Show the new form
////                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
////                    }
////                    else
////                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                }
////            }

////        }

////        private void BtnSalesAanlysis_Click(object sender, EventArgs e)
////        {
////            bool showDialog = true;
////            while (showDialog)
////            {
////                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
////                {
////                    if (dialog.ShowDialog() != DialogResult.OK) return;

////                    string userInput = dialog.InputValue;
////                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
////                    {
////                        showDialog = false;
////                        Form ProductForm = new Form();
////                        ProductForm.Text = "Order Sales Report Analysis";
////                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

////                        // Create an instance of your User Control
////                        // Replace 'YourUserControl' with the actual name of your User Control

////                        var FormCtrl = new SalesReportDataControl();
////                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

////                        // Add the User Control to the new Form's controls collection
////                        ProductForm.Controls.Add(FormCtrl);
////                        ProductForm.Width = 890; ProductForm.Height = 625;
////                        // Show the new form
////                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
////                    }
////                    else
////                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                }
////            }
////        }

////        private void BtnWeeklySalesChart_Click(object sender, EventArgs e)
////        {
////            bool showDialog = true;
////            while (showDialog)
////            {
////                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
////                {
////                    if (dialog.ShowDialog() != DialogResult.OK) return;

////                    string userInput = dialog.InputValue;
////                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
////                    {
////                        showDialog = false;
////                        Form ProductForm = new Form();
////                        ProductForm.Text = "Weekly Order Report Analysis";
////                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

////                        // Create an instance of your User Control
////                        // Replace 'YourUserControl' with the actual name of your User Control
////                        var FormCtrl = new OrderReportControlUI();

////                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

////                        // Add the User Control to the new Form's controls collection
////                        ProductForm.Controls.Add(FormCtrl);
////                        ProductForm.Width = 750; ProductForm.Height = 525;
////                        // Show the new form
////                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
////                    }
////                    else
////                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                }
////            }
////        }

////        private void PurchaseReportsBtn_Click(object sender, EventArgs e)
////        {
////            bool showDialog = true;
////            while (showDialog)
////            {
////                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
////                {
////                    if (dialog.ShowDialog() != DialogResult.OK) return;

////                    string userInput = dialog.InputValue;
////                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
////                    {
////                        showDialog = false;

////                        var purchaseOrderForm = new POS_Shop.Views.Controllers.Reports.FormReportsMenu();

////                        this.Hide();
////                        purchaseOrderForm.ShowDialog();
////                        this.Show();

////                    }
////                    else
////                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
////                }
////            }
////        }
////    }
////}



//using POS_Shop.Interfaces;
//using POS_Shop.Models;
//using POS_Shop.Repositories;
//using POS_Shop.Views.Controllers.Order;
//using POS_Shop.Views.Reports;
//using System;
//using System.Data;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Controllers
//{
//    public partial class HomeControlUI : UserControl
//    {
//        private static readonly ILicenseService _licenseService = new LicenseService();

//        public HomeControlUI()
//        {
//            InitializeComponent();
//            this.Load += HomeControlUI_Load;

//        }

//        private async void HomeControlUI_Load(object sender, EventArgs e)
//        {
//            using (var context = new POSDbContext())
//            {
//                var today = DateTime.Today.Date;

//                // Get tomorrow's date with time set to midnight
//                var tomorrow = today.AddDays(1);
//                var TodayOrderCount = await Task.Run(() => context.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Count());
//                var TodaySale = await Task.Run(() => context.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Sum(s => (float?)s.TotalBill) ?? 0f);
//                var TodayTempOrderCount = await Task.Run(() => context.TempOrders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Count());
//                //// Update UI controls on the main thread
//                //this.Invoke(new Action(() =>
//                //{
//                //    TodayTotalOrderLbl.Text = TodayOrderCount.ToString();
//                //    TempTotalOrderLbl.Text = TodayTempOrderCount.ToString();
//                //}));

//                // Use BeginInvoke with IsHandleCreated check
//                if (this.IsHandleCreated)
//                {
//                    this.BeginInvoke(new Action(() =>
//                    {
//                        TodayTotalOrderLbl.Text = TodayOrderCount.ToString();
//                        TempTotalOrderLbl.Text = TodayTempOrderCount.ToString();
//                    }));
//                }
//            }
//        }



//        private void ReportAnalysisLblBtn_Click(object sender, EventArgs e)
//        {
//            bool showDialog = true;
//            while (showDialog)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;

//                    string userInput = dialog.InputValue;
//                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
//                    {
//                        showDialog = false;
//                        Form ProductForm = new Form();
//                        ProductForm.Text = "Order Report Form";
//                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

//                        // Create an instance of your User Control
//                        // Replace 'YourUserControl' with the actual name of your User Control

//                        var FormCtrl = new SalesReportDataControl();
//                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//                        // Add the User Control to the new Form's controls collection
//                        ProductForm.Controls.Add(FormCtrl);
//                        ProductForm.Width = 890; ProductForm.Height = 625;
//                        // Show the new form
//                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
//                    }
//                    else
//                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }

//        }

//        private void SalesReportBtn_Click(object sender, EventArgs e)
//        {
//            bool showDialog = true;
//            while (showDialog)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: "Sales Chart Report", isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;

//                    string userInput = dialog.InputValue;
//                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
//                    {
//                        showDialog = false;
//                        var saleChartForm = new SalesChartForm();
//                        saleChartForm.ShowDialog();
//                        this.Show();
//                    }
//                    else
//                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }

//        }

//        private void ProductSaleTrendBtn_Click(object sender, EventArgs e)
//        {
//            bool showDialog = true;
//            while (showDialog)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: "Sales Chart Report", isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;

//                    string userInput = dialog.InputValue;
//                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
//                    {
//                        showDialog = false;
//                        var saleChartForm = new ProductSalesTrendForm();
//                        saleChartForm.ShowDialog();
//                        this.Show();
//                    }
//                    else
//                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

//                }
//            }
//        }



//        private void WeeklyReportAnalysisBtn_Click(object sender, EventArgs e)
//        {
//            bool showDialog = true;
//            while (showDialog)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;

//                    string userInput = dialog.InputValue;
//                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
//                    {
//                        showDialog = false;
//                        Form ProductForm = new Form();
//                        ProductForm.Text = "Order Report Form";
//                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

//                        // Create an instance of your User Control
//                        // Replace 'YourUserControl' with the actual name of your User Control
//                        var FormCtrl = new OrderReportControlUI();

//                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//                        // Add the User Control to the new Form's controls collection
//                        ProductForm.Controls.Add(FormCtrl);
//                        ProductForm.Width = 750; ProductForm.Height = 525;
//                        // Show the new form
//                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
//                    }
//                    else
//                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }

//        }

//        private void BtnSalesAanlysis_Click(object sender, EventArgs e)
//        {
//           var licenseInfo= _licenseService.ReadLicenseFile();
//             if(licenseInfo.LicenseType== Models.LicenseModels.LicenseType.Trial)
//            {
//                MessageBox.Show("This action can't perfom on trail version", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

//                return;
//            }
//            bool showDialog = true;
//            while (showDialog)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;

//                    string userInput = dialog.InputValue;
//                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
//                    {
//                        showDialog = false;
//                        Form ProductForm = new Form();
//                        ProductForm.Text = "Order Sales Report Analysis";
//                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

//                        // Create an instance of your User Control
//                        // Replace 'YourUserControl' with the actual name of your User Control

//                        var FormCtrl = new SalesReportDataControl();
//                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//                        // Add the User Control to the new Form's controls collection
//                        ProductForm.Controls.Add(FormCtrl);
//                        ProductForm.Width = 890; ProductForm.Height = 625;
//                        // Show the new form
//                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
//                    }
//                    else
//                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void BtnWeeklySalesChart_Click(object sender, EventArgs e)
//        {
//            bool showDialog = true;
//            while (showDialog)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;

//                    string userInput = dialog.InputValue;
//                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
//                    {
//                        showDialog = false;
//                        Form ProductForm = new Form();
//                        ProductForm.Text = "Weekly Order Report Analysis";
//                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

//                        // Create an instance of your User Control
//                        // Replace 'YourUserControl' with the actual name of your User Control
//                        var FormCtrl = new OrderReportControlUI();

//                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

//                        // Add the User Control to the new Form's controls collection
//                        ProductForm.Controls.Add(FormCtrl);
//                        ProductForm.Width = 750; ProductForm.Height = 525;
//                        // Show the new form
//                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
//                    }
//                    else
//                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }

//        private void PurchaseReportsBtn_Click(object sender, EventArgs e)
//        {
//            bool showDialog = true;
//            while (showDialog)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;

//                    string userInput = dialog.InputValue;
//                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
//                    {
//                        showDialog = false;

//                        var purchaseOrderForm = new POS_Shop.Views.Controllers.Reports.FormReportsMenu();

//                        this.Hide();
//                        purchaseOrderForm.ShowDialog();
//                        this.Show();

//                    }
//                    else
//                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }
//    }
//}

using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using POS_Shop.Views.Controllers.Order;
using POS_Shop.Views.Reports;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_Shop.Views.Controllers
{
    public partial class HomeControlUI : UserControl
    {
        private static readonly ILicenseService _licenseService = new LicenseService();
        private const int LOW_STOCK_THRESHOLD = 10;

        public HomeControlUI()
        {
            InitializeComponent();
            this.Load += HomeControlUI_Load;
            this.Resize += HomeControlUI_Resize;
        }

        private void HomeControlUI_Resize(object sender, EventArgs e)
        {
            // Adjust layout when form resizes
            ArrangeControls();
        }

        private void ArrangeControls()
        {
            int width = this.ClientSize.Width - 40;
            int cardGap = 12;
            int cardW = (width - (5 * cardGap)) / 6;

            // Adjust card widths
            Panel[] cards = { CardTodayOrders, CardRevenue, CardTemp, CardProducts, CardCustomers, CardSuppliers };
            for (int i = 0; i < cards.Length; i++)
            {
                if (cards[i] != null && cardW > 100)
                {
                    cards[i].Width = cardW;
                    cards[i].Location = new Point(i * (cardW + cardGap), 5);

                    // Adjust label sizes
                    foreach (Control ctrl in cards[i].Controls)
                    {
                        if (ctrl is Label lbl)
                        {
                            lbl.Width = cardW - 16;
                            if (lbl.Text == "0" || lbl.Text.StartsWith("PKR"))
                            {
                                float fontSize = Math.Min(22f, Math.Max(12f, cardW / 8f));
                                if (lbl == LblRevenue) fontSize = Math.Min(14f, Math.Max(10f, cardW / 12f));
                                lbl.Font = new Font("Segoe UI", fontSize, FontStyle.Bold);
                            }
                        }
                    }
                }
            }

            // Adjust button widths
            int btnW = (width - (4 * cardGap)) / 5;
            Button[] actionBtns = { BtnSalesAnalysis, BtnWeeklySalesChart, BtnSalesCharts, BtnProductTrends, BtnPurchaseReports };
            for (int i = 0; i < actionBtns.Length; i++)
            {
                if (actionBtns[i] != null && btnW > 120)
                {
                    actionBtns[i].Width = btnW;
                    actionBtns[i].Location = new Point(i * (btnW + cardGap), 0);
                }
            }
        }

        private async void HomeControlUI_Load(object sender, EventArgs e)
        {
            await LoadDashboardDataAsync();
        }

        private async Task LoadAnalyticsDataAsync()
        {
            try
            {
                var today = DateTime.Today;
                var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
                var tomorrow = today.AddDays(1); // Calculate outside LINQ

                // Load top 3 products for current month
                var topProductsTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.OrderDetails
                            .Where(oi => oi.Order.CreatedDate >= firstDayOfMonth && oi.Order.CreatedDate < tomorrow)
                            .GroupBy(oi => oi.ProductId)
                            .Select(g => new
                            {
                                ProductId = g.Key,
                                ProductName = ctx.Products.Where(p => p.Id == g.Key).Select(p => p.ProductEnglishName).FirstOrDefault() ?? "Unknown",
                                TotalQuantity = g.Sum(oi => oi.Quantity)
                            })
                            .OrderByDescending(x => x.TotalQuantity)
                            .Take(3)
                            .ToList();
                    }
                });

                // Load top customers
                var topCustomersTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Orders
                            .Where(o => o.CreatedDate >= firstDayOfMonth && o.CreatedDate < tomorrow)
                            .Where(o => o.customerId != null)
                            .GroupBy(o => o.customerId)
                            .Select(g => new
                            {
                                CustomerId = g.Key,
                                CustomerName = ctx.Customers.Where(c => c.Id == g.Key).Select(c => c.CustomerName).FirstOrDefault() ?? "Unknown",
                                OrderCount = g.Count(),
                                TotalSpent = g.Sum(o => (float?)o.TotalBill) ?? 0f
                            })
                            .OrderByDescending(x => x.TotalSpent)
                            .Take(10)
                            .ToList();
                    }
                });

                await Task.WhenAll(topProductsTask, topCustomersTask);

                var topProducts = topProductsTask.Result;
                var topCustomers = topCustomersTask.Result;

                if (!this.IsHandleCreated) return;

                this.BeginInvoke(new Action(() =>
                {
                    // Update chart with top products
                    UpdateTopProductsChart(topProducts);

                    // Update top customers grid
                    UpdateTopCustomersGrid(topCustomers);
                }));
            }
            catch (Exception ex)
            {
                // Log error but don't crash
                Console.WriteLine($"Error loading analytics: {ex.Message}");
            }
        }

        private void UpdateTopProductsChart(dynamic topProducts)
        {
            ChartTopProducts.Series.Clear();

            if (topProducts == null || topProducts.Count == 0)
            {
                // Show no data message
                Series noDataSeries = new Series("No Data");
                noDataSeries.Points.AddY(0);
                noDataSeries.IsVisibleInLegend = false;
                ChartTopProducts.Series.Add(noDataSeries);
                ChartTopProducts.ChartAreas[0].AxisY.Title = "";

                // Add a label saying "No data"
                ChartTopProducts.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
                return;
            }

            ChartTopProducts.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;

            Series series = new Series("Top Products");
            series.ChartType = SeriesChartType.Column;
            series.BorderWidth = 2;
            series.ShadowOffset = 2;

            Color[] colors = { Color.FromArgb(59, 130, 246), Color.FromArgb(16, 185, 129), Color.FromArgb(245, 158, 11) };
            int colorIndex = 0;

            foreach (var product in topProducts)
            {
                int pointIndex = series.Points.AddXY(product.ProductName, product.TotalQuantity);
                series.Points[pointIndex].Color = colors[colorIndex % colors.Length];
                series.Points[pointIndex].Label = product.TotalQuantity.ToString();
                series.Points[pointIndex].LabelForeColor = Color.FromArgb(15, 23, 42);
                series.Points[pointIndex].LabelBackColor = Color.Transparent;
                colorIndex++;
            }

            ChartTopProducts.Series.Add(series);
            ChartTopProducts.ChartAreas[0].AxisX.Interval = 1;
            ChartTopProducts.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Segoe UI", 8F);
            ChartTopProducts.ChartAreas[0].AxisX.LabelStyle.Angle = -20;
            ChartTopProducts.ChartAreas[0].AxisY.Title = "Quantity Sold";
            ChartTopProducts.ChartAreas[0].AxisY.TitleFont = new Font("Segoe UI", 8F);
            ChartTopProducts.ChartAreas[0].AxisY.TitleForeColor = Color.FromArgb(100, 116, 139);
        }

        private void UpdateTopCustomersGrid(dynamic topCustomers)
        {
            GridTopCustomers.Rows.Clear();

            if (topCustomers == null || topCustomers.Count == 0)
            {
                GridTopCustomers.Rows.Add("", "No customer data available", "", "");
                return;
            }

            int rank = 1;
            foreach (var customer in topCustomers)
            {
                GridTopCustomers.Rows.Add(
                    rank.ToString(),
                    customer.CustomerName ?? "Unknown",
                    customer.OrderCount.ToString(),
                    customer.TotalSpent.ToString("N0")
                );
                rank++;
            }
        }

        private async Task LoadDashboardDataAsync()
        {
            try
            {
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                var yesterday = today.AddDays(-1);

                // Create separate contexts for each concurrent operation
                var todayOrderCountTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Orders.Count(s => s.CreatedDate >= today && s.CreatedDate < tomorrow);
                    }
                });

                var todayRevenueTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow)
                                         .Sum(s => (float?)s.TotalBill) ?? 0f;
                    }
                });

                var tempOrderCountTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.TempOrders.Count(s => s.CreatedDate >= today && s.CreatedDate < tomorrow);
                    }
                });

                var productCountTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Products.Count();
                    }
                });

                var customerCountTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Customers.Count(c => !c.IsDeleted);
                    }
                });

                var supplierCountTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Suppliers.Count(s => !s.IsDeleted);
                    }
                });

                var lowStockCountTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Products.Count(p => p.Qty <= LOW_STOCK_THRESHOLD);
                    }
                });

                var yesterdayOrderCountTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Orders.Count(s => s.CreatedDate >= yesterday && s.CreatedDate < today);
                    }
                });

                var last10OrdersTask = Task.Run(() =>
                {
                    using (var ctx = new POSDbContext())
                    {
                        return ctx.Orders
                            .OrderByDescending(o => o.CreatedDate)
                            .Take(10)
                            .Select(o => new
                            {
                                o.Id,
                                o.InvoiceNumber,
                                o.CreatedDate,
                                o.TotalBill,
                                o.ReceiveAmount,
                                o.paymentType,
                                CustomerName = o.customerId != null
                                    ? ctx.Customers
                                        .Where(c => c.Id == o.customerId)
                                        .Select(c => c.CustomerName)
                                        .FirstOrDefault()
                                    : "Walk-in"
                            })
                            .ToList();
                    }
                });

                await Task.WhenAll(
                    todayOrderCountTask, todayRevenueTask, tempOrderCountTask,
                    productCountTask, customerCountTask, supplierCountTask,
                    lowStockCountTask, yesterdayOrderCountTask, last10OrdersTask
                );

                int todayOrders = todayOrderCountTask.Result;
                float todayRevenue = todayRevenueTask.Result;
                int tempOrders = tempOrderCountTask.Result;
                int products = productCountTask.Result;
                int customers = customerCountTask.Result;
                int suppliers = supplierCountTask.Result;
                int lowStock = lowStockCountTask.Result;
                int yesterdayOrders = yesterdayOrderCountTask.Result;
                var last10 = last10OrdersTask.Result;

                if (!this.IsHandleCreated) return;

                this.BeginInvoke(new Action(() =>
                {
                    // Stat cards
                    LblTodayOrders.Text = todayOrders.ToString();
                    LblRevenue.Text = "PKR " + todayRevenue.ToString("N0");
                    LblTempOrders.Text = tempOrders.ToString();
                    LblProducts.Text = products.ToString();
                    LblCustomers.Text = customers.ToString();
                    LblSuppliers.Text = suppliers.ToString();

                    // Trend
                    int diff = todayOrders - yesterdayOrders;
                    LblOrderTrend.Text = diff >= 0
                        ? $"↑ {diff} from yesterday"
                        : $"↓ {Math.Abs(diff)} from yesterday";
                    LblOrderTrend.ForeColor = diff >= 0
                        ? Color.FromArgb(5, 150, 105)
                        : Color.FromArgb(220, 38, 38);

                    // Low stock alert
                    if (lowStock > 0)
                    {
                        PanelAlert.Visible = true;
                        LblAlertText.Text = $"⚠  {lowStock} product{(lowStock > 1 ? "s are" : " is")} running low on stock — review inventory.";
                    }
                    else
                    {
                        PanelAlert.Visible = false;
                    }

                    // Orders table
                    OrdersGrid.Rows.Clear();
                    foreach (var o in last10)
                    {
                        string shortInv = o.InvoiceNumber?.Length > 8
                            ? "#" + o.InvoiceNumber.Split('-').LastOrDefault()
                            : o.InvoiceNumber ?? "-";

                        OrdersGrid.Rows.Add(
                            shortInv,
                            o.CreatedDate.ToString("yyyy-MM-dd"),
                            o.TotalBill.ToString("N0"),
                            o.ReceiveAmount.ToString("N0"),
                            o.paymentType ?? "Cash",
                            string.IsNullOrEmpty(o.CustomerName) ? "Walk-in" : o.CustomerName
                        );
                    }

                    // Date label
                    LblCurrentDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");

                    // Load analytics data
                    _ = LoadAnalyticsDataAsync();
                }));
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                    this.BeginInvoke(new Action(() =>
                        MessageBox.Show("Error loading dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ));
            }
        }

        // ── Refresh button ──────────────────────────────────────────────
        private async void BtnRefresh_Click(object sender, EventArgs e)
        {
            BtnRefresh.Enabled = false;
            BtnRefresh.Text = "Refreshing...";
            await LoadDashboardDataAsync();
            BtnRefresh.Enabled = true;
            BtnRefresh.Text = "↻  Refresh";
        }

        // ── Quick-action buttons ─────────────────────────────────────────
        private void BtnSalesAnalysis_Click(object sender, EventArgs e)
        {
            //var licenseInfo = _licenseService.ReadLicenseFile();
            //if (licenseInfo.LicenseType == Models.LicenseModels.LicenseType.Trial)
            //{
            //    MessageBox.Show("This action cannot be performed on the trial version.", "Upgrade Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            OpenWithPassword("Sales Analysis", () =>
            {
                var form = new Form { Text = "Order Sales Report Analysis", StartPosition = FormStartPosition.CenterScreen, Width = 890, Height = 625 };
                var ctrl = new SalesReportDataControl { Dock = DockStyle.Fill };
                form.Controls.Add(ctrl);
                form.ShowDialog();
            });
        }

        private void BtnWeeklySalesChart_Click(object sender, EventArgs e)
        {
            OpenWithPassword("Weekly Sales Chart", () =>
            {
                var form = new Form { Text = "Weekly Order Report Analysis", StartPosition = FormStartPosition.CenterScreen, Width = 750, Height = 525 };
                var ctrl = new OrderReportControlUI { Dock = DockStyle.Fill };
                form.Controls.Add(ctrl);
                form.ShowDialog();
            });
        }

        private void BtnSalesCharts_Click(object sender, EventArgs e)
        {
            OpenWithPassword("Sales Charts", () =>
            {
                var f = new SalesChartForm();
                f.ShowDialog();
                this.Show();
            });
        }

        private void BtnProductTrends_Click(object sender, EventArgs e)
        {
            OpenWithPassword("Product Sales Trends", () =>
            {
                var f = new ProductSalesTrendForm();
                f.ShowDialog();
                this.Show();
            });
        }

        private void BtnPurchaseReports_Click(object sender, EventArgs e)
        {
            OpenWithPassword("Purchase Reports", () =>
            {
                var f = new POS_Shop.Views.Controllers.Reports.FormReportsMenu();
                this.Hide();
                f.ShowDialog();
                this.Show();
            });
        }

        // ── Helper ───────────────────────────────────────────────────────
        private void OpenWithPassword(string title, Action onSuccess)
        {
            while (true)
            {
                using (var dialog = new InputDialog("Enter Password:", title: title, isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    if (!string.IsNullOrWhiteSpace(dialog.InputValue) && dialog.InputValue.ToLower() == "show")
                    {
                        onSuccess();
                        return;
                    }
                    MessageBox.Show("Invalid password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

//using POS_Shop.Interfaces;
//using POS_Shop.Models;
//using POS_Shop.Repositories;
//using POS_Shop.Views.Controllers.Order;
//using POS_Shop.Views.Reports;
//using System;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Controllers
//{
//    public partial class HomeControlUI : UserControl
//    {
//        private static readonly ILicenseService _licenseService = new LicenseService();
//        private const int LOW_STOCK_THRESHOLD = 10;

//        public HomeControlUI()
//        {
//            InitializeComponent();
//            this.Load += HomeControlUI_Load;
//        }

//        private async void HomeControlUI_Load(object sender, EventArgs e)
//        {
//            await LoadDashboardDataAsync();
//        }

//        private async Task LoadDashboardDataAsync()
//        {
//            try
//            {
//                var today = DateTime.Today;
//                var tomorrow = today.AddDays(1);
//                var yesterday = today.AddDays(-1);

//                // Create separate contexts for each concurrent operation
//                var todayOrderCountTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Orders.Count(s => s.CreatedDate >= today && s.CreatedDate < tomorrow);
//                    }
//                });

//                var todayRevenueTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow)
//                                         .Sum(s => (float?)s.TotalBill) ?? 0f;
//                    }
//                });

//                var tempOrderCountTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.TempOrders.Count(s => s.CreatedDate >= today && s.CreatedDate < tomorrow);
//                    }
//                });

//                var productCountTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Products.Count();
//                    }
//                });

//                var customerCountTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Customers.Count(c => !c.IsDeleted);
//                    }
//                });

//                var supplierCountTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Suppliers.Count(s => !s.IsDeleted);
//                    }
//                });

//                var lowStockCountTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Products.Count(p => p.Qty <= LOW_STOCK_THRESHOLD);
//                    }
//                });

//                var yesterdayOrderCountTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Orders.Count(s => s.CreatedDate >= yesterday && s.CreatedDate < today);
//                    }
//                });

//                var last10OrdersTask = Task.Run(() =>
//                {
//                    using (var ctx = new POSDbContext())
//                    {
//                        return ctx.Orders
//                            .OrderByDescending(o => o.CreatedDate)
//                            .Take(10)
//                            .Select(o => new
//                            {
//                                o.Id,
//                                o.InvoiceNumber,
//                                o.CreatedDate,
//                                o.TotalBill,
//                                o.ReceiveAmount,
//                                o.paymentType,
//                                CustomerName = o.customerId != null
//                                    ? ctx.Customers
//                                        .Where(c => c.Id == o.customerId)
//                                        .Select(c => c.CustomerName)
//                                        .FirstOrDefault()
//                                    : "Walk-in"
//                            })
//                            .ToList();
//                    }
//                });

//                await Task.WhenAll(
//                    todayOrderCountTask, todayRevenueTask, tempOrderCountTask,
//                    productCountTask, customerCountTask, supplierCountTask,
//                    lowStockCountTask, yesterdayOrderCountTask, last10OrdersTask
//                );

//                int todayOrders = todayOrderCountTask.Result;
//                float todayRevenue = todayRevenueTask.Result;
//                int tempOrders = tempOrderCountTask.Result;
//                int products = productCountTask.Result;
//                int customers = customerCountTask.Result;
//                int suppliers = supplierCountTask.Result;
//                int lowStock = lowStockCountTask.Result;
//                int yesterdayOrders = yesterdayOrderCountTask.Result;
//                var last10 = last10OrdersTask.Result;

//                if (!this.IsHandleCreated) return;

//                this.BeginInvoke(new Action(() =>
//                {
//                    // Stat cards
//                    LblTodayOrders.Text = todayOrders.ToString();
//                    LblRevenue.Text = "PKR " + todayRevenue.ToString("N0");
//                    LblTempOrders.Text = tempOrders.ToString();
//                    LblProducts.Text = products.ToString();
//                    LblCustomers.Text = customers.ToString();
//                    LblSuppliers.Text = suppliers.ToString();

//                    // Trend
//                    int diff = todayOrders - yesterdayOrders;
//                    LblOrderTrend.Text = diff >= 0
//                        ? $"↑ {diff} from yesterday"
//                        : $"↓ {Math.Abs(diff)} from yesterday";
//                    LblOrderTrend.ForeColor = diff >= 0
//                        ? Color.FromArgb(5, 150, 105)
//                        : Color.FromArgb(220, 38, 38);

//                    // Low stock alert
//                    if (lowStock > 0)
//                    {
//                        PanelAlert.Visible = true;
//                        LblAlertText.Text = $"⚠  {lowStock} product{(lowStock > 1 ? "s are" : " is")} running low on stock — review inventory.";
//                    }
//                    else
//                    {
//                        PanelAlert.Visible = false;
//                    }

//                    // Orders table
//                    OrdersGrid.Rows.Clear();
//                    foreach (var o in last10)
//                    {
//                        string shortInv = o.InvoiceNumber?.Length > 8
//                            ? "#" + o.InvoiceNumber.Split('-').LastOrDefault()
//                            : o.InvoiceNumber ?? "-";

//                        OrdersGrid.Rows.Add(
//                            shortInv,
//                            o.CreatedDate.ToString("yyyy-MM-dd"),
//                            o.TotalBill.ToString("N0"),
//                            o.ReceiveAmount.ToString("N0"),
//                            o.paymentType ?? "Cash",
//                            string.IsNullOrEmpty(o.CustomerName) ? "Walk-in" : o.CustomerName
//                        );
//                    }

//                    // Date label
//                    LblCurrentDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
//                }));
//            }
//            catch (Exception ex)
//            {
//                if (this.IsHandleCreated)
//                    this.BeginInvoke(new Action(() =>
//                        MessageBox.Show("Error loading dashboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
//                    ));
//            }
//        }
//        // ── Refresh button ──────────────────────────────────────────────
//        private async void BtnRefresh_Click(object sender, EventArgs e)
//        {
//            BtnRefresh.Enabled = false;
//            BtnRefresh.Text = "Refreshing...";
//            await LoadDashboardDataAsync();
//            BtnRefresh.Enabled = true;
//            BtnRefresh.Text = "↻  Refresh";
//        }

//        // ── Quick-action buttons ─────────────────────────────────────────
//        private void BtnSalesAnalysis_Click(object sender, EventArgs e)
//        {
//            var licenseInfo = _licenseService.ReadLicenseFile();
//            if (licenseInfo.LicenseType == Models.LicenseModels.LicenseType.Trial)
//            {
//                MessageBox.Show("This action cannot be performed on the trial version.", "Upgrade Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                return;
//            }
//            OpenWithPassword("Sales Analysis", () =>
//            {
//                var form = new Form { Text = "Order Sales Report Analysis", StartPosition = FormStartPosition.CenterScreen, Width = 890, Height = 625 };
//                var ctrl = new SalesReportDataControl { Dock = DockStyle.Fill };
//                form.Controls.Add(ctrl);
//                form.ShowDialog();
//            });
//        }

//        private void BtnWeeklySalesChart_Click(object sender, EventArgs e)
//        {
//            OpenWithPassword("Weekly Sales Chart", () =>
//            {
//                var form = new Form { Text = "Weekly Order Report Analysis", StartPosition = FormStartPosition.CenterScreen, Width = 750, Height = 525 };
//                var ctrl = new OrderReportControlUI { Dock = DockStyle.Fill };
//                form.Controls.Add(ctrl);
//                form.ShowDialog();
//            });
//        }

//        private void BtnSalesCharts_Click(object sender, EventArgs e)
//        {
//            OpenWithPassword("Sales Charts", () =>
//            {
//                var f = new SalesChartForm();
//                f.ShowDialog();
//                this.Show();
//            });
//        }

//        private void BtnProductTrends_Click(object sender, EventArgs e)
//        {
//            OpenWithPassword("Product Sales Trends", () =>
//            {
//                var f = new ProductSalesTrendForm();
//                f.ShowDialog();
//                this.Show();
//            });
//        }

//        private void BtnPurchaseReports_Click(object sender, EventArgs e)
//        {
//            OpenWithPassword("Purchase Reports", () =>
//            {
//                var f = new POS_Shop.Views.Controllers.Reports.FormReportsMenu();
//                this.Hide();
//                f.ShowDialog();
//                this.Show();
//            });
//        }

//        // ── Helper ───────────────────────────────────────────────────────
//        private void OpenWithPassword(string title, Action onSuccess)
//        {
//            while (true)
//            {
//                using (var dialog = new InputDialog("Enter Password:", title: title, isTextBoxProtected: true))
//                {
//                    if (dialog.ShowDialog() != DialogResult.OK) return;
//                    if (!string.IsNullOrWhiteSpace(dialog.InputValue) && dialog.InputValue.ToLower() == "show")
//                    {
//                        onSuccess();
//                        return;
//                    }
//                    MessageBox.Show("Invalid password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                }
//            }
//        }
//    }
//}
