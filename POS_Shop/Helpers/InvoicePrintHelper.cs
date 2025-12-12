using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Helpers
{
    public static class InvoicePrintHelper
    {

        //private static string FormatMixedText(string input)
        //{
        //    if (string.IsNullOrWhiteSpace(input))
        //        return input;

        //    // Use these directional marks
        //    const char LRM = '\u200E'; // Left-to-Right Mark
        //    const char RLM = '\u200F'; // Right-to-Left Mark

        //    // Pattern to find English/number sequences (keep them together)
        //    var englishPattern = new System.Text.RegularExpressions.Regex(@"[0-9A-Za-z\-\.]+");

        //    // Start with RLM for overall RTL context
        //    var result = new System.Text.StringBuilder().Append(RLM);

        //    int lastIndex = 0;

        //    foreach (System.Text.RegularExpressions.Match match in englishPattern.Matches(input))
        //    {
        //        // Add Urdu text before this English segment
        //        if (match.Index > lastIndex)
        //        {
        //            result.Append(input.Substring(lastIndex, match.Index - lastIndex));
        //        }

        //        // Wrap English segment with LRM to keep it LTR
        //        result.Append(LRM);
        //        result.Append(match.Value);
        //        result.Append(RLM);

        //        lastIndex = match.Index + match.Length;
        //    }

        //    // Add remaining Urdu text
        //    if (lastIndex < input.Length)
        //    {
        //        result.Append(input.Substring(lastIndex));
        //    }

        //    return result.ToString();
        //}


        //public static void PrintInvoice(PrintPageEventArgs e, DataGridView cartProductList,
        //                              string customerName, string invoiceNo, string totalAmount,
        //                              bool isCashPayment, string receivedAmount, bool hideShopName)
        //{
        //    // Thermal printer settings (80mm paper)
        //    int paperWidth = 280; // pixels for 80mm paper
        //    int leftMargin = 0;
        //    int currentY = 5;
        //    int lineHeight = 12;
        //    int sectionSpacing = 3;

        //    // Fonts for thermal printing
        //    Font titleFont = new Font("Arial", 11, FontStyle.Bold);
        //    Font headerFont = new Font("Arial", 9, FontStyle.Bold);
        //    Font regularFont = new Font("Arial", 8, FontStyle.Regular);
        //    Font smallFont = new Font("Arial", 7, FontStyle.Regular);

        //    // Urdu font
        //    Font urduFont = new Font("Arial", 9, FontStyle.Regular);
        //    if (urduFont.Name != "Nafees Web Naskh")
        //        urduFont = new Font("Arial", 8, FontStyle.Regular);

        //    // Center alignment
        //    StringFormat centerFormat = new StringFormat();
        //    centerFormat.Alignment = StringAlignment.Center;

        //    // Right alignment for Urdu (right-to-left)
        //    StringFormat rightFormat = new StringFormat();
        //    rightFormat.Alignment = StringAlignment.Near;
        //    rightFormat.LineAlignment = StringAlignment.Near;

        //    // Left alignment for English text
        //    StringFormat leftFormat = new StringFormat();
        //    leftFormat.Alignment = StringAlignment.Near;

        //    string dashLine = new string('-', 82);

        //    // 1. COMPANY HEADER
        //    if (!hideShopName)
        //    {
        //        e.Graphics.DrawString("Electric Shop", titleFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
        //        currentY += lineHeight * 2;
        //        e.Graphics.DrawString("Contact: 1234567", smallFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        //        currentY += lineHeight;
        //        currentY += lineHeight + 2;
        //    }

        //    // 2. INVOICE INFO - Mixed Urdu and English
        //    e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    string cName = !string.IsNullOrEmpty(customerName) ? customerName : "";
        //    e.Graphics.DrawString($"کسٹمر: {cName}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    e.Graphics.DrawString("انوائس نمبر:" + invoiceNo, urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
        //    currentY += lineHeight + 2;

        //    // Define columns - ADJUSTED WIDTHS
        //    int col1 = leftMargin;                    // کل (Total)
        //    int col1Width = 60;  // INCREASED from 40

        //    int col2 = col1 + col1Width + 5;          // قیمت (Price)
        //    int col2Width = 50;  // INCREASED from 40

        //    int col3 = col2 + col2Width + 5;          // تعداد + قسم (Quantity + Type)
        //    int col3Width = 100; // INCREASED from 40 to accommodate both fields

        //    // REMOVED separate قسم column since it's now combined with تعداد
        //    int productCol = col3 + col3Width + 5;    // پروڈکٹ (Product name)
        //    int productColWidth = paperWidth - productCol - 5;

        //    // Draw Urdu table headers - UPDATED
        //    e.Graphics.DrawString("قیمت", headerFont, Brushes.Black,
        //                         new Rectangle(col1, currentY, col1Width, lineHeight), rightFormat);
        //    e.Graphics.DrawString("ریٹ ", headerFont, Brushes.Black,
        //                         new Rectangle(col2, currentY, col2Width, lineHeight), rightFormat);
        //    e.Graphics.DrawString("تعداد", headerFont, Brushes.Black,  // COMBINED HEADER
        //                         new Rectangle(col3, currentY, col3Width, lineHeight), rightFormat);
        //    e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.Black,
        //                         new Rectangle(productCol, currentY, productColWidth, lineHeight), rightFormat);

        //    currentY += lineHeight;
        //    e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
        //    currentY += 5;

        //    // TABLE ROWS - 2 ROWS PER PRODUCT
        //    foreach (DataGridViewRow row in cartProductList.Rows)
        //    {
        //        if (row.Cells[0].Value != null)
        //        {
        //            // Extract values
        //            decimal amount = row.Cells["Amount"]?.Value != null ? Convert.ToDecimal(row.Cells["Amount"].Value) : 0;
        //            decimal salePrice = row.Cells["SalePrice"]?.Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
        //            decimal qty = row.Cells["Qty"]?.Value != null ? Convert.ToDecimal(row.Cells["Qty"].Value) : 0;
        //            string productType = row.Cells["ProductType"]?.Value?.ToString() ?? "";
        //            string productName = row.Cells["Urdu Name"]?.Value?.ToString() ?? "";

        //            // ROW 1: PRODUCT NAME ONLY - SPANS ALL COLUMNS
        //            StringFormat productFormat = new StringFormat();
        //            productFormat.Alignment = StringAlignment.Far; // Right aligned
        //            productFormat.LineAlignment = StringAlignment.Center;
        //            productFormat.FormatFlags = StringFormatFlags.NoWrap;
        //            productFormat.Trimming = StringTrimming.None;

        //          string finalProductName=  FormatMixedText(productName);
        //            // Product name uses FULL WIDTH from leftMargin to right edge
        //            int fullWidth = paperWidth - leftMargin - 5;
        //            e.Graphics.DrawString(finalProductName, regularFont, Brushes.Black,
        //                                 new Rectangle(leftMargin, currentY, fullWidth, lineHeight), productFormat);

        //            // ROW 2: DETAILS IN SEPARATE COLUMNS
        //            int detailsY = currentY + lineHeight;

        //            // Draw details in their respective columns
        //            e.Graphics.DrawString($"{amount:0}", regularFont, Brushes.Black,
        //                                 new Rectangle(col1, detailsY, col1Width, lineHeight), rightFormat);
        //            e.Graphics.DrawString($"{salePrice:0}", regularFont, Brushes.Black,
        //                                 new Rectangle(col2, detailsY, col2Width, lineHeight), rightFormat);

        //            // COMBINED: تعداد + قسم in same column
        //            string combinedQtyType = $"{productType} {qty:0} ";
        //            e.Graphics.DrawString(combinedQtyType, regularFont, Brushes.Black,
        //                                 new Rectangle(col3, detailsY, col3Width, lineHeight), rightFormat);

        //            // Product column is EMPTY on details row since name was in row 1
        //            e.Graphics.DrawString("", regularFont, Brushes.Black,
        //                                 new Rectangle(productCol, detailsY, productColWidth, lineHeight), rightFormat);

        //            currentY = detailsY + lineHeight;
        //            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY); // Bottom line
        //            currentY += 5; // Extra spacing between products
        //        }
        //    }

        //    currentY += sectionSpacing;

        //    // 5. TOTALS SECTION - Urdu labels
        //    decimal subtotal = decimal.Parse(totalAmount);
        //    decimal taxAmount = 0m; // 0% tax
        //    decimal total = subtotal + taxAmount;

        //    // Totals section with Urdu labels
        //    e.Graphics.DrawString($"سب ٹوٹل: {subtotal:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    e.Graphics.DrawString($"کل رقم: {total:0}", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    currentY += lineHeight;

        //    e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
        //    currentY += lineHeight + 2;

        //    var method = isCashPayment ? "نقد" : "بینک ٹرانسفر";
        //    // 6. PAYMENT INFORMATION - Urdu
        //    e.Graphics.DrawString($"ادائیگی کا طریقہ: {method}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    decimal tendered = !string.IsNullOrEmpty(receivedAmount) ? decimal.Parse(receivedAmount) : decimal.Parse(totalAmount);
        //    decimal change = tendered - total;

        //    e.Graphics.DrawString($"وصول رقم: {tendered:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    e.Graphics.DrawString($"بقایا: {change:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight + 2;

        //    // 7. URDU FOOTER TEXT
        //    e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
        //    currentY += lineHeight;

        //    string footerText2 = "چائنہ مال کی وارنٹی نہیں۔";

        //    e.Graphics.DrawString(footerText2, headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight), rightFormat);
        //    currentY += lineHeight;

        //    // Dispose fonts
        //    titleFont.Dispose();
        //    headerFont.Dispose();
        //    regularFont.Dispose();
        //    smallFont.Dispose();
        //    urduFont.Dispose();
        //    centerFormat.Dispose();
        //    rightFormat.Dispose();
        //    leftFormat.Dispose();
        //}


        //public static void PrintInvoice(PrintPageEventArgs e, DataGridView cartProductList,
        //                      string customerName, string invoiceNo, string totalAmount,
        //                      bool isCashPayment, string receivedAmount, bool hideShopName)
        //{
        //    // 1️⃣  Dynamic height calculation
        //    int baseHeight = 350; // header, totals, footer space
        //    int itemHeight = 30;  // each item (2 rows: name + details)
        //    int totalHeight = baseHeight + (cartProductList.Rows.Count * itemHeight);

        //    // Safety cap
        //    if (totalHeight < 400) totalHeight = 400;

        //    // Set the custom paper size dynamically
        //    PaperSize customSize = new PaperSize("Custom", 280, totalHeight);
        //    e.PageSettings.PaperSize = customSize;

        //    // 2️⃣ Now your existing print logic continues

        //    int paperWidth = 280;
        //    int leftMargin = 0;
        //    int currentY = 5;
        //    int lineHeight = 12;
        //    int sectionSpacing = 3;

        //    Font titleFont = new Font("Arial", 11, FontStyle.Bold);
        //    Font headerFont = new Font("Arial", 9, FontStyle.Bold);
        //    Font regularFont = new Font("Arial", 9, FontStyle.Regular);
        //    Font smallFont = new Font("Arial", 7, FontStyle.Regular);
        //    Font urduFont = new Font("Nafees Web Naskh", 9, FontStyle.Regular);

        //    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
        //    StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        //    StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };

        //    string dashLine = new string('-', 82);

        //    // HEADER
        //    if (!hideShopName)
        //    {
        //        e.Graphics.DrawString("الیکٹرک سٹور", titleFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
        //        currentY += lineHeight * 2;
        //        //e.Graphics.DrawString("Contact: 1234567", smallFont, Brushes.Black,
        //        //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        //        //currentY += lineHeight + 2;
        //    }

        //    e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString($"کسٹمر: {customerName}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString("انوائس نمبر:" + invoiceNo, urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
        //    currentY += lineHeight;

        //    // HEADERS
        //    e.Graphics.DrawString("قیمت", headerFont, Brushes.Black,
        //                         new Rectangle(0, currentY, 60, lineHeight), rightFormat);
        //    e.Graphics.DrawString("ریٹ", headerFont, Brushes.Black,
        //                         new Rectangle(65, currentY, 50, lineHeight), rightFormat);
        //    e.Graphics.DrawString("تعداد", headerFont, Brushes.Black,
        //                         new Rectangle(120, currentY, 100, lineHeight), rightFormat);
        //    e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.Black,
        //                         new Rectangle(225, currentY, 50, lineHeight), rightFormat);
        //    currentY += lineHeight + 3;

        //    e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
        //    currentY += 5;

        //    // TABLE
        //    foreach (DataGridViewRow row in cartProductList.Rows)
        //    {
        //        if (row.Cells[0].Value == null) continue;

        //        decimal amount = row.Cells["Amount"]?.Value != null ? Convert.ToDecimal(row.Cells["Amount"].Value) : 0;
        //        decimal salePrice = row.Cells["SalePrice"]?.Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
        //        decimal qty = row.Cells["Qty"]?.Value != null ? Convert.ToDecimal(row.Cells["Qty"].Value) : 0;
        //        string productType = row.Cells["ProductType"]?.Value?.ToString() ?? "";
        //        string productName = row.Cells["Urdu Name"]?.Value?.ToString() ?? "";

        //        string formattedProduct = TextFormatHelper.FormatMixedText(productName);

        //        // Product Name
        //        e.Graphics.DrawString(formattedProduct, regularFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth - 5, lineHeight + 2),
        //                             new StringFormat { Alignment = StringAlignment.Far });
        //        int detailsY = currentY + lineHeight;

        //        // Row Details
        //        e.Graphics.DrawString($"{amount:0}", regularFont, Brushes.Black,
        //                             new Rectangle(0, detailsY, 60, lineHeight), rightFormat);
        //        e.Graphics.DrawString($"{salePrice:0}", regularFont, Brushes.Black,
        //                             new Rectangle(65, detailsY, 50, lineHeight), rightFormat);

        //        string formattedQtyValue = TextFormatHelper.FormatMixedText($"{productType} {qty:0}");
        //        e.Graphics.DrawString($"{formattedQtyValue}", regularFont, Brushes.Black,
        //                             new Rectangle(120, detailsY, 100, lineHeight), rightFormat);

        //        currentY = detailsY + lineHeight;
        //        e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
        //        currentY += 4;
        //    }

        //    // TOTALS
        //    currentY += sectionSpacing;
        //    decimal subtotal = decimal.Parse(totalAmount);
        //    decimal total = subtotal;

        //    e.Graphics.DrawString($"ٹوٹل: {subtotal:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;

        //    e.Graphics.DrawString($"کل رقم: {total:0}", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;


        //    decimal tendered = !string.IsNullOrEmpty(receivedAmount) ? decimal.Parse(receivedAmount) : subtotal;
        //    decimal change = tendered - total;

        //    e.Graphics.DrawString($"وصول رقم: {tendered:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;

        //    e.Graphics.DrawString($"بقایا: {change:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;


        //    string method = isCashPayment ? "نقد" : "بینک ٹرانسفر";
        //    e.Graphics.DrawString($"ادائیگی کا طریقہ: {method}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;


        //    e.Graphics.DrawString("چائنہ مال کی وارنٹی نہیں۔", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);

        //}

        public static void PrintInvoice(PrintPageEventArgs e, DataGridView cartProductList,
                              string customerName, string invoiceNo, string totalAmount,
                              bool isCashPayment, string receivedAmount, bool hideShopName)
        {
            // 1️⃣  Dynamic height calculation
            int baseHeight = 350; // header, totals, footer space
            int itemHeight = 30;  // each item (2 rows: name + details)
            int totalHeight = baseHeight + (cartProductList.Rows.Count * itemHeight);

            // Safety cap
            if (totalHeight < 400) totalHeight = 400;

            // Set the custom paper size dynamically
            PaperSize customSize = new PaperSize("Custom", 280, totalHeight);
            e.PageSettings.PaperSize = customSize;

            // 2️⃣ Now your existing print logic continues

            int paperWidth = 280;
            int leftMargin = 0;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            Font titleFont = new Font("Arial", 11, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font regularFont = new Font("Arial", 9, FontStyle.Regular);
            Font smallFont = new Font("Arial", 7, FontStyle.Regular);
            Font urduFont = new Font("Nafees Web Naskh", 9, FontStyle.Regular);

            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
            StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };

            string dashLine = new string('-', 82);

            // HEADER
            if (!hideShopName)
            {
                e.Graphics.DrawString(Properties.Settings.Default.UserName == "city" ? " سٹی الیکٹرونکس" : "ایس اے الیکٹرک اسٹور", titleFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
                currentY += lineHeight * 2;
                //e.Graphics.DrawString("Contact: 1234567", smallFont, Brushes.Black,
                //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
                //currentY += lineHeight + 2;
            }

            e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;

            //string cName= !string.IsNullOrEmpty(customerName) ? customerName.Split('-')[1].Trim() : "";
            e.Graphics.DrawString($"کسٹمر: {customerName}", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;

            //e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), urduFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            //currentY += lineHeight + 2;

            e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd"), urduFont, Brushes.Black,
                              new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;


            e.Graphics.DrawString($"انوائس :" + invoiceNo, urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            // HEADERS with Gray Background
            int headerStartY = currentY;
            int headerHeight = lineHeight + 3;

            // Draw gray background for headers
            using (Brush grayBrush = new SolidBrush(Color.Black))
            {
                e.Graphics.FillRectangle(grayBrush, leftMargin, headerStartY, paperWidth, headerHeight);
            }

            // Draw header text on top of gray background
            e.Graphics.DrawString("قیمت", headerFont, Brushes.White,
                                 new Rectangle(0, currentY, 60, lineHeight), rightFormat);
            e.Graphics.DrawString("ریٹ", headerFont, Brushes.White,
                                 new Rectangle(65, currentY, 50, lineHeight), rightFormat);
            e.Graphics.DrawString("تعداد", headerFont, Brushes.White,
                                 new Rectangle(120, currentY, 100, lineHeight), rightFormat);
            e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.White,
                                 new Rectangle(225, currentY, 50, lineHeight), rightFormat);
            currentY += lineHeight + 3;

            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
            currentY += 5;

            // TABLE ROWS (without background)
            foreach (DataGridViewRow row in cartProductList.Rows)
            {
                if (row.Cells[0].Value == null) continue;

                decimal amount = row.Cells["Amount"]?.Value != null ? Convert.ToDecimal(row.Cells["Amount"].Value) : 0;
                decimal salePrice = row.Cells["SalePrice"]?.Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
                decimal qty = row.Cells["Qty"]?.Value != null ? Convert.ToDecimal(row.Cells["Qty"].Value) : 0;
                string productType = row.Cells["ProductType"]?.Value?.ToString() ?? "";
                string productName = row.Cells["Urdu Name"]?.Value?.ToString() ?? "";

                string formattedProduct = TextFormatHelper.FormatMixedText(productName);

                // Product Name
                e.Graphics.DrawString(formattedProduct, regularFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth - 5, lineHeight + 2),
                                     new StringFormat { Alignment = StringAlignment.Far });
                int detailsY = currentY + lineHeight;

                // Row Details
                e.Graphics.DrawString($"{amount:0}", regularFont, Brushes.Black,
                                     new Rectangle(0, detailsY, 60, lineHeight), rightFormat);
                e.Graphics.DrawString($"{salePrice:0}", regularFont, Brushes.Black,
                                     new Rectangle(65, detailsY, 50, lineHeight), rightFormat);

                string formattedQtyValue = TextFormatHelper.FormatMixedText($"{productType} {qty:0}");
                e.Graphics.DrawString($"{formattedQtyValue}", urduFont, Brushes.Black,
                                     new Rectangle(120, detailsY, 100, lineHeight), rightFormat);

                currentY = detailsY + lineHeight;
                e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
                currentY += 4;
            }

            // TOTALS
            currentY += sectionSpacing;
            decimal subtotal = decimal.Parse(totalAmount);
            decimal total = subtotal;

            //e.Graphics.DrawString($"ٹوٹل: {subtotal:0}", headerFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            //currentY += lineHeight + 4;

            e.Graphics.DrawString($"کل رقم: {total:0}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 4;


            decimal tendered = !string.IsNullOrEmpty(receivedAmount) ? decimal.Parse(receivedAmount) : subtotal;
            decimal change = tendered - total;

            e.Graphics.DrawString($"وصول رقم: {tendered:0}", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 4;

            e.Graphics.DrawString($"بقایا: {change:0}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 4;


            string method = isCashPayment ? "نقد" : "بینک ٹرانسفر";
            e.Graphics.DrawString($"ادائیگی کا طریقہ: {method}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 4;
            e.Graphics.DrawString("ٹوٹے ہوۓ سامان کی واپسی نہیں۔", headerFont, Brushes.Black,
                       new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 4;

            e.Graphics.DrawString("چائنہ مال کی وارنٹی نہیں۔", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        }

        public static void PrintEnglishInvoice(PrintPageEventArgs e, DataGridView cartProductList,
                    string customerName, string invoiceNo, string totalAmount,
                    bool isCashPayment, string receivedAmount, bool hideShopName)
        {
            // Thermal printer settings (80mm paper)
            int paperWidth = 280; // pixels for 80mm paper
            int leftMargin = 5;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            // Fonts for thermal printing
            Font titleFont = new Font("Arial", 11, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font regularFont = new Font("Arial", 8, FontStyle.Regular);
            Font smallFont = new Font("Arial", 7, FontStyle.Regular);

            // Urdu font
            Font urduFont = new Font("Nafees Web Naskh", 8, FontStyle.Regular);
            if (urduFont.Name != "Nafees Web Naskh")
                urduFont = new Font("Arial", 8, FontStyle.Regular);

            // Center alignment
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;

            // Right alignment for numbers
            StringFormat rightFormat = new StringFormat();
            rightFormat.Alignment = StringAlignment.Far;

            string dashLine = new string('-', 82);

            // 1. COMPANY HEADER
            if (!hideShopName)
            {
                e.Graphics.DrawString("Electric Shop", titleFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
                currentY += lineHeight * 2;

                e.Graphics.DrawString("Contact: 1234567", smallFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
                currentY += lineHeight;

                currentY += lineHeight + 2;
            }


            // 2. INVOICE INFO
            e.Graphics.DrawString("INVOICE", headerFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            string cName = !string.IsNullOrEmpty(customerName) ? customerName : "";
            e.Graphics.DrawString($"Customer: {cName}", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Invoice #:" + invoiceNo, regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 3. TABLE LAYOUT - FIXED COLUMN POSITIONS TO PREVENT OVERLAP
            int productCol = leftMargin;                    // Product name column
            int productColWidth = 120;                      // Width for product names

            int typeCol = productCol + productColWidth + 5; // Type column
            int typeColWidth = 30;

            int qtyCol = typeCol + typeColWidth + 5;        // Qty column
            int qtyColWidth = 25;

            int priceCol = qtyCol + qtyColWidth + 5;        // Price column
            int priceColWidth = 40;

            int totalCol = priceCol + priceColWidth + 5;    // Total column
            int totalColWidth = 40;

            // Draw table headers
            e.Graphics.DrawString("Product", headerFont, Brushes.Black, productCol, currentY);
            e.Graphics.DrawString("Type", headerFont, Brushes.Black, typeCol, currentY);
            e.Graphics.DrawString("Qty", headerFont, Brushes.Black, qtyCol, currentY);
            e.Graphics.DrawString("Price", headerFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString("Total", headerFont, Brushes.Black, totalCol, currentY);

            currentY += lineHeight;
            currentY += 3;
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
            currentY += 5;

            foreach (DataGridViewRow row in cartProductList.Rows)
            {


                if (row.Cells[0].Value != null) // Check if row has data
                {
                    // First line: Product name only (left aligned)
                    e.Graphics.DrawString(row.Cells["Urdu Name"].Value?.ToString(), regularFont, Brushes.Black, productCol, currentY);
                    currentY += lineHeight;

                    // Second line: Type, Qty, Price, Total (in columns)
                    e.Graphics.DrawString(row.Cells["ProductType"].Value?.ToString(), urduFont, Brushes.Black, typeCol, currentY);
                    e.Graphics.DrawString(row.Cells["Qty"].Value?.ToString(), regularFont, Brushes.Black, qtyCol, currentY);
                    e.Graphics.DrawString(row.Cells["SalePrice"].Value?.ToString(), regularFont, Brushes.Black, priceCol, currentY);
                    e.Graphics.DrawString(row.Cells["Amount"].Value.ToString(), regularFont, Brushes.Black, totalCol, currentY);
                    currentY += lineHeight;


                    // Light separator line between items
                    e.Graphics.DrawLine(Pens.LightGray, leftMargin, currentY, totalCol + totalColWidth, currentY);
                    currentY += 2;
                }

            }

            // Bottom line of table
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
            currentY += lineHeight;

            // 5. TOTALS SECTION - MOVED LEFT FOR BETTER ALIGNMENT
            decimal subtotal = decimal.Parse(totalAmount);
            decimal taxRate = 0.05m;
            //decimal taxAmount = Math.Round(subtotal * taxRate, 2);
            decimal taxAmount = Math.Round(0m, 2);
            decimal total = subtotal + taxAmount;

            // Move totals left by using priceCol-20 instead of priceCol
            int totalsLabelCol = priceCol - 20; // Move labels 20 pixels left
            int totalsValueCol = totalCol - 15; // Move values 15 pixels left

            e.Graphics.DrawString("Subtotal:", regularFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(subtotal.ToString("0"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            //e.Graphics.DrawString("Tax (0%):", regularFont, Brushes.Black, totalsLabelCol, currentY);
            //e.Graphics.DrawString(taxAmount.ToString("0.00"), regularFont, Brushes.Black, totalsValueCol, currentY);
            //currentY += lineHeight;

            e.Graphics.DrawString("TOTAL:", headerFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(total.ToString("0"), headerFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            currentY += lineHeight;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 6. PAYMENT INFORMATION

            string method = isCashPayment ? "Cash" : "Bank Transger";
            e.Graphics.DrawString($"Payment Method: {method}", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            decimal tendered = !string.IsNullOrEmpty(receivedAmount) ? decimal.Parse(receivedAmount) : decimal.Parse(totalAmount);
            decimal change = tendered - total;

            e.Graphics.DrawString("Paid: " + tendered.ToString("0"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            var changeLabel = change < 0 ? "Remaining: " : "Return: ";
            e.Graphics.DrawString($"{changeLabel}: {change.ToString("0")}", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 7. FOOTER
            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("خریدا ہوا سامان واپس یا تبدیل نہیں ہوگا", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight;

            //e.Graphics.DrawString("7-day return with receipt", smallFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        }

        private static void DrawLine(Graphics graphics, int paperWidth, ref int yPos)
        {
            graphics.DrawLine(Pens.Black, 10, yPos, paperWidth - 10, yPos);
            yPos += 5;
        }


        private static void DrawCenteredString(Graphics graphics, string text, Font font, int paperWidth, ref int yPos)
        {
            SizeF textSize = graphics.MeasureString(text, font);
            int xPos = (paperWidth - (int)textSize.Width) / 2;
            graphics.DrawString(text, font, Brushes.Black, xPos, yPos);
            yPos += (int)textSize.Height + 2;
        }

        //public static void PrintInvoice(PrintPageEventArgs e, DataGridView cartProductList,
        //              string customerName, string invoiceNo, string totalAmount,
        //              bool isCashPayment, string receivedAmount, bool hideShopName)
        //{
        //    // 1️⃣ Dynamic height calculation
        //    int baseHeight = 350; // header, totals, footer space
        //    int itemHeight = 30;  // each item (2 rows: name + details)
        //    int totalHeight = baseHeight + (cartProductList.Rows.Count * itemHeight);

        //    // Safety cap
        //    if (totalHeight < 400) totalHeight = 400;

        //    // Set the custom paper size dynamically
        //    PaperSize customSize = new PaperSize("Custom", 280, totalHeight);
        //    e.PageSettings.PaperSize = customSize;

        //    // 2️⃣ Print logic

        //    int paperWidth = 280;
        //    int leftMargin = 0;
        //    int currentY = 5;
        //    int lineHeight = 12;
        //    int sectionSpacing = 3;

        //    Font titleFont = new Font("Arial", 11, FontStyle.Bold);
        //    Font headerFont = new Font("Arial", 9, FontStyle.Bold);
        //    Font regularFont = new Font("Arial", 9, FontStyle.Regular);
        //    Font smallFont = new Font("Arial", 7, FontStyle.Regular);
        //    Font urduFont = new Font("Nafees Web Naskh", 9, FontStyle.Regular);

        //    StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
        //    StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
        //    StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };

        //    string dashLine = new string('-', 82);

        //    // HEADER
        //    if (!hideShopName)
        //    {
        //        e.Graphics.DrawString("الیکٹرک سٹور", titleFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
        //        currentY += lineHeight * 2;
        //    }

        //    e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString($"کسٹمر: {customerName}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString("انوائس نمبر:" + invoiceNo, urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 2;

        //    e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
        //    currentY += lineHeight;

        //    // 🧾 TABLE HEADER (Top of bordered section)
        //    int tableTopY = currentY - 2; // Start slightly above the header
        //    int tableLeft = leftMargin;
        //    int tableRight = paperWidth - 5;

        //    // HEADER ROW
        //    e.Graphics.DrawString("قیمت", headerFont, Brushes.Black,
        //                         new Rectangle(0, currentY, 60, lineHeight), rightFormat);
        //    e.Graphics.DrawString("ریٹ", headerFont, Brushes.Black,
        //                         new Rectangle(65, currentY, 50, lineHeight), rightFormat);
        //    e.Graphics.DrawString("تعداد", headerFont, Brushes.Black,
        //                         new Rectangle(120, currentY, 100, lineHeight), rightFormat);
        //    e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.Black,
        //                         new Rectangle(225, currentY, 50, lineHeight), rightFormat);

        //    currentY += lineHeight + 3;
        //    e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
        //    currentY += 5;

        //    // 🧮 TABLE CONTENT (no internal lines)
        //    foreach (DataGridViewRow row in cartProductList.Rows)
        //    {
        //        if (row.Cells[0].Value == null) continue;

        //        decimal amount = row.Cells["Amount"]?.Value != null ? Convert.ToDecimal(row.Cells["Amount"].Value) : 0;
        //        decimal salePrice = row.Cells["SalePrice"]?.Value != null ? Convert.ToDecimal(row.Cells["SalePrice"].Value) : 0;
        //        decimal qty = row.Cells["Qty"]?.Value != null ? Convert.ToDecimal(row.Cells["Qty"].Value) : 0;
        //        string productType = row.Cells["ProductType"]?.Value?.ToString() ?? "";
        //        string productName = row.Cells["Urdu Name"]?.Value?.ToString() ?? "";

        //        string formattedProduct = TextFormatHelper.FormatMixedText(productName);

        //        // Product Name (top line)
        //        e.Graphics.DrawString(formattedProduct, regularFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth - 5, lineHeight + 2),
        //                             new StringFormat { Alignment = StringAlignment.Far });

        //        int detailsY = currentY + lineHeight;

        //        // Row details (bottom line)
        //        e.Graphics.DrawString($"{amount:0}", regularFont, Brushes.Black,
        //                             new Rectangle(0, detailsY, 60, lineHeight), rightFormat);
        //        e.Graphics.DrawString($"{salePrice:0}", regularFont, Brushes.Black,
        //                             new Rectangle(65, detailsY, 50, lineHeight), rightFormat);

        //        string formattedQtyValue = TextFormatHelper.FormatMixedText($"{productType} {qty:0}");
        //        e.Graphics.DrawString($"{formattedQtyValue}", regularFont, Brushes.Black,
        //                             new Rectangle(120, detailsY, 100, lineHeight), rightFormat);

        //        currentY = detailsY + lineHeight + 2; // More spacing between rows
        //    }

        //    // Mark the very bottom of the table (after all rows)
        //    int tableBottomY = currentY + 2;

        //    // 🖋️ Draw a single outer border (clean rectangle)
        //    e.Graphics.DrawRectangle(Pens.Black,
        //        new Rectangle(tableLeft, tableTopY, tableRight - tableLeft, tableBottomY - tableTopY));

        //    // ==== TOTALS SECTION ====
        //    currentY += sectionSpacing;
        //    decimal subtotal = decimal.Parse(totalAmount);
        //    decimal total = subtotal;

        //    e.Graphics.DrawString($"ٹوٹل: {subtotal:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;

        //    e.Graphics.DrawString($"کل رقم: {total:0}", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;

        //    decimal tendered = !string.IsNullOrEmpty(receivedAmount) ? decimal.Parse(receivedAmount) : subtotal;
        //    decimal change = tendered - total;

        //    e.Graphics.DrawString($"وصول رقم: {tendered:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;

        //    e.Graphics.DrawString($"بقایا: {change:0}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;

        //    string method = isCashPayment ? "نقد" : "بینک ٹرانسفر";
        //    e.Graphics.DrawString($"ادائیگی کا طریقہ: {method}", urduFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //    currentY += lineHeight + 4;

        //    e.Graphics.DrawString("چائنہ مال کی وارنٹی نہیں۔", headerFont, Brushes.Black,
        //                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
        //}



        // Optional: Overloaded method with default parameters
        public static void PrintInvoice(PrintPageEventArgs e, DataGridView cartProductList,
                                      string customerName, string invoiceNo, string totalAmount)
        {
            PrintInvoice(e, cartProductList, customerName, invoiceNo, totalAmount,
                        true, totalAmount, false);
        }
    }
}
 