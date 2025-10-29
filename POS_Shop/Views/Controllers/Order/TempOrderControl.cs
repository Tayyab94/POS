using POS_Shop.Helpers;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Order
{
    public partial class TempOrderControl : UserControl
    {
       

        private int PageSize = 100;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";

        public string CustomerName { get; set; } = string.Empty;
        public int CustomerId { get; set; } = 0;
       
        public TempOrderControl()
        {
            InitializeComponent();
            this.InvoiceNoLbl.Text = string.Empty;

            this.Load += OrdersControlUI_Load;

            
        }

        private async void OrdersControlUI_Load(object sender, EventArgs e)
        {


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

                var result = await orderRepository.GetTempOrderPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;
                DataTable dt = new DataTable();
                dt.Columns.Add("Invoice No", typeof(string));
                dt.Columns.Add("Total Bill", typeof(float));
                dt.Columns.Add("Customer", typeof(string));
                dt.Columns.Add("CustomerId", typeof(int));
                dt.Columns.Add("Date", typeof(DateTime));

                foreach (var item in result.data)
                {
                    dt.Rows.Add( item.InvoiceNumber, item.TotalBill, item.CustomerName, item.customerId, item.CreatedDate);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                OrderListDataGrid.ReadOnly = true;
                OrderListDataGrid.AllowUserToAddRows = false;

                OrderListDataGrid.DataSource = dt;
                OrderListDataGrid.Columns[0].Width = 150;

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

       

    

        private async void SearchOrderTxt_TextChange(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = SearchOrderTxt.Text.Trim();
            await LoadOrdersForDataGridView();
        }

        private async void NextPageBtn_Click_1(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadOrdersForDataGridView();
            }
        }

        private async void PreviousPageBtn_Click_1(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadOrdersForDataGridView();
            }
        }

        private void OrderListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (OrderListDataGrid.Rows.Count > 0)
            {
                InvoiceNoLbl.Text = (string)OrderListDataGrid.CurrentRow.Cells[0].Value;
                CustomerName = OrderListDataGrid.CurrentRow.Cells[2].Value != DBNull.Value ? (string)OrderListDataGrid.CurrentRow.Cells[2].Value : string.Empty;
                CustomerId = OrderListDataGrid.CurrentRow.Cells[3].Value != DBNull.Value ? Convert.ToInt32(OrderListDataGrid.CurrentRow.Cells[3].Value) : 0;
                // Close the parent form
                Form parentForm = this.FindForm();
                parentForm?.Close();

            }
        }
    }
}
