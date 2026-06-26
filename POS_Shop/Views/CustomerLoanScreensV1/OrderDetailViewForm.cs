using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.CustomerLoanScreensV1
{
    /// <summary>
    /// Read-only view of a single Order opened from the customer ledger
    /// by clicking an INV- note link.
    /// Shows order meta (customer, date, payment type),
    /// all OrderDetail line items in a grid, and the bill summary.
    /// </summary>
    public partial class OrderDetailViewForm : Form
    {
        // ─── Fields ──────────────────────────────────────────────────
        private readonly Order _order;

        // ─── Constructor ─────────────────────────────────────────────
        public OrderDetailViewForm(Order order)
        {
            InitializeComponent();
            _order = order ?? throw new ArgumentNullException(nameof(order));
        }

        // ─── Load ─────────────────────────────────────────────────────
        private void OrderDetailViewForm_Load(object sender, EventArgs e)
        {
            try
            {
                PopulateHeader();
                PopulateMeta();
                SetupGrid();
                PopulateGrid();
                PopulateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order details:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ─── Header ───────────────────────────────────────────────────
        private void PopulateHeader()
        {
            string inv = string.IsNullOrWhiteSpace(_order.InvoiceNumber)
                ? $"Order #{_order.Id}"
                : _order.InvoiceNumber;

            this.Text = $"Order — {inv}";
            lblTitle.Text = "🧾 Order Details";
            lblInvoiceNo.Text = $"Invoice:  {inv}";
        }

        // ─── Meta row ─────────────────────────────────────────────────
        private void PopulateMeta()
        {
            // Customer
            if (_order.Customer != null && !string.IsNullOrWhiteSpace(_order.Customer.CustomerName))
                lblCustomerVal.Text = _order.Customer.CustomerName;
            else if (_order.customerId.HasValue)
                lblCustomerVal.Text = $"Customer #{_order.customerId}";
            else
                lblCustomerVal.Text = "Walk-in";

            // Date
            lblDateVal.Text = _order.CreatedDate.ToString("dd-MMM-yyyy  hh:mm tt");

            // Payment type
            string pt = string.IsNullOrWhiteSpace(_order.paymentType)
                ? "Cash" : _order.paymentType;
            lblPaymentTypeVal.Text = pt;

            // Color-code payment type
            lblPaymentTypeVal.ForeColor = pt.Equals("Cash", StringComparison.OrdinalIgnoreCase)
                ? Color.FromArgb(39, 174, 96)
                : Color.FromArgb(0, 102, 204);
        }

        // ─── Grid Setup ───────────────────────────────────────────────
        private void SetupGrid()
        {
            OrderItemsGrid.AutoGenerateColumns = false;
            OrderItemsGrid.Font = new Font("Segoe UI", 9);
            OrderItemsGrid.RowTemplate.Height = 34;
            OrderItemsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250);
            OrderItemsGrid.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            OrderItemsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            OrderItemsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            OrderItemsGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            OrderItemsGrid.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 0, 6, 0);

            OrderItemsGrid.Columns.Clear();

            // # (row number)
            AddCol("RowNum", "#", 40,
                align: DataGridViewContentAlignment.MiddleCenter);

            // Product name
            AddCol("ProductName", "Product / Item", 240);

            // Quantity
            AddCol("QuantityDisplay", "Qty", 80,
                align: DataGridViewContentAlignment.MiddleCenter);

            // Unit type
            AddCol("QuantityType", "Unit", 80,
                align: DataGridViewContentAlignment.MiddleCenter);

            // Unit price
            AddCol("PriceDisplay", "Unit Price (PKR)", 130,
                align: DataGridViewContentAlignment.MiddleRight);

            // Line total
            AddCol("LineTotalDisplay", "Line Total (PKR)", 140,
                align: DataGridViewContentAlignment.MiddleRight);

            // Product detail / notes
            AddCol("ProductDetail", "Notes", 150);

            // Style header row
            OrderItemsGrid.CellFormatting += OrderItemsGrid_CellFormatting;
        }

        private void AddCol(string prop, string header, int width,
            DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleLeft)
        {
            OrderItemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = prop,
                HeaderText = header,
                Width = width,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = align }
            });
        }

        // ─── Grid CellFormatting ──────────────────────────────────────
        private void OrderItemsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string prop = OrderItemsGrid.Columns[e.ColumnIndex].DataPropertyName;

            // Line total — blue bold
            if (prop == "LineTotalDisplay")
            {
                e.CellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                e.CellStyle.ForeColor = Color.FromArgb(0, 102, 204);
            }

            // Row number — gray
            if (prop == "RowNum")
                e.CellStyle.ForeColor = Color.Gray;
        }

        // ─── Grid Data ────────────────────────────────────────────────
        private void PopulateGrid()
        {
            var items = BuildGridRows();
            OrderItemsGrid.DataSource = items;
            lblGridTitle.Text = $"ORDER ITEMS  ({items.Count} line{(items.Count == 1 ? "" : "s")})";
        }

        private List<OrderDetailRow> BuildGridRows()
        {
            var rows = new List<OrderDetailRow>();
            if (_order.OrderDetails == null) return rows;

            int i = 1;
            foreach (var d in _order.OrderDetails.OrderBy(x => x.Id))
            {
                // Resolve product name: prefer Product.Name, fallback to OtherProductName
                string productName = "—";
                if (d.Product != null && !string.IsNullOrWhiteSpace(d.Product.ProductUrduName))
                    productName = d.Product.ProductUrduName;
                else if (!string.IsNullOrWhiteSpace(d.OtherProductName))
                    productName = d.OtherProductName;

                float lineTotal = d.Quantity * d.Price;

                rows.Add(new OrderDetailRow
                {
                    RowNum = i++,
                    ProductName = productName,
                    QuantityDisplay = d.Quantity.ToString("N0"),
                    QuantityType = string.IsNullOrWhiteSpace(d.QuantityType) ? "—" : d.QuantityType,
                    PriceDisplay = $"{d.Price:N2}",
                    LineTotalDisplay = $"{lineTotal:N2}",
                    ProductDetail = string.IsNullOrWhiteSpace(d.ProductDetail) ? "" : d.ProductDetail
                });
            }
            return rows;
        }

        // ─── Totals ───────────────────────────────────────────────────
        private void PopulateTotals()
        {
            float total = _order.TotalBill;
            float paid = _order.ReceiveAmount;
            float balance = total - paid;

            lblTotalVal.Text = $"PKR {total:N2}";
            lblPaidVal.Text = $"PKR {paid:N2}";
            lblBalanceVal.Text = $"PKR {Math.Abs(balance):N2}";

            if (balance <= 0)
            {
                // Fully paid or overpaid
                lblBalanceLbl.Text = balance < 0 ? "OVERPAID" : "BALANCE DUE";
                lblBalanceVal.ForeColor = balance < 0
                    ? Color.FromArgb(0, 102, 204)
                    : Color.FromArgb(39, 174, 96);
            }
            else
            {
                lblBalanceLbl.Text = "BALANCE DUE";
                lblBalanceVal.ForeColor = Color.FromArgb(192, 0, 0);
            }
        }

        // ─── Print Receipt ────────────────────────────────────────────
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                using (PrintDialog pd = new PrintDialog())
                {
                    var doc = new PrintDocument();
                    doc.DocumentName = $"Receipt-{_order.InvoiceNumber ?? _order.Id.ToString()}";
                    doc.PrintPage += PrintReceiptPage;

                    pd.Document = doc;
                    if (pd.ShowDialog(this) == DialogResult.OK)
                        doc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print error:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReceiptPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // ── Fonts ──────────────────────────────────────────────────
            using (Font fTitle = new Font("Segoe UI", 14, FontStyle.Bold))
            using (Font fBold = new Font("Segoe UI", 9, FontStyle.Bold))
            using (Font fNormal = new Font("Segoe UI", 9))
            using (Font fSmall = new Font("Segoe UI", 8))
            using (Font fHeader = new Font("Segoe UI", 8, FontStyle.Bold))
            using (Pen linePen = new Pen(Color.FromArgb(200, 200, 200), 0.5f))
            using (SolidBrush darkBrush = new SolidBrush(Color.FromArgb(44, 62, 80)))
            using (SolidBrush grayBrush = new SolidBrush(Color.Gray))
            using (SolidBrush blueBrush = new SolidBrush(Color.FromArgb(0, 102, 204)))
            using (SolidBrush redBrush = new SolidBrush(Color.FromArgb(192, 0, 0)))
            using (SolidBrush greenBrush = new SolidBrush(Color.FromArgb(39, 174, 96)))
            using (SolidBrush headerFill = new SolidBrush(Color.FromArgb(44, 62, 80)))
            {
                const int lm = 40;    // left margin
                const int pw = 520;   // print width
                int y = 30;

                // ── Title ──────────────────────────────────────────────
                g.DrawString("Sales Receipt", fTitle, darkBrush, lm, y);
                y += 28;

                string inv = string.IsNullOrWhiteSpace(_order.InvoiceNumber)
                    ? $"Order #{_order.Id}"
                    : _order.InvoiceNumber;
                g.DrawString($"Invoice: {inv}", fNormal, grayBrush, lm, y);
                y += 18;
                g.DrawString($"Date: {_order.CreatedDate:dd-MMM-yyyy  hh:mm tt}", fNormal, grayBrush, lm, y);
                y += 18;

                string customerName = _order.Customer?.CustomerName ?? "Walk-in";
                g.DrawString($"Customer: {customerName}", fNormal, darkBrush, lm, y);
                y += 18;
                g.DrawString($"Payment: {_order.paymentType ?? "Cash"}", fNormal, darkBrush, lm, y);
                y += 16;

                // Divider
                g.DrawLine(linePen, lm, y, lm + pw, y);
                y += 10;

                // ── Grid header ────────────────────────────────────────
                const int c1 = 180; // product width
                const int c2 = 60;  // qty
                const int c3 = 70;  // unit
                const int c4 = 90;  // price
                const int c5 = 100; // total

                int x1 = lm, x2 = x1 + c1, x3 = x2 + c2, x4 = x3 + c3, x5 = x4 + c4;

                // Header bar
                g.FillRectangle(headerFill, lm, y, pw, 20);
                g.DrawString("Product", fHeader, Brushes.White,
                    new Rectangle(x1 + 2, y, c1, 20),
                    new StringFormat { LineAlignment = StringAlignment.Center });
                g.DrawString("Qty", fHeader, Brushes.White,
                    new Rectangle(x2, y, c2, 20),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                g.DrawString("Unit", fHeader, Brushes.White,
                    new Rectangle(x3, y, c3, 20),
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                g.DrawString("Price", fHeader, Brushes.White,
                    new Rectangle(x4, y, c4, 20),
                    new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                g.DrawString("Total", fHeader, Brushes.White,
                    new Rectangle(x5, y, c5 - 4, 20),
                    new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                y += 20;

                // ── Rows ───────────────────────────────────────────────
                bool alt = false;
                if (_order.OrderDetails != null)
                {
                    foreach (var d in _order.OrderDetails.OrderBy(x => x.Id))
                    {
                        string productName = "—";
                        if (d.Product != null && !string.IsNullOrWhiteSpace(d.Product.ProductUrduName))
                            productName = d.Product.ProductUrduName;
                        else if (!string.IsNullOrWhiteSpace(d.OtherProductName))
                            productName = d.OtherProductName;

                        float lineTotal = d.Quantity * d.Price;

                        if (alt)
                            g.FillRectangle(
                                new SolidBrush(Color.FromArgb(248, 248, 248)),
                                lm, y, pw, 18);

                        var rf = new StringFormat
                        { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };
                        var cf = new StringFormat
                        { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        var lf = new StringFormat { LineAlignment = StringAlignment.Center };

                        g.DrawString(productName, fSmall, darkBrush,
                            new Rectangle(x1 + 2, y, c1 - 4, 18), lf);
                        g.DrawString(d.Quantity.ToString("N0"), fSmall, darkBrush,
                            new Rectangle(x2, y, c2, 18), cf);
                        g.DrawString(d.QuantityType ?? "", fSmall, grayBrush,
                            new Rectangle(x3, y, c3, 18), cf);
                        g.DrawString(d.Price.ToString("N2"), fSmall, grayBrush,
                            new Rectangle(x4, y, c4, 18), rf);
                        g.DrawString(lineTotal.ToString("N2"), fSmall, blueBrush,
                            new Rectangle(x5, y, c5 - 4, 18), rf);

                        g.DrawLine(linePen, lm, y + 18, lm + pw, y + 18);
                        y += 18;
                        alt = !alt;
                    }
                }

                y += 10;
                // Divider
                g.DrawLine(new Pen(Color.FromArgb(44, 62, 80), 1), lm, y, lm + pw, y);
                y += 10;

                // ── Totals ─────────────────────────────────────────────
                float total = _order.TotalBill;
                float paid = _order.ReceiveAmount;
                float balance = total - paid;

                var totSfRight = new StringFormat
                { Alignment = StringAlignment.Far };

                int totLx = lm + pw - 220;
                int totVx = lm + pw - 4;
                const int totRowH = 20;

                g.DrawString("Total Bill:", fBold, darkBrush, totLx, y);
                g.DrawString($"PKR {total:N2}", fBold, darkBrush,
                    new Rectangle(totLx, y, 220, totRowH), totSfRight);
                y += totRowH;

                g.DrawString("Amount Paid:", fBold, greenBrush, totLx, y);
                g.DrawString($"PKR {paid:N2}", fBold, greenBrush,
                    new Rectangle(totLx, y, 220, totRowH), totSfRight);
                y += totRowH;

                Brush balBrush = balance > 0 ? redBrush
                    : (balance < 0 ? blueBrush : greenBrush);
                string balLabel = balance > 0 ? "Balance Due:" : (balance < 0 ? "Overpaid:" : "Settled:");
                g.DrawString(balLabel, fBold, balBrush, totLx, y);
                g.DrawString($"PKR {Math.Abs(balance):N2}", fBold, balBrush,
                    new Rectangle(totLx, y, 220, totRowH), totSfRight);
                y += totRowH + 16;

                // ── Footer ─────────────────────────────────────────────
                g.DrawLine(linePen, lm, y, lm + pw, y);
                y += 8;
                g.DrawString($"Printed: {DateTime.Now:dd-MMM-yyyy  HH:mm}",
                    fSmall, grayBrush, lm, y);
            }

            e.HasMorePages = false;
        }

        // ─── Close ────────────────────────────────────────────────────
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ─── Grid Row Model ───────────────────────────────────────────
        /// <summary>Flat projection of OrderDetail for DataGridView binding.</summary>
        private class OrderDetailRow
        {
            public int RowNum { get; set; }
            public string ProductName { get; set; }
            public string QuantityDisplay { get; set; }
            public string QuantityType { get; set; }
            public string PriceDisplay { get; set; }
            public string LineTotalDisplay { get; set; }
            public string ProductDetail { get; set; }
        }
    }
}