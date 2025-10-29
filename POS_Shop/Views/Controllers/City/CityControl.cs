using Bunifu.UI.WinForms;
using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.City
{
    public partial class CityControl : UserControl
    {

        private int PageSize = 30;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";
        public CityControl()
        {
            InitializeComponent();

            this.Load += CityControl_Load;

            //loadCountriesForDropdown();
            //LoadCitiesForDataGridView();
        }
        private async void CityControl_Load(object sender, EventArgs e)
        {
            loadCountriesForDropdown(); // If this is sync, keep it here
            await LoadCitiesForDataGridView(); // Now you can await it safely
        }


        private void loadCountriesForDropdown()
        {
            using (var context = new POSDbContext())
            {
                var countriesList = context.Countries.ToList();
                CountryDropDownLst.Items.Clear();
                CountryDropDownLst.DataSource = countriesList;
                CountryDropDownLst.DisplayMember = "CountryName";
                CountryDropDownLst.ValueMember = "Id";
            }
        }
        private async void SaveCityBtn_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(CityNameTxt.Text))
                {

                    MessageBox.Show("Please Enter City Name or Select Country", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int selectedIndex = CountryDropDownLst.SelectedIndex - 1; // Adjust for default item

                using (var context = new POSDbContext())
                {
                    if(context.Cities.Any(s=>s.Name== CityNameTxt.Text))
                    {

                        MessageBox.Show("City name is already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    LoadingManager.ShowLoading();
                    int selectedId = Convert.ToInt32(CountryDropDownLst.SelectedValue);
                    ICityRepository countryRepository = new CityRepository(context);

                    countryRepository.Insert(new Models.City()
                    {
                        Name = CityNameTxt.Text,
                        IsActive = true,
                        CountryId = selectedId,
                    });
                    countryRepository.Save();
                }
                ClearFormFunction();
                 await LoadCitiesForDataGridView();

                LoadingManager.HideLoading();
                MessageBox.Show("City saved successfully!");
            }
            catch (Exception ex)
            {

                LoadingManager.HideLoading();
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private async Task LoadCitiesForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                ICityRepository cityRepository = new CityRepository(context);
                //var cities = await cityRepository.GetCitiesListAsync();

                var result= await cityRepository.GetCitiesPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Country Id", typeof(int));
                dt.Columns.Add("Country Name", typeof(string));
                dt.Columns.Add("IsActive", typeof(string));

                foreach (var country in result.data)
                {
                    dt.Rows.Add(country.Id, country.Name, country.CountryId, country.CountryName, country.IsActive);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                CountryDatagridView.ReadOnly = true;
                CountryDatagridView.AllowUserToAddRows = false;

                CountryDatagridView.DataSource = dt;
                CountryDatagridView.Columns[2].Visible = false;

                UpdatePager();
            }
        }
        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
          //  lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PrevBtn.Enabled = PageIndex > 1;
            NextBtn.Enabled = PageIndex < totalPages;
        }


        private void CountryDatagridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CountryDatagridView.Rows[e.RowIndex];
                CityNameTxt.Text = row.Cells["Name"].Value.ToString();
                cityIdTxt.Text = row.Cells["ID"].Value.ToString();
                if (row.Cells["Country Id"] != null && row.Cells["Country Id"].Value != null)
                {
                    int countryId = Convert.ToInt32(row.Cells["Country Id"].Value);
                    CountryDropDownLst.SelectedValue = countryId;
                }

                UpdateCitybtn.Enabled = true;
                UpdateCitybtn.Visible = true;

            }
        }

        private async void UpdateCitybtn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(cityIdTxt.Text) || !int.TryParse(cityIdTxt.Text, out int cityId) || cityId <= 0)
            {
                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var context = new POSDbContext())
            {
                ICityRepository cityRepository = new CityRepository(context);
                var response = await cityRepository.UpdateCity(new Models.City()
                {
                    Id = Convert.ToInt32(cityId.ToString()),
                    Name = CityNameTxt.Text,
                    IsActive = true,
                    CountryId = Convert.ToInt32(CountryDropDownLst.SelectedValue),
                });

                if (response)
                    MessageBox.Show("Record has been Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Something went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                await LoadCitiesForDataGridView();
            }
        }



        private async void SearchCity_TextChanged(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = SearchCityTxt.Text.Trim();
            await LoadCitiesForDataGridView();
        }

        private async void NextBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadCitiesForDataGridView();
            }
        }

        private async void PrevBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
              await  LoadCitiesForDataGridView();
            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            CityPrintPreviewDialog.Document = CityPrintDocument;
            //CityPrintPreviewDialog.ShowDialog();

            // Simulate Ctrl + F11 key press, to shift the control automatically because we are using Auto sharing printer usb
            SendKeys.SendWait("^{F11}");
            CityPrintDocument.Print();
        }



        private void CityPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Thermal printer settings (80mm paper)
            int paperWidth = 280; // pixels for 80mm paper
            int leftMargin = 5;
            int currentY = 5;
            int lineHeight = 12;
            int sectionSpacing = 3;

            // Fonts for thermal printing
            Font titleFont = new Font("Arial", 11, FontStyle.Bold);
            Font headerFont = new Font("Arial", 9, FontStyle.Bold);
            Font regularFont = new Font("Arial", 8, FontStyle.Regular);
            Font smallFont = new Font("Arial", 7, FontStyle.Regular);

            // Urdu font
            Font urduFont = new Font("Nafees Web Naskh", 8, FontStyle.Regular);
            if (urduFont.Name != "Nafees Web Naskh")
                urduFont = new Font("Arial", 8, FontStyle.Regular);

            // Center alignment
            StringFormat centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;

            // Right alignment for numbers
            StringFormat rightFormat = new StringFormat();
            rightFormat.Alignment = StringAlignment.Far;

            string dashLine = new string('-', 82);

            // 1. COMPANY HEADER

            e.Graphics.DrawString("CITY ELECTRONICS", titleFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
            currentY += lineHeight * 2;

            e.Graphics.DrawString("Contact: 0551234567", smallFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight;

            currentY += lineHeight + 2;

            // 2. INVOICE INFO
            e.Graphics.DrawString("INVOICE", headerFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Customer: Tayyab Bhatti", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Invoice #: INV-" + DateTime.Now.ToString("yyyyMMdd-HHmm"), regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 3. TABLE LAYOUT - FIXED COLUMN POSITIONS TO PREVENT OVERLAP
            int productCol = leftMargin;                    // Product name column
            int productColWidth = 120;                      // Width for product names

            int typeCol = productCol + productColWidth + 5; // Type column
            int typeColWidth = 30;

            int qtyCol = typeCol + typeColWidth + 5;        // Qty column
            int qtyColWidth = 25;

            int priceCol = qtyCol + qtyColWidth + 5;        // Price column
            int priceColWidth = 40;

            int totalCol = priceCol + priceColWidth + 5;    // Total column
            int totalColWidth = 40;

            // Draw table headers
            e.Graphics.DrawString("Product", headerFont, Brushes.Black, productCol, currentY);
            e.Graphics.DrawString("Type", headerFont, Brushes.Black, typeCol, currentY);
            e.Graphics.DrawString("Qty", headerFont, Brushes.Black, qtyCol, currentY);
            e.Graphics.DrawString("Price", headerFont, Brushes.Black, priceCol, currentY);
            e.Graphics.DrawString("Total", headerFont, Brushes.Black, totalCol, currentY);

            currentY += lineHeight;
            currentY += 3;
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
            currentY += 5;

            // 4. ITEM LISTING - SEPARATE ROWS FOR PRODUCT NAMES AND DETAILS
            var items = new[]
            {
                new { Name = "USB Cable 3.0", Type = "ڈبیہ", Qty = 2, Price = 5.00m, Total = 10.00m },
                new { Name = "Phone Case X4", Type = "عدد", Qty = 1, Price = 15.00m, Total = 15.00m },
                new { Name = "Screen Protector", Type = "جوڑی", Qty = 3, Price = 8.00m, Total = 24.00m },
                new { Name = "Wireless Charger", Type = "گز", Qty = 1, Price = 25.00m, Total = 25.00m },
                new { Name = "HDMI Cable 2m", Type = "پیکٹ", Qty = 2, Price = 12.50m, Total = 25.00m },
                new { Name = "Batteries AA", Type = "بنڈل", Qty = 1, Price = 10.00m, Total = 10.00m }
            };

            // Draw each item with product name on one line and details on the next
            foreach (var item in items)
            {
                // First line: Product name only (left aligned)
                e.Graphics.DrawString(item.Name, regularFont, Brushes.Black, productCol, currentY);
                currentY += lineHeight;

                // Second line: Type, Qty, Price, Total (in columns)
                e.Graphics.DrawString(item.Type, urduFont, Brushes.Black, typeCol, currentY);
                e.Graphics.DrawString(item.Qty.ToString(), regularFont, Brushes.Black, qtyCol, currentY);
                e.Graphics.DrawString(item.Price.ToString("0.00"), regularFont, Brushes.Black, priceCol, currentY);
                e.Graphics.DrawString(item.Total.ToString("0.00"), regularFont, Brushes.Black, totalCol, currentY);
                currentY += lineHeight;

                // Light separator line between items
                e.Graphics.DrawLine(Pens.LightGray, leftMargin, currentY, totalCol + totalColWidth, currentY);
                currentY += 2;
            }

            // Bottom line of table
            e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, totalCol + totalColWidth, currentY);
            currentY += lineHeight;

            // 5. TOTALS SECTION - MOVED LEFT FOR BETTER ALIGNMENT
            decimal subtotal = items.Sum(item => item.Total);
            decimal taxRate = 0.05m;
            decimal taxAmount = Math.Round(subtotal * taxRate, 2);
            decimal total = subtotal + taxAmount;

            // Move totals left by using priceCol-20 instead of priceCol
            int totalsLabelCol = priceCol - 20; // Move labels 20 pixels left
            int totalsValueCol = totalCol - 15; // Move values 15 pixels left

            e.Graphics.DrawString("Subtotal:", regularFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(subtotal.ToString("0.00"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("Tax (5%):", regularFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(taxAmount.ToString("0.00"), regularFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("TOTAL:", headerFont, Brushes.Black, totalsLabelCol, currentY);
            e.Graphics.DrawString(total.ToString("0.00"), headerFont, Brushes.Black, totalsValueCol, currentY);
            currentY += lineHeight;

            currentY += lineHeight;

            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight + 2;

            // 6. PAYMENT INFORMATION
            e.Graphics.DrawString("Payment Method: CASH", regularFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            decimal tendered = 120.00m;
            decimal change = tendered - total;

            e.Graphics.DrawString("Paid: " + tendered.ToString("0.00"), regularFont, Brushes.Black, leftMargin, currentY);
            e.Graphics.DrawString("Change: " + change.ToString("0.00"), regularFont, Brushes.Black, (totalsValueCol - 35), currentY);
            currentY += lineHeight + 2;

            // 7. FOOTER
            e.Graphics.DrawString(dashLine, smallFont, Brushes.Black, leftMargin, currentY);
            currentY += lineHeight;

            e.Graphics.DrawString("خریدا ہوا سامان واپس یا تبدیل نہیں ہوگا", headerFont, Brushes.Black,
                                 new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
            currentY += lineHeight;

            //e.Graphics.DrawString("7-day return with receipt", smallFont, Brushes.Black,
            //                     new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        }

        private void CityPrintDocument_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font regularFont = new Font("Courier New", 8);
            Font boldFont = new Font("Courier New", 8, FontStyle.Bold);
            Font titleFont = new Font("Courier New", 12, FontStyle.Bold);

            int paperWidth = 280;
            int margin = 10;
            int yPos = margin;
            int lineHeight = 15;

            // Header
            DrawCenteredString(graphics, "CITIES LIST REPORT", titleFont, paperWidth, ref yPos);
            yPos += 10;

            graphics.DrawString($"Report Date: {DateTime.Now:dd-MMM-yyyy HH:mm}", regularFont, Brushes.Black, margin, yPos);
            yPos += lineHeight;

            DrawLine(graphics, paperWidth, ref yPos);

            // Column headers
            int[] columnWidths = { 40, 100, 100, 40 };
            int xPos = margin;

            string[] headers = { "ID", "City Name", "Country", "Status" };
            for (int i = 0; i < headers.Length; i++)
            {
                graphics.DrawString(headers[i], boldFont, Brushes.Black, xPos, yPos);
                xPos += columnWidths[i];
            }

            yPos += lineHeight;
            DrawLine(graphics, paperWidth, ref yPos);

            // Data rows - print all rows on one page
            foreach (DataGridViewRow row in CountryDatagridView.Rows)
            {
                if (row.IsNewRow) continue;

                xPos = margin;

                graphics.DrawString(row.Cells["ID"].Value?.ToString() ?? "", regularFont, Brushes.Black, xPos, yPos);
                xPos += columnWidths[0];

                graphics.DrawString(row.Cells["Name"].Value?.ToString() ?? "", regularFont, Brushes.Black, xPos, yPos);
                xPos += columnWidths[1];

                graphics.DrawString(row.Cells["Country Name"].Value?.ToString() ?? "", regularFont, Brushes.Black, xPos, yPos);
                xPos += columnWidths[2];

                graphics.DrawString(row.Cells["IsActive"].Value?.ToString() ?? "", regularFont, Brushes.Black, xPos, yPos);

                yPos += lineHeight;
            }

            // Footer
            DrawLine(graphics, paperWidth, ref yPos);
            yPos += 5;

            int totalRecords = CountryDatagridView.Rows.Count - (CountryDatagridView.AllowUserToAddRows ? 1 : 0);
            graphics.DrawString($"Total Records: {totalRecords}", boldFont, Brushes.Black, margin, yPos);
            yPos += lineHeight;

            DrawCenteredString(graphics, "*** End of Report ***", regularFont, paperWidth, ref yPos);

            e.HasMorePages = false; // Always false for single page
        }

        private void DrawLine(Graphics graphics, int paperWidth, ref int yPos)
        {
            graphics.DrawLine(Pens.Black, 10, yPos, paperWidth - 10, yPos);
            yPos += 5;
        }


        private void DrawCenteredString(Graphics graphics, string text, Font font, int paperWidth, ref int yPos)
        {
            SizeF textSize = graphics.MeasureString(text, font);
            int xPos = (paperWidth - (int)textSize.Width) / 2;
            graphics.DrawString(text, font, Brushes.Black, xPos, yPos);
            yPos += (int)textSize.Height + 2;
        }

        private async void ResetFromBtn_Click(object sender, EventArgs e)
        {
            ClearFormFunction();

            await LoadCitiesForDataGridView();
        }

        private void ClearFormFunction()
        {
            // Clear all input fields
            CityNameTxt.Clear();
            cityIdTxt.Clear();
            CountryDropDownLst.SelectedIndex = 0;
        }

    }
}


