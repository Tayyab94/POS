using DocumentFormat.OpenXml.Spreadsheet;
using Org.BouncyCastle.Asn1.Cmp;
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
            var g = e.Graphics;
            var blue = new SolidBrush(System.Drawing.Color.FromArgb(21, 101, 192));
            var grey = new SolidBrush(System.Drawing.Color.FromArgb(120, 144, 156));
            var black = Brushes.Black;

            var bold18 = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            var bold12 = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            var bold11 = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            var bold10 = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            var reg10 = new System.Drawing.Font("Segoe UI", 10F);
            var reg9 = new System.Drawing.Font("Segoe UI", 9F);

            int x = 40, y = 40, lh = 22;

            // Title block
            g.DrawString("Purchase Invoice", bold18, blue, x, y);
            g.DrawString(lblInvoiceNo.Text, bold12, new SolidBrush(System.Drawing.Color.FromArgb(144, 164, 174)), x + 260, y + 4);
            g.DrawString(lblStatus.Text, bold10, grey, x + 540, y + 6);
            y += 40;

            // Meta
            g.DrawString($"Supplier  :  {lblSupVal.Text}", reg10, black, x, y); y += lh;
            g.DrawString($"Date      :  {lblDateVal.Text}", reg10, black, x, y); y += lh;
            if (lblRefVal.Text != "—")
            { g.DrawString($"Ref No    :  {lblRefVal.Text}", reg10, black, x, y); y += lh; }
            y += 8;

            // Column header bar
            g.FillRectangle(blue, x, y, 720, 26);
            g.DrawString("#", bold10, Brushes.White, x + 4, y + 3);
            g.DrawString("Product", bold10, Brushes.White, x + 34, y + 3);
            g.DrawString("Unit", bold10, Brushes.White, x + 310, y + 3);
            g.DrawString("Qty", bold10, Brushes.White, x + 380, y + 3);
            g.DrawString("Price", bold10, Brushes.White, x + 440, y + 3);
            g.DrawString("Total", bold10, Brushes.White, x + 580, y + 3);
            y += 28;

            // Rows
            bool alt = false;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (alt)
                    g.FillRectangle(new SolidBrush(System.Drawing.Color.FromArgb(245, 249, 255)), x, y, 720, 20);
                g.DrawString(row.Cells["colSrNo"].Value?.ToString(), reg9, black, x + 4, y + 2);
                g.DrawString(row.Cells["colProduct"].Value?.ToString(), reg9, black, x + 34, y + 2);
                g.DrawString(row.Cells["colUnit"].Value?.ToString(), reg9, black, x + 310, y + 2);
                g.DrawString(row.Cells["colQty"].Value?.ToString(), reg9, black, x + 380, y + 2);
                g.DrawString(row.Cells["colPrice"].Value?.ToString(), reg9, black, x + 440, y + 2);
                g.DrawString(row.Cells["colTotal"].Value?.ToString(), reg9, black, x + 580, y + 2);
                y += 20;
                alt = !alt;
            }

            y += 10;
            g.DrawLine(Pens.LightGray, x, y, x + 720, y);
            y += 10;

            // Totals
            g.DrawString($"Subtotal   :  {lblSubVal.Text}", reg10, black, x + 440, y); y += lh;
            g.DrawString($"Discount   :  {lblDiscVal.Text}", reg10, black, x + 440, y); y += lh;
            g.DrawString($"Net Amount :  {lblNetVal.Text}", bold11, blue, x + 440, y); y += lh + 6;
            g.DrawString($"Paid       :  {lblPaidVal.Text}", reg10, black, x + 440, y); y += lh;
            g.DrawString($"Balance    :  {lblBalVal.Text}", bold10, black, x + 440, y); y += lh;

            // Notes
            if (lblNotesVal.Text != "—")
            {
                y += 10;
                g.DrawString("Notes: " + lblNotesVal.Text, reg9, grey, x, y);
            }

            // Cleanup
            foreach (var f in new System.Drawing.Font[] { bold18, bold12, bold11, bold10, reg10, reg9 }) f.Dispose();
            blue.Dispose(); grey.Dispose();
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