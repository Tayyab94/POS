using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Order
{
    public partial class OrdersControlUI : UserControl
    {
        private int PageSize = 100;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";
        public bool isRecordSelected = false;
        public float ReceiveAmount { get; set; }
        public float TotalBill { get; set; }

        public string CustomerName { get; set; } = string.Empty;
        public int CustomerId { get; set; } = 0;

        public OrdersControlUI()
        {
            InitializeComponent();

            // Set row height after binding
            OrderListDataGrid.RowTemplate.Height = 30;
            this.Load += OrdersControlUI_Load;
            this.isRecordSelected = false;


            // Add this line
            OrderListDataGrid.CellClick += OrderListDataGrid_CellClick;
            OrderListDataGrid.KeyDown += OrderListDataGrid_KeyDown;

            SetItemGridView();

            // Make sure control can receive focus
            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;

            // Click/Enter events to ensure control gets focus
            this.Enter += (s, e) => this.Focus();
            this.Click += (s, e) => this.Focus();

        }

        private void SetItemGridView()
        {
            OrderDetailList.ColumnCount = 7;

            OrderDetailList.Columns[0].Name = "Amount";
            OrderDetailList.Columns[1].Name = "SalePrice";
            OrderDetailList.Columns[2].Name = "Urdu Name";
            OrderDetailList.Columns[3].Name = "Type";
            OrderDetailList.Columns[4].Name = "Qty";
            OrderDetailList.Columns[5].Name = "ProductId";
            OrderDetailList.Columns[6].Name = "ProductDetail";

            // Set column widths here
            OrderDetailList.Columns[0].Width = 80;
            OrderDetailList.Columns[1].Width = 60;
            OrderDetailList.Columns[2].Width = 190;
            OrderDetailList.Columns[3].Width = 30;
            OrderDetailList.Columns[4].Width = 50;
            OrderDetailList.Columns[5].Width = 50;

            OrderDetailList.Columns[5].Visible = false;
            OrderDetailList.Columns[6].Visible = false;

            OrderDetailList.Columns["Amount"].ReadOnly = true; // Amount
            OrderDetailList.Columns["Urdu Name"].ReadOnly = true; // Urdu Name
            OrderDetailList.Columns["Type"].ReadOnly = true; // Type

            OrderDetailList.AllowUserToAddRows = false;

        }

        private async void OrdersControlUI_Load(object sender, EventArgs e)
        {

            OrderIDLbl.Text = string.Empty;
            string invRef = TextFormatHelper.GetPrefix(Properties.Settings.Default.UserName);
            InvoiceNoLbl.Text = invRef+DateTime.Now.ToString("ddMMyy-HHmmss");

            LoadingManager.ShowLoading();
            await LoadOrdersForDataGridView();
            LoadingManager.HideLoading();
        }


        private async Task LoadOrdersForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                var orderRepository = new OrderRepository(context);
                //var cities = await cityRepository.GetCitiesListAsync();

                var result = await orderRepository.GetOrderPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Invoice No", typeof(string));
                dt.Columns.Add("Bill", typeof(float));
                dt.Columns.Add("Received", typeof(float));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Customer", typeof(string));
                dt.Columns.Add("CustomerId", typeof(int));
                dt.Columns.Add("Date", typeof(DateTime));

                foreach (var item in result.data)
                {
                    dt.Rows.Add(item.Id, item.InvoiceNumber, item.TotalBill, item.ReceiveAmount, item.paymentType, item.CustomerName, item.customerId,
                        item.CreatedDate);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                OrderListDataGrid.ReadOnly = true;
                OrderListDataGrid.AllowUserToAddRows = false;

                // Clear existing data source first
                OrderListDataGrid.DataSource = null;
                // IMPORTANT: Clear all existing columns
                OrderListDataGrid.Columns.Clear();
                OrderListDataGrid.DataSource = dt;

                // Configure columns AFTER setting data source
                OrderListDataGrid.Columns["ID"].Visible = false;      // Hide ID column by name
                OrderListDataGrid.Columns["CustomerId"].Visible = false;  // Hide CustomerId column

                // Set column widths
                OrderListDataGrid.Columns["Invoice No"].Width = 120;
                OrderListDataGrid.Columns["Bill"].Width = 75;
                OrderListDataGrid.Columns["Received"].Width = 75;
                OrderListDataGrid.Columns["Type"].Width = 35;
                OrderListDataGrid.Columns["Customer"].Width = 160;
                OrderListDataGrid.Columns["Date"].Width = 150;

                // Add button column programmatically (better approach)
                AddButtonColumnToDataGridView();


                UpdatePager();
            }
        }
        private void AddButtonColumnToDataGridView()
        {
            // Remove existing button column if it exists
            if (OrderListDataGrid.Columns.Contains("DetailsColumn"))
            {
                OrderListDataGrid.Columns.Remove("DetailsColumn");
            }

            DataGridViewButtonColumn detailsButtonColumn = new DataGridViewButtonColumn();
            detailsButtonColumn.Name = "DetailsColumn";
            detailsButtonColumn.HeaderText = "Action";
            detailsButtonColumn.Text = "Details";
            detailsButtonColumn.UseColumnTextForButtonValue = true;
            detailsButtonColumn.Width = 40;
            detailsButtonColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           
            // Add the button column
            OrderListDataGrid.Columns.Add(detailsButtonColumn);

            // Adjust column positions - move button column to the end
            detailsButtonColumn.DisplayIndex = OrderListDataGrid.Columns.Count - 1;

            // Subscribe to Mouse Event for cursor change
            OrderListDataGrid.CellMouseEnter += OrderListDataGrid_CellMouseEnter;
            OrderListDataGrid.CellMouseLeave += OrderListDataGrid_CellMouseLeave;
        }

        private void OrderListDataGrid_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 &&
                OrderListDataGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                OrderListDataGrid.Cursor = Cursors.Hand;
            }
        }

        private void OrderListDataGrid_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            OrderListDataGrid.Cursor = Cursors.Default;
        }

        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
             lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PreviousPageBtn.Enabled = PageIndex > 1;
            NextPageBtn.Enabled = PageIndex < totalPages;
        }

        private async void NextPageBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadOrdersForDataGridView();
            }
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadOrdersForDataGridView();
            }
        }

        private async void SearchOrderTxt_TextChange(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = SearchOrderTxt.Text.Trim();
            await LoadOrdersForDataGridView();
        }


        private async Task HandleRowAction(int rowIndex, bool isDetailsColumn = false)
        {
            if (isDetailsColumn)
            {
                if (OrderListDataGrid.Rows[rowIndex].Cells[0].Value != null)
                {
                    int orderId = Convert.ToInt32(OrderListDataGrid.Rows[rowIndex].Cells[0].Value);
                    string invoiceNo = OrderListDataGrid.Rows[rowIndex].Cells[1].Value.ToString();
                    await ShowOrderDetails(orderId, invoiceNo);
                    InvNumbnerLbl.Text = invoiceNo;
                }
            }
            else
            {
                SelectRow(rowIndex);
            }
        }

        // Updated CellClick event
        //private async void OrderListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
        //    {
        //        bool isDetailsColumn = OrderListDataGrid.Columns[e.ColumnIndex].Name == "DetailsColumn";
        //        await HandleRowAction(e.RowIndex, isDetailsColumn);
        //    }
        //}

        // Updated KeyDown event
        private async void OrderListDataGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                if (OrderListDataGrid.CurrentRow != null &&
                    OrderListDataGrid.CurrentRow.Index == 0)
                {
                    SearchOrderTxt.Focus();
                    SearchOrderTxt.SelectAll();
                    e.Handled = true;
                    return;
                }
            }
            else if (e.KeyCode == Keys.Escape && OrderListDataGrid.Visible)
            {
                SearchOrderTxt.Focus();
                e.Handled = true;
                return;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (OrderListDataGrid.CurrentRow != null && OrderListDataGrid.CurrentRow.Index >= 0)
                {
                    int rowIndex = OrderListDataGrid.CurrentRow.Index;
                    bool isDetailsColumn = false;

                    // Check if current cell is in Details column
                    if (OrderListDataGrid.CurrentCell != null)
                    {
                        isDetailsColumn = OrderListDataGrid.CurrentCell.OwningColumn.Name == "DetailsColumn";
                    }

                    await HandleRowAction(rowIndex, isDetailsColumn);
                }
            }
        }


        private async void OrderListDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on the button column and not on header
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {

                if (OrderListDataGrid.Columns[e.ColumnIndex].Name == "DetailsColumn")
                {

                    // Get the Order ID from the row
                    if (OrderListDataGrid.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        int orderId = Convert.ToInt32(OrderListDataGrid.Rows[e.RowIndex].Cells[0].Value);

                        string invoiceNo =OrderListDataGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                        //OrderIDLbl.Text = Convert.ToInt32(OrderListDataGrid.CurrentRow.Cells[0].Value).ToString();
                        //InvoiceNoLbl.Text = (string)OrderListDataGrid.CurrentRow.Cells[1].Value;
                        await ShowOrderDetails(orderId,invoiceNo);
                        InvNumbnerLbl.Text = invoiceNo;
                    }
                }
                else
                {

                    // Row click - select and close
                    SelectRow(e.RowIndex);
                }
            }
        }

        private void SelectRow(int rowIndex)
        {
            if (OrderListDataGrid.Rows.Count > 0)
            {
                OrderIDLbl.Text = OrderListDataGrid.Rows[rowIndex].Cells[0].Value.ToString();
                InvoiceNoLbl.Text = (string)OrderListDataGrid.Rows[rowIndex].Cells[1].Value;

                CustomerId = OrderListDataGrid.Rows[rowIndex].Cells[6].Value != DBNull.Value
                    ? Convert.ToInt32(OrderListDataGrid.Rows[rowIndex].Cells[6].Value)
                    : 0;
                CustomerName = OrderListDataGrid.Rows[rowIndex].Cells[5].Value != DBNull.Value
                    ? (string)OrderListDataGrid.Rows[rowIndex].Cells[5].Value
                    : string.Empty;
                this.ReceiveAmount = OrderListDataGrid.Rows[rowIndex].Cells[3].Value != DBNull.Value
                    ? Convert.ToSingle(OrderListDataGrid.Rows[rowIndex].Cells[3].Value)
                    : 0;
                this.TotalBill = OrderListDataGrid.Rows[rowIndex].Cells[2].Value != DBNull.Value
                    ? Convert.ToSingle(OrderListDataGrid.Rows[rowIndex].Cells[2].Value)
                    : 0;

                Form parentForm = this.FindForm();
                parentForm?.Close();
                isRecordSelected = true;
            }
        }

        private async Task ShowOrderDetails(int orderId, string invoiceNo)
        {
            try
            {
                LoadingManager.ShowLoading();

                Task.Delay(1200).Wait();
                using (var context = new POSDbContext())
                {
                    var orderRepo = new OrderRepository(context);
                    var result = await orderRepo.GetOrderByIdAsync(orderId,invoiceNo);
                    if (result != null)
                    {

                        OrderDetailList.Rows.Clear();

                        // Safely add order details
                        foreach (var order in result.OrderDetailsList)
                        {
                            string productId = order.ProductId.ToString() ?? "0";
                            string finalName = !string.IsNullOrEmpty(order.ProductDetail) ?
                                $"{order.ProductName} {order.ProductDetail}" : order.ProductName;

                            string productType = order.QuantityType;
                            decimal salePrice = Math.Round(decimal.Parse(order.Price.ToString()), 1);
                            int qty = order.Quantity;
                            decimal amount = salePrice * qty;

                            OrderDetailList.Rows.Add(amount, salePrice, TextFormatHelper.FormatMixedText(finalName) ,
                                                   productType, qty, productId, order.ProductDetail);
                        }
                    }
                }


                LoadingManager.HideLoading();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order details: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchOrderTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && OrderListDataGrid.Visible)
            {
                if (OrderListDataGrid.Rows.Count > 0)
                {
                    OrderListDataGrid.Focus();
                    OrderListDataGrid.Rows[0].Selected = true;
                    e.Handled = true;
                }
            }
        }
    }
}
