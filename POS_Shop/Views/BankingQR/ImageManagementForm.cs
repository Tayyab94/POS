
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.BankingQR
{
    public partial class ImageManagementForm : Form
    {
        private readonly string imageFolderPath;

        // UI Controls
        private TextBox txtImageName;
        private TextBox txtImagePath;
        private Button btnBrowse;
        private Button btnUpload;
        private TableLayoutPanel tlpImageGrid;
        private Label lblStatus;

        public ImageManagementForm()
        {
            InitializeComponent();
            imageFolderPath = Path.Combine(Application.StartupPath, "QRImages");
            CreateImageFolderIfNotExists();
            LoadImages();
        }

        private void InitializeComponent()
        {
            this.Text = "POS QR Manager";
            this.Size = new Size(950, 700);          // Reduced height
            this.MinimumSize = new Size(800, 600);   // Prevent excessive shrinking
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            // Upload GroupBox
            GroupBox grpUpload = new GroupBox()
            {
                Text = "Upload Image",
                Location = new Point(12, 12),
                Size = new Size(926, 120),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            Label lblName = new Label() { Text = "Image Name:", Location = new Point(15, 35), Size = new Size(90, 25), Font = new Font("Segoe UI", 9) };
            txtImageName = new TextBox() { Location = new Point(110, 33), Size = new Size(200, 25), Font = new Font("Segoe UI", 9) };

            Label lblPath = new Label() { Text = "Image File:", Location = new Point(15, 70), Size = new Size(90, 25), Font = new Font("Segoe UI", 9) };
            txtImagePath = new TextBox() { Location = new Point(110, 68), Size = new Size(500, 25), ReadOnly = true, BackColor = Color.White, Font = new Font("Segoe UI", 9) };

            btnBrowse = new Button() { Text = "Browse...", Location = new Point(620, 67), Size = new Size(100, 28), BackColor = Color.LightSteelBlue, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9) };
            btnBrowse.Click += BtnBrowse_Click;

            btnUpload = new Button() { Text = "Upload Image", Location = new Point(730, 67), Size = new Size(110, 28), BackColor = Color.FromArgb(30, 58, 138), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9, FontStyle.Bold) };
            btnUpload.Click += BtnUpload_Click;

            grpUpload.Controls.AddRange(new Control[] { lblName, txtImageName, lblPath, txtImagePath, btnBrowse, btnUpload });

            // Image List GroupBox – height reduced to fit smaller form
            GroupBox grpImageList = new GroupBox()
            {
                Text = "QR List",
                Location = new Point(12, 140),
                Size = new Size(926, 480),            // Was 580
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            tlpImageGrid = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                ColumnCount = 2,
                RowCount = 0,
                Padding = new Padding(10),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            tlpImageGrid.ColumnStyles.Clear();
            tlpImageGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpImageGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            tlpImageGrid.RowStyles.Clear();
            tlpImageGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            grpImageList.Controls.Add(tlpImageGrid);

            // Status label – repositioned to match new form height
            lblStatus = new Label()
            {
                Text = "Ready",
                Location = new Point(12, 630),        // Adjusted from 730
                Size = new Size(926, 20),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.Gray
            };

            this.Controls.AddRange(new Control[] { grpUpload, grpImageList, lblStatus });
        }

        private void CreateImageFolderIfNotExists()
        {
            if (!Directory.Exists(imageFolderPath))
                Directory.CreateDirectory(imageFolderPath);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = ofd.FileName;
                    if (string.IsNullOrWhiteSpace(txtImageName.Text))
                        txtImageName.Text = Path.GetFileNameWithoutExtension(ofd.FileName);
                }
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtImageName.Text))
            {
                MessageBox.Show("Please enter an image name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtImagePath.Text) || !File.Exists(txtImagePath.Text))
            {
                MessageBox.Show("Please select a valid image file.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string imageName = SanitizeFileName(txtImageName.Text.Trim());
            if (string.IsNullOrEmpty(imageName))
            {
                MessageBox.Show("Image name contains invalid characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string extension = Path.GetExtension(txtImagePath.Text);
            string targetFileName = imageName + extension;
            string targetPath = Path.Combine(imageFolderPath, targetFileName);

            if (File.Exists(targetPath))
            {
                DialogResult result = MessageBox.Show($"Image '{targetFileName}' already exists. Do you want to replace it?",
                    "Duplicate File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;
            }

            try
            {
                File.Copy(txtImagePath.Text, targetPath, true);
                lblStatus.Text = $"Successfully uploaded: {targetFileName}";
                lblStatus.ForeColor = Color.Green;
                txtImageName.Clear();
                txtImagePath.Clear();
                LoadImages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error uploading image: {ex.Message}", "Upload Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Upload failed.";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private string SanitizeFileName(string fileName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c.ToString(), "");
            return fileName.Trim();
        }

        private void LoadImages()
        {
            tlpImageGrid.Controls.Clear();
            tlpImageGrid.RowCount = 0;
            tlpImageGrid.RowStyles.Clear();

            if (!Directory.Exists(imageFolderPath)) return;

            string[] imageFiles = Directory.GetFiles(imageFolderPath, "*.*")
                .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                .ToArray();

            if (imageFiles.Length == 0)
            {
                Label noImagesLabel = new Label()
                {
                    Text = "No images found. Upload your first image!",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10, FontStyle.Italic),
                    ForeColor = Color.Gray
                };
                tlpImageGrid.Controls.Add(noImagesLabel, 0, 0);
                tlpImageGrid.SetColumnSpan(noImagesLabel, 2);
                return;
            }

            int row = 0;
            for (int i = 0; i < imageFiles.Length; i += 2)
            {
                Panel card1 = CreateImageCard(imageFiles[i]);
                tlpImageGrid.Controls.Add(card1, 0, row);

                if (i + 1 < imageFiles.Length)
                {
                    Panel card2 = CreateImageCard(imageFiles[i + 1]);
                    tlpImageGrid.Controls.Add(card2, 1, row);
                }
                else
                {
                    Panel emptyPanel = new Panel() { Size = new Size(1, 1) };
                    tlpImageGrid.Controls.Add(emptyPanel, 1, row);
                }

                row++;
            }

            tlpImageGrid.RowCount = row;
            for (int r = 0; r < row; r++)
                tlpImageGrid.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            lblStatus.Text = $"Loaded {imageFiles.Length} image(s)";
            lblStatus.ForeColor = Color.Gray;
        }

        private Panel CreateImageCard(string imagePath)
        {
            Panel itemPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(8),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Height = 300   // Same card height, scrolling will appear if needed
            };

            PictureBox picBox = new PictureBox()
            {
                Location = new Point(10, 10),
                Size = new Size(180, 180),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.LightGray,
                Anchor = AnchorStyles.Top
            };
            picBox.Left = (itemPanel.Width - picBox.Width) / 2;
            itemPanel.Resize += (s, e) => picBox.Left = (itemPanel.Width - picBox.Width) / 2;

            try
            {
                using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    picBox.Image = Image.FromStream(fs);
            }
            catch
            {
                picBox.Image = null;
                picBox.BackColor = Color.LightPink;
            }

            string displayName = Path.GetFileNameWithoutExtension(imagePath);
            Label lblImageName = new Label()
            {
                Text = displayName,
                Location = new Point(5, 200),
                Width = itemPanel.Width - 10,
                Height = 35,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoEllipsis = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            itemPanel.Resize += (s, e) => lblImageName.Width = itemPanel.Width - 10;

            Button btnDelete = new Button()
            {
                Text = "Delete",
                Location = new Point(40, 245),
                Size = new Size(120, 35),
                BackColor = Color.IndianRed,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Tag = imagePath,
                Anchor = AnchorStyles.Top
            };
            btnDelete.Click += BtnDelete_Click;

            itemPanel.Controls.AddRange(new Control[] { picBox, lblImageName, btnDelete });
            return itemPanel;
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            Button deleteBtn = sender as Button;
            string filePath = deleteBtn?.Tag as string;
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Image file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadImages();
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete '{Path.GetFileName(filePath)}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    File.Delete(filePath);
                    lblStatus.Text = $"Deleted: {Path.GetFileName(filePath)}";
                    lblStatus.ForeColor = Color.Orange;
                    LoadImages();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting file: {ex.Message}", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Delete failed.";
                    lblStatus.ForeColor = Color.Red;
                }
            }
        }
    }
}