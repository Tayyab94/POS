namespace POS_Shop.Views.Controllers.Country
{
    partial class CountryControl1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CountryControl1));
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges1 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges2 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties1 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties2 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties3 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties4 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties5 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties6 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties7 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuTextBox.StateProperties stateProperties8 = new Bunifu.UI.WinForms.BunifuTextBox.StateProperties();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges3 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges borderEdges4 = new Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderEdges();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.countryIdTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.UpdateCountrybtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.RemoveCountryBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.SaveCityBtn = new Bunifu.UI.WinForms.BunifuButton.BunifuButton();
            this.CountryNameTxt = new Bunifu.UI.WinForms.BunifuTextBox();
            this.lblCityName = new System.Windows.Forms.Label();
            this.CountryDatagridView = new Bunifu.UI.WinForms.BunifuDataGridView();
            this.cityGrdiheadingLbl = new System.Windows.Forms.Label();
            this.CountryListGroup = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CountryDatagridView)).BeginInit();
            this.CountryListGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.countryIdTxt);
            this.panel1.Controls.Add(this.UpdateCountrybtn);
            this.panel1.Controls.Add(this.RemoveCountryBtn);
            this.panel1.Controls.Add(this.SaveCityBtn);
            this.panel1.Controls.Add(this.CountryNameTxt);
            this.panel1.Controls.Add(this.lblCityName);
            this.panel1.Location = new System.Drawing.Point(20, 70);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1160, 100);
            this.panel1.TabIndex = 1;
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            // Removed BorderStyle.FixedSingle for a cleaner look
            // 
            // lblCityName (Country Name Label)
            // 
            this.lblCityName.AutoSize = true;
            this.lblCityName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular);
            this.lblCityName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(69)))));
            this.lblCityName.Location = new System.Drawing.Point(15, 15);
            this.lblCityName.Name = "lblCityName";
            this.lblCityName.Size = new System.Drawing.Size(110, 19);
            this.lblCityName.TabIndex = 0;
            this.lblCityName.Text = "Country Name";
            // 
            // CountryNameTxt
            // 
            this.CountryNameTxt.AcceptsReturn = false;
            this.CountryNameTxt.AcceptsTab = false;
            this.CountryNameTxt.AnimationSpeed = 200;
            this.CountryNameTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.CountryNameTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.CountryNameTxt.BackColor = System.Drawing.Color.Transparent;
            this.CountryNameTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CountryNameTxt.BackgroundImage")));
            this.CountryNameTxt.BorderColorActive = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.CountryNameTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.CountryNameTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.CountryNameTxt.BorderColorIdle = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CountryNameTxt.BorderRadius = 5;
            this.CountryNameTxt.BorderThickness = 1;
            this.CountryNameTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.CountryNameTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.CountryNameTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 10F);
            this.CountryNameTxt.DefaultText = "";
            this.CountryNameTxt.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.CountryNameTxt.HideSelection = true;
            this.CountryNameTxt.IconLeft = null;
            this.CountryNameTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.CountryNameTxt.IconPadding = 10;
            this.CountryNameTxt.IconRight = null;
            this.CountryNameTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.CountryNameTxt.Lines = new string[0];
            this.CountryNameTxt.Location = new System.Drawing.Point(15, 40);
            this.CountryNameTxt.MaxLength = 32767;
            this.CountryNameTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.CountryNameTxt.Modified = false;
            this.CountryNameTxt.Multiline = false;
            this.CountryNameTxt.Name = "CountryNameTxt";
            stateProperties1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            stateProperties1.FillColor = System.Drawing.Color.Empty;
            stateProperties1.ForeColor = System.Drawing.Color.Empty;
            stateProperties1.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.CountryNameTxt.OnActiveState = stateProperties1;
            stateProperties2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties2.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.CountryNameTxt.OnDisabledState = stateProperties2;
            stateProperties3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            stateProperties3.FillColor = System.Drawing.Color.Empty;
            stateProperties3.ForeColor = System.Drawing.Color.Empty;
            stateProperties3.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.CountryNameTxt.OnHoverState = stateProperties3;
            stateProperties4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            stateProperties4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            stateProperties4.ForeColor = System.Drawing.Color.Empty;
            stateProperties4.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.CountryNameTxt.OnIdleState = stateProperties4;
            this.CountryNameTxt.Padding = new System.Windows.Forms.Padding(3);
            this.CountryNameTxt.PasswordChar = '\0';
            this.CountryNameTxt.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(145)))));
            this.CountryNameTxt.PlaceholderText = "Enter country name";
            this.CountryNameTxt.ReadOnly = false;
            this.CountryNameTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.CountryNameTxt.SelectedText = "";
            this.CountryNameTxt.SelectionLength = 0;
            this.CountryNameTxt.SelectionStart = 0;
            this.CountryNameTxt.ShortcutsEnabled = true;
            this.CountryNameTxt.Size = new System.Drawing.Size(300, 45);
            this.CountryNameTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.CountryNameTxt.TabIndex = 2;
            this.CountryNameTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.CountryNameTxt.TextMarginBottom = 0;
            this.CountryNameTxt.TextMarginLeft = 3;
            this.CountryNameTxt.TextMarginTop = 0;
            this.CountryNameTxt.TextPlaceholder = "Enter country name";
            this.CountryNameTxt.UseSystemPasswordChar = false;
            this.CountryNameTxt.WordWrap = true;
            // 
            // SaveCityBtn
            // 
            this.SaveCityBtn.AllowAnimations = true;
            this.SaveCityBtn.AllowMouseEffects = true;
            this.SaveCityBtn.AllowToggling = false;
            this.SaveCityBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveCityBtn.AnimationSpeed = 200;
            this.SaveCityBtn.AutoGenerateColors = false;
            this.SaveCityBtn.AutoRoundBorders = false;
            this.SaveCityBtn.AutoSizeLeftIcon = true;
            this.SaveCityBtn.AutoSizeRightIcon = true;
            this.SaveCityBtn.BackColor = System.Drawing.Color.Transparent;
            this.SaveCityBtn.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(200)))), ((int)(((byte)(138))))); // Professional Green
            this.SaveCityBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("SaveCityBtn.BackgroundImage")));
            this.SaveCityBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCityBtn.ButtonText = "Save";
            this.SaveCityBtn.ButtonTextMarginLeft = 0;
            this.SaveCityBtn.ColorContrastOnClick = 45;
            this.SaveCityBtn.ColorContrastOnHover = 45;
            this.SaveCityBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges1.BottomLeft = true;
            borderEdges1.BottomRight = true;
            borderEdges1.TopLeft = true;
            borderEdges1.TopRight = true;
            this.SaveCityBtn.CustomizableEdges = borderEdges1;
            this.SaveCityBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.SaveCityBtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.SaveCityBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SaveCityBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.SaveCityBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.SaveCityBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.SaveCityBtn.ForeColor = System.Drawing.Color.White;
            this.SaveCityBtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SaveCityBtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.SaveCityBtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.SaveCityBtn.IconMarginLeft = 11;
            this.SaveCityBtn.IconPadding = 10;
            this.SaveCityBtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SaveCityBtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.SaveCityBtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.SaveCityBtn.IconSize = 25;
            this.SaveCityBtn.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(200)))), ((int)(((byte)(138)))));
            this.SaveCityBtn.IdleBorderRadius = 5;
            this.SaveCityBtn.IdleBorderThickness = 1;
            this.SaveCityBtn.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(200)))), ((int)(((byte)(138)))));
            this.SaveCityBtn.IdleIconLeftImage = null;
            this.SaveCityBtn.IdleIconRightImage = null;
            this.SaveCityBtn.IndicateFocus = false;
            this.SaveCityBtn.Location = new System.Drawing.Point(750, 40);
            this.SaveCityBtn.Name = "SaveCityBtn";
            this.SaveCityBtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.SaveCityBtn.OnDisabledState.BorderRadius = 5;
            this.SaveCityBtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCityBtn.OnDisabledState.BorderThickness = 1;
            this.SaveCityBtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.SaveCityBtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.SaveCityBtn.OnDisabledState.IconLeftImage = null;
            this.SaveCityBtn.OnDisabledState.IconRightImage = null;
            this.SaveCityBtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(188)))), ((int)(((byte)(128)))));
            this.SaveCityBtn.onHoverState.BorderRadius = 5;
            this.SaveCityBtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCityBtn.onHoverState.BorderThickness = 1;
            this.SaveCityBtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(188)))), ((int)(((byte)(128)))));
            this.SaveCityBtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.SaveCityBtn.onHoverState.IconLeftImage = null;
            this.SaveCityBtn.onHoverState.IconRightImage = null;
            this.SaveCityBtn.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(200)))), ((int)(((byte)(138)))));
            this.SaveCityBtn.OnIdleState.BorderRadius = 5;
            this.SaveCityBtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCityBtn.OnIdleState.BorderThickness = 1;
            this.SaveCityBtn.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(200)))), ((int)(((byte)(138)))));
            this.SaveCityBtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.SaveCityBtn.OnIdleState.IconLeftImage = null;
            this.SaveCityBtn.OnIdleState.IconRightImage = null;
            this.SaveCityBtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.SaveCityBtn.OnPressedState.BorderRadius = 5;
            this.SaveCityBtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.SaveCityBtn.OnPressedState.BorderThickness = 1;
            this.SaveCityBtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this.SaveCityBtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.SaveCityBtn.OnPressedState.IconLeftImage = null;
            this.SaveCityBtn.OnPressedState.IconRightImage = null;
            this.SaveCityBtn.Size = new System.Drawing.Size(120, 45);
            this.SaveCityBtn.TabIndex = 3;
            this.SaveCityBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SaveCityBtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.SaveCityBtn.TextMarginLeft = 0;
            this.SaveCityBtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.SaveCityBtn.UseDefaultRadiusAndThickness = true;
            this.SaveCityBtn.Click += new System.EventHandler(this.SaveCityBtn_Click);
            // 
            // RemoveCountryBtn
            // 
            this.RemoveCountryBtn.AllowAnimations = true;
            this.RemoveCountryBtn.AllowMouseEffects = true;
            this.RemoveCountryBtn.AllowToggling = false;
            this.RemoveCountryBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RemoveCountryBtn.AnimationSpeed = 200;
            this.RemoveCountryBtn.AutoGenerateColors = false;
            this.RemoveCountryBtn.AutoRoundBorders = false;
            this.RemoveCountryBtn.AutoSizeLeftIcon = true;
            this.RemoveCountryBtn.AutoSizeRightIcon = true;
            this.RemoveCountryBtn.BackColor = System.Drawing.Color.Transparent;
            this.RemoveCountryBtn.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(74)))), ((int)(((byte)(59))))); // Professional Red
            this.RemoveCountryBtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("RemoveCountryBtn.BackgroundImage")));
            this.RemoveCountryBtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveCountryBtn.ButtonText = "Delete";
            this.RemoveCountryBtn.ButtonTextMarginLeft = 0;
            this.RemoveCountryBtn.ColorContrastOnClick = 45;
            this.RemoveCountryBtn.ColorContrastOnHover = 45;
            this.RemoveCountryBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges2.BottomLeft = true;
            borderEdges2.BottomRight = true;
            borderEdges2.TopLeft = true;
            borderEdges2.TopRight = true;
            this.RemoveCountryBtn.CustomizableEdges = borderEdges2;
            this.RemoveCountryBtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.RemoveCountryBtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.RemoveCountryBtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.RemoveCountryBtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.RemoveCountryBtn.Enabled = false;
            this.RemoveCountryBtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.RemoveCountryBtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.RemoveCountryBtn.ForeColor = System.Drawing.Color.White;
            this.RemoveCountryBtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.RemoveCountryBtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.RemoveCountryBtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.RemoveCountryBtn.IconMarginLeft = 11;
            this.RemoveCountryBtn.IconPadding = 10;
            this.RemoveCountryBtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.RemoveCountryBtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.RemoveCountryBtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.RemoveCountryBtn.IconSize = 25;
            this.RemoveCountryBtn.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(74)))), ((int)(((byte)(59)))));
            this.RemoveCountryBtn.IdleBorderRadius = 5;
            this.RemoveCountryBtn.IdleBorderThickness = 1;
            this.RemoveCountryBtn.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(74)))), ((int)(((byte)(59)))));
            this.RemoveCountryBtn.IdleIconLeftImage = null;
            this.RemoveCountryBtn.IdleIconRightImage = null;
            this.RemoveCountryBtn.IndicateFocus = false;
            this.RemoveCountryBtn.Location = new System.Drawing.Point(1020, 40);
            this.RemoveCountryBtn.Name = "RemoveCountryBtn";
            this.RemoveCountryBtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.RemoveCountryBtn.OnDisabledState.BorderRadius = 5;
            this.RemoveCountryBtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveCountryBtn.OnDisabledState.BorderThickness = 1;
            this.RemoveCountryBtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.RemoveCountryBtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.RemoveCountryBtn.OnDisabledState.IconLeftImage = null;
            this.RemoveCountryBtn.OnDisabledState.IconRightImage = null;
            this.RemoveCountryBtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(45)))));
            this.RemoveCountryBtn.onHoverState.BorderRadius = 5;
            this.RemoveCountryBtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveCountryBtn.onHoverState.BorderThickness = 1;
            this.RemoveCountryBtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(60)))), ((int)(((byte)(45)))));
            this.RemoveCountryBtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.RemoveCountryBtn.onHoverState.IconLeftImage = null;
            this.RemoveCountryBtn.onHoverState.IconRightImage = null;
            this.RemoveCountryBtn.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(74)))), ((int)(((byte)(59)))));
            this.RemoveCountryBtn.OnIdleState.BorderRadius = 5;
            this.RemoveCountryBtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveCountryBtn.OnIdleState.BorderThickness = 1;
            this.RemoveCountryBtn.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(74)))), ((int)(((byte)(59)))));
            this.RemoveCountryBtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.RemoveCountryBtn.OnIdleState.IconLeftImage = null;
            this.RemoveCountryBtn.OnIdleState.IconRightImage = null;
            this.RemoveCountryBtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.RemoveCountryBtn.OnPressedState.BorderRadius = 5;
            this.RemoveCountryBtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.RemoveCountryBtn.OnPressedState.BorderThickness = 1;
            this.RemoveCountryBtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(50)))), ((int)(((byte)(35)))));
            this.RemoveCountryBtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.RemoveCountryBtn.OnPressedState.IconLeftImage = null;
            this.RemoveCountryBtn.OnPressedState.IconRightImage = null;
            this.RemoveCountryBtn.Size = new System.Drawing.Size(120, 45);
            this.RemoveCountryBtn.TabIndex = 9;
            this.RemoveCountryBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RemoveCountryBtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.RemoveCountryBtn.TextMarginLeft = 0;
            this.RemoveCountryBtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.RemoveCountryBtn.UseDefaultRadiusAndThickness = true;
            this.RemoveCountryBtn.Visible = false;
            this.RemoveCountryBtn.Click += new System.EventHandler(this.RemoveCountryBtn_Click);
            // 
            // UpdateCountrybtn
            // 
            this.UpdateCountrybtn.AllowAnimations = true;
            this.UpdateCountrybtn.AllowMouseEffects = true;
            this.UpdateCountrybtn.AllowToggling = false;
            this.UpdateCountrybtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateCountrybtn.AnimationSpeed = 200;
            this.UpdateCountrybtn.AutoGenerateColors = false;
            this.UpdateCountrybtn.AutoRoundBorders = false;
            this.UpdateCountrybtn.AutoSizeLeftIcon = true;
            this.UpdateCountrybtn.AutoSizeRightIcon = true;
            this.UpdateCountrybtn.BackColor = System.Drawing.Color.Transparent;
            this.UpdateCountrybtn.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223))))); // Professional Blue
            this.UpdateCountrybtn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("UpdateCountrybtn.BackgroundImage")));
            this.UpdateCountrybtn.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.UpdateCountrybtn.ButtonText = "Update";
            this.UpdateCountrybtn.ButtonTextMarginLeft = 0;
            this.UpdateCountrybtn.ColorContrastOnClick = 45;
            this.UpdateCountrybtn.ColorContrastOnHover = 45;
            this.UpdateCountrybtn.Cursor = System.Windows.Forms.Cursors.Hand;
            borderEdges3.BottomLeft = true;
            borderEdges3.BottomRight = true;
            borderEdges3.TopLeft = true;
            borderEdges3.TopRight = true;
            this.UpdateCountrybtn.CustomizableEdges = borderEdges3;
            this.UpdateCountrybtn.DialogResult = System.Windows.Forms.DialogResult.None;
            this.UpdateCountrybtn.DisabledBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.UpdateCountrybtn.DisabledFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.UpdateCountrybtn.DisabledForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.UpdateCountrybtn.Enabled = false;
            this.UpdateCountrybtn.FocusState = Bunifu.UI.WinForms.BunifuButton.BunifuButton.ButtonStates.Pressed;
            this.UpdateCountrybtn.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.UpdateCountrybtn.ForeColor = System.Drawing.Color.White;
            this.UpdateCountrybtn.IconLeftAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.UpdateCountrybtn.IconLeftCursor = System.Windows.Forms.Cursors.Default;
            this.UpdateCountrybtn.IconLeftPadding = new System.Windows.Forms.Padding(11, 3, 3, 3);
            this.UpdateCountrybtn.IconMarginLeft = 11;
            this.UpdateCountrybtn.IconPadding = 10;
            this.UpdateCountrybtn.IconRightAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.UpdateCountrybtn.IconRightCursor = System.Windows.Forms.Cursors.Default;
            this.UpdateCountrybtn.IconRightPadding = new System.Windows.Forms.Padding(3, 3, 7, 3);
            this.UpdateCountrybtn.IconSize = 25;
            this.UpdateCountrybtn.IdleBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.UpdateCountrybtn.IdleBorderRadius = 5;
            this.UpdateCountrybtn.IdleBorderThickness = 1;
            this.UpdateCountrybtn.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.UpdateCountrybtn.IdleIconLeftImage = null;
            this.UpdateCountrybtn.IdleIconRightImage = null;
            this.UpdateCountrybtn.IndicateFocus = false;
            this.UpdateCountrybtn.Location = new System.Drawing.Point(885, 40);
            this.UpdateCountrybtn.Name = "UpdateCountrybtn";
            this.UpdateCountrybtn.OnDisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(191)))), ((int)(((byte)(191)))));
            this.UpdateCountrybtn.OnDisabledState.BorderRadius = 5;
            this.UpdateCountrybtn.OnDisabledState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.UpdateCountrybtn.OnDisabledState.BorderThickness = 1;
            this.UpdateCountrybtn.OnDisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.UpdateCountrybtn.OnDisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(168)))), ((int)(((byte)(160)))), ((int)(((byte)(168)))));
            this.UpdateCountrybtn.OnDisabledState.IconLeftImage = null;
            this.UpdateCountrybtn.OnDisabledState.IconRightImage = null;
            this.UpdateCountrybtn.onHoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            this.UpdateCountrybtn.onHoverState.BorderRadius = 5;
            this.UpdateCountrybtn.onHoverState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.UpdateCountrybtn.onHoverState.BorderThickness = 1;
            this.UpdateCountrybtn.onHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            this.UpdateCountrybtn.onHoverState.ForeColor = System.Drawing.Color.White;
            this.UpdateCountrybtn.onHoverState.IconLeftImage = null;
            this.UpdateCountrybtn.onHoverState.IconRightImage = null;
            this.UpdateCountrybtn.OnIdleState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.UpdateCountrybtn.OnIdleState.BorderRadius = 5;
            this.UpdateCountrybtn.OnIdleState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.UpdateCountrybtn.OnIdleState.BorderThickness = 1;
            this.UpdateCountrybtn.OnIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.UpdateCountrybtn.OnIdleState.ForeColor = System.Drawing.Color.White;
            this.UpdateCountrybtn.OnIdleState.IconLeftImage = null;
            this.UpdateCountrybtn.OnIdleState.IconRightImage = null;
            this.UpdateCountrybtn.OnPressedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(150)))));
            this.UpdateCountrybtn.OnPressedState.BorderRadius = 5;
            this.UpdateCountrybtn.OnPressedState.BorderStyle = Bunifu.UI.WinForms.BunifuButton.BunifuButton.BorderStyles.Solid;
            this.UpdateCountrybtn.OnPressedState.BorderThickness = 1;
            this.UpdateCountrybtn.OnPressedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(70)))), ((int)(((byte)(150)))));
            this.UpdateCountrybtn.OnPressedState.ForeColor = System.Drawing.Color.White;
            this.UpdateCountrybtn.OnPressedState.IconLeftImage = null;
            this.UpdateCountrybtn.OnPressedState.IconRightImage = null;
            this.UpdateCountrybtn.Size = new System.Drawing.Size(120, 45);
            this.UpdateCountrybtn.TabIndex = 8;
            this.UpdateCountrybtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UpdateCountrybtn.TextAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.UpdateCountrybtn.TextMarginLeft = 0;
            this.UpdateCountrybtn.TextPadding = new System.Windows.Forms.Padding(0);
            this.UpdateCountrybtn.UseDefaultRadiusAndThickness = true;
            this.UpdateCountrybtn.Click += new System.EventHandler(this.UpdateCountrybtn_Click_1);
            // 
            // countryIdTxt
            // 
            this.countryIdTxt.AcceptsReturn = false;
            this.countryIdTxt.AcceptsTab = false;
            this.countryIdTxt.AnimationSpeed = 200;
            this.countryIdTxt.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.None;
            this.countryIdTxt.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.None;
            this.countryIdTxt.BackColor = System.Drawing.Color.Transparent;
            this.countryIdTxt.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("countryIdTxt.BackgroundImage")));
            this.countryIdTxt.BorderColorActive = System.Drawing.Color.DodgerBlue;
            this.countryIdTxt.BorderColorDisabled = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.countryIdTxt.BorderColorHover = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            this.countryIdTxt.BorderColorIdle = System.Drawing.Color.Silver;
            this.countryIdTxt.BorderRadius = 1;
            this.countryIdTxt.BorderThickness = 1;
            this.countryIdTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.countryIdTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.countryIdTxt.DefaultFont = new System.Drawing.Font("Segoe UI", 9.25F);
            this.countryIdTxt.DefaultText = "";
            this.countryIdTxt.FillColor = System.Drawing.Color.White;
            this.countryIdTxt.HideSelection = true;
            this.countryIdTxt.IconLeft = null;
            this.countryIdTxt.IconLeftCursor = System.Windows.Forms.Cursors.IBeam;
            this.countryIdTxt.IconPadding = 10;
            this.countryIdTxt.IconRight = null;
            this.countryIdTxt.IconRightCursor = System.Windows.Forms.Cursors.IBeam;
            this.countryIdTxt.Lines = new string[0];
            this.countryIdTxt.Location = new System.Drawing.Point(350, 40);
            this.countryIdTxt.MaxLength = 32767;
            this.countryIdTxt.MinimumSize = new System.Drawing.Size(1, 1);
            this.countryIdTxt.Modified = false;
            this.countryIdTxt.Multiline = false;
            this.countryIdTxt.Name = "countryIdTxt";
            stateProperties5.BorderColor = System.Drawing.Color.DodgerBlue;
            stateProperties5.FillColor = System.Drawing.Color.Empty;
            stateProperties5.ForeColor = System.Drawing.Color.Empty;
            stateProperties5.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.countryIdTxt.OnActiveState = stateProperties5;
            stateProperties6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            stateProperties6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            stateProperties6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            stateProperties6.PlaceholderForeColor = System.Drawing.Color.DarkGray;
            this.countryIdTxt.OnDisabledState = stateProperties6;
            stateProperties7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(181)))), ((int)(((byte)(255)))));
            stateProperties7.FillColor = System.Drawing.Color.Empty;
            stateProperties7.ForeColor = System.Drawing.Color.Empty;
            stateProperties7.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.countryIdTxt.OnHoverState = stateProperties7;
            stateProperties8.BorderColor = System.Drawing.Color.Silver;
            stateProperties8.FillColor = System.Drawing.Color.White;
            stateProperties8.ForeColor = System.Drawing.Color.Empty;
            stateProperties8.PlaceholderForeColor = System.Drawing.Color.Empty;
            this.countryIdTxt.OnIdleState = stateProperties8;
            this.countryIdTxt.Padding = new System.Windows.Forms.Padding(3);
            this.countryIdTxt.PasswordChar = '\0';
            this.countryIdTxt.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.countryIdTxt.PlaceholderText = "Country Id";
            this.countryIdTxt.ReadOnly = false;
            this.countryIdTxt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.countryIdTxt.SelectedText = "";
            this.countryIdTxt.SelectionLength = 0;
            this.countryIdTxt.SelectionStart = 0;
            this.countryIdTxt.ShortcutsEnabled = true;
            this.countryIdTxt.Size = new System.Drawing.Size(260, 41);
            this.countryIdTxt.Style = Bunifu.UI.WinForms.BunifuTextBox._Style.Bunifu;
            this.countryIdTxt.TabIndex = 7;
            this.countryIdTxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.countryIdTxt.TextMarginBottom = 0;
            this.countryIdTxt.TextMarginLeft = 3;
            this.countryIdTxt.TextMarginTop = 0;
            this.countryIdTxt.TextPlaceholder = "Country Id";
            this.countryIdTxt.UseSystemPasswordChar = false;
            this.countryIdTxt.Visible = false;
            this.countryIdTxt.WordWrap = true;
            // 
            // CountryDatagridView
            // 
            this.CountryDatagridView.AllowCustomTheming = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(69)))));
            this.CountryDatagridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CountryDatagridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CountryDatagridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CountryDatagridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CountryDatagridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.CountryDatagridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CountryDatagridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.CountryDatagridView.ColumnHeadersHeight = 50;
            this.CountryDatagridView.CurrentTheme.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.CountryDatagridView.CurrentTheme.AlternatingRowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.CountryDatagridView.CurrentTheme.AlternatingRowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(69)))));
            this.CountryDatagridView.CurrentTheme.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.CountryDatagridView.CurrentTheme.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.CountryDatagridView.CurrentTheme.BackColor = System.Drawing.Color.White;
            this.CountryDatagridView.CurrentTheme.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.CountryDatagridView.CurrentTheme.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.CountryDatagridView.CurrentTheme.HeaderStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 11.75F, System.Drawing.FontStyle.Bold);
            this.CountryDatagridView.CurrentTheme.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.CountryDatagridView.CurrentTheme.HeaderStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(90)))), ((int)(((byte)(180)))));
            this.CountryDatagridView.CurrentTheme.HeaderStyle.SelectionForeColor = System.Drawing.Color.White;
            this.CountryDatagridView.CurrentTheme.Name = null;
            this.CountryDatagridView.CurrentTheme.RowsStyle.BackColor = System.Drawing.Color.White;
            this.CountryDatagridView.CurrentTheme.RowsStyle.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.CountryDatagridView.CurrentTheme.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(69)))));
            this.CountryDatagridView.CurrentTheme.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.CountryDatagridView.CurrentTheme.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(69)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CountryDatagridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.CountryDatagridView.EnableHeadersVisualStyles = false;
            this.CountryDatagridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(250)))));
            this.CountryDatagridView.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.CountryDatagridView.HeaderBgColor = System.Drawing.Color.Empty;
            this.CountryDatagridView.HeaderForeColor = System.Drawing.Color.White;
            this.CountryDatagridView.Location = new System.Drawing.Point(10, 30);
            this.CountryDatagridView.Name = "CountryDatagridView";
            this.CountryDatagridView.RowHeadersVisible = false;
            this.CountryDatagridView.RowHeadersWidth = 51;
            this.CountryDatagridView.RowTemplate.Height = 45;
            this.CountryDatagridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CountryDatagridView.Size = new System.Drawing.Size(1148, 310);
            this.CountryDatagridView.TabIndex = 4;
            this.CountryDatagridView.Theme = Bunifu.UI.WinForms.BunifuDataGridView.PresetThemes.Light;
            this.CountryDatagridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CountryDatagridView_CellClick);
            // 
            // cityGrdiheadingLbl
            // 
            this.cityGrdiheadingLbl.AutoSize = true;
            this.cityGrdiheadingLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.cityGrdiheadingLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(115)))), ((int)(((byte)(223)))));
            this.cityGrdiheadingLbl.Location = new System.Drawing.Point(20, 20);
            this.cityGrdiheadingLbl.Name = "cityGrdiheadingLbl";
            this.cityGrdiheadingLbl.Size = new System.Drawing.Size(250, 32);
            this.cityGrdiheadingLbl.TabIndex = 11;
            this.cityGrdiheadingLbl.Text = "Country Management";
            // 
            // CountryListGroup
            // 
            this.CountryListGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CountryListGroup.Controls.Add(this.CountryDatagridView);
            this.CountryListGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.CountryListGroup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(59)))), ((int)(((byte)(69)))));
            this.CountryListGroup.Location = new System.Drawing.Point(20, 190);
            this.CountryListGroup.Name = "CountryListGroup";
            this.CountryListGroup.Size = new System.Drawing.Size(1160, 380);
            this.CountryListGroup.TabIndex = 12;
            this.CountryListGroup.TabStop = false;
            this.CountryListGroup.Text = "Country Records";
            // 
            // CountryControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250))))); // Light Gray Background
            this.Controls.Add(this.CountryListGroup);
            this.Controls.Add(this.cityGrdiheadingLbl);
            this.Controls.Add(this.panel1);
            this.Name = "CountryControl1";
            this.Size = new System.Drawing.Size(1200, 629);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CountryDatagridView)).EndInit();
            this.CountryListGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCityName;
        private Bunifu.UI.WinForms.BunifuTextBox CountryNameTxt;
        private Bunifu.UI.WinForms.BunifuDataGridView CountryDatagridView;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton SaveCityBtn;
        private System.Windows.Forms.Label cityGrdiheadingLbl;
        private Bunifu.UI.WinForms.BunifuTextBox countryIdTxt;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton UpdateCountrybtn;
        private Bunifu.UI.WinForms.BunifuButton.BunifuButton RemoveCountryBtn;
        private System.Windows.Forms.GroupBox CountryListGroup;
    }
}
