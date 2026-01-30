using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.DB_Screens
{
    public partial class ErrorLogForm : Form
    {
        private List<string> errors;
        private TextBox txtErrors;
        private Button btnClose;
        private Button btnSaveLog;

        public ErrorLogForm(List<string> errors)
        {
            this.errors = errors;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Import Errors";
            this.Size = new Size(700, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            // Title Label
            var lblTitle = new Label
            {
                Text = $"Import Errors ({errors.Count} errors found)",
                Location = new Point(20, 20),
                Size = new Size(660, 25),
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 53, 69)
            };

            // Error Count Label
            var lblCount = new Label
            {
                Text = $"Total errors: {errors.Count}",
                Location = new Point(20, 55),
                Size = new Size(660, 20),
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular),
                ForeColor = Color.Gray
            };

            // Errors TextBox
            txtErrors = new TextBox
            {
                Location = new Point(20, 85),
                Size = new Size(660, 320),
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                ReadOnly = true,
                Font = new Font("Consolas", 9F),
                BackColor = Color.FromArgb(248, 249, 250)
            };

            // Populate errors
            txtErrors.Text = string.Join(Environment.NewLine + new string('-', 80) + Environment.NewLine, errors);

            // Save Log Button
            btnSaveLog = new Button
            {
                Text = "Save Error Log",
                Location = new Point(20, 420),
                Size = new Size(120, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
            };
            btnSaveLog.FlatAppearance.BorderSize = 0;
            btnSaveLog.Click += BtnSaveLog_Click;

            // Close Button
            btnClose = new Button
            {
                Text = "Close",
                Location = new Point(560, 420),
                Size = new Size(120, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblCount);
            this.Controls.Add(txtErrors);
            this.Controls.Add(btnSaveLog);
            this.Controls.Add(btnClose);
        }

        private void BtnSaveLog_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                sfd.FileName = $"Import_Errors_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                sfd.Title = "Save Error Log";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        System.IO.File.WriteAllText(sfd.FileName, txtErrors.Text);
                        MessageBox.Show("Error log saved successfully!",
                                      "Success",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}",
                                      "Error",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
