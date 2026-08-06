using DocumentFormat.OpenXml.Spreadsheet;
using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    /// <summary>
    /// Read-only invoice detail popup.
    ///
    /// Open it from SupplierPaymentForm when the user clicks a cell in the
    /// "Invoice No" column:
    ///
    ///     int purchaseId = (int)dgvInvoices.Rows[e.RowIndex].Tag;
    ///     new PurchaseDetailForm(purchaseId).ShowDialog(this);
    ///
    /// Shows: supplier, purchase date, reference, all line items,
    /// subtotal / discount / net, total paid, balance, payment status, notes.
    /// Nothing can be edited.
    /// </summary>
    public partial class PurchaseDetailForm : Form
    {
        private readonly POSDbContext _db;
        private readonly int _purchaseId;

        // ── Constructor ────────────────────────────────────────────────────────
        public PurchaseDetailForm(int purchaseId)
        {
            InitializeComponent();
            _db = new POSDbContext();
            _purchaseId = purchaseId;

            // Hover effects
            HoverBtn(btnClose, System.Drawing.Color.FromArgb(207, 216, 220), System.Drawing.Color.FromArgb(236, 239, 241));
            HoverBtn(btnPrint,
                     System.Drawing.Color.FromArgb(13, 71, 161),
                     System.Drawing.Color.FromArgb(21, 101, 192));

            this.Load += (s, e) => LoadInvoice();
        }

        // ══════════════════════════════════════════════════════════════════════
        //  LOAD DATA
        // ══════════════════════════════════════════════════════════════════════

        //private void LoadInvoice()
        //{
        //    try
        //    {
        //        var purchase = _db.Purchases
        //            .Include("PurchaseItems")
        //            .Include("Supplier")
        //            .FirstOrDefault(p => p.Id == _purchaseId);

        //        if (purchase == null)
        //        {
        //            MessageBox.Show("Invoice not found.", "Error",
        //                MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            this.Close();
        //            return;
        //        }

        //        // ── Form title ────────────────────────────────────────────────
        //        this.Text = $"Invoice — {purchase.InvoiceNumber}";
        //        lblInvoiceNo.Text = purchase.InvoiceNumber;

        //        // ── Status badge ──────────────────────────────────────────────
        //        ApplyStatusBadge(purchase.PaymentStatus);

        //        // ── Meta strip ────────────────────────────────────────────────
        //        if (purchase.Supplier != null)
        //            lblSupVal.Text = $"{purchase.Supplier.SupplierName}  —  {purchase.Supplier.ShopName}";
        //        else
        //            lblSupVal.Text = $"Supplier #{purchase.SupplierId}";

        //        lblDateVal.Text = purchase.PurchaseDate.ToString("dd MMM yyyy  (dddd)");
        //        lblRefVal.Text = !string.IsNullOrWhiteSpace(purchase.SupplierReferenceNo)
        //                             ? purchase.SupplierReferenceNo
        //                             : "—";
        //        lblCreatedVal.Text = purchase.CreatedAt.ToLocalTime().ToString("dd MMM yyyy  HH:mm");

        //        // ── Items grid ────────────────────────────────────────────────
        //        var items = purchase.PurchaseItems
        //            .Where(i => !i.IsDeleted)
        //            .OrderBy(i => i.Id)
        //            .ToList();

        //        int sr = 1;
        //        foreach (var item in items)
        //        {
        //            // Resolve product
        //            string name = $"Product #{item.ProductId}";
        //            string code = "";
        //            var prod = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
        //            if (prod != null)
        //            {
        //                name = prod.ProductEnglishName;
        //                code = prod.SearchByProductCode ?? "";
        //            }

        //            // Resolve unit
        //            string unit = "—";
        //            //if (item.ProductUnitId.HasValue && item.ProductUnitId > 0)
        //            //{
        //            //    var u = _db.ProductUnits.FirstOrDefault(x => x.Id == item.ProductUnitId);
        //            //    if (u != null) unit = u.Name;
        //            //}

        //            int idx = dgvItems.Rows.Add();
        //            var row = dgvItems.Rows[idx];
        //            row.Cells["colSrNo"].Value = sr++;
        //            row.Cells["colCode"].Value = code;
        //            row.Cells["colProduct"].Value = name;
        //            row.Cells["colUnit"].Value = item.ProductUnitType;
        //            row.Cells["colQty"].Value = item.Quantity;
        //            row.Cells["colPrice"].Value = item.PurchasePrice;
        //            row.Cells["colTotal"].Value = item.TotalPrice;
        //        }

        //        // ── Grid title ────────────────────────────────────────────────
        //        lblGridTitle.Text = $"  Items Purchased  ({items.Count} item{(items.Count != 1 ? "s" : "")})";

        //        // ── Totals ────────────────────────────────────────────────────
        //        lblSubVal.Text = $"Rs. {purchase.TotalAmount:N2}";
        //        lblDiscVal.Text = purchase.Discount > 0
        //                          ? $"−  Rs. {purchase.Discount:N2}"
        //                          : "Rs. 0.00";
        //        lblNetVal.Text = $"Rs. {purchase.NetAmount:N2}";

        //        // ── Payment summary ───────────────────────────────────────────
        //        lblPaidVal.Text = $"Rs. {purchase.TotalPaid:N2}";
        //        lblBalVal.Text = $"Rs. {purchase.Balance:N2}";
        //        lblBalVal.ForeColor = purchase.Balance <= 0
        //            ? System.Drawing.Color.FromArgb(46, 125, 50)
        //            : System.Drawing.Color.FromArgb(198, 40, 40);

        //        // ── Notes ─────────────────────────────────────────────────────
        //        lblNotesVal.Text = !string.IsNullOrWhiteSpace(purchase.Notes)
        //                           ? purchase.Notes
        //                           : "—";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error loading invoice:\n" + ex.Message, "Error",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private void LoadInvoice()
        {
            try
            {
                var purchase = _db.Purchases
                    .Include("PurchaseItems")
                    .Include("Supplier")
                    .FirstOrDefault(p => p.Id == _purchaseId);

                if (purchase == null)
                {
                    MessageBox.Show("Invoice not found.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // ── Form title ────────────────────────────────────────────────
                this.Text = $"Invoice — {purchase.InvoiceNumber}";
                lblInvoiceNo.Text = purchase.InvoiceNumber;

                // ── Status badge ──────────────────────────────────────────────
                ApplyStatusBadge(purchase.PaymentStatus);

                // ── Meta strip ────────────────────────────────────────────────
                if (purchase.Supplier != null)
                    lblSupVal.Text = $"{purchase.Supplier.SupplierName}  —  {purchase.Supplier.ShopName}";
                else
                    lblSupVal.Text = $"Supplier #{purchase.SupplierId}";

                lblDateVal.Text = purchase.PurchaseDate.ToString("dd MMM yyyy  (dddd)");
                lblRefVal.Text = !string.IsNullOrWhiteSpace(purchase.SupplierReferenceNo)
                                     ? purchase.SupplierReferenceNo
                                     : "—";
                lblCreatedVal.Text = purchase.CreatedAt.ToLocalTime().ToString("dd MMM yyyy  HH:mm");

                // ── Items grid ────────────────────────────────────────────────
                var items = purchase.PurchaseItems
                    .Where(i => !i.IsDeleted)
                    .OrderBy(i => i.Id)
                    .ToList();

                int sr = 1;
                foreach (var item in items)
                {
                    // Resolve product
                    string name = $"Product #{item.ProductId}";
                    string code = "";
                    var prod = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (prod != null)
                    {
                        name = prod.ProductEnglishName;
                        code = prod.SearchByProductCode ?? "";
                    }

                    // Resolve unit
                    string unit = "—";
                    if (item.ProductUnitId.HasValue && item.ProductUnitId > 0)
                    {
                        var u = _db.ProductUnits.FirstOrDefault(x => x.Id == item.ProductUnitId);
                        if (u != null) unit = u.Name;
                    }

                    int idx = dgvItems.Rows.Add();
                    var row = dgvItems.Rows[idx];
                    row.Cells["colSrNo"].Value = sr++;
                    row.Cells["colCode"].Value = code;
                    row.Cells["colProduct"].Value = name;
                    row.Cells["colUnit"].Value = unit;
                    row.Cells["colQty"].Value = item.Quantity;
                    row.Cells["colPrice"].Value = item.PurchasePrice;
                    row.Cells["colTotal"].Value = item.TotalPrice;
                }

                // ── Grid title ────────────────────────────────────────────────
                lblGridTitle.Text = $"  Items Purchased  ({items.Count} item{(items.Count != 1 ? "s" : "")})";

                // ── Totals ────────────────────────────────────────────────────
                lblSubVal.Text = $"Rs. {purchase.TotalAmount:N2}";
                lblDiscVal.Text = purchase.Discount > 0
                                  ? $"−  Rs. {purchase.Discount:N2}"
                                  : "Rs. 0.00";
                lblNetVal.Text = $"Rs. {purchase.NetAmount:N2}";

                // ── Payment summary ───────────────────────────────────────────
                lblPaidVal.Text = $"Rs. {purchase.TotalPaid:N2}";
                lblBalVal.Text = $"Rs. {purchase.Balance:N2}";
                lblBalVal.ForeColor = purchase.Balance <= 0
                    ? System.Drawing.Color.FromArgb(46, 125, 50)
                    : System.Drawing.Color.FromArgb(198, 40, 40);

                // ── Notes ─────────────────────────────────────────────────────
                lblNotesVal.Text = !string.IsNullOrWhiteSpace(purchase.Notes)
                                   ? purchase.Notes
                                   : "—";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // ══════════════════════════════════════════════════════════════════════
        //  STATUS BADGE
        // ══════════════════════════════════════════════════════════════════════

        private void ApplyStatusBadge(PurchasePaymentStatus status)
        {
            switch (status)
            {
                case PurchasePaymentStatus.Pending:
                    pnlStatusBadge.BackColor = System.Drawing.Color.FromArgb(245, 124, 0);   // orange
                    lblStatus.Text = "⏳  PENDING";
                    lblPayStatVal.Text = "Pending";
                    lblPayStatVal.ForeColor = System.Drawing.Color.FromArgb(245, 124, 0);
                    pnlPayStatus.BackColor = System.Drawing.Color.FromArgb(255, 243, 224);
                    break;

                case PurchasePaymentStatus.PartiallyPaid:
                    pnlStatusBadge.BackColor = System.Drawing.Color.FromArgb(2, 119, 189);   // blue
                    lblStatus.Text = "🔵  PARTIAL";
                    lblPayStatVal.Text = "Partially Paid";
                    lblPayStatVal.ForeColor = System.Drawing.Color.FromArgb(2, 119, 189);
                    pnlPayStatus.BackColor = System.Drawing.Color.FromArgb(225, 245, 254);
                    break;

                case PurchasePaymentStatus.Paid:
                    pnlStatusBadge.BackColor = System.Drawing.Color.FromArgb(46, 125, 50);   // green
                    lblStatus.Text = "✔  PAID";
                    lblPayStatVal.Text = "Fully Paid";
                    lblPayStatVal.ForeColor = System.Drawing.Color.FromArgb(46, 125, 50);
                    pnlPayStatus.BackColor = System.Drawing.Color.FromArgb(200, 230, 201);
                    break;
            }
        }

        // ══════════════════════════════════════════════════════════════════════
        //  PRINT / EXPORT
        // ══════════════════════════════════════════════════════════════════════

        //private void BtnPrint_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var pd = new System.Drawing.Printing.PrintDocument();
        //        pd.PrintPage += PrintPage;

        //        using (var dlg = new PrintPreviewDialog
        //        {
        //            Document = pd,
        //            Width = 920,
        //            Height = 720,
        //            StartPosition = FormStartPosition.CenterParent
        //        })
        //        {
        //            dlg.ShowDialog(this);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Print error:\n" + ex.Message, "Print",
        //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    }
        //}

        //private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    var g = e.Graphics;
        //    var blue = new SolidBrush(System.Drawing.Color.FromArgb(21, 101, 192));
        //    var grey = new SolidBrush(System.Drawing.Color.FromArgb(120, 144, 156));
        //    var black = Brushes.Black;

        //    var bold18 = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        //    var bold12 = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        //    var bold11 = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
        //    var bold10 = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
        //    var reg10 = new System.Drawing.Font("Segoe UI", 10F);
        //    var reg9 = new System.Drawing.Font("Segoe UI", 9F);

        //    int x = 40, y = 40, lh = 22;

        //    // Title block
        //    g.DrawString("Purchase Invoice", bold18, blue, x, y);
        //    g.DrawString(lblInvoiceNo.Text, bold12, new SolidBrush(System.Drawing.Color.FromArgb(144, 164, 174)), x + 260, y + 4);
        //    g.DrawString(lblStatus.Text, bold10, grey, x + 540, y + 6);
        //    y += 40;

        //    // Meta
        //    g.DrawString($"Supplier  :  {lblSupVal.Text}", reg10, black, x, y); y += lh;
        //    g.DrawString($"Date      :  {lblDateVal.Text}", reg10, black, x, y); y += lh;
        //    if (lblRefVal.Text != "—")
        //    { g.DrawString($"Ref No    :  {lblRefVal.Text}", reg10, black, x, y); y += lh; }
        //    y += 8;

        //    // Column header bar
        //    g.FillRectangle(blue, x, y, 720, 26);
        //    g.DrawString("#", bold10, Brushes.White, x + 4, y + 3);
        //    g.DrawString("Product", bold10, Brushes.White, x + 34, y + 3);
        //    g.DrawString("Unit", bold10, Brushes.White, x + 310, y + 3);
        //    g.DrawString("Qty", bold10, Brushes.White, x + 380, y + 3);
        //    g.DrawString("Price", bold10, Brushes.White, x + 440, y + 3);
        //    g.DrawString("Total", bold10, Brushes.White, x + 580, y + 3);
        //    y += 28;

        //    // Rows
        //    bool alt = false;
        //    foreach (DataGridViewRow row in dgvItems.Rows)
        //    {
        //        if (alt)
        //            g.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(245, 249, 255)), x, y, 720, 20);
        //        g.DrawString(row.Cells["colSrNo"].Value?.ToString(), reg9, black, x + 4, y + 2);
        //        g.DrawString(row.Cells["colProduct"].Value?.ToString(), reg9, black, x + 34, y + 2);
        //        g.DrawString(row.Cells["colUnit"].Value?.ToString(), reg9, black, x + 310, y + 2);
        //        g.DrawString(row.Cells["colQty"].Value?.ToString(), reg9, black, x + 380, y + 2);
        //        g.DrawString(row.Cells["colPrice"].Value?.ToString(), reg9, black, x + 440, y + 2);
        //        g.DrawString(row.Cells["colTotal"].Value?.ToString(), reg9, black, x + 580, y + 2);
        //        y += 20;
        //        alt = !alt;
        //    }

        //    y += 10;
        //    g.DrawLine(Pens.LightGray, x, y, x + 720, y);
        //    y += 10;

        //    // Totals
        //    g.DrawString($"Subtotal   :  {lblSubVal.Text}", reg10, black, x + 440, y); y += lh;
        //    g.DrawString($"Discount   :  {lblDiscVal.Text}", reg10, black, x + 440, y); y += lh;
        //    g.DrawString($"Net Amount :  {lblNetVal.Text}", bold11, blue, x + 440, y); y += lh + 6;
        //    g.DrawString($"Paid       :  {lblPaidVal.Text}", reg10, black, x + 440, y); y += lh;
        //    g.DrawString($"Balance    :  {lblBalVal.Text}", bold10, black, x + 440, y); y += lh;

        //    // Notes
        //    if (lblNotesVal.Text != "—")
        //    {
        //        y += 10;
        //        g.DrawString("Notes: " + lblNotesVal.Text, reg9, grey, x, y);
        //    }

        //    // Cleanup
        //    foreach (var f in new System.Drawing.Font[] { bold18, bold12, bold11, bold10, reg10, reg9 }) f.Dispose();
        //    blue.Dispose(); grey.Dispose();
        //}

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var pd = new System.Drawing.Printing.PrintDocument();
                pd.PrintPage += PrintPage;

                using (var dlg = new PrintPreviewDialog
                {
                    Document = pd,
                    Width = 920,
                    Height = 720,
                    StartPosition = FormStartPosition.CenterParent
                })
                {
                    dlg.PrintPreviewControl.Zoom = 1.0;
                    dlg.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print error:\n" + ex.Message, "Print",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // 1️⃣ Dynamic height calculation
            int baseHeight = 350;
            int itemHeight = 30;
            int totalHeight = baseHeight + (dgvItems.Rows.Count * itemHeight);

            if (totalHeight < 400) totalHeight = 400;

            PaperSize customSize = new PaperSize("Custom", 280, totalHeight);
            e.PageSettings.PaperSize = customSize;

            // 2️⃣ Print logic
            int paperWidth = 280;
            int leftMargin = 0;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 16, FontStyle.Bold);
            System.Drawing.Font headerFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
            System.Drawing.Font regularFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
            System.Drawing.Font smallFont = new System.Drawing.Font("Arial", 7, FontStyle.Regular);
            System.Drawing.Font urduFont = new System.Drawing.Font("Nafees Web Naskh", 9, FontStyle.Regular);
            System.Drawing.Font amountFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);

            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Near };
            StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };

            string dashLine = new string('-', 82);

            // HEADER
            if (ConfigurationManager.Configuration.Features.ShowHideShopName)
            {
                var invoiceInfo = ConfigurationManager.Configuration.InvoiceSettings;

                e.Graphics.DrawString("صادات الیکٹرک اسٹور", titleFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
                currentY += lineHeight * 2;
                e.Graphics.DrawString("موتی بازار، وزیرآباد", smallFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
                currentY += lineHeight + 2;
                e.Graphics.DrawString("0301-6244700", smallFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
                currentY += lineHeight + 2;
            }

            // Title in Urdu
            e.Graphics.DrawString("خریداری انوائس", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;

            e.Graphics.DrawString($"{lblSupVal.Text} : سپلائر", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;

            e.Graphics.DrawString($"{lblDateVal.Text}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;

            if (lblRefVal.Text != "—")
            {
                e.Graphics.DrawString($"{lblRefVal.Text} : حوالہ نمبر", urduFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
                currentY += lineHeight + 2;
            }

            e.Graphics.DrawString($"{lblInvoiceNo.Text} : انوائس نمبر", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            // COLUMN HEADERS with Black Background
            int headerStartY = currentY;
            int headerHeight = lineHeight + 3;

            using (Brush blackBrush = new SolidBrush(System.Drawing.Color.Black))
            {
                e.Graphics.FillRectangle(blackBrush, leftMargin, headerStartY, paperWidth, headerHeight);
            }

            e.Graphics.DrawString("قیمت", headerFont, Brushes.White,
                                 new Rectangle(0, currentY, 60, lineHeight), rightFormat);
            e.Graphics.DrawString("ریٹ", headerFont, Brushes.White,
                                 new Rectangle(65, currentY, 50, lineHeight), rightFormat);
            e.Graphics.DrawString("تعداد", headerFont, Brushes.White,
                                 new Rectangle(100, currentY, 100, lineHeight), rightFormat);
            e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.White,
                                 new Rectangle(225, currentY, 50, lineHeight), rightFormat);
            currentY += lineHeight + 3;

            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
            currentY += 5;

            // TABLE ROWS
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                string productName = row.Cells["colProduct"].Value?.ToString() ?? "";
                string unit = row.Cells["colUnit"].Value?.ToString() ?? "";
                string qty = row.Cells["colQty"].Value?.ToString() ?? "";
                string price = row.Cells["colPrice"].Value?.ToString() ?? "";
                string total = row.Cells["colTotal"].Value?.ToString() ?? "";

                string formattedQty = FormatAmount(qty);
                string formattedPrice = FormatAmount(price);
                string formattedTotal = FormatAmount(total);


                string formattedProduct = TextFormatHelper.FormatMixedText(productName);

                // Row 1: Product Name (right aligned)
                e.Graphics.DrawString(formattedProduct, regularFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth - 5, lineHeight + 3),
                                     new StringFormat { Alignment = StringAlignment.Far });
                int detailsY = currentY + lineHeight;

                // Row 2: Total | Price | Qty+Unit
                e.Graphics.DrawString(formattedTotal, regularFont, Brushes.Black,
                                     new Rectangle(0, detailsY, 60, lineHeight + 3), rightFormat);
                e.Graphics.DrawString(formattedPrice, regularFont, Brushes.Black,
                                     new Rectangle(65, detailsY, 50, lineHeight + 3), rightFormat);

                string formattedQtyValue = $"{unit} {formattedQty}";
                e.Graphics.DrawString(formattedQtyValue, urduFont, Brushes.Black,
                                     new Rectangle(100, detailsY, 100, lineHeight + 3), rightFormat);

                currentY = detailsY + lineHeight + 4;
                e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
                currentY += 4;
            }

            // ─── TOTALS SECTION (FIXED) ───────────────────────────────────────────────
            currentY += sectionSpacing;

            string subtotal = FormatAmount(lblSubVal.Text);
            string discount = FormatAmount(lblDiscVal.Text);
            string netAmount = FormatAmount(lblNetVal.Text);
            string paid = FormatAmount(lblPaidVal.Text);
            string balance = FormatAmount(lblBalVal.Text);

            // Labels sit near the right edge; values sit immediately to the left of labels
            int labelWidth = 80;
            int valueWidth = 65;
            int labelX = paperWidth - labelWidth;      // e.g. 200
            int valueX = labelX - valueWidth - 4;      // e.g. 131  — right next to the label

            // Helper: draw one totals row
            void DrawTotalRow(string value, string label, System.Drawing.Font labelFont, bool isNegative = false)
            {
                string displayValue = isNegative ? $"-{value}" : value;
                e.Graphics.DrawString(displayValue, amountFont, Brushes.Black,
                                     new Rectangle(valueX, currentY, valueWidth, lineHeight + 2), rightFormat);
                e.Graphics.DrawString(label, labelFont, Brushes.Black,
                                     new Rectangle(labelX, currentY, labelWidth, lineHeight + 2), rightFormat);
                currentY += lineHeight + 4;
            }

            DrawTotalRow(subtotal, "سب ٹوٹل", urduFont);

            if (discount != "0" && discount != "—")
                DrawTotalRow(discount, "ڈسکاؤنٹ", urduFont, isNegative: true);

            DrawTotalRow(netAmount, "کل رقم", headerFont);
            DrawTotalRow(paid, "وصول رقم", headerFont);
            DrawTotalRow(balance, "بقایا", urduFont);

            // Payment method
            string paymentMethod = GetPaymentMethod();
            e.Graphics.DrawString($"ادائیگی کا طریقہ: {paymentMethod}", urduFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
            currentY += lineHeight + 4;
            // ─────────────────────────────────────────────────────────────────────────

            #region Checking if the status is Paid
            //// PAID STAMP
            //bool isPaid = CheckIfPaid();
            //if (isPaid)
            //{
            //    int stampSize = 70;
            //    int stampX = paperWidth - 120;
            //    int stampY = currentY - 50;

            //    var oldTransform = e.Graphics.Transform.Clone();
            //    e.Graphics.TranslateTransform(stampX + stampSize / 2, stampY + stampSize / 2);
            //    e.Graphics.RotateTransform(-15);
            //    e.Graphics.TranslateTransform(-(stampX + stampSize / 2), -(stampY + stampSize / 2));

            //    using (Pen pen = new Pen(System.Drawing.Color.Black, 2f))
            //    {
            //        e.Graphics.DrawEllipse(pen, stampX, stampY, stampSize, stampSize);
            //    }

            //    using (System.Drawing.Font stampFont = new System.Drawing.Font("Arial", 18, FontStyle.Regular))
            //    using (SolidBrush brush = new SolidBrush(System.Drawing.Color.Black))
            //    {
            //        StringFormat format = new StringFormat
            //        {
            //            Alignment = StringAlignment.Center,
            //            LineAlignment = StringAlignment.Center
            //        };
            //        e.Graphics.DrawString("PAID", stampFont, brush,
            //            new Rectangle(stampX, stampY, stampSize, stampSize), format);
            //    }

            //    e.Graphics.Transform = oldTransform;
            //}

            #endregion

            // NOTES
            if (lblNotesVal.Text != "—" && !string.IsNullOrEmpty(lblNotesVal.Text))
            {
                e.Graphics.DrawString("نوٹ:", headerFont, Brushes.Black,
                                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
                currentY += lineHeight + 2;

                string notes = lblNotesVal.Text;
                int maxCharsPerLine = 40;
                if (notes.Length > maxCharsPerLine)
                {
                    for (int i = 0; i < notes.Length; i += maxCharsPerLine)
                    {
                        string line = notes.Substring(i, Math.Min(maxCharsPerLine, notes.Length - i));
                        e.Graphics.DrawString(line, smallFont, Brushes.Black,
                                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
                        currentY += lineHeight + 2;
                    }
                }
                else
                {
                    e.Graphics.DrawString(notes, smallFont, Brushes.Black,
                                         new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
                    currentY += lineHeight + 4;
                }
            }

            currentY += lineHeight + 3;

            // Draw black line
            e.Graphics.DrawLine(new Pen(System.Drawing.Color.Black, 1), leftMargin, currentY, paperWidth, currentY);
            currentY += lineHeight + 1;

            // Advertisement - POS Software
            string advertisement = "03364978771 " + "سافٹ ویئر بنوانے کے لیے رابطہ نمبر";
            e.Graphics.DrawString(TextFormatHelper.FormatMixedText(advertisement), headerFont, Brushes.Black,
                                  new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);

            // Cleanup
            titleFont.Dispose();
            headerFont.Dispose();
            regularFont.Dispose();
            smallFont.Dispose();
            urduFont.Dispose();
            amountFont.Dispose();
        }

        private string FormatAmount(string amountText)
        {
            if (string.IsNullOrEmpty(amountText) || amountText == "—")
                return "0";

            amountText = amountText.Replace("Rs:", "").Replace("Rs.", "").Replace(",", "").Trim();

            if (decimal.TryParse(amountText, out decimal amount))
                return Math.Round(amount, 0).ToString("0");
            else if (amountText.Contains("."))
                return amountText.Split('.')[0];

            return amountText;
        }

        private string GetPaymentMethod()
        {
            return "نقد";
        }

        private bool CheckIfPaid()
        {
            if (lblBalVal.Text != null && lblBalVal.Text != "—")
            {
                string balanceText = lblBalVal.Text.Replace("Rs:", "").Replace("Rs.", "").Replace(",", "").Trim();
                if (decimal.TryParse(balanceText, out decimal balance))
                    return balance <= 0;
            }
            return false;
        }

        // ══════════════════════════════════════════════════════════════════════
        //  CLOSE
        // ══════════════════════════════════════════════════════════════════════

        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape) { this.Close(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _db.Dispose();
        }

        // ── Helpers ────────────────────────────────────────────────────────────
        private static void HoverBtn(Button b, System.Drawing.Color hover, System.Drawing.Color normal)
        {
            b.MouseEnter += (s, e) => b.BackColor = hover;
            b.MouseLeave += (s, e) => b.BackColor = normal;
        }

        private void BtnPDF_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.FileName = $"Invoice_{lblInvoiceNo.Text}";
                saveDialog.Filter = "PDF Files (*.pdf)|*.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (PrintDocument pd = new PrintDocument())
                        {
                            // Custom paper size (width: 8.27 inches, height: 11.69 inches for A4)
                            // Values are in hundredths of an inch
                            int width = (int)(8.27m * 100);  // 827
                            int height = (int)(11.69m * 100); // 1169

                            PaperSize customSize = new PaperSize("Custom", width, height);
                            pd.DefaultPageSettings.PaperSize = customSize;

                            // Adjust margins as needed (in hundredths of an inch)
                            pd.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

                            // Landscape or Portrait
                            pd.DefaultPageSettings.Landscape = false;

                            // Attach your print handler
                            pd.PrintPage += PrintPage;

                            // Set PDF printer
                            pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                            pd.PrinterSettings.PrintToFile = true;
                            pd.PrinterSettings.PrintFileName = saveDialog.FileName;

                            // Print
                            pd.Print();
                        }

                        // Optional: Open the PDF after creation
                        DialogResult result = MessageBox.Show(
                            "PDF generated successfully!\n\nDo you want to open it?",
                            "Success",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "PDF Generation Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}



//using POS_Shop.Models;
//using POS_Shop.Models.Suppliers;
//using System;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Controllers.Supplier
//{
//    /// <summary>
//    /// Read-only invoice detail popup.
//    ///
//    /// Open it from SupplierPaymentForm when the user clicks a cell in the
//    /// "Invoice No" column:
//    ///
//    ///     int purchaseId = (int)dgvInvoices.Rows[e.RowIndex].Tag;
//    ///     new PurchaseDetailForm(purchaseId).ShowDialog(this);
//    ///
//    /// Shows: supplier, purchase date, reference, all line items,
//    /// subtotal / discount / net, total paid, balance, payment status, notes.
//    /// Nothing can be edited.
//    /// </summary>
//    public partial class PurchaseDetailForm : Form
//    {
//        private readonly POSDbContext _db;
//        private readonly int _purchaseId;

//        // ── Constructor ────────────────────────────────────────────────────────
//        public PurchaseDetailForm(int purchaseId)
//        {
//            InitializeComponent();
//            _db = new POSDbContext();
//            _purchaseId = purchaseId;

//            // Hover effects
//            HoverBtn(btnClose, Color.FromArgb(207, 216, 220), Color.FromArgb(236, 239, 241));
//            HoverBtn(btnPrint,
//                     Color.FromArgb(13, 71, 161),
//                     Color.FromArgb(21, 101, 192));

//            this.Load += (s, e) => { LoadInvoice(); ResizeFooterCards(); };
//            this.Resize += (s, e) => ResizeFooterCards();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  LOAD DATA
//        // ══════════════════════════════════════════════════════════════════════

//        private void LoadInvoice()
//        {
//            try
//            {
//                var purchase = _db.Purchases
//                    .Include("PurchaseItems")
//                    .Include("Supplier")
//                    .FirstOrDefault(p => p.Id == _purchaseId);

//                if (purchase == null)
//                {
//                    MessageBox.Show("Invoice not found.", "Error",
//                        MessageBoxButtons.OK, MessageBoxIcon.Error);
//                    this.Close();
//                    return;
//                }

//                // ── Form title ────────────────────────────────────────────────
//                this.Text = $"Invoice — {purchase.InvoiceNumber}";
//                lblInvoiceNo.Text = purchase.InvoiceNumber;

//                // ── Status badge ──────────────────────────────────────────────
//                ApplyStatusBadge(purchase.PaymentStatus);

//                // ── Meta strip ────────────────────────────────────────────────
//                if (purchase.Supplier != null)
//                    lblSupVal.Text = $"{purchase.Supplier.SupplierName}  —  {purchase.Supplier.ShopName}";
//                else
//                    lblSupVal.Text = $"Supplier #{purchase.SupplierId}";

//                lblDateVal.Text = purchase.PurchaseDate.ToString("dd MMM yyyy  (dddd)");
//                lblRefVal.Text = !string.IsNullOrWhiteSpace(purchase.SupplierReferenceNo)
//                                     ? purchase.SupplierReferenceNo
//                                     : "—";
//                lblCreatedVal.Text = purchase.CreatedAt.ToLocalTime().ToString("dd MMM yyyy  HH:mm");

//                // ── Items grid ────────────────────────────────────────────────
//                var items = purchase.PurchaseItems
//                    .Where(i => !i.IsDeleted)
//                    .OrderBy(i => i.Id)
//                    .ToList();

//                int sr = 1;
//                foreach (var item in items)
//                {
//                    // Resolve product
//                    string name = $"Product #{item.ProductId}";
//                    string code = "";
//                    var prod = _db.Products.FirstOrDefault(p => p.Id == item.ProductId);
//                    if (prod != null)
//                    {
//                        name = prod.ProductEnglishName;
//                        code = prod.SearchByProductCode ?? "";
//                    }

//                    // Resolve unit
//                    string unit = "—";
//                    if (item.ProductUnitId.HasValue && item.ProductUnitId > 0)
//                    {
//                        var u = _db.ProductUnits.FirstOrDefault(x => x.Id == item.ProductUnitId);
//                        if (u != null) unit = u.Name;
//                    }

//                    int idx = dgvItems.Rows.Add();
//                    var row = dgvItems.Rows[idx];
//                    row.Cells["colSrNo"].Value = sr++;
//                    row.Cells["colCode"].Value = code;
//                    row.Cells["colProduct"].Value = name;
//                    row.Cells["colUnit"].Value = unit;
//                    row.Cells["colQty"].Value = item.Quantity;
//                    row.Cells["colPrice"].Value = item.PurchasePrice;
//                    row.Cells["colTotal"].Value = item.TotalPrice;
//                }

//                // ── Grid title ────────────────────────────────────────────────
//                lblGridTitle.Text = $"  Items Purchased  ({items.Count} item{(items.Count != 1 ? "s" : "")})";

//                // ── Totals ────────────────────────────────────────────────────
//                lblSubVal.Text = $"Rs. {purchase.TotalAmount:N2}";
//                lblDiscVal.Text = purchase.Discount > 0
//                                  ? $"−  Rs. {purchase.Discount:N2}"
//                                  : "Rs. 0.00";
//                lblNetVal.Text = $"Rs. {purchase.NetAmount:N2}";

//                // ── Payment summary ───────────────────────────────────────────
//                lblPaidVal.Text = $"Rs. {purchase.TotalPaid:N2}";
//                lblBalVal.Text = $"Rs. {purchase.Balance:N2}";
//                lblBalVal.ForeColor = purchase.Balance <= 0
//                    ? Color.FromArgb(46, 125, 50)
//                    : Color.FromArgb(198, 40, 40);

//                // ── Notes ─────────────────────────────────────────────────────
//                lblNotesVal.Text = !string.IsNullOrWhiteSpace(purchase.Notes)
//                                   ? purchase.Notes
//                                   : "—";
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Error loading invoice:\n" + ex.Message, "Error",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  STATUS BADGE
//        // ══════════════════════════════════════════════════════════════════════

//        private void ApplyStatusBadge(PurchasePaymentStatus status)
//        {
//            switch (status)
//            {
//                case PurchasePaymentStatus.Pending:
//                    pnlStatusBadge.BackColor = Color.FromArgb(245, 124, 0);   // orange
//                    lblStatus.Text = "⏳  PENDING";
//                    lblPayStatVal.Text = "Pending";
//                    lblPayStatVal.ForeColor = Color.FromArgb(245, 124, 0);
//                    pnlPayStatus.BackColor = Color.FromArgb(255, 243, 224);
//                    break;

//                case PurchasePaymentStatus.PartiallyPaid:
//                    pnlStatusBadge.BackColor = Color.FromArgb(2, 119, 189);   // blue
//                    lblStatus.Text = "🔵  PARTIAL";
//                    lblPayStatVal.Text = "Partially Paid";
//                    lblPayStatVal.ForeColor = Color.FromArgb(2, 119, 189);
//                    pnlPayStatus.BackColor = Color.FromArgb(225, 245, 254);
//                    break;

//                case PurchasePaymentStatus.Paid:
//                    pnlStatusBadge.BackColor = Color.FromArgb(46, 125, 50);   // green
//                    lblStatus.Text = "✔  PAID";
//                    lblPayStatVal.Text = "Fully Paid";
//                    lblPayStatVal.ForeColor = Color.FromArgb(46, 125, 50);
//                    pnlPayStatus.BackColor = Color.FromArgb(200, 230, 201);
//                    break;
//            }
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  PRINT / EXPORT
//        // ══════════════════════════════════════════════════════════════════════

//        private void BtnPrint_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                var pd = new System.Drawing.Printing.PrintDocument();
//                pd.PrintPage += PrintPage;

//                using (var dlg = new PrintPreviewDialog
//                {
//                    Document = pd,
//                    Width = 920,
//                    Height = 720,
//                    StartPosition = FormStartPosition.CenterParent
//                })
//                {
//                    dlg.ShowDialog(this);
//                }
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show("Print error:\n" + ex.Message, "Print",
//                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
//            }
//        }

//        private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            var g = e.Graphics;
//            var blue = new SolidBrush(Color.FromArgb(21, 101, 192));
//            var grey = new SolidBrush(Color.FromArgb(120, 144, 156));
//            var black = Brushes.Black;

//            var bold18 = new Font("Segoe UI", 18F, FontStyle.Bold);
//            var bold12 = new Font("Segoe UI", 12F, FontStyle.Bold);
//            var bold11 = new Font("Segoe UI", 11F, FontStyle.Bold);
//            var bold10 = new Font("Segoe UI", 10F, FontStyle.Bold);
//            var reg10 = new Font("Segoe UI", 10F);
//            var reg9 = new Font("Segoe UI", 9F);

//            int x = 40, y = 40, lh = 22;

//            // Title block
//            g.DrawString("Purchase Invoice", bold18, blue, x, y);
//            g.DrawString(lblInvoiceNo.Text, bold12, new SolidBrush(Color.FromArgb(144, 164, 174)), x + 260, y + 4);
//            g.DrawString(lblStatus.Text, bold10, grey, x + 540, y + 6);
//            y += 40;

//            // Meta
//            g.DrawString($"Supplier  :  {lblSupVal.Text}", reg10, black, x, y); y += lh;
//            g.DrawString($"Date      :  {lblDateVal.Text}", reg10, black, x, y); y += lh;
//            if (lblRefVal.Text != "—")
//            { g.DrawString($"Ref No    :  {lblRefVal.Text}", reg10, black, x, y); y += lh; }
//            y += 8;

//            // Column header bar
//            g.FillRectangle(blue, x, y, 720, 26);
//            g.DrawString("#", bold10, Brushes.White, x + 4, y + 3);
//            g.DrawString("Product", bold10, Brushes.White, x + 34, y + 3);
//            g.DrawString("Unit", bold10, Brushes.White, x + 310, y + 3);
//            g.DrawString("Qty", bold10, Brushes.White, x + 380, y + 3);
//            g.DrawString("Price", bold10, Brushes.White, x + 440, y + 3);
//            g.DrawString("Total", bold10, Brushes.White, x + 580, y + 3);
//            y += 28;

//            // Rows
//            bool alt = false;
//            foreach (DataGridViewRow row in dgvItems.Rows)
//            {
//                if (alt)
//                    g.FillRectangle(new SolidBrush(Color.FromArgb(245, 249, 255)), x, y, 720, 20);
//                g.DrawString(row.Cells["colSrNo"].Value?.ToString(), reg9, black, x + 4, y + 2);
//                g.DrawString(row.Cells["colProduct"].Value?.ToString(), reg9, black, x + 34, y + 2);
//                g.DrawString(row.Cells["colUnit"].Value?.ToString(), reg9, black, x + 310, y + 2);
//                g.DrawString(row.Cells["colQty"].Value?.ToString(), reg9, black, x + 380, y + 2);
//                g.DrawString(row.Cells["colPrice"].Value?.ToString(), reg9, black, x + 440, y + 2);
//                g.DrawString(row.Cells["colTotal"].Value?.ToString(), reg9, black, x + 580, y + 2);
//                y += 20;
//                alt = !alt;
//            }

//            y += 10;
//            g.DrawLine(Pens.LightGray, x, y, x + 720, y);
//            y += 10;

//            // Totals
//            g.DrawString($"Subtotal   :  {lblSubVal.Text}", reg10, black, x + 440, y); y += lh;
//            g.DrawString($"Discount   :  {lblDiscVal.Text}", reg10, black, x + 440, y); y += lh;
//            g.DrawString($"Net Amount :  {lblNetVal.Text}", bold11, blue, x + 440, y); y += lh + 6;
//            g.DrawString($"Paid       :  {lblPaidVal.Text}", reg10, black, x + 440, y); y += lh;
//            g.DrawString($"Balance    :  {lblBalVal.Text}", bold10, black, x + 440, y); y += lh;

//            // Notes
//            if (lblNotesVal.Text != "—")
//            {
//                y += 10;
//                g.DrawString("Notes: " + lblNotesVal.Text, reg9, grey, x, y);
//            }

//            // Cleanup
//            foreach (var f in new Font[] { bold18, bold12, bold11, bold10, reg10, reg9 }) f.Dispose();
//            blue.Dispose(); grey.Dispose();
//        }

//        // ══════════════════════════════════════════════════════════════════════
//        //  CLOSE
//        // ══════════════════════════════════════════════════════════════════════

//        private void BtnClose_Click(object sender, EventArgs e) => this.Close();

//        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
//        {
//            if (keyData == Keys.Escape) { this.Close(); return true; }
//            return base.ProcessCmdKey(ref msg, keyData);
//        }

//        protected override void OnFormClosed(FormClosedEventArgs e)
//        {
//            base.OnFormClosed(e);
//            _db.Dispose();
//        }

//        // ── Helpers ────────────────────────────────────────────────────────────
//        private static void HoverBtn(Button b, Color hover, Color normal)
//        {
//            b.MouseEnter += (s, e) => b.BackColor = hover;
//            b.MouseLeave += (s, e) => b.BackColor = normal;
//        }

//        /// <summary>
//        /// Keeps both footer cards side-by-side and equal-width when the form
//        /// is resized.  Each card takes ~48% of the footer width with a 10px gap
//        /// between them and 14px margins on each side.
//        /// </summary>
//        private void ResizeFooterCards()
//        {
//            int margin = 14;
//            int gap = 10;
//            int available = pnlFooter.ClientSize.Width - (margin * 2) - gap;
//            int half = available / 2;

//            pnlTotals.Location = new Point(margin, 10);
//            pnlTotals.Width = half;

//            pnlPayStatus.Location = new Point(margin + half + gap, 10);
//            pnlPayStatus.Width = half;

//            // Keep value labels right-aligned inside their respective panels
//            lblSubVal.Location = new Point(pnlTotals.Width - 164, lblSubVal.Top);
//            lblDiscVal.Location = new Point(pnlTotals.Width - 164, lblDiscVal.Top);
//            lblNetVal.Location = new Point(pnlTotals.Width - 164, lblNetVal.Top);
//            pnlSep.Width = pnlTotals.Width - 28;

//            lblPaidVal.Location = new Point(pnlPayStatus.Width - 174, lblPaidVal.Top);
//            lblBalVal.Location = new Point(pnlPayStatus.Width - 174, lblBalVal.Top);
//        }
//    }
//}