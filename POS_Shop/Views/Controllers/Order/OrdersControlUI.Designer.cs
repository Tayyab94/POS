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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OrdersControlUI));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchOrderTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.OrderListDataGrid = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.PreviousPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.NextPageBtn = new Bunifu.UI.WinForms.BunifuImageButton();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OrderListDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(130, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Search Order By Invoice Number";
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
            this.SearchOrderTxt.Location = new System.Drawing.Point(59, 89);
            this.SearchOrderTxt.MaxLength = 32767;
            this.SearchOrderTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.SearchOrderTxt.Modified = false;
            this.SearchOrderTxt.Multiline = false;
            this.SearchOrderTxt.Name = "SearchOrderTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchOrderTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.SearchOrderTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchOrderTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.SearchOrderTxt.OnIdleState = stateProperties4;
            this.SearchOrderTxt.Padding = new System.Windows.Forms.Padding(3);
            this.SearchOrderTxt.PasswordChar = '\0';
            this.SearchOrderTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.SearchOrderTxt.PlaceholderText = "Search Order";
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
            this.SearchOrderTxt.TextPlaceholder = "Search Order";
            this.SearchOrderTxt.UseSystemPasswordChar = false;
            this.SearchOrderTxt.WordWrap = true;
            this.SearchOrderTxt.TextChange += new System.EventHandler(this.SearchOrderTxt_TextChange);
            // 
            // OrderListDataGrid
            // 
            this.OrderListDataGrid.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.OrderListDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.OrderListDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OrderListDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.OrderListDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.OrderListDataGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.OrderListDataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.OrderListDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.OrderListDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.OrderListDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.OrderListDataGrid.EnableHeadersVisualStyles = false;
            this.OrderListDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.OrderListDataGrid.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.OrderListDataGrid.HeaderBgColor = System.Drawing.Color.Empty;
            this.OrderListDataGrid.HeaderForeColor = System.Drawing.Color.White;
            this.OrderListDataGrid.Location = new System.Drawing.Point(15, 171);
            this.OrderListDataGrid.Name = "OrderListDataGrid";
            this.OrderListDataGrid.RowHeadersVisible = false;
            this.OrderListDataGrid.RowHeadersWidth = 51;
            this.OrderListDataGrid.RowTemplate.Height = 40;
            this.OrderListDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OrderListDataGrid.Size = new System.Drawing.Size(663, 402);
            this.OrderListDataGrid.TabIndex = 2;
            this.OrderListDataGrid.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            // 
            // PreviousPageBtn
            // 
            this.PreviousPageBtn.ActiveImage = null;
            this.PreviousPageBtn.AllowAnimations = true;
            this.PreviousPageBtn.AllowBuffering = false;
            this.PreviousPageBtn.AllowToggling = false;
            this.PreviousPageBtn.AllowZooming = false;
            this.PreviousPageBtn.AllowZoomingOnFocus = false;
            this.PreviousPageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PreviousPageBtn.BackColor = System.Drawing.Color.Transparent;
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
            this.PreviousPageBtn.Location = new System.Drawing.Point(592, 127);
            this.PreviousPageBtn.Name = "PreviousPageBtn";
            this.PreviousPageBtn.Rotation = 0;
            this.PreviousPageBtn.ShowActiveImage = true;
            this.PreviousPageBtn.ShowCursorChanges = true;
            this.PreviousPageBtn.ShowImageBorders = true;
            this.PreviousPageBtn.ShowSizeMarkers = false;
            this.PreviousPageBtn.Size = new System.Drawing.Size(35, 38);
            this.PreviousPageBtn.TabIndex = 22;
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
            this.NextPageBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.NextPageBtn.BackColor = System.Drawing.Color.Transparent;
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
            this.NextPageBtn.Location = new System.Drawing.Point(632, 127);
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
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(443, 149);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 20;
            this.lblStatus.Text = "Status";
            // 
            // OrdersControlUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PreviousPageBtn);
            this.Controls.Add(this.NextPageBtn);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.OrderListDataGrid);
            this.Controls.Add(this.SearchOrderTxt);
            this.Controls.Add(this.label1);
            this.Name = "OrdersControlUI";
            this.Size = new System.Drawing.Size(696, 594);
            ((System.ComponentModel.ISupportInitialize)(this.OrderListDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Bunifu.UI.WinForms.BunifuTextBox SearchOrderTxt;
        private Bunifu.UI.WinForms.BunifuDataGridView OrderListDataGrid;
        private Bunifu.UI.WinForms.BunifuImageButton PreviousPageBtn;
        private Bunifu.UI.WinForms.BunifuImageButton NextPageBtn;
        private System.Windows.Forms.Label lblStatus;
    }
}
