namespace POS_Shop.Views.Controllers.Product
{
    partial class ProductUnitControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductUnitControl));
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges3 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ProductUnitDatagridView = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProdUnitActiveChkBox = new System.Windows.Forms.CheckBox();
            this.ProdUnitAbbreviationTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RemoveProductUnitBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.productUnitIdTxt = new System.Windows.Forms.TextBox();
            this.updateProductIUnitBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.SaveProdUnitBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.ProdUnitNameTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.categoryNameLbl = new System.Windows.Forms.Label();
            this.ProductUnitHeatingLbl = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductUnitDatagridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.ProductUnitDatagridView);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(21, 214);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1159, 402);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Units List";
            // 
            // ProductUnitDatagridView
            // 
            this.ProductUnitDatagridView.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.ProductUnitDatagridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProductUnitDatagridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProductUnitDatagridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ProductUnitDatagridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ProductUnitDatagridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProductUnitDatagridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ProductUnitDatagridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ProductUnitDatagridView.ColumnHeadersHeight = 40;
            this.ProductUnitDatagridView.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.ProductUnitDatagridView.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.ProductUnitDatagridView.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.ProductUnitDatagridView.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.ProductUnitDatagridView.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.ProductUnitDatagridView.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.ProductUnitDatagridView.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.ProductUnitDatagridView.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.ProductUnitDatagridView.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.ProductUnitDatagridView.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.ProductUnitDatagridView.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.ProductUnitDatagridView.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.ProductUnitDatagridView.CurrentTheme.Name = null;
            this.ProductUnitDatagridView.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.ProductUnitDatagridView.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.ProductUnitDatagridView.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.ProductUnitDatagridView.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.ProductUnitDatagridView.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ProductUnitDatagridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductUnitDatagridView.EnableHeadersVisualStyles = false;
            this.ProductUnitDatagridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.ProductUnitDatagridView.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.ProductUnitDatagridView.HeaderBgColor = System.Drawing.Color.Empty;
            this.ProductUnitDatagridView.HeaderForeColor = System.Drawing.Color.White;
            this.ProductUnitDatagridView.Location = new System.Drawing.Point(19, 27);
            this.ProductUnitDatagridView.Name = "ProductUnitDatagridView";
            this.ProductUnitDatagridView.RowHeadersVisible = false;
            this.ProductUnitDatagridView.RowHeadersWidth = 51;
            this.ProductUnitDatagridView.RowTemplate.Height = 40;
            this.ProductUnitDatagridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProductUnitDatagridView.Size = new System.Drawing.Size(1134, 359);
            this.ProductUnitDatagridView.TabIndex = 4;
            this.ProductUnitDatagridView.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.ProductUnitDatagridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProductUnitDatagridView_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.ProdUnitActiveChkBox);
            this.groupBox1.Controls.Add(this.ProdUnitAbbreviationTxt);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.RemoveProductUnitBtn);
            this.groupBox1.Controls.Add(this.productUnitIdTxt);
            this.groupBox1.Controls.Add(this.updateProductIUnitBtn);
            this.groupBox1.Controls.Add(this.SaveProdUnitBtn);
            this.groupBox1.Controls.Add(this.ProdUnitNameTxt);
            this.groupBox1.Controls.Add(this.categoryNameLbl);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(21, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(987, 165);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Unit Form";
            // 
            // ProdUnitActiveChkBox
            // 
            this.ProdUnitActiveChkBox.AutoSize = true;
            this.ProdUnitActiveChkBox.Checked = true;
            this.ProdUnitActiveChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ProdUnitActiveChkBox.Location = new System.Drawing.Point(865, 73);
            this.ProdUnitActiveChkBox.Name = "ProdUnitActiveChkBox";
            this.ProdUnitActiveChkBox.Size = new System.Drawing.Size(81, 26);
            this.ProdUnitActiveChkBox.TabIndex = 8;
            this.ProdUnitActiveChkBox.Text = "Active";
            this.ProdUnitActiveChkBox.UseVisualStyleBackColor = true;
            // 
            // ProdUnitAbbreviationTxt
            // 
            this.ProdUnitAbbreviationTxt.AcceptsReturn = false;
            this.ProdUnitAbbreviationTxt.AcceptsTab = false;
            this.ProdUnitAbbreviationTxt.AnimationSpeed = 200;
            this.ProdUnitAbbreviationTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.ProdUnitAbbreviationTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.ProdUnitAbbreviationTxt.BackColor = System.Drawing.Color.Transparent;
            this.ProdUnitAbbreviationTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ProdUnitAbbreviationTxt.BackgroundImage")));
            this.ProdUnitAbbreviationTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.ProdUnitAbbreviationTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ProdUnitAbbreviationTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.ProdUnitAbbreviationTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.ProdUnitAbbreviationTxt.BorderRadius = 1;
            this.ProdUnitAbbreviationTxt.BorderThickness = 1;
            this.ProdUnitAbbreviationTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.ProdUnitAbbreviationTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdUnitAbbreviationTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.ProdUnitAbbreviationTxt.DefaultText = "";
            this.ProdUnitAbbreviationTxt.FillColor = System.Drawing.Color.White;
            this.ProdUnitAbbreviationTxt.HideSelection = true;
            this.ProdUnitAbbreviationTxt.IconLeft = null;
            this.ProdUnitAbbreviationTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdUnitAbbreviationTxt.IconPadding = 10;
            this.ProdUnitAbbreviationTxt.IconRight = null;
            this.ProdUnitAbbreviationTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdUnitAbbreviationTxt.Lines = new string[0];
            this.ProdUnitAbbreviationTxt.Location = new System.Drawing.Point(416, 58);
            this.ProdUnitAbbreviationTxt.MaxLength = 32767;
            this.ProdUnitAbbreviationTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.ProdUnitAbbreviationTxt.Modified = false;
            this.ProdUnitAbbreviationTxt.Multiline = false;
            this.ProdUnitAbbreviationTxt.Name = "ProdUnitAbbreviationTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdUnitAbbreviationTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.ProdUnitAbbreviationTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdUnitAbbreviationTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdUnitAbbreviationTxt.OnIdleState = stateProperties4;
            this.ProdUnitAbbreviationTxt.Padding = new System.Windows.Forms.Padding(3);
            this.ProdUnitAbbreviationTxt.PasswordChar = '\0';
            this.ProdUnitAbbreviationTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.ProdUnitAbbreviationTxt.PlaceholderText = "Enter text";
            this.ProdUnitAbbreviationTxt.ReadOnly = false;
            this.ProdUnitAbbreviationTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ProdUnitAbbreviationTxt.SelectedText = "";
            this.ProdUnitAbbreviationTxt.SelectionLength = 0;
            this.ProdUnitAbbreviationTxt.SelectionStart = 0;
            this.ProdUnitAbbreviationTxt.ShortcutsEnabled = true;
            this.ProdUnitAbbreviationTxt.Size = new System.Drawing.Size(417, 41);
            this.ProdUnitAbbreviationTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.ProdUnitAbbreviationTxt.TabIndex = 7;
            this.ProdUnitAbbreviationTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ProdUnitAbbreviationTxt.TextMarginBottom = 0;
            this.ProdUnitAbbreviationTxt.TextMarginLeft = 3;
            this.ProdUnitAbbreviationTxt.TextMarginTop = 0;
            this.ProdUnitAbbreviationTxt.TextPlaceholder = "Enter text";
            this.ProdUnitAbbreviationTxt.UseSystemPasswordChar = false;
            this.ProdUnitAbbreviationTxt.WordWrap = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(413, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Abbrevation";
            // 
            // RemoveProductUnitBtn
            // 
            this.RemoveProductUnitBtn.AllowAnimations = true;
            this.RemoveProductUnitBtn.AllowMouseEffects = true;
            this.RemoveProductUnitBtn.AllowToggling = false;
            this.RemoveProductUnitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveProductUnitBtn.AnimationSpeed = 200;
            this.RemoveProductUnitBtn.AutoGenerateColors = false;
            this.RemoveProductUnitBtn.AutoRoundBorders = false;
            this.RemoveProductUnitBtn.AutoSizeLeftIcon = true;
            this.RemoveProductUnitBtn.AutoSizeRightIcon = true;
            this.RemoveProductUnitBtn.BackColor = System.Drawing.Color.Transparent;
            this.RemoveProductUnitBtn.BackColor1 = System.Drawing.Color.Red;
            this.RemoveProductUnitBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RemoveProductUnitBtn.BackgroundImage")));
            this.RemoveProductUnitBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveProductUnitBtn.ButtonText = "Delete";
            this.RemoveProductUnitBtn.ButtonTextMarginLeft = 0;
            this.RemoveProductUnitBtn.ColorContrastOnClick = 45;
            this.RemoveProductUnitBtn.ColorContrastOnHover = 45;
            this.RemoveProductUnitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.RemoveProductUnitBtn.CustomizableEdges = borderEdges1;
            this.RemoveProductUnitBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.RemoveProductUnitBtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.RemoveProductUnitBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.RemoveProductUnitBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.RemoveProductUnitBtn.Enabled = false;
            this.RemoveProductUnitBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.RemoveProductUnitBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.RemoveProductUnitBtn.ForeColor = System.Drawing.Color.White;
            this.RemoveProductUnitBtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RemoveProductUnitBtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.RemoveProductUnitBtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.RemoveProductUnitBtn.IconMarginLeft = 11;
            this.RemoveProductUnitBtn.IconPadding = 10;
            this.RemoveProductUnitBtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RemoveProductUnitBtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.RemoveProductUnitBtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.RemoveProductUnitBtn.IconSize = 25;
            this.RemoveProductUnitBtn.IdleBorderColor = System.Drawing.Color.CornflowerBlue;
            this.RemoveProductUnitBtn.IdleBorderRadius = 30;
            this.RemoveProductUnitBtn.IdleBorderThickness = 1;
            this.RemoveProductUnitBtn.IdleFillColor = System.Drawing.Color.Red;
            this.RemoveProductUnitBtn.IdleIconLeftImage = null;
            this.RemoveProductUnitBtn.IdleIconRightImage = null;
            this.RemoveProductUnitBtn.IndicateFocus = false;
            this.RemoveProductUnitBtn.Location = new System.Drawing.Point(694, 122);
            this.RemoveProductUnitBtn.Name = "RemoveProductUnitBtn";
            this.RemoveProductUnitBtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.RemoveProductUnitBtn.OnDisabledState.BorderRadius = 30;
            this.RemoveProductUnitBtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveProductUnitBtn.OnDisabledState.BorderThickness = 1;
            this.RemoveProductUnitBtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.RemoveProductUnitBtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.RemoveProductUnitBtn.OnDisabledState.IconLeftImage = null;
            this.RemoveProductUnitBtn.OnDisabledState.IconRightImage = null;
            this.RemoveProductUnitBtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.RemoveProductUnitBtn.onHoverState.BorderRadius = 30;
            this.RemoveProductUnitBtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveProductUnitBtn.onHoverState.BorderThickness = 1;
            this.RemoveProductUnitBtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.RemoveProductUnitBtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.RemoveProductUnitBtn.onHoverState.IconLeftImage = null;
            this.RemoveProductUnitBtn.onHoverState.IconRightImage = null;
            this.RemoveProductUnitBtn.OnIdleState.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.RemoveProductUnitBtn.OnIdleState.BorderRadius = 30;
            this.RemoveProductUnitBtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveProductUnitBtn.OnIdleState.BorderThickness = 1;
            this.RemoveProductUnitBtn.OnIdleState.FillColor = System.Drawing.Color.Red;
            this.RemoveProductUnitBtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.RemoveProductUnitBtn.OnIdleState.IconLeftImage = null;
            this.RemoveProductUnitBtn.OnIdleState.IconRightImage = null;
            this.RemoveProductUnitBtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.RemoveProductUnitBtn.OnPressedState.BorderRadius = 30;
            this.RemoveProductUnitBtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveProductUnitBtn.OnPressedState.BorderThickness = 1;
            this.RemoveProductUnitBtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.RemoveProductUnitBtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.RemoveProductUnitBtn.OnPressedState.IconLeftImage = null;
            this.RemoveProductUnitBtn.OnPressedState.IconRightImage = null;
            this.RemoveProductUnitBtn.Size = new System.Drawing.Size(76, 37);
            this.RemoveProductUnitBtn.TabIndex = 5;
            this.RemoveProductUnitBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RemoveProductUnitBtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.RemoveProductUnitBtn.TextMarginLeft = 0;
            this.RemoveProductUnitBtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.RemoveProductUnitBtn.UseDefaultRadiusAndThickness = true;
            this.RemoveProductUnitBtn.Visible = false;
            this.RemoveProductUnitBtn.Click += new System.EventHandler(this.RemoveProductUnitBtn_Click);
            // 
            // productUnitIdTxt
            // 
            this.productUnitIdTxt.Location = new System.Drawing.Point(109, 27);
            this.productUnitIdTxt.Name = "productUnitIdTxt";
            this.productUnitIdTxt.Size = new System.Drawing.Size(26, 28);
            this.productUnitIdTxt.TabIndex = 4;
            this.productUnitIdTxt.Visible = false;
            // 
            // updateProductIUnitBtn
            // 
            this.updateProductIUnitBtn.AllowAnimations = true;
            this.updateProductIUnitBtn.AllowMouseEffects = true;
            this.updateProductIUnitBtn.AllowToggling = false;
            this.updateProductIUnitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateProductIUnitBtn.AnimationSpeed = 200;
            this.updateProductIUnitBtn.AutoGenerateColors = false;
            this.updateProductIUnitBtn.AutoRoundBorders = false;
            this.updateProductIUnitBtn.AutoSizeLeftIcon = true;
            this.updateProductIUnitBtn.AutoSizeRightIcon = true;
            this.updateProductIUnitBtn.BackColor = System.Drawing.Color.Transparent;
            this.updateProductIUnitBtn.BackColor1 = System.Drawing.Color.MediumSlateBlue;
            this.updateProductIUnitBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("updateProductIUnitBtn.BackgroundImage")));
            this.updateProductIUnitBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateProductIUnitBtn.ButtonText = "Update";
            this.updateProductIUnitBtn.ButtonTextMarginLeft = 0;
            this.updateProductIUnitBtn.ColorContrastOnClick = 45;
            this.updateProductIUnitBtn.ColorContrastOnHover = 45;
            this.updateProductIUnitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.updateProductIUnitBtn.CustomizableEdges = borderEdges2;
            this.updateProductIUnitBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.updateProductIUnitBtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.updateProductIUnitBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.updateProductIUnitBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.updateProductIUnitBtn.Enabled = false;
            this.updateProductIUnitBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.updateProductIUnitBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.updateProductIUnitBtn.ForeColor = System.Drawing.Color.White;
            this.updateProductIUnitBtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.updateProductIUnitBtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.updateProductIUnitBtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.updateProductIUnitBtn.IconMarginLeft = 11;
            this.updateProductIUnitBtn.IconPadding = 10;
            this.updateProductIUnitBtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updateProductIUnitBtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.updateProductIUnitBtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.updateProductIUnitBtn.IconSize = 25;
            this.updateProductIUnitBtn.IdleBorderColor = System.Drawing.Color.CornflowerBlue;
            this.updateProductIUnitBtn.IdleBorderRadius = 30;
            this.updateProductIUnitBtn.IdleBorderThickness = 1;
            this.updateProductIUnitBtn.IdleFillColor = System.Drawing.Color.MediumSlateBlue;
            this.updateProductIUnitBtn.IdleIconLeftImage = null;
            this.updateProductIUnitBtn.IdleIconRightImage = null;
            this.updateProductIUnitBtn.IndicateFocus = false;
            this.updateProductIUnitBtn.Location = new System.Drawing.Point(776, 122);
            this.updateProductIUnitBtn.Name = "updateProductIUnitBtn";
            this.updateProductIUnitBtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.updateProductIUnitBtn.OnDisabledState.BorderRadius = 30;
            this.updateProductIUnitBtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateProductIUnitBtn.OnDisabledState.BorderThickness = 1;
            this.updateProductIUnitBtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.updateProductIUnitBtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.updateProductIUnitBtn.OnDisabledState.IconLeftImage = null;
            this.updateProductIUnitBtn.OnDisabledState.IconRightImage = null;
            this.updateProductIUnitBtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.updateProductIUnitBtn.onHoverState.BorderRadius = 30;
            this.updateProductIUnitBtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateProductIUnitBtn.onHoverState.BorderThickness = 1;
            this.updateProductIUnitBtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.updateProductIUnitBtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.updateProductIUnitBtn.onHoverState.IconLeftImage = null;
            this.updateProductIUnitBtn.onHoverState.IconRightImage = null;
            this.updateProductIUnitBtn.OnIdleState.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.updateProductIUnitBtn.OnIdleState.BorderRadius = 30;
            this.updateProductIUnitBtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateProductIUnitBtn.OnIdleState.BorderThickness = 1;
            this.updateProductIUnitBtn.OnIdleState.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.updateProductIUnitBtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.updateProductIUnitBtn.OnIdleState.IconLeftImage = null;
            this.updateProductIUnitBtn.OnIdleState.IconRightImage = null;
            this.updateProductIUnitBtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.updateProductIUnitBtn.OnPressedState.BorderRadius = 30;
            this.updateProductIUnitBtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateProductIUnitBtn.OnPressedState.BorderThickness = 1;
            this.updateProductIUnitBtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.updateProductIUnitBtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.updateProductIUnitBtn.OnPressedState.IconLeftImage = null;
            this.updateProductIUnitBtn.OnPressedState.IconRightImage = null;
            this.updateProductIUnitBtn.Size = new System.Drawing.Size(83, 37);
            this.updateProductIUnitBtn.TabIndex = 3;
            this.updateProductIUnitBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.updateProductIUnitBtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.updateProductIUnitBtn.TextMarginLeft = 0;
            this.updateProductIUnitBtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.updateProductIUnitBtn.UseDefaultRadiusAndThickness = true;
            this.updateProductIUnitBtn.Click += new System.EventHandler(this.updateProductIUnitBtn_Click);
            // 
            // SaveProdUnitBtn
            // 
            this.SaveProdUnitBtn.AllowAnimations = true;
            this.SaveProdUnitBtn.AllowMouseEffects = true;
            this.SaveProdUnitBtn.AllowToggling = false;
            this.SaveProdUnitBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveProdUnitBtn.AnimationSpeed = 200;
            this.SaveProdUnitBtn.AutoGenerateColors = false;
            this.SaveProdUnitBtn.AutoRoundBorders = false;
            this.SaveProdUnitBtn.AutoSizeLeftIcon = true;
            this.SaveProdUnitBtn.AutoSizeRightIcon = true;
            this.SaveProdUnitBtn.BackColor = System.Drawing.Color.Transparent;
            this.SaveProdUnitBtn.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.SaveProdUnitBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveProdUnitBtn.BackgroundImage")));
            this.SaveProdUnitBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveProdUnitBtn.ButtonText = "Save";
            this.SaveProdUnitBtn.ButtonTextMarginLeft = 0;
            this.SaveProdUnitBtn.ColorContrastOnClick = 45;
            this.SaveProdUnitBtn.ColorContrastOnHover = 45;
            this.SaveProdUnitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges3.BottomLeft = true;
            borderEdges3.BottomRight = true;
            borderEdges3.TopLeft = true;
            borderEdges3.TopRight = true;
            this.SaveProdUnitBtn.CustomizableEdges = borderEdges3;
            this.SaveProdUnitBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.SaveProdUnitBtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.SaveProdUnitBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SaveProdUnitBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.SaveProdUnitBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.SaveProdUnitBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SaveProdUnitBtn.ForeColor = System.Drawing.Color.White;
            this.SaveProdUnitBtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveProdUnitBtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.SaveProdUnitBtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.SaveProdUnitBtn.IconMarginLeft = 11;
            this.SaveProdUnitBtn.IconPadding = 10;
            this.SaveProdUnitBtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveProdUnitBtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.SaveProdUnitBtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.SaveProdUnitBtn.IconSize = 25;
            this.SaveProdUnitBtn.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.SaveProdUnitBtn.IdleBorderRadius = 30;
            this.SaveProdUnitBtn.IdleBorderThickness = 1;
            this.SaveProdUnitBtn.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.SaveProdUnitBtn.IdleIconLeftImage = null;
            this.SaveProdUnitBtn.IdleIconRightImage = null;
            this.SaveProdUnitBtn.IndicateFocus = false;
            this.SaveProdUnitBtn.Location = new System.Drawing.Point(865, 122);
            this.SaveProdUnitBtn.Name = "SaveProdUnitBtn";
            this.SaveProdUnitBtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.SaveProdUnitBtn.OnDisabledState.BorderRadius = 30;
            this.SaveProdUnitBtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveProdUnitBtn.OnDisabledState.BorderThickness = 1;
            this.SaveProdUnitBtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SaveProdUnitBtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.SaveProdUnitBtn.OnDisabledState.IconLeftImage = null;
            this.SaveProdUnitBtn.OnDisabledState.IconRightImage = null;
            this.SaveProdUnitBtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SaveProdUnitBtn.onHoverState.BorderRadius = 30;
            this.SaveProdUnitBtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveProdUnitBtn.onHoverState.BorderThickness = 1;
            this.SaveProdUnitBtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SaveProdUnitBtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.SaveProdUnitBtn.onHoverState.IconLeftImage = null;
            this.SaveProdUnitBtn.onHoverState.IconRightImage = null;
            this.SaveProdUnitBtn.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.SaveProdUnitBtn.OnIdleState.BorderRadius = 30;
            this.SaveProdUnitBtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveProdUnitBtn.OnIdleState.BorderThickness = 1;
            this.SaveProdUnitBtn.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.SaveProdUnitBtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.SaveProdUnitBtn.OnIdleState.IconLeftImage = null;
            this.SaveProdUnitBtn.OnIdleState.IconRightImage = null;
            this.SaveProdUnitBtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.SaveProdUnitBtn.OnPressedState.BorderRadius = 30;
            this.SaveProdUnitBtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveProdUnitBtn.OnPressedState.BorderThickness = 1;
            this.SaveProdUnitBtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.SaveProdUnitBtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.SaveProdUnitBtn.OnPressedState.IconLeftImage = null;
            this.SaveProdUnitBtn.OnPressedState.IconRightImage = null;
            this.SaveProdUnitBtn.Size = new System.Drawing.Size(99, 37);
            this.SaveProdUnitBtn.TabIndex = 2;
            this.SaveProdUnitBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SaveProdUnitBtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.SaveProdUnitBtn.TextMarginLeft = 0;
            this.SaveProdUnitBtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.SaveProdUnitBtn.UseDefaultRadiusAndThickness = true;
            this.SaveProdUnitBtn.Click += new System.EventHandler(this.SaveProdUnitBtn_Click);
            // 
            // ProdUnitNameTxt
            // 
            this.ProdUnitNameTxt.AcceptsReturn = false;
            this.ProdUnitNameTxt.AcceptsTab = false;
            this.ProdUnitNameTxt.AnimationSpeed = 200;
            this.ProdUnitNameTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.ProdUnitNameTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.ProdUnitNameTxt.BackColor = System.Drawing.Color.Transparent;
            this.ProdUnitNameTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ProdUnitNameTxt.BackgroundImage")));
            this.ProdUnitNameTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.ProdUnitNameTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.ProdUnitNameTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.ProdUnitNameTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.ProdUnitNameTxt.BorderRadius = 1;
            this.ProdUnitNameTxt.BorderThickness = 1;
            this.ProdUnitNameTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.ProdUnitNameTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdUnitNameTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.ProdUnitNameTxt.DefaultText = "";
            this.ProdUnitNameTxt.FillColor = System.Drawing.Color.White;
            this.ProdUnitNameTxt.HideSelection = true;
            this.ProdUnitNameTxt.IconLeft = null;
            this.ProdUnitNameTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdUnitNameTxt.IconPadding = 10;
            this.ProdUnitNameTxt.IconRight = null;
            this.ProdUnitNameTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.ProdUnitNameTxt.Lines = new string[0];
            this.ProdUnitNameTxt.Location = new System.Drawing.Point(19, 58);
            this.ProdUnitNameTxt.MaxLength = 32767;
            this.ProdUnitNameTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.ProdUnitNameTxt.Modified = false;
            this.ProdUnitNameTxt.Multiline = false;
            this.ProdUnitNameTxt.Name = "ProdUnitNameTxt";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdUnitNameTxt.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.ProdUnitNameTxt.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdUnitNameTxt.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.White;
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.ProdUnitNameTxt.OnIdleState = stateProperties8;
            this.ProdUnitNameTxt.Padding = new System.Windows.Forms.Padding(3);
            this.ProdUnitNameTxt.PasswordChar = '\0';
            this.ProdUnitNameTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.ProdUnitNameTxt.PlaceholderText = "Enter text";
            this.ProdUnitNameTxt.ReadOnly = false;
            this.ProdUnitNameTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ProdUnitNameTxt.SelectedText = "";
            this.ProdUnitNameTxt.SelectionLength = 0;
            this.ProdUnitNameTxt.SelectionStart = 0;
            this.ProdUnitNameTxt.ShortcutsEnabled = true;
            this.ProdUnitNameTxt.Size = new System.Drawing.Size(377, 41);
            this.ProdUnitNameTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.ProdUnitNameTxt.TabIndex = 1;
            this.ProdUnitNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.ProdUnitNameTxt.TextMarginBottom = 0;
            this.ProdUnitNameTxt.TextMarginLeft = 3;
            this.ProdUnitNameTxt.TextMarginTop = 0;
            this.ProdUnitNameTxt.TextPlaceholder = "Enter text";
            this.ProdUnitNameTxt.UseSystemPasswordChar = false;
            this.ProdUnitNameTxt.WordWrap = true;
            this.ProdUnitNameTxt.TextChange += new System.EventHandler(this.ProdUnitNameTxt_TextChange);
            this.ProdUnitNameTxt.Validating += new System.ComponentModel.CancelEventHandler(this.ProdUnitNameTxt_Validating);
            // 
            // categoryNameLbl
            // 
            this.categoryNameLbl.AutoSize = true;
            this.categoryNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryNameLbl.Location = new System.Drawing.Point(16, 33);
            this.categoryNameLbl.Name = "categoryNameLbl";
            this.categoryNameLbl.Size = new System.Drawing.Size(87, 18);
            this.categoryNameLbl.TabIndex = 0;
            this.categoryNameLbl.Text = "Unit Name";
            // 
            // ProductUnitHeatingLbl
            // 
            this.ProductUnitHeatingLbl.AutoSize = true;
            this.ProductUnitHeatingLbl.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ProductUnitHeatingLbl.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductUnitHeatingLbl.ForeColor = System.Drawing.SystemColors.Control;
            this.ProductUnitHeatingLbl.Location = new System.Drawing.Point(406, 16);
            this.ProductUnitHeatingLbl.Name = "ProductUnitHeatingLbl";
            this.ProductUnitHeatingLbl.Size = new System.Drawing.Size(213, 26);
            this.ProductUnitHeatingLbl.TabIndex = 4;
            this.ProductUnitHeatingLbl.Text = "Product Unit Form";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // ProductUnitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProductUnitHeatingLbl);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ProductUnitControl";
            this.Size = new System.Drawing.Size(1200, 629);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductUnitDatagridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private Bunifu.UI.WinForms.BunifuDataGridView ProductUnitDatagridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton RemoveProductUnitBtn;
        private System.Windows.Forms.TextBox productUnitIdTxt;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton updateProductIUnitBtn;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton SaveProdUnitBtn;
        private Bunifu.UI.WinForms.BunifuTextBox ProdUnitNameTxt;
        private System.Windows.Forms.Label categoryNameLbl;
        private Bunifu.UI.WinForms.BunifuTextBox ProdUnitAbbreviationTxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ProdUnitActiveChkBox;
        private System.Windows.Forms.Label ProductUnitHeatingLbl;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
