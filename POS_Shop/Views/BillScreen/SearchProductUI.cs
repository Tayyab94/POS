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

namespace POS_Shop.Views.BillScreen
{
    public partial class SearchProductUI : Form
    {

        private int PageSize = 50;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";


        public SearchProductUI()
        {
            InitializeComponent();
            this.Load += SearchProductUI_Load;
            
            LoadingManager.HideLoading();


            this.Shown += (s, e) => { SearchProductTxt.Focus(); };

        }

        private async void SearchProductUI_Load(object sender, EventArgs e)
        {
            await LoadProductsForDataGridView();
        }

        private async Task LoadProductsForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                IProductRepository productRepository = new ProductRepository(context);
                var result = await productRepository.GetProductPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("U-Name", typeof(string));
                //dt.Columns.Add("P-Price", typeof(string));
              
                //dt.Columns.Add("C-p", typeof(int));
                dt.Columns.Add("Type", typeof(string));
                dt.Columns.Add("S-P", typeof(string));
             

                foreach (var item in result.data)
                {
                    dt.Rows.Add(item.Id, item.ProductEnglishName, item.ProductUrduName, item.ProductType, item.SalePrice);
                }

                ProductListGrid.ReadOnly = true;
                ProductListGrid.AllowUserToAddRows = false;
                ProductListGrid.DataSource = dt;
              
                //ConfigureDataGridView();
                UpdatePager();
            }
        }

        private void ProductListGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (ProductListGrid.Rows.Count > 0)
            {
                int pId = Convert.ToInt32(ProductListGrid.CurrentRow.Cells[0].Value);
                PNameLbl.Text = (string)ProductListGrid.CurrentRow.Cells[1].Value;
                PUNameLbl.Text = (string)ProductListGrid.CurrentRow.Cells[2].Value;
                //PTypeLbl.Text = (string)ProductListGrid.CurrentRow.Cells[3].Value;
                PTypeLbl.Text = ProductListGrid.CurrentRow.Cells[3].Value == null
                            || ProductListGrid.CurrentRow.Cells[3].Value == DBNull.Value
                            ? string.Empty
                            : ProductListGrid.CurrentRow.Cells[3].Value.ToString();
                //ProdSalePriceLbl.Text = (string)ProductListGrid.CurrentRow.Cells[4].Value;
                ProdSalePriceLbl.Text = ProductListGrid.CurrentRow.Cells[4].Value == null
                    || ProductListGrid.CurrentRow.Cells[4].Value == DBNull.Value
                    ? string.Empty
                    : ProductListGrid.CurrentRow.Cells[4].Value.ToString();
                ProdIdLbl.Text = pId.ToString();
                FormCloseLbl.Text = "false";
                this.Close();
            }
        }

        private void ConfigureDataGridView()
        {
            // Clear existing columns if any
            ProductListGrid.Columns.Clear();

            // Checkbox column bound to DataTable field
            DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn()
            {
                Name = "IsSelected",
                DataPropertyName = "IsSelected",
                HeaderText = "",
                Width = 30,
                ReadOnly = false,
                FlatStyle = FlatStyle.Standard
            };
            ProductListGrid.Columns.Add(chk);

            // Add other columns
            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "ID",
                DataPropertyName = "ID",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Product Name",
                Width = 150,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Type",
                DataPropertyName = "Type",
                HeaderText = "Type",
                Width = 100,
                ReadOnly = true
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Cost",
                DataPropertyName = "Cost",
                HeaderText = "Cost",
                Width = 80,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N0" }
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "SalePrice",
                DataPropertyName = "Sale-Price",
                HeaderText = "Sale Price",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
            });

            ProductListGrid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "PurchasePrice",
                DataPropertyName = "Purchase-Price",
                HeaderText = "Purchase Price",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle() { Format = "N2" }
            });




            // Edit button column
            DataGridViewButtonColumn editColumn = new DataGridViewButtonColumn()
            {
                Name = "Edit",
                HeaderText = "Edit",
                Text = "Edit",
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            ProductListGrid.Columns.Add(editColumn);

            // Delete button column
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn()
            {
                Name = "Delete",
                HeaderText = "Delete",
                Text = "Delete",
                UseColumnTextForButtonValue = true,
                Width = 50
            };
            ProductListGrid.Columns.Add(deleteColumn);



            // Configure grid properties
            ProductListGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            chk.Width = 30;


            editColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            editColumn.Width = 50;
            deleteColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            deleteColumn.Width = 50;


            ProductListGrid.AllowUserToResizeColumns = true;
            ProductListGrid.AllowUserToResizeRows = false;
            ProductListGrid.RowHeadersVisible = false;
            ProductListGrid.BackgroundColor = SystemColors.Window;
            ProductListGrid.BorderStyle = BorderStyle.None;
            ProductListGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            ProductListGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            ProductListGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            ProductListGrid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlDark;
            ProductListGrid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;
            ProductListGrid.EnableHeadersVisualStyles = false;

            // Subscribe to events
            //ProductListGrid.CellValueChanged += ProductListGrid_CellValueChanged;
            //ProductListGrid.CurrentCellDirtyStateChanged += ProductListGrid_CurrentCellDirtyStateChanged;
        }


        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PreviousPageBtn.Enabled = PageIndex > 1;
            NextPageBtn.Enabled = PageIndex < totalPages;

        }
        // Your existing pagination and search methods
        private async void SearchProductTxt_TextChanged(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = SearchProductTxt.Text.Trim();
            await LoadProductsForDataGridView();
        }

        private async void PreviousPageBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadProductsForDataGridView();
            }
        }

        private async void NextPageBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadProductsForDataGridView();
            }
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            FormCloseLbl.Text = "true";
            this.Close();
        }



        private void HandleEnterPressed(int rowIndex)
        {
            
                // Your logic when Enter is pressed on a row
                DataGridViewRow selectedRow = ProductListGrid.Rows[rowIndex];
                 int pId = Convert.ToInt32(selectedRow.Cells[0].Value);
                    PNameLbl.Text = (string)selectedRow.Cells[1].Value;
                    PUNameLbl.Text = (string)selectedRow.Cells[2].Value;
                    //PTypeLbl.Text = (string)selectedRow.Cells[3].Value;
                    PTypeLbl.Text = selectedRow.Cells[3].Value == null
                                || selectedRow.Cells[3].Value == DBNull.Value
                                ? string.Empty
                                : selectedRow.Cells[3].Value.ToString();
                    //ProdSalePriceLbl.Text = (string)selectedRow.Cells[4].Value;
                    ProdSalePriceLbl.Text = selectedRow.Cells[4].Value == null
                        || selectedRow.Cells[4].Value == DBNull.Value
                        ? string.Empty
                        : selectedRow.Cells[4].Value.ToString();
                    ProdIdLbl.Text = pId.ToString();
                    FormCloseLbl.Text = "false";
        }

        private void SearchProductTxt_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Down)
            {
                ProductListGrid.Focus();

                if (ProductListGrid.Rows.Count > 0)
                {
                    ProductListGrid.Rows[0].Selected = true;
                    ProductListGrid.CurrentCell = ProductListGrid.Rows[0].Cells[0];
                }

                e.Handled = true;
            }
        }


        private void ProductListGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                // If we're on the first row, move focus back to TextBox
                if (ProductListGrid.CurrentRow != null &&
                    ProductListGrid.CurrentRow.Index == 0)
                {
                    SearchProductTxt.Focus();
                    SearchProductTxt.SelectAll(); // Optional: select all text
                    e.Handled = true;
                }
            }

            if (e.KeyCode == Keys.Enter && !e.Handled)
            {
                e.Handled = true;
                e.SuppressKeyPress = true; // This prevents the beep sound and default behavior

                if (ProductListGrid.CurrentRow != null && ProductListGrid.CurrentRow.Index >= 0)
                {
                    HandleRowSelection(ProductListGrid.CurrentRow.Index);
                }
            }
        }

        private void HandleRowSelection(int rowIndex)
        {
            try
            {
                DataGridViewRow selectedRow = ProductListGrid.Rows[rowIndex];

                // Safely get values with null checking
                ProdIdLbl.Text = selectedRow.Cells[0].Value?.ToString() ?? string.Empty;
                PNameLbl.Text = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
                PUNameLbl.Text = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
                PTypeLbl.Text = selectedRow.Cells[3].Value?.ToString() ?? string.Empty;
                ProdSalePriceLbl.Text = selectedRow.Cells[4].Value?.ToString() ?? string.Empty;

                FormCloseLbl.Text = "false";
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        //private void HandleEnterPressed(int rowIndex)
        //{

        //    // Your logic when Enter is pressed on a row
        //    DataGridViewRow selectedRow = ProductListGrid.Rows[rowIndex];
        //    int pId = Convert.ToInt32(selectedRow.Cells[0].Value);
        //    PNameLbl.Text = (string)selectedRow.Cells[1].Value;
        //    PUNameLbl.Text = (string)selectedRow.Cells[2].Value;
        //    //PTypeLbl.Text = (string)selectedRow.Cells[3].Value;
        //    PTypeLbl.Text = selectedRow.Cells[3].Value == null
        //                || selectedRow.Cells[3].Value == DBNull.Value
        //                ? string.Empty
        //                : selectedRow.Cells[3].Value.ToString();
        //    //ProdSalePriceLbl.Text = (string)selectedRow.Cells[4].Value;
        //    ProdSalePriceLbl.Text = selectedRow.Cells[4].Value == null
        //        || selectedRow.Cells[4].Value == DBNull.Value
        //        ? string.Empty
        //        : selectedRow.Cells[4].Value.ToString();
        //    ProdIdLbl.Text = pId.ToString();
        //    FormCloseLbl.Text = "false";
        //}
        //private void ProductListGrid_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == (char)Keys.Enter && !e.Handled)
        //    {
        //        e.Handled = true;

        //        if (ProductListGrid.CurrentRow != null &&
        //            ProductListGrid.CurrentRow.Index >= 0)
        //        {

        //            //int pId = Convert.ToInt32(ProductListGrid.CurrentRow.Cells[0].Value);
        //            //PNameLbl.Text = (string)ProductListGrid.CurrentRow.Cells[1].Value;
        //            //PUNameLbl.Text = (string)ProductListGrid.CurrentRow.Cells[2].Value;
        //            ////PTypeLbl.Text = (string)ProductListGrid.CurrentRow.Cells[3].Value;
        //            //PTypeLbl.Text = ProductListGrid.CurrentRow.Cells[3].Value == null
        //            //            || ProductListGrid.CurrentRow.Cells[3].Value == DBNull.Value
        //            //            ? string.Empty
        //            //            : ProductListGrid.CurrentRow.Cells[3].Value.ToString();
        //            ////ProdSalePriceLbl.Text = (string)ProductListGrid.CurrentRow.Cells[4].Value;
        //            //ProdSalePriceLbl.Text = ProductListGrid.CurrentRow.Cells[4].Value == null
        //            //    || ProductListGrid.CurrentRow.Cells[4].Value == DBNull.Value
        //            //    ? string.Empty
        //            //    : ProductListGrid.CurrentRow.Cells[4].Value.ToString();
        //            //ProdIdLbl.Text = pId.ToString();
        //            //FormCloseLbl.Text = "false";
        //            //int currentIndex = ProductListGrid.CurrentRow.Index;
        //            //int targetIndex;

        //            //// Determine target index based on your logic
        //            //if (currentIndex == ProductListGrid.Rows.Count - 1)
        //            //{
        //            //    targetIndex = currentIndex + 1; // Move to next if last row
        //            //}
        //            //else
        //            //{
        //            //    targetIndex = currentIndex - 1; // Move to previous otherwise
        //            //}

        //            //int currentIndex = ProductListGrid.CurrentRow.Index;
        //            //int targetIndex = currentIndex - 1; // your logic

        //            //if (targetIndex < 0 || targetIndex >= ProductListGrid.Rows.Count)
        //            //    targetIndex = currentIndex; // fallback to current row

        //            //int pId = Convert.ToInt32(ProductListGrid.CurrentRow.Cells[0].Value);
        //            //PNameLbl.Text = (string)ProductListGrid.CurrentRow.Cells[1].Value;
        //            //int currentIndex = ProductListGrid.CurrentRow.Index;


        //            //    int targetIndex = currentIndex; // default to current row

        //            //    if (currentIndex > 0 && currentIndex < ProductListGrid.Rows.Count - 1)
        //            //    {
        //            //        targetIndex = currentIndex - 1; // only go previous if not first/last
        //            //    }

        //            //HandleEnterPressed(targetIndex);
        //            //this.Close();


        //            // Simulate a mouse click on the current row
        //            int rowIndex = ProductListGrid.CurrentRow.Index;
        //            int columnIndex = ProductListGrid.CurrentCell?.ColumnIndex ?? 0;

        //            // Create mouse event args and call the mouse click handler
        //            var mouseArgs = new DataGridViewCellMouseEventArgs(
        //                columnIndex, rowIndex, 0, 0, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0)
        //            );

        //            ProductListGrid_CellMouseClick(sender, mouseArgs);
        //        }
        //    }
        //}



    }
}
