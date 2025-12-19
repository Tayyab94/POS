using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace POS_Shop.Views.Settings
{
    public partial class PirntersAndScanners : Form
    {
        private PrintDocument printDoc = new PrintDocument();

        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string Name);
        public PirntersAndScanners()
        {
            InitializeComponent();
            PopulateInstalledPrintersCombo();
        }

        private void PopulateInstalledPrintersCombo()
        {

            List<string> printers = new List<string>();
            // Add list of installed printers found to the combo box.
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                printers.Add(printer);
            }

            PrinterScannersDDL.DataSource = printers;

            // Optional: Select the default printer automatically.
            string defaultPrinter = printDoc.PrinterSettings.PrinterName;
            if (PrinterScannersDDL.Items.Contains(defaultPrinter))
            {
                PrinterScannersDDL.SelectedItem = defaultPrinter;
            }
        }



        private void MinimizeBtn_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void SetDefaultPrinterBtn_Click(object sender, EventArgs e)
        {
            if (PrinterScannersDDL.SelectedIndex != -1)
            {
                //printDoc.PrinterSettings.PrinterName= PrinterScannersDDL.SelectedItem.ToString();

                string selected = PrinterScannersDDL.SelectedItem.ToString();
                if (SetDefaultPrinter(selected))
                {
                    MessageBox.Show("Windows system default changed!", "Printer Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Get the specific Windows error code if it fails
                    int error = Marshal.GetLastWin32Error();
                    MessageBox.Show($"Failed to set default printer. Error code: {error}");
                }
            }
        }
    }
}
