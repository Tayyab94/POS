namespace POS_Shop.Views.Controllers
{
    partial class HomeControlUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeControlUI));
            this.bunifuGradientPanel1 = new Bunifu.UI.WinForms.BunifuGradientPanel();
            this.TodayTotalOrderLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bunifuPanel2 = new Bunifu.UI.WinForms.BunifuPanel();
            this.TempTotalOrderLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ReportAnalysisPanelButton = new Bunifu.UI.WinForms.BunifuPanel();
            this.ReportAnalysisLblBtn = new System.Windows.Forms.Label();
            this.bunifuGradientPanel1.SuspendLayout();
            this.bunifuPanel2.SuspendLayout();
            this.ReportAnalysisPanelButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuGradientPanel1
            // 
            this.bunifuGradientPanel1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuGradientPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuGradientPanel1.BackgroundImage")));
            this.bunifuGradientPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuGradientPanel1.BorderRadius = 1;
            this.bunifuGradientPanel1.Controls.Add(this.TodayTotalOrderLbl);
            this.bunifuGradientPanel1.Controls.Add(this.label2);
            this.bunifuGradientPanel1.GradientBottomLeft = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(92)))), ((int)(((byte)(188)))));
            this.bunifuGradientPanel1.GradientBottomRight = System.Drawing.Color.DeepPink;
            this.bunifuGradientPanel1.GradientTopLeft = System.Drawing.Color.DodgerBlue;
            this.bunifuGradientPanel1.GradientTopRight = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(60)))), ((int)(((byte)(212)))));
            this.bunifuGradientPanel1.Location = new System.Drawing.Point(83, 37);
            this.bunifuGradientPanel1.Name = "bunifuGradientPanel1";
            this.bunifuGradientPanel1.Quality = 10;
            this.bunifuGradientPanel1.Size = new System.Drawing.Size(264, 103);
            this.bunifuGradientPanel1.TabIndex = 1;
            // 
            // TodayTotalOrderLbl
            // 
            this.TodayTotalOrderLbl.AutoSize = true;
            this.TodayTotalOrderLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TodayTotalOrderLbl.ForeColor = System.Drawing.SystemColors.Control;
            this.TodayTotalOrderLbl.Location = new System.Drawing.Point(63, 51);
            this.TodayTotalOrderLbl.Name = "TodayTotalOrderLbl";
            this.TodayTotalOrderLbl.Size = new System.Drawing.Size(107, 16);
            this.TodayTotalOrderLbl.TabIndex = 1;
            this.TodayTotalOrderLbl.Text = "Today\'s Order";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(16, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Today\'s Order";
            // 
            // bunifuPanel2
            // 
            this.bunifuPanel2.BackgroundColor = System.Drawing.Color.Wheat;
            this.bunifuPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuPanel2.BackgroundImage")));
            this.bunifuPanel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuPanel2.BorderColor = System.Drawing.Color.Transparent;
            this.bunifuPanel2.BorderRadius = 3;
            this.bunifuPanel2.BorderThickness = 1;
            this.bunifuPanel2.Controls.Add(this.TempTotalOrderLbl);
            this.bunifuPanel2.Controls.Add(this.label3);
            this.bunifuPanel2.Location = new System.Drawing.Point(470, 37);
            this.bunifuPanel2.Name = "bunifuPanel2";
            this.bunifuPanel2.ShowBorders = true;
            this.bunifuPanel2.Size = new System.Drawing.Size(286, 103);
            this.bunifuPanel2.TabIndex = 1;
            // 
            // TempTotalOrderLbl
            // 
            this.TempTotalOrderLbl.AutoSize = true;
            this.TempTotalOrderLbl.BackColor = System.Drawing.Color.Transparent;
            this.TempTotalOrderLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TempTotalOrderLbl.ForeColor = System.Drawing.Color.DarkOrange;
            this.TempTotalOrderLbl.Location = new System.Drawing.Point(120, 51);
            this.TempTotalOrderLbl.Name = "TempTotalOrderLbl";
            this.TempTotalOrderLbl.Size = new System.Drawing.Size(98, 16);
            this.TempTotalOrderLbl.TabIndex = 2;
            this.TempTotalOrderLbl.Text = "Temp Orders";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkOrange;
            this.label3.Location = new System.Drawing.Point(15, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Temp Orders";
            // 
            // ReportAnalysisPanelButton
            // 
            this.ReportAnalysisPanelButton.BackgroundColor = System.Drawing.Color.SlateBlue;
            this.ReportAnalysisPanelButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ReportAnalysisPanelButton.BackgroundImage")));
            this.ReportAnalysisPanelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ReportAnalysisPanelButton.BorderColor = System.Drawing.Color.Transparent;
            this.ReportAnalysisPanelButton.BorderRadius = 3;
            this.ReportAnalysisPanelButton.BorderThickness = 1;
            this.ReportAnalysisPanelButton.Controls.Add(this.ReportAnalysisLblBtn);
            this.ReportAnalysisPanelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ReportAnalysisPanelButton.Location = new System.Drawing.Point(222, 247);
            this.ReportAnalysisPanelButton.Name = "ReportAnalysisPanelButton";
            this.ReportAnalysisPanelButton.ShowBorders = true;
            this.ReportAnalysisPanelButton.Size = new System.Drawing.Size(330, 103);
            this.ReportAnalysisPanelButton.TabIndex = 3;
            // 
            // ReportAnalysisLblBtn
            // 
            this.ReportAnalysisLblBtn.AutoSize = true;
            this.ReportAnalysisLblBtn.BackColor = System.Drawing.Color.Transparent;
            this.ReportAnalysisLblBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportAnalysisLblBtn.ForeColor = System.Drawing.Color.DarkOrange;
            this.ReportAnalysisLblBtn.Location = new System.Drawing.Point(55, 32);
            this.ReportAnalysisLblBtn.Name = "ReportAnalysisLblBtn";
            this.ReportAnalysisLblBtn.Size = new System.Drawing.Size(228, 32);
            this.ReportAnalysisLblBtn.TabIndex = 1;
            this.ReportAnalysisLblBtn.Text = "Report Analysis";
            this.ReportAnalysisLblBtn.Click += new System.EventHandler(this.ReportAnalysisLblBtn_Click);
            // 
            // HomeControlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ReportAnalysisPanelButton);
            this.Controls.Add(this.bunifuPanel2);
            this.Controls.Add(this.bunifuGradientPanel1);
            this.Name = "HomeControlUI";
            this.Size = new System.Drawing.Size(1009, 544);
            this.bunifuGradientPanel1.ResumeLayout(false);
            this.bunifuGradientPanel1.PerformLayout();
            this.bunifuPanel2.ResumeLayout(false);
            this.bunifuPanel2.PerformLayout();
            this.ReportAnalysisPanelButton.ResumeLayout(false);
            this.ReportAnalysisPanelButton.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuGradientPanel bunifuGradientPanel1;
        private Bunifu.UI.WinForms.BunifuPanel bunifuPanel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label TodayTotalOrderLbl;
        private System.Windows.Forms.Label TempTotalOrderLbl;
        private Bunifu.UI.WinForms.BunifuPanel ReportAnalysisPanelButton;
        private System.Windows.Forms.Label ReportAnalysisLblBtn;
    }
}
