using POS_Shop.DTOs.Product;
using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Repositories;
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

namespace POS_Shop.Views.Reports
{
    public partial class SalesChartForm : Form
    {
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerate;
        private Chart chartSales;
        private ComboBox cmbChartType;
        private ComboBox cmbQuantityType;
        private TextBox txtQty;
        private Label lblQty;
        private TabControl tabControl;
        private TabPage tabChart;
        private TabPage tabData;

        public SalesChartForm()
        {
            InitializeComponent();
            CreateControls();

            InitializeProductUnitsDropdown();
        }

        private void InitializeProductUnitsDropdown()
        {
            using (var context = new POSDbContext())
            {
                var productUnitRepo = new ProductUnitRepository(context);
                var productUnit = productUnitRepo.GetAll().Select(s => new ProductUnit()
                {
                    Id = s.Id,
                    Name = s.Name,

                }).ToList();
                cmbQuantityType.Items.Clear();

                // Add default option
                var allItems = new List<ProductUnit>();
                allItems.Add(new ProductUnit { Id = 0, Name = "All" });
                allItems.AddRange(productUnit);
                cmbQuantityType.DataSource = allItems;
                cmbQuantityType.DisplayMember = "Name";
                cmbQuantityType.ValueMember = "Name";
                cmbQuantityType.SelectedIndex = 1;
            }
        }

        private void CreateControls()
        {
            this.Size = new Size(1000, 700); // Increased height for tabs
            this.Text = "Top Selling Products Analysis";

            // Create Tab Control
            tabControl = new TabControl
            {
                Location = new Point(10, 10),
                Size = new Size(980, 630),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Create Tab Pages
            tabChart = new TabPage("Chart View");
            tabData = new TabPage("Data View");

            tabControl.TabPages.Add(tabChart);
            tabControl.TabPages.Add(tabData);

            // Add controls to tabChart
            CreateChartTabControls();

            this.Controls.Add(tabControl);
        }

        private void CreateChartTabControls()
        {
            // Start Date
            Label lblStart = new Label { Text = "From:", Location = new Point(20, 20), Size = new Size(40, 20) };
            dtpStartDate = new DateTimePicker
            {
                Location = new Point(65, 20),
                Size = new Size(130, 20),
                Value = DateTime.Now.AddMonths(-1)
            };

            // End Date
            Label lblEnd = new Label { Text = "To:", Location = new Point(200, 20), Size = new Size(25, 20) };
            dtpEndDate = new DateTimePicker
            {
                Location = new Point(230, 20),
                Size = new Size(120, 20),
                Value = DateTime.Now
            };

            // Quantity Type Filter
            Label lblQuantityType = new Label { Text = "Quantity Type:", Location = new Point(370, 20), Size = new Size(80, 20) };
            cmbQuantityType = new ComboBox
            {
                Location = new Point(455, 20),
                Size = new Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            //cmbQuantityType.Items.AddRange(new[] { "All",    
            //                "عدد",    // Piece/Unit
            //                "ڈبہ",    // Box
            //                "درجن",   // Dozen
            //                "پیکٹ",   // Packet
            //                "بنڈل",   // Bundle
            //                "کارٹن",  // Carton (corrected)
            //                "رول",    // Roll
            //                 "ڈبی",
            //                "کلو",    // Kilogram
            //                "گز",     // Yard
            //                "جوڑی",   // Pair
            //                "سبقہ" });
            //cmbQuantityType.SelectedIndex = 0;

            // Quantity Textbox
            lblQty = new Label { Text = "Qty:", Location = new Point(570, 20), Size = new Size(30, 20) };
            txtQty = new TextBox
            {
                Location = new Point(605, 20),
                Size = new Size(60, 20),
                Text = "4" // Default value
            };

            // Chart Type Selection
            Label lblChartType = new Label { Text = "Chart by:", Location = new Point(680, 20), Size = new Size(50, 20) };
            cmbChartType = new ComboBox
            {
                Location = new Point(735, 20),
                Size = new Size(100, 20),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbChartType.Items.AddRange(new[] { "Quantity", "Revenue" });
            cmbChartType.SelectedIndex = 0;

            // Generate Button
            btnGenerate = new Button
            {
                Text = "Generate Chart",
                Location = new Point(850, 18),
                Size = new Size(100, 25)
            };
            btnGenerate.Click += BtnGenerate_Click;

            // Chart Control
            chartSales = new Chart
            {
                Location = new Point(20, 60),
                Size = new Size(930, 500),
                BackColor = Color.White
            };

            // Add controls to chart tab
            tabChart.Controls.AddRange(new Control[] {
                lblStart, dtpStartDate, lblEnd, dtpEndDate,
                lblQuantityType, cmbQuantityType,
                lblQty, txtQty,
                lblChartType, cmbChartType, btnGenerate, chartSales
            });
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                LoadingManager.ShowLoading();
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);
                string selectedQuantityType = cmbQuantityType.SelectedValue?.ToString() ?? "All";

                if (startDate > endDate)
                {
                    LoadingManager.HideLoading();
                    MessageBox.Show("Start date cannot be after end date.");
                    return;
                }

                var topProducts = GetTopSellingProducts(startDate, endDate, selectedQuantityType, int.Parse(txtQty.Text.ToString()));

                if (topProducts == null || !topProducts.Any())
                {
                    LoadingManager.HideLoading();
                    MessageBox.Show("No sales data found for the selected criteria.");
                    return;
                }

                // Update both tabs
                if (cmbChartType.SelectedItem?.ToString() == "Revenue")
                {
                    GenerateRevenuePieChart(topProducts, startDate, endDate, selectedQuantityType);
                }
                else
                {
                    GenerateQuantityPieChart(topProducts, startDate, endDate, selectedQuantityType);
                }

                // Update data grid tab
                UpdateDataGridTab(topProducts);

            }
            catch (Exception ex)
            {
                LoadingManager.HideLoading();
                MessageBox.Show($"Error generating chart: {ex.Message}\n\nDetails: {ex.InnerException?.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadingManager.HideLoading();
        }

        private void UpdateDataGridTab(List<ProductSalesData> products)
        {
            // Clear existing controls from data tab
            tabData.Controls.Clear();

            // Create DataGridView
            DataGridView dataGridView = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(940, 550),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoGenerateColumns = false,
                ReadOnly = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None
            };

            // Add columns
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                HeaderText = "Product ID",
                DataPropertyName = "ProductId",
                Width = 80
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                Width = 200
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalQuantity",
                HeaderText = "Total Quantity",
                DataPropertyName = "TotalQuantity",
                Width = 100
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalRevenue",
                HeaderText = "Total Revenue",
                DataPropertyName = "TotalRevenue",
                Width = 120,
            });

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QuantityType",
                HeaderText = "Quantity Type",
                DataPropertyName = "QuantityType",
                Width = 100
            });

            // Bind data
            dataGridView.DataSource = products;

            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Add summary label
            Label summaryLabel = new Label
            {
                Text = $"Total Products: {products.Count} | Total Quantity: {products.Sum(p => p.TotalQuantity)} | Total Revenue: {products.Sum(p => p.TotalRevenue):C2}",
                Location = new Point(10, 570),
                Size = new Size(940, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.DarkBlue
            };

            tabData.Controls.Add(dataGridView);
            tabData.Controls.Add(summaryLabel);
        }

        private void GenerateQuantityPieChart(List<ProductSalesData> products, DateTime startDate, DateTime endDate, string quantityType)
        {
            chartSales.Series.Clear();
            chartSales.Titles.Clear();
            chartSales.Legends.Clear();
            chartSales.ChartAreas.Clear();

            // Chart Area
            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.White;
            chartSales.ChartAreas.Add(chartArea);

            // Title with QuantityType info
            string quantityTypeText = quantityType == "All" ? "All Types" : quantityType;
            chartSales.Titles.Add($"Top {products.Count} Selling Products by Quantity\n({quantityTypeText}) - {startDate:MMM dd, yyyy} to {endDate:MMM dd, yyyy}");
            chartSales.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
            chartSales.Titles[0].Alignment = ContentAlignment.TopCenter;

            // Legend
            Legend legend = new Legend();
            legend.Title = "Products";
            legend.Docking = Docking.Right;
            legend.Font = new Font("Arial", 9);
            legend.BackColor = Color.WhiteSmoke;
            chartSales.Legends.Add(legend);

            // Series
            Series series = new Series("Products");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            series.Label = "#PERCENT{P1}";
            series.LegendText = "#VALX";
            series.ToolTip = "#VALX: #VALY units\n#PERCENT{P2} of total";
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";
            series.BorderWidth = 2;
            series.Font = new Font("Arial", 9);

            // Colors
            Color[] colors = {
                Color.FromArgb(65, 105, 225),   // Royal Blue
                Color.FromArgb(34, 139, 34),    // Forest Green
                Color.FromArgb(218, 165, 32),   // Golden Rod
                Color.FromArgb(220, 20, 60)     // Crimson
            };

            // Add data points
            for (int i = 0; i < products.Count; i++)
            {
                var product = products[i];
                DataPoint point = new DataPoint();
                point.SetValueXY(product.ProductName, product.TotalQuantity);
                point.Color = i < colors.Length ? colors[i] : GetRandomColor();
                point.Label = $"{product.TotalQuantity}";
                point.LegendText = $"{TextFormatHelper.FormatUrduText(product.ProductName)} ({product.TotalQuantity} SI)";

                series.Points.Add(point);
            }

            chartSales.Series.Add(series);
        }

        private void GenerateRevenuePieChart(List<ProductSalesData> products, DateTime startDate, DateTime endDate, string quantityType)
        {
            chartSales.Series.Clear();
            chartSales.Titles.Clear();
            chartSales.Legends.Clear();
            chartSales.ChartAreas.Clear();

            // Chart Area
            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = Color.White;
            chartArea.Area3DStyle.Enable3D = true;
            chartArea.Area3DStyle.Inclination = 10;
            chartSales.ChartAreas.Add(chartArea);

            // Title
            string title = $"Top {products.Count} Products by Revenue\n{startDate:MMM dd, yyyy} to {endDate:MMM dd, yyyy}";
            chartSales.Titles.Add(title);
            chartSales.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);

            // Series
            Series series = new Series("Revenue");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;

            // Don't set series.Label - we'll set individual point labels
            series.Font = new Font("Arial", 8);
            series.LabelForeColor = Color.Black;
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Black";

            // Add data with custom labels
            foreach (var product in products)
            {
                DataPoint point = new DataPoint();
                string shortName = GetShortProductName(product.ProductName);
                point.SetValueXY(shortName, (double)product.TotalRevenue);

                // Set custom label for each point - Product name and Revenue
                point.Label = $"{shortName}\nRs. {product.TotalRevenue:N0}";

                point.ToolTip = $"{product.ProductName}: Rs. {product.TotalRevenue:N0}";
                point.Color = GetRandomColor();
                series.Points.Add(point);
            }

            chartSales.Series.Add(series);

            // Add legend
            Legend legend = new Legend();
            legend.Docking = Docking.Right;
            legend.Font = new Font("Arial", 9);
            chartSales.Legends.Add(legend);
        }

        // Helper method to shorten product names
        private string GetShortProductName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return "Unknown";

            if (fullName.Length <= 12)
                return fullName;

            return fullName.Substring(0, 10) + "...";
        }

        private List<ProductSalesData> GetTopSellingProducts(DateTime startDate, DateTime endDate, string quantityType, int topCount = 4)
        {
            using (var context = new POSDbContext())
            {
                try
                {

                    // Step 1: Get top product IDs by TOTAL quantity (all types)
                    var topProductIds = context.OrderDetails
                        .AsNoTracking()
                        .Where(od => od.ProductId.HasValue &&
                                     od.Order.CreatedDate >= startDate &&
                                     od.Order.CreatedDate <= endDate)
                        .GroupBy(od => od.ProductId)
                        .Select(g => new
                        {
                            ProductId = g.Key.Value,
                            TotalQuantity = g.Sum(x => x.Quantity)
                        })
                        .OrderByDescending(x => x.TotalQuantity)
                        .Take(topCount)
                        .Select(x => x.ProductId)
                        .ToList();

                    if (!topProductIds.Any()|| topProductIds.Count()<0)
                        return new List<ProductSalesData>();

                    // Step 2: Calculate metrics for these products filtered by quantityType
                    var topProducts = context.OrderDetails
                        .AsNoTracking()
                        .Where(od => topProductIds.Contains(od.ProductId.Value) &&
                                     od.Order.CreatedDate >= startDate &&
                                     od.Order.CreatedDate <= endDate &&
                                     (quantityType == "All" || od.QuantityType == quantityType))
                        .GroupBy(od => od.ProductId)
                        .Select(g => new
                        {
                            ProductId = g.Key.Value,
                            TotalQuantity = g.Sum(x => x.Quantity),
                            TotalRevenue = g.Sum(x => x.Quantity * x.Price)
                        })
                        .ToList();

                    // Step 3: Fetch product names efficiently
                    var productNames = context.Products
                        .AsNoTracking()
                        .Where(p => topProductIds.Contains(p.Id))
                        .ToDictionary(p => p.Id, p => p.ProductUrduName);

                    // Step 4: Combine results
                    var productsData = topProducts
                        .Select(x => new ProductSalesData
                        {
                            ProductId = x.ProductId,
                            ProductName = productNames.TryGetValue(x.ProductId, out var name)
                                ? name
                                : "Unknown",
                            TotalQuantity = x.TotalQuantity,
                            TotalRevenue = x.TotalRevenue,
                            QuantityType = quantityType
                        }).OrderByDescending(s=>s.TotalQuantity)
                        .ToList();

                    return productsData;

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database error: {ex.Message}");
                    return new List<ProductSalesData>();
                }
            }
        }

        private static Random rand = new Random(); // Create once

        private Color GetRandomColor()
        {
            return Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
        }
    }
}






//using POS_Shop.DTOs.Product;
//using POS_Shop.Helpers;
//using POS_Shop.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Data.Entity;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Windows.Forms.DataVisualization.Charting;

//namespace POS_Shop.Views.Reports
//{
//    public partial class SalesChartForm : Form
//    {
//        private DateTimePicker dtpStartDate;
//        private DateTimePicker dtpEndDate;
//        private Button btnGenerate;
//        private Chart chartSales;
//        private ComboBox cmbChartType;
//        private ComboBox cmbQuantityType;
//        private TextBox txtQty;
//        private Label lblQty;

//        public SalesChartForm()
//        {
//            InitializeComponent();
//            CreateControls();
//        }

//        private void CreateControls()
//        {
//            this.Size = new Size(1000, 650);
//            this.Text = "Top Selling Products Analysis";

//            // Start Date
//            Label lblStart = new Label { Text = "From:", Location = new Point(20, 20), Size = new Size(40, 20) };
//            dtpStartDate = new DateTimePicker
//            {
//                Location = new Point(65, 20),
//                Size = new Size(130, 20),
//                Value = DateTime.Now.AddMonths(-1)
//            };

//            // End Date
//            Label lblEnd = new Label { Text = "To:", Location = new Point(200, 20), Size = new Size(25, 20) };
//            dtpEndDate = new DateTimePicker
//            {
//                Location = new Point(230, 20),
//                Size = new Size(120, 20),
//                Value = DateTime.Now
//            };

//            // Quantity Type Filter
//            Label lblQuantityType = new Label { Text = "Quantity Type:", Location = new Point(370, 20), Size = new Size(80, 20) };
//            cmbQuantityType = new ComboBox
//            {
//                Location = new Point(455, 20),
//                Size = new Size(100, 20),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            cmbQuantityType.Items.AddRange(new[] { "All", "عدد", "ڈبہ" });
//            cmbQuantityType.SelectedIndex = 0;

//            // Quantity Textbox
//            lblQty = new Label { Text = "Qty:", Location = new Point(570, 20), Size = new Size(30, 20) };
//            txtQty = new TextBox
//            {
//                Location = new Point(605, 20),
//                Size = new Size(60, 20),
//                Text = "10" // Default value
//            };

//            // Chart Type Selection
//            Label lblChartType = new Label { Text = "Chart by:", Location = new Point(680, 20), Size = new Size(50, 20) };
//            cmbChartType = new ComboBox
//            {
//                Location = new Point(735, 20),
//                Size = new Size(100, 20),
//                DropDownStyle = ComboBoxStyle.DropDownList
//            };
//            cmbChartType.Items.AddRange(new[] { "Quantity", "Revenue" });
//            cmbChartType.SelectedIndex = 0;

//            // Generate Button
//            btnGenerate = new Button
//            {
//                Text = "Generate Chart",
//                Location = new Point(850, 18),
//                Size = new Size(100, 25)
//            };
//            btnGenerate.Click += BtnGenerate_Click;

//            // Chart Control
//            chartSales = new Chart
//            {
//                Location = new Point(20, 60),
//                Size = new Size(950, 520),
//                BackColor = Color.White
//            };

//            // Add controls to form
//            this.Controls.AddRange(new Control[] {
//            lblStart, dtpStartDate, lblEnd, dtpEndDate,
//            lblQuantityType, cmbQuantityType,
//            lblQty, txtQty,
//            lblChartType, cmbChartType, btnGenerate, chartSales
//        });
//        }


//        private void BtnGenerate_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                LoadingManager.ShowLoading();
//                DateTime startDate = dtpStartDate.Value.Date;
//                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);
//                string selectedQuantityType = cmbQuantityType.SelectedItem?.ToString() ?? "All";

//                if (startDate > endDate)
//                {
//                    MessageBox.Show("Start date cannot be after end date.");
//                    return;
//                }

//                var topProducts = GetTopSellingProducts(startDate, endDate, selectedQuantityType, int.Parse(txtQty.Text.ToString()));

//                if (topProducts == null || !topProducts.Any())
//                {
//                    MessageBox.Show("No sales data found for the selected criteria.");
//                    return;
//                }

//                if (cmbChartType.SelectedItem?.ToString() == "Revenue")
//                {
//                    GenerateRevenuePieChart(topProducts, startDate, endDate, selectedQuantityType);
//                }
//                else
//                {
//                    GenerateQuantityPieChart(topProducts, startDate, endDate, selectedQuantityType);
//                }


//            }
//            catch (Exception ex)
//            {
//                LoadingManager.HideLoading();
//                MessageBox.Show($"Error generating chart: {ex.Message}\n\nDetails: {ex.InnerException?.Message}",
//                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//            LoadingManager.HideLoading();
//        }

//        private void GenerateQuantityPieChart(List<ProductSalesData> products, DateTime startDate, DateTime endDate, string quantityType)
//        {
//            chartSales.Series.Clear();
//            chartSales.Titles.Clear();
//            chartSales.Legends.Clear();
//            chartSales.ChartAreas.Clear();

//            // Chart Area
//            ChartArea chartArea = new ChartArea();
//            chartArea.BackColor = Color.White;
//            chartSales.ChartAreas.Add(chartArea);

//            // Title with QuantityType info
//            string quantityTypeText = quantityType == "All" ? "All Types" : quantityType;
//            chartSales.Titles.Add($"Top {products.Count} Selling Products by Quantity\n({quantityTypeText}) - {startDate:MMM dd, yyyy} to {endDate:MMM dd, yyyy}");
//            chartSales.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);
//            chartSales.Titles[0].Alignment = ContentAlignment.TopCenter;

//            // Legend
//            Legend legend = new Legend();
//            legend.Title = "Products";
//            legend.Docking = Docking.Right;
//            legend.Font = new Font("Arial", 9);
//            legend.BackColor = Color.WhiteSmoke;
//            chartSales.Legends.Add(legend);

//            // Series
//            Series series = new Series("Products");
//            series.ChartType = SeriesChartType.Pie;
//            series.IsValueShownAsLabel = true;
//            series.Label = "#PERCENT{P1}";
//            series.LegendText = "#VALX";
//            series.ToolTip = "#VALX: #VALY units\n#PERCENT{P2} of total";
//            series["PieLabelStyle"] = "Outside";
//            series["PieLineColor"] = "Black";
//            series.BorderWidth = 2;
//            series.Font = new Font("Arial", 9);

//            // Colors
//            Color[] colors = {
//        Color.FromArgb(65, 105, 225),   // Royal Blue
//        Color.FromArgb(34, 139, 34),    // Forest Green
//        Color.FromArgb(218, 165, 32),   // Golden Rod
//        Color.FromArgb(220, 20, 60)     // Crimson
//    };

//            // Add data points
//            for (int i = 0; i < products.Count; i++)
//            {
//                var product = products[i];
//                DataPoint point = new DataPoint();
//                point.SetValueXY(product.ProductName, product.TotalQuantity);
//                point.Color = i < colors.Length ? colors[i] : GetRandomColor();
//                point.Label = $"{product.TotalQuantity}";
//                point.LegendText = $"{TextFormatHelper.FormatUrduText(product.ProductName)} ({product.TotalQuantity} SI)";

//                series.Points.Add(point);
//            }

//            chartSales.Series.Add(series);
//        }


//        private void GenerateRevenuePieChart(List<ProductSalesData> products, DateTime startDate, DateTime endDate, string quantityType)
//        {
//            chartSales.Series.Clear();
//            chartSales.Titles.Clear();
//            chartSales.Legends.Clear();
//            chartSales.ChartAreas.Clear();

//            // Chart Area
//            ChartArea chartArea = new ChartArea();
//            chartArea.BackColor = Color.White;
//            chartArea.Area3DStyle.Enable3D = true;
//            chartArea.Area3DStyle.Inclination = 10;
//            chartSales.ChartAreas.Add(chartArea);

//            // Title
//            string title = $"Top {products.Count} Products by Revenue\n{startDate:MMM dd, yyyy} to {endDate:MMM dd, yyyy}";
//            chartSales.Titles.Add(title);
//            chartSales.Titles[0].Font = new Font("Arial", 12, FontStyle.Bold);

//            // Series
//            Series series = new Series("Revenue");
//            series.ChartType = SeriesChartType.Pie;
//            series.IsValueShownAsLabel = true;

//            // Don't set series.Label - we'll set individual point labels
//            series.Font = new Font("Arial", 8);
//            series.LabelForeColor = Color.Black;
//            series["PieLabelStyle"] = "Outside";
//            series["PieLineColor"] = "Black";

//            // Add data with custom labels
//            foreach (var product in products)
//            {
//                DataPoint point = new DataPoint();
//                string shortName = GetShortProductName(product.ProductName);
//                point.SetValueXY(shortName, (double)product.TotalRevenue);

//                // Set custom label for each point - Product name and Revenue
//                point.Label = $"{shortName}\nRs. {product.TotalRevenue:N0}";

//                point.ToolTip = $"{product.ProductName}: Rs. {product.TotalRevenue:N0}";
//                point.Color = GetRandomColor();
//                series.Points.Add(point);
//            }

//            chartSales.Series.Add(series);

//            // Add legend
//            Legend legend = new Legend();
//            legend.Docking = Docking.Right;
//            legend.Font = new Font("Arial", 9);
//            chartSales.Legends.Add(legend);
//        }

//        // Helper method to shorten product names
//        private string GetShortProductName(string fullName)
//        {
//            if (string.IsNullOrEmpty(fullName))
//                return "Unknown";

//            if (fullName.Length <= 12)
//                return fullName;

//            return fullName.Substring(0, 10) + "...";
//        }


//        private List<ProductSalesData> GetTopSellingProducts(DateTime startDate, DateTime endDate, string quantityType, int topCount = 4)
//        {
//            using (var context = new POSDbContext())
//            {
//                try
//                {
//                    //// STEP 1: Get ALL order details
//                    //var allOrderDetails = context.OrderDetails
//                    //    .Include(od => od.Product)
//                    //    .Include(od => od.Order)
//                    //    .Where(od => od.ProductId != null &&
//                    //                od.Order.CreatedDate >= startDate &&
//                    //                od.Order.CreatedDate <= endDate)
//                    //    .ToList();

//                    //// Determine top products based on TOTAL quantity
//                    //var topProductsInfo = allOrderDetails
//                    //    .GroupBy(od => new { od.ProductId, ProductName = od.Product.ProductUrduName })
//                    //    .Select(g => new
//                    //    {
//                    //        ProductId = g.Key.ProductId.Value,
//                    //        ProductName = g.Key.ProductName,
//                    //        TotalQuantityAll = g.Sum(x => x.Quantity),
//                    //        AdadQuantity = g.Where(x => x.QuantityType == "عدد").Sum(x => x.Quantity),
//                    //        DabaQuantity = g.Where(x => x.QuantityType == "ڈبہ").Sum(x => x.Quantity),
//                    //        TotalRevenue = quantityType=="All"?g.Sum(x => (decimal)(x.Quantity * x.Price)): g.Where(s=>s.QuantityType== quantityType).Sum(x => (decimal)(x.Quantity * x.Price)) // Calculate in memory
//                    //    })
//                    //    .OrderByDescending(x => x.TotalQuantityAll)
//                    //    .Take(topCount)
//                    //    .ToList();

//                    //// STEP 2: For each top product, get the quantity based on selected type
//                    //var products = new List<ProductSalesData>();

//                    //foreach (var productInfo in topProductsInfo)
//                    //{
//                    //    int displayQuantity;

//                    //    if (quantityType == "All")
//                    //    {
//                    //        displayQuantity = productInfo.TotalQuantityAll;
//                    //    }
//                    //    else if (quantityType == "عدد")
//                    //    {
//                    //        displayQuantity = productInfo.AdadQuantity;
//                    //    }
//                    //    else // "ڈبہ"
//                    //    {
//                    //        displayQuantity = productInfo.DabaQuantity;
//                    //    }

//                    //    products.Add(new ProductSalesData
//                    //    {
//                    //        ProductName = productInfo.ProductName,
//                    //        ProductId = productInfo.ProductId,
//                    //        TotalQuantity = displayQuantity,
//                    //        TotalRevenue = productInfo.TotalRevenue,// You can calculate this similarly if needed
//                    //        QuantityType = quantityType,
//                    //    });
//                    //}

//                    //// Order by the display quantity
//                    //return products.OrderByDescending(x => x.TotalQuantity).ToList();




//                    /// 2nd Query
//                    /// 

//                    //// First get top products by total quantity (database side)
//                    var topProductIds = context.OrderDetails
//                            .Include(od => od.Product)
//                            .Include(od => od.Order)
//                        .Where(od => od.ProductId != null &&
//                                    od.Order.CreatedDate >= startDate &&
//                                    od.Order.CreatedDate <= endDate)
//                        .GroupBy(od => od.ProductId)
//                        .Select(g => new
//                        {
//                            ProductId = g.Key.Value,
//                            TotalQuantity = g.Sum(x => x.Quantity)
//                        })
//                        .OrderByDescending(x => x.TotalQuantity)
//                        .Take(topCount)
//                        .Select(x => x.ProductId)
//                        .ToList();

//                    // Then get detailed info only for top products
//                    var productsData = context.OrderDetails
//                        .Where(od => topProductIds.Contains(od.ProductId.Value) &&
//                                    od.Order.CreatedDate >= startDate &&
//                                    od.Order.CreatedDate <= endDate)
//                        .GroupBy(od => new { od.ProductId, ProductName = od.Product.ProductUrduName })
//                        .Select(g => new
//                        {
//                            ProductId = g.Key.ProductId.Value,
//                            ProductName = g.Key.ProductName,
//                            TotalQuantityAll = g.Sum(x => x.Quantity),
//                            QuantityByType = g.GroupBy(x => x.QuantityType)
//                                             .Select(grp => new { Type = grp.Key, Quantity = grp.Sum(x => x.Quantity) })
//                                             .ToList(),
//                            // Remove double cast - let EF handle the type
//                            TotalRevenue = quantityType == "All"
//                                ? g.Sum(x => x.Quantity * x.Price)
//                                : g.Where(x => x.QuantityType == quantityType).Sum(x => x.Quantity * x.Price)
//                        })
//                        .ToList();

//                    // Now the revenue is already in the correct type (no conversion needed)
//                    var products = productsData.Select(p => new ProductSalesData
//                    {
//                        ProductId = p.ProductId,
//                        ProductName = p.ProductName,
//                        TotalQuantity = quantityType == "All"
//                            ? p.TotalQuantityAll
//                            : p.QuantityByType.Where(q => q.Type == quantityType).Sum(q => q.Quantity),
//                        TotalRevenue = p.TotalRevenue, // No conversion needed
//                        QuantityType = quantityType
//                    })
//                    .OrderByDescending(x => x.TotalQuantity)
//                    .ToList();
//                    return products;
//                    // 4th Call

//                    //// Pure database query - most efficient
//                    //var query = context.OrderDetails
//                    //    .Where(od => od.ProductId != null &&
//                    //                od.Order.CreatedDate >= startDate &&
//                    //                od.Order.CreatedDate <= endDate &&
//                    //                (quantityType == "All" || od.QuantityType == quantityType))
//                    //    .GroupBy(od => new { od.ProductId, ProductName = od.Product.ProductUrduName })
//                    //    .Select(g => new ProductSalesData
//                    //    {
//                    //        ProductId = g.Key.ProductId.Value,
//                    //        ProductName = g.Key.ProductName,
//                    //        TotalQuantity = g.Sum(x => x.Quantity),
//                    //        TotalRevenue = g.Sum(x => x.Quantity * x.Price),
//                    //        QuantityType = quantityType
//                    //    })
//                    //    .OrderByDescending(x => x.TotalQuantity)
//                    //    .Take(topCount);

//                    //return query.ToList();
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Database error: {ex.Message}");
//                    return new List<ProductSalesData>();
//                }
//            }
//        }

//        private static Random rand = new Random(); // Create once

//        private Color GetRandomColor()
//        {
//            //Random rand = new Random();
//            return Color.FromArgb(rand.Next(100, 255), rand.Next(100, 255), rand.Next(100, 255));
//        }

//    }
//}
