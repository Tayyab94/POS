namespace POS_Shop.Views.Controllers.Category
{
    partial class CategoryControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CategoryControl));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.categoryIdTxt = new System.Windows.Forms.TextBox();
            this.updateCategoryBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.SaveCategoryBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.categoryNameTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.categoryNameLbl = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CategoryDatagridView = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CategoryDatagridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.categoryIdTxt);
            this.groupBox1.Controls.Add(this.updateCategoryBtn);
            this.groupBox1.Controls.Add(this.SaveCategoryBtn);
            this.groupBox1.Controls.Add(this.categoryNameTxt);
            this.groupBox1.Controls.Add(this.categoryNameLbl);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(22, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(987, 165);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Category Form";
            // 
            // categoryIdTxt
            // 
            this.categoryIdTxt.Location = new System.Drawing.Point(192, 105);
            this.categoryIdTxt.Name = "categoryIdTxt";
            this.categoryIdTxt.Size = new System.Drawing.Size(50, 28);
            this.categoryIdTxt.TabIndex = 4;
            this.categoryIdTxt.Visible = false;
            // 
            // updateCategoryBtn
            // 
            this.updateCategoryBtn.AllowAnimations = true;
            this.updateCategoryBtn.AllowMouseEffects = true;
            this.updateCategoryBtn.AllowToggling = false;
            this.updateCategoryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateCategoryBtn.AnimationSpeed = 200;
            this.updateCategoryBtn.AutoGenerateColors = false;
            this.updateCategoryBtn.AutoRoundBorders = false;
            this.updateCategoryBtn.AutoSizeLeftIcon = true;
            this.updateCategoryBtn.AutoSizeRightIcon = true;
            this.updateCategoryBtn.BackColor = System.Drawing.Color.Transparent;
            this.updateCategoryBtn.BackColor1 = System.Drawing.Color.MediumSlateBlue;
            this.updateCategoryBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("updateCategoryBtn.BackgroundImage")));
            this.updateCategoryBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateCategoryBtn.ButtonText = "Update";
            this.updateCategoryBtn.ButtonTextMarginLeft = 0;
            this.updateCategoryBtn.ColorContrastOnClick = 45;
            this.updateCategoryBtn.ColorContrastOnHover = 45;
            this.updateCategoryBtn.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.updateCategoryBtn.CustomizableEdges = borderEdges1;
            this.updateCategoryBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.updateCategoryBtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.updateCategoryBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.updateCategoryBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.updateCategoryBtn.Enabled = false;
            this.updateCategoryBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.updateCategoryBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.updateCategoryBtn.ForeColor = System.Drawing.Color.White;
            this.updateCategoryBtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.updateCategoryBtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.updateCategoryBtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.updateCategoryBtn.IconMarginLeft = 11;
            this.updateCategoryBtn.IconPadding = 10;
            this.updateCategoryBtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.updateCategoryBtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.updateCategoryBtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.updateCategoryBtn.IconSize = 25;
            this.updateCategoryBtn.IdleBorderColor = System.Drawing.Color.CornflowerBlue;
            this.updateCategoryBtn.IdleBorderRadius = 1;
            this.updateCategoryBtn.IdleBorderThickness = 1;
            this.updateCategoryBtn.IdleFillColor = System.Drawing.Color.MediumSlateBlue;
            this.updateCategoryBtn.IdleIconLeftImage = null;
            this.updateCategoryBtn.IdleIconRightImage = null;
            this.updateCategoryBtn.IndicateFocus = false;
            this.updateCategoryBtn.Location = new System.Drawing.Point(647, 110);
            this.updateCategoryBtn.Name = "updateCategoryBtn";
            this.updateCategoryBtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.updateCategoryBtn.OnDisabledState.BorderRadius = 1;
            this.updateCategoryBtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateCategoryBtn.OnDisabledState.BorderThickness = 1;
            this.updateCategoryBtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.updateCategoryBtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.updateCategoryBtn.OnDisabledState.IconLeftImage = null;
            this.updateCategoryBtn.OnDisabledState.IconRightImage = null;
            this.updateCategoryBtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.updateCategoryBtn.onHoverState.BorderRadius = 1;
            this.updateCategoryBtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateCategoryBtn.onHoverState.BorderThickness = 1;
            this.updateCategoryBtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.updateCategoryBtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.updateCategoryBtn.onHoverState.IconLeftImage = null;
            this.updateCategoryBtn.onHoverState.IconRightImage = null;
            this.updateCategoryBtn.OnIdleState.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.updateCategoryBtn.OnIdleState.BorderRadius = 1;
            this.updateCategoryBtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateCategoryBtn.OnIdleState.BorderThickness = 1;
            this.updateCategoryBtn.OnIdleState.FillColor = System.Drawing.Color.MediumSlateBlue;
            this.updateCategoryBtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.updateCategoryBtn.OnIdleState.IconLeftImage = null;
            this.updateCategoryBtn.OnIdleState.IconRightImage = null;
            this.updateCategoryBtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.updateCategoryBtn.OnPressedState.BorderRadius = 1;
            this.updateCategoryBtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.updateCategoryBtn.OnPressedState.BorderThickness = 1;
            this.updateCategoryBtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.updateCategoryBtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.updateCategoryBtn.OnPressedState.IconLeftImage = null;
            this.updateCategoryBtn.OnPressedState.IconRightImage = null;
            this.updateCategoryBtn.Size = new System.Drawing.Size(118, 42);
            this.updateCategoryBtn.TabIndex = 3;
            this.updateCategoryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.updateCategoryBtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.updateCategoryBtn.TextMarginLeft = 0;
            this.updateCategoryBtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.updateCategoryBtn.UseDefaultRadiusAndThickness = true;
            this.updateCategoryBtn.Click += new System.EventHandler(this.updateCategoryBtn_Click);
            // 
            // SaveCategoryBtn
            // 
            this.SaveCategoryBtn.AllowAnimations = true;
            this.SaveCategoryBtn.AllowMouseEffects = true;
            this.SaveCategoryBtn.AllowToggling = false;
            this.SaveCategoryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveCategoryBtn.AnimationSpeed = 200;
            this.SaveCategoryBtn.AutoGenerateColors = false;
            this.SaveCategoryBtn.AutoRoundBorders = false;
            this.SaveCategoryBtn.AutoSizeLeftIcon = true;
            this.SaveCategoryBtn.AutoSizeRightIcon = true;
            this.SaveCategoryBtn.BackColor = System.Drawing.Color.Transparent;
            this.SaveCategoryBtn.BackColor1 = System.Drawing.Color.DodgerBlue;
            this.SaveCategoryBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveCategoryBtn.BackgroundImage")));
            this.SaveCategoryBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCategoryBtn.ButtonText = "Save";
            this.SaveCategoryBtn.ButtonTextMarginLeft = 0;
            this.SaveCategoryBtn.ColorContrastOnClick = 45;
            this.SaveCategoryBtn.ColorContrastOnHover = 45;
            this.SaveCategoryBtn.Cursor = System.Windows.Forms.Cursors.Default;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.SaveCategoryBtn.CustomizableEdges = borderEdges2;
            this.SaveCategoryBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.SaveCategoryBtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.SaveCategoryBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SaveCategoryBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.SaveCategoryBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.SaveCategoryBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SaveCategoryBtn.ForeColor = System.Drawing.Color.White;
            this.SaveCategoryBtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveCategoryBtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.SaveCategoryBtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.SaveCategoryBtn.IconMarginLeft = 11;
            this.SaveCategoryBtn.IconPadding = 10;
            this.SaveCategoryBtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveCategoryBtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.SaveCategoryBtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.SaveCategoryBtn.IconSize = 25;
            this.SaveCategoryBtn.IdleBorderColor = System.Drawing.Color.DodgerBlue;
            this.SaveCategoryBtn.IdleBorderRadius = 1;
            this.SaveCategoryBtn.IdleBorderThickness = 1;
            this.SaveCategoryBtn.IdleFillColor = System.Drawing.Color.DodgerBlue;
            this.SaveCategoryBtn.IdleIconLeftImage = null;
            this.SaveCategoryBtn.IdleIconRightImage = null;
            this.SaveCategoryBtn.IndicateFocus = false;
            this.SaveCategoryBtn.Location = new System.Drawing.Point(784, 111);
            this.SaveCategoryBtn.Name = "SaveCategoryBtn";
            this.SaveCategoryBtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.SaveCategoryBtn.OnDisabledState.BorderRadius = 1;
            this.SaveCategoryBtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCategoryBtn.OnDisabledState.BorderThickness = 1;
            this.SaveCategoryBtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SaveCategoryBtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.SaveCategoryBtn.OnDisabledState.IconLeftImage = null;
            this.SaveCategoryBtn.OnDisabledState.IconRightImage = null;
            this.SaveCategoryBtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SaveCategoryBtn.onHoverState.BorderRadius = 1;
            this.SaveCategoryBtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCategoryBtn.onHoverState.BorderThickness = 1;
            this.SaveCategoryBtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.SaveCategoryBtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.SaveCategoryBtn.onHoverState.IconLeftImage = null;
            this.SaveCategoryBtn.onHoverState.IconRightImage = null;
            this.SaveCategoryBtn.OnIdleState.BorderColor = System.Drawing.Color.DodgerBlue;
            this.SaveCategoryBtn.OnIdleState.BorderRadius = 1;
            this.SaveCategoryBtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCategoryBtn.OnIdleState.BorderThickness = 1;
            this.SaveCategoryBtn.OnIdleState.FillColor = System.Drawing.Color.DodgerBlue;
            this.SaveCategoryBtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.SaveCategoryBtn.OnIdleState.IconLeftImage = null;
            this.SaveCategoryBtn.OnIdleState.IconRightImage = null;
            this.SaveCategoryBtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.SaveCategoryBtn.OnPressedState.BorderRadius = 1;
            this.SaveCategoryBtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCategoryBtn.OnPressedState.BorderThickness = 1;
            this.SaveCategoryBtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(96)))), ((int)(((byte)(144)))));
            this.SaveCategoryBtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.SaveCategoryBtn.OnPressedState.IconLeftImage = null;
            this.SaveCategoryBtn.OnPressedState.IconRightImage = null;
            this.SaveCategoryBtn.Size = new System.Drawing.Size(180, 39);
            this.SaveCategoryBtn.TabIndex = 2;
            this.SaveCategoryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SaveCategoryBtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.SaveCategoryBtn.TextMarginLeft = 0;
            this.SaveCategoryBtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.SaveCategoryBtn.UseDefaultRadiusAndThickness = true;
            this.SaveCategoryBtn.Click += new System.EventHandler(this.SaveCategoryBtn_Click);
            // 
            // categoryNameTxt
            // 
            this.categoryNameTxt.AcceptsReturn = false;
            this.categoryNameTxt.AcceptsTab = false;
            this.categoryNameTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.categoryNameTxt.AnimationSpeed = 200;
            this.categoryNameTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.categoryNameTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.categoryNameTxt.BackColor = System.Drawing.Color.Transparent;
            this.categoryNameTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("categoryNameTxt.BackgroundImage")));
            this.categoryNameTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.categoryNameTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.categoryNameTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.categoryNameTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.categoryNameTxt.BorderRadius = 1;
            this.categoryNameTxt.BorderThickness = 1;
            this.categoryNameTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.categoryNameTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.categoryNameTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.categoryNameTxt.DefaultText = "";
            this.categoryNameTxt.FillColor = System.Drawing.Color.White;
            this.categoryNameTxt.HideSelection = true;
            this.categoryNameTxt.IconLeft = null;
            this.categoryNameTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.categoryNameTxt.IconPadding = 10;
            this.categoryNameTxt.IconRight = null;
            this.categoryNameTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.categoryNameTxt.Lines = new string[0];
            this.categoryNameTxt.Location = new System.Drawing.Point(187, 44);
            this.categoryNameTxt.MaxLength = 32767;
            this.categoryNameTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.categoryNameTxt.Modified = false;
            this.categoryNameTxt.Multiline = false;
            this.categoryNameTxt.Name = "categoryNameTxt";
            stateProperties1.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.categoryNameTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.categoryNameTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.categoryNameTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.Silver;
            stateProperties4.FillColor = System.Drawing.Color.White;
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.categoryNameTxt.OnIdleState = stateProperties4;
            this.categoryNameTxt.Padding = new System.Windows.Forms.Padding(3);
            this.categoryNameTxt.PasswordChar = '\0';
            this.categoryNameTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.categoryNameTxt.PlaceholderText = "Enter text";
            this.categoryNameTxt.ReadOnly = false;
            this.categoryNameTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.categoryNameTxt.SelectedText = "";
            this.categoryNameTxt.SelectionLength = 0;
            this.categoryNameTxt.SelectionStart = 0;
            this.categoryNameTxt.ShortcutsEnabled = true;
            this.categoryNameTxt.Size = new System.Drawing.Size(772, 41);
            this.categoryNameTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.categoryNameTxt.TabIndex = 1;
            this.categoryNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.categoryNameTxt.TextMarginBottom = 0;
            this.categoryNameTxt.TextMarginLeft = 3;
            this.categoryNameTxt.TextMarginTop = 0;
            this.categoryNameTxt.TextPlaceholder = "Enter text";
            this.categoryNameTxt.UseSystemPasswordChar = false;
            this.categoryNameTxt.WordWrap = true;
            this.categoryNameTxt.TextChange += new System.EventHandler(this.CategoryNameTxt_Changed);
            this.categoryNameTxt.Validating += new System.ComponentModel.CancelEventHandler(this.categoryNameTxt_Validating);
            // 
            // categoryNameLbl
            // 
            this.categoryNameLbl.AutoSize = true;
            this.categoryNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryNameLbl.Location = new System.Drawing.Point(29, 64);
            this.categoryNameLbl.Name = "categoryNameLbl";
            this.categoryNameLbl.Size = new System.Drawing.Size(125, 18);
            this.categoryNameLbl.TabIndex = 0;
            this.categoryNameLbl.Text = "Category Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.CategoryDatagridView);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(22, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1159, 389);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Category List";
            // 
            // CategoryDatagridView
            // 
            this.CategoryDatagridView.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.CategoryDatagridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CategoryDatagridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CategoryDatagridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CategoryDatagridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CategoryDatagridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.CategoryDatagridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CategoryDatagridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CategoryDatagridView.ColumnHeadersHeight = 40;
            this.CategoryDatagridView.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.CategoryDatagridView.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.CategoryDatagridView.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Black;
            this.CategoryDatagridView.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.CategoryDatagridView.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.CategoryDatagridView.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.CategoryDatagridView.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.CategoryDatagridView.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.DodgerBlue;
            this.CategoryDatagridView.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.CategoryDatagridView.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.CategoryDatagridView.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(115)))), ((int)(((byte)(204)))));
            this.CategoryDatagridView.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.CategoryDatagridView.CurrentTheme.Name = null;
            this.CategoryDatagridView.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.CategoryDatagridView.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.CategoryDatagridView.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.CategoryDatagridView.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.CategoryDatagridView.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CategoryDatagridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.CategoryDatagridView.EnableHeadersVisualStyles = false;
            this.CategoryDatagridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(255)))));
            this.CategoryDatagridView.HeaderBackColor = System.Drawing.Color.DodgerBlue;
            this.CategoryDatagridView.HeaderBgColor = System.Drawing.Color.Empty;
            this.CategoryDatagridView.HeaderForeColor = System.Drawing.Color.White;
            this.CategoryDatagridView.Location = new System.Drawing.Point(19, 42);
            this.CategoryDatagridView.Name = "CategoryDatagridView";
            this.CategoryDatagridView.RowHeadersVisible = false;
            this.CategoryDatagridView.RowHeadersWidth = 51;
            this.CategoryDatagridView.RowTemplate.Height = 40;
            this.CategoryDatagridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CategoryDatagridView.Size = new System.Drawing.Size(1134, 322);
            this.CategoryDatagridView.TabIndex = 4;
            this.CategoryDatagridView.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.CategoryDatagridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CategoryDatagridView_CellClick);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 608);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1200, 21);
            this.panel1.TabIndex = 2;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CategoryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CategoryControl";
            this.Size = new System.Drawing.Size(1200, 629);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CategoryDatagridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label categoryNameLbl;
        private Bunifu.UI.WinForms.BunifuTextBox categoryNameTxt;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton updateCategoryBtn;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton SaveCategoryBtn;
        private Bunifu.UI.WinForms.BunifuDataGridView CategoryDatagridView;
        private System.Windows.Forms.TextBox categoryIdTxt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
