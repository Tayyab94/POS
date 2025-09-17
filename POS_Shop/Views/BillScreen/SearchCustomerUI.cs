using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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

namespace POS_Shop.Views.BillScreen
{
    public partial class SearchCustomerUI : Form
    {
        private int PageSize = 50;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";

        public SearchCustomerUI()
        {
            InitializeComponent();
            this.Load += SearchCustomerUI_Load;
        }

        private async void SearchCustomerUI_Load(object sender, EventArgs e)
        {
            await LoadCustomersForDataGridView();
        }

        private async Task LoadCustomersForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                ICustomerRepository customerRepository = new CustomerRepository(context);
                var result = await customerRepository.GetCustomerPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                //dt.Columns.Add("Address", typeof(string));
                //dt.Columns.Add("Phone", typeof(string));
                //dt.Columns.Add("CityId", typeof(int));
                //dt.Columns.Add("City Name", typeof(string));
                dt.Columns.Add("Active", typeof(bool));

                foreach (var item in result.data)
                {

                    //dt.Rows.Add(item.Id, item.CustomerName, item.CustomerAddress,
                    //            item.ContactNo, item.CityId, item.CityName, !item.IsDeleted);

                    dt.Rows.Add(item.Id, item.CustomerName,!item.IsDeleted);
                }

                CustomerListDataGrid.ReadOnly = true;
                CustomerListDataGrid.AllowUserToAddRows = false;
                //ProductListGrid.AutoGenerateColumns = false;

                CustomerListDataGrid.DataSource = dt;
                CustomerListDataGrid.Columns[0].Visible = false;

                UpdatePager();
            }
        }


        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PreviousPageBtn.Enabled = PageIndex > 1;
            NextPageBtn.Enabled = PageIndex < totalPages;
        }
        private void CloseBtn_Click(object sender, EventArgs e)
        {
            FormCloseLbl.Text = "true";
            this.Close();
        }

        private async void NextPageBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadCustomersForDataGridView();
            }
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadCustomersForDataGridView();
            }

        }

        private async void SearchCustomerTxt_TextChange(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = SearchCustomerTxt.Text.Trim();
            await LoadCustomersForDataGridView();
        }

        private void CustomerListDataGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (CustomerListDataGrid.Rows.Count > 0)
            {
                HandleEnterPressed(CustomerListDataGrid.CurrentRow.Index);
            }
        }

        private void CustomerListDataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !e.Handled)
            {
                e.Handled = true;

                if (CustomerListDataGrid.CurrentRow != null &&
                    CustomerListDataGrid.CurrentRow.Index >= 0 &&
                    CustomerListDataGrid.CurrentRow.Index != CustomerListDataGrid.NewRowIndex)
                {

                    int currentIndex = CustomerListDataGrid.CurrentRow.Index;
                    int targetIndex = currentIndex; // default to current row

                    if (currentIndex > 0 && currentIndex < CustomerListDataGrid.Rows.Count - 1)
                    {
                        targetIndex = currentIndex - 1; // only go previous if not first/last
                    }
                    HandleEnterPressed(targetIndex);
                    this.Close();
                }
            }
        }

        private void HandleEnterPressed(int rowIndex)
        {
            // Your logic when Enter is pressed on a row
            DataGridViewRow selectedRow = CustomerListDataGrid.Rows[rowIndex];
            CustomerIdLbl.Text = Convert.ToString(selectedRow.Cells[0].Value);
            CustomerName.Text = (string)selectedRow.Cells[1].Value;
            FormCloseLbl.Text = "false";
        }

        private void AddNewCustomerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Create a new instance of your Form
            Form customerForm = new Form();
            customerForm.Text = "Add New Customer";
            customerForm.StartPosition = FormStartPosition.CenterScreen;

            // Create an instance of your User Control
            // Replace 'YourUserControl' with the actual name of your User Control
            var CustomerFormCtrl = new Views.Controllers.Customers.CustomerFormControl();

            CustomerFormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

            // Add the User Control to the new Form's controls collection
            customerForm.Controls.Add(CustomerFormCtrl);

            customerForm.Width = 1050; customerForm.Height = 625;
            // Show the new form
            customerForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
        }
    }
}
