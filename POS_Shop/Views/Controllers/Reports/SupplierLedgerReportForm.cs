using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Reports
{
    public partial class SupplierLedgerReportForm : Form
    {
        private readonly POSDbContext _db;
        private readonly Timer _supTimer = new Timer { Interval = 300 };
        private int? _supId;
        private bool _suppress;

        public SupplierLedgerReportForm() { InitializeComponent(); _db = new POSDbContext(); dtpFrom.Value = DateTime.Today.AddMonths(-6); dtpTo.Value = DateTime.Today; WireEvents(); }

        private void WireEvents()
        {
            btnRun.Click += (s, e) => RunReport();
            btnPrint.Click += (s, e) => ReportBase.PrintGrid(dgvReport, lblBar.Text, this);
            btnClose.Click += (s, e) => Close();
            lstSupSugg.Leave += (s, e) => { if (!txtSup.Focused) HideSugg(); };
            txtSup.TextChanged += TxtSup_Changed;
            txtSup.KeyDown += TxtSup_KeyDown;
            txtSup.Leave += (s, e) => { if (!lstSupSugg.Focused) HideSugg(); };
            lstSupSugg.MouseClick += (s, e) => SelectSup();
            lstSupSugg.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { SelectSup(); e.Handled = true; } };
            lstSupSugg.DrawItem += DrawSupItem;
            _supTimer.Tick += (s, e) => { _supTimer.Stop(); SearchSup(txtSup.Text.Trim()); };
            btnClrSup.Click += (s, e) => ClearSup();
            this.Resize += (s, e) => ReposSugg();
            dgvReport.CellClick += DgvReport_CellClick;
            dgvReport.CellFormatting += DgvReport_CellFormatting;
            dgvReport.CellMouseEnter += (s, e) => { if (e.RowIndex >= 0 && dgvReport.Columns[e.ColumnIndex].Name == "colLRef" && dgvReport.Rows[e.RowIndex].Tag is int) dgvReport.Cursor = Cursors.Hand; };
            dgvReport.CellMouseLeave += (s, e) => dgvReport.Cursor = Cursors.Default;
            ReportBase.Hover(btnRun, Color.FromArgb(27, 94, 32), ReportBase.Green);
            ReportBase.Hover(btnPrint, Color.FromArgb(55, 71, 79), Color.FromArgb(80, 100, 110));
            ReportBase.Hover(btnClose, Color.FromArgb(140, 20, 20), ReportBase.Red);
            KeyPreview = true; KeyDown += (s, e) => { if (e.KeyCode == Keys.Escape) Close(); };
        }

        // ── Supplier search ────────────────────────────────────────────────────
        private void TxtSup_Changed(object s, EventArgs e) { if (_suppress) return; if (_supId.HasValue) ClearSup(); _supTimer.Stop(); _supTimer.Start(); }
        private void TxtSup_KeyDown(object s, KeyEventArgs e) { if (!lstSupSugg.Visible) return; if (e.KeyCode == Keys.Down) { lstSupSugg.Focus(); if (lstSupSugg.Items.Count > 0) lstSupSugg.SelectedIndex = 0; e.Handled = true; } else if (e.KeyCode == Keys.Escape) { HideSugg(); e.Handled = true; } }
        private void SearchSup(string q) { if (q.Length < 1) { HideSugg(); return; } var list = _db.Suppliers.Where(s => !s.IsDeleted && (s.SupplierName.Contains(q) || s.ShopName.Contains(q))).Take(8).ToList(); lstSupSugg.DataSource = list.Count > 0 ? (object)list : null; lstSupSugg.DisplayMember = "SupplierName"; lstSupSugg.ValueMember = "Id"; ShowSugg(list.Count); }
        private void SelectSup() { if (!(lstSupSugg.SelectedItem is Models.Supplier s)) return; _supId = s.Id; _suppress = true; txtSup.Text = ""; _suppress = false; lblSelSup.Text = $"{s.SupplierName}  —  {s.ShopName}"; pnlSupBadge.Visible = true; HideSugg(); }
        private void ClearSup() { _supId = null; pnlSupBadge.Visible = false; }
        private void ShowSugg(int n) { if (n == 0) { HideSugg(); return; } var pt = PointToClient(txtSup.Parent.PointToScreen(txtSup.Location)); lstSupSugg.Location = new Point(pt.X, pt.Y + txtSup.Height); lstSupSugg.Width = txtSup.Width; lstSupSugg.Height = Math.Min(n, 6) * lstSupSugg.ItemHeight + 2; lstSupSugg.BringToFront(); lstSupSugg.Visible = true; }
        private void HideSugg() { lstSupSugg.Visible = false; lstSupSugg.DataSource = null; }
        private void ReposSugg() { if (lstSupSugg.Visible) ShowSugg(lstSupSugg.Items.Count); }
        private void DrawSupItem(object s, DrawItemEventArgs e) { if (e.Index < 0) return; bool sel = (e.State & DrawItemState.Selected) == DrawItemState.Selected; e.Graphics.FillRectangle(sel ? new SolidBrush(Color.FromArgb(232, 245, 233)) : Brushes.White, e.Bounds); if (e.Index > 0) e.Graphics.DrawLine(new Pen(Color.FromArgb(236, 239, 241)), e.Bounds.Left, e.Bounds.Top, e.Bounds.Right, e.Bounds.Top); if (lstSupSugg.Items[e.Index] is Models.Supplier sup) { e.Graphics.DrawString(sup.SupplierName, new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold), new SolidBrush(Color.FromArgb(27, 94, 32)), e.Bounds.Left + 8, e.Bounds.Top + 4); e.Graphics.DrawString(sup.ShopName, new System.Drawing.Font("Segoe UI", 8.5f), new SolidBrush(Color.FromArgb(120, 144, 156)), e.Bounds.Left + 8, e.Bounds.Top + 22); } e.DrawFocusRectangle(); }

        // ── Run ────────────────────────────────────────────────────────────────
        private void RunReport()
        {
            if (!_supId.HasValue) { MessageBox.Show("Please select a supplier.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            try
            {
                DateTime from = dtpFrom.Value.Date, to = dtpTo.Value.Date.AddDays(1).AddTicks(-1); int sid = _supId.Value;
                var purchases = _db.Purchases.Where(p => p.SupplierId == sid && !p.IsDeleted && p.PurchaseDate >= from && p.PurchaseDate <= to).OrderBy(p => p.PurchaseDate).ThenBy(p => p.Id).ToList();
                var payments = _db.SupplierPayments.Where(p => p.SupplierId == sid && !p.IsDeleted && p.PaymentDate >= from && p.PaymentDate <= to).OrderBy(p => p.PaymentDate).ThenBy(p => p.Id).ToList();
                var ledger = new List<(DateTime Date, string Type, string Ref, decimal Debit, decimal Credit, int? Pid)>();
                foreach (var p in purchases) ledger.Add((p.PurchaseDate, "Invoice", p.InvoiceNumber, p.NetAmount, 0, p.Id));
                foreach (var p in payments) ledger.Add((p.PaymentDate, "Payment", p.PaymentNumber, 0, p.TotalAmountPaid, (int?)null));
                ledger = ledger.OrderBy(x => x.Date).ThenBy(x => x.Type).ToList();
                dgvReport.Rows.Clear();
                decimal bal = 0;
                foreach (var e in ledger)
                {
                    bal += e.Debit - e.Credit;
                    var row = dgvReport.Rows[dgvReport.Rows.Add()];
                    row.Tag = e.Pid;
                    row.Cells["colLDate"].Value = e.Date.ToString("dd MMM yyyy");
                    row.Cells["colLType"].Value = e.Type;
                    row.Cells["colLRef"].Value = e.Ref;
                    row.Cells["colLDebit"].Value = e.Debit > 0 ? e.Debit : (object)DBNull.Value;
                    row.Cells["colLCredit"].Value = e.Credit > 0 ? e.Credit : (object)DBNull.Value;
                    row.Cells["colLBalance"].Value = bal;
                    if (e.Type == "Payment") { row.Cells["colLCredit"].Style.ForeColor = ReportBase.Green; row.Cells["colLCredit"].Style.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold); }
                    else row.Cells["colLDebit"].Style.ForeColor = ReportBase.Red;
                    row.Cells["colLBalance"].Style.ForeColor = bal > 0 ? ReportBase.Red : ReportBase.Green;
                }
                var sup = _db.Suppliers.FirstOrDefault(x => x.Id == sid);
                lblBar.Text = $"  Supplier Ledger  ·  {(sup != null ? sup.SupplierName : "")}  ·  {from:dd MMM yyyy} → {dtpTo.Value.Date:dd MMM yyyy}  ·  Closing Balance: Rs. {bal:N2}";
                lblEmpty.Visible = ledger.Count == 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        // ── Grid click → PurchaseDetailForm ───────────────────────────────────
        private void DgvReport_CellClick(object s, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvReport.Columns[e.ColumnIndex].Name != "colLRef") return;
            if (dgvReport.Rows[e.RowIndex].Tag is int pid)
                using (var f = new POS_Shop.Views.Controllers.Supplier.PurchaseDetailForm(pid)) f.ShowDialog(this);
        }
        private void DgvReport_CellFormatting(object s, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvReport.Columns[e.ColumnIndex].Name != "colLRef" || !(dgvReport.Rows[e.RowIndex].Tag is int)) return;
            e.CellStyle.ForeColor = ReportBase.Blue;
            e.CellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Underline);
        }

        protected override void OnFormClosed(FormClosedEventArgs e) { base.OnFormClosed(e); _supTimer.Dispose(); _db.Dispose(); }
    }
}