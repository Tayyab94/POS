using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Helpers
{
    /// <summary>
    /// Encapsulates all receipt printing logic for a temporary order.
    /// Completely decoupled from the UI control — pass data in, get a PrintDocument out.
    /// </summary>
    public class TempOrderPrintHelper
    {
        // ── Data ─────────────────────────────────────────────────────────────
        private readonly List<TempOrderDetail> _details;
        private readonly string _invoiceNo;
        private readonly string _customerName;
        private readonly decimal _receivedAmount;

        // ── Layout constants ──────────────────────────────────────────────────
        private const int PaperWidthHundredths = 280;   // 2.8 inches in hundredths
        private const int BaseHeightHundredths = 350;
        private const int ItemHeightHundredths = 30;
        private const int LeftMargin = 0;
        private const int LineHeight = 12;
        private const int SectionSpacing = 3;

        public TempOrderPrintHelper(
            List<TempOrderDetail> details,
            string invoiceNo,
            string customerName,
            decimal receivedAmount)
        {
            _details = details ?? throw new ArgumentNullException(nameof(details));
            _invoiceNo = invoiceNo;
            _customerName = customerName ?? string.Empty;
            _receivedAmount = receivedAmount;
        }

        /// <summary>
        /// Creates a ready-to-use PrintDocument. Caller owns disposal.
        /// </summary>
        public PrintDocument CreatePrintDocument()
        {
            var pd = new PrintDocument();
            pd.PrintPage += OnPrintPage;

            return pd;
        }

        // ── Private helpers ───────────────────────────────────────────────────

        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            // Dynamic paper height based on item count
            int totalHeight = Math.Max(
                400,
                BaseHeightHundredths + (_details.Count * ItemHeightHundredths));

            e.PageSettings.PaperSize = new PaperSize("Custom", PaperWidthHundredths, totalHeight);

            using (var titleFont = new Font("Arial", 11, FontStyle.Bold))
            using (var headerFont = new Font("Arial", 9, FontStyle.Bold))
            using (var regularFont = new Font("Arial", 9, FontStyle.Regular))
            using (var smallFont = new Font("Arial", 7, FontStyle.Regular))
            using (var urduFont = new Font("Nafees Web Naskh", 9, FontStyle.Regular))
            {
                var rightFmt = new StringFormat { Alignment = StringAlignment.Near };
                var centerFmt = new StringFormat { Alignment = StringAlignment.Center };
                string dashLine = new string('-', 82);

                int y = 5;

                // ── Header ────────────────────────────────────────────────────
                DrawLine(e, headerFont, "انوائس", y, rightFmt); y += LineHeight + 2;
                DrawLine(e, headerFont, $"کسٹمر: {_customerName}", y, rightFmt); y += LineHeight + 2;

                DrawAt(e, urduFont, "تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd"), LeftMargin, y, 190, LineHeight + 2, rightFmt);
                DrawAt(e, urduFont, "کل اشیاء :" + _details.Count, 190, y, PaperWidthHundredths, LineHeight + 2, rightFmt);
                y += LineHeight + 2;

                DrawLine(e, urduFont, $"انوائس :{_invoiceNo}", y, rightFmt); y += LineHeight + 2;

                e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, LeftMargin, y);
                y += LineHeight;

                // ── Column headers (black background) ─────────────────────────
                using (Brush blackBrush = new SolidBrush(Color.Black))
                    e.Graphics.FillRectangle(blackBrush, LeftMargin, y, PaperWidthHundredths, LineHeight + 3);

                DrawAt(e, headerFont, "قیمت", 0, y, 60, LineHeight, rightFmt, Brushes.White);
                DrawAt(e, headerFont, "ریٹ", 65, y, 50, LineHeight, rightFmt, Brushes.White);
                DrawAt(e, headerFont, "تعداد", 120, y, 100, LineHeight, rightFmt, Brushes.White);
                DrawAt(e, headerFont, "پروڈکٹ", 225, y, 50, LineHeight, rightFmt, Brushes.White);
                y += LineHeight + 3;

                e.Graphics.DrawLine(Pens.Black, LeftMargin, y, PaperWidthHundredths, y);
                y += 5;

                // ── Rows ──────────────────────────────────────────────────────
                foreach (var row in _details)
                {
                    decimal amount = (decimal)(row.Price * row.Quantity);
                    string productName = TextFormatHelper.FormatMixedText(row.ProductName);
                    string qtyFormatted = TextFormatHelper.FormatMixedText($"{row.QuantityType} {row.Quantity:0}");

                    // Product name (full width, right-aligned)
                    e.Graphics.DrawString(productName, regularFont, Brushes.Black,
                        new Rectangle(LeftMargin, y, PaperWidthHundredths - 5, LineHeight + 2),
                        new StringFormat { Alignment = StringAlignment.Far });

                    int detailY = y + LineHeight;
                    DrawAt(e, regularFont, $"{amount:0}", 0, detailY, 60, LineHeight, rightFmt);
                    DrawAt(e, regularFont, $"{(decimal)row.Price:0}", 65, detailY, 50, LineHeight, rightFmt);
                    DrawAt(e, urduFont, qtyFormatted, 120, detailY, 100, LineHeight, rightFmt);

                    y = detailY + LineHeight;
                    e.Graphics.DrawLine(Pens.Black, LeftMargin, y, PaperWidthHundredths, y);
                    y += 4;
                }

                // ── Totals ────────────────────────────────────────────────────
                y += SectionSpacing;
                decimal total = (decimal)_details.Sum(x => x.Quantity * x.Price);
                decimal change = _receivedAmount - total;

                DrawLine(e, urduFont, $"کل رقم: {total:0}", y, rightFmt); y += LineHeight + 4;
                DrawLine(e, headerFont, $"وصول رقم: {_receivedAmount:0}", y, rightFmt); y += LineHeight + 4;
                DrawLine(e, urduFont, $"بقایا: {change:0}", y, rightFmt); y += LineHeight + 4;

                // ── Footer ────────────────────────────────────────────────────
                DrawLine(e, headerFont, "ٹوٹے ہوۓ سامان کی واپسی نہیں۔", y, rightFmt); y += LineHeight + 4;
                DrawLine(e, headerFont, "چائنہ مال کی وارنٹی نہیں۔", y, rightFmt); y += LineHeight + 4;

                ///  Advertisement No..
                DrawLine(e, smallFont, "---------------------------------------------------------------------------------", y, rightFmt); y += LineHeight + 6;

                string advertisement = "03364978771 " + "سافٹ ویئر بنوانے کے لیے رابطہ نمبر";
                DrawLine(e, headerFont, TextFormatHelper.FormatMixedText(advertisement), y, rightFmt); y += LineHeight + 4;

            }
        }

        // ── Drawing utility methods ───────────────────────────────────────────

        private static void DrawLine(PrintPageEventArgs e, Font font, string text, int y, StringFormat fmt, Brush brush = null)
            => e.Graphics.DrawString(text, font, brush ?? Brushes.Black,
                   new Rectangle(LeftMargin, y, PaperWidthHundredths, LineHeight + 2), fmt);

        private static void DrawAt(PrintPageEventArgs e, Font font, string text,
            int x, int y, int width, int height, StringFormat fmt, Brush brush = null)
            => e.Graphics.DrawString(text, font, brush ?? Brushes.Black,
                   new Rectangle(x, y, width, height), fmt);
    }
}
