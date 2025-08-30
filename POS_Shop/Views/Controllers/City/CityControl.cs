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
                LoadingManager.ShowLoading();
                if (string.IsNullOrEmpty(CityNameTxt.Text))
                {
                    MessageBox.Show("Please Enter City Name or Select Country", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int selectedIndex = CountryDropDownLst.SelectedIndex - 1; // Adjust for default item

                using (var context = new POSDbContext())
                {

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
               
                await LoadCitiesForDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                LoadingManager.HideLoading();
                MessageBox.Show("City saved successfully!");
            }
            

        }



        private async Task LoadCitiesForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                ICityRepository cityRepository = new CityRepository(context);
                var cities = await cityRepository.GetCitiesListAsync();

                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Country Id", typeof(int));
                dt.Columns.Add("Country Name", typeof(string));
                dt.Columns.Add("IsActive", typeof(string));

                foreach (var country in cities)
                {
                    dt.Rows.Add(country.Id, country.Name, country.CountryId, country.CountryName, country.IsActive);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                CountryDatagridView.ReadOnly = true;
                CountryDatagridView.AllowUserToAddRows = false;

                CountryDatagridView.DataSource = dt;
                CountryDatagridView.Columns[2].Visible = false;
            }
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

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            CityPrintPreviewDialog.Document = CityPrintDocument;
            //CityPrintPreviewDialog.ShowDialog();
            CityPrintDocument.Print();
        }



        //string dashLine = "----------------------------------------------";

        //e.Graphics.DrawString("City Electronics", new Font("Arial",18, FontStyle.Bold), Brushes.Black, new Point(13, 30));
        //e.Graphics.DrawString("Contact No: 0551234567", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, new Point(13, 60));

        ////e.Graphics.DrawString(dashLine, new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(10, 90));

        //e.Graphics.DrawString("Tayyab Bhatti", new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(25, 100));
        //e.Graphics.DrawString("Date :" + DateTime.Now.ToShortDateString(), new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(25, 130));
        //e.Graphics.DrawString(dashLine, new Font("Arial", 11, FontStyle.Regular), Brushes.Black, new Point(25, 142));

        //    private void CityPrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //    {
        //        // Thermal printer settings (80mm paper)
        //        int paperWidth = 280; // pixels for 80mm paper
        //        int leftMargin = 10;
        //        int currentY = 10;
        //        int lineHeight = 15;
        //        int sectionSpacing = 5;

        //        // Fonts for thermal printing
        //        Font titleFont = new Font("Arial", 14, FontStyle.Bold);
        //        Font headerFont = new Font("Arial", 10, FontStyle.Bold);
        //        Font regularFont = new Font("Arial", 9, FontStyle.Regular);
        //        Font smallFont = new Font("Arial", 8, FontStyle.Regular);

        //        // Urdu font (make sure you have a Urdu font installed)
        //        Font urduFont = new Font("Nafees Web Naskh", 9, FontStyle.Regular);
        //        // Fallback if Urdu font not available
        //        if (urduFont.Name != "Nafees Web Naskh")
        //            urduFont = new Font("Arial", 9, FontStyle.Regular);

        //        // Center alignment
        //        StringFormat centerFormat = new StringFormat();
        //        centerFormat.Alignment = StringAlignment.Center;

        //        // Right alignment for numbers
        //        StringFormat rightFormat = new StringFormat();
        //        rightFormat.Alignment = StringAlignment.Far;

        //        string dashLine = new string('-', 42);
        //        string doubleLine = new string('=', 42);

        //        // 1. COMPANY HEADER (Centered)
        //        e.Graphics.DrawString("CITY ELECTRONICS", titleFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2), centerFormat);
        //        currentY += lineHeight * 2;

        //        e.Graphics.DrawString("Contact: 0551234567", regularFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("123 Main Street, City Center", smallFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        //        currentY += lineHeight + sectionSpacing;

        //        e.Graphics.DrawString(dashLine, regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight + sectionSpacing;

        //        // 2. INVOICE INFO
        //        e.Graphics.DrawString("INVOICE", headerFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("Customer: Tayyab Bhatti", regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("Invoice #: INV-" + DateTime.Now.ToString("yyyyMMdd-HHmm"), regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight + sectionSpacing;

        //        e.Graphics.DrawString(dashLine, regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight + sectionSpacing;

        //        // 3. TABLE COLUMN HEADERS FOR DETAIL ROW
        //        int col1 = leftMargin + 15;                 // Type column (indented)
        //        int col1Width = 40;

        //        int col2 = col1 + col1Width + 10;           // Qty column
        //        int col2Width = 30;

        //        int col3 = col2 + col2Width + 10;           // Price column
        //        int col3Width = 50;

        //        int col4 = col3 + col3Width + 10;           // Total column
        //        int col4Width = 50;

        //        // Draw table headers with background for details row
        //        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
        //                               new Rectangle(col1, currentY, col1Width, lineHeight));
        //        e.Graphics.DrawString("Type", headerFont, Brushes.Black, col1, currentY);

        //        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
        //                               new Rectangle(col2, currentY, col2Width, lineHeight));
        //        e.Graphics.DrawString("Qty", headerFont, Brushes.Black, col2, currentY);

        //        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
        //                               new Rectangle(col3, currentY, col3Width, lineHeight));
        //        e.Graphics.DrawString("Price", headerFont, Brushes.Black, col3, currentY);

        //        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
        //                               new Rectangle(col4, currentY, col4Width, lineHeight));
        //        e.Graphics.DrawString("Total", headerFont, Brushes.Black, col4, currentY);

        //        currentY += lineHeight;

        //        // Draw horizontal line under headers
        //        e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, col4 + col4Width, currentY);
        //        currentY += 2;

        //        // 4. ITEM LISTING WITH TWO-ROW FORMAT
        //        var items = new[]
        //        {
        //    new { Name = "USB Cable 3.0", Type = "ڈبیہ", Qty = 2, Price = 5.00m, Total = 10.00m },
        //    new { Name = "Phone Case X4", Type = "عدد", Qty = 1, Price = 15.00m, Total = 15.00m },
        //    new { Name = "Screen Protector", Type = "جوڑی", Qty = 3, Price = 8.00m, Total = 24.00m },
        //    new { Name = "Wireless Charger", Type = "گز", Qty = 1, Price = 25.00m, Total = 25.00m },
        //    new { Name = "HDMI Cable 2m", Type = "پیکٹ", Qty = 2, Price = 12.50m, Total = 25.00m },
        //    new { Name = "Batteries AA", Type = "بنڈل", Qty = 1, Price = 10.00m, Total = 10.00m }
        //};

        //        foreach (var item in items)
        //        {
        //            // First row: Product name only
        //            if (currentY % (lineHeight * 2) == 0) // Alternate row colors
        //                e.Graphics.FillRectangle(new SolidBrush(Color.WhiteSmoke),
        //                                       new Rectangle(leftMargin, currentY, paperWidth, lineHeight * 2));

        //            e.Graphics.DrawString(item.Name, regularFont, Brushes.Black, leftMargin, currentY);
        //            currentY += lineHeight;

        //            // Second row: Type, Qty, Price, Total
        //            e.Graphics.DrawString(item.Type, urduFont, Brushes.Black, col1, currentY);
        //            e.Graphics.DrawString(item.Qty.ToString(), regularFont, Brushes.Black, col2, currentY);
        //            e.Graphics.DrawString(item.Price.ToString("0.00"), regularFont, Brushes.Black, col3, currentY);
        //            e.Graphics.DrawString(item.Total.ToString("0.00"), regularFont, Brushes.Black, col4, currentY);

        //            // Draw vertical lines between columns for details row only
        //            e.Graphics.DrawLine(Pens.LightGray, col1, currentY, col1, currentY + lineHeight);
        //            e.Graphics.DrawLine(Pens.LightGray, col2, currentY, col2, currentY + lineHeight);
        //            e.Graphics.DrawLine(Pens.LightGray, col3, currentY, col3, currentY + lineHeight);
        //            e.Graphics.DrawLine(Pens.LightGray, col4, currentY, col4, currentY + lineHeight);

        //            currentY += lineHeight;

        //            // Draw horizontal line between items
        //            e.Graphics.DrawLine(Pens.LightGray, leftMargin, currentY, col4 + col4Width, currentY);
        //            currentY += 2;
        //        }

        //        // Draw bottom line of table
        //        e.Graphics.DrawLine(Pens.Black, leftMargin, currentY, col4 + col4Width, currentY);
        //        currentY += lineHeight;

        //        // 5. TOTALS SECTION - PROPERLY ALIGNED
        //        decimal subtotal = items.Sum(item => item.Total);
        //        decimal taxRate = 0.05m; // 5% tax
        //        decimal taxAmount = Math.Round(subtotal * taxRate, 2);
        //        decimal total = subtotal + taxAmount;

        //        // Align totals with the price and total columns
        //        e.Graphics.DrawString("Subtotal:", regularFont, Brushes.Black, col3, currentY);
        //        e.Graphics.DrawString(subtotal.ToString("0.00"), regularFont, Brushes.Black, col4, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("Tax (5%):", regularFont, Brushes.Black, col3, currentY);
        //        e.Graphics.DrawString(taxAmount.ToString("0.00"), regularFont, Brushes.Black, col4, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString(doubleLine, regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("TOTAL:", headerFont, Brushes.Black, col3, currentY);
        //        e.Graphics.DrawString(total.ToString("0.00"), headerFont, Brushes.Black, col4, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString(doubleLine, regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight * 2;

        //        // 6. PAYMENT INFORMATION
        //        e.Graphics.DrawString("Payment Method: CASH", regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight;

        //        decimal tendered = 120.00m;
        //        decimal change = tendered - total;

        //        e.Graphics.DrawString("Amount Tendered: " + tendered.ToString("0.00"), regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("Change: " + change.ToString("0.00"), regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight + sectionSpacing;

        //        e.Graphics.DrawString(dashLine, regularFont, Brushes.Black, leftMargin, currentY);
        //        currentY += lineHeight + sectionSpacing;

        //        // 7. FOOTER
        //        e.Graphics.DrawString("Thank you for your business!", regularFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        //        currentY += lineHeight;

        //        e.Graphics.DrawString("7-day return policy with receipt", smallFont, Brushes.Black,
        //                             new Rectangle(leftMargin, currentY, paperWidth, lineHeight), centerFormat);
        //    }


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

      
    }
}





//using Bunifu.UI.WinForms;
//using POS_Shop.Interfaces;
//using POS_Shop.Models;
//using POS_Shop.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace POS_Shop.Views.Controllers.City
//{
//    public partial class CityControl : UserControl
//    {
//        public CityControl()
//        {
//            InitializeComponent();

//            this.Load += CityControl_Load;

//            //loadCountriesForDropdown();
//            //LoadCitiesForDataGridView();
//        }
//        private async void CityControl_Load(object sender, EventArgs e)
//        {
//            loadCountriesForDropdown(); // If this is sync, keep it here
//            await LoadCitiesForDataGridView(); // Now you can await it safely
//        }


//        private void loadCountriesForDropdown()
//        {
//            using (var context = new POSDbContext())
//            {
//                var countriesList = context.Countries.ToList();
//                CountryDropDownLst.Items.Clear();
//                CountryDropDownLst.DataSource = countriesList;
//                CountryDropDownLst.DisplayMember = "CountryName";
//                CountryDropDownLst.ValueMember = "Id";
//            }
//        }
//        private async void SaveCityBtn_Click(object sender, EventArgs e)
//        {
//            int selectedIndex = CountryDropDownLst.SelectedIndex - 1; // Adjust for default item

//            using (var context = new POSDbContext())
//            {

//                int selectedId = Convert.ToInt32(CountryDropDownLst.SelectedValue);
//                ICityRepository countryRepository = new CityRepository(context);

//                countryRepository.Insert(new Models.City()
//                {
//                    Name = CityNameTxt.Text,
//                    IsActive = true,
//                    CountryId = selectedId,
//                });
//                countryRepository.Save();
//            }
//            MessageBox.Show("City saved successfully!");
//            await LoadCitiesForDataGridView();

//        }



//        private async Task LoadCitiesForDataGridView()
//        {
//            using (var context = new POSDbContext())
//            {
//                ICityRepository cityRepository = new CityRepository(context);
//                var cities = await cityRepository.GetCitiesListAsync();

//                DataTable dt = new DataTable();
//                dt.Columns.Add("ID", typeof(int));
//                dt.Columns.Add("Name", typeof(string));
//                dt.Columns.Add("Country Id", typeof(int));
//                dt.Columns.Add("Country Name", typeof(string));
//                dt.Columns.Add("IsActive", typeof(string));

//                foreach (var country in cities)
//                {
//                    dt.Rows.Add(country.Id, country.Name, country.CountryId, country.CountryName, country.IsActive);
//                }

//                //CountryDatagridView.AutoGenerateColumns = true;
//                CountryDatagridView.ReadOnly = true;
//                CountryDatagridView.AllowUserToAddRows = false;

//                CountryDatagridView.DataSource = dt;
//                CountryDatagridView.Columns[2].Visible = false;
//            }
//        }

//        private void CountryDatagridView_CellClick(object sender, DataGridViewCellEventArgs e)
//        {

//            if (e.RowIndex >= 0)
//            {
//                DataGridViewRow row = CountryDatagridView.Rows[e.RowIndex];
//                CityNameTxt.Text = row.Cells["Name"].Value.ToString();
//                cityIdTxt.Text = row.Cells["ID"].Value.ToString();
//                if (row.Cells["Country Id"] != null && row.Cells["Country Id"].Value != null)
//                {
//                    int countryId = Convert.ToInt32(row.Cells["Country Id"].Value);
//                    CountryDropDownLst.SelectedValue = countryId;
//                }

//                UpdateCitybtn.Enabled = true;
//                UpdateCitybtn.Visible = true;

//            }
//        }

//        private void UpdateCitybtn_Click(object sender, EventArgs e)
//        {

//            if (string.IsNullOrEmpty(cityIdTxt.Text) || !int.TryParse(cityIdTxt.Text, out int cityId) || cityId <= 0)
//            {
//                MessageBox.Show("Please select Record first", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
//                return;
//            }

//            using (var context = new POSDbContext())
//            {
//                ICityRepository cityRepository = new CityRepository(context);

//            }
//        }

//        private void PrintBtn_Click(object sender, EventArgs e)
//        {
//            PrintRawToThermalPrinter();
//        }

//        private void PrintRawToThermalPrinter()
//        {
//            try
//            {
//                StringBuilder sb = new StringBuilder();

//                // Printer initialization commands (ESC/POS)
//                sb.Append((char)27 + "@"); // Initialize printer
//                sb.Append((char)27 + "!" + (char)48); // Character style

//                // Header
//                sb.Append((char)27 + "!" + (char)52); // Double height and width
//                sb.Append("INVOICE\n");
//                sb.Append((char)27 + "!" + (char)0); // Normal text
//                sb.Append($"Date: {DateTime.Now:yyyy-MM-dd HH:mm}\n");
//                sb.Append("--------------------------------\n");

//                // Column headers
//                foreach (DataGridViewColumn column in CountryDatagridView.Columns)
//                {
//                    if (column.Visible)
//                    {
//                        sb.Append(column.HeaderText.PadRight(15).Substring(0, 15));
//                    }
//                }
//                sb.Append("\n--------------------------------\n");

//                // Data rows
//                foreach (DataGridViewRow row in CountryDatagridView.Rows)
//                {
//                    if (!row.IsNewRow)
//                    {
//                        foreach (DataGridViewCell cell in row.Cells)
//                        {
//                            if (cell.OwningColumn.Visible)
//                            {
//                                string value = cell.Value?.ToString() ?? "";
//                                sb.Append(value.PadRight(15).Substring(0, 15));
//                            }
//                        }
//                        sb.Append("\n");
//                    }
//                }

//                // Footer
//                sb.Append("--------------------------------\n");
//                //decimal total = CalculateTotal();
//                decimal total = 3.4m;
//                sb.Append((char)27 + "!" + (char)16); // Bold
//                sb.Append($"TOTAL: {total:C}\n");
//                sb.Append((char)27 + "!" + (char)0); // Normal
//                sb.Append("Thank you!\n");

//                sb.Append((char)29 + "V" + (char)66 + (char)0); // Cut paper

//                // Send to printer (you'll need to know your printer's name)
//                RawPrinterHelper.SendStringToPrinter("SpeedX Thermal Printer", sb.ToString());
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show($"Printing error: {ex.Message}");
//            }
//        }
//    }

//    // Helper class for raw printing
//    public class RawPrinterHelper
//    {
//        [System.Runtime.InteropServices.DllImport("winspool.Drv", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
//        private static extern bool OpenPrinter(string szPrinter, out IntPtr hPrinter, IntPtr pd);

//        [System.Runtime.InteropServices.DllImport("winspool.Drv")]
//        private static extern bool ClosePrinter(IntPtr hPrinter);

//        [System.Runtime.InteropServices.DllImport("winspool.Drv", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
//        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

//        public static bool SendStringToPrinter(string printerName, string data)
//        {
//            IntPtr pUnmanagedBytes = IntPtr.Zero;
//            Int32 dwCount = data.Length;

//            pUnmanagedBytes = System.Runtime.InteropServices.Marshal.StringToCoTaskMemAnsi(data);

//            IntPtr hPrinter;
//            OpenPrinter(printerName, out hPrinter, IntPtr.Zero);

//            Int32 dwWritten = 0;
//            bool success = WritePrinter(hPrinter, pUnmanagedBytes, dwCount, out dwWritten);

//            ClosePrinter(hPrinter);
//            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(pUnmanagedBytes);

//            return success;
//        }
//    }
//}


