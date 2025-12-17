namespace POS_Shop.Views.Controllers.Order
{
    partial class SalesReportDataControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.WeeklySaleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnLastMonth = new System.Windows.Forms.Button();
            this.btnThisMonth = new System.Windows.Forms.Button();
            this.btnLast7Days = new System.Windows.Forms.Button();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.panelTotalOrders = new System.Windows.Forms.Panel();
            this.lblTotalOrdersValue = new System.Windows.Forms.Label();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.panelAvgSales = new System.Windows.Forms.Panel();
            this.lblAvgSalesValue = new System.Windows.Forms.Label();
            this.lblAvgSales = new System.Windows.Forms.Label();
            this.panelTotalRevenue = new System.Windows.Forms.Panel();
            this.lblTotalRevenueValue = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.WeeklySaleChart)).BeginInit();
            this.panelTop.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelTotalOrders.SuspendLayout();
            this.panelAvgSales.SuspendLayout();
            this.panelTotalRevenue.SuspendLayout();
            this.SuspendLayout();
            // 
            // WeeklySaleChart
            // 
            chartArea1.Name = "ChartArea1";
            this.WeeklySaleChart.ChartAreas.Add(chartArea1);
            this.WeeklySaleChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.WeeklySaleChart.Legends.Add(legend1);
            this.WeeklySaleChart.Location = new System.Drawing.Point(0, 200);
            this.WeeklySaleChart.Name = "WeeklySaleChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.WeeklySaleChart.Series.Add(series1);
            this.WeeklySaleChart.Size = new System.Drawing.Size(1100, 530);
            this.WeeklySaleChart.TabIndex = 0;
            this.WeeklySaleChart.Text = "chart1";
            title1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "Sales Report";
            this.WeeklySaleChart.Titles.Add(title1);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.SteelBlue;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1100, 60);
            this.panelTop.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(233, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sales Analytics";
            // 
            // panelFilters
            // 
            this.panelFilters.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFilters.Controls.Add(this.groupBox1);
            this.panelFilters.Controls.Add(this.btnGenerateReport);
            this.panelFilters.Controls.Add(this.dtpToDate);
            this.panelFilters.Controls.Add(this.label2);
            this.panelFilters.Controls.Add(this.dtpFromDate);
            this.panelFilters.Controls.Add(this.label1);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(0, 60);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(15);
            this.panelFilters.Size = new System.Drawing.Size(1100, 80);
            this.panelFilters.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLastMonth);
            this.groupBox1.Controls.Add(this.btnThisMonth);
            this.groupBox1.Controls.Add(this.btnLast7Days);
            this.groupBox1.Location = new System.Drawing.Point(650, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(380, 55);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quick Filters";
            // 
            // btnLastMonth
            // 
            this.btnLastMonth.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnLastMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLastMonth.ForeColor = System.Drawing.Color.White;
            this.btnLastMonth.Location = new System.Drawing.Point(260, 19);
            this.btnLastMonth.Name = "btnLastMonth";
            this.btnLastMonth.Size = new System.Drawing.Size(110, 30);
            this.btnLastMonth.TabIndex = 2;
            this.btnLastMonth.Text = "Last Month";
            this.btnLastMonth.UseVisualStyleBackColor = false;
            this.btnLastMonth.Click += new System.EventHandler(this.btnLastMonth_Click);
            // 
            // btnThisMonth
            // 
            this.btnThisMonth.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnThisMonth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThisMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThisMonth.ForeColor = System.Drawing.Color.White;
            this.btnThisMonth.Location = new System.Drawing.Point(140, 19);
            this.btnThisMonth.Name = "btnThisMonth";
            this.btnThisMonth.Size = new System.Drawing.Size(110, 30);
            this.btnThisMonth.TabIndex = 1;
            this.btnThisMonth.Text = "This Month";
            this.btnThisMonth.UseVisualStyleBackColor = false;
            this.btnThisMonth.Click += new System.EventHandler(this.btnThisMonth_Click);
            // 
            // btnLast7Days
            // 
            this.btnLast7Days.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLast7Days.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLast7Days.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLast7Days.ForeColor = System.Drawing.Color.White;
            this.btnLast7Days.Location = new System.Drawing.Point(20, 18);
            this.btnLast7Days.Name = "btnLast7Days";
            this.btnLast7Days.Size = new System.Drawing.Size(110, 30);
            this.btnLast7Days.TabIndex = 0;
            this.btnLast7Days.Text = "Last 7 Days";
            this.btnLast7Days.UseVisualStyleBackColor = false;
            this.btnLast7Days.Click += new System.EventHandler(this.btnLast7Days_Click);
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.BackColor = System.Drawing.Color.SteelBlue;
            this.btnGenerateReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerateReport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateReport.Location = new System.Drawing.Point(480, 15);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(140, 35);
            this.btnGenerateReport.TabIndex = 5;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = false;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(310, 20);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(150, 22);
            this.dtpToDate.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(230, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd-MMM-yyyy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(70, 20);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(150, 22);
            this.dtpFromDate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "From";
            // 
            // panelStats
            // 
            this.panelStats.BackColor = System.Drawing.Color.White;
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.panelTotalOrders);
            this.panelStats.Controls.Add(this.panelAvgSales);
            this.panelStats.Controls.Add(this.panelTotalRevenue);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 140);
            this.panelStats.Name = "panelStats";
            this.panelStats.Padding = new System.Windows.Forms.Padding(10);
            this.panelStats.Size = new System.Drawing.Size(1100, 60);
            this.panelStats.TabIndex = 3;
            // 
            // panelTotalOrders
            // 
            this.panelTotalOrders.BackColor = System.Drawing.Color.LavenderBlush;
            this.panelTotalOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTotalOrders.Controls.Add(this.lblTotalOrdersValue);
            this.panelTotalOrders.Controls.Add(this.lblTotalOrders);
            this.panelTotalOrders.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTotalOrders.Location = new System.Drawing.Point(730, 10);
            this.panelTotalOrders.Name = "panelTotalOrders";
            this.panelTotalOrders.Padding = new System.Windows.Forms.Padding(5);
            this.panelTotalOrders.Size = new System.Drawing.Size(360, 38);
            this.panelTotalOrders.TabIndex = 8;
            // 
            // lblTotalOrdersValue
            // 
            this.lblTotalOrdersValue.AutoSize = true;
            this.lblTotalOrdersValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalOrdersValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrdersValue.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTotalOrdersValue.Location = new System.Drawing.Point(287, 5);
            this.lblTotalOrdersValue.Name = "lblTotalOrdersValue";
            this.lblTotalOrdersValue.Size = new System.Drawing.Size(66, 25);
            this.lblTotalOrdersValue.TabIndex = 9;
            this.lblTotalOrdersValue.Text = "1,234";
            this.lblTotalOrdersValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotalOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrders.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTotalOrders.Location = new System.Drawing.Point(5, 5);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(121, 20);
            this.lblTotalOrders.TabIndex = 8;
            this.lblTotalOrders.Text = "Total Orders:";
            // 
            // panelAvgSales
            // 
            this.panelAvgSales.BackColor = System.Drawing.Color.Honeydew;
            this.panelAvgSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAvgSales.Controls.Add(this.lblAvgSalesValue);
            this.panelAvgSales.Controls.Add(this.lblAvgSales);
            this.panelAvgSales.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelAvgSales.Location = new System.Drawing.Point(370, 10);
            this.panelAvgSales.Name = "panelAvgSales";
            this.panelAvgSales.Padding = new System.Windows.Forms.Padding(5);
            this.panelAvgSales.Size = new System.Drawing.Size(360, 38);
            this.panelAvgSales.TabIndex = 7;
            // 
            // lblAvgSalesValue
            // 
            this.lblAvgSalesValue.AutoSize = true;
            this.lblAvgSalesValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblAvgSalesValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgSalesValue.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblAvgSalesValue.Location = new System.Drawing.Point(245, 5);
            this.lblAvgSalesValue.Name = "lblAvgSalesValue";
            this.lblAvgSalesValue.Size = new System.Drawing.Size(108, 25);
            this.lblAvgSalesValue.TabIndex = 8;
            this.lblAvgSalesValue.Text = "25,737.60";
            this.lblAvgSalesValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAvgSales
            // 
            this.lblAvgSales.AutoSize = true;
            this.lblAvgSales.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblAvgSales.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvgSales.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblAvgSales.Location = new System.Drawing.Point(5, 5);
            this.lblAvgSales.Name = "lblAvgSales";
            this.lblAvgSales.Size = new System.Drawing.Size(122, 20);
            this.lblAvgSales.TabIndex = 7;
            this.lblAvgSales.Text = "Average/Day:";
            // 
            // panelTotalRevenue
            // 
            this.panelTotalRevenue.BackColor = System.Drawing.Color.AliceBlue;
            this.panelTotalRevenue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTotalRevenue.Controls.Add(this.lblTotalRevenueValue);
            this.panelTotalRevenue.Controls.Add(this.lblTotalRevenue);
            this.panelTotalRevenue.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelTotalRevenue.Location = new System.Drawing.Point(10, 10);
            this.panelTotalRevenue.Name = "panelTotalRevenue";
            this.panelTotalRevenue.Padding = new System.Windows.Forms.Padding(5);
            this.panelTotalRevenue.Size = new System.Drawing.Size(360, 38);
            this.panelTotalRevenue.TabIndex = 6;
            // 
            // lblTotalRevenueValue
            // 
            this.lblTotalRevenueValue.AutoSize = true;
            this.lblTotalRevenueValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTotalRevenueValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenueValue.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotalRevenueValue.Location = new System.Drawing.Point(233, 5);
            this.lblTotalRevenueValue.Name = "lblTotalRevenueValue";
            this.lblTotalRevenueValue.Size = new System.Drawing.Size(120, 25);
            this.lblTotalRevenueValue.TabIndex = 7;
            this.lblTotalRevenueValue.Text = "772,128.00";
            this.lblTotalRevenueValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblTotalRevenue.Location = new System.Drawing.Point(5, 5);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(135, 20);
            this.lblTotalRevenue.TabIndex = 6;
            this.lblTotalRevenue.Text = "Total Revenue:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(0, 730);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 0, 0, 10);
            this.lblStatus.Size = new System.Drawing.Size(138, 28);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Ready to generate";
            // 
            // SalesReportDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WeeklySaleChart);
            this.Controls.Add(this.panelStats);
            this.Controls.Add(this.panelFilters);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblStatus);
            this.Name = "SalesReportDataControl";
            this.Size = new System.Drawing.Size(1100, 758);
            this.Load += new System.EventHandler(this.SalesChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.WeeklySaleChart)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelFilters.ResumeLayout(false);
            this.panelFilters.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panelStats.ResumeLayout(false);
            this.panelTotalOrders.ResumeLayout(false);
            this.panelTotalOrders.PerformLayout();
            this.panelAvgSales.ResumeLayout(false);
            this.panelAvgSales.PerformLayout();
            this.panelTotalRevenue.ResumeLayout(false);
            this.panelTotalRevenue.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart WeeklySaleChart;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLastMonth;
        private System.Windows.Forms.Button btnThisMonth;
        private System.Windows.Forms.Button btnLast7Days;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Panel panelTotalOrders;
        private System.Windows.Forms.Label lblTotalOrdersValue;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Panel panelAvgSales;
        private System.Windows.Forms.Label lblAvgSalesValue;
        private System.Windows.Forms.Label lblAvgSales;
        private System.Windows.Forms.Panel panelTotalRevenue;
        private System.Windows.Forms.Label lblTotalRevenueValue;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblStatus;
    }
}

