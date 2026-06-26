using ClosedXML.Excel;
using POS_Shop.Models.LoanModelsV1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreensV1
{
    /// <summary>
    /// Print or export the customer ledger statement.
    /// Supports: Print Preview, Direct Print, Export to Excel.
    /// </summary>
    public partial class Customerledgerreportform : Form
    {
        // ─── Fields ───────────────────────────────────────────────────────────
        private readonly int _customerId;
        private readonly string _customerName;
        private readonly DateTime _from;
        private readonly DateTime _to;
        private readonly List<CustomerLedgerRow> _rows;

        private PrintDocument _printDocument;
        private List<string[]> _printRows;
        private int _printPageIndex;
        private const int RowsPerPage = 25;

        private readonly string[] _colHeaders =
            { "Date", "Type", "Debit (PKR)", "Credit (PKR)", "Balance (PKR)", "Status", "Note" };
        private readonly int[] _colWidths = { 85, 150, 95, 95, 105, 65, 170 };

        // ─── Constructor ─────────────────────────────────────────────────────
        public Customerledgerreportform(
            int customerId, string customerName,
            DateTime from, DateTime to, List<CustomerLedgerRow> rows)
        {
            InitializeComponent();
            _customerId = customerId;
            _customerName = customerName;
            _from = from;
            _to = to;
            _rows = rows;

            this.Size = new Size(650, 420);
            this.MinimumSize = new Size(650, 420);
            this.MaximumSize = new Size(650, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.Load += Customerledgerreportform_Load;
            this.Shown += (s, e) => this.CenterToParent();
        }

        // ─── Load ─────────────────────────────────────────────────────────────
        private void Customerledgerreportform_Load(object sender, EventArgs e)
        {
            try
            {
                lblReportTitle.Text = $"Ledger Statement — {_customerName}";
                lblDateRange.Text = $"{_from:dd-MMM-yyyy}  to  {_to:dd-MMM-yyyy}";
                lblEntries.Text = $"Total Entries: {_rows.Count}";

                decimal totalDebit = _rows.Sum(r => r.Debit);
                decimal totalCredit = _rows.Sum(r => r.Credit);
                decimal lastBalance = _rows.Any() ? _rows.Last().Balance : 0;

                lblSummary.Text =
                    $"Total Debited: PKR {totalDebit:N2}   |   " +
                    $"Total Credited: PKR {totalCredit:N2}   |   " +
                    $"Closing Balance: PKR {Math.Abs(lastBalance):N2} " +
                    (lastBalance > 0 ? "LOAN" : lastBalance < 0 ? "ADVANCE" : "CLEAR");

                lblSummary.Height = 40;

                BuildPrintDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Build Print Document ─────────────────────────────────────────────
        private void BuildPrintDocument()
        {
            try
            {
                _printRows = _rows.Select(r => new string[]
                {
                    r.EntryDate.ToString("dd-MMM-yyyy"),
                    r.EntryTypeDisplay.Contains("Adjustment") ? "Loan" : "Advance Deposite",
                    r.DebitDisplay,
                    r.CreditDisplay,
                    r.BalanceDisplay,
                    r.BalanceTypeDisplay,
                    r.Note ?? ""
                }).ToList();

                _printDocument = new PrintDocument();
                _printDocument.DefaultPageSettings.Landscape = true;
                _printDocument.DefaultPageSettings.PaperSize = new PaperSize("A4", 1169, 827);
                _printDocument.BeginPrint += (s, e) => _printPageIndex = 0;
                _printDocument.PrintPage += PrintDocument_PrintPage;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error building print document:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Print Page ───────────────────────────────────────────────────────
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                if (_printRows == null || _printRows.Count == 0)
                {
                    e.HasMorePages = false;
                    return;
                }

                Graphics g = e.Graphics;
                float margin = 40;
                float y = margin;
                float pageWidth = e.PageBounds.Width - margin * 2;
                float pageHeight = e.PageBounds.Height - margin * 2;
                float usablePageHeight = pageHeight - 50;   // reserve space for footer

                // ── Header ───────────────────────────────────────────────────
                using (var headerFont = new Font("Segoe UI", 14, FontStyle.Bold))
                using (var subFont = new Font("Segoe UI", 9))
                using (var grayBrush = new SolidBrush(Color.Gray))
                using (var darkBrush = new SolidBrush(Color.FromArgb(44, 62, 80)))
                {
                    g.DrawString("Customer Ledger Statement", headerFont, darkBrush, margin, y);
                    y += 28;

                    g.DrawString($"Customer: {_customerName}", subFont, Brushes.Black, margin, y);
                    g.DrawString($"Period: {_from:dd-MMM-yyyy} to {_to:dd-MMM-yyyy}",
                        subFont, grayBrush, margin + 250, y);
                    g.DrawString($"Printed: {DateTime.Now:dd-MMM-yyyy HH:mm}",
                        subFont, grayBrush, margin + 550, y);
                    y += 18;
                }

                // ── Separator ────────────────────────────────────────────────
                using (var pen = new Pen(Color.FromArgb(44, 62, 80), 1.5f))
                    g.DrawLine(pen, margin, y, margin + pageWidth, y);
                y += 10;

                // ── KPI Row ───────────────────────────────────────────────────
                decimal totalDebit = _rows.Sum(r => r.Debit);
                decimal totalCredit = _rows.Sum(r => r.Credit);
                decimal lastBal = _rows.Any() ? _rows.First().Balance : 0;

                using (var kpiFont = new Font("Segoe UI", 9, FontStyle.Bold))
                {
                    g.DrawString($"Total Debited: PKR {totalDebit:N2}",
                        kpiFont, Brushes.DarkRed, margin, y);
                    g.DrawString($"Total Credited: PKR {totalCredit:N2}",
                        kpiFont, Brushes.DarkGreen, margin + 250, y);

                    string balStr = lastBal > 0 ? $"Loan: PKR {lastBal:N2}"
                                  : lastBal < 0 ? $"Advance: PKR {Math.Abs(lastBal):N2}"
                                  : "Fully Settled";
                    Brush balBrush = lastBal > 0 ? Brushes.DarkRed
                                   : lastBal < 0 ? Brushes.DarkBlue
                                   : Brushes.DarkGreen;
                    g.DrawString($"Closing Balance: {balStr}", kpiFont, balBrush, margin + 520, y);
                }
                y += 22;

                // ── Table Header ──────────────────────────────────────────────
                using (var hdrFont = new Font("Segoe UI", 8, FontStyle.Bold))
                using (var hdrBg = new SolidBrush(Color.FromArgb(44, 62, 80)))
                using (var hdrFg = new SolidBrush(Color.White))
                {
                    g.FillRectangle(hdrBg, margin, y, pageWidth, 22);
                    float x = margin + 4;
                    for (int i = 0; i < _colHeaders.Length; i++)
                    {
                        g.DrawString(_colHeaders[i], hdrFont, hdrFg, x, y + 4);
                        x += _colWidths[i];
                    }
                }
                y += 22;

                // ── Data Rows ─────────────────────────────────────────────────
                int start = _printPageIndex * RowsPerPage;
                int end = Math.Min(start + RowsPerPage, _printRows.Count);
                int rowsPrinted = 0;

                using (var rowFont = new Font("Segoe UI", 8))
                using (var altBg = new SolidBrush(Color.FromArgb(250, 250, 250)))
                using (var linePen = new Pen(Color.FromArgb(220, 220, 220)))
                {
                    for (int i = start; i < end; i++)
                    {
                        if (y + 22 > usablePageHeight)
                        {
                            e.HasMorePages = true;
                            break;
                        }

                        var cols = _printRows[i];
                        var row = _rows[i];

                        // Alternating row background
                        if (i % 2 == 1)
                            g.FillRectangle(altBg, margin, y, pageWidth, 20);

                        float x = margin + 4;
                        for (int c = 0; c < cols.Length; c++)
                        {
                            // ── Cell text colour ──────────────────────────────
                            Brush fg = Brushes.Black;
                            if (c == 2 && row.Debit > 0) fg = Brushes.DarkRed;
                            if (c == 3 && row.Credit > 0) fg = Brushes.DarkGreen;
                            if (c == 4)
                            {
                                fg = row.Balance > 0 ? Brushes.DarkRed
                                   : row.Balance < 0 ? Brushes.DarkBlue
                                   : Brushes.DarkGreen;
                            }

                            // ── Truncate text if too wide for column ──────────
                            string text = cols[c];
                            var textSize = g.MeasureString(text, rowFont);
                            if (textSize.Width > _colWidths[c] - 8)
                            {
                                while (text.Length > 3 &&
                                       g.MeasureString(text + "...", rowFont).Width > _colWidths[c] - 8)
                                    text = text.Substring(0, text.Length - 1);
                                text += "...";
                            }

                            var clipRect = new RectangleF(x, y + 2, _colWidths[c] - 4, 18);
                            g.DrawString(text, rowFont, fg, clipRect);

                            // ── Circle around first row Balance value only ────
                            // Marks the opening/first balance so it stands out.
                            // Sized by measuring the actual text — handles any number of digits.
                            if (i == 0 && c == 4)
                            {
                                // Measure the exact rendered text width (no padding tricks)
                                var sz = g.MeasureString(cols[c], rowFont);

                                float padX = 5f;   // horizontal space left+right of text
                                float padY = 3f;   // vertical space top+bottom of text

                                float circleW = sz.Width + padX * 2;
                                float circleH = sz.Height + padY * 2;

                                // Align circle to where the text actually starts (clipRect.X - padX)
                                // clipRect starts at x (column start), text is drawn from there
                                float circleX = clipRect.X - padX;
                                float circleY = y + (20 - circleH) / 2f;

                                Color circleColor = row.Balance > 0 ? Color.DarkRed
                                                  : row.Balance < 0 ? Color.DarkBlue
                                                  : Color.DarkGreen;

                                using (var circlePen = new Pen(circleColor, 1.5f))
                                    g.DrawEllipse(circlePen, circleX, circleY, circleW, circleH);
                            }

                            x += _colWidths[c];
                        }

                        g.DrawLine(linePen, margin, y + 20, margin + pageWidth, y + 20);
                        y += 20;
                        rowsPrinted++;
                    }
                }

                // ── Page Footer ───────────────────────────────────────────────
                using (var footFont = new Font("Segoe UI", 8))
                {
                    int totalPages = (int)Math.Ceiling(_printRows.Count / (double)RowsPerPage);
                    string pageStr = $"Page {_printPageIndex + 1} of {totalPages}";
                    var sz = g.MeasureString(pageStr, footFont);
                    g.DrawString(pageStr, footFont, Brushes.Gray,
                        margin + pageWidth - sz.Width,
                        e.PageBounds.Height - 30);
                }

                if (rowsPrinted > 0)
                    _printPageIndex++;

                e.HasMorePages = start + rowsPrinted < _printRows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Printing error:\n{ex.Message}", "Print Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.HasMorePages = false;
            }
        }

        // ─── Button Handlers ──────────────────────────────────────────────────

        private void PrintPreviewBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _printPageIndex = 0;
                using (var dlg = new PrintPreviewDialog())
                {
                    dlg.Document = _printDocument;
                    dlg.WindowState = FormWindowState.Maximized;
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Print preview error:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _printPageIndex = 0;
                using (var pd = new PrintDialog())
                {
                    pd.Document = _printDocument;
                    pd.UseEXDialog = true;
                    if (pd.ShowDialog() == DialogResult.OK)
                        _printDocument.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Print error:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportExcelBtn_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel Files|*.xlsx";
                dlg.FileName = $"Ledger_{_customerName}_{DateTime.Today:yyyyMMdd}.xlsx";
                dlg.Title = "Save Ledger Report";
                dlg.DefaultExt = "xlsx";

                if (dlg.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Ledger");

                        // ── Title rows ────────────────────────────────────────
                        ws.Cell(1, 1).Value = $"Customer Ledger Statement — {_customerName}";
                        ws.Range(1, 1, 1, 7).Merge().Style
                            .Font.SetBold(true).Font.SetFontSize(14)
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        ws.Cell(2, 1).Value = $"Period: {_from:dd-MMM-yyyy} to {_to:dd-MMM-yyyy}";
                        ws.Range(2, 1, 2, 7).Merge().Style
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        ws.Cell(3, 1).Value = $"Generated: {DateTime.Now:dd-MMM-yyyy HH:mm}";
                        ws.Range(3, 1, 3, 7).Merge().Style
                            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                            .Font.SetItalic(true);

                        // ── Column headers ────────────────────────────────────
                        var headers = new[]
                            { "Date", "Type", "Debit (PKR)", "Credit (PKR)", "Balance (PKR)", "Status", "Note" };
                        for (int i = 0; i < headers.Length; i++)
                        {
                            var cell = ws.Cell(5, i + 1);
                            cell.Value = headers[i];
                            cell.Style.Font.SetBold(true)
                                .Fill.SetBackgroundColor(XLColor.FromArgb(44, 62, 80))
                                .Font.SetFontColor(XLColor.White)
                                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                                .Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                        }

                        // ── Data rows ─────────────────────────────────────────
                        for (int r = 0; r < _rows.Count; r++)
                        {
                            int row = r + 6;
                            var ld = _rows[r];

                            ws.Cell(row, 1).Value = ld.EntryDate.ToString("dd-MMM-yyyy");
                            ws.Cell(row, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            ws.Cell(row, 2).Value = ld.EntryTypeDisplay.Contains("Adjustment") ? "Loan" : ld.EntryTypeDisplay;

                            if (ld.Debit > 0)
                            {
                                ws.Cell(row, 3).Value = ld.Debit;
                                ws.Cell(row, 3).Style.Font.SetFontColor(XLColor.DarkRed);
                            }
                            else ws.Cell(row, 3).Value = Blank.Value;

                            if (ld.Credit > 0)
                            {
                                ws.Cell(row, 4).Value = ld.Credit;
                                ws.Cell(row, 4).Style.Font.SetFontColor(XLColor.DarkGreen);
                            }
                            else ws.Cell(row, 4).Value = Blank.Value;

                            ws.Cell(row, 3).Style.NumberFormat.Format = "#,##0.00";
                            ws.Cell(row, 4).Style.NumberFormat.Format = "#,##0.00";

                            ws.Cell(row, 5).Value = Math.Abs(ld.Balance);
                            ws.Cell(row, 5).Style.NumberFormat.Format = "#,##0.00";
                            ws.Cell(row, 5).Style.Font.SetFontColor(
                                ld.Balance > 0 ? XLColor.DarkRed :
                                ld.Balance < 0 ? XLColor.DarkBlue : XLColor.DarkGreen);

                            ws.Cell(row, 6).Value = ld.BalanceTypeDisplay;
                            ws.Cell(row, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                            ws.Cell(row, 7).Value = ld.Note ?? "";

                            // Alternate row shading
                            if (r % 2 == 1)
                                ws.Range(row, 1, row, 7).Style
                                    .Fill.SetBackgroundColor(XLColor.FromArgb(250, 250, 250));

                            // Row bottom border
                            ws.Range(row, 1, row, 7).Style
                                .Border.SetBottomBorder(XLBorderStyleValues.Thin)
                                .Border.SetBottomBorderColor(XLColor.FromArgb(220, 220, 220));
                        }

                        // ── Totals row ────────────────────────────────────────
                        int totRow = _rows.Count + 7;
                        ws.Cell(totRow, 1).Value = "TOTALS";

                        ws.Cell(totRow, 3).FormulaA1 = $"=SUM(C6:C{_rows.Count + 5})";
                        ws.Cell(totRow, 4).FormulaA1 = $"=SUM(D6:D{_rows.Count + 5})";

                        decimal lastBal = _rows.Any() ? _rows.First().Balance : 0;
                        ws.Cell(totRow, 5).Value = Math.Abs(lastBal);
                        ws.Cell(totRow, 6).Value = lastBal > 0 ? "LOAN" : lastBal < 0 ? "ADVANCE" : "CLEAR";

                        ws.Cell(totRow, 3).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell(totRow, 4).Style.NumberFormat.Format = "#,##0.00";
                        ws.Cell(totRow, 5).Style.NumberFormat.Format = "#,##0.00";

                        ws.Range(totRow, 1, totRow, 7).Style
                            .Font.SetBold(true)
                            .Border.SetTopBorder(XLBorderStyleValues.Medium)
                            .Border.SetTopBorderColor(XLColor.FromArgb(44, 62, 80));

                        ws.Columns().AdjustToContents();
                        wb.SaveAs(dlg.FileName);
                    }

                    var open = MessageBox.Show(
                        $"Exported to:\n{dlg.FileName}\n\nOpen the file now?",
                        "Export Successful", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (open == DialogResult.Yes)
                        System.Diagnostics.Process.Start(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed:\n{ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e) => this.Close();

        // ─── Cleanup ──────────────────────────────────────────────────────────
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (_printDocument != null)
            {
                _printDocument.PrintPage -= PrintDocument_PrintPage;
                _printDocument.Dispose();
                _printDocument = null;
            }
            base.OnFormClosing(e);
        }
    }
}
