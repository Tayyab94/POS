////using POS_Shop.Helpers;
////using POS_Shop.Models;
////using POS_Shop.Repositories;
////using System;
////using System.Collections.Generic;
////using System.ComponentModel;
////using System.Data;
////using System.Drawing;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.Windows.Forms;

////namespace POS_Shop.Views.Controllers.Order
////{
////    public partial class TempOrderControl : UserControl
////    {


////        private int PageSize = 100;
////        private int PageIndex = 1;
////        private int RecordCount = 0;
////        private string SearchTerm = "";
////        public bool isRecordSelected = false;
////        public string CustomerName { get; set; } = string.Empty;
////        public int CustomerId { get; set; } = 0;
////        public float ReceivedAmount = 0;

////        public TempOrderControl()
////        {
////            InitializeComponent();
////            this.InvoiceNoLbl.Text = string.Empty;
////            this.isRecordSelected = false;
////            this.Load += OrdersControlUI_Load;

////            // Make sure control can receive focus
////            this.SetStyle(ControlStyles.Selectable, true);
////            this.TabStop = true;

////            // Click/Enter events to ensure control gets focus
////            this.Enter += (s, e) => this.Focus();
////            this.Click += (s, e) => this.Focus();

////            OrderListDataGrid.KeyDown += OrderListDataGrid_KeyDown;

////        }
////        // Override ProcessCmdKey to intercept keyboard shortcuts
////        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
////        {
////            // Check for Escape
////            if (keyData == (Keys.F4 | Keys.Alt))
////            {
////                Form parentForm = this.FindForm();
////                parentForm?.Close();
////                return true; // Key has been handled
////            }
////            return base.ProcessCmdKey(ref msg, keyData);
////        }
////        private async void OrdersControlUI_Load(object sender, EventArgs e)
////        {


////            LoadingManager.ShowLoading();
////            await LoadOrdersForDataGridView();
////            LoadingManager.HideLoading();
////        }


////        private async Task LoadOrdersForDataGridView()
////        {
////            using (var context = new POSDbContext())
////            {
////                var orderRepository = new OrderRepository(context);
////                //var cities = await cityRepository.GetCitiesListAsync();

////                var result = await orderRepository.GetTempOrderPagingListAsync(PageIndex, PageSize, SearchTerm);
////                RecordCount = result.totalCount;
////                DataTable dt = new DataTable();
////                dt.Columns.Add("Invoice No", typeof(string));
////                dt.Columns.Add("Total Bill", typeof(float));
////                dt.Columns.Add("Received Amt", typeof(float));
////                dt.Columns.Add("Customer", typeof(string));
////                dt.Columns.Add("CustomerId", typeof(int));
////                dt.Columns.Add("Date", typeof(DateTime));

////                foreach (var item in result.data)
////                {
////                    dt.Rows.Add( item.InvoiceNumber, item.TotalBill,item.ReceiveAmount, item.CustomerName, item.customerId, item.CreatedDate);
////                }

////                //CountryDatagridView.AutoGenerateColumns = true;
////                OrderListDataGrid.ReadOnly = true;
////                OrderListDataGrid.AllowUserToAddRows = false;

////                OrderListDataGrid.DataSource = dt;
////                OrderListDataGrid.Columns[0].Width = 150;

////                UpdatePager();
////            }
////        }
////        private void UpdatePager()
////        {
////            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
////            //  lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

////            PreviousPageBtn.Enabled = PageIndex > 1;
////            NextPageBtn.Enabled = PageIndex < totalPages;
////        }

////        private async void SearchOrderTxt_TextChange(object sender, EventArgs e)
////        {
////            PageIndex = 1;
////            SearchTerm = SearchOrderTxt.Text.Trim();
////            await LoadOrdersForDataGridView();
////        }

////        private async void NextPageBtn_Click_1(object sender, EventArgs e)
////        {
////            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
////            if (PageIndex < totalPages)
////            {
////                PageIndex++;
////                await LoadOrdersForDataGridView();
////            }
////        }

////        private async void PreviousPageBtn_Click_1(object sender, EventArgs e)
////        {
////            if (PageIndex > 1)
////            {
////                PageIndex--;
////                await LoadOrdersForDataGridView();
////            }
////        }

////        //private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
////        //{
////        //    if (OrderListDataGrid.Rows.Count > 0)
////        //    {
////        //        InvoiceNoLbl.Text = (string)OrderListDataGrid.CurrentRow.Cells[0].Value;
////        //        CustomerName = OrderListDataGrid.CurrentRow.Cells[2].Value != DBNull.Value ? (string)OrderListDataGrid.CurrentRow.Cells[2].Value : string.Empty;
////        //        CustomerId = OrderListDataGrid.CurrentRow.Cells[3].Value != DBNull.Value ? Convert.ToInt32(OrderListDataGrid.CurrentRow.Cells[3].Value) : 0;

////        //        isRecordSelected = true;
////        //        // Close the parent form
////        //        Form parentForm = this.FindForm();
////        //        parentForm?.Close();

////        //    }
////        //}


////        private void SelectCurrentRowAndClose()
////        {
////            if (OrderListDataGrid.Rows.Count > 0 &&
////                OrderListDataGrid.CurrentRow != null &&
////                OrderListDataGrid.CurrentRow.Index >= 0)
////            {
////                DataGridViewRow currentRow = OrderListDataGrid.CurrentRow;

////                if (currentRow.Cells[0].Value != null)
////                {
////                    InvoiceNoLbl.Text = currentRow.Cells[0].Value.ToString();
////                }
////                ReceivedAmount =float.Parse(currentRow.Cells[2].Value.ToString());
////                CustomerName = currentRow.Cells[3].Value != DBNull.Value
////                    ? (string)currentRow.Cells[3].Value
////                    : string.Empty;

////                CustomerId = currentRow.Cells[4].Value != DBNull.Value
////                    ? Convert.ToInt32(currentRow.Cells[4].Value)
////                    : 0;

////                isRecordSelected = true;

////                Form parentForm = this.FindForm();
////                parentForm?.Close();
////            }
////        }

////        private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
////        {
////            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
////            {
////                SelectCurrentRowAndClose();
////            }
////        }

////        private void OrderListDataGrid_KeyDown(object sender, KeyEventArgs e)
////        {
////            if (e.KeyCode == Keys.Up)
////            {
////                if (OrderListDataGrid.CurrentRow != null &&
////                    OrderListDataGrid.CurrentRow.Index == 0)
////                {
////                    SearchOrderTxt.Focus();
////                    SearchOrderTxt.SelectAll();
////                    e.Handled = true;
////                    return;
////                }
////            }
////            else if (e.KeyCode == Keys.Escape && OrderListDataGrid.Visible)
////            {
////                SearchOrderTxt.Focus();
////                e.Handled = true;
////                return;
////            }
////            else if (e.KeyCode == Keys.Enter)
////            {
////                e.Handled = true;
////                e.SuppressKeyPress = true;
////                SelectCurrentRowAndClose();
////            }
////        }

////        private void SearchOrderTxt_KeyDown(object sender, KeyEventArgs e)
////        {
////            if (e.KeyCode == Keys.Down && OrderListDataGrid.Visible)
////            {
////                if (OrderListDataGrid.Rows.Count > 0)
////                {
////                    OrderListDataGrid.Focus();
////                    OrderListDataGrid.Rows[0].Selected = true;
////                    e.Handled = true;
////                }
////            }
////        }
////    }
////}




//using DocumentFormat.OpenXml.Spreadsheet;
//using POS_Shop.Helpers;
//using POS_Shop.Models;
//using POS_Shop.Repositories;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Color = System.Drawing.Color;

//namespace POS_Shop.Views.Controllers.Order
//{
//    public partial class TempOrderControl : UserControl
//    {


//        private int PageSize = 100;
//        private int PageIndex = 1;
//        private int RecordCount = 0;
//        private string SearchTerm = "";
//        public bool isRecordSelected = false;
//        public string CustomerName { get; set; } = string.Empty;
//        public int CustomerId { get; set; } = 0;
//        public float ReceivedAmount = 0;
//        public float totalAmount = 0;
//        private List<TempOrderDetail> _printOrderDetails = new List<TempOrderDetail>();
//        private TempOrder _printOrder = null;
//        public TempOrderControl()
//        {
//            InitializeComponent();
//            this.InvoiceNoLbl.Text = string.Empty;
//            this.isRecordSelected = false;
//            this.Load += OrdersControlUI_Load;

//            // Make sure control can receive focus
//            this.SetStyle(ControlStyles.Selectable, true);
//            this.TabStop = true;

//            // Click/Enter events to ensure control gets focus
//            this.Enter += (s, e) => this.Focus();
//            this.Click += (s, e) => this.Focus();

//            OrderListDataGrid.KeyDown += OrderListDataGrid_KeyDown;

//        }
//        // Override ProcessCmdKey to intercept keyboard shortcuts
//        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
//        {
//            // Check for Escape
//            if (keyData == (Keys.F4 | Keys.Alt))
//            {
//                Form parentForm = this.FindForm();
//                parentForm?.Close();
//                return true; // Key has been handled
//            }
//            return base.ProcessCmdKey(ref msg, keyData);
//        }
//        private async void OrdersControlUI_Load(object sender, EventArgs e)
//        {


//            LoadingManager.ShowLoading();
//            await LoadOrdersForDataGridView();
//            LoadingManager.HideLoading();
//        }


//        private async Task LoadOrdersForDataGridView()
//        {
//            using (var context = new POSDbContext())
//            {
//                var orderRepository = new OrderRepository(context);
//                //var cities = await cityRepository.GetCitiesListAsync();

//                var result = await orderRepository.GetTempOrderPagingListAsync(PageIndex, PageSize, SearchTerm);
//                RecordCount = result.totalCount;
//                DataTable dt = new DataTable();
//                dt.Columns.Add("Invoice No", typeof(string));
//                dt.Columns.Add("Total Bill", typeof(float));
//                dt.Columns.Add("Received Amt", typeof(float));
//                dt.Columns.Add("Customer", typeof(string));
//                dt.Columns.Add("CustomerId", typeof(int));
//                dt.Columns.Add("Date", typeof(DateTime));

//                foreach (var item in result.data)
//                {
//                    dt.Rows.Add(item.InvoiceNumber, item.TotalBill, item.ReceiveAmount, item.CustomerName, item.customerId, item.CreatedDate);
//                }

//                //CountryDatagridView.AutoGenerateColumns = true;
//                OrderListDataGrid.ReadOnly = true;
//                OrderListDataGrid.AllowUserToAddRows = false;

//                OrderListDataGrid.DataSource = dt;
//                OrderListDataGrid.Columns[0].Width = 150;

//                //// Remove old Action column if it exists, then re-add fresh
//                //if (OrderListDataGrid.Columns.Contains("Action"))
//                //    OrderListDataGrid.Columns.Remove("Action");

//                //DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
//                //deleteBtn.Name = "Action";
//                //deleteBtn.HeaderText = "Action";
//                //deleteBtn.Text = "Delete";

//                //deleteBtn.FlatStyle = FlatStyle.Flat;
//                //deleteBtn.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
//                //deleteBtn.DefaultCellStyle.ForeColor = Color.White;
//                //deleteBtn.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 28, 28);
//                //deleteBtn.DefaultCellStyle.SelectionForeColor = Color.White;
//                //deleteBtn.Width = 80;
//                //deleteBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
//                //OrderListDataGrid.Columns.Add(deleteBtn);


//                // Remove old columns if exist
//                foreach (string colName in new[] { "DeleteAction", "PrintAction", "SaveAction" })
//                    if (OrderListDataGrid.Columns.Contains(colName))
//                        OrderListDataGrid.Columns.Remove(colName);

//                DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
//                deleteBtn.Name = "DeleteAction";
//                deleteBtn.HeaderText = "Delete";
//                deleteBtn.Text = "Delete";
//                deleteBtn.UseColumnTextForButtonValue = true;
//                deleteBtn.FlatStyle = FlatStyle.Flat;
//                deleteBtn.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
//                deleteBtn.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
//                deleteBtn.DefaultCellStyle.SelectionBackColor = Color.FromArgb(185, 28, 28);
//                deleteBtn.DefaultCellStyle.SelectionForeColor = Color.White;
//                deleteBtn.Width = 65;
//                deleteBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
//                OrderListDataGrid.Columns.Add(deleteBtn);

//                DataGridViewButtonColumn printBtn = new DataGridViewButtonColumn();
//                printBtn.Name = "PrintAction";
//                printBtn.HeaderText = "Print";
//                printBtn.Text = "Print";
//                printBtn.UseColumnTextForButtonValue = true;
//                printBtn.FlatStyle = FlatStyle.Flat;
//                printBtn.DefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
//                printBtn.DefaultCellStyle.ForeColor = Color.White;
//                printBtn.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 86, 179);
//                printBtn.DefaultCellStyle.SelectionForeColor = Color.White;
//                printBtn.Width = 55;
//                printBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
//                OrderListDataGrid.Columns.Add(printBtn);

//                //DataGridViewButtonColumn saveBtn = new DataGridViewButtonColumn();
//                //saveBtn.Name = "SaveAction";
//                //saveBtn.HeaderText = "Save DB";
//                //saveBtn.Text = "Save DB";
//                //saveBtn.UseColumnTextForButtonValue = true;
//                //saveBtn.FlatStyle = FlatStyle.Flat;
//                //saveBtn.DefaultCellStyle.BackColor = Color.FromArgb(40, 167, 69);
//                //saveBtn.DefaultCellStyle.ForeColor = Color.White;
//                //saveBtn.DefaultCellStyle.SelectionBackColor = Color.FromArgb(30, 126, 52);
//                //saveBtn.DefaultCellStyle.SelectionForeColor = Color.White;
//                //saveBtn.Width = 65;
//                //saveBtn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
//                //OrderListDataGrid.Columns.Add(saveBtn);

//                // Make all data columns read-only
//                foreach (DataGridViewColumn col in OrderListDataGrid.Columns)
//                {
//                    if (col.Name != "Action")
//                        col.ReadOnly = true;
//                }

//                // Wire up the event in constructor:
//                OrderListDataGrid.CellContentClick += OrderListDataGrid_CellContentClick;
//                UpdatePager();
//            }
//        }
//        private void UpdatePager()
//        {
//            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
//            //  lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

//            PreviousPageBtn.Enabled = PageIndex > 1;
//            NextPageBtn.Enabled = PageIndex < totalPages;
//        }

//        // The handler:
//        private bool _isDeleting = false; // add this field at the top of the class

//        //private async void OrderListDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        //{
//        //    if (e.RowIndex < 0) return;
//        //    if (OrderListDataGrid.Columns[e.ColumnIndex].Name != "Action") return;
//        //    if (_isDeleting) return; // prevent re-entry

//        //    string invoiceNo = OrderListDataGrid.Rows[e.RowIndex].Cells[0].Value?.ToString();
//        //    if (string.IsNullOrEmpty(invoiceNo)) return;

//        //    var confirm = MessageBox.Show(
//        //        $"Delete order {invoiceNo}?",
//        //        "Confirm Delete",
//        //        MessageBoxButtons.YesNo,
//        //        MessageBoxIcon.Warning);

//        //    if (confirm != DialogResult.Yes) return;

//        //    _isDeleting = true;
//        //    OrderListDataGrid.Enabled = false; // block any further clicks

//        //    try
//        //    {
//        //        using (var context = new POSDbContext())
//        //        {
//        //            var orderRepository = new OrderRepository(context);
//        //            bool deleted = await orderRepository.DeleteTempOrderAsync(invoiceNo);
//        //            if (deleted)
//        //            {
//        //                MessageBox.Show("Order deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
//        //                await LoadOrdersForDataGridView();
//        //            }
//        //            else
//        //            {
//        //                MessageBox.Show("Failed to delete order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
//        //            }
//        //        }
//        //    }
//        //    finally
//        //    {
//        //        _isDeleting = false;
//        //        OrderListDataGrid.Enabled = true; // restore clicks
//        //    }
//        //}

//        private async void OrderListDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (e.RowIndex < 0) return;

//            string colName = OrderListDataGrid.Columns[e.ColumnIndex].Name;
//            string invoiceNo = OrderListDataGrid.Rows[e.RowIndex].Cells[0].Value?.ToString();
//            totalAmount = float.Parse(OrderListDataGrid.Rows[e.RowIndex].Cells["Total Bill"].Value?.ToString());
//            ReceivedAmount = float.Parse(OrderListDataGrid.Rows[e.RowIndex].Cells["Received Amt"].Value?.ToString());
//            CustomerName = OrderListDataGrid.Rows[e.RowIndex].Cells["Customer"].Value?.ToString();


//            if (string.IsNullOrEmpty(invoiceNo)) return;

//            if (colName == "DeleteAction")
//            {
//                if (_isDeleting) return;

//                var confirm = MessageBox.Show(
//                    $"Delete order {invoiceNo}?",
//                    "Confirm Delete",
//                    MessageBoxButtons.YesNo,
//                    MessageBoxIcon.Warning);

//                if (confirm != DialogResult.Yes) return;

//                _isDeleting = true;
//                OrderListDataGrid.Enabled = false;

//                try
//                {
//                    using (var context = new POSDbContext())
//                    {
//                        var orderRepository = new OrderRepository(context);
//                        bool deleted = await orderRepository.DeleteTempOrderAsync(invoiceNo);
//                        if (deleted)
//                        {
//                            MessageBox.Show("Order deleted successfully.", "Success",
//                                MessageBoxButtons.OK, MessageBoxIcon.Information);
//                            await LoadOrdersForDataGridView();
//                        }
//                        else
//                        {
//                            MessageBox.Show("Failed to delete order.", "Error",
//                                MessageBoxButtons.OK, MessageBoxIcon.Error);
//                        }
//                    }
//                }
//                finally
//                {
//                    _isDeleting = false;
//                    OrderListDataGrid.Enabled = true;
//                }
//            }
//            else if (colName == "PrintAction")
//            {

//                using(var context = new POSDbContext())
//                {
//                    var orderRepo = new OrderRepository(context);
//                    _printOrderDetails = orderRepo.GetTempOrderDetailByInvoice(invoiceNo);
//                }
//                var pd = new System.Drawing.Printing.PrintDocument();
//                pd.PrintPage += TempOrder_PrintPage;

//                using (var dlg = new PrintPreviewDialog
//                {
//                    Document = pd,
//                    Width = 920,
//                    Height = 720,
//                    StartPosition = FormStartPosition.CenterParent,

//                })
//                {
//                    dlg.ShowDialog(this.FindForm());
//                }
//            }
//            //else if (colName == "SaveAction")
//            //{
//            //    var confirm = MessageBox.Show(
//            //        $"Save order {invoiceNo} to permanent DB?",
//            //        "Confirm Save",
//            //        MessageBoxButtons.YesNo,
//            //        MessageBoxIcon.Question);

//            //    if (confirm != DialogResult.Yes) return;

//            //    // your save logic here
//            //    MessageBox.Show($"Save: {invoiceNo}"); // replace with actual save call
//            //}
//        }

//        private void TempOrder_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
//        {
//            // 1️⃣  Dynamic height calculation
//            int baseHeight = 350; // header, totals, footer space
//            int itemHeight = 30;  // each item (2 rows: name + details)
//            int totalHeight = baseHeight + (_printOrderDetails.Count * itemHeight);

//            // Safety cap
//            if (totalHeight < 400) totalHeight = 400;

//            // Set the custom paper size dynamically
//            PaperSize customSize = new PaperSize("Custom", 280, totalHeight);
//            e.PageSettings.PaperSize = customSize;

//            // 2️⃣ Now your existing print logic continues

//            int paperWidth = 280;
//            int leftMargin = 0;
//            int currentY = 5;
//            int lineHeight = 12;
//            int sectionSpacing = 3;

//            System.Drawing.Font titleFont = new System.Drawing.Font("Arial", 11, FontStyle.Bold);
//            System.Drawing.Font headerFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
//            System.Drawing.Font regularFont = new System.Drawing.Font("Arial", 9, FontStyle.Regular);
//            System.Drawing.Font smallFont = new System.Drawing.Font("Arial", 7, FontStyle.Regular);
//            System.Drawing.Font urduFont = new System.Drawing.Font("Nafees Web Naskh", 9, FontStyle.Regular);

//            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
//            StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near };
//            StringFormat leftFormat = new StringFormat { Alignment = StringAlignment.Near };

//            string dashLine = new string('-', 82);


//            e.Graphics.DrawString("انوائس", headerFont, Brushes.Black,
//                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
//            currentY += lineHeight + 2;

//            //string cName= !string.IsNullOrEmpty(customerName) ? customerName.Split('-')[1].Trim() : "";
//            e.Graphics.DrawString($"کسٹمر: {CustomerName}", headerFont, Brushes.Black,
//                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
//            currentY += lineHeight + 2;

//            e.Graphics.DrawString("تاریخ: " + DateTime.Now.ToString("yyyy-MM-dd"), urduFont, Brushes.Black,
//                              new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);

//            e.Graphics.DrawString($"کل اشیاء :" + _printOrderDetails.Count, urduFont, Brushes.Black,
//                                 new Rectangle(190, currentY, paperWidth, lineHeight + 2), rightFormat);
//            currentY += lineHeight + 2;


//            e.Graphics.DrawString($"انوائس :" + InvoiceNoLbl.Text, urduFont, Brushes.Black,
//                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);

//            currentY += lineHeight + 2;

//            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
//            currentY += lineHeight;

//            // HEADERS with Gray Background
//            int headerStartY = currentY;
//            int headerHeight = lineHeight + 3;

//            // Draw gray background for headers
//            using (Brush grayBrush = new SolidBrush(Color.Black))
//            {
//                e.Graphics.FillRectangle(grayBrush, leftMargin, headerStartY, paperWidth, headerHeight);
//            }

//            // Draw header text on top of gray background
//            e.Graphics.DrawString("قیمت", headerFont, Brushes.White,
//                                 new Rectangle(0, currentY, 60, lineHeight), rightFormat);
//            e.Graphics.DrawString("ریٹ", headerFont, Brushes.White,
//                                 new Rectangle(65, currentY, 50, lineHeight), rightFormat);
//            e.Graphics.DrawString("تعداد", headerFont, Brushes.White,
//                                 new Rectangle(120, currentY, 100, lineHeight), rightFormat);
//            e.Graphics.DrawString("پروڈکٹ", headerFont, Brushes.White,
//                                 new Rectangle(225, currentY, 50, lineHeight), rightFormat);
//            currentY += lineHeight + 3;

//            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
//            currentY += 5;

//            // TABLE ROWS (without background)
//            foreach (var row in _printOrderDetails)
//            {
//                decimal amount =Convert.ToDecimal(row.Price * row.Quantity);
//                decimal salePrice =Convert.ToDecimal(row.Price);
//                decimal qty = row.Quantity;
//                string productType = row.QuantityType;
//                string productName = row.ProductName;

//                string formattedProduct = TextFormatHelper.FormatMixedText(productName);

//                // Product Name
//                e.Graphics.DrawString(formattedProduct, regularFont, Brushes.Black,
//                                     new Rectangle(leftMargin, currentY, paperWidth - 5, lineHeight + 2),
//                                     new StringFormat { Alignment = StringAlignment.Far });
//                int detailsY = currentY + lineHeight;

//                // Row Details
//                e.Graphics.DrawString($"{amount:0}", regularFont, Brushes.Black,
//                                     new Rectangle(0, detailsY, 60, lineHeight), rightFormat);
//                e.Graphics.DrawString($"{salePrice:0}", regularFont, Brushes.Black,
//                                     new Rectangle(65, detailsY, 50, lineHeight), rightFormat);

//                string formattedQtyValue = TextFormatHelper.FormatMixedText($"{productType} {qty:0}");
//                e.Graphics.DrawString($"{formattedQtyValue}", urduFont, Brushes.Black,
//                                     new Rectangle(120, detailsY, 100, lineHeight), rightFormat);

//                currentY = detailsY + lineHeight;
//                e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, paperWidth, currentY);
//                currentY += 4;
//            }

//            // TOTALS
//            currentY += sectionSpacing;
//            decimal subtotal =Convert.ToDecimal(_printOrderDetails.Sum(x => x.Quantity * x.Price));
//            decimal total = subtotal;

//            e.Graphics.DrawString($"کل رقم: {total:0}", urduFont, Brushes.Black,
//                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
//            currentY += lineHeight + 4;


//            decimal tendered =decimal.Parse(ReceivedAmount.ToString());
//            decimal change = tendered - total;

//            e.Graphics.DrawString($"وصول رقم: {tendered:0}", headerFont, Brushes.Black,
//                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
//            currentY += lineHeight + 4;

//            e.Graphics.DrawString($"بقایا: {change:0}", urduFont, Brushes.Black,
//                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
//            currentY += lineHeight + 4;

//            e.Graphics.DrawString("ٹوٹے ہوۓ سامان کی واپسی نہیں۔", headerFont, Brushes.Black,
//                       new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);
//            currentY += lineHeight + 4;
//            e.Graphics.DrawString("چائنہ مال کی وارنٹی نہیں۔", headerFont, Brushes.Black,
//                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight + 2), rightFormat);

//        }


//        private async void SearchOrderTxt_TextChange(object sender, EventArgs e)
//        {
//            PageIndex = 1;
//            SearchTerm = SearchOrderTxt.Text.Trim();
//            await LoadOrdersForDataGridView();
//        }

//        private async void NextPageBtn_Click_1(object sender, EventArgs e)
//        {
//            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
//            if (PageIndex < totalPages)
//            {
//                PageIndex++;
//                await LoadOrdersForDataGridView();
//            }
//        }

//        private async void PreviousPageBtn_Click_1(object sender, EventArgs e)
//        {
//            if (PageIndex > 1)
//            {
//                PageIndex--;
//                await LoadOrdersForDataGridView();
//            }
//        }

//        //private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
//        //{
//        //    if (OrderListDataGrid.Rows.Count > 0)
//        //    {
//        //        InvoiceNoLbl.Text = (string)OrderListDataGrid.CurrentRow.Cells[0].Value;
//        //        CustomerName = OrderListDataGrid.CurrentRow.Cells[2].Value != DBNull.Value ? (string)OrderListDataGrid.CurrentRow.Cells[2].Value : string.Empty;
//        //        CustomerId = OrderListDataGrid.CurrentRow.Cells[3].Value != DBNull.Value ? Convert.ToInt32(OrderListDataGrid.CurrentRow.Cells[3].Value) : 0;

//        //        isRecordSelected = true;
//        //        // Close the parent form
//        //        Form parentForm = this.FindForm();
//        //        parentForm?.Close();

//        //    }
//        //}


//        private void SelectCurrentRowAndClose()
//        {
//            if (OrderListDataGrid.Rows.Count > 0 &&
//                OrderListDataGrid.CurrentRow != null &&
//                OrderListDataGrid.CurrentRow.Index >= 0)
//            {
//                DataGridViewRow currentRow = OrderListDataGrid.CurrentRow;

//                if (currentRow.Cells[0].Value != null)
//                {
//                    InvoiceNoLbl.Text = currentRow.Cells[0].Value.ToString();
//                }
//                ReceivedAmount = float.Parse(currentRow.Cells[2].Value.ToString());
//                CustomerName = currentRow.Cells[3].Value != DBNull.Value
//                    ? (string)currentRow.Cells[3].Value
//                    : string.Empty;

//                CustomerId = currentRow.Cells[4].Value != DBNull.Value
//                    ? Convert.ToInt32(currentRow.Cells[4].Value)
//                    : 0;

//                isRecordSelected = true;

//                Form parentForm = this.FindForm();
//                parentForm?.Close();
//            }
//        }

//        //private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
//        //{
//        //    if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

//        //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
//        //    {
//        //        if (OrderListDataGrid.Columns[e.ColumnIndex].Name != "Action")
//        //        {
//        //            SelectCurrentRowAndClose(); // ← This should be fine...
//        //        }

//        //    }
//        //}

//        private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
//        {
//            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

//            string colName = OrderListDataGrid.Columns[e.ColumnIndex].Name;
//            if (colName != "DeleteAction" && colName != "PrintAction" && colName != "SaveAction")
//            {
//                SelectCurrentRowAndClose();
//            }
//        }

//        private void OrderListDataGrid_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Up)
//            {
//                if (OrderListDataGrid.CurrentRow != null &&
//                    OrderListDataGrid.CurrentRow.Index == 0)
//                {
//                    SearchOrderTxt.Focus();
//                    SearchOrderTxt.SelectAll();
//                    e.Handled = true;
//                    return;
//                }
//            }
//            else if (e.KeyCode == Keys.Escape && OrderListDataGrid.Visible)
//            {
//                SearchOrderTxt.Focus();
//                e.Handled = true;
//                return;
//            }
//            else if (e.KeyCode == Keys.Enter)
//            {
//                e.Handled = true;
//                e.SuppressKeyPress = true;
//                SelectCurrentRowAndClose();
//            }
//        }

//        private void SearchOrderTxt_KeyDown(object sender, KeyEventArgs e)
//        {
//            if (e.KeyCode == Keys.Down && OrderListDataGrid.Visible)
//            {
//                if (OrderListDataGrid.Rows.Count > 0)
//                {
//                    OrderListDataGrid.Focus();
//                    OrderListDataGrid.Rows[0].Selected = true;
//                    e.Handled = true;
//                }
//            }
//        }
//    }
//}



using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Order
{
    public partial class TempOrderControl : UserControl
    {
        // ── Paging state ──────────────────────────────────────────────────────
        private int _pageSize = 100;
        private int _pageIndex = 1;
        private int _recordCount;
        private string _searchTerm = string.Empty;

        // ── Guard flags ───────────────────────────────────────────────────────
        private bool _isDeleting;

        // ── Selected record (exposed to parent form) ──────────────────────────
        public bool IsRecordSelected { get; private set; }
        public string CustomerName { get; private set; } = string.Empty;
        public int CustomerId { get; private set; }
        public decimal ReceivedAmount { get; private set; }
        public decimal TotalAmount { get; private set; }

        // ── Label exposed for invoice number ─────────────────────────────────
        // (kept so parent forms that read InvoiceNoLbl.Text still work)

        // ── Constructor ───────────────────────────────────────────────────────
        public TempOrderControl()
        {
            InitializeComponent();

            InvoiceNoLbl.Text = string.Empty;

            // Focus plumbing
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
            this.Enter += (s, e) => this.Focus();
            this.Click += (s, e) => this.Focus();

            // Wire events ONCE here — not inside every data-load call
            OrderListDataGrid.KeyDown += OrderListDataGrid_KeyDown;
            OrderListDataGrid.CellContentClick += OrderListDataGrid_CellContentClick;
            OrderListDataGrid.CellMouseClick += OrderListDataGrid_CellMouseClick;

            // Add action columns ONCE — data loads only bind the data source
            TempOrderGridHelper.AddActionColumns(OrderListDataGrid);

            this.Load += async (s, e) =>
            {
                LoadingManager.ShowLoading();
                await LoadDataAsync();
                LoadingManager.HideLoading();
            };
        }

        // ── Alt+F4 passthrough ────────────────────────────────────────────────
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.F4 | Keys.Alt))
            {
                this.FindForm()?.Close();
                return true;
            }

            // Check for Ctrl+P for printing
            if (keyData == (Keys.Control | Keys.P))
            {
                // Check if DataGridView has focus and a row is selected
                if (OrderListDataGrid.Focused && OrderListDataGrid.CurrentRow != null)
                {
                    string invoiceNo = OrderListDataGrid.CurrentRow.Cells[TempOrderGridHelper.ColInvoiceNo].Value?.ToString();
                    if (!string.IsNullOrEmpty(invoiceNo))
                    {
                        ReadRowValues(OrderListDataGrid.CurrentRow);
                        _ = HandlePrintAsync(invoiceNo);
                    }
                    return true;  // Handled
                }
            }
            // Check for Ctrl+D for deleting
            if (keyData == (Keys.Control | Keys.D))
            {
                // Check if DataGridView has focus and a row is selected
                if (OrderListDataGrid.Focused && OrderListDataGrid.CurrentRow != null)
                {
                    string invoiceNo = OrderListDataGrid.CurrentRow.Cells[TempOrderGridHelper.ColInvoiceNo].Value?.ToString();
                    if (!string.IsNullOrEmpty(invoiceNo))
                    {
                        ReadRowValues(OrderListDataGrid.CurrentRow);
                        _ = HandleDeleteAsync(invoiceNo);
                    }
                    return true;  // Handled
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ── Data loading ──────────────────────────────────────────────────────

        private async Task LoadDataAsync()
        {
            var dt = await FetchOrderDataTableAsync();

            // Only rebind data — columns were set up once in the constructor
            OrderListDataGrid.ReadOnly = true;
            OrderListDataGrid.AllowUserToAddRows = false;
            OrderListDataGrid.DataSource = dt;

            // Freeze Invoice No column width after bind
            if (OrderListDataGrid.Columns.Contains(TempOrderGridHelper.ColInvoiceNo))
                OrderListDataGrid.Columns[TempOrderGridHelper.ColInvoiceNo].Width = 150;

            UpdatePager();
        }

        private async Task<DataTable> FetchOrderDataTableAsync()
        {
            using (var context = new POSDbContext())
            {
                var repo = new OrderRepository(context);
                var result = await repo.GetTempOrderPagingListAsync(_pageIndex, _pageSize, _searchTerm);

                _recordCount = result.totalCount;

                var dt = new DataTable();
                dt.Columns.Add(TempOrderGridHelper.ColInvoiceNo, typeof(string));
                dt.Columns.Add(TempOrderGridHelper.ColTotalBill, typeof(decimal));
                dt.Columns.Add(TempOrderGridHelper.ColReceivedAmt, typeof(decimal));
                dt.Columns.Add(TempOrderGridHelper.ColCustomer, typeof(string));
                dt.Columns.Add(TempOrderGridHelper.ColCustomerId, typeof(int));
                dt.Columns.Add(TempOrderGridHelper.ColDate, typeof(DateTime));

                foreach (var item in result.data)
                {
                    dt.Rows.Add(
                        item.InvoiceNumber,
                        (decimal)item.TotalBill,
                        (decimal)item.ReceiveAmount,
                        item.CustomerName,
                        item.customerId,
                        item.CreatedDate);
                }

                return dt;
            }
        }

        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)_recordCount / _pageSize);
            PreviousPageBtn.Enabled = _pageIndex > 1;
            NextPageBtn.Enabled = _pageIndex < totalPages;
        }

        // ── Cell click handlers ───────────────────────────────────────────────

        /// <summary>
        /// Handles button column clicks (Delete / Print).
        /// </summary>
        private async void OrderListDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = OrderListDataGrid.Columns[e.ColumnIndex].Name;
            var row = OrderListDataGrid.Rows[e.RowIndex];
            string invoiceNo = row.Cells[TempOrderGridHelper.ColInvoiceNo].Value?.ToString();

            if (string.IsNullOrEmpty(invoiceNo)) return;

            // Cache values before any async gap
            ReadRowValues(row);

            switch (colName)
            {
                case TempOrderGridHelper.ColDeleteAction:

                    var confirm = MessageBox.Show(
                        $"You are about to permanently delete this record \n" +
                        "This action cannot be undone. are you sure?",
                        "confirm delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);   // no is default

                    if (confirm == DialogResult.Yes)
                    {
                        await HandleDeleteAsync(invoiceNo);
                    }
                    break;

                case TempOrderGridHelper.ColPrintAction:
                    await HandlePrintAsync(invoiceNo);
                    break;
            }
        }

        /// <summary>
        /// Handles data-cell clicks → selects the row and closes the parent form.
        /// </summary>
        private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = OrderListDataGrid.Columns[e.ColumnIndex].Name;
            if (colName != TempOrderGridHelper.ColDeleteAction &&
                colName != TempOrderGridHelper.ColPrintAction)
            {
                SelectCurrentRowAndClose();
            }
        }

        // ── Action handlers ───────────────────────────────────────────────────

        private async Task HandleDeleteAsync(string invoiceNo)
        {
            if (_isDeleting) return;

            var confirm = MessageBox.Show(
                $"Final Confirmation.... You are about to delete order {invoiceNo}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            _isDeleting = true;
            OrderListDataGrid.Enabled = false;

            try
            {
                using (var context = new POSDbContext())
                {
                    var repo = new OrderRepository(context);
                    bool deleted = await repo.DeleteTempOrderAsync(invoiceNo);

                    if (deleted)
                    {
                        MessageBox.Show("Order deleted successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete order.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            finally
            {
                _isDeleting = false;
                OrderListDataGrid.Enabled = true;
            }
        }

        private async Task HandlePrintAsync(string invoiceNo)
        {
            var details = await Task.Run(() =>
            {
                using (var context = new POSDbContext())
                {
                    var repo = new OrderRepository(context);
                    return repo.GetTempOrderDetailByInvoice(invoiceNo);
                }
            });

            // All data is passed directly to the helper — no shared mutable state
            var printer = new TempOrderPrintHelper(
                details: details,
                invoiceNo: invoiceNo,
                customerName: CustomerName,
                receivedAmount: ReceivedAmount);

            using (var pd = printer.CreatePrintDocument())
            {
                pd.DefaultPageSettings.PaperSize = new PaperSize("FullInvoice", 280, 32767);
                pd.Print();
            }
            //using (var pd = printer.CreatePrintDocument())
            //using (var dlg = new PrintPreviewDialog
            //{
            //    Document = pd,
            //    Width = 920,
            //    Height = 720,
            //    StartPosition = FormStartPosition.CenterParent,


            //})
            //{
            //    dlg.PrintPreviewControl.Zoom = 1.0;
            //    pd.DefaultPageSettings.PaperSize = new PaperSize("FullInvoice", 280, 32767);
            //    pd.Print();
            //    //dlg.ShowDialog(this.FindForm());
            //}
        }

        // ── Row selection ─────────────────────────────────────────────────────

        private void SelectCurrentRowAndClose()
        {
            var currentRow = OrderListDataGrid.CurrentRow;
            if (currentRow == null || currentRow.Index < 0) return;

            ReadRowValues(currentRow);
            InvoiceNoLbl.Text = currentRow.Cells[TempOrderGridHelper.ColInvoiceNo].Value?.ToString() ?? string.Empty;
            IsRecordSelected = true;

            this.FindForm()?.Close();
        }

        /// <summary>
        /// Reads the common fields from a row into the exposed properties.
        /// Single place to update if columns ever change.
        /// </summary>
        private void ReadRowValues(DataGridViewRow row)
        {
            CustomerName = row.Cells[TempOrderGridHelper.ColCustomer].Value as string ?? string.Empty;
            CustomerId = row.Cells[TempOrderGridHelper.ColCustomerId].Value is int id ? id : 0;
            ReceivedAmount = row.Cells[TempOrderGridHelper.ColReceivedAmt].Value is decimal ra ? ra : 0;
            TotalAmount = row.Cells[TempOrderGridHelper.ColTotalBill].Value is decimal tb ? tb : 0;
        }

        // ── Search & paging ───────────────────────────────────────────────────

        private async void SearchOrderTxt_TextChange(object sender, EventArgs e)
        {
            _pageIndex = 1;
            _searchTerm = SearchOrderTxt.Text.Trim();
            await LoadDataAsync();
        }

        private async void NextPageBtn_Click_1(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)_recordCount / _pageSize);
            if (_pageIndex < totalPages)
            {
                _pageIndex++;
                await LoadDataAsync();
            }
        }

        private async void PreviousPageBtn_Click_1(object sender, EventArgs e)
        {
            if (_pageIndex > 1)
            {
                _pageIndex--;
                await LoadDataAsync();
            }
        }

        // ── Keyboard navigation ───────────────────────────────────────────────

        private void OrderListDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up when OrderListDataGrid.CurrentRow?.Index == 0:
                    SearchOrderTxt.Focus();
                    SearchOrderTxt.SelectAll();
                    e.Handled = true;
                    break;

                case Keys.Escape:
                    SearchOrderTxt.Focus();
                    e.Handled = true;
                    break;

                case Keys.Enter:
                    e.Handled = e.SuppressKeyPress = true;
                    SelectCurrentRowAndClose();
                    break;
            }
        }

        private void SearchOrderTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && OrderListDataGrid.Rows.Count > 0)
            {
                OrderListDataGrid.Focus();
                OrderListDataGrid.Rows[0].Selected = true;
                e.Handled = true;
            }
        }
    }
}
