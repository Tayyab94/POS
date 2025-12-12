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
//    public partial class ProductSalesTrendForm : Form
//    {
//        private TextBox txtProductSearch;
//        private ComboBox cmbTimePeriod;
//        private ComboBox cmbQuantityType;
//        private ComboBox cmbAnalysisType; // New dropdown
//        private DateTimePicker dtpStartDate;
//        private DateTimePicker dtpEndDate;
//        private Button btnGenerate;
//        private Chart chartSalesTrend;
//        private DataGridView dgvSalesData;
//        private SplitContainer splitContainer;

//        public ProductSalesTrendForm()
//        {
//            InitializeComponent();
//            CreateControls();
//        }

//        private void CreateControls()
//        {
//            this.Size = new Size(1200, 800);
//            this.Text = "Product Sales Trend Analysis";
//            this.StartPosition = FormStartPosition.CenterScreen;

//            // Main container
//            splitContainer = new SplitContainer
//            {
//                Location = new Point(10, 10),
//                Size = new Size(1160, 740),
//                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
//                Orientation = Orientation.Horizontal,
//                SplitterDistance = 500 // Increased splitter distance for larger chart area
//            };

//            // Search and Filter Panel
//            Panel controlPanel = new Panel
//            {
//                Location = new Point(10, 10),
//                Size = new Size(1140, 100), // Increased height to accommodate all controls
//                BackColor = Color.WhiteSmoke,
//                BorderStyle = BorderStyle.FixedSingle
//            };

//            // Product Search - First Row
//            Label lblProductSearch = new Label
//            {
//                Text = "Product Search:",
//                Location = new Point(20, 15),
//                Size = new Size(100, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            txtProductSearch = new TextBox
//            {
//                Location = new Point(125, 12),
//                Size = new Size(200, 25),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            txtProductSearch.TextChanged += TxtProductSearch_TextChanged;

//            // Time Period Selection - First Row
//            Label lblTimePeriod = new Label
//            {
//                Text = "Time Period:",
//                Location = new Point(340, 15),
//                Size = new Size(80, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbTimePeriod = new ComboBox
//            {
//                Location = new Point(425, 12),
//                Size = new Size(120, 25),
//                DropDownStyle = ComboBoxStyle.DropDownList,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbTimePeriod.Items.AddRange(new[] { "Monthly", "Yearly" });
//            cmbTimePeriod.SelectedIndex = 0;
//            cmbTimePeriod.SelectedIndexChanged += CmbTimePeriod_SelectedIndexChanged;

//            // Quantity Type Filter - First Row
//            Label lblQuantityType = new Label
//            {
//                Text = "Quantity Type:",
//                Location = new Point(560, 15),
//                Size = new Size(85, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbQuantityType = new ComboBox
//            {
//                Location = new Point(650, 12),
//                Size = new Size(100, 25), // Increased width for better visibility
//                DropDownStyle = ComboBoxStyle.DropDownList,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            // Add all quantity types with "All" as default
//                        cmbQuantityType.Items.AddRange(new[] {
//                    "عدد",    // Piece/Unit
//                    "ڈبہ",    // Box
//                    "درجن",   // Dozen
//                    "پیکٹ",   // Packet
//                    "بنڈل",   // Bundle
//                    "کارٹن",  // Carton
//                    "رول",    // Roll
//                    "ڈبی",    // Tray/Container
//                    "کلو",    // Kilogram
//                    "گز",     // Yard
//                    "جوڑی"    // Pair
//                });
//            cmbQuantityType.SelectedIndex = 0;

//            // Analysis Type Filter - First Row (New Dropdown)
//            Label lblAnalysisType = new Label
//            {
//                Text = "Analysis Type:",
//                Location = new Point(765, 15),
//                Size = new Size(85, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbAnalysisType = new ComboBox
//            {
//                Location = new Point(855, 12),
//                Size = new Size(100, 25),
//                DropDownStyle = ComboBoxStyle.DropDownList,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            // Add analysis types
//            cmbAnalysisType.Items.AddRange(new[] { "Quantity", "Revenue" });
//            cmbAnalysisType.SelectedIndex = 0;

//            // Generate Button - First Row (Moved to accommodate new dropdown)
//            btnGenerate = new Button
//            {
//                Text = "Generate Report",
//                Location = new Point(970, 10),
//                Size = new Size(120, 30),
//                BackColor = Color.SteelBlue,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            btnGenerate.Click += BtnGenerate_Click;

//            // Start Date (Month/Year only) - Second Row
//            Label lblStartDate = new Label
//            {
//                Text = "From:",
//                Location = new Point(20, 55),
//                Size = new Size(40, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            dtpStartDate = new DateTimePicker
//            {
//                Location = new Point(65, 52),
//                Size = new Size(120, 25),
//                Format = DateTimePickerFormat.Custom,
//                CustomFormat = "MM/yyyy",
//                ShowUpDown = true,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            SetMonthYearPicker(dtpStartDate);
//            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);

//            // End Date (Month/Year only) - Second Row
//            Label lblEndDate = new Label
//            {
//                Text = "To:",
//                Location = new Point(200, 55),
//                Size = new Size(25, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            dtpEndDate = new DateTimePicker
//            {
//                Location = new Point(230, 52),
//                Size = new Size(120, 25),
//                Format = DateTimePickerFormat.Custom,
//                CustomFormat = "MM/yyyy",
//                ShowUpDown = true,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            SetMonthYearPicker(dtpEndDate);
//            dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

//            // Add controls to control panel
//            controlPanel.Controls.AddRange(new Control[] {
//        lblProductSearch, txtProductSearch,
//        lblTimePeriod, cmbTimePeriod,
//        lblQuantityType, cmbQuantityType,
//        lblAnalysisType, cmbAnalysisType, // Added new label and combobox
//        lblStartDate, dtpStartDate,
//        lblEndDate, dtpEndDate,
//        btnGenerate
//    });

//            // Chart for trends - adjust location to account for taller control panel
//            chartSalesTrend = new Chart
//            {
//                Location = new Point(10, 120), // Moved down to accommodate taller control panel
//                Size = new Size(1140, 370), // Increased height from 260 to 370 (110px increase)
//                BackColor = Color.White
//            };

//            // Data Grid for detailed view
//            dgvSalesData = new DataGridView
//            {
//                Location = new Point(10, 500), // Adjusted location due to larger chart
//                Size = new Size(1140, 230), // Reduced height to accommodate larger chart
//                ReadOnly = true,
//                BackColor = Color.White,
//                BorderStyle = BorderStyle.Fixed3D,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };

//            // Add to split container
//            splitContainer.Panel1.Controls.Add(controlPanel);
//            splitContainer.Panel1.Controls.Add(chartSalesTrend);
//            splitContainer.Panel2.Controls.Add(dgvSalesData);

//            this.Controls.Add(splitContainer);

//            // Load initial data
//            LoadInitialData();
//        }
//        private void SetMonthYearPicker(DateTimePicker picker)
//        {
//            picker.Format = DateTimePickerFormat.Custom;
//            picker.CustomFormat = "MM/yyyy";
//            picker.ShowUpDown = true;
//        }

//        private void CmbTimePeriod_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (cmbTimePeriod.SelectedItem?.ToString() == "Yearly")
//            {
//                dtpStartDate.CustomFormat = "yyyy";
//                dtpEndDate.CustomFormat = "yyyy";
//                dtpStartDate.Value = new DateTime(DateTime.Now.Year - 5, 1, 1);
//                dtpEndDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
//            }
//            else
//            {
//                dtpStartDate.CustomFormat = "MM/yyyy";
//                dtpEndDate.CustomFormat = "MM/yyyy";
//                dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
//                dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
//            }
//        }

//        private void TxtProductSearch_TextChanged(object sender, EventArgs e)
//        {
//            // Auto-search could be implemented here if needed
//        }

//        private void LoadInitialData()
//        {
//            InitializeChart();
//            InitializeDataGrid();
//        }

//        private void InitializeChart()
//        {
//            chartSalesTrend.Series.Clear();
//            chartSalesTrend.Titles.Clear();
//            chartSalesTrend.Legends.Clear();
//            chartSalesTrend.ChartAreas.Clear();

//            ChartArea chartArea = new ChartArea();
//            chartArea.BackColor = Color.White;
//            chartArea.AxisX.MajorGrid.Enabled = false;
//            chartArea.AxisY.MajorGrid.Enabled = true;
//            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
//            chartArea.AxisY2.Enabled = AxisEnabled.False;
//            chartSalesTrend.ChartAreas.Add(chartArea);

//            chartSalesTrend.Titles.Add("Product Sales Trend Analysis");
//            chartSalesTrend.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
//            chartSalesTrend.Titles[0].ForeColor = Color.SteelBlue;
//        }

//        private void InitializeDataGrid()
//        {
//            dgvSalesData.Columns.Clear();
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Period", DataPropertyName = "Period", Width = 150 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Product Name", DataPropertyName = "ProductName", Width = 250 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity Type", DataPropertyName = "QuantityType", Width = 100 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity Sold", DataPropertyName = "Quantity", Width = 120 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Revenue", DataPropertyName = "Revenue", Width = 150 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Average Price", DataPropertyName = "AveragePrice", Width = 120 });
//        }

//        private void BtnGenerate_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                LoadingManager.ShowLoading();
//                string searchTerm = txtProductSearch.Text.Trim();
//                string timePeriod = cmbTimePeriod.SelectedItem?.ToString() ?? "Monthly";
//                string quantityType = cmbQuantityType.SelectedItem?.ToString() ?? "All";
//                DateTime startDate = dtpStartDate.Value;
//                DateTime endDate = dtpEndDate.Value;

//                if (startDate > endDate)
//                {

//                    LoadingManager.HideLoading();
//                    MessageBox.Show("Start date cannot be after end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                var salesData = GetProductSalesTrend(searchTerm, startDate, endDate, timePeriod, quantityType);

//                if (salesData == null || !salesData.Any())
//                {
//                    LoadingManager.HideLoading();
//                    MessageBox.Show("No sales data found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    return;
//                }
//                InitializeChart();
//                GenerateTrendChart(salesData, timePeriod, quantityType);
//                UpdateDataGrid(salesData);

//            }
//            catch (Exception ex)
//            {
//                LoadingManager.HideLoading();
//                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {

//                LoadingManager.HideLoading();
//            }

//        }

//        private List<ProductTrendData> GetProductSalesTrend(string searchTerm, DateTime startDate, DateTime endDate, string timePeriod, string quantityType)
//        {
//            using (var context = new POSDbContext())
//            {
//                try
//                {
//                    // Calculate end date properly
//                    DateTime effectiveEndDate = timePeriod == "Yearly"
//                        ? new DateTime(endDate.Year, 12, 31)
//                        : new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

//                    // Get all order details first (this is safe since we're filtering by dates)
//                    var orderDetails = context.OrderDetails.AsNoTracking()

//                        .Where(od => od.ProductId == 1041 &&
//                                    od.Order.CreatedDate >= startDate &&
//                                    od.Order.CreatedDate <= effectiveEndDate
//                                   && od.QuantityType == quantityType
//                                    //&&(string.IsNullOrEmpty(searchTerm) || od.Product.ProductUrduName.Contains(searchTerm))
//                                    );


//                    var filteredDetails = orderDetails.Include(s=>s.Product).ToList();
//                    if(!filteredDetails.Any())
//                        return new List<ProductTrendData>();

//                    // Process in memory - much simpler and avoids EF translation issues
//                    if (timePeriod == "Yearly")
//                    {

//                        // Merge all quantity types into one record per period
//                        var result = filteredDetails
//                            .GroupBy(od => new
//                            {
//                                Year = od.Order.CreatedDate.Year,
//                                ProductId = od.ProductId,
//                                ProductName = od.Product.ProductUrduName
//                            })
//                            .Select(g => new ProductTrendData
//                            {
//                                Period = g.Key.Year.ToString(),
//                                ProductId = g.Key.ProductId.Value,
//                                ProductName = g.Key.ProductName,
//                                QuantityType = quantityType == "All" ? "All" : quantityType, // Set as "All" when merged
//                                Quantity = g.Sum(x => x.Quantity),
//                                Revenue = g.Sum(x => x.Quantity * x.Price),
//                                AveragePrice = g.Average(x => x.Price)
//                            })
//                            .OrderBy(x => x.Period)
//                            .ThenByDescending(x => x.Quantity)
//                            .ToList();

//                        return result;

//                    }
//                    else // Monthly
//                    {
//                        // Merge all quantity types into one record per period
//                        var result = filteredDetails
//                            .GroupBy(od => new
//                            {
//                                Year = od.Order.CreatedDate.Year,
//                                Month = od.Order.CreatedDate.Month,
//                                ProductId = od.ProductId,
//                                ProductName = od.Product.ProductUrduName
//                            })
//                            .Select(g => new ProductTrendData
//                            {
//                                Period = g.Key.Year + "-" + g.Key.Month.ToString("00"),
//                                ProductId = g.Key.ProductId.Value,
//                                ProductName = g.Key.ProductName,
//                                QuantityType = quantityType == "All" ? "All" : quantityType, // Set as "All" when merged
//                                Quantity = g.Sum(x => x.Quantity),
//                                Revenue = g.Sum(x => x.Quantity * x.Price),
//                                AveragePrice = g.Average(x => x.Price)
//                            })
//                            .OrderBy(x => x.Period)
//                            .ThenByDescending(x => x.Quantity)
//                            .ToList();

//                        return result;

//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return new List<ProductTrendData>();
//                }
//            }
//        }

//        private void GenerateTrendChart(List<ProductTrendData> salesData, string timePeriod, string quantityType)
//        {

//            chartSalesTrend.Series.Clear();
//            chartSalesTrend.Legends.Clear();
//            chartSalesTrend.ChartAreas[0].AxisX.LabelStyle.Format = "";

//            // Group by product for multiple series
//            var products = salesData.GroupBy(x => new { x.ProductId, x.ProductName });

//            // Color palette
//            Color[] colors = {
//                Color.SteelBlue, Color.Goldenrod, Color.ForestGreen, Color.IndianRed,
//                Color.DarkOrchid, Color.Teal, Color.Coral, Color.MediumSeaGreen,
//                Color.DodgerBlue, Color.Orange, Color.LimeGreen, Color.Violet
//            };

//            int colorIndex = 0;

//            foreach (var productGroup in products.Take(8)) // Limit to 8 products for clarity
//            {
//                var product = productGroup.First();
//                string shortName = GetShortProductName(product.ProductName, 15);

//                Series series = new Series(shortName);
//                series.ChartType = SeriesChartType.Column;
//                series.IsValueShownAsLabel = true;
//                series.Label = "#VALY";
//                series.Color = colors[colorIndex % colors.Length];
//                series.Font = new Font("Arial", 8);
//                series["PointWidth"] = "0.8";

//                foreach (var dataPoint in productGroup.OrderBy(x => x.Period))
//                {
//                    string periodLabel = timePeriod == "Yearly" ? dataPoint.Period : GetMonthName(dataPoint.Period);

//                    var da= cmbAnalysisType.SelectedItem.ToString()=="Revenue" ? dataPoint.Revenue : dataPoint.Quantity;
//                    series.Points.AddXY(periodLabel, da);
//                    series.Points[series.Points.Count - 1].ToolTip = $"{product.ProductName}\n{periodLabel}: {da} {dataPoint.QuantityType}\nRevenue: Rs. {dataPoint.Revenue:N0}";
//                }

//                chartSalesTrend.Series.Add(series);
//                colorIndex++;
//            }

//            // Add legend
//            Legend legend = new Legend();
//            legend.Docking = Docking.Top;
//            legend.Alignment = StringAlignment.Center;
//            legend.Font = new Font("Arial", 9);
//            legend.BackColor = Color.Transparent;
//            chartSalesTrend.Legends.Add(legend);

//            // Chart title with quantity type info
//            //        string quantityTypeText = quantityType == "All" ? $"All {cmbAnalysisType.SelectedItem.ToString()} Types" : quantityType;
//            string quantityTypeText = quantityType == "All" ? "All Quantity Types" : quantityType;
//            string title = timePeriod == "Yearly"
//                ? $"Yearly Sales Trend ({quantityTypeText})"
//                : $"Monthly Sales Trend ({quantityTypeText})";

//            chartSalesTrend.Titles[0].Text = title;

//            // Enable 3D for better visualization
//            chartSalesTrend.ChartAreas[0].Area3DStyle.Enable3D = true;
//            chartSalesTrend.ChartAreas[0].Area3DStyle.Inclination = 15;
//        }
//        private void UpdateDataGrid(List<ProductTrendData> salesData)
//        {
//            var gridData = salesData.Select(x => new
//            {
//                Period = cmbTimePeriod.SelectedItem?.ToString() == "Yearly" ? x.Period + " Year" : GetMonthName(x.Period),
//                x.ProductName,
//                x.QuantityType,
//                x.Quantity,
//                Revenue = x.Revenue.ToString("C2"),
//                AveragePrice = x.AveragePrice.ToString("C2")
//            }).ToList();

//            dgvSalesData.DataSource = gridData;

//            // Add summary row
//            AddSummaryRow(salesData);
//        }

//        private void AddSummaryRow(List<ProductTrendData> salesData)
//        {
//            string quantityType = cmbQuantityType.SelectedItem?.ToString() ?? "All";
//            string quantityTypeText = quantityType == "All" ? "All Types" : quantityType;

//            var summary = new
//            {
//                Period = "TOTAL",
//                ProductName = $"{salesData.Select(x => x.ProductId).Distinct().Count()} Products",
//                QuantityType = quantityTypeText,
//                Quantity = salesData.Sum(x => x.Quantity),
//                Revenue = salesData.Sum(x => x.Revenue).ToString(),
//                AveragePrice = salesData.Average(x => x.AveragePrice).ToString()
//            };

//            // You can add this as a footer or separate summary display
//            var summaryLabel = new Label
//            {
//                Text = $"Summary: {summary.Quantity} units sold ({quantityTypeText}) | Total Revenue: {summary.Revenue} | {summary.ProductName}",
//                Location = new Point(10, 370),
//                Size = new Size(1140, 25),
//                TextAlign = ContentAlignment.MiddleLeft,
//                Font = new Font("Arial", 10, FontStyle.Bold),
//                ForeColor = Color.DarkGreen,
//                BackColor = Color.LightYellow
//            };

//            if (splitContainer.Panel2.Controls.Count > 1)
//            {
//                var existingLabel = splitContainer.Panel2.Controls.OfType<Label>().FirstOrDefault();
//                if (existingLabel != null)
//                    splitContainer.Panel2.Controls.Remove(existingLabel);
//            }

//            splitContainer.Panel2.Controls.Add(summaryLabel);
//            summaryLabel.BringToFront();
//        }

//        private string GetMonthName(string period)
//        {
//            try
//            {
//                var parts = period.Split('-');
//                if (parts.Length == 2 && int.TryParse(parts[0], out int year) && int.TryParse(parts[1], out int month))
//                {
//                    return new DateTime(year, month, 1).ToString("MMM yyyy");
//                }
//            }
//            catch
//            {
//                // If parsing fails, return original period
//            }
//            return period;
//        }

//        private string GetShortProductName(string fullName, int maxLength = 15)
//        {
//            if (string.IsNullOrEmpty(fullName))
//                return "Unknown";

//            if (fullName.Length <= maxLength)
//                return fullName;

//            return fullName.Substring(0, maxLength - 3) + "...";
//        }
//    }

//    public class ProductTrendData
//    {
//        public string Period { get; set; }
//        public int ProductId { get; set; }
//        public string ProductName { get; set; }
//        public string QuantityType { get; set; }
//        public int Quantity { get; set; }
//        public float Revenue { get; set; }
//        public float AveragePrice { get; set; }
//    }
//}



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
//    public partial class ProductSalesTrendForm : Form
//    {
//        private TextBox txtProductSearch;
//        private ComboBox cmbTimePeriod;
//        private ComboBox cmbQuantityType;
//        private ComboBox cmbAnalysisType;
//        private DateTimePicker dtpStartDate;
//        private DateTimePicker dtpEndDate;
//        private Button btnGenerate;
//        private Chart chartSalesTrend;
//        private DataGridView dgvSalesData;
//        private DataGridView dgvProductSearch; // New DataGridView for product search
//        private SplitContainer splitContainer;
//        private Panel productSearchPanel; // New panel for search area

//        private int selectedProductId = 0;

//        public ProductSalesTrendForm()
//        {
//            InitializeComponent();
//            CreateControls();
//        }

//        private void CreateControls()
//        {
//            this.Size = new Size(1190, 600);
//            this.Text = "Product Sales Trend Analysis";
//            this.StartPosition = FormStartPosition.CenterScreen;

//            // Main container
//            splitContainer = new SplitContainer
//            {
//                Location = new Point(10, 10),
//                Size = new Size(1160, 740),
//                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
//                Orientation = Orientation.Horizontal,
//                SplitterDistance = 500
//            };

//            // Search and Filter Panel
//            Panel controlPanel = new Panel
//            {
//                Location = new Point(10, 10),
//                Size = new Size(1140, 150), // Increased height to accommodate product search grid
//                BackColor = Color.WhiteSmoke,
//                BorderStyle = BorderStyle.FixedSingle
//            };

//            // Product Search Panel - Contains search box and product grid
//            productSearchPanel = new Panel
//            {
//                Location = new Point(20, 10),
//                Size = new Size(400, 130),
//                BorderStyle = BorderStyle.FixedSingle,
//                BackColor = Color.White
//            };

//            // Product Search Label
//            Label lblProductSearch = new Label
//            {
//                Text = "Product Search:",
//                Location = new Point(5, 8),
//                Size = new Size(100, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };

//            // Product Search TextBox
//            txtProductSearch = new TextBox
//            {
//                Location = new Point(110, 5),
//                Size = new Size(280, 25),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            txtProductSearch.TextChanged += TxtProductSearch_TextChanged;

//            // Product Search DataGridView
//            dgvProductSearch = new DataGridView
//            {
//                Location = new Point(5, 35),
//                Size = new Size(390, 90),
//                ReadOnly = true,
//                BackColor = Color.White,
//                BorderStyle = BorderStyle.Fixed3D,
//                AllowUserToAddRows = false,
//                AllowUserToDeleteRows = false,
//                AllowUserToResizeRows = false,
//                RowHeadersVisible = false,
//                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
//                Font = new Font("Arial", 8, FontStyle.Regular),
//                Visible = false // Initially hidden
//            };

//            // Configure product search grid columns
//            dgvProductSearch.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                HeaderText = "ID",
//                DataPropertyName = "Id",
//                Width = 50,
//                Visible = false // Hide ID column
//            });
//            dgvProductSearch.Columns.Add(new DataGridViewTextBoxColumn
//            {
//                HeaderText = "Product Name",
//                DataPropertyName = "Name",
//                Width = 340,
//                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
//            });

//            dgvProductSearch.CellClick += DgvProductSearch_CellClick;
//            dgvProductSearch.KeyDown += DgvProductSearch_KeyDown;

//            // Add controls to product search panel
//            productSearchPanel.Controls.Add(lblProductSearch);
//            productSearchPanel.Controls.Add(txtProductSearch);
//            productSearchPanel.Controls.Add(dgvProductSearch);

//            // Time Period Selection
//            Label lblTimePeriod = new Label
//            {
//                Text = "Time Period:",
//                Location = new Point(440, 15),
//                Size = new Size(80, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbTimePeriod = new ComboBox
//            {
//                Location = new Point(525, 12),
//                Size = new Size(120, 25),
//                DropDownStyle = ComboBoxStyle.DropDownList,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbTimePeriod.Items.AddRange(new[] { "Monthly", "Yearly" });
//            cmbTimePeriod.SelectedIndex = 0;
//            cmbTimePeriod.SelectedIndexChanged += CmbTimePeriod_SelectedIndexChanged;

//            // Quantity Type Filter
//            Label lblQuantityType = new Label
//            {
//                Text = "Quantity Type:",
//                Location = new Point(660, 15),
//                Size = new Size(85, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbQuantityType = new ComboBox
//            {
//                Location = new Point(750, 12),
//                Size = new Size(100, 25),
//                DropDownStyle = ComboBoxStyle.DropDownList,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbQuantityType.Items.AddRange(new[] {
//                "عدد",    // Piece/Unit
//                "ڈبہ",    // Box
//                "درجن",   // Dozen
//                "پیکٹ",   // Packet
//                "بنڈل",   // Bundle
//                "کارٹن",  // Carton
//                "رول",    // Roll
//                "ڈبی",    // Tray/Container
//                "کلو",    // Kilogram
//                "گز",     // Yard
//                "جوڑی"    // Pair
//            });
//            cmbQuantityType.SelectedIndex = 0;

//            // Analysis Type Filter
//            Label lblAnalysisType = new Label
//            {
//                Text = "Analysis Type:",
//                Location = new Point(865, 15),
//                Size = new Size(85, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbAnalysisType = new ComboBox
//            {
//                Location = new Point(955, 12),
//                Size = new Size(100, 25),
//                DropDownStyle = ComboBoxStyle.DropDownList,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            cmbAnalysisType.Items.AddRange(new[] { "Quantity", "Revenue" });
//            cmbAnalysisType.SelectedIndex = 0;

//            // Generate Button
//            btnGenerate = new Button
//            {
//                Text = "Generate Report",
//                Location = new Point(780, 50),
//                Size = new Size(120, 30),
//                BackColor = Color.SteelBlue,
//                ForeColor = Color.White,
//                Font = new Font("Arial", 10, FontStyle.Bold)
//            };
//            btnGenerate.Click += BtnGenerate_Click;

//            // Start Date (Month/Year only)
//            Label lblStartDate = new Label
//            {
//                Text = "From:",
//                Location = new Point(440, 55),
//                Size = new Size(40, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            dtpStartDate = new DateTimePicker
//            {
//                Location = new Point(485, 52),
//                Size = new Size(120, 25),
//                Format = DateTimePickerFormat.Custom,
//                CustomFormat = "MM/yyyy",
//                ShowUpDown = true,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            SetMonthYearPicker(dtpStartDate);
//            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);

//            // End Date (Month/Year only)
//            Label lblEndDate = new Label
//            {
//                Text = "To:",
//                Location = new Point(620, 55),
//                Size = new Size(25, 20),
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            dtpEndDate = new DateTimePicker
//            {
//                Location = new Point(650, 52),
//                Size = new Size(120, 25),
//                Format = DateTimePickerFormat.Custom,
//                CustomFormat = "MM/yyyy",
//                ShowUpDown = true,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };
//            SetMonthYearPicker(dtpEndDate);
//            dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

//            // Add controls to control panel
//            controlPanel.Controls.AddRange(new Control[] {
//                productSearchPanel,
//                lblTimePeriod, cmbTimePeriod,
//                lblQuantityType, cmbQuantityType,
//                lblAnalysisType, cmbAnalysisType,
//                lblStartDate, dtpStartDate,
//                lblEndDate, dtpEndDate,
//                btnGenerate
//            });

//            // Chart for trends
//            chartSalesTrend = new Chart
//            {
//                Location = new Point(10, 170), // Adjusted for taller control panel
//                Size = new Size(1140, 320),
//                BackColor = Color.White
//            };

//            // Data Grid for detailed view
//            dgvSalesData = new DataGridView
//            {
//                Location = new Point(10, 500),
//                Size = new Size(1140, 230),
//                ReadOnly = true,
//                BackColor = Color.White,
//                BorderStyle = BorderStyle.Fixed3D,
//                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
//                Font = new Font("Arial", 9, FontStyle.Regular)
//            };

//            // Add to split container
//            splitContainer.Panel1.Controls.Add(controlPanel);
//            splitContainer.Panel1.Controls.Add(chartSalesTrend);
//            splitContainer.Panel2.Controls.Add(dgvSalesData);

//            this.Controls.Add(splitContainer);

//            // Load initial data
//            LoadInitialData();
//        }

//        private void SetMonthYearPicker(DateTimePicker picker)
//        {
//            picker.Format = DateTimePickerFormat.Custom;
//            picker.CustomFormat = "MM/yyyy";
//            picker.ShowUpDown = true;
//        }

//        private void CmbTimePeriod_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            if (cmbTimePeriod.SelectedItem?.ToString() == "Yearly")
//            {
//                dtpStartDate.CustomFormat = "yyyy";
//                dtpEndDate.CustomFormat = "yyyy";
//                dtpStartDate.Value = new DateTime(DateTime.Now.Year - 5, 1, 1);
//                dtpEndDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
//            }
//            else
//            {
//                dtpStartDate.CustomFormat = "MM/yyyy";
//                dtpEndDate.CustomFormat = "MM/yyyy";
//                dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
//                dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
//            }
//        }

//        private void TxtProductSearch_TextChanged(object sender, EventArgs e)
//        {
//            // Show/hide product grid based on search text
//            if (string.IsNullOrEmpty(txtProductSearch.Text))
//            {
//                dgvProductSearch.Visible = false;
//                return;
//            }

//            // Search for products
//            var products = SearchProducts(txtProductSearch.Text);

//            if (products.Any())
//            {
//                dgvProductSearch.DataSource = products;
//                dgvProductSearch.Visible = true;
//                dgvProductSearch.BringToFront();
//            }
//            else
//            {
//                dgvProductSearch.Visible = false;
//            }
//        }

//        private void DgvProductSearch_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex >= 0 && e.RowIndex < dgvProductSearch.Rows.Count)
//            {
//                SelectProductFromGrid();
//            }
//        }

//        private void DgvProductSearch_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Enter && dgvProductSearch.SelectedRows.Count > 0)
//            {
//                SelectProductFromGrid();
//                e.Handled = true;
//            }
//            else if (e.KeyCode == Keys.Escape)
//            {
//                dgvProductSearch.Visible = false;
//                e.Handled = true;
//            }
//        }

//        private void SelectProductFromGrid()
//        {
//            if (dgvProductSearch.SelectedRows.Count > 0)
//            {
//                var selectedRow = dgvProductSearch.SelectedRows[0];
//                string productName = selectedRow.Cells[1].Value?.ToString();
//                selectedProductId= selectedRow.Cells[0].Value != null ? Convert.ToInt32(selectedRow.Cells[0].Value) : 0;
//                if (!string.IsNullOrEmpty(productName))
//                {
//                    txtProductSearch.Text = productName;
//                    dgvProductSearch.Visible = false;
//                    txtProductSearch.Focus();
//                }
//            }
//        }

//        private List<ProductSearchResult> SearchProducts(string searchTerm)
//        {
//            try
//            {
//                using (var context = new POSDbContext())
//                {
//                    var query = context.Products.AsNoTracking();

//                    if (!string.IsNullOrEmpty(searchTerm))
//                    {
//                        var searchWords = searchTerm.ToLower().Split(' ');

//                        foreach (var word in searchWords)
//                        {
//                            query = (System.Data.Entity.Infrastructure.DbQuery<Models.Product>)query.Where(s =>
//                                s.ProductEnglishName.ToLower().Contains(word) ||
//                                s.Id.ToString().Contains(word) ||
//                                s.SearchByProductCode.ToLower().Contains(word));
//                        }
//                    }

//                    var data = query
//                        .Select(p => new ProductSearchResult
//                        {
//                            Id = p.Id,
//                            Name = p.ProductUrduName
//                        })
//                        .Take(20) // Limit results for performance
//                        .ToList();

//                    return data;
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Error searching products: {ex.Message}", "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return new List<ProductSearchResult>();
//            }
//        }

//        // Helper class for product search results
//        public class ProductSearchResult
//        {
//            public int Id { get; set; }
//            public string Name { get; set; }
//        }

//        // Handle click outside to hide the product grid
//        protected override void OnClick(EventArgs e)
//        {
//            base.OnClick(e);
//            // Hide product grid when clicking outside
//            if (!productSearchPanel.Bounds.Contains(PointToClient(MousePosition)))
//            {
//                dgvProductSearch.Visible = false;
//            }
//        }

//        private void LoadInitialData()
//        {
//            InitializeChart();
//            InitializeDataGrid();
//        }

//        private void InitializeChart()
//        {
//            chartSalesTrend.Series.Clear();
//            chartSalesTrend.Titles.Clear();
//            chartSalesTrend.Legends.Clear();
//            chartSalesTrend.ChartAreas.Clear();

//            ChartArea chartArea = new ChartArea();
//            chartArea.BackColor = Color.White;
//            chartArea.AxisX.MajorGrid.Enabled = false;
//            chartArea.AxisY.MajorGrid.Enabled = true;
//            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
//            chartArea.AxisY2.Enabled = AxisEnabled.False;
//            chartSalesTrend.ChartAreas.Add(chartArea);

//            chartSalesTrend.Titles.Add("Product Sales Trend Analysis");
//            chartSalesTrend.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
//            chartSalesTrend.Titles[0].ForeColor = Color.SteelBlue;
//        }

//        private void InitializeDataGrid()
//        {
//            dgvSalesData.Columns.Clear();
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Period", DataPropertyName = "Period", Width = 150 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Product Name", DataPropertyName = "ProductName", Width = 250 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity Type", DataPropertyName = "QuantityType", Width = 100 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Quantity Sold", DataPropertyName = "Quantity", Width = 120 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total Revenue", DataPropertyName = "Revenue", Width = 150 });
//            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Average Price", DataPropertyName = "AveragePrice", Width = 120 });
//        }

//        private void BtnGenerate_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                LoadingManager.ShowLoading();
//                string searchTerm = txtProductSearch.Text.Trim();

//                string timePeriod = cmbTimePeriod.SelectedItem?.ToString() ?? "Monthly";
//                string quantityType = cmbQuantityType.SelectedItem?.ToString() ?? "All";
//                DateTime startDate = dtpStartDate.Value;
//                DateTime endDate = dtpEndDate.Value;

//                if(string.IsNullOrEmpty(searchTerm))
//                    {
//                    LoadingManager.HideLoading();
//                    MessageBox.Show("Please select a product to generate the report.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                if (startDate > endDate)
//                {
//                    LoadingManager.HideLoading();
//                    MessageBox.Show("Start date cannot be after end date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//                    return;
//                }

//                var salesData = GetProductSalesTrend(selectedProductId.ToString(), startDate, endDate, timePeriod, quantityType);

//                if (salesData == null || !salesData.Any())
//                {
//                    LoadingManager.HideLoading();
//                    MessageBox.Show("No sales data found for the selected criteria.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
//                    return;
//                }
//                InitializeChart();
//                GenerateTrendChart(salesData, timePeriod, quantityType);
//                UpdateDataGrid(salesData);

//            }
//            catch (Exception ex)
//            {
//                LoadingManager.HideLoading();
//                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//            finally
//            {
//                LoadingManager.HideLoading();
//            }
//        }

//        private List<ProductTrendData> GetProductSalesTrend(string searchTerm, DateTime startDate, DateTime endDate, string timePeriod, string quantityType)
//        {
//            using (var context = new POSDbContext())
//            {
//                try
//                {
//                    int id = int.Parse(searchTerm);
//                    // Calculate end date properly
//                    DateTime effectiveEndDate = timePeriod == "Yearly"
//                        ? new DateTime(endDate.Year, 12, 31)
//                        : new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month));

//                    // Get all order details first (this is safe since we're filtering by dates)
//                    var orderDetails = context.OrderDetails.AsNoTracking()
//                        .Where(od => od.ProductId == id &&
//                                    od.Order.CreatedDate >= startDate &&
//                                    od.Order.CreatedDate <= effectiveEndDate
//                                   && od.QuantityType == quantityType);

//                    var filteredDetails = orderDetails.Include(s => s.Product).ToList();
//                    if (!filteredDetails.Any())
//                        return new List<ProductTrendData>();

//                    // Process in memory - much simpler and avoids EF translation issues
//                    if (timePeriod == "Yearly")
//                    {
//                        // Merge all quantity types into one record per period
//                        var result = filteredDetails
//                            .GroupBy(od => new
//                            {
//                                Year = od.Order.CreatedDate.Year,
//                                ProductId = od.ProductId,
//                                ProductName = od.Product.ProductUrduName
//                            })
//                            .Select(g => new ProductTrendData
//                            {
//                                Period = g.Key.Year.ToString(),
//                                ProductId = g.Key.ProductId.Value,
//                                ProductName = g.Key.ProductName,
//                                QuantityType = quantityType == "All" ? "All" : quantityType,
//                                Quantity = g.Sum(x => x.Quantity),
//                                Revenue = g.Sum(x => x.Quantity * x.Price),
//                                AveragePrice = g.Average(x => x.Price)
//                            })
//                            .OrderBy(x => x.Period)
//                            .ThenByDescending(x => x.Quantity)
//                            .ToList();

//                        return result;
//                    }
//                    else // Monthly
//                    {
//                        // Merge all quantity types into one record per period
//                        var result = filteredDetails
//                            .GroupBy(od => new
//                            {
//                                Year = od.Order.CreatedDate.Year,
//                                Month = od.Order.CreatedDate.Month,
//                                ProductId = od.ProductId,
//                                ProductName = od.Product.ProductUrduName
//                            })
//                            .Select(g => new ProductTrendData
//                            {
//                                Period = g.Key.Year + "-" + g.Key.Month.ToString("00"),
//                                ProductId = g.Key.ProductId.Value,
//                                ProductName = g.Key.ProductName,
//                                QuantityType = quantityType == "All" ? "All" : quantityType,
//                                Quantity = g.Sum(x => x.Quantity),
//                                Revenue = g.Sum(x => x.Quantity * x.Price),
//                                AveragePrice = g.Average(x => x.Price)
//                            })
//                            .OrderBy(x => x.Period)
//                            .ThenByDescending(x => x.Quantity)
//                            .ToList();

//                        return result;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    return new List<ProductTrendData>();
//                }
//            }
//        }

//        private void GenerateTrendChart(List<ProductTrendData> salesData, string timePeriod, string quantityType)
//        {
//            chartSalesTrend.Series.Clear();
//            chartSalesTrend.Legends.Clear();
//            chartSalesTrend.ChartAreas[0].AxisX.LabelStyle.Format = "";

//            // Group by product for multiple series
//            var products = salesData.GroupBy(x => new { x.ProductId, x.ProductName });

//            // Color palette
//            Color[] colors = {
//                Color.SteelBlue, Color.Goldenrod, Color.ForestGreen, Color.IndianRed,
//                Color.DarkOrchid, Color.Teal, Color.Coral, Color.MediumSeaGreen,
//                Color.DodgerBlue, Color.Orange, Color.LimeGreen, Color.Violet
//            };

//            int colorIndex = 0;

//            foreach (var productGroup in products.Take(8))
//            {
//                var product = productGroup.First();
//                string shortName = GetShortProductName(product.ProductName, 15);

//                Series series = new Series(shortName);
//                series.ChartType = SeriesChartType.Column;
//                series.IsValueShownAsLabel = true;
//                series.Label = "#VALY";
//                series.Color = colors[colorIndex % colors.Length];
//                series.Font = new Font("Arial", 8);
//                series["PointWidth"] = "0.8";

//                foreach (var dataPoint in productGroup.OrderBy(x => x.Period))
//                {
//                    string periodLabel = timePeriod == "Yearly" ? dataPoint.Period : GetMonthName(dataPoint.Period);

//                    var da = cmbAnalysisType.SelectedItem.ToString() == "Revenue" ? dataPoint.Revenue : dataPoint.Quantity;
//                    series.Points.AddXY(periodLabel, da);
//                    series.Points[series.Points.Count - 1].ToolTip = $"{product.ProductName}\n{periodLabel}: {da} {dataPoint.QuantityType}\nRevenue: Rs. {dataPoint.Revenue:N0}";
//                }

//                chartSalesTrend.Series.Add(series);
//                colorIndex++;
//            }

//            // Add legend
//            Legend legend = new Legend();
//            legend.Docking = Docking.Top;
//            legend.Alignment = StringAlignment.Center;
//            legend.Font = new Font("Arial", 9);
//            legend.BackColor = Color.Transparent;
//            chartSalesTrend.Legends.Add(legend);

//            string quantityTypeText = quantityType == "All" ? "All Quantity Types" : quantityType;
//            string title = timePeriod == "Yearly"
//                ? $"Yearly Sales Trend ({quantityTypeText})"
//                : $"Monthly Sales Trend ({quantityTypeText})";

//            chartSalesTrend.Titles[0].Text = title;

//            // Enable 3D for better visualization
//            chartSalesTrend.ChartAreas[0].Area3DStyle.Enable3D = true;
//            chartSalesTrend.ChartAreas[0].Area3DStyle.Inclination = 15;
//        }

//        private void UpdateDataGrid(List<ProductTrendData> salesData)
//        {
//            var gridData = salesData.Select(x => new
//            {
//                Period = cmbTimePeriod.SelectedItem?.ToString() == "Yearly" ? x.Period + " Year" : GetMonthName(x.Period),
//                x.ProductName,
//                x.QuantityType,
//                x.Quantity,
//                Revenue = x.Revenue.ToString("C2"),
//                AveragePrice = x.AveragePrice.ToString("C2")
//            }).ToList();

//            dgvSalesData.DataSource = gridData;

//            // Add summary row
//            AddSummaryRow(salesData);
//        }

//        private void AddSummaryRow(List<ProductTrendData> salesData)
//        {
//            string quantityType = cmbQuantityType.SelectedItem?.ToString() ?? "All";
//            string quantityTypeText = quantityType == "All" ? "All Types" : quantityType;

//            var summary = new
//            {
//                Period = "TOTAL",
//                ProductName = $"{salesData.Select(x => x.ProductId).Distinct().Count()} Products",
//                QuantityType = quantityTypeText,
//                Quantity = salesData.Sum(x => x.Quantity),
//                Revenue = salesData.Sum(x => x.Revenue).ToString(),
//                AveragePrice = salesData.Average(x => x.AveragePrice).ToString()
//            };

//            var summaryLabel = new Label
//            {
//                Text = $"Summary: {summary.Quantity} units sold ({quantityTypeText}) | Total Revenue: {summary.Revenue} | {summary.ProductName}",
//                Location = new Point(10, 370),
//                Size = new Size(1140, 25),
//                TextAlign = ContentAlignment.MiddleLeft,
//                Font = new Font("Arial", 10, FontStyle.Bold),
//                ForeColor = Color.DarkGreen,
//                BackColor = Color.LightYellow
//            };

//            if (splitContainer.Panel2.Controls.Count > 1)
//            {
//                var existingLabel = splitContainer.Panel2.Controls.OfType<Label>().FirstOrDefault();
//                if (existingLabel != null)
//                    splitContainer.Panel2.Controls.Remove(existingLabel);
//            }

//            splitContainer.Panel2.Controls.Add(summaryLabel);
//            summaryLabel.BringToFront();
//        }

//        private string GetMonthName(string period)
//        {
//            try
//            {
//                var parts = period.Split('-');
//                if (parts.Length == 2 && int.TryParse(parts[0], out int year) && int.TryParse(parts[1], out int month))
//                {
//                    return new DateTime(year, month, 1).ToString("MMM yyyy");
//                }
//            }
//            catch
//            {
//                // If parsing fails, return original period
//            }
//            return period;
//        }

//        private string GetShortProductName(string fullName, int maxLength = 15)
//        {
//            if (string.IsNullOrEmpty(fullName))
//                return "Unknown";

//            if (fullName.Length <= maxLength)
//                return fullName;

//            return fullName.Substring(0, maxLength - 3) + "...";
//        }
//    }

//    public class ProductTrendData
//    {
//        public string Period { get; set; }
//        public int ProductId { get; set; }
//        public string ProductName { get; set; }
//        public string QuantityType { get; set; }
//        public int Quantity { get; set; }
//        public float Revenue { get; set; }
//        public float AveragePrice { get; set; }
//    }
//}



using POS_Shop.DTOs.Product;
using POS_Shop.Helpers;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_Shop.Views.Reports
{
    public partial class ProductSalesTrendForm : Form
    {
        private TextBox txtProductSearch;
        private ComboBox cmbTimePeriod;
        private ComboBox cmbQuantityType;
        private ComboBox cmbAnalysisType;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpEndDate;
        private Button btnGenerate;
        private Chart chartSalesTrend;
        private DataGridView dgvSalesData;
        private DataGridView dgvProductSearch;
        private SplitContainer splitContainer;
        private Panel productSearchPanel;

        private int selectedProductId = 0;
        private System.Threading.Timer _searchDebounceTimer;
        private const int DEBOUNCE_DELAY_MS = 300;
        private const int MIN_SEARCH_LENGTH = 2;

        // Cache for product search
        private static Dictionary<string, List<ProductSearchResult>> _searchCache =
            new Dictionary<string, List<ProductSearchResult>>();
        private static DateTime _cacheExpiry = DateTime.MinValue;
        private const int CACHE_DURATION_MINUTES = 5;

        // Cancellation token for async operations
        private CancellationTokenSource _cts;

        public ProductSalesTrendForm()
        {
            InitializeComponent();
            CreateControls();
        }

        private void CreateControls()
        {
            this.Size = new Size(1190, 800);
            this.Text = "Product Sales Trend Analysis";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Main container
            splitContainer = new SplitContainer
            {
                Location = new Point(10, 10),
                Size = new Size(1160, 740),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 500
            };

            // Search and Filter Panel
            Panel controlPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(1140, 150),
                BackColor = Color.WhiteSmoke,
                BorderStyle = BorderStyle.FixedSingle
            };

            // Product Search Panel
            productSearchPanel = new Panel
            {
                Location = new Point(20, 10),
                Size = new Size(400, 130),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            // Product Search Label
            Label lblProductSearch = new Label
            {
                Text = "Product Search:",
                Location = new Point(5, 8),
                Size = new Size(100, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };

            // Product Search TextBox
            txtProductSearch = new TextBox
            {
                Location = new Point(110, 5),
                Size = new Size(280, 25),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            txtProductSearch.TextChanged += TxtProductSearch_TextChanged;

            // Product Search DataGridView
            dgvProductSearch = new DataGridView
            {
                Location = new Point(5, 35),
                Size = new Size(390, 90),
                ReadOnly = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Arial", 8, FontStyle.Regular),
                Visible = false,
                EnableHeadersVisualStyles = false
            };

            // Enable double buffering using extension method
            dgvProductSearch.EnableDoubleBuffering();

            // Configure product search grid columns
            dgvProductSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "ID",
                Name = "Id",
                DataPropertyName = "Id",
                Width = 50,
                Visible = false
            });
            dgvProductSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product Name",
                Name = "Name",
                DataPropertyName = "Name",
                Width = 340,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            }); 
            dgvProductSearch.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Urdu Name",
                Name = "urduName",
                DataPropertyName = "urduName",
                Width = 340,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProductSearch.CellClick += DgvProductSearch_CellClick;
            dgvProductSearch.KeyDown += DgvProductSearch_KeyDown;

            // Add controls to product search panel
            productSearchPanel.Controls.Add(lblProductSearch);
            productSearchPanel.Controls.Add(txtProductSearch);
            productSearchPanel.Controls.Add(dgvProductSearch);

            // Time Period Selection
            Label lblTimePeriod = new Label
            {
                Text = "Time Period:",
                Location = new Point(440, 15),
                Size = new Size(80, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            cmbTimePeriod = new ComboBox
            {
                Location = new Point(525, 12),
                Size = new Size(120, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            cmbTimePeriod.Items.AddRange(new[] { "Monthly", "Yearly" });
            cmbTimePeriod.SelectedIndex = 0;
            cmbTimePeriod.SelectedIndexChanged += CmbTimePeriod_SelectedIndexChanged;

            // Quantity Type Filter
            Label lblQuantityType = new Label
            {
                Text = "Quantity Type:",
                Location = new Point(660, 15),
                Size = new Size(85, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            cmbQuantityType = new ComboBox
            {
                Location = new Point(750, 12),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            cmbQuantityType.Items.AddRange(new[] {
                "عدد", "ڈبہ", "درجن", "پیکٹ", "بنڈل",
                "کارٹن", "رول", "ڈبی", "کلو", "گز", "جوڑی"
            });
            cmbQuantityType.SelectedIndex = 0;

            // Analysis Type Filter
            Label lblAnalysisType = new Label
            {
                Text = "Analysis Type:",
                Location = new Point(865, 15),
                Size = new Size(85, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            cmbAnalysisType = new ComboBox
            {
                Location = new Point(955, 12),
                Size = new Size(100, 25),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            cmbAnalysisType.Items.AddRange(new[] { "Quantity", "Revenue" });
            cmbAnalysisType.SelectedIndex = 0;

            // Generate Button
            btnGenerate = new Button
            {
                Text = "Generate Report",
                Location = new Point(780, 50),
                Size = new Size(120, 30),
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnGenerate.Click += BtnGenerate_Click;

            // Start Date
            Label lblStartDate = new Label
            {
                Text = "From:",
                Location = new Point(440, 55),
                Size = new Size(40, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            dtpStartDate = new DateTimePicker
            {
                Location = new Point(485, 52),
                Size = new Size(120, 25),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MM/yyyy",
                ShowUpDown = true,
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            SetMonthYearPicker(dtpStartDate);
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);

            // End Date
            Label lblEndDate = new Label
            {
                Text = "To:",
                Location = new Point(620, 55),
                Size = new Size(25, 20),
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            dtpEndDate = new DateTimePicker
            {
                Location = new Point(650, 52),
                Size = new Size(120, 25),
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "MM/yyyy",
                ShowUpDown = true,
                Font = new Font("Arial", 9, FontStyle.Regular)
            };
            SetMonthYearPicker(dtpEndDate);
            dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Add controls to control panel
            controlPanel.Controls.AddRange(new Control[] {
                productSearchPanel,
                lblTimePeriod, cmbTimePeriod,
                lblQuantityType, cmbQuantityType,
                lblAnalysisType, cmbAnalysisType,
                lblStartDate, dtpStartDate,
                lblEndDate, dtpEndDate,
                btnGenerate
            });

            // Chart for trends
            chartSalesTrend = new Chart
            {
                Location = new Point(10, 170),
                Size = new Size(1140, 320),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Data Grid for detailed view
            dgvSalesData = new DataGridView
            {
                Location = new Point(10, 500),
                Size = new Size(1140, 230),
                ReadOnly = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Arial", 9, FontStyle.Regular),
                EnableHeadersVisualStyles = false
            };

            // Enable double buffering using extension method
            dgvSalesData.EnableDoubleBuffering();

            // Add to split container
            splitContainer.Panel1.Controls.Add(controlPanel);
            splitContainer.Panel1.Controls.Add(chartSalesTrend);
            splitContainer.Panel2.Controls.Add(dgvSalesData);

            this.Controls.Add(splitContainer);

            // Load initial data
            LoadInitialData();
        }

        private void SetMonthYearPicker(DateTimePicker picker)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = "MM/yyyy";
            picker.ShowUpDown = true;
        }

        private void CmbTimePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTimePeriod.SelectedItem?.ToString() == "Yearly")
            {
                dtpStartDate.CustomFormat = "yyyy";
                dtpEndDate.CustomFormat = "yyyy";
                dtpStartDate.Value = new DateTime(DateTime.Now.Year - 5, 1, 1);
                dtpEndDate.Value = new DateTime(DateTime.Now.Year, 1, 1);
            }
            else
            {
                dtpStartDate.CustomFormat = "MM/yyyy";
                dtpEndDate.CustomFormat = "MM/yyyy";
                dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-11);
                dtpEndDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
        }

        #region Product Search with Debouncing

        private void TxtProductSearch_TextChanged(object sender, EventArgs e)
        {
            // Cancel previous timer
            _searchDebounceTimer?.Dispose();

            var searchTerm = txtProductSearch.Text.Trim();

            // Hide grid if search is empty or too short
            if (string.IsNullOrEmpty(searchTerm) || searchTerm.Length < MIN_SEARCH_LENGTH)
            {
                dgvProductSearch.Visible = false;
                return;
            }

            // Start new timer for debounced search
            _searchDebounceTimer = new System.Threading.Timer(
                callback: _ =>
                {
                    try
                    {
                        // Execute on UI thread
                        if (!this.IsDisposed)
                        {
                            this.Invoke(new Action(() => PerformProductSearch(searchTerm)));
                        }
                    }
                    catch (ObjectDisposedException)
                    {
                        // Form is disposed, ignore
                    }
                },
                state: null,
                dueTime: DEBOUNCE_DELAY_MS,
                period: Timeout.Infinite
            );
        }

        private void PerformProductSearch(string searchTerm)
        {
            try
            {
                var products = SearchProducts(searchTerm);

                if (products != null && products.Any())
                {
                    dgvProductSearch.DataSource = products;
                    dgvProductSearch.Visible = true;
                    dgvProductSearch.BringToFront();
                }
                else
                {
                    dgvProductSearch.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Search error: {ex.Message}");
                dgvProductSearch.Visible = false;
            }
        }

        private List<ProductSearchResult> SearchProducts(string searchTerm)
        {
            try
            {
                searchTerm = searchTerm.Trim();

                // Check cache
                string cacheKey = searchTerm.ToLower();
                if (_searchCache.ContainsKey(cacheKey) && DateTime.Now < _cacheExpiry)
                {
                    return _searchCache[cacheKey];
                }

                // Clear old cache
                if (DateTime.Now >= _cacheExpiry)
                {
                    _searchCache.Clear();
                    _cacheExpiry = DateTime.Now.AddMinutes(CACHE_DURATION_MINUTES);
                }

                using (var context = new POSDbContext())
                {
                    // Build optimized query
                    var query = context.Products.AsNoTracking();

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        // Split search terms
                        var searchWords = searchTerm.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (var word in searchWords)
                        {
                            var localWord = word; // Capture for closure

                            // Use Contains without ToLower() for better SQL translation
                            query = (System.Data.Entity.Infrastructure.DbQuery<Models.Product>)query.Where(p =>
                                p.ProductEnglishName.Contains(localWord) ||
                                p.ProductUrduName.Contains(localWord) ||
                                p.SearchByProductCode.Contains(localWord) ||
                                SqlFunctions.StringConvert((double)p.Id).Contains(localWord)
                            );
                        }
                    }

                    var results = query
                        .OrderBy(p => p.ProductEnglishName)
                        .Take(20)
                        .Select(p => new ProductSearchResult
                        {
                            Id = p.Id,
                            Name = p.ProductEnglishName,
                            UrduName = p.ProductUrduName
                        }).AsNoTracking().ToList();

                    // Cache results
                    if (!_searchCache.ContainsKey(cacheKey))
                    {
                        _searchCache[cacheKey] = results;
                    }

                    return results;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error searching products: {ex.Message}");
                return new List<ProductSearchResult>();
            }
        }

        #endregion

        #region Product Selection

        private void DgvProductSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvProductSearch.Rows.Count)
            {
                SelectProductFromGrid();
            }
        }

        private void DgvProductSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvProductSearch.SelectedRows.Count > 0)
            {
                SelectProductFromGrid();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                dgvProductSearch.Visible = false;
                e.Handled = true;
            }
        }

        private void SelectProductFromGrid()
        {
            if (dgvProductSearch.SelectedRows.Count > 0)
            {
                var selectedRow = dgvProductSearch.SelectedRows[0];
                string productName = selectedRow.Cells["Name"].Value?.ToString();
                selectedProductId = selectedRow.Cells["Id"].Value != null
                    ? Convert.ToInt32(selectedRow.Cells["Id"].Value)
                    : 0;

                if (!string.IsNullOrEmpty(productName))
                {
                    txtProductSearch.Text = productName;
                    dgvProductSearch.Visible = false;
                    txtProductSearch.Focus();
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!productSearchPanel.Bounds.Contains(PointToClient(MousePosition)))
            {
                dgvProductSearch.Visible = false;
            }
        }

        #endregion

        #region Initialization

        private void LoadInitialData()
        {
            InitializeChart();
            InitializeDataGrid();
        }

        private void InitializeChart()
        {
            chartSalesTrend.Series.Clear();
            chartSalesTrend.Titles.Clear();
            chartSalesTrend.Legends.Clear();
            chartSalesTrend.ChartAreas.Clear();

            ChartArea chartArea = new ChartArea
            {
                BackColor = Color.White
            };
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY2.Enabled = AxisEnabled.False;
            chartSalesTrend.ChartAreas.Add(chartArea);

            var title = new Title("Product Sales Trend Analysis")
            {
                Font = new Font("Arial", 14, FontStyle.Bold),
                ForeColor = Color.SteelBlue
            };
            chartSalesTrend.Titles.Add(title);
        }

        private void InitializeDataGrid()
        {
            dgvSalesData.Columns.Clear();
            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Period",
                DataPropertyName = "Period",
                Width = 150
            });
            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Product Name",
                DataPropertyName = "ProductName",
                Width = 250
            });
            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantity Type",
                DataPropertyName = "QuantityType",
                Width = 100
            });
            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantity Sold",
                DataPropertyName = "Quantity",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Total Revenue",
                DataPropertyName = "Revenue",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
            dgvSalesData.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Average Price",
                DataPropertyName = "AveragePrice",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
            });
        }

        #endregion

        #region Report Generation

        private async void BtnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                // Cancel previous operation if any
                _cts?.Cancel();
                _cts = new CancellationTokenSource();

                // Disable button to prevent multiple clicks
                btnGenerate.Enabled = false;
                LoadingManager.ShowLoading();

                string searchTerm = txtProductSearch.Text.Trim();
                string timePeriod = cmbTimePeriod.SelectedItem?.ToString() ?? "Monthly";
                string quantityType = cmbQuantityType.SelectedItem?.ToString() ?? "عدد";
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                // Validation
                if (string.IsNullOrEmpty(searchTerm) || selectedProductId == 0)
                {
                    LoadingManager.HideLoading();
                    MessageBox.Show("Please select a product to generate the report.",
                        "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (startDate > endDate)
                {
                    LoadingManager.HideLoading();
                    MessageBox.Show("Start date cannot be after end date.",
                        "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Run database operation on background thread
                var salesData = await Task.Run(() =>
                    GetProductSalesTrend(selectedProductId, startDate, endDate, timePeriod, quantityType, _cts.Token),
                    _cts.Token);

                if (_cts.Token.IsCancellationRequested)
                    return;

                if (salesData == null || !salesData.Any())
                {
                    LoadingManager.HideLoading();
                    MessageBox.Show("No sales data found for the selected criteria.",
                        "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // UI updates on UI thread
                InitializeChart();
                GenerateTrendChart(salesData, timePeriod, quantityType);
                UpdateDataGrid(salesData);
            }
            catch (OperationCanceledException)
            {
                // User cancelled operation
                System.Diagnostics.Debug.WriteLine("Operation cancelled by user");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LoadingManager.HideLoading();
                btnGenerate.Enabled = true;
            }
        }

        private List<ProductTrendData> GetProductSalesTrend(
            int productId,
            DateTime startDate,
            DateTime endDate,
            string timePeriod,
            string quantityType,
            CancellationToken cancellationToken)
        {
            using (var context = new POSDbContext())
            {
                try
                {
                    // Calculate end date properly
                    DateTime effectiveEndDate = timePeriod == "Yearly"
                        ? new DateTime(endDate.Year, 12, 31, 23, 59, 59)
                        : new DateTime(endDate.Year, endDate.Month,
                            DateTime.DaysInMonth(endDate.Year, endDate.Month), 23, 59, 59);

                    // Check cancellation
                    cancellationToken.ThrowIfCancellationRequested();

                    // Build optimized query - do aggregation in SQL
                    var query = context.OrderDetails
                        .AsNoTracking()
                        .Where(od =>
                            od.ProductId == productId &&
                            od.Order.CreatedDate >= startDate &&
                            od.Order.CreatedDate <= effectiveEndDate &&
                            od.QuantityType == quantityType);

                    List<ProductTrendData> result;

                    if (timePeriod == "Yearly")
                    {
                        // Aggregate in SQL, not in memory
                        var yearlyData = query
                            .GroupBy(od => new
                            {
                                Year = od.Order.CreatedDate.Year,
                                ProductId = od.ProductId,
                                ProductName = od.Product.ProductUrduName
                            })
                            .Select(g => new
                            {
                                Year = g.Key.Year,
                                ProductId = g.Key.ProductId,
                                ProductName = g.Key.ProductName,
                                Quantity = g.Sum(x => x.Quantity),
                                Revenue = g.Sum(x => x.Quantity * x.Price),
                                AveragePrice = g.Average(x => x.Price)
                            })
                            .OrderBy(x => x.Year)
                            .ToList(); // Bring to memory only after aggregation

                        // Check cancellation
                        cancellationToken.ThrowIfCancellationRequested();

                        result = yearlyData.Select(x => new ProductTrendData
                        {
                            Period = x.Year.ToString(),
                            ProductId = x.ProductId.Value,
                            ProductName = x.ProductName,
                            QuantityType = quantityType,
                            Quantity = x.Quantity,
                            Revenue = x.Revenue,
                            AveragePrice = x.AveragePrice
                        }).ToList();
                    }
                    else // Monthly
                    {
                        // Aggregate in SQL, not in memory
                        var monthlyData = query
                            .GroupBy(od => new
                            {
                                Year = od.Order.CreatedDate.Year,
                                Month = od.Order.CreatedDate.Month,
                                ProductId = od.ProductId,
                                ProductName = od.Product.ProductUrduName
                            })
                            .Select(g => new
                            {
                                Year = g.Key.Year,
                                Month = g.Key.Month,
                                ProductId = g.Key.ProductId,
                                ProductName = g.Key.ProductName,
                                Quantity = g.Sum(x => x.Quantity),
                                Revenue = g.Sum(x => x.Quantity * x.Price),
                                AveragePrice = g.Average(x => x.Price)
                            })
                            .OrderBy(x => x.Year).ThenBy(x => x.Month)
                            .ToList(); // Bring to memory only after aggregation

                        // Check cancellation
                        cancellationToken.ThrowIfCancellationRequested();

                        result = monthlyData.Select(x => new ProductTrendData
                        {
                            Period = $"{x.Year}-{x.Month:00}",
                            ProductId = x.ProductId.Value,
                            ProductName = x.ProductName,
                            QuantityType = quantityType,
                            Quantity = x.Quantity,
                            Revenue = x.Revenue,
                            AveragePrice = x.AveragePrice
                        }).ToList();
                    }

                    return result;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
                    throw new Exception($"Error retrieving sales data: {ex.Message}", ex);
                }
            }
        }

        #endregion

        #region Chart Generation

        private void GenerateTrendChart(List<ProductTrendData> salesData, string timePeriod, string quantityType)
        {
            // Warn for large datasets
            if (salesData.Count > 100)
            {
                var result = MessageBox.Show(
                    "Large dataset detected. Chart rendering may take a moment. Continue?",
                    "Performance Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                    return;
            }

            // Suspend layout for better performance
            chartSalesTrend.SuspendLayout();

            try
            {
                chartSalesTrend.Series.Clear();
                chartSalesTrend.Legends.Clear();
                chartSalesTrend.ChartAreas[0].AxisX.LabelStyle.Format = "";

                // Group by product
                var products = salesData.GroupBy(x => new { x.ProductId, x.ProductName });

                // Color palette
                Color[] colors = {
                    Color.SteelBlue, Color.Goldenrod, Color.ForestGreen, Color.IndianRed,
                    Color.DarkOrchid, Color.Teal, Color.Coral, Color.MediumSeaGreen,
                    Color.DodgerBlue, Color.Orange, Color.LimeGreen, Color.Violet
                };

                int colorIndex = 0;
                bool isRevenueAnalysis = cmbAnalysisType.SelectedItem?.ToString() == "Revenue";

                foreach (var productGroup in products.Take(8))
                {
                    var product = productGroup.First();
                    string shortName = GetShortProductName(product.ProductName, 15);

                    Series series = new Series(shortName)
                    {
                        ChartType = SeriesChartType.Column,
                        IsValueShownAsLabel = true,
                        Label = "#VALY",
                        Color = colors[colorIndex % colors.Length],
                        Font = new Font("Arial", 8)
                    };
                    series["PointWidth"] = "0.8";

                    // Add points one by one (no AddRange in DataPointCollection)
                    foreach (var dataPoint in productGroup.OrderBy(x => x.Period))
                    {
                        string periodLabel = timePeriod == "Yearly"
                            ? dataPoint.Period
                            : GetMonthName(dataPoint.Period);

                        double value = isRevenueAnalysis
                            ? dataPoint.Revenue
                            : dataPoint.Quantity;

                        var point = new DataPoint(0, value)
                        {
                            AxisLabel = periodLabel,
                            ToolTip = $"{product.ProductName}\n{periodLabel}: {value:N0} " +
                                     $"{(isRevenueAnalysis ? "Rs." : dataPoint.QuantityType)}\n" +
                                     $"Revenue: Rs. {dataPoint.Revenue:N0}"
                        };

                        series.Points.Add(point); // Add one by one
                    }

                    chartSalesTrend.Series.Add(series);
                    colorIndex++;
                }

                // Add legend
                Legend legend = new Legend
                {
                    Docking = Docking.Top,
                    Alignment = StringAlignment.Center,
                    Font = new Font("Arial", 9),
                    BackColor = Color.Transparent
                };
                chartSalesTrend.Legends.Add(legend);

                // Update title
                string quantityTypeText = quantityType;
                string analysisTypeText = isRevenueAnalysis ? "Revenue" : "Quantity";
                string title = timePeriod == "Yearly"
                    ? $"Yearly Sales Trend - {analysisTypeText} ({quantityTypeText})"
                    : $"Monthly Sales Trend - {analysisTypeText} ({quantityTypeText})";

                chartSalesTrend.Titles[0].Text = title;

                // Disable 3D for better performance (optional)
                chartSalesTrend.ChartAreas[0].Area3DStyle.Enable3D = false;
            }
            finally
            {
                chartSalesTrend.ResumeLayout(true);
            }
        }

        #endregion

        #region DataGrid Update

        private void UpdateDataGrid(List<ProductTrendData> salesData)
        {
            // Suspend layout for better performance
            dgvSalesData.SuspendLayout();

            try
            {
                var gridData = salesData.Select(x => new
                {
                    Period = cmbTimePeriod.SelectedItem?.ToString() == "Yearly"
                        ? x.Period + " Year"
                        : GetMonthName(x.Period),
                    x.ProductName,
                    x.QuantityType,
                    Quantity = x.Quantity,
                    Revenue = x.Revenue.ToString("N2"),
                    AveragePrice = x.AveragePrice.ToString("N2")
                }).ToList();

                // Use BindingList for better performance
                dgvSalesData.DataSource = new BindingList<object>(gridData.Cast<object>().ToList());

                // Add summary row
                AddSummaryRow(salesData);
            }
            finally
            {
                dgvSalesData.ResumeLayout(true);
            }
        }

        private void AddSummaryRow(List<ProductTrendData> salesData)
        {
            string quantityType = cmbQuantityType.SelectedItem?.ToString() ?? "All";

            var summary = new
            {
                TotalQuantity = salesData.Sum(x => x.Quantity),
                TotalRevenue = salesData.Sum(x => x.Revenue),
                AvgPrice = salesData.Any() ? salesData.Average(x => x.AveragePrice) : 0,
                ProductCount = salesData.Select(x => x.ProductId).Distinct().Count()
            };

            var summaryLabel = new Label
            {
                Text = $"Summary: {summary.TotalQuantity:N0} units sold ({quantityType}) | " +
                       $"Total Revenue: Rs. {summary.TotalRevenue:N2} | " +
                       $"Avg Price: Rs. {summary.AvgPrice:N2} | " +
                       $"{summary.ProductCount} Product(s)",
                Location = new Point(10, dgvSalesData.Bottom + 5),
                Size = new Size(1140, 25),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                BackColor = Color.LightYellow,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Remove existing summary label if any
            var existingLabel = splitContainer.Panel2.Controls.OfType<Label>()
                .FirstOrDefault(l => l.BackColor == Color.LightYellow);
            if (existingLabel != null)
            {
                splitContainer.Panel2.Controls.Remove(existingLabel);
                existingLabel.Dispose();
            }

            splitContainer.Panel2.Controls.Add(summaryLabel);
            summaryLabel.BringToFront();
        }

        #endregion

        #region Helper Methods

        private string GetMonthName(string period)
        {
            try
            {
                var parts = period.Split('-');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int year) &&
                    int.TryParse(parts[1], out int month))
                {
                    return new DateTime(year, month, 1).ToString("MMM yyyy");
                }
            }
            catch
            {
                // If parsing fails, return original period
            }
            return period;
        }

        private string GetShortProductName(string fullName, int maxLength = 15)
        {
            if (string.IsNullOrEmpty(fullName))
                return "Unknown";

            if (fullName.Length <= maxLength)
                return fullName;

            return fullName.Substring(0, maxLength - 3) + "...";
        }

        #endregion

        #region Cleanup

        // Cleanup method - will be called when form is disposed
        private void CleanupResources()
        {
            // Dispose timers
            _searchDebounceTimer?.Dispose();
            _cts?.Dispose();

            // Clear cache on form close
            _searchCache.Clear();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            CleanupResources();
        }

        #endregion

        #region Helper Classes

        public class ProductSearchResult
        {
            public int Id { get; set; }
            public string UrduName { get; set; }
            public string Name { get; set; }
        }

        #endregion
    }

    #region Data Models

    public class ProductTrendData
    {
        public string Period { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string QuantityType { get; set; }
        public int Quantity { get; set; }
        public float Revenue { get; set; }
        public float AveragePrice { get; set; }
    }

    #endregion

    #region Extension Methods for DoubleBuffering

    public static class DataGridViewExtensions
    {
        public static void EnableDoubleBuffering(this DataGridView dgv)
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.SetProperty,
                    null,
                    dgv,
                    new object[] { true });
            }
            catch
            {
                // If reflection fails, ignore (not critical)
            }
        }
    }

    #endregion
}



