using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    /// <summary>
    /// Shared helpers used by every report form.
    /// </summary>
    internal static class ReportBase
    {
        // ── Accent colours ─────────────────────────────────────────────────────
        public static readonly Color Blue = Color.FromArgb(21, 101, 192);
        public static readonly Color Green = Color.FromArgb(46, 125, 50);
        public static readonly Color Red = Color.FromArgb(198, 40, 40);
        public static readonly Color Orange = Color.FromArgb(230, 81, 0);
        public static readonly Color Purple = Color.FromArgb(106, 27, 154);
        public static readonly Color Teal = Color.FromArgb(0, 105, 92);
        public static readonly Color Brown = Color.FromArgb(109, 76, 65);

        // ── Cell styles ────────────────────────────────────────────────────────
        public static DataGridViewCellStyle HdrStyle(Color accent) => new DataGridViewCellStyle
        {
            BackColor = accent,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            SelectionBackColor = accent,
            SelectionForeColor = Color.White
        };

        public static DataGridViewCellStyle CellStyle() => new DataGridViewCellStyle
        {
            Font = new Font("Segoe UI", 9.5f),
            BackColor = Color.White,
            SelectionBackColor = Color.FromArgb(187, 222, 251),
            SelectionForeColor = Color.FromArgb(13, 71, 161)
        };

        public static DataGridViewCellStyle AltStyle() =>
            new DataGridViewCellStyle { BackColor = Color.FromArgb(245, 249, 255) };

        public static DataGridViewCellStyle NumStyle() => new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleRight,
            Format = "N2",
            Font = new Font("Segoe UI", 9.5f)
        };

        public static DataGridViewCellStyle NumBoldStyle(Color fg) => new DataGridViewCellStyle
        {
            Alignment = DataGridViewContentAlignment.MiddleRight,
            Format = "N2",
            Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
            ForeColor = fg
        };

        // ── Standard grid setup ────────────────────────────────────────────────
        public static void StyleGrid(DataGridView g, Color accent)
        {
            g.AllowUserToAddRows = false;
            g.AllowUserToDeleteRows = false;
            g.AllowUserToResizeRows = false;
            g.ReadOnly = true;
            g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            g.BackgroundColor = Color.White;
            g.BorderStyle = BorderStyle.None;
            g.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            g.GridColor = Color.FromArgb(236, 239, 241);
            g.ColumnHeadersDefaultCellStyle = HdrStyle(accent);
            g.ColumnHeadersHeight = 40;
            g.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            g.DefaultCellStyle = CellStyle();
            g.AlternatingRowsDefaultCellStyle = AltStyle();
            g.EnableHeadersVisualStyles = false;
            g.MultiSelect = false;
            g.RowHeadersVisible = false;
            g.RowTemplate.Height = 36;
            g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            g.Dock = DockStyle.Fill;
        }

        // ── Grand-total row painter ────────────────────────────────────────────
        public static void StyleTotalRow(DataGridViewRow row, Color bg)
        {
            foreach (DataGridViewCell c in row.Cells)
            {
                c.Style.BackColor = bg;
                c.Style.ForeColor = Color.White;
                c.Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
        }

        // ── Button hover ───────────────────────────────────────────────────────
        public static void Hover(Button b, Color on, Color off)
        {
            b.MouseEnter += (s, e) => b.BackColor = on;
            b.MouseLeave += (s, e) => b.BackColor = off;
        }

        // ── Print-preview ──────────────────────────────────────────────────────
        public static void PrintGrid(DataGridView dgv, string title, IWin32Window owner = null)
        {
            var pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += (s, e) =>
            {
                var gr = e.Graphics;
                var acc = new SolidBrush(Blue);
                int x = 30, y = 28;
                gr.DrawString(title.Trim(), new Font("Segoe UI", 11f, FontStyle.Bold), acc, x, y); y += 24;
                gr.DrawString("Printed: " + DateTime.Now.ToString("dd MMM yyyy  HH:mm"),
                    new Font("Segoe UI", 8f), Brushes.Gray, x, y); y += 18;
                gr.DrawLine(Pens.LightGray, x, y, 780, y); y += 8;

                float total = 750f;
                float fill = dgv.Columns.Cast<DataGridViewColumn>().Sum(c => c.FillWeight);
                float[] w = dgv.Columns.Cast<DataGridViewColumn>()
                               .Select(c => c.FillWeight / fill * total).ToArray();

                var hf = new Font("Segoe UI", 8.5f, FontStyle.Bold);
                var cf = new Font("Segoe UI", 8.5f);
                float cx = x;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    gr.FillRectangle(acc, cx, y, w[i], 20);
                    gr.DrawString(dgv.Columns[i].HeaderText, hf, Brushes.White, cx + 2, y + 2);
                    cx += w[i];
                }
                y += 22;

                bool alt = false;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (y > e.PageBounds.Height - 40) break;
                    gr.FillRectangle(alt ? new SolidBrush(Color.FromArgb(245, 249, 255)) : Brushes.White,
                        x, y, total, 18);
                    cx = x;
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        gr.DrawString(row.Cells[i].FormattedValue?.ToString() ?? "",
                            cf, Brushes.Black, cx + 2, y + 1);
                        cx += w[i];
                    }
                    y += 18; alt = !alt;
                }
                acc.Dispose(); hf.Dispose(); cf.Dispose();
            };
            new PrintPreviewDialog
            {
                Document = pd,
                Width = 1000,
                Height = 720,
                StartPosition = FormStartPosition.CenterScreen
            }.ShowDialog(owner);
        }
    }
}