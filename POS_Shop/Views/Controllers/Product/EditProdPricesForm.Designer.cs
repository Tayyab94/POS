namespace POS_Shop.Views.Controllers.Product
{
    partial class EditProdPricesForm
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.selectionPanel = new System.Windows.Forms.Panel();
            this.lblSelectType = new System.Windows.Forms.Label();
            this.cmbProductUnit = new System.Windows.Forms.ComboBox(); // Changed name
            this.btnAddPrice = new System.Windows.Forms.Button();
            this.priceControlsContainer = new System.Windows.Forms.Panel();
            this.lblSummary = new System.Windows.Forms.Label();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.selectionPanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductName.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblProductName.Location = new System.Drawing.Point(20, 20);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(400, 30);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "Product: {productName}";
            // 
            // lblInstructions
            // 
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInstructions.ForeColor = System.Drawing.Color.Gray;
            this.lblInstructions.Location = new System.Drawing.Point(20, 54);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(668, 33);
            this.lblInstructions.TabIndex = 1;
            this.lblInstructions.Text = "Add product prices for different units. Each unit represents a different packaging/selling unit.";
            // 
            // selectionPanel
            // 
            this.selectionPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectionPanel.Controls.Add(this.lblSelectType);
            this.selectionPanel.Controls.Add(this.cmbProductUnit); // Updated control name
            this.selectionPanel.Controls.Add(this.btnAddPrice);
            this.selectionPanel.Location = new System.Drawing.Point(20, 90);
            this.selectionPanel.Name = "selectionPanel";
            this.selectionPanel.Padding = new System.Windows.Forms.Padding(5);
            this.selectionPanel.Size = new System.Drawing.Size(800, 54);
            this.selectionPanel.TabIndex = 2;
            // 
            // lblSelectType
            // 
            this.lblSelectType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSelectType.Location = new System.Drawing.Point(10, 8);
            this.lblSelectType.Name = "lblSelectType";
            this.lblSelectType.Size = new System.Drawing.Size(80, 25);
            this.lblSelectType.TabIndex = 0;
            this.lblSelectType.Text = "Select Unit:";
            this.lblSelectType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbProductUnit
            // 
            this.cmbProductUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductUnit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmbProductUnit.FormattingEnabled = true;
            this.cmbProductUnit.Location = new System.Drawing.Point(100, 6);
            this.cmbProductUnit.Name = "cmbProductUnit";
            this.cmbProductUnit.Size = new System.Drawing.Size(200, 28);
            this.cmbProductUnit.TabIndex = 1;
            // 
            // btnAddPrice
            // 
            this.btnAddPrice.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAddPrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddPrice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddPrice.ForeColor = System.Drawing.Color.White;
            this.btnAddPrice.Location = new System.Drawing.Point(310, 6);
            this.btnAddPrice.Name = "btnAddPrice";
            this.btnAddPrice.Size = new System.Drawing.Size(120, 28);
            this.btnAddPrice.TabIndex = 2;
            this.btnAddPrice.Text = "Add Price Unit";
            this.btnAddPrice.UseVisualStyleBackColor = false;
            this.btnAddPrice.Click += new System.EventHandler(this.btnAddPrice_Click);
            // 
            // priceControlsContainer
            // 
            this.priceControlsContainer.AutoScroll = true;
            this.priceControlsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.priceControlsContainer.Location = new System.Drawing.Point(20, 150);
            this.priceControlsContainer.Name = "priceControlsContainer";
            this.priceControlsContainer.Size = new System.Drawing.Size(800, 350);
            this.priceControlsContainer.TabIndex = 3;
            // 
            // lblSummary
            // 
            this.lblSummary.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSummary.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblSummary.Location = new System.Drawing.Point(20, 509);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(800, 25);
            this.lblSummary.TabIndex = 4;
            this.lblSummary.Text = "Summary";
            // 
            // buttonPanel
            // 
            this.buttonPanel.Controls.Add(this.btnSave);
            this.buttonPanel.Controls.Add(this.btnCancel);
            this.buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.buttonPanel.Location = new System.Drawing.Point(0, 540);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Padding = new System.Windows.Forms.Padding(10);
            this.buttonPanel.Size = new System.Drawing.Size(850, 60);
            this.buttonPanel.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SeaGreen;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(500, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save All Prices";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(660, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // NewProductPriceForm
            // 
            this.ClientSize = new System.Drawing.Size(850, 600);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.selectionPanel);
            this.Controls.Add(this.priceControlsContainer);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.buttonPanel);
            this.MinimumSize = new System.Drawing.Size(850, 400);
            this.Name = "NewProductPriceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Product Prices";
            this.selectionPanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Panel selectionPanel;
        private System.Windows.Forms.Label lblSelectType;
        private System.Windows.Forms.ComboBox cmbProductUnit; // Updated
        private System.Windows.Forms.Button btnAddPrice;
        private System.Windows.Forms.Panel priceControlsContainer;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.Panel buttonPanel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}