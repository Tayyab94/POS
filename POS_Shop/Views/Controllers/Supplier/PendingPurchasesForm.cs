


using ClosedXML.Excel;
using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    /// <summary>
    /// Shows all Purchases whose PaymentStatus != 2 (Paid).
    /// Supports multi-select export to Excel with full relational data.
    /// All FK links in the export use natural business keys (InvoiceNumber,
    /// PaymentNumber) so the file can be re-imported after a table reset.
    /// </summary>
    public partial class PendingPurchasesForm : Form
    {
        // ── Win32 placeholder text for TextBox (works on all .NET versions) ──
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);
        private const uint EM_SETCUEBANNER = 0x1501;

        private readonly string _connectionString;

        private DataTable _sourceData;
        private DataTable _filteredData;

        public PendingPurchasesForm(string connectionString)
        {
            InitializeComponent();
            _connectionString = connectionString;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  FORM LOAD
        // ─────────────────────────────────────────────────────────────────────

        private void PendingPurchasesForm_Load(object sender, EventArgs e)
        {
            // Set placeholder via Win32 (works on .NET Framework & .NET 5+)
            SendMessage(txtSearch.Handle, EM_SETCUEBANNER, (IntPtr)1, "Invoice #, Supplier, Ref…");

            ConfigureGrid();
            LoadData();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  GRID CONFIGURATION
        // ─────────────────────────────────────────────────────────────────────

        private void ConfigureGrid()
        {
            dgvPurchases.AutoGenerateColumns = false;
            dgvPurchases.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPurchases.MultiSelect = true;
            dgvPurchases.ReadOnly = true;
            dgvPurchases.AllowUserToAddRows = false;
            dgvPurchases.AllowUserToDeleteRows = false;
            dgvPurchases.RowHeadersVisible = false;
            dgvPurchases.BackgroundColor = Color.White;
            dgvPurchases.BorderStyle = BorderStyle.None;
            dgvPurchases.EnableHeadersVisualStyles = false;
            dgvPurchases.ColumnHeadersHeight = 38;
            dgvPurchases.RowTemplate.Height = 32;
            dgvPurchases.Font = new Font("Segoe UI", 9.5f);
            dgvPurchases.GridColor = Color.FromArgb(220, 230, 241);
            dgvPurchases.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Header style
            dgvPurchases.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(46, 64, 87);
            dgvPurchases.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPurchases.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgvPurchases.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Alternate / selection colours
            dgvPurchases.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(242, 247, 255);
            dgvPurchases.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvPurchases.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvPurchases.Columns.Clear();
            dgvPurchases.Columns.AddRange(
                Col("Id", "#", 55, false, "N0", DataGridViewContentAlignment.MiddleCenter),
                Col("InvoiceNumber", "Invoice #", 120, false, null, DataGridViewContentAlignment.MiddleLeft),
                Col("SupplierName", "Supplier", 180, false, null, DataGridViewContentAlignment.MiddleLeft),
                Col("PurchaseDate", "Date", 130, false, "dd-MMM-yyyy", DataGridViewContentAlignment.MiddleCenter),
                Col("NetAmount", "Net Amount", 120, false, "N2", DataGridViewContentAlignment.MiddleRight),
                Col("TotalPaid", "Total Paid", 120, false, "N2", DataGridViewContentAlignment.MiddleRight),
                Col("Balance", "Balance", 120, false, "N2", DataGridViewContentAlignment.MiddleRight),
                Col("PaymentStatus", "Status", 135, false, null, DataGridViewContentAlignment.MiddleCenter),
                Col("SupplierReferenceNo", "Supplier Ref", 130, false, null, DataGridViewContentAlignment.MiddleLeft),
                Col("Notes", "Notes", 200, true, null, DataGridViewContentAlignment.MiddleLeft)
            );

            dgvPurchases.CellFormatting += DgvPurchases_CellFormatting;
        }

        private static DataGridViewTextBoxColumn Col(
            string dataField, string header, int width,
            bool autoExpand, string format,
            DataGridViewContentAlignment align)
        {
            var c = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataField,
                HeaderText = header,
                Width = width,
                AutoSizeMode = autoExpand
                                   ? DataGridViewAutoSizeColumnMode.Fill
                                   : DataGridViewAutoSizeColumnMode.None,
                DefaultCellStyle = { Alignment = align }
            };
            if (!string.IsNullOrEmpty(format))
                c.DefaultCellStyle.Format = format;
            return c;
        }

        private void DgvPurchases_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvPurchases.Columns[e.ColumnIndex].DataPropertyName != "PaymentStatus") return;
            if (e.Value == null) return;

            switch (e.Value.ToString())
            {
                case "Pending":
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.FromArgb(133, 100, 4);
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                    break;
                case "Partially Paid":
                    e.CellStyle.BackColor = Color.FromArgb(204, 229, 255);
                    e.CellStyle.ForeColor = Color.FromArgb(0, 64, 133);
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                    break;
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        //  DATA LOADING
        // ─────────────────────────────────────────────────────────────────────

        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblStatus.Text = "Loading…";
                Application.DoEvents();

                string sql = @"
                    SELECT
                        p.Id,
                        p.InvoiceNumber,
                        p.SupplierReferenceNo,
                        p.PurchaseDate,
                        p.SupplierId,
                        s.SupplierName,
                        p.NetAmount,
                        p.TotalPaid,
                        p.Balance,
                        CASE p.PaymentStatus
                            WHEN 0 THEN 'Pending'
                            WHEN 1 THEN 'Partially Paid'
                        END AS PaymentStatus,
                        p.Notes
                    FROM Purchases p
                    INNER JOIN Suppliers s ON s.Id = p.SupplierId
                    WHERE p.IsDeleted     = 0
                      AND p.PaymentStatus != 2
                    ORDER BY p.PurchaseDate DESC";

                _sourceData = new DataTable();
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    da.Fill(_sourceData);
                }

                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data:\n{ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ApplyFilter()
        {
            string search = txtSearch.Text.Trim().ToLowerInvariant();
            string status = cmbStatusFilter.SelectedItem?.ToString() ?? "All";

            IEnumerable<DataRow> rows = _sourceData.AsEnumerable();

            if (status != "All")
                rows = rows.Where(r => r["PaymentStatus"].ToString() == status);

            if (!string.IsNullOrEmpty(search))
                rows = rows.Where(r =>
                    r["InvoiceNumber"].ToString().ToLowerInvariant().Contains(search) ||
                    r["SupplierName"].ToString().ToLowerInvariant().Contains(search) ||
                    r["SupplierReferenceNo"].ToString().ToLowerInvariant().Contains(search));

            _filteredData = rows.Any()
                ? rows.CopyToDataTable()
                : _sourceData.Clone();

            dgvPurchases.DataSource = _filteredData;

            decimal totalBalance = _filteredData.AsEnumerable()
                .Sum(r => r.Field<decimal>("Balance"));
            int total = _filteredData.Rows.Count;
            int pending = _filteredData.AsEnumerable().Count(r => r["PaymentStatus"].ToString() == "Pending");
            int partial = _filteredData.AsEnumerable().Count(r => r["PaymentStatus"].ToString() == "Partially Paid");

            lblStatus.Text =
                $"Showing {total} record(s)  |  Pending: {pending}  |  Partial: {partial}  |  " +
                $"Total Outstanding: {totalBalance:N2}";
        }

        // ─────────────────────────────────────────────────────────────────────
        //  FILTER / SEARCH EVENTS
        // ─────────────────────────────────────────────────────────────────────

        private void txtSearch_TextChanged(object sender, EventArgs e) => ApplyFilter();

        private void cmbStatusFilter_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilter();

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();

        // ─────────────────────────────────────────────────────────────────────
        //  SELECT ALL / DESELECT
        // ─────────────────────────────────────────────────────────────────────

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            dgvPurchases.SelectAll();
            dgvPurchases.Focus();
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            dgvPurchases.ClearSelection();
        }

        // ─────────────────────────────────────────────────────────────────────
        //  EXPORT
        // ─────────────────────────────────────────────────────────────────────

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<long> idsToExport;

            if (dgvPurchases.SelectedRows.Count > 0)
            {
                // Export only the selected rows
                idsToExport = dgvPurchases.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(r => Convert.ToInt64(r.Cells[0].Value))
                    .Distinct()
                    .ToList();
            }
            else
            {
                // Nothing selected → ask whether to export all visible rows
                var answer = MessageBox.Show(
                    "No rows are selected.\n\nDo you want to export ALL visible records?",
                    "Export", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (answer != DialogResult.Yes) return;

                idsToExport = _filteredData.AsEnumerable()
                    .Select(r => Convert.ToInt64(r["Id"]))
                    .ToList();
            }

            if (idsToExport.Count == 0)
            {
                MessageBox.Show("Nothing to export.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Title = "Save Purchase Export",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"PurchaseExport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx",
                DefaultExt = "xlsx"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    Cursor = Cursors.WaitCursor;
                    btnExport.Enabled = false;
                    lblStatus.Text = $"Exporting {idsToExport.Count} purchase(s)…";
                    Application.DoEvents();

                    string saved = PurchaseExportService.Export(
                        _connectionString, idsToExport, sfd.FileName);

                    lblStatus.Text = $"Export complete → {saved}";

                    var open = MessageBox.Show(
                        $"Export saved successfully:\n{saved}\n\nOpen file now?",
                        "Export Complete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (open == DialogResult.Yes)
                        Process.Start(new ProcessStartInfo(saved) { UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed:\n{ex.Message}",
                        "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblStatus.Text = "Export failed.";
                }
                finally
                {
                    Cursor = Cursors.Default;
                    btnExport.Enabled = true;
                }
            }
        }
    }
}



/// <summary>
/// Exports one or more Purchase records — with all related data — into a
/// structured, import-ready Excel workbook.
///
/// Sheet layout
/// ────────────
///   Sheet 1 – Import Instructions  (step-by-step re-import guide)
///   Sheet 2 – Summary              (totals and counts)
///   Sheet 3 – Purchases            (one row per invoice)
///   Sheet 4 – Purchase Items       (line items; linked by InvoiceNumber)
///   Sheet 5 – Supplier Payments    (payment runs; linked by PaymentNumber)
///   Sheet 6 – Payment Allocations  (allocations; linked by InvoiceNumber + PaymentNumber)
/// </summary>
public static class PurchaseExportService
{
    // ── colour palette ────────────────────────────────────────────────────
    private static readonly XLColor HeaderBg = XLColor.FromHtml("#2E4057");
    private static readonly XLColor HeaderFg = XLColor.White;
    private static readonly XLColor AltRowBg = XLColor.FromHtml("#F2F7FF");
    private static readonly XLColor PendingBg = XLColor.FromHtml("#FFF3CD");
    private static readonly XLColor PartialBg = XLColor.FromHtml("#CCE5FF");
    private static readonly XLColor SummaryBg = XLColor.FromHtml("#E8F4FD");
    private static readonly XLColor BorderColor = XLColor.FromHtml("#BDD7EE");

    // ─────────────────────────────────────────────────────────────────────
    //  PUBLIC ENTRY POINT
    // ─────────────────────────────────────────────────────────────────────

    /// <summary>
    /// Builds and saves the workbook. Returns the saved file path.
    /// </summary>
    /// <param name="connectionString">SQL Server connection string.</param>
    /// <param name="purchaseIds">IDs to export. Pass null to export ALL non-paid purchases.</param>
    /// <param name="outputPath">Full path including filename. If null, saves to Desktop.</param>
    public static string Export(
        string connectionString,
        IEnumerable<long> purchaseIds = null,
        string outputPath = null)
    {
        if (string.IsNullOrWhiteSpace(outputPath))
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            outputPath = Path.Combine(desktop,
                $"PurchaseExport_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
        }

        var idList = purchaseIds?.ToList();

        DataTable dtPurchases = LoadPurchases(connectionString, idList);
        DataTable dtItems = LoadPurchaseItems(connectionString, idList);
        DataTable dtPayments = LoadSupplierPayments(connectionString, idList);
        DataTable dtPaymentDetails = LoadPaymentDetails(connectionString, idList);

        using (var wb = new XLWorkbook())
        {
            BuildInstructionsSheet(wb);
            BuildSummarySheet(wb, dtPurchases);
            BuildPurchasesSheet(wb, dtPurchases);
            BuildItemsSheet(wb, dtItems);
            BuildPaymentsSheet(wb, dtPayments);
            BuildPaymentDetailsSheet(wb, dtPaymentDetails);

            wb.SaveAs(outputPath);
        }

        return outputPath;
    }

    // ─────────────────────────────────────────────────────────────────────
    //  DATA LOADERS
    // ─────────────────────────────────────────────────────────────────────

    private static DataTable LoadPurchases(string cs, List<long> ids)
    {
        string filter = BuildIdFilter("p.Id", ids);
        string sql = $@"
                SELECT
                    p.Id,
                    p.InvoiceNumber,
                    p.SupplierReferenceNo,
                    p.PurchaseDate,
                    p.SupplierId,
                    s.SupplierName,
                    s.ContactNo         AS SupplierPhone,
                    p.TotalAmount,
                    p.Discount,
                    p.NetAmount,
                    p.TotalPaid,
                    p.Balance,
                    CASE p.PaymentStatus
                        WHEN 0 THEN 'Pending'
                        WHEN 1 THEN 'Partially Paid'
                        WHEN 2 THEN 'Paid'
                        ELSE 'Unknown'
                    END             AS PaymentStatus,
                    p.Notes,
                    p.CreatedAt,
                    p.UpdatedAt
                FROM Purchases p
                INNER JOIN Suppliers s ON s.Id = p.SupplierId
                WHERE p.IsDeleted = 0
                  {filter}
                ORDER BY p.PurchaseDate DESC";

        return ExecuteQuery(cs, sql);
    }

    private static DataTable LoadPurchaseItems(string cs, List<long> ids)
    {
        string filter = BuildIdFilter("pi.PurchaseId", ids);
        string sql = $@"
                SELECT
                    pi.Id,
                    pi.PurchaseId,
                    p.InvoiceNumber,
                    pi.ProductId,
                    pr.ProductEnglishName         AS ProductName,
                    pi.ProductUnitType,
                    pi.Quantity,
                    pi.PurchasePrice,
                    pi.TotalPrice
                FROM PurchaseItems pi
                INNER JOIN Purchases p  ON p.Id  = pi.PurchaseId
                INNER JOIN Products  pr ON pr.Id = pi.ProductId
                WHERE pi.IsDeleted = 0
                  AND p.IsDeleted  = 0
                  {filter}
                ORDER BY pi.PurchaseId, pi.Id";

        return ExecuteQuery(cs, sql);
    }

    private static DataTable LoadSupplierPayments(string cs, List<long> ids)
    {
        string filter = ids != null && ids.Count > 0
            ? $@"AND sp.Id IN (
                        SELECT DISTINCT spd.SupplierPaymentId
                        FROM SupplierPaymentDetails spd
                        WHERE spd.PurchaseId IN ({string.Join(",", ids)}))"
            : string.Empty;

        string sql = $@"
                SELECT
                    sp.Id,
                    sp.PaymentNumber,
                    sp.SupplierId,
                    s.SupplierName,
                    sp.PaymentDate,
                    sp.TotalAmountPaid,
                    sp.TotalAllocated,
                    CASE sp.PaymentMethod
                        WHEN 0 THEN 'Cash'
                        WHEN 1 THEN 'Bank Transfer'
                        WHEN 2 THEN 'Cheque'
                        ELSE 'Other'
                    END             AS PaymentMethod,
                    sp.TransactionReference,
                    sp.Notes,
                    sp.CreatedAt
                FROM SupplierPayments sp
                INNER JOIN Suppliers s ON s.Id = sp.SupplierId
                WHERE sp.IsDeleted = 0
                  {filter}
                ORDER BY sp.PaymentDate DESC";

        return ExecuteQuery(cs, sql);
    }

    private static DataTable LoadPaymentDetails(string cs, List<long> ids)
    {
        string filter = BuildIdFilter("spd.PurchaseId", ids);
        string sql = $@"
                SELECT
                    spd.Id,
                    spd.SupplierPaymentId,
                    sp.PaymentNumber,
                    spd.PurchaseId,
                    p.InvoiceNumber,
                    s.SupplierName,
                    spd.AmountAllocated,
                    spd.CreatedAt
                FROM SupplierPaymentDetails spd
                INNER JOIN SupplierPayments sp ON sp.Id = spd.SupplierPaymentId
                INNER JOIN Purchases p         ON p.Id  = spd.PurchaseId
                INNER JOIN Suppliers s         ON s.Id  = p.SupplierId
                WHERE sp.IsDeleted = 0
                  AND p.IsDeleted  = 0
                  {filter}
                ORDER BY spd.PurchaseId, spd.SupplierPaymentId";

        return ExecuteQuery(cs, sql);
    }

    // ─────────────────────────────────────────────────────────────────────
    //  SHEET BUILDERS
    // ─────────────────────────────────────────────────────────────────────

    private static void BuildInstructionsSheet(XLWorkbook wb)
    {
        var ws = wb.Worksheets.Add("Import Instructions");

        // Title
        ws.Cell("B2").Value = "HOW TO RE-IMPORT THIS FILE";
        ws.Cell("B2").Style.Font.Bold = true;
        ws.Cell("B2").Style.Font.FontSize = 16;
        ws.Cell("B2").Style.Font.FontColor = XLColor.FromHtml("#2E4057");

        ws.Cell("B3").Value = "Follow the steps below IN ORDER to avoid foreign key conflicts.";
        ws.Cell("B3").Style.Font.Italic = true;
        ws.Cell("B3").Style.Font.FontColor = XLColor.Red;

        ws.Cell("B4").Value = "All sheets use natural business keys (InvoiceNumber, PaymentNumber) — NOT auto-increment IDs.";
        ws.Cell("B4").Style.Font.Italic = true;
        ws.Cell("B4").Style.Font.FontColor = XLColor.FromHtml("#155724");

        var steps = new[]
        {
                new
                {
                    Step   = "STEP 1",
                    Sheet  = "Purchases",
                    Action = "Import this sheet first. The database will generate new IDs automatically.",
                    Key    = "InvoiceNumber is the unique business key. Store old→new ID mapping."
                },
                new
                {
                    Step   = "STEP 2",
                    Sheet  = "Purchase Items",
                    Action = "Import after Purchases. Match InvoiceNumber to resolve the correct new PurchaseId.",
                    Key    = "Link column: InvoiceNumber → Purchases.InvoiceNumber"
                },
                new
                {
                    Step   = "STEP 3",
                    Sheet  = "Supplier Payments",
                    Action = "Import after Purchases. DB generates new payment IDs automatically.",
                    Key    = "PaymentNumber is the unique business key. Store old→new ID mapping."
                },
                new
                {
                    Step   = "STEP 4",
                    Sheet  = "Payment Allocations",
                    Action = "Import last. Resolve PaymentNumber and InvoiceNumber to get the new FK IDs.",
                    Key    = "PaymentNumber → SupplierPayments | InvoiceNumber → Purchases"
                },
            };

        int row = 7;
        // Header row
        StyleCell(ws.Cell(row, 2), "Step", true, HeaderFg, HeaderBg);
        StyleCell(ws.Cell(row, 3), "Sheet", true, HeaderFg, HeaderBg);
        StyleCell(ws.Cell(row, 4), "Action", true, HeaderFg, HeaderBg);
        StyleCell(ws.Cell(row, 5), "Linking Key", true, HeaderFg, HeaderBg);

        foreach (var s in steps)
        {
            row++;
            XLColor bg = row % 2 == 0 ? AltRowBg : XLColor.White;

            ws.Cell(row, 2).Value = s.Step;
            ws.Cell(row, 2).Style.Font.Bold = true;
            ws.Cell(row, 2).Style.Font.FontColor = XLColor.FromHtml("#721C24");
            ws.Cell(row, 2).Style.Fill.BackgroundColor = bg;

            ws.Cell(row, 3).Value = s.Sheet;
            ws.Cell(row, 3).Style.Font.Bold = true;
            ws.Cell(row, 3).Style.Fill.BackgroundColor = bg;

            ws.Cell(row, 4).Value = s.Action;
            ws.Cell(row, 4).Style.Fill.BackgroundColor = bg;

            ws.Cell(row, 5).Value = s.Key;
            ws.Cell(row, 5).Style.Font.Italic = true;
            ws.Cell(row, 5).Style.Font.FontColor = XLColor.FromHtml("#004085");
            ws.Cell(row, 5).Style.Fill.BackgroundColor = bg;

            foreach (int c in new[] { 2, 3, 4, 5 })
            {
                ws.Cell(row, c).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                ws.Cell(row, c).Style.Border.BottomBorderColor = BorderColor;
            }
        }

        ws.Column(2).Width = 12;
        ws.Column(3).Width = 25;
        ws.Column(4).Width = 60;
        ws.Column(5).Width = 55;

        ws.Position = 1;
    }

    private static void BuildSummarySheet(XLWorkbook wb, DataTable dtPurchases)
    {
        var ws = wb.Worksheets.Add("Summary");

        // Title block
        ws.Cell("B2").Value = "Purchase Export Report";
        ws.Cell("B2").Style.Font.Bold = true;
        ws.Cell("B2").Style.Font.FontSize = 18;
        ws.Cell("B2").Style.Font.FontColor = XLColor.FromHtml("#2E4057");

        ws.Cell("B3").Value = $"Generated: {DateTime.Now:dd-MMM-yyyy HH:mm}";
        ws.Cell("B3").Style.Font.Italic = true;
        ws.Cell("B3").Style.Font.FontColor = XLColor.Gray;

        ws.Cell("B4").Value = $"Total Purchases in export: {dtPurchases.Rows.Count}";

        decimal totalNet = dtPurchases.AsEnumerable().Sum(r => r.Field<decimal>("NetAmount"));
        decimal totalPaid = dtPurchases.AsEnumerable().Sum(r => r.Field<decimal>("TotalPaid"));
        decimal totalBalance = dtPurchases.AsEnumerable().Sum(r => r.Field<decimal>("Balance"));

        int countPending = dtPurchases.AsEnumerable().Count(r => r["PaymentStatus"].ToString() == "Pending");
        int countPartial = dtPurchases.AsEnumerable().Count(r => r["PaymentStatus"].ToString() == "Partially Paid");
        int countPaid = dtPurchases.AsEnumerable().Count(r => r["PaymentStatus"].ToString() == "Paid");

        var summaryData = new[]
        {
                new { Label = "Total Invoice Value (Net)",  Value = totalNet.ToString("N2"),     FgColor = XLColor.Black },
                new { Label = "Total Amount Paid",          Value = totalPaid.ToString("N2"),    FgColor = XLColor.FromHtml("#155724") },
                new { Label = "Total Outstanding Balance",  Value = totalBalance.ToString("N2"), FgColor = XLColor.FromHtml("#721C24") },
                new { Label = "Pending Invoices",           Value = countPending.ToString(),     FgColor = XLColor.FromHtml("#856404") },
                new { Label = "Partially Paid Invoices",    Value = countPartial.ToString(),     FgColor = XLColor.FromHtml("#004085") },
                new { Label = "Fully Paid Invoices",        Value = countPaid.ToString(),        FgColor = XLColor.FromHtml("#155724") },
            };

        int row = 7;
        StyleCell(ws.Cell(row, 2), "Metric", true, HeaderFg, HeaderBg);
        StyleCell(ws.Cell(row, 3), "Value", true, HeaderFg, HeaderBg);
        ws.Cell(row, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

        foreach (var item in summaryData)
        {
            row++;
            ws.Cell(row, 2).Value = item.Label;
            ws.Cell(row, 2).Style.Fill.BackgroundColor = SummaryBg;
            ws.Cell(row, 2).Style.Font.Bold = true;

            ws.Cell(row, 3).Value = item.Value;
            ws.Cell(row, 3).Style.Font.FontColor = item.FgColor;
            ws.Cell(row, 3).Style.Fill.BackgroundColor = SummaryBg;
            ws.Cell(row, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            ws.Range(row, 2, row, 3).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            ws.Range(row, 2, row, 3).Style.Border.BottomBorderColor = BorderColor;
        }

        ws.Column(2).Width = 35;
        ws.Column(3).Width = 20;
    }

    private static void BuildPurchasesSheet(XLWorkbook wb, DataTable dt)
    {
        var ws = wb.Worksheets.Add("Purchases");

        string[] headers = {
                "Invoice Number",       // natural key — import uses this
                "Supplier Name",        // match by name on import
                "Supplier Ref No",
                "Purchase Date",
                "Supplier Phone",
                "Total Amount",
                "Discount",
                "Net Amount",
                "Total Paid",
                "Balance",
                "Payment Status",
                "Notes",
                "Created At"
                // Raw IDs (Id, SupplierId) excluded — not needed for re-import
            };

        WriteHeaders(ws, headers);

        int row = 2;
        foreach (DataRow dr in dt.Rows)
        {
            string status = dr["PaymentStatus"].ToString();
            XLColor rowBg = status == "Pending" ? PendingBg
                          : status == "Partially Paid" ? PartialBg
                          : (row % 2 == 0) ? AltRowBg
                          : XLColor.White;

            int col = 1;
            WriteCell(ws, row, col++, dr["InvoiceNumber"], rowBg);
            WriteCell(ws, row, col++, dr["SupplierName"], rowBg);
            WriteCell(ws, row, col++, dr["SupplierReferenceNo"], rowBg);
            WriteDateCell(ws, row, col++, dr["PurchaseDate"], rowBg);
            WriteCell(ws, row, col++, dr["SupplierPhone"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["TotalAmount"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["Discount"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["NetAmount"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["TotalPaid"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["Balance"], rowBg);

            // Status cell with colour
            var statusCell = ws.Cell(row, col++);
            statusCell.Value = status;
            statusCell.Style.Fill.BackgroundColor =
                status == "Pending" ? XLColor.FromHtml("#FFC107") :
                status == "Partially Paid" ? XLColor.FromHtml("#17A2B8") :
                                             XLColor.FromHtml("#28A745");
            statusCell.Style.Font.FontColor = XLColor.White;
            statusCell.Style.Font.Bold = true;
            statusCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            WriteCell(ws, row, col++, dr["Notes"], rowBg);
            WriteDateCell(ws, row, col++, dr["CreatedAt"], rowBg);

            row++;
        }

        AutoFitAndFreeze(ws, headers.Length, row - 1);
        AddTotalsRow(ws, row, new[] { 6, 7, 8, 9, 10 }, headers.Length);
    }

    private static void BuildItemsSheet(XLWorkbook wb, DataTable dt)
    {
        var ws = wb.Worksheets.Add("Purchase Items");

        string[] headers = {
                "Invoice Number",       // FK → Purchases.InvoiceNumber (natural key)
                "Product Name",         // match by name on import
                "Unit Type",
                "Quantity",
                "Purchase Price",
                "Total Price"
                // Raw IDs excluded
            };

        WriteHeaders(ws, headers);

        int row = 2;
        foreach (DataRow dr in dt.Rows)
        {
            XLColor rowBg = row % 2 == 0 ? AltRowBg : XLColor.White;
            int col = 1;
            WriteCell(ws, row, col++, dr["InvoiceNumber"], rowBg);
            WriteCell(ws, row, col++, dr["ProductName"], rowBg);
            WriteCell(ws, row, col++, dr["ProductUnitType"], rowBg);

            var qtyCell = ws.Cell(row, col++);
            qtyCell.Value = Convert.ToDecimal(dr["Quantity"]);
            qtyCell.Style.Fill.BackgroundColor = rowBg;
            qtyCell.Style.NumberFormat.Format = "#,##0.00";
            qtyCell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
            qtyCell.Style.Border.BottomBorderColor = BorderColor;

            WriteCurrencyCell(ws, row, col++, dr["PurchasePrice"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["TotalPrice"], rowBg);

            row++;
        }

        AutoFitAndFreeze(ws, headers.Length, row - 1);
        AddTotalsRow(ws, row, new[] { 5, 6 }, headers.Length);
    }

    private static void BuildPaymentsSheet(XLWorkbook wb, DataTable dt)
    {
        var ws = wb.Worksheets.Add("Supplier Payments");

        string[] headers = {
                "Payment Number",       // natural key
                "Supplier Name",        // match by name on import
                "Payment Date",
                "Total Amount Paid",
                "Total Allocated",
                "Payment Method",
                "Transaction Reference",
                "Notes",
                "Created At"
                // Raw IDs excluded
            };

        WriteHeaders(ws, headers);

        int row = 2;
        foreach (DataRow dr in dt.Rows)
        {
            XLColor rowBg = row % 2 == 0 ? AltRowBg : XLColor.White;
            int col = 1;
            WriteCell(ws, row, col++, dr["PaymentNumber"], rowBg);
            WriteCell(ws, row, col++, dr["SupplierName"], rowBg);
            WriteDateCell(ws, row, col++, dr["PaymentDate"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["TotalAmountPaid"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["TotalAllocated"], rowBg);
            WriteCell(ws, row, col++, dr["PaymentMethod"], rowBg);
            WriteCell(ws, row, col++, dr["TransactionReference"], rowBg);
            WriteCell(ws, row, col++, dr["Notes"], rowBg);
            WriteDateCell(ws, row, col++, dr["CreatedAt"], rowBg);

            row++;
        }

        AutoFitAndFreeze(ws, headers.Length, row - 1);
        AddTotalsRow(ws, row, new[] { 4, 5 }, headers.Length);
    }

    private static void BuildPaymentDetailsSheet(XLWorkbook wb, DataTable dt)
    {
        var ws = wb.Worksheets.Add("Payment Allocations");

        string[] headers = {
                "Payment Number",       // FK → SupplierPayments.PaymentNumber (natural key)
                "Invoice Number",       // FK → Purchases.InvoiceNumber (natural key)
                "Supplier Name",
                "Amount Allocated",
                "Created At"
                // Raw IDs excluded — resolved via natural keys on import
            };

        WriteHeaders(ws, headers);

        int row = 2;
        foreach (DataRow dr in dt.Rows)
        {
            XLColor rowBg = row % 2 == 0 ? AltRowBg : XLColor.White;
            int col = 1;
            WriteCell(ws, row, col++, dr["PaymentNumber"], rowBg);
            WriteCell(ws, row, col++, dr["InvoiceNumber"], rowBg);
            WriteCell(ws, row, col++, dr["SupplierName"], rowBg);
            WriteCurrencyCell(ws, row, col++, dr["AmountAllocated"], rowBg);
            WriteDateCell(ws, row, col++, dr["CreatedAt"], rowBg);

            row++;
        }

        AutoFitAndFreeze(ws, headers.Length, row - 1);
        AddTotalsRow(ws, row, new[] { 4 }, headers.Length);
    }

    // ─────────────────────────────────────────────────────────────────────
    //  STYLE HELPERS
    // ─────────────────────────────────────────────────────────────────────

    private static void WriteHeaders(IXLWorksheet ws, string[] headers)
    {
        for (int i = 0; i < headers.Length; i++)
        {
            var cell = ws.Cell(1, i + 1);
            cell.Value = headers[i];
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = HeaderFg;
            cell.Style.Fill.BackgroundColor = HeaderBg;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            cell.Style.Border.BottomBorderColor = XLColor.White;
        }
    }

    private static void WriteCell(IXLWorksheet ws, int row, int col, object value, XLColor bg)
    {
        var cell = ws.Cell(row, col);
        cell.Value = value?.ToString() ?? string.Empty;
        cell.Style.Fill.BackgroundColor = bg;
        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        cell.Style.Border.BottomBorderColor = BorderColor;
    }

    private static void WriteCurrencyCell(IXLWorksheet ws, int row, int col, object value, XLColor bg)
    {
        var cell = ws.Cell(row, col);
        if (decimal.TryParse(value?.ToString(), out decimal d))
        {
            cell.Value = d;
            cell.Style.NumberFormat.Format = "#,##0.00";
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
        }
        else
        {
            cell.Value = value?.ToString() ?? string.Empty;
        }
        cell.Style.Fill.BackgroundColor = bg;
        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        cell.Style.Border.BottomBorderColor = BorderColor;
    }

    private static void WriteDateCell(IXLWorksheet ws, int row, int col, object value, XLColor bg)
    {
        var cell = ws.Cell(row, col);
        if (value != DBNull.Value && value != null &&
            DateTime.TryParse(value.ToString(), out DateTime dt))
        {
            cell.Value = dt;
            cell.Style.NumberFormat.Format = "dd-MMM-yyyy HH:mm";
        }
        else
        {
            cell.Value = string.Empty;
        }
        cell.Style.Fill.BackgroundColor = bg;
        cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        cell.Style.Border.BottomBorderColor = BorderColor;
    }

    private static void StyleCell(IXLCell cell, string value, bool bold, XLColor fg, XLColor bg)
    {
        cell.Value = value;
        cell.Style.Font.Bold = bold;
        cell.Style.Font.FontColor = fg;
        cell.Style.Fill.BackgroundColor = bg;
        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }

    private static void AutoFitAndFreeze(IXLWorksheet ws, int colCount, int lastDataRow)
    {
        ws.SheetView.FreezeRows(1);

        for (int c = 1; c <= colCount; c++)
        {
            ws.Column(c).AdjustToContents(1, lastDataRow + 1);
            if (ws.Column(c).Width < 12) ws.Column(c).Width = 12;
            if (ws.Column(c).Width > 45) ws.Column(c).Width = 45;
        }

        if (lastDataRow >= 2)
        {
            ws.Range(1, 1, lastDataRow, colCount)
              .Style.Border.OutsideBorder = XLBorderStyleValues.Medium;
        }
    }

    private static void AddTotalsRow(IXLWorksheet ws, int row, int[] currencyCols, int totalCols)
    {
        ws.Cell(row, 1).Value = "TOTAL";
        ws.Cell(row, 1).Style.Font.Bold = true;
        ws.Cell(row, 1).Style.Fill.BackgroundColor = HeaderBg;
        ws.Cell(row, 1).Style.Font.FontColor = HeaderFg;

        for (int c = 2; c <= totalCols; c++)
            ws.Cell(row, c).Style.Fill.BackgroundColor = HeaderBg;

        foreach (int col in currencyCols)
        {
            string colLetter = ws.Column(col).ColumnLetter();
            ws.Cell(row, col).FormulaA1 = $"=SUM({colLetter}2:{colLetter}{row - 1})";
            ws.Cell(row, col).Style.Font.Bold = true;
            ws.Cell(row, col).Style.Fill.BackgroundColor = HeaderBg;
            ws.Cell(row, col).Style.Font.FontColor = HeaderFg;
            ws.Cell(row, col).Style.NumberFormat.Format = "#,##0.00";
        }
    }

    // ─────────────────────────────────────────────────────────────────────
    //  DB HELPERS
    // ─────────────────────────────────────────────────────────────────────

    private static string BuildIdFilter(string column, List<long> ids)
    {
        if (ids == null || ids.Count == 0) return string.Empty;
        return $"AND {column} IN ({string.Join(",", ids)})";
    }

    private static DataTable ExecuteQuery(string cs, string sql)
    {
        var dt = new DataTable();
        using (var conn = new SqlConnection(cs))
        using (var cmd = new SqlCommand(sql, conn))
        using (var da = new SqlDataAdapter(cmd))
        {
            conn.Open();
            da.Fill(dt);
        }
        return dt;
    }
}
