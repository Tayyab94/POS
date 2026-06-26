using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Helpers
{
    /// <summary>
    /// Builds the action button columns for the TempOrder DataGridView.
    /// Extracted so the setup runs once (in the constructor) rather than on every data load.
    /// </summary>
    public static class TempOrderGridHelper
    {
        // Column name constants — reference these everywhere instead of magic strings
        public const string ColInvoiceNo = "Invoice No";
        public const string ColTotalBill = "Total Bill";
        public const string ColReceivedAmt = "Received Amt";
        public const string ColCustomer = "Customer";
        public const string ColCustomerId = "CustomerId";
        public const string ColDate = "Date";
        public const string ColDeleteAction = "DeleteAction";
        public const string ColPrintAction = "PrintAction";

        /// <summary>
        /// Adds Delete and Print button columns to the grid.
        /// Call this ONCE after InitializeComponent(), not on every reload.
        /// </summary>
        public static void AddActionColumns(DataGridView grid)
        {
            grid.Columns.Add(BuildButton(
                name: ColDeleteAction,
                header: "Delete",
                backColor: Color.FromArgb(220, 53, 69),
                selColor: Color.FromArgb(185, 28, 28),
                width: 65));

            grid.Columns.Add(BuildButton(
                name: ColPrintAction,
                header: "Print",
                backColor: Color.FromArgb(0, 123, 255),
                selColor: Color.FromArgb(0, 86, 179),
                width: 55));
        }

        // ── Private factory ───────────────────────────────────────────────────

        private static DataGridViewButtonColumn BuildButton(
            string name, string header, Color backColor, Color selColor, int width)
        {
            return new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = header,
                Text = header,
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                Width = width,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle =
                {
                    BackColor           = backColor,
                    ForeColor           = Color.White,
                    SelectionBackColor  = selColor,
                    SelectionForeColor  = Color.White
                }
            };
        }
    }
}
