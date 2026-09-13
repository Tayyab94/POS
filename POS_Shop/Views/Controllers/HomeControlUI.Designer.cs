using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace POS_Shop.Views.Controllers
{
    partial class HomeControlUI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            // ── Controls declaration ─────────────────────────────────────
            this.PanelMain = new Panel();
            this.PanelHeader = new Panel();
            this.LblTitle = new Label();
            this.LblCurrentDate = new Label();
            this.BtnRefresh = new Button();

            this.PanelAlert = new Panel();
            this.LblAlertText = new Label();

            // Stat cards
            this.PanelStats = new Panel();

            this.CardTodayOrders = new Panel();
            this.LblTodayOrdersHdr = new Label();
            this.LblTodayOrders = new Label();
            this.LblOrderTrend = new Label();

            this.CardRevenue = new Panel();
            this.LblRevenueHdr = new Label();
            this.LblRevenue = new Label();
            this.LblRevenueSub = new Label();

            this.CardTemp = new Panel();
            this.LblTempHdr = new Label();
            this.LblTempOrders = new Label();
            this.LblTempSub = new Label();

            this.CardProducts = new Panel();
            this.LblProductsHdr = new Label();
            this.LblProducts = new Label();
            this.LblProductsSub = new Label();

            this.CardCustomers = new Panel();
            this.LblCustomersHdr = new Label();
            this.LblCustomers = new Label();
            this.LblCustomersSub = new Label();

            this.CardSuppliers = new Panel();
            this.LblSuppliersHdr = new Label();
            this.LblSuppliers = new Label();
            this.LblSuppliersSub = new Label();

            // Orders table
            this.PanelTable = new Panel();
            this.PanelTableHeader = new Panel();
            this.LblOrdersTitle = new Label();
            this.LblOrdersBadge = new Label();
            this.OrdersGrid = new DataGridView();

            // ── NEW: Analytics Panel ────────────────────────────────────
            this.PanelAnalytics = new Panel();
            this.PanelTopProducts = new Panel();
            this.LblTopProductsTitle = new Label();
            this.ChartTopProducts = new Chart();
            this.PanelTopCustomers = new Panel();
            this.LblTopCustomersTitle = new Label();
            this.GridTopCustomers = new DataGridView();

            // Quick actions
            this.PanelActions = new Panel();
            this.LblActionsTitle = new Label();
            this.PanelActionsRow = new Panel();
            this.BtnSalesAnalysis = new Button();
            this.BtnWeeklySalesChart = new Button();
            this.BtnSalesCharts = new Button();
            this.BtnProductTrends = new Button();
            this.BtnPurchaseReports = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.OrdersGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTopProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTopCustomers)).BeginInit();
            this.PanelMain.SuspendLayout();
            this.PanelTable.SuspendLayout();
            this.PanelTableHeader.SuspendLayout();
            this.PanelAnalytics.SuspendLayout();
            this.PanelTopProducts.SuspendLayout();
            this.PanelTopCustomers.SuspendLayout();
            this.PanelActions.SuspendLayout();
            this.PanelActionsRow.SuspendLayout();
            this.SuspendLayout();

            // ── Color palette ────────────────────────────────────────────
            Color clrBg = Color.FromArgb(248, 250, 252);
            Color clrSurface = Color.White;
            Color clrBorder = Color.FromArgb(226, 232, 240);
            Color clrText = Color.FromArgb(15, 23, 42);
            Color clrMuted = Color.FromArgb(100, 116, 139);
            Color clrBlue = Color.FromArgb(59, 130, 246);
            Color clrGreen = Color.FromArgb(16, 185, 129);
            Color clrAmber = Color.FromArgb(245, 158, 11);
            Color clrPurple = Color.FromArgb(139, 92, 246);
            Color clrRed = Color.FromArgb(239, 68, 68);
            Color clrAlertBg = Color.FromArgb(255, 251, 235);
            Color clrAlertText = Color.FromArgb(146, 64, 14);
            Color clrGridHdr = Color.FromArgb(241, 245, 249);

            // ── PanelMain ────────────────────────────────────────────────
            this.PanelMain.BackColor = clrBg;
            this.PanelMain.Dock = DockStyle.Fill;
            this.PanelMain.AutoScroll = true;
            this.PanelMain.Padding = new Padding(20);
            this.PanelMain.BorderStyle = BorderStyle.None;

            // ── Header ───────────────────────────────────────────────────
            this.PanelHeader.BackColor = clrBg;
            this.PanelHeader.Height = 48;
            this.PanelHeader.Dock = DockStyle.Top;

            this.LblTitle.Text = "POS Dashboard";
            this.LblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.LblTitle.ForeColor = clrText;
            this.LblTitle.AutoSize = true;
            this.LblTitle.Location = new Point(0, 8);

            this.LblCurrentDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            this.LblCurrentDate.Font = new Font("Segoe UI", 9F);
            this.LblCurrentDate.ForeColor = clrMuted;
            this.LblCurrentDate.AutoSize = true;
            this.LblCurrentDate.Location = new Point(290, 16);

            this.BtnRefresh.Text = "↻  Refresh";
            this.BtnRefresh.Font = new Font("Segoe UI", 9F);
            this.BtnRefresh.ForeColor = clrBlue;
            this.BtnRefresh.BackColor = clrSurface;
            this.BtnRefresh.FlatStyle = FlatStyle.Flat;
            this.BtnRefresh.FlatAppearance.BorderColor = clrBorder;
            this.BtnRefresh.FlatAppearance.BorderSize = 1;
            this.BtnRefresh.Size = new Size(110, 34);
            this.BtnRefresh.Location = new Point(930, 7);
            this.BtnRefresh.Cursor = Cursors.Hand;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);

            this.PanelHeader.Controls.Add(this.LblTitle);
            this.PanelHeader.Controls.Add(this.LblCurrentDate);
            this.PanelHeader.Controls.Add(this.BtnRefresh);

            // ── Alert panel ──────────────────────────────────────────────
            this.PanelAlert.BackColor = clrAlertBg;
            this.PanelAlert.Height = 40;
            this.PanelAlert.Dock = DockStyle.Top;
            this.PanelAlert.Visible = false;
            this.PanelAlert.Padding = new Padding(2);

            this.PanelAlert.Paint += (s, e) => {
                using (var pen = new Pen(Color.FromArgb(253, 230, 138), 2))
                {
                    var rect = new Rectangle(0, 0, this.PanelAlert.Width - 1, this.PanelAlert.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            this.LblAlertText.Text = "";
            this.LblAlertText.Font = new Font("Segoe UI", 9.5F);
            this.LblAlertText.ForeColor = clrAlertText;
            this.LblAlertText.Dock = DockStyle.Fill;
            this.LblAlertText.TextAlign = ContentAlignment.MiddleLeft;
            this.LblAlertText.Padding = new Padding(10, 0, 0, 0);
            this.PanelAlert.Controls.Add(this.LblAlertText);

            // ── Stat cards ───────────────────────────────────────────────
            this.PanelStats.BackColor = clrBg;
            this.PanelStats.Height = 110;
            this.PanelStats.Dock = DockStyle.Top;
            this.PanelStats.Padding = new Padding(0, 5, 0, 5);

            void AddCardBorder(Panel card, Color accentColor)
            {
                card.Paint += (s, e) => {
                    using (var pen = new Pen(accentColor, 3))
                    {
                        e.Graphics.DrawLine(pen, 0, 0, 0, card.Height);
                    }
                    using (var pen = new Pen(clrBorder, 1))
                    {
                        var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                };
            }

            // Card 1: Today's Orders (Blue)
            this.CardTodayOrders.BackColor = clrSurface;
            this.CardTodayOrders.Height = 100;
            this.CardTodayOrders.Width = 166;
            this.CardTodayOrders.Location = new Point(0, 5);
            AddCardBorder(this.CardTodayOrders, clrBlue);

            this.LblTodayOrdersHdr.Text = "Today's orders";
            this.LblTodayOrdersHdr.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblTodayOrdersHdr.ForeColor = clrMuted;
            this.LblTodayOrdersHdr.AutoSize = false;
            this.LblTodayOrdersHdr.Size = new Size(150, 18);
            this.LblTodayOrdersHdr.Location = new Point(14, 14);

            this.LblTodayOrders.Text = "0";
            this.LblTodayOrders.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.LblTodayOrders.ForeColor = clrBlue;
            this.LblTodayOrders.AutoSize = false;
            this.LblTodayOrders.Size = new Size(150, 40);
            this.LblTodayOrders.Location = new Point(14, 34);

            this.LblOrderTrend.Text = "Loading...";
            this.LblOrderTrend.Font = new Font("Segoe UI", 7.5F);
            this.LblOrderTrend.ForeColor = clrMuted;
            this.LblOrderTrend.AutoSize = false;
            this.LblOrderTrend.Size = new Size(150, 16);
            this.LblOrderTrend.Location = new Point(14, 78);

            this.CardTodayOrders.Controls.Add(this.LblTodayOrdersHdr);
            this.CardTodayOrders.Controls.Add(this.LblTodayOrders);
            this.CardTodayOrders.Controls.Add(this.LblOrderTrend);
            this.PanelStats.Controls.Add(this.CardTodayOrders);

            // Card 2: Revenue (Green)
            this.CardRevenue.BackColor = clrSurface;
            this.CardRevenue.Height = 100;
            this.CardRevenue.Width = 166;
            this.CardRevenue.Location = new Point(178, 5);
            AddCardBorder(this.CardRevenue, clrGreen);

            this.LblRevenueHdr.Text = "Today's revenue";
            this.LblRevenueHdr.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblRevenueHdr.ForeColor = clrMuted;
            this.LblRevenueHdr.AutoSize = false;
            this.LblRevenueHdr.Size = new Size(150, 18);
            this.LblRevenueHdr.Location = new Point(14, 14);

            this.LblRevenue.Text = "PKR 0";
            this.LblRevenue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.LblRevenue.ForeColor = clrGreen;
            this.LblRevenue.AutoSize = false;
            this.LblRevenue.Size = new Size(150, 40);
            this.LblRevenue.Location = new Point(14, 34);

            this.LblRevenueSub.Text = "Cash";
            this.LblRevenueSub.Font = new Font("Segoe UI", 7.5F);
            this.LblRevenueSub.ForeColor = clrMuted;
            this.LblRevenueSub.AutoSize = false;
            this.LblRevenueSub.Size = new Size(150, 16);
            this.LblRevenueSub.Location = new Point(14, 78);



            this.CardRevenue.Controls.Add(this.LblRevenueHdr);
            this.CardRevenue.Controls.Add(this.LblRevenue);
            this.CardRevenue.Controls.Add(this.LblRevenueSub);
            this.PanelStats.Controls.Add(this.CardRevenue);


            // Card 7: Stock Value (Teal)
            this.CardStockValue = new Panel();
            this.CardStockValue.BackColor = clrSurface;
            this.CardStockValue.Height = 100;
            this.CardStockValue.Width = 166;
            this.CardStockValue.Location = new Point(1068, 5); // Adjust position
            AddCardBorder(this.CardStockValue, Color.FromArgb(20, 184, 166)); // Teal color

            // Card 3: Temp Orders (Amber)
            this.CardTemp.BackColor = clrSurface;
            this.CardTemp.Height = 100;
            this.CardTemp.Width = 166;
            this.CardTemp.Location = new Point(356, 5);
            AddCardBorder(this.CardTemp, clrAmber);

            this.LblTempHdr.Text = "Temp orders";
            this.LblTempHdr.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblTempHdr.ForeColor = clrMuted;
            this.LblTempHdr.AutoSize = false;
            this.LblTempHdr.Size = new Size(150, 18);
            this.LblTempHdr.Location = new Point(14, 14);

            this.LblTempOrders.Text = "0";
            this.LblTempOrders.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.LblTempOrders.ForeColor = clrAmber;
            this.LblTempOrders.AutoSize = false;
            this.LblTempOrders.Size = new Size(150, 40);
            this.LblTempOrders.Location = new Point(14, 34);

            this.LblTempSub.Text = "Pending checkout";
            this.LblTempSub.Font = new Font("Segoe UI", 7.5F);
            this.LblTempSub.ForeColor = clrMuted;
            this.LblTempSub.AutoSize = false;
            this.LblTempSub.Size = new Size(150, 16);
            this.LblTempSub.Location = new Point(14, 78);

            this.CardTemp.Controls.Add(this.LblTempHdr);
            this.CardTemp.Controls.Add(this.LblTempOrders);
            this.CardTemp.Controls.Add(this.LblTempSub);
            this.PanelStats.Controls.Add(this.CardTemp);

            // Card 4: Products (Blue)
            this.CardProducts.BackColor = clrSurface;
            this.CardProducts.Height = 100;
            this.CardProducts.Width = 166;
            this.CardProducts.Location = new Point(534, 5);
            AddCardBorder(this.CardProducts, clrBlue);

            this.LblProductsHdr.Text = "Total products";
            this.LblProductsHdr.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblProductsHdr.ForeColor = clrMuted;
            this.LblProductsHdr.AutoSize = false;
            this.LblProductsHdr.Size = new Size(150, 18);
            this.LblProductsHdr.Location = new Point(14, 14);

            this.LblProducts.Text = "0";
            this.LblProducts.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.LblProducts.ForeColor = clrBlue;
            this.LblProducts.AutoSize = false;
            this.LblProducts.Size = new Size(150, 40);
            this.LblProducts.Location = new Point(14, 34);

            this.LblProductsSub.Text = "In inventory";
            this.LblProductsSub.Font = new Font("Segoe UI", 7.5F);
            this.LblProductsSub.ForeColor = clrMuted;
            this.LblProductsSub.AutoSize = false;
            this.LblProductsSub.Size = new Size(150, 16);
            this.LblProductsSub.Location = new Point(14, 78);

            this.CardProducts.Controls.Add(this.LblProductsHdr);
            this.CardProducts.Controls.Add(this.LblProducts);
            this.CardProducts.Controls.Add(this.LblProductsSub);
            this.PanelStats.Controls.Add(this.CardProducts);

            // Card 5: Customers (Purple)
            this.CardCustomers.BackColor = clrSurface;
            this.CardCustomers.Height = 100;
            this.CardCustomers.Width = 166;
            this.CardCustomers.Location = new Point(712, 5);
            AddCardBorder(this.CardCustomers, clrPurple);

            this.LblCustomersHdr.Text = "Customers";
            this.LblCustomersHdr.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblCustomersHdr.ForeColor = clrMuted;
            this.LblCustomersHdr.AutoSize = false;
            this.LblCustomersHdr.Size = new Size(150, 18);
            this.LblCustomersHdr.Location = new Point(14, 14);

            this.LblCustomers.Text = "0";
            this.LblCustomers.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.LblCustomers.ForeColor = clrPurple;
            this.LblCustomers.AutoSize = false;
            this.LblCustomers.Size = new Size(150, 40);
            this.LblCustomers.Location = new Point(14, 34);

            this.LblCustomersSub.Text = "Registered";
            this.LblCustomersSub.Font = new Font("Segoe UI", 7.5F);
            this.LblCustomersSub.ForeColor = clrMuted;
            this.LblCustomersSub.AutoSize = false;
            this.LblCustomersSub.Size = new Size(150, 16);
            this.LblCustomersSub.Location = new Point(14, 78);

            this.CardCustomers.Controls.Add(this.LblCustomersHdr);
            this.CardCustomers.Controls.Add(this.LblCustomers);
            this.CardCustomers.Controls.Add(this.LblCustomersSub);
            this.PanelStats.Controls.Add(this.CardCustomers);

            // Card 6: Suppliers (Red)
            this.CardSuppliers.BackColor = clrSurface;
            this.CardSuppliers.Height = 100;
            this.CardSuppliers.Width = 166;
            this.CardSuppliers.Location = new Point(890, 5);
            AddCardBorder(this.CardSuppliers, clrRed);

            this.LblSuppliersHdr.Text = "Suppliers";
            this.LblSuppliersHdr.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblSuppliersHdr.ForeColor = clrMuted;
            this.LblSuppliersHdr.AutoSize = false;
            this.LblSuppliersHdr.Size = new Size(150, 18);
            this.LblSuppliersHdr.Location = new Point(14, 14);

            this.LblSuppliers.Text = "0";
            this.LblSuppliers.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            this.LblSuppliers.ForeColor = clrRed;
            this.LblSuppliers.AutoSize = false;
            this.LblSuppliers.Size = new Size(150, 40);
            this.LblSuppliers.Location = new Point(14, 34);

            this.LblSuppliersSub.Text = "Active";
            this.LblSuppliersSub.Font = new Font("Segoe UI", 7.5F);
            this.LblSuppliersSub.ForeColor = clrMuted;
            this.LblSuppliersSub.AutoSize = false;
            this.LblSuppliersSub.Size = new Size(150, 16);
            this.LblSuppliersSub.Location = new Point(14, 78);

            this.CardSuppliers.Controls.Add(this.LblSuppliersHdr);
            this.CardSuppliers.Controls.Add(this.LblSuppliers);
            this.CardSuppliers.Controls.Add(this.LblSuppliersSub);
            this.PanelStats.Controls.Add(this.CardSuppliers);

            // ── Orders table ─────────────────────────────────────────────
            this.PanelTable.BackColor = clrSurface;
            this.PanelTable.Dock = DockStyle.Top;
            this.PanelTable.Height = 280;
            this.PanelTable.Padding = new Padding(1);

            this.PanelTable.Paint += (s, e) => {
                using (var pen = new Pen(clrBorder, 1))
                {
                    var rect = new Rectangle(0, 0, this.PanelTable.Width - 1, this.PanelTable.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            this.PanelTableHeader.BackColor = clrSurface;
            this.PanelTableHeader.Dock = DockStyle.Top;
            this.PanelTableHeader.Height = 44;
            this.PanelTableHeader.Padding = new Padding(0, 0, 0, 1);

            this.PanelTableHeader.Paint += (s, e) => {
                using (var pen = new Pen(clrBorder, 1))
                {
                    e.Graphics.DrawLine(pen, 0, 43, this.PanelTableHeader.Width, 43);
                }
            };

            this.LblOrdersTitle.Text = "Recent orders";
            this.LblOrdersTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.LblOrdersTitle.ForeColor = clrText;
            this.LblOrdersTitle.AutoSize = true;
            this.LblOrdersTitle.Location = new Point(16, 12);

            this.LblOrdersBadge.Text = " Last 10 ";
            this.LblOrdersBadge.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblOrdersBadge.ForeColor = clrBlue;
            this.LblOrdersBadge.BackColor = Color.FromArgb(219, 234, 254);
            this.LblOrdersBadge.AutoSize = true;
            this.LblOrdersBadge.Location = new Point(160, 14);
            this.LblOrdersBadge.Padding = new Padding(4, 2, 4, 2);

            this.PanelTableHeader.Controls.Add(this.LblOrdersTitle);
            this.PanelTableHeader.Controls.Add(this.LblOrdersBadge);



            this.LblStockValueHdr = new Label();
            this.LblStockValueHdr.Text = "Stock Value";
            this.LblStockValueHdr.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            this.LblStockValueHdr.ForeColor = clrMuted;
            this.LblStockValueHdr.AutoSize = false;
            this.LblStockValueHdr.Size = new Size(150, 18);
            this.LblStockValueHdr.Location = new Point(14, 14);

            this.LblStockValue = new Label();
            this.LblStockValue.Text = "Rs 0";
            this.LblStockValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.LblStockValue.ForeColor = Color.FromArgb(20, 184, 166);
            this.LblStockValue.AutoSize = false;
            this.LblStockValue.Size = new Size(150, 40);
            this.LblStockValue.Location = new Point(14, 34);

            this.LblStockValueSub = new Label();
            this.LblStockValueSub.Text = "Total inventory value";
            this.LblStockValueSub.Font = new Font("Segoe UI", 7.5F);
            this.LblStockValueSub.ForeColor = clrMuted;
            this.LblStockValueSub.AutoSize = false;
            this.LblStockValueSub.Size = new Size(150, 16);
            this.LblStockValueSub.Location = new Point(14, 78);

            this.CardStockValue.Controls.Add(this.LblStockValueHdr);
            this.CardStockValue.Controls.Add(this.LblStockValue);
            this.CardStockValue.Controls.Add(this.LblStockValueSub);
            this.PanelStats.Controls.Add(this.CardStockValue);

            // DataGridView
            this.OrdersGrid.BackgroundColor = clrSurface;
            this.OrdersGrid.BorderStyle = BorderStyle.None;
            this.OrdersGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.OrdersGrid.RowHeadersVisible = false;
            this.OrdersGrid.AllowUserToAddRows = false;
            this.OrdersGrid.AllowUserToDeleteRows = false;
            this.OrdersGrid.ReadOnly = true;
            this.OrdersGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.OrdersGrid.MultiSelect = false;
            this.OrdersGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.OrdersGrid.ColumnHeadersHeight = 36;
            this.OrdersGrid.RowTemplate.Height = 36;
            this.OrdersGrid.Font = new Font("Segoe UI", 9F);
            this.OrdersGrid.GridColor = clrBorder;
            this.OrdersGrid.Dock = DockStyle.Fill;
            this.OrdersGrid.ScrollBars = ScrollBars.Vertical;

            DataGridViewCellStyle hdrStyle = this.OrdersGrid.ColumnHeadersDefaultCellStyle;
            hdrStyle.BackColor = clrGridHdr;
            hdrStyle.ForeColor = clrMuted;
            hdrStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            hdrStyle.SelectionBackColor = clrGridHdr;
            hdrStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            hdrStyle.Padding = new Padding(6, 0, 0, 0);

            DataGridViewCellStyle rowStyle = this.OrdersGrid.DefaultCellStyle;
            rowStyle.BackColor = clrSurface;
            rowStyle.ForeColor = clrText;
            rowStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            rowStyle.SelectionForeColor = clrText;
            rowStyle.Padding = new Padding(6, 0, 0, 0);

            this.OrdersGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            typeof(DataGridView).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(this.OrdersGrid, true);

            this.OrdersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Invoice",
                HeaderText = "INVOICE",
                FillWeight = 18
            });
            this.OrdersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "DATE",
                FillWeight = 16
            });
            this.OrdersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Total",
                HeaderText = "TOTAL (PKR)",
                FillWeight = 15
            });
            this.OrdersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Received",
                HeaderText = "RECEIVED",
                FillWeight = 15
            });
            this.OrdersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Payment",
                HeaderText = "PAYMENT",
                FillWeight = 14
            });
            this.OrdersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Customer",
                HeaderText = "CUSTOMER",
                FillWeight = 22
            });

            this.PanelTable.Controls.Add(this.OrdersGrid);
            this.PanelTable.Controls.Add(this.PanelTableHeader);

            // ── NEW: Analytics Panel ────────────────────────────────────
            this.PanelAnalytics.BackColor = clrBg;
            this.PanelAnalytics.Dock = DockStyle.Top;
            this.PanelAnalytics.Height = 260;
            this.PanelAnalytics.Padding = new Padding(0, 10, 0, 10);

            // ── Top Products Panel (Left) ──────────────────────────────
            this.PanelTopProducts.BackColor = clrSurface;
            this.PanelTopProducts.Dock = DockStyle.Left;
            this.PanelTopProducts.Width = 520;
            this.PanelTopProducts.Padding = new Padding(10);
            this.PanelTopProducts.Margin = new Padding(0, 0, 10, 0);

            this.PanelTopProducts.Paint += (s, e) => {
                using (var pen = new Pen(clrBorder, 1))
                {
                    var rect = new Rectangle(0, 0, this.PanelTopProducts.Width - 1, this.PanelTopProducts.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            this.LblTopProductsTitle.Text = "🏆 Top Selling Products (This Month)";
            this.LblTopProductsTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.LblTopProductsTitle.ForeColor = clrText;
            this.LblTopProductsTitle.Dock = DockStyle.Top;
            this.LblTopProductsTitle.Height = 30;
            this.LblTopProductsTitle.TextAlign = ContentAlignment.MiddleLeft;

            // Chart for top products
            this.ChartTopProducts.Dock = DockStyle.Fill;
            this.ChartTopProducts.BackColor = clrSurface;
            this.ChartTopProducts.BorderlineColor = clrBorder;
            this.ChartTopProducts.BorderlineWidth = 0;

            // Chart area
            ChartArea chartArea = new ChartArea();
            chartArea.BackColor = clrSurface;
            chartArea.BorderColor = clrBorder;
            chartArea.BorderWidth = 0;
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(230, 230, 230);
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.Title = "Quantity Sold";
            chartArea.AxisY.TitleFont = new Font("Segoe UI", 8F);
            chartArea.AxisY.TitleForeColor = clrMuted;
            this.ChartTopProducts.ChartAreas.Add(chartArea);

            // Legend
            Legend legend = new Legend();
            legend.Enabled = false;
            this.ChartTopProducts.Legends.Add(legend);

            this.PanelTopProducts.Controls.Add(this.ChartTopProducts);
            this.PanelTopProducts.Controls.Add(this.LblTopProductsTitle);

            // ── Top Customers Panel (Right) ─────────────────────────────
            this.PanelTopCustomers.BackColor = clrSurface;
            this.PanelTopCustomers.Dock = DockStyle.Fill;
            this.PanelTopCustomers.Padding = new Padding(10);
            this.PanelTopCustomers.Margin = new Padding(10, 0, 0, 0);

            this.PanelTopCustomers.Paint += (s, e) => {
                using (var pen = new Pen(clrBorder, 1))
                {
                    var rect = new Rectangle(0, 0, this.PanelTopCustomers.Width - 1, this.PanelTopCustomers.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };

            this.LblTopCustomersTitle.Text = "👥 Top Buying Customers";
            this.LblTopCustomersTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.LblTopCustomersTitle.ForeColor = clrText;
            this.LblTopCustomersTitle.Dock = DockStyle.Top;
            this.LblTopCustomersTitle.Height = 30;
            this.LblTopCustomersTitle.TextAlign = ContentAlignment.MiddleLeft;

            // Grid for top customers
            this.GridTopCustomers.BackgroundColor = clrSurface;
            this.GridTopCustomers.BorderStyle = BorderStyle.None;
            this.GridTopCustomers.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.GridTopCustomers.RowHeadersVisible = false;
            this.GridTopCustomers.AllowUserToAddRows = false;
            this.GridTopCustomers.AllowUserToDeleteRows = false;
            this.GridTopCustomers.ReadOnly = true;
            this.GridTopCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.GridTopCustomers.MultiSelect = false;
            this.GridTopCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.GridTopCustomers.ColumnHeadersHeight = 30;
            this.GridTopCustomers.RowTemplate.Height = 30;
            this.GridTopCustomers.Font = new Font("Segoe UI", 9F);
            this.GridTopCustomers.GridColor = clrBorder;
            this.GridTopCustomers.Dock = DockStyle.Fill;
            this.GridTopCustomers.ScrollBars = ScrollBars.Vertical;

            DataGridViewCellStyle customerHdrStyle = this.GridTopCustomers.ColumnHeadersDefaultCellStyle;
            customerHdrStyle.BackColor = clrGridHdr;
            customerHdrStyle.ForeColor = clrMuted;
            customerHdrStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            customerHdrStyle.SelectionBackColor = clrGridHdr;
            customerHdrStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            customerHdrStyle.Padding = new Padding(6, 0, 0, 0);

            DataGridViewCellStyle customerRowStyle = this.GridTopCustomers.DefaultCellStyle;
            customerRowStyle.BackColor = clrSurface;
            customerRowStyle.ForeColor = clrText;
            customerRowStyle.SelectionBackColor = Color.FromArgb(239, 246, 255);
            customerRowStyle.SelectionForeColor = clrText;
            customerRowStyle.Padding = new Padding(6, 0, 0, 0);

            this.GridTopCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            this.GridTopCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Rank",
                HeaderText = "#",
                FillWeight = 8
            });
            this.GridTopCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Customer",
                HeaderText = "CUSTOMER NAME",
                FillWeight = 35
            });
            this.GridTopCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Orders",
                HeaderText = "ORDERS",
                FillWeight = 20
            });
            this.GridTopCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalSpent",
                HeaderText = "TOTAL SPENT (PKR)",
                FillWeight = 37
            });

            this.PanelTopCustomers.Controls.Add(this.GridTopCustomers);
            this.PanelTopCustomers.Controls.Add(this.LblTopCustomersTitle);

            // Add panels to analytics
            this.PanelAnalytics.Controls.Add(this.PanelTopCustomers);
            this.PanelAnalytics.Controls.Add(this.PanelTopProducts);

            // ── Quick-action buttons ──────────────────────────────────────
            this.PanelActions.BackColor = clrBg;
            this.PanelActions.Dock = DockStyle.Top;
            this.PanelActions.Height = 120;
            this.PanelActions.Padding = new Padding(0, 5, 0, 5);

            //this.LblActionsTitle.Text = "Quick actions";
            //this.LblActionsTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            //this.LblActionsTitle.ForeColor = clrText;
            //this.LblActionsTitle.AutoSize = true;
            //this.LblActionsTitle.Location = new Point(0, 5);
            //this.PanelActions.Controls.Add(this.LblActionsTitle);

            this.PanelActionsRow.BackColor = clrBg;
            this.PanelActionsRow.Dock = DockStyle.Top;
            this.PanelActionsRow.Height = 70;
            this.PanelActionsRow.Location = new Point(0, 30);

            void CreateActionButton(Button btn, string text, string subtitle, Color accent, int index)
            {
                int btnW = 200, btnH = 65, btnGap = 12;
                btn.Size = new Size(btnW, btnH);
                btn.Location = new Point(index * (btnW + btnGap), 0);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = clrBorder;
                btn.FlatAppearance.BorderSize = 1;
                btn.BackColor = clrSurface;
                btn.ForeColor = clrText;
                btn.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                btn.Text = text;
                btn.Cursor = Cursors.Hand;
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Padding = new Padding(0, 0, 0, 12);

                btn.Paint += (s, e) => {
                    using (var pen = new Pen(accent, 3))
                    {
                        e.Graphics.DrawLine(pen, 0, 0, btn.Width, 0);
                    }
                    using (var pen = new Pen(clrBorder, 1))
                    {
                        var rect = new Rectangle(0, 0, btn.Width - 1, btn.Height - 1);
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                    using (var subFont = new Font("Segoe UI", 8F))
                    using (var subBrush = new SolidBrush(clrMuted))
                    {
                        var size = e.Graphics.MeasureString(subtitle, subFont);
                        float x = (btn.Width - size.Width) / 2;
                        e.Graphics.DrawString(subtitle, subFont, subBrush, x, btn.Height - 20);
                    }
                };

                this.PanelActionsRow.Controls.Add(btn);
            }

            CreateActionButton(this.BtnSalesAnalysis, "Sales Analysis", "View report", clrPurple, 0);
            CreateActionButton(this.BtnWeeklySalesChart, "Weekly Sales Chart", "7-day trend", clrBlue, 1);
            CreateActionButton(this.BtnSalesCharts, "Charts", "Revenue chart", clrGreen, 2);
            CreateActionButton(this.BtnProductTrends, "Product Trends", "By product", clrAmber, 3);
            CreateActionButton(this.BtnPurchaseReports, "Purchase Reports", "Supplier orders", clrRed, 4);

            this.BtnSalesAnalysis.Click += new System.EventHandler(this.BtnSalesAnalysis_Click);
            this.BtnWeeklySalesChart.Click += new System.EventHandler(this.BtnWeeklySalesChart_Click);
            this.BtnSalesCharts.Click += new System.EventHandler(this.BtnSalesCharts_Click);
            this.BtnProductTrends.Click += new System.EventHandler(this.BtnProductTrends_Click);
            this.BtnPurchaseReports.Click += new System.EventHandler(this.BtnPurchaseReports_Click);

            this.PanelActions.Controls.Add(this.PanelActionsRow);

            // ── Assemble main panel ──────────────────────────────────────
            this.PanelMain.Controls.Add(this.PanelActions);
            this.PanelMain.Controls.Add(this.PanelAnalytics);
            this.PanelMain.Controls.Add(this.PanelTable);
            this.PanelMain.Controls.Add(this.PanelStats);
            this.PanelMain.Controls.Add(this.PanelAlert);
            this.PanelMain.Controls.Add(this.PanelHeader);

            // ── UserControl ──────────────────────────────────────────────
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = clrBg;
            this.Controls.Add(this.PanelMain);
            this.Name = "HomeControlUI";
            this.Size = new Size(1110, 750);

            // Clean up
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChartTopProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridTopCustomers)).EndInit();
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.PanelTable.ResumeLayout(false);
            this.PanelTable.PerformLayout();
            this.PanelTableHeader.ResumeLayout(false);
            this.PanelTableHeader.PerformLayout();
            this.PanelAnalytics.ResumeLayout(false);
            this.PanelTopProducts.ResumeLayout(false);
            this.PanelTopCustomers.ResumeLayout(false);
            this.PanelActions.ResumeLayout(false);
            this.PanelActions.PerformLayout();
            this.PanelActionsRow.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        // ── Control declarations ─────────────────────────────────────────
        private Panel PanelMain, PanelHeader, PanelAlert, PanelStats;
        private Panel PanelTable, PanelTableHeader, PanelActions, PanelActionsRow;
        private Panel PanelAnalytics, PanelTopProducts, PanelTopCustomers;
        private Label LblTitle, LblCurrentDate, LblAlertText;
        private Label LblOrdersTitle, LblOrdersBadge, LblActionsTitle;
        private Label LblTopProductsTitle, LblTopCustomersTitle;
        private Button BtnRefresh;
        private DataGridView OrdersGrid;
        private DataGridView GridTopCustomers;
        private Chart ChartTopProducts;

        // Stat card controls
        private Panel CardTodayOrders, CardRevenue, CardTemp, CardProducts, CardCustomers, CardSuppliers;
        private Label LblTodayOrdersHdr, LblTodayOrders, LblOrderTrend;
        private Label LblRevenueHdr, LblRevenue, LblRevenueSub;
        private Label LblTempHdr, LblTempOrders, LblTempSub;
        private Label LblProductsHdr, LblProducts, LblProductsSub;
        private Label LblCustomersHdr, LblCustomers, LblCustomersSub;
        private Label LblSuppliersHdr, LblSuppliers, LblSuppliersSub;
        // Add these declarations after the existing card declarations
        private Panel CardStockValue;
        private Label LblStockValueHdr;
        private Label LblStockValue;
        private Label LblStockValueSub;
        // Action buttons
        private Button BtnSalesAnalysis, BtnWeeklySalesChart, BtnSalesCharts, BtnProductTrends, BtnPurchaseReports;
    }
}
