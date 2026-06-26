//namespace POS_Shop.Views.Controllers.Order
//{
//    partial class OrdersControlUI
//    {
//        /// <summary> 
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary> 
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        /// <summary> 
//        /// Required method for Designer support - do not modify 
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersControlUI));
//            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
//            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
//            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
//            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
//            this.label1 = new System.Windows.Forms.Label();
//            this.SearchOrderTxt = new Bunifu.UI.WinForms.BunifuTextBox();
//            this.OrderListDataGrid = new Bunifu.UI.WinForms.BunifuDataGridView();
//            this.PreviousPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
//            this.NextPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
//            this.lblStatus = new System.Windows.Forms.Label();
//            this.OrderIDLbl = new System.Windows.Forms.Label();
//            this.InvoiceNoLbl = new System.Windows.Forms.Label();
//            this.OrderDetailGroup = new System.Windows.Forms.GroupBox();
//            this.OrderDetailList = new System.Windows.Forms.DataGridView();
//            this.InvNumbnerLbl = new System.Windows.Forms.Label();
//            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
//            this.OrderListGroup = new System.Windows.Forms.GroupBox();
//            ((System.ComponentModel.ISupportInitialize)(this.OrderListDataGrid)).BeginInit();
//            this.OrderDetailGroup.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.OrderDetailList)).BeginInit();
//            this.OrderListGroup.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.label1.Location = new System.Drawing.Point(21, 57);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(366, 29);
//            this.label1.TabIndex = 0;
//            this.label1.Text = "Search Order By Invoice Number";
//            // 
//            // SearchOrderTxt
//            // 
//            this.SearchOrderTxt.AcceptsReturn = false;
//            this.SearchOrderTxt.AcceptsTab = false;
//            this.SearchOrderTxt.AnimationSpeed = 200;
//            this.SearchOrderTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
//            this.SearchOrderTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
//            this.SearchOrderTxt.BackColor = System.Drawing.Color.Transparent;
//            this.SearchOrderTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchOrderTxt.BackgroundImage")));
//            this.SearchOrderTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
//            this.SearchOrderTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
//            this.SearchOrderTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
//            this.SearchOrderTxt.BorderColorIdle = System.Drawing.Color.Silver;
//            this.SearchOrderTxt.BorderRadius = 1;
//            this.SearchOrderTxt.BorderThickness = 1;
//            this.SearchOrderTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
//            this.SearchOrderTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
//            this.SearchOrderTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
//            this.SearchOrderTxt.DefaultText = "";
//            this.SearchOrderTxt.FillColor = System.Drawing.Color.White;
//            this.SearchOrderTxt.HideSelection = true;
//            this.SearchOrderTxt.IconLeft = null;
//            this.SearchOrderTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
//            this.SearchOrderTxt.IconPadding = 10;
//            this.SearchOrderTxt.IconRight = null;
//            this.SearchOrderTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
//            this.SearchOrderTxt.Lines = new string[0];
//            this.SearchOrderTxt.Location = new System.Drawing.Point(18, 93);
//            this.SearchOrderTxt.MaxLength = 32767;
//            this.SearchOrderTxt.MinimumSize = new System.Drawing.Size(1, 1);
//            this.SearchOrderTxt.Modified = false;
//            this.SearchOrderTxt.Multiline = false;
//            this.SearchOrderTxt.Name = "SearchOrderTxt";
//            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
//            stateProperties1.FillColor = System.Drawing.Color.Empty;
//            stateProperties1.ForeColor = System.Drawing.Color.Empty;
//            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
//            this.SearchOrderTxt.OnActiveState = stateProperties1;
//            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
//            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
//            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
//            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
//            this.SearchOrderTxt.OnDisabledState = stateProperties2;
//            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
//            stateProperties3.FillColor = System.Drawing.Color.Empty;
//            stateProperties3.ForeColor = System.Drawing.Color.Empty;
//            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
//            this.SearchOrderTxt.OnHoverState = stateProperties3;
//            stateProperties4.BorderColor = System.Drawing.Color.Silver;
//            stateProperties4.FillColor = System.Drawing.Color.White;
//            stateProperties4.ForeColor = System.Drawing.Color.Empty;
//            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
//            this.SearchOrderTxt.OnIdleState = stateProperties4;
//            this.SearchOrderTxt.Padding = new System.Windows.Forms.Padding(3);
//            this.SearchOrderTxt.PasswordChar = '\0';
//            this.SearchOrderTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
//            this.SearchOrderTxt.PlaceholderText = "Search Order";
//            this.SearchOrderTxt.ReadOnly = false;
//            this.SearchOrderTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
//            this.SearchOrderTxt.SelectedText = "";
//            this.SearchOrderTxt.SelectionLength = 0;
//            this.SearchOrderTxt.SelectionStart = 0;
//            this.SearchOrderTxt.ShortcutsEnabled = true;
//            this.SearchOrderTxt.Size = new System.Drawing.Size(500, 41);
//            this.SearchOrderTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
//            this.SearchOrderTxt.TabIndex = 1;
//            this.SearchOrderTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
//            this.SearchOrderTxt.TextMarginBottom = 0;
//            this.SearchOrderTxt.TextMarginLeft = 3;
//            this.SearchOrderTxt.TextMarginTop = 0;
//            this.SearchOrderTxt.TextPlaceholder = "Search Order";
//            this.SearchOrderTxt.UseSystemPasswordChar = false;
//            this.SearchOrderTxt.WordWrap = true;
//            this.SearchOrderTxt.TextChange += new System.EventHandler(this.SearchOrderTxt_TextChange);
//            this.SearchOrderTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchOrderTxt_KeyDown);
//            // 
//            // OrderListDataGrid
//            // 
//            this.OrderListDataGrid.AllowCustomTheming = false;
//            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
//            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
//            this.OrderListDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
//            this.OrderListDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.OrderListDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.OrderListDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
//            this.OrderListDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
//            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
//            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
//            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
//            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
//            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
//            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
//            this.OrderListDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
//            this.OrderListDataGrid.ColumnHeadersHeight = 40;
//            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
//            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
//            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
//            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
//            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
//            this.OrderListDataGrid.CurrentTheme.BackColor = System.Drawing.Color.White;
//            this.OrderListDataGrid.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
//            this.OrderListDataGrid.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
//            this.OrderListDataGrid.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
//            this.OrderListDataGrid.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
//            this.OrderListDataGrid.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
//            this.OrderListDataGrid.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
//            this.OrderListDataGrid.CurrentTheme.Name = null;
//            this.OrderListDataGrid.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
//            this.OrderListDataGrid.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
//            this.OrderListDataGrid.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
//            this.OrderListDataGrid.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
//            this.OrderListDataGrid.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
//            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
//            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
//            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
//            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
//            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
//            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
//            this.OrderListDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
//            this.OrderListDataGrid.EnableHeadersVisualStyles = false;
//            this.OrderListDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
//            this.OrderListDataGrid.HeaderBackColor = System.Drawing.Color.DodgerBlue;
//            this.OrderListDataGrid.HeaderBgColor = System.Drawing.Color.Empty;
//            this.OrderListDataGrid.HeaderForeColor = System.Drawing.Color.White;
//            this.OrderListDataGrid.Location = new System.Drawing.Point(6, 21);
//            this.OrderListDataGrid.Name = "OrderListDataGrid";
//            this.OrderListDataGrid.RowHeadersVisible = false;
//            this.OrderListDataGrid.RowHeadersWidth = 51;
//            this.OrderListDataGrid.RowTemplate.Height = 40;
//            this.OrderListDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
//            this.OrderListDataGrid.Size = new System.Drawing.Size(1047, 634);
//            this.OrderListDataGrid.TabIndex = 2;
//            this.OrderListDataGrid.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
//            // 
//            // PreviousPageBtn
//            // 
//            this.PreviousPageBtn.ActiveImage = null;
//            this.PreviousPageBtn.AllowAnimations = true;
//            this.PreviousPageBtn.AllowBuffering = false;
//            this.PreviousPageBtn.AllowToggling = false;
//            this.PreviousPageBtn.AllowZooming = false;
//            this.PreviousPageBtn.AllowZoomingOnFocus = false;
//            this.PreviousPageBtn.BackColor = System.Drawing.Color.Transparent;
//            this.PreviousPageBtn.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.PreviousPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
//            this.PreviousPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.ErrorImage")));
//            this.PreviousPageBtn.FadeWhenInactive = false;
//            this.PreviousPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
//            this.PreviousPageBtn.Image = global::POS_Shop.Properties.Resources.iconPrev;
//            this.PreviousPageBtn.ImageActive = null;
//            this.PreviousPageBtn.ImageLocation = null;
//            this.PreviousPageBtn.ImageMargin = 2;
//            this.PreviousPageBtn.ImageSize = new System.Drawing.Size(33, 36);
//            this.PreviousPageBtn.ImageZoomSize = new System.Drawing.Size(35, 38);
//            this.PreviousPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.InitialImage")));
//            this.PreviousPageBtn.Location = new System.Drawing.Point(987, 130);
//            this.PreviousPageBtn.Name = "PreviousPageBtn";
//            this.PreviousPageBtn.Rotation = 0;
//            this.PreviousPageBtn.ShowActiveImage = true;
//            this.PreviousPageBtn.ShowCursorChanges = true;
//            this.PreviousPageBtn.ShowImageBorders = true;
//            this.PreviousPageBtn.ShowSizeMarkers = false;
//            this.PreviousPageBtn.Size = new System.Drawing.Size(35, 38);
//            this.PreviousPageBtn.TabIndex = 22;
//            this.PreviousPageBtn.ToolTipText = "";
//            this.PreviousPageBtn.WaitOnLoad = false;
//            this.PreviousPageBtn.Zoom = 2;
//            this.PreviousPageBtn.ZoomSpeed = 10;
//            this.PreviousPageBtn.Click += new System.EventHandler(this.PreviousPageBtn_Click);
//            // 
//            // NextPageBtn
//            // 
//            this.NextPageBtn.ActiveImage = null;
//            this.NextPageBtn.AllowAnimations = true;
//            this.NextPageBtn.AllowBuffering = false;
//            this.NextPageBtn.AllowToggling = false;
//            this.NextPageBtn.AllowZooming = false;
//            this.NextPageBtn.AllowZoomingOnFocus = false;
//            this.NextPageBtn.BackColor = System.Drawing.Color.Transparent;
//            this.NextPageBtn.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.NextPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
//            this.NextPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.ErrorImage")));
//            this.NextPageBtn.FadeWhenInactive = false;
//            this.NextPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
//            this.NextPageBtn.Image = global::POS_Shop.Properties.Resources.iconNext;
//            this.NextPageBtn.ImageActive = null;
//            this.NextPageBtn.ImageLocation = null;
//            this.NextPageBtn.ImageMargin = 2;
//            this.NextPageBtn.ImageSize = new System.Drawing.Size(33, 36);
//            this.NextPageBtn.ImageZoomSize = new System.Drawing.Size(35, 38);
//            this.NextPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.InitialImage")));
//            this.NextPageBtn.Location = new System.Drawing.Point(1028, 130);
//            this.NextPageBtn.Name = "NextPageBtn";
//            this.NextPageBtn.Rotation = 0;
//            this.NextPageBtn.ShowActiveImage = true;
//            this.NextPageBtn.ShowCursorChanges = true;
//            this.NextPageBtn.ShowImageBorders = true;
//            this.NextPageBtn.ShowSizeMarkers = false;
//            this.NextPageBtn.Size = new System.Drawing.Size(35, 38);
//            this.NextPageBtn.TabIndex = 21;
//            this.NextPageBtn.ToolTipText = "";
//            this.NextPageBtn.WaitOnLoad = false;
//            this.NextPageBtn.Zoom = 2;
//            this.NextPageBtn.ZoomSpeed = 10;
//            this.NextPageBtn.Click += new System.EventHandler(this.NextPageBtn_Click);
//            // 
//            // lblStatus
//            // 
//            this.lblStatus.AutoSize = true;
//            this.lblStatus.Location = new System.Drawing.Point(98, 152);
//            this.lblStatus.Name = "lblStatus";
//            this.lblStatus.Size = new System.Drawing.Size(44, 16);
//            this.lblStatus.TabIndex = 20;
//            this.lblStatus.Text = "Status";
//            // 
//            // OrderIDLbl
//            // 
//            this.OrderIDLbl.AutoSize = true;
//            this.OrderIDLbl.Location = new System.Drawing.Point(25, 26);
//            this.OrderIDLbl.Name = "OrderIDLbl";
//            this.OrderIDLbl.Size = new System.Drawing.Size(54, 16);
//            this.OrderIDLbl.TabIndex = 23;
//            this.OrderIDLbl.Text = "OrderID";
//            this.OrderIDLbl.Visible = false;
//            // 
//            // InvoiceNoLbl
//            // 
//            this.InvoiceNoLbl.AutoSize = true;
//            this.InvoiceNoLbl.Location = new System.Drawing.Point(97, 26);
//            this.InvoiceNoLbl.Name = "InvoiceNoLbl";
//            this.InvoiceNoLbl.Size = new System.Drawing.Size(68, 16);
//            this.InvoiceNoLbl.TabIndex = 24;
//            this.InvoiceNoLbl.Text = "InvoiceNo";
//            this.InvoiceNoLbl.Visible = false;
//            // 
//            // OrderDetailGroup
//            // 
//            this.OrderDetailGroup.Controls.Add(this.OrderDetailList);
//            this.OrderDetailGroup.Controls.Add(this.InvNumbnerLbl);
//            this.OrderDetailGroup.Location = new System.Drawing.Point(1089, 171);
//            this.OrderDetailGroup.Name = "OrderDetailGroup";
//            this.OrderDetailGroup.Size = new System.Drawing.Size(747, 670);
//            this.OrderDetailGroup.TabIndex = 25;
//            this.OrderDetailGroup.TabStop = false;
//            this.OrderDetailGroup.Text = "Order Detail";
//            // 
//            // OrderDetailList
//            // 
//            this.OrderDetailList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.OrderDetailList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.OrderDetailList.Location = new System.Drawing.Point(9, 22);
//            this.OrderDetailList.Name = "OrderDetailList";
//            this.OrderDetailList.RowHeadersWidth = 51;
//            this.OrderDetailList.RowTemplate.Height = 24;
//            this.OrderDetailList.Size = new System.Drawing.Size(724, 642);
//            this.OrderDetailList.TabIndex = 0;
//            // 
//            // InvNumbnerLbl
//            // 
//            this.InvNumbnerLbl.AutoSize = true;
//            this.InvNumbnerLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            this.InvNumbnerLbl.Location = new System.Drawing.Point(117, -5);
//            this.InvNumbnerLbl.Name = "InvNumbnerLbl";
//            this.InvNumbnerLbl.Size = new System.Drawing.Size(125, 18);
//            this.InvNumbnerLbl.TabIndex = 26;
//            this.InvNumbnerLbl.Text = "Invoice Number";
//            // 
//            // OrderListGroup
//            // 
//            this.OrderListGroup.Controls.Add(this.OrderListDataGrid);
//            this.OrderListGroup.Location = new System.Drawing.Point(14, 171);
//            this.OrderListGroup.Name = "OrderListGroup";
//            this.OrderListGroup.Size = new System.Drawing.Size(1059, 670);
//            this.OrderListGroup.TabIndex = 26;
//            this.OrderListGroup.TabStop = false;
//            this.OrderListGroup.Text = "Order List";
//            // 
//            // OrdersControlUI
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.Controls.Add(this.OrderListGroup);
//            this.Controls.Add(this.OrderDetailGroup);
//            this.Controls.Add(this.InvoiceNoLbl);
//            this.Controls.Add(this.OrderIDLbl);
//            this.Controls.Add(this.PreviousPageBtn);
//            this.Controls.Add(this.NextPageBtn);
//            this.Controls.Add(this.lblStatus);
//            this.Controls.Add(this.SearchOrderTxt);
//            this.Controls.Add(this.label1);
//            this.Name = "OrdersControlUI";
//            this.Size = new System.Drawing.Size(1849, 864);
//            ((System.ComponentModel.ISupportInitialize)(this.OrderListDataGrid)).EndInit();
//            this.OrderDetailGroup.ResumeLayout(false);
//            this.OrderDetailGroup.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.OrderDetailList)).EndInit();
//            this.OrderListGroup.ResumeLayout(false);
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.Label label1;
//        private Bunifu.UI.WinForms.BunifuTextBox SearchOrderTxt;
//        private Bunifu.UI.WinForms.BunifuDataGridView OrderListDataGrid;
//        private Bunifu.UI.WinForms.BunifuImageButton PreviousPageBtn;
//        private Bunifu.UI.WinForms.BunifuImageButton NextPageBtn;
//        private System.Windows.Forms.Label lblStatus;
//        public System.Windows.Forms.Label OrderIDLbl;
//        public System.Windows.Forms.Label InvoiceNoLbl;
//        private System.Windows.Forms.GroupBox OrderDetailGroup;
//        private System.Windows.Forms.DataGridView OrderDetailList;
//        private System.Windows.Forms.Label InvNumbnerLbl;
//        private System.ComponentModel.BackgroundWorker backgroundWorker1;
//        private System.Windows.Forms.GroupBox OrderListGroup;
//    }
//}


namespace POS_Shop.Views.Controllers.Order
{
    partial class OrdersControlUI
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersControlUI));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties49 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties50 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties51 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties52 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties53 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties54 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties55 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties56 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.OrderListDataGrid = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            this.OrderIDLbl = new System.Windows.Forms.Label();
            this.InvoiceNoLbl = new System.Windows.Forms.Label();
            this.OrderDetailGroup = new System.Windows.Forms.GroupBox();
            this.OrderDetailList = new System.Windows.Forms.DataGridView();
            this.InvNumbnerLbl = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.OrderListGroup = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ResetBtn = new System.Windows.Forms.Button();
            this.SearchProductNameTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.SearchOrderTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.PreviousPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.NextPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OrderListDataGrid)).BeginInit();
            this.OrderDetailGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDetailList)).BeginInit();
            this.OrderListGroup.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // OrderListDataGrid
            // 
            this.OrderListDataGrid.AllowCustomTheming = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.White;
            this.OrderListDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.OrderListDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OrderListDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OrderListDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.OrderListDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderListDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.OrderListDataGrid.ColumnHeadersHeight = 40;
            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.OrderListDataGrid.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.OrderListDataGrid.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.OrderListDataGrid.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.OrderListDataGrid.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.OrderListDataGrid.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.OrderListDataGrid.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.OrderListDataGrid.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.OrderListDataGrid.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.OrderListDataGrid.CurrentTheme.Name = null;
            this.OrderListDataGrid.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.OrderListDataGrid.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.OrderListDataGrid.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.OrderListDataGrid.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.OrderListDataGrid.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.OrderListDataGrid.DefaultCellStyle = dataGridViewCellStyle21;
            this.OrderListDataGrid.EnableHeadersVisualStyles = false;
            this.OrderListDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.OrderListDataGrid.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.OrderListDataGrid.HeaderBgColor = System.Drawing.Color.Empty;
            this.OrderListDataGrid.HeaderForeColor = System.Drawing.Color.White;
            this.OrderListDataGrid.Location = new System.Drawing.Point(6, 21);
            this.OrderListDataGrid.Name = "OrderListDataGrid";
            this.OrderListDataGrid.RowHeadersVisible = false;
            this.OrderListDataGrid.RowHeadersWidth = 51;
            this.OrderListDataGrid.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.OrderListDataGrid.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.OrderListDataGrid.RowTemplate.Height = 40;
            this.OrderListDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrderListDataGrid.Size = new System.Drawing.Size(1047, 634);
            this.OrderListDataGrid.TabIndex = 0;
            this.OrderListDataGrid.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(98, 156);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // OrderIDLbl
            // 
            this.OrderIDLbl.AutoSize = true;
            this.OrderIDLbl.Location = new System.Drawing.Point(1048, 70);
            this.OrderIDLbl.Name = "OrderIDLbl";
            this.OrderIDLbl.Size = new System.Drawing.Size(54, 16);
            this.OrderIDLbl.TabIndex = 0;
            this.OrderIDLbl.Text = "OrderID";
            this.OrderIDLbl.Visible = false;
            // 
            // InvoiceNoLbl
            // 
            this.InvoiceNoLbl.AutoSize = true;
            this.InvoiceNoLbl.Location = new System.Drawing.Point(1048, 97);
            this.InvoiceNoLbl.Name = "InvoiceNoLbl";
            this.InvoiceNoLbl.Size = new System.Drawing.Size(68, 16);
            this.InvoiceNoLbl.TabIndex = 0;
            this.InvoiceNoLbl.Text = "InvoiceNo";
            this.InvoiceNoLbl.Visible = false;
            // 
            // OrderDetailGroup
            // 
            this.OrderDetailGroup.Controls.Add(this.OrderDetailList);
            this.OrderDetailGroup.Controls.Add(this.InvNumbnerLbl);
            this.OrderDetailGroup.Location = new System.Drawing.Point(1089, 171);
            this.OrderDetailGroup.Name = "OrderDetailGroup";
            this.OrderDetailGroup.Size = new System.Drawing.Size(747, 670);
            this.OrderDetailGroup.TabIndex = 0;
            this.OrderDetailGroup.TabStop = false;
            this.OrderDetailGroup.Text = "Order Detail";
            // 
            // OrderDetailList
            // 
            this.OrderDetailList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OrderDetailList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrderDetailList.Location = new System.Drawing.Point(9, 22);
            this.OrderDetailList.Name = "OrderDetailList";
            this.OrderDetailList.RowHeadersWidth = 51;
            this.OrderDetailList.RowTemplate.Height = 24;
            this.OrderDetailList.Size = new System.Drawing.Size(724, 642);
            this.OrderDetailList.TabIndex = 0;
            // 
            // InvNumbnerLbl
            // 
            this.InvNumbnerLbl.AutoSize = true;
            this.InvNumbnerLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InvNumbnerLbl.Location = new System.Drawing.Point(117, -5);
            this.InvNumbnerLbl.Name = "InvNumbnerLbl";
            this.InvNumbnerLbl.Size = new System.Drawing.Size(125, 18);
            this.InvNumbnerLbl.TabIndex = 0;
            this.InvNumbnerLbl.Text = "Invoice Number";
            // 
            // OrderListGroup
            // 
            this.OrderListGroup.Controls.Add(this.OrderListDataGrid);
            this.OrderListGroup.Location = new System.Drawing.Point(14, 171);
            this.OrderListGroup.Name = "OrderListGroup";
            this.OrderListGroup.Size = new System.Drawing.Size(1059, 670);
            this.OrderListGroup.TabIndex = 0;
            this.OrderListGroup.TabStop = false;
            this.OrderListGroup.Text = "Order List";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(218, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(8, 8);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(205, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(8, 8);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.ResetBtn);
            this.groupBox3.Controls.Add(this.SearchProductNameTxt);
            this.groupBox3.Controls.Add(this.SearchOrderTxt);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(20, 47);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(961, 98);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Invoice No, Customer Name, Address Or Product Name";
            // 
            // ResetBtn
            // 
            this.ResetBtn.BackColor = System.Drawing.Color.Red;
            this.ResetBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ResetBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResetBtn.ForeColor = System.Drawing.Color.White;
            this.ResetBtn.Location = new System.Drawing.Point(849, 47);
            this.ResetBtn.Name = "ResetBtn";
            this.ResetBtn.Size = new System.Drawing.Size(112, 41);
            this.ResetBtn.TabIndex = 3;
            this.ResetBtn.Text = "Reset";
            this.ResetBtn.UseVisualStyleBackColor = false;
            this.ResetBtn.Click += new System.EventHandler(this.ResetBtn_Click);
            // 
            // SearchProductNameTxt
            // 
            this.SearchProductNameTxt.AcceptsReturn = false;
            this.SearchProductNameTxt.AcceptsTab = false;
            this.SearchProductNameTxt.AnimationSpeed = 200;
            this.SearchProductNameTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.SearchProductNameTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.SearchProductNameTxt.BackColor = System.Drawing.Color.Transparent;
            this.SearchProductNameTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchProductNameTxt.BackgroundImage")));
            this.SearchProductNameTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.SearchProductNameTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SearchProductNameTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SearchProductNameTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.SearchProductNameTxt.BorderRadius = 1;
            this.SearchProductNameTxt.BorderThickness = 1;
            this.SearchProductNameTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.SearchProductNameTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchProductNameTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.SearchProductNameTxt.DefaultText = "";
            this.SearchProductNameTxt.FillColor = System.Drawing.Color.White;
            this.SearchProductNameTxt.HideSelection = true;
            this.SearchProductNameTxt.IconLeft = null;
            this.SearchProductNameTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchProductNameTxt.IconPadding = 10;
            this.SearchProductNameTxt.IconRight = null;
            this.SearchProductNameTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchProductNameTxt.Lines = new string[0];
            this.SearchProductNameTxt.Location = new System.Drawing.Point(518, 47);
            this.SearchProductNameTxt.MaxLength = 32767;
            this.SearchProductNameTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.SearchProductNameTxt.Modified = false;
            this.SearchProductNameTxt.Multiline = false;
            this.SearchProductNameTxt.Name = "SearchProductNameTxt";
            stateProperties49.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties49.FillColor = System.Drawing.Color.Empty;
            stateProperties49.ForeColor = System.Drawing.Color.Empty;
            stateProperties49.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchProductNameTxt.OnActiveState = stateProperties49;
            stateProperties50.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties50.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties50.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties50.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.SearchProductNameTxt.OnDisabledState = stateProperties50;
            stateProperties51.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties51.FillColor = System.Drawing.Color.Empty;
            stateProperties51.ForeColor = System.Drawing.Color.Empty;
            stateProperties51.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchProductNameTxt.OnHoverState = stateProperties51;
            stateProperties52.BorderColor = System.Drawing.Color.Silver;
            stateProperties52.FillColor = System.Drawing.Color.White;
            stateProperties52.ForeColor = System.Drawing.Color.Empty;
            stateProperties52.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchProductNameTxt.OnIdleState = stateProperties52;
            this.SearchProductNameTxt.Padding = new System.Windows.Forms.Padding(3);
            this.SearchProductNameTxt.PasswordChar = '\0';
            this.SearchProductNameTxt.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.SearchProductNameTxt.PlaceholderText = "Search Product Name";
            this.SearchProductNameTxt.ReadOnly = false;
            this.SearchProductNameTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SearchProductNameTxt.SelectedText = "";
            this.SearchProductNameTxt.SelectionLength = 0;
            this.SearchProductNameTxt.SelectionStart = 0;
            this.SearchProductNameTxt.ShortcutsEnabled = true;
            this.SearchProductNameTxt.Size = new System.Drawing.Size(325, 41);
            this.SearchProductNameTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.SearchProductNameTxt.TabIndex = 2;
            this.SearchProductNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SearchProductNameTxt.TextMarginBottom = 0;
            this.SearchProductNameTxt.TextMarginLeft = 3;
            this.SearchProductNameTxt.TextMarginTop = 0;
            this.SearchProductNameTxt.TextPlaceholder = "Search Product Name";
            this.SearchProductNameTxt.UseSystemPasswordChar = false;
            this.SearchProductNameTxt.WordWrap = true;
            this.SearchProductNameTxt.TextChange += new System.EventHandler(this.SearchProductNameTxt_TextChange);
            this.SearchProductNameTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchProductNameTxt_KeyDown);
            // 
            // SearchOrderTxt
            // 
            this.SearchOrderTxt.AcceptsReturn = false;
            this.SearchOrderTxt.AcceptsTab = false;
            this.SearchOrderTxt.AnimationSpeed = 200;
            this.SearchOrderTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.SearchOrderTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.SearchOrderTxt.BackColor = System.Drawing.Color.Transparent;
            this.SearchOrderTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SearchOrderTxt.BackgroundImage")));
            this.SearchOrderTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.SearchOrderTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SearchOrderTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SearchOrderTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.SearchOrderTxt.BorderRadius = 1;
            this.SearchOrderTxt.BorderThickness = 1;
            this.SearchOrderTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.SearchOrderTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchOrderTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.SearchOrderTxt.DefaultText = "";
            this.SearchOrderTxt.FillColor = System.Drawing.Color.White;
            this.SearchOrderTxt.HideSelection = true;
            this.SearchOrderTxt.IconLeft = null;
            this.SearchOrderTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchOrderTxt.IconPadding = 10;
            this.SearchOrderTxt.IconRight = null;
            this.SearchOrderTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.SearchOrderTxt.Lines = new string[0];
            this.SearchOrderTxt.Location = new System.Drawing.Point(6, 48);
            this.SearchOrderTxt.MaxLength = 32767;
            this.SearchOrderTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.SearchOrderTxt.Modified = false;
            this.SearchOrderTxt.Multiline = false;
            this.SearchOrderTxt.Name = "SearchOrderTxt";
            stateProperties53.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties53.FillColor = System.Drawing.Color.Empty;
            stateProperties53.ForeColor = System.Drawing.Color.Empty;
            stateProperties53.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchOrderTxt.OnActiveState = stateProperties53;
            stateProperties54.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties54.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties54.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties54.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.SearchOrderTxt.OnDisabledState = stateProperties54;
            stateProperties55.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties55.FillColor = System.Drawing.Color.Empty;
            stateProperties55.ForeColor = System.Drawing.Color.Empty;
            stateProperties55.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchOrderTxt.OnHoverState = stateProperties55;
            stateProperties56.BorderColor = System.Drawing.Color.Silver;
            stateProperties56.FillColor = System.Drawing.Color.White;
            stateProperties56.ForeColor = System.Drawing.Color.Empty;
            stateProperties56.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchOrderTxt.OnIdleState = stateProperties56;
            this.SearchOrderTxt.Padding = new System.Windows.Forms.Padding(3);
            this.SearchOrderTxt.PasswordChar = '\0';
            this.SearchOrderTxt.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.SearchOrderTxt.PlaceholderText = "Search Invoice No, Customer Name and Address";
            this.SearchOrderTxt.ReadOnly = false;
            this.SearchOrderTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SearchOrderTxt.SelectedText = "";
            this.SearchOrderTxt.SelectionLength = 0;
            this.SearchOrderTxt.SelectionStart = 0;
            this.SearchOrderTxt.ShortcutsEnabled = true;
            this.SearchOrderTxt.Size = new System.Drawing.Size(500, 41);
            this.SearchOrderTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.SearchOrderTxt.TabIndex = 1;
            this.SearchOrderTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SearchOrderTxt.TextMarginBottom = 0;
            this.SearchOrderTxt.TextMarginLeft = 3;
            this.SearchOrderTxt.TextMarginTop = 0;
            this.SearchOrderTxt.TextPlaceholder = "Search Invoice No, Customer Name and Address";
            this.SearchOrderTxt.UseSystemPasswordChar = false;
            this.SearchOrderTxt.WordWrap = true;
            this.SearchOrderTxt.TextChange += new System.EventHandler(this.SearchOrderTxt_TextChange);
            this.SearchOrderTxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchOrderTxt_KeyDown);
            // 
            // PreviousPageBtn
            // 
            this.PreviousPageBtn.ActiveImage = null;
            this.PreviousPageBtn.AllowAnimations = true;
            this.PreviousPageBtn.AllowBuffering = false;
            this.PreviousPageBtn.AllowToggling = false;
            this.PreviousPageBtn.AllowZooming = false;
            this.PreviousPageBtn.AllowZoomingOnFocus = false;
            this.PreviousPageBtn.BackColor = System.Drawing.Color.Transparent;
            this.PreviousPageBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PreviousPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.PreviousPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.ErrorImage")));
            this.PreviousPageBtn.FadeWhenInactive = false;
            this.PreviousPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.PreviousPageBtn.Image = global::POS_Shop.Properties.Resources.iconPrev;
            this.PreviousPageBtn.ImageActive = null;
            this.PreviousPageBtn.ImageLocation = null;
            this.PreviousPageBtn.ImageMargin = 2;
            this.PreviousPageBtn.ImageSize = new System.Drawing.Size(33, 36);
            this.PreviousPageBtn.ImageZoomSize = new System.Drawing.Size(35, 38);
            this.PreviousPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("PreviousPageBtn.InitialImage")));
            this.PreviousPageBtn.Location = new System.Drawing.Point(987, 130);
            this.PreviousPageBtn.Name = "PreviousPageBtn";
            this.PreviousPageBtn.Rotation = 0;
            this.PreviousPageBtn.ShowActiveImage = true;
            this.PreviousPageBtn.ShowCursorChanges = true;
            this.PreviousPageBtn.ShowImageBorders = true;
            this.PreviousPageBtn.ShowSizeMarkers = false;
            this.PreviousPageBtn.Size = new System.Drawing.Size(35, 38);
            this.PreviousPageBtn.TabIndex = 0;
            this.PreviousPageBtn.ToolTipText = "";
            this.PreviousPageBtn.WaitOnLoad = false;
            this.PreviousPageBtn.Zoom = 2;
            this.PreviousPageBtn.ZoomSpeed = 10;
            this.PreviousPageBtn.Click += new System.EventHandler(this.PreviousPageBtn_Click);
            // 
            // NextPageBtn
            // 
            this.NextPageBtn.ActiveImage = null;
            this.NextPageBtn.AllowAnimations = true;
            this.NextPageBtn.AllowBuffering = false;
            this.NextPageBtn.AllowToggling = false;
            this.NextPageBtn.AllowZooming = false;
            this.NextPageBtn.AllowZoomingOnFocus = false;
            this.NextPageBtn.BackColor = System.Drawing.Color.Transparent;
            this.NextPageBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NextPageBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.NextPageBtn.ErrorImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.ErrorImage")));
            this.NextPageBtn.FadeWhenInactive = false;
            this.NextPageBtn.Flip = Bunifu.UI.WinForms.BunifuImageButton.FlipOrientation.Normal;
            this.NextPageBtn.Image = global::POS_Shop.Properties.Resources.iconNext;
            this.NextPageBtn.ImageActive = null;
            this.NextPageBtn.ImageLocation = null;
            this.NextPageBtn.ImageMargin = 2;
            this.NextPageBtn.ImageSize = new System.Drawing.Size(33, 36);
            this.NextPageBtn.ImageZoomSize = new System.Drawing.Size(35, 38);
            this.NextPageBtn.InitialImage = ((System.Drawing.Image)(resources.GetObject("NextPageBtn.InitialImage")));
            this.NextPageBtn.Location = new System.Drawing.Point(1028, 130);
            this.NextPageBtn.Name = "NextPageBtn";
            this.NextPageBtn.Rotation = 0;
            this.NextPageBtn.ShowActiveImage = true;
            this.NextPageBtn.ShowCursorChanges = true;
            this.NextPageBtn.ShowImageBorders = true;
            this.NextPageBtn.ShowSizeMarkers = false;
            this.NextPageBtn.Size = new System.Drawing.Size(35, 38);
            this.NextPageBtn.TabIndex = 21;
            this.NextPageBtn.ToolTipText = "";
            this.NextPageBtn.WaitOnLoad = false;
            this.NextPageBtn.Zoom = 2;
            this.NextPageBtn.ZoomSpeed = 10;
            this.NextPageBtn.Click += new System.EventHandler(this.NextPageBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Esc to Focus";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.label2.Location = new System.Drawing.Point(523, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "F1 to Focus";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            this.label3.Location = new System.Drawing.Point(856, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ctrl+R";
            // 
            // OrdersControlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.OrderListGroup);
            this.Controls.Add(this.OrderDetailGroup);
            this.Controls.Add(this.InvoiceNoLbl);
            this.Controls.Add(this.OrderIDLbl);
            this.Controls.Add(this.PreviousPageBtn);
            this.Controls.Add(this.NextPageBtn);
            this.Controls.Add(this.lblStatus);
            this.Name = "OrdersControlUI";
            this.Size = new System.Drawing.Size(1849, 864);
            ((System.ComponentModel.ISupportInitialize)(this.OrderListDataGrid)).EndInit();
            this.OrderDetailGroup.ResumeLayout(false);
            this.OrderDetailGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderDetailList)).EndInit();
            this.OrderListGroup.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Bunifu.UI.WinForms.BunifuTextBox SearchOrderTxt;
        private Bunifu.UI.WinForms.BunifuDataGridView OrderListDataGrid;
        private Bunifu.UI.WinForms.BunifuImageButton PreviousPageBtn;
        private Bunifu.UI.WinForms.BunifuImageButton NextPageBtn;
        private System.Windows.Forms.Label lblStatus;
        public System.Windows.Forms.Label OrderIDLbl;
        public System.Windows.Forms.Label InvoiceNoLbl;
        private System.Windows.Forms.GroupBox OrderDetailGroup;
        private System.Windows.Forms.DataGridView OrderDetailList;
        private System.Windows.Forms.Label InvNumbnerLbl;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox OrderListGroup;
        private Bunifu.UI.WinForms.BunifuTextBox SearchProductNameTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ResetBtn;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
    }
}

