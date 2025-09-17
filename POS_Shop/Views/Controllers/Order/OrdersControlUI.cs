using POS_Shop.Helpers;
using POS_Shop.Interfaces;
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
    public partial class OrdersControlUI : UserControl
    {
        private int PageSize = 100;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";


        public OrdersControlUI()
        {
            InitializeComponent();
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

                var result = await orderRepository.GetOrderPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Invoice No", typeof(string));
                dt.Columns.Add("Total Bill", typeof(float));
                dt.Columns.Add("Received Amt", typeof(float));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("Customer", typeof(string));
                dt.Columns.Add("Date", typeof(DateTime));

                foreach (var item in result.data)
                {
                    dt.Rows.Add(item.Id, item.InvoiceNumber, item.TotalBill, item.ReceiveAmount, item.paymentType,item.CustomerName  ,item.CreatedDate);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                OrderListDataGrid.ReadOnly = true;
                OrderListDataGrid.AllowUserToAddRows = false;

                OrderListDataGrid.DataSource = dt;
                OrderListDataGrid.Columns[0].Width = 50;

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

        private async  void PreviousPageBtn_Click(object sender, EventArgs e)
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
    }
}
