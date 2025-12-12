using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using Rectangle = System.Drawing.Rectangle;

namespace POS_Shop.Helpers
{
    public class PrintToPdfGenerator
    {
        private DataGridView _dataGrid;
        private string customerName;
        private string invoiceNumber;
        private decimal _grandTotal;
        private decimal _subTotal;
        private bool _isFirstPage = true;

        private decimal receivedAmount=0m;  
        private int _currentRow = 0;
        private readonly StringFormat _centerFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        private readonly StringFormat _leftFormat = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        private readonly StringFormat _rightFormat = new StringFormat() { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        public void GenerateInvoice(DataGridView dataGrid, string filePath, string customerName,
            string invoiceNo, string totalAmount, string receivedAmount)
        {
            this.customerName = customerName;
            this.invoiceNumber = invoiceNo;
            _dataGrid = dataGrid;
            _grandTotal = 0;
            _subTotal = 0;
            _currentRow = 0;
            this.receivedAmount= decimal.TryParse(receivedAmount, out decimal rcv) ? rcv : 0m;
            CalculateTotals();

            PrintDocument pd = new PrintDocument();

            // Use standard 80mm width and long enough height to allow printing continuation
            PaperSize paperSize = new PaperSize("Custom", 330, 1200);
            pd.DefaultPageSettings.PaperSize = paperSize;
            pd.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);

            pd.PrintPage += new PrintPageEventHandler(PrintPageThermal);

            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            pd.PrinterSettings.PrintToFile = true;
            pd.PrinterSettings.PrintFileName = filePath;

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"پی ڈی ایف بنانے میں خرابی: {ex.Message}", "خرابی", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintPageThermal(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // Fonts
            Font titleFont = new Font("Arial", 10, FontStyle.Bold);
            Font headerFont = new Font("Arial", 8, FontStyle.Bold);
            Font normalFont = new Font("Arial", 8);
            Font productNameFont = new Font("Arial", 9);
            Font boldFont = new Font("Arial", 8, FontStyle.Bold);
            Font totalFont = new Font("Arial", 9, FontStyle.Bold);

            Rectangle pageBounds = e.PageBounds;
            float leftMargin = 5;
            float rightMargin = pageBounds.Width - 10;
            float centerX = pageBounds.Width / 2;
            float yPos = 20;


            if (_isFirstPage)
            {
                g.DrawString("انوائس", titleFont, Brushes.Black, centerX, yPos, _centerFormat);
                yPos += 20;
                g.DrawString($"تاریخ: {DateTime.Now:dd/MM/yyyy}", normalFont, Brushes.Black, rightMargin, yPos, _rightFormat);
                yPos += 12;
                g.DrawString($"انوائس نمبر: {invoiceNumber}", normalFont, Brushes.Black, rightMargin, yPos, _rightFormat);
                yPos += 12;

                //string cName= string.IsNullOrEmpty(customerName)==true? "" : customerName.Split('-')[1].Trim();
                g.DrawString($"گاہک: {customerName}", normalFont, Brushes.Black, rightMargin, yPos, _rightFormat);
                yPos += 15;
                g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
                yPos += 10;
            }
            else
            {
                // if not first page, skip some space for consistency
                yPos = 20;
            }
            // ===== TABLE HEADER =====
            float col1Left = leftMargin;
            float col2Left = col1Left + 70;
            float col3Left = col2Left + 70;

            g.DrawString("رقم", headerFont, Brushes.Black, col1Left, yPos, _leftFormat);
            g.DrawString("قیمت", headerFont, Brushes.Black, col2Left, yPos, _leftFormat);
            g.DrawString("مقدار", headerFont, Brushes.Black, col3Left, yPos, _leftFormat);
            yPos += 12;
            g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 8;

            // ===== DATA ROWS =====
            float rowHeight = 28; // Height for each item (product + detail line)
            int rowsPerPage = 30; // around 30 items per page

            int rowsPrinted = 0;
            for (; _currentRow < _dataGrid.Rows.Count; _currentRow++)
            {
                var row = _dataGrid.Rows[_currentRow];
                if (row.IsNewRow) continue;

                string urduName = row.Cells["Urdu Name"].Value?.ToString()?.Trim() ?? "پرڈکٹ";
                string qty = row.Cells["Qty"].Value?.ToString() ?? "0";
                string price = row.Cells["SalePrice"].Value?.ToString() ?? "0";
                string type = row.Cells["ProductType"].Value?.ToString() ?? "";

                int qtyValue = int.TryParse(qty, out int q) ? q : 0;
                decimal priceValue = decimal.TryParse(price, out decimal p) ? p : 0m;
                decimal amount = priceValue * qtyValue;

                //if (urduName.Length > 35)
                //    urduName = urduName.Substring(0, 35) + "...";

                string finalName = TextFormatHelper.FormatMixedText(urduName);   
                // Product name
                g.DrawString(finalName, productNameFont, Brushes.Black,
                    new RectangleF(leftMargin, yPos, rightMargin - leftMargin, 13), _rightFormat);
                yPos += 19;

                // Price, Amount, Qty
                g.DrawString(amount.ToString("F0"), normalFont, Brushes.Black, col1Left, yPos, _leftFormat);
                g.DrawString(priceValue.ToString("F0"), normalFont, Brushes.Black, col2Left, yPos, _leftFormat);

               string miqdaar= TextFormatHelper.FormatMixedText($"{type} {qtyValue} ");
                g.DrawString($"{miqdaar} ", normalFont, Brushes.Black, col3Left, yPos, _leftFormat);
                yPos += 12;

                // Line separator
                g.DrawLine(new Pen(Color.Gray, 0.5f), leftMargin, yPos, rightMargin, yPos);
                yPos += 2;

                rowsPrinted++;

                // When we reach page limit, continue to next page
                if (rowsPrinted >= rowsPerPage && _currentRow < _dataGrid.Rows.Count - 1)
                {
                    _currentRow++; // Move to next row for next page
                    _isFirstPage = false;

                    e.HasMorePages = true;
                    return;
                }
            }

            // ===== TOTALS (only on last page) =====
            yPos += 10;
            g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 10;

            g.DrawString($"کل رقم: {_subTotal:F2}", boldFont, Brushes.Black, leftMargin, yPos, _leftFormat);
            yPos += 14;
            g.DrawString($"وصول رقم: {receivedAmount:F2}", totalFont, Brushes.Black, leftMargin, yPos, _leftFormat);
            yPos += 20;

            decimal change = receivedAmount - _subTotal;
            g.DrawString($"بقایا: {change:F0}", totalFont, Brushes.Black, leftMargin, yPos, _leftFormat);
            yPos += 20;

            g.DrawLine(Pens.Black, leftMargin, yPos, rightMargin, yPos);
            yPos += 12;
            g.DrawString("آپ کے کاروبار کا شکریہ", normalFont, Brushes.Black, centerX, yPos, _centerFormat);

            e.HasMorePages = false;
            _isFirstPage = false;
        }

        private void CalculateTotals()
        {
            foreach (DataGridViewRow row in _dataGrid.Rows)
            {
                if (row.IsNewRow) continue;
                string qty = row.Cells["Qty"].Value?.ToString() ?? "0";
                string price = row.Cells["SalePrice"].Value?.ToString() ?? "0";

                if (decimal.TryParse(qty, out decimal q) && decimal.TryParse(price, out decimal p))
                    _subTotal += q * p;
            }
            _grandTotal = _subTotal;
        }

    }
}
