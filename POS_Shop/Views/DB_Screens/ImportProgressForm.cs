using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.DB_Screens
{
    public partial class ImportProgressForm : Form
    {
        private ProgressBar progressBar;
        private Label lblCurrentAction;
        private Label lblProgress;
        private Button btnCancel;

        public bool IsCancelled { get; private set; }

        public ImportProgressForm()
        {
            InitializeComponent();
            IsCancelled = false;
        }

        private void InitializeComponent()
        {
            this.Text = "Importing...";
            this.Size = new Size(500, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // Current Action Label
            lblCurrentAction = new Label
            {
                Text = "Starting import...",
                Location = new Point(20, 20),
                Size = new Size(460, 25),
                Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 123, 255)
            };

            // Progress Label
            lblProgress = new Label
            {
                Text = "Progress: 0%",
                Location = new Point(20, 55),
                Size = new Size(460, 20),
                Font = new Font("Microsoft Sans Serif", 9F)
            };

            // Progress Bar
            progressBar = new ProgressBar
            {
                Location = new Point(20, 85),
                Size = new Size(460, 30),
                Style = ProgressBarStyle.Continuous,
                Minimum = 0,
                Maximum = 100,
                Value = 0
            };

            // Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel Import",
                Location = new Point(200, 130),
                Size = new Size(100, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) =>
            {
                IsCancelled = true;
                this.Close();
            };

            this.Controls.Add(lblCurrentAction);
            this.Controls.Add(lblProgress);
            this.Controls.Add(progressBar);
            this.Controls.Add(btnCancel);
        }

        public void UpdateProgress(string action, int current, int total)
        {
            if (this.IsDisposed || this.Disposing)
                return;

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateProgress(action, current, total)));
                return;
            }

            lblCurrentAction.Text = action;

            if (total > 0)
            {
                int percentage = (int)((current / (double)total) * 100);
                lblProgress.Text = $"Progress: {current} of {total} ({percentage}%)";
                progressBar.Value = Math.Min(percentage, 100);
            }
            else
            {
                lblProgress.Text = $"Progress: {current} items";
                progressBar.Value = 0;
            }

            Application.DoEvents();
        }

        public void UpdateMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateMessage(message)));
                return;
            }

            lblCurrentAction.Text = message;
            Application.DoEvents();
        }
    }
}
