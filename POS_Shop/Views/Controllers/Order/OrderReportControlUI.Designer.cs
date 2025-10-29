namespace POS_Shop.Views.Controllers.Order
{
    partial class OrderReportControlUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrderReportControlUI));
            this.WeeklySaleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bunifuPanel1 = new Bunifu.UI.WinForms.BunifuPanel();
            this.TodayTotalOrderSaleLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.WeeklySaleChart)).BeginInit();
            this.bunifuPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // WeeklySaleChart
            // 
            chartArea1.Name = "ChartArea1";
            this.WeeklySaleChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.WeeklySaleChart.Legends.Add(legend1);
            this.WeeklySaleChart.Location = new System.Drawing.Point(60, 177);
            this.WeeklySaleChart.Name = "WeeklySaleChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.WeeklySaleChart.Series.Add(series1);
            this.WeeklySaleChart.Size = new System.Drawing.Size(911, 340);
            this.WeeklySaleChart.TabIndex = 3;
            this.WeeklySaleChart.Text = "Weekly Sale";
            // 
            // bunifuPanel1
            // 
            this.bunifuPanel1.BackgroundColor = System.Drawing.Color.SlateBlue;
            this.bunifuPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuPanel1.BackgroundImage")));
            this.bunifuPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuPanel1.BorderColor = System.Drawing.Color.Transparent;
            this.bunifuPanel1.BorderRadius = 3;
            this.bunifuPanel1.BorderThickness = 1;
            this.bunifuPanel1.Controls.Add(this.TodayTotalOrderSaleLbl);
            this.bunifuPanel1.Controls.Add(this.label1);
            this.bunifuPanel1.Location = new System.Drawing.Point(60, 33);
            this.bunifuPanel1.Name = "bunifuPanel1";
            this.bunifuPanel1.ShowBorders = true;
            this.bunifuPanel1.Size = new System.Drawing.Size(293, 106);
            this.bunifuPanel1.TabIndex = 4;
            // 
            // TodayTotalOrderSaleLbl
            // 
            this.TodayTotalOrderSaleLbl.AutoSize = true;
            this.TodayTotalOrderSaleLbl.BackColor = System.Drawing.Color.Transparent;
            this.TodayTotalOrderSaleLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodayTotalOrderSaleLbl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TodayTotalOrderSaleLbl.Location = new System.Drawing.Point(104, 54);
            this.TodayTotalOrderSaleLbl.Name = "TodayTotalOrderSaleLbl";
            this.TodayTotalOrderSaleLbl.Size = new System.Drawing.Size(46, 20);
            this.TodayTotalOrderSaleLbl.TabIndex = 1;
            this.TodayTotalOrderSaleLbl.Text = "Sale";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sale";
            // 
            // OrderReportControlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bunifuPanel1);
            this.Controls.Add(this.WeeklySaleChart);
            this.Name = "OrderReportControlUI";
            this.Size = new System.Drawing.Size(1028, 545);
            ((System.ComponentModel.ISupportInitialize)(this.WeeklySaleChart)).EndInit();
            this.bunifuPanel1.ResumeLayout(false);
            this.bunifuPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart WeeklySaleChart;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel1;
        private System.Windows.Forms.Label TodayTotalOrderSaleLbl;
        private System.Windows.Forms.Label label1;
    }
}
