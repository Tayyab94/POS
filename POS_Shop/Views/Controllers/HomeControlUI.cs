using POS_Shop.Models;
using POS_Shop.Views.Controllers.Order;
using POS_Shop.Views.Reports;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers
{
    public partial class HomeControlUI : UserControl
    {
        public HomeControlUI()
        {
            InitializeComponent();
            this.Load += HomeControlUI_Load;

        }

        private async void HomeControlUI_Load(object sender, EventArgs e)
        {
            using (var context = new POSDbContext())
            {
                var today = DateTime.Today.Date;

                // Get tomorrow's date with time set to midnight
                var tomorrow = today.AddDays(1);
                var TodayOrderCount = await Task.Run(() => context.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Count());
                var TodaySale = await Task.Run(() => context.Orders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Sum(s => (float?)s.TotalBill) ?? 0f);
                var TodayTempOrderCount = await Task.Run(() => context.TempOrders.Where(s => s.CreatedDate >= today && s.CreatedDate < tomorrow).Count());
                //// Update UI controls on the main thread
                //this.Invoke(new Action(() =>
                //{
                //    TodayTotalOrderLbl.Text = TodayOrderCount.ToString();
                //    TempTotalOrderLbl.Text = TodayTempOrderCount.ToString();
                //}));

                // Use BeginInvoke with IsHandleCreated check
                if (this.IsHandleCreated)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        TodayTotalOrderLbl.Text = TodayOrderCount.ToString();
                        TempTotalOrderLbl.Text = TodayTempOrderCount.ToString();
                    }));
                }
            }
        }



        private void ReportAnalysisLblBtn_Click(object sender, EventArgs e)
        {
            bool showDialog = true;
            while (showDialog)
            {
                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;

                    string userInput = dialog.InputValue;
                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
                    {
                        showDialog = false;
                        Form ProductForm = new Form();
                        ProductForm.Text = "Order Report Form";
                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

                        // Create an instance of your User Control
                        // Replace 'YourUserControl' with the actual name of your User Control

                        var FormCtrl = new SalesReportDataControl();
                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

                        // Add the User Control to the new Form's controls collection
                        ProductForm.Controls.Add(FormCtrl);
                        ProductForm.Width = 890; ProductForm.Height = 625;
                        // Show the new form
                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
                    }
                    else
                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void SalesReportBtn_Click(object sender, EventArgs e)
        {
            bool showDialog = true;
            while (showDialog)
            {
                using (var dialog = new InputDialog("Enter Password:", title: "Sales Chart Report", isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;

                    string userInput = dialog.InputValue;
                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
                    {
                        showDialog = false;
                        var saleChartForm = new SalesChartForm();
                        saleChartForm.ShowDialog();
                        this.Show();
                    }
                    else
                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void ProductSaleTrendBtn_Click(object sender, EventArgs e)
        {
            bool showDialog = true;
            while (showDialog)
            {
                using (var dialog = new InputDialog("Enter Password:", title: "Sales Chart Report", isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;

                    string userInput = dialog.InputValue;
                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
                    {
                        showDialog = false;
                        var saleChartForm = new ProductSalesTrendForm();
                        saleChartForm.ShowDialog();
                        this.Show();
                    }
                    else
                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }



        private void WeeklyReportAnalysisBtn_Click(object sender, EventArgs e)
        {
            bool showDialog = true;
            while (showDialog)
            {
                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;

                    string userInput = dialog.InputValue;
                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
                    {
                        showDialog = false;
                        Form ProductForm = new Form();
                        ProductForm.Text = "Order Report Form";
                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

                        // Create an instance of your User Control
                        // Replace 'YourUserControl' with the actual name of your User Control
                        var FormCtrl = new OrderReportControlUI();

                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

                        // Add the User Control to the new Form's controls collection
                        ProductForm.Controls.Add(FormCtrl);
                        ProductForm.Width = 750; ProductForm.Height = 525;
                        // Show the new form
                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
                    }
                    else
                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void BtnSalesAanlysis_Click(object sender, EventArgs e)
        {
            bool showDialog = true;
            while (showDialog)
            {
                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;

                    string userInput = dialog.InputValue;
                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
                    {
                        showDialog = false;
                        Form ProductForm = new Form();
                        ProductForm.Text = "Order Sales Report Analysis";
                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

                        // Create an instance of your User Control
                        // Replace 'YourUserControl' with the actual name of your User Control

                        var FormCtrl = new SalesReportDataControl();
                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

                        // Add the User Control to the new Form's controls collection
                        ProductForm.Controls.Add(FormCtrl);
                        ProductForm.Width = 890; ProductForm.Height = 625;
                        // Show the new form
                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
                    }
                    else
                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnWeeklySalesChart_Click(object sender, EventArgs e)
        {
            bool showDialog = true;
            while (showDialog)
            {
                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;

                    string userInput = dialog.InputValue;
                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
                    {
                        showDialog = false;
                        Form ProductForm = new Form();
                        ProductForm.Text = "Weekly Order Report Analysis";
                        ProductForm.StartPosition = FormStartPosition.CenterScreen;

                        // Create an instance of your User Control
                        // Replace 'YourUserControl' with the actual name of your User Control
                        var FormCtrl = new OrderReportControlUI();

                        FormCtrl.Dock = DockStyle.Fill; // Dock it to fill the entire form

                        // Add the User Control to the new Form's controls collection
                        ProductForm.Controls.Add(FormCtrl);
                        ProductForm.Width = 750; ProductForm.Height = 525;
                        // Show the new form
                        ProductForm.ShowDialog(); // Use ShowDialog() to open it as a modal dialog
                    }
                    else
                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PurchaseReportsBtn_Click(object sender, EventArgs e)
        {
            bool showDialog = true;
            while (showDialog)
            {
                using (var dialog = new InputDialog("Enter Password:", title: "Analysis Info", isTextBoxProtected: true))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;

                    string userInput = dialog.InputValue;
                    if (!string.IsNullOrWhiteSpace(userInput) && userInput.ToLower() == "show")
                    {
                        showDialog = false;
                        var purchaseOrderForm = new POS_Shop.Views.Controllers.Reports.ReportsMenuForm();

                        this.Hide();
                        purchaseOrderForm.ShowDialog();
                        this.Show();
                    }
                    else
                        MessageBox.Show("Invalid Input", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
