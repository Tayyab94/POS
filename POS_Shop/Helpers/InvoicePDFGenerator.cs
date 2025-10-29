using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Windows.Forms;

public class UrduInvoiceGenerator
{
    private BaseFont _urduFont;

    public UrduInvoiceGenerator()
    {
        InitializeUrduFont();
    }

    private void InitializeUrduFont()
    {
        try
        {
            // Use a font that supports Urdu - try different options
            string[] fontPaths = {
                @"C:\Windows\Fonts\arial.ttf",           // Arial
                @"C:\Windows\Fonts\tahoma.ttf",         // Tahoma
                @"C:\Windows\Fonts\seguiui.ttf",        // Segoe UI
                @"C:\Windows\Fonts\times.ttf",          // Times New Roman
                @"C:\Windows\Fonts\arialuni.ttf"        // Arial Unicode MS
            };

            foreach (string fontPath in fontPaths)
            {
                if (File.Exists(fontPath))
                {
                    _urduFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    Console.WriteLine($"Successfully loaded font: {fontPath}");
                    break;
                }
            }

            // If no font found, create a basic one
            if (_urduFont == null)
            {
                _urduFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Console.WriteLine("Using fallback font: Helvetica");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Font initialization error: {ex.Message}");
            _urduFont = BaseFont.CreateFont();
        }
    }

    public void GenerateInvoice(DataGridView CartProductList, string filePath)
    {
        // Use A4 size for better compatibility
        Document document = new Document(PageSize.A4, 20f, 20f, 30f, 20f);

        try
        {
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            AddInvoiceHeader(document);

            PdfPTable table = CreateProductTable();
            int currentPageRowCount = 0;
            decimal grandTotal = 0;

            foreach (DataGridViewRow row in CartProductList.Rows)
            {
                if (row.IsNewRow) continue;

                // Pagination: 35 rows per page
                if (currentPageRowCount >= 35)
                {
                    document.Add(table);
                    document.NewPage();
                    AddInvoiceHeader(document);
                    table = CreateProductTable();
                    currentPageRowCount = 0;
                }

                decimal rowTotal = AddProductRow(table, row);
                grandTotal += rowTotal;
                currentPageRowCount++;
            }

            // Add final table and totals
            document.Add(table);
            document.Add(new Paragraph("\n"));
            AddInvoiceTotals(document, grandTotal);
            AddFooterNote(document);

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error generating PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            document.Close();
        }
    }

    private void AddInvoiceHeader(Document document)
    {
        // Header with Urdu support
        Paragraph header = new Paragraph();
        header.Alignment = Element.ALIGN_CENTER;

        Font titleFont = new Font(_urduFont, 16, Font.BOLD);
        Font normalFont = new Font(_urduFont, 10, Font.NORMAL);

        header.Add(new Chunk("انوائس\n", titleFont)); // "Invoice" in Urdu
        header.Add(new Chunk("آپ کا اسٹور نام\n", normalFont)); // "Your Store Name" in Urdu
        header.Add(new Chunk("فون: 123-456-7890\n\n", normalFont)); // "Phone" in Urdu

        document.Add(header);

        // Invoice details
        PdfPTable infoTable = new PdfPTable(2);
        infoTable.WidthPercentage = 100;
        infoTable.SetWidths(new float[] { 1, 1 });

        AddInfoCell(infoTable, "انوائس نمبر:", "INV-" + DateTime.Now.ToString("yyyyMMdd-HHmm"));
        AddInfoCell(infoTable, "تاریخ:", DateTime.Now.ToString("dd/MM/yyyy"));
        AddInfoCell(infoTable, "وقت:", DateTime.Now.ToString("HH:mm"));
        AddInfoCell(infoTable, "گاہک:", "واک ان");

        document.Add(infoTable);
        document.Add(new Paragraph("\n"));
        AddSeparatorLine(document);
        document.Add(new Chunk("\n"));
    }

    private PdfPTable CreateProductTable()
    {
        PdfPTable table = new PdfPTable(4);
        table.WidthPercentage = 100;
        table.SetWidths(new float[] { 4, 1, 2, 2 }); // More space for Urdu names

        // Table headers in Urdu
        AddTableHeader(table, "پرڈکٹ نام");
        AddTableHeader(table, "مقدار");
        AddTableHeader(table, "قیمت");
        AddTableHeader(table, "رقم");

        return table;
    }

    private decimal AddProductRow(PdfPTable table, DataGridViewRow row)
    {
        try
        {
            // Get values from DataGridView
            object urduNameValue = row.Cells["Urdu Name"].Value;
            object qtyValue = row.Cells["Qty"].Value;
            object salePriceValue = row.Cells["SalePrice"].Value;

            string urduName = urduNameValue?.ToString() ?? "نام موجود نہیں";
            string qty = qtyValue?.ToString() ?? "0";
            string salePrice = salePriceValue?.ToString() ?? "0";

            // Parse numerical values
            int qtyInt = int.TryParse(qty, out int q) ? q : 0;
            decimal priceDecimal = decimal.TryParse(salePrice, out decimal p) ? p : 0m;

            // Calculate total
            decimal rowTotal = priceDecimal * qtyInt;

            // Process Urdu name - ensure it's not empty
            if (string.IsNullOrWhiteSpace(urduName) || IsNumeric(urduName))
            {
                urduName = "پرڈکٹ"; // Fallback to "Product" in Urdu
            }

            // Limit length for display
            if (urduName.Length > 30)
            {
                urduName = urduName.Substring(0, 30) + "...";
            }

            // Add cells with proper Urdu font
            AddTableCell(table, urduName, Element.ALIGN_RIGHT, true); // Urdu text right-aligned
            AddTableCell(table, qtyInt.ToString(), Element.ALIGN_CENTER, false);
            AddTableCell(table, priceDecimal.ToString("F2"), Element.ALIGN_RIGHT, false);
            AddTableCell(table, rowTotal.ToString("F2"), Element.ALIGN_RIGHT, false);

            return rowTotal;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error in row: {ex.Message}");
            // Add error row
            AddTableCell(table, "خرابی", Element.ALIGN_RIGHT, true);
            AddTableCell(table, "0", Element.ALIGN_CENTER, false);
            AddTableCell(table, "0.00", Element.ALIGN_RIGHT, false);
            AddTableCell(table, "0.00", Element.ALIGN_RIGHT, false);
            return 0;
        }
    }

    private bool IsNumeric(string value)
    {
        return decimal.TryParse(value, out _);
    }

    private void AddInvoiceTotals(Document document, decimal grandTotal)
    {
        AddSeparatorLine(document);
        document.Add(new Paragraph("\n"));

        PdfPTable totalsTable = new PdfPTable(2);
        totalsTable.WidthPercentage = 100;
        totalsTable.SetWidths(new float[] { 3, 2 });

        Font boldFont = new Font(_urduFont, 12, Font.BOLD);
        Font normalFont = new Font(_urduFont, 10, Font.NORMAL);

        // Totals in Urdu
        AddTotalRow(totalsTable, "کل رقم:", grandTotal.ToString("F2"), normalFont);

        decimal tax = grandTotal * 0.10m;
        AddTotalRow(totalsTable, "ٹیکس (10%):", tax.ToString("F2"), normalFont);

        decimal discount = grandTotal * 0.05m;
        AddTotalRow(totalsTable, "رعایت (5%):", (-discount).ToString("F2"), normalFont);

        AddSeparatorRow(totalsTable);

        decimal finalTotal = grandTotal + tax - discount;
        AddTotalRow(totalsTable, "حتمی رقم:", finalTotal.ToString("F2"), boldFont);

        document.Add(totalsTable);
    }

    private void AddFooterNote(Document document)
    {
        document.Add(new Paragraph("\n"));

        Paragraph footer = new Paragraph();
        footer.Alignment = Element.ALIGN_CENTER;
        Font footerFont = new Font(_urduFont, 8, Font.ITALIC);

        footer.Add(new Chunk("آپ کے کاروبار کا شکریہ!\n", footerFont));
        footer.Add(new Chunk("یہ کمپیوٹر سے بنائی گئی انوائس ہے\n", footerFont));

        document.Add(footer);
    }

    // Helper methods
    private void AddInfoCell(PdfPTable table, string label, string value)
    {
        Font boldFont = new Font(_urduFont, 10, Font.BOLD);
        Font normalFont = new Font(_urduFont, 10, Font.NORMAL);

        table.AddCell(new PdfPCell(new Phrase(label, boldFont))
        {
            Border = 0,
            Padding = 3
        });
        table.AddCell(new PdfPCell(new Phrase(value, normalFont))
        {
            Border = 0,
            Padding = 3
        });
    }

    private void AddTableHeader(PdfPTable table, string text)
    {
        Font font = new Font(_urduFont, 11, Font.BOLD, BaseColor.WHITE);
        PdfPCell cell = new PdfPCell(new Phrase(text, font));
        cell.BackgroundColor = new BaseColor(70, 130, 180); // Nice blue color
        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.Padding = 5;
        table.AddCell(cell);
    }

    private void AddTableCell(PdfPTable table, string text, int alignment, bool useUrduFont)
    {
        Font cellFont = useUrduFont ?
            new Font(_urduFont, 10, Font.NORMAL) :
            new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);

        PdfPCell cell = new PdfPCell(new Phrase(text, cellFont));
        cell.Padding = 5;
        cell.HorizontalAlignment = alignment;
        cell.BorderWidth = 0.5f;
        table.AddCell(cell);
    }

    private void AddTotalRow(PdfPTable table, string label, string value, Font font)
    {
        table.AddCell(new PdfPCell(new Phrase(label, font))
        {
            Border = PdfPCell.NO_BORDER,
            Padding = 4,
            HorizontalAlignment = Element.ALIGN_RIGHT
        });

        table.AddCell(new PdfPCell(new Phrase(value, font))
        {
            Border = PdfPCell.NO_BORDER,
            HorizontalAlignment = Element.ALIGN_RIGHT,
            Padding = 4
        });
    }

    private void AddSeparatorRow(PdfPTable table)
    {
        table.AddCell(new PdfPCell(new Phrase(""))
        {
            Border = PdfPCell.TOP_BORDER,
            Colspan = 2,
            Padding = 2,
            FixedHeight = 8f
        });
    }

    private void AddSeparatorLine(Document document)
    {
        Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, -1)));
        document.Add(line);
    }
}