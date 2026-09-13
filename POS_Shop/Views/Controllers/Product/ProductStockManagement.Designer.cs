using System;
using System.Drawing;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Product
{
    partial class ProductStockManagement
    {
        private System.ComponentModel.IContainer components = null;

        // Main containers
        private Panel pnlMain;
        private Panel pnlHeader;
        private Panel pnlTable;
        private Panel pnlButtons;

        // Header controls
        private Label lblTitle;
        private Label lblProductName;

        // Table-like display
        private Label lblIdTitle;
        private Label lblIdValue;
        private Label lblStockUnitTitle;
        private Label lblStockUnitValue;
        private Label lblPreQtyTitle;
        private Label lblPreQtyValue;
        private Label lblNewQtyTitle;
        private TextBox txtNewQty;
        private Label lblNewQty;
        private Label lblCalculatedTotal;
        private Label lblStatus;

        // Buttons
        private Button btnUpdate;
        private Button btnRefresh;
        private Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.pnlTable = new System.Windows.Forms.Panel();
            this.lblIdTitle = new System.Windows.Forms.Label();
            this.lblIdValue = new System.Windows.Forms.Label();
            this.lblStockUnitTitle = new System.Windows.Forms.Label();
            this.lblStockUnitValue = new System.Windows.Forms.Label();
            this.lblPreQtyTitle = new System.Windows.Forms.Label();
            this.lblPreQtyValue = new System.Windows.Forms.Label();
            this.lblNewQtyTitle = new System.Windows.Forms.Label();
            this.txtNewQty = new System.Windows.Forms.TextBox();
            this.lblNewQty = new System.Windows.Forms.Label();
            this.lblCalculatedTotal = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlTable.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlHeader);
            this.pnlMain.Controls.Add(this.pnlTable);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(550, 360);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(20, 300);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(510, 45);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(8, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(331, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Product Stock Management";
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblProductName.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblProductName.Location = new System.Drawing.Point(24, 190);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(20, 25);
            this.lblProductName.TabIndex = 1;
            this.lblProductName.Text = "-";
            // 
            // pnlTable
            // 
            this.pnlTable.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTable.Controls.Add(this.lblIdTitle);
            this.pnlTable.Controls.Add(this.lblProductName);
            this.pnlTable.Controls.Add(this.lblIdValue);
            this.pnlTable.Controls.Add(this.lblStockUnitTitle);
            this.pnlTable.Controls.Add(this.lblStockUnitValue);
            this.pnlTable.Controls.Add(this.lblPreQtyTitle);
            this.pnlTable.Controls.Add(this.lblPreQtyValue);
            this.pnlTable.Controls.Add(this.lblNewQtyTitle);
            this.pnlTable.Controls.Add(this.txtNewQty);
            this.pnlTable.Controls.Add(this.lblNewQty);
            this.pnlTable.Controls.Add(this.lblCalculatedTotal);
            this.pnlTable.Controls.Add(this.lblStatus);
            this.pnlTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTable.Location = new System.Drawing.Point(20, 80);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTable.Size = new System.Drawing.Size(510, 220);
            this.pnlTable.TabIndex = 1;
            // 
            // lblIdTitle
            // 
            this.lblIdTitle.AutoSize = true;
            this.lblIdTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblIdTitle.Location = new System.Drawing.Point(25, 25);
            this.lblIdTitle.Name = "lblIdTitle";
            this.lblIdTitle.Size = new System.Drawing.Size(26, 23);
            this.lblIdTitle.TabIndex = 0;
            this.lblIdTitle.Text = "Id";
            // 
            // lblIdValue
            // 
            this.lblIdValue.AutoSize = true;
            this.lblIdValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdValue.Location = new System.Drawing.Point(210, 25);
            this.lblIdValue.Name = "lblIdValue";
            this.lblIdValue.Size = new System.Drawing.Size(17, 23);
            this.lblIdValue.TabIndex = 1;
            this.lblIdValue.Text = "-";
            // 
            // lblStockUnitTitle
            // 
            this.lblStockUnitTitle.AutoSize = true;
            this.lblStockUnitTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStockUnitTitle.Location = new System.Drawing.Point(25, 55);
            this.lblStockUnitTitle.Name = "lblStockUnitTitle";
            this.lblStockUnitTitle.Size = new System.Drawing.Size(94, 23);
            this.lblStockUnitTitle.TabIndex = 2;
            this.lblStockUnitTitle.Text = "Stock Unit";
            // 
            // lblStockUnitValue
            // 
            this.lblStockUnitValue.AutoSize = true;
            this.lblStockUnitValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStockUnitValue.Location = new System.Drawing.Point(210, 55);
            this.lblStockUnitValue.Name = "lblStockUnitValue";
            this.lblStockUnitValue.Size = new System.Drawing.Size(17, 23);
            this.lblStockUnitValue.TabIndex = 3;
            this.lblStockUnitValue.Text = "-";
            // 
            // lblPreQtyTitle
            // 
            this.lblPreQtyTitle.AutoSize = true;
            this.lblPreQtyTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPreQtyTitle.Location = new System.Drawing.Point(25, 85);
            this.lblPreQtyTitle.Name = "lblPreQtyTitle";
            this.lblPreQtyTitle.Size = new System.Drawing.Size(72, 23);
            this.lblPreQtyTitle.TabIndex = 4;
            this.lblPreQtyTitle.Text = "Pre_Qty";
            // 
            // lblPreQtyValue
            // 
            this.lblPreQtyValue.AutoSize = true;
            this.lblPreQtyValue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPreQtyValue.Location = new System.Drawing.Point(210, 85);
            this.lblPreQtyValue.Name = "lblPreQtyValue";
            this.lblPreQtyValue.Size = new System.Drawing.Size(19, 23);
            this.lblPreQtyValue.TabIndex = 5;
            this.lblPreQtyValue.Text = "0";
            // 
            // lblNewQtyTitle
            // 
            this.lblNewQtyTitle.AutoSize = true;
            this.lblNewQtyTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNewQtyTitle.Location = new System.Drawing.Point(25, 115);
            this.lblNewQtyTitle.Name = "lblNewQtyTitle";
            this.lblNewQtyTitle.Size = new System.Drawing.Size(75, 23);
            this.lblNewQtyTitle.TabIndex = 6;
            this.lblNewQtyTitle.Text = "NewQty";
            // 
            // txtNewQty
            // 
            this.txtNewQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewQty.Location = new System.Drawing.Point(200, 102);
            this.txtNewQty.Name = "txtNewQty";
            this.txtNewQty.Size = new System.Drawing.Size(80, 30);
            this.txtNewQty.TabIndex = 7;
            this.txtNewQty.Text = "0";
            this.txtNewQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNewQty.TextChanged += new System.EventHandler(this.txtNewQty_TextChanged);
            this.txtNewQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewQty_KeyPress);
            // 
            // lblNewQty
            // 
            this.lblNewQty.AutoSize = true;
            this.lblNewQty.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNewQty.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblNewQty.Location = new System.Drawing.Point(210, 150);
            this.lblNewQty.Name = "lblNewQty";
            this.lblNewQty.Size = new System.Drawing.Size(88, 25);
            this.lblNewQty.TabIndex = 8;
            this.lblNewQty.Text = "0 + 0 = 0";
            // 
            // lblCalculatedTotal
            // 
            this.lblCalculatedTotal.AutoSize = true;
            this.lblCalculatedTotal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCalculatedTotal.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblCalculatedTotal.Location = new System.Drawing.Point(440, 150);
            this.lblCalculatedTotal.Name = "lblCalculatedTotal";
            this.lblCalculatedTotal.Size = new System.Drawing.Size(24, 28);
            this.lblCalculatedTotal.TabIndex = 9;
            this.lblCalculatedTotal.Text = "0";
            this.lblCalculatedTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(25, 195);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(13, 20);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = " ";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnUpdate);
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(20, 20);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(510, 60);
            this.pnlButtons.TabIndex = 2;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.ForestGreen;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(10, 15);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(110, 35);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Update Qty";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.SteelBlue;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(130, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 35);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Crimson;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(400, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 35);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ProductStockManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 360);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "ProductStockManagement";
            this.Text = "Product Stock Management";
            this.pnlMain.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTable.ResumeLayout(false);
            this.pnlTable.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}