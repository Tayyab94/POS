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
            this.Load += OrdersControlUI_Load;
            this.isRecordSelected = false;
        }

        private async void OrdersControlUI_Load(object sender, EventArgs e)
        {

            OrderIDLbl.Text = string.Empty;
            InvoiceNoLbl.Text = DateTime.Now.ToString("ddMMyy-HHmmss");

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
                dt.Columns.Add("Total Bill", typeof(float));
                dt.Columns.Add("Received Amt", typeof(float));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Customer", typeof(string));
                dt.Columns.Add("CustomerId", typeof(int));
                dt.Columns.Add("Date", typeof(DateTime));

                foreach (var item in result.data)
                {
                    dt.Rows.Add(item.Id, item.InvoiceNumber, item.TotalBill, item.ReceiveAmount, item.paymentType, item.CustomerName, item.customerId, item.CreatedDate);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                OrderListDataGrid.ReadOnly = true;
                OrderListDataGrid.AllowUserToAddRows = false;

                OrderListDataGrid.DataSource = dt;
                OrderListDataGrid.Columns[2].Width = 75;
                OrderListDataGrid.Columns[3].Width = 75;
                OrderListDataGrid.Columns[4].Width = 35;
                OrderListDataGrid.Columns[5].Width = 160;
                OrderListDataGrid.Columns[7].Width = 70;

                OrderListDataGrid.Columns[0].Visible = false;
                OrderListDataGrid.Columns[6].Visible = false;

                UpdatePager();
            }
        }
        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            //  lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

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

        private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (OrderListDataGrid.Rows.Count > 0)
            {
                OrderIDLbl.Text = Convert.ToInt32(OrderListDataGrid.CurrentRow.Cells[0].Value).ToString();
                InvoiceNoLbl.Text = (string)OrderListDataGrid.CurrentRow.Cells[1].Value;

                CustomerId = OrderListDataGrid.CurrentRow.Cells[6].Value != DBNull.Value ? Convert.ToInt32(OrderListDataGrid.CurrentRow.Cells[6].Value) : 0;
                CustomerName = OrderListDataGrid.CurrentRow.Cells[5].Value != DBNull.Value ? (string)OrderListDataGrid.CurrentRow.Cells[5].Value : string.Empty;
                this.ReceiveAmount = OrderListDataGrid.CurrentRow.Cells[3].Value != DBNull.Value ? Convert.ToSingle(OrderListDataGrid.CurrentRow.Cells[3].Value) : 0;
                this.TotalBill = OrderListDataGrid.CurrentRow.Cells[2].Value != DBNull.Value ? Convert.ToSingle(OrderListDataGrid.CurrentRow.Cells[2].Value) : 0;
             // Close the parent form
                Form parentForm = this.FindForm();
                parentForm?.Close();
                isRecordSelected = true;
            }
            else
                isRecordSelected = false;

        }
    }
}
