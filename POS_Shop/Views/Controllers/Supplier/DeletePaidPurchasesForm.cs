using POS_Shop.Models;
using POS_Shop.Models.Suppliers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.Supplier
{
    /// <summary>
    /// Permanently deletes PAID purchase records within a date range.
    ///
    /// Delete cascade (in order):
    ///   1. SupplierPaymentDetail rows linked to the purchase
    ///   2. SupplierPayment headers that become empty after step 1
    ///   3. PurchaseItem rows belonging to the purchase
    ///   4. Purchase header itself
    ///
    /// Only purchases with PaymentStatus == Paid are eligible.
    /// </summary>
    public partial class DeletePaidPurchasesForm : Form
    {
        private readonly POSDbContext _db;

        public DeletePaidPurchasesForm()
        {
            InitializeComponent();
            _db = new POSDbContext();
            WireEvents();
            SetupForm();
        }

        // ══════════════════════════════════════════════════════════════════
        //  SETUP
        // ══════════════════════════════════════════════════════════════════

        private void SetupForm()
        {
            dtpFrom.Value = DateTime.Now.AddMonths(-2);
            dtpTo.Value = DateTime.Now;
            RefreshPreview();
        }

        private void WireEvents()
        {
            dtpFrom.ValueChanged += (s, e) => { chkOld.Checked = false; RefreshPreview(); };
            dtpTo.ValueChanged += (s, e) => { chkOld.Checked = false; RefreshPreview(); };
            chkOld.CheckedChanged += ChkOld_CheckedChanged;
            cmbDateField.SelectedIndexChanged += (s, e) => RefreshPreview();
            btnDelete.Click += BtnDelete_Click;
            btnCancel.Click += (s, e) => this.Close();
        }

        // ══════════════════════════════════════════════════════════════════
        //  1.5-MONTH QUICK CHECKBOX
        // ══════════════════════════════════════════════════════════════════

        private void ChkOld_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOld.Checked)
            {
                dtpFrom.Value = new DateTime(2000, 1, 1);           // from beginning
                dtpTo.Value = DateTime.Now.AddDays(-45);          // older than 1.5 months
            }
            RefreshPreview();
        }

        // ══════════════════════════════════════════════════════════════════
        //  PREVIEW — count matching paid purchases
        // ══════════════════════════════════════════════════════════════════

        private void RefreshPreview()
        {
            try
            {
                int count = GetEligiblePurchases().Count;
                lblPreview.Text = count == 0
                    ? "No paid purchases found in this range."
                    : $"{count} paid purchase(s) will be permanently deleted.";
                lblPreview.ForeColor = count == 0
                    ? Color.FromArgb(46, 125, 50)
                    : Color.FromArgb(198, 40, 40);

                btnDelete.Enabled = count > 0;
            }
            catch (Exception ex)
            {
                lblPreview.Text = "Error: " + ex.Message;
            }
        }

        private List<Purchase> GetEligiblePurchases()
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1); // inclusive end

            bool byPurchaseDate = cmbDateField.SelectedIndex == 0;

            if (byPurchaseDate)
            {
                return _db.Purchases
                    .Where(p => !p.IsDeleted
                             && p.PaymentStatus == PurchasePaymentStatus.Paid
                             && p.PurchaseDate >= from
                             && p.PurchaseDate <= to && p.PaymentStatus== PurchasePaymentStatus.Paid)
                    .ToList();
            }
            else
            {
                // Filter by the date of their latest linked SupplierPayment
                return _db.Purchases
                    .Include("PaymentDetails")
                    .Where(p => !p.IsDeleted
                             && p.PaymentStatus == PurchasePaymentStatus.Paid)
                    .ToList()
                    .Where(p =>
                    {
                        // Find the most recent payment date touching this purchase
                        var paymentIds = p.PaymentDetails.Select(d => d.SupplierPaymentId).Distinct().ToList();
                        if (!paymentIds.Any()) return false;
                        var latestPayDate = _db.SupplierPayments
                            .Where(sp => paymentIds.Contains(sp.Id))
                            .Max(sp => sp.PaymentDate);
                        return latestPayDate >= from && latestPayDate <= to;
                    })
                    .ToList();
            }
        }

        // ══════════════════════════════════════════════════════════════════
        //  DELETE
        // ══════════════════════════════════════════════════════════════════

        //private void BtnDelete_Click(object sender, EventArgs e)
        //{
        //    var purchases = GetEligiblePurchases();
        //    if (purchases.Count == 0) { RefreshPreview(); return; }

        //    string rangeLabel = chkOld.Checked
        //        ? "older than 1.5 months"
        //        : $"{dtpFrom.Value:dd-MMM-yyyy}  to  {dtpTo.Value:dd-MMM-yyyy}";

        //    var confirm = MessageBox.Show(
        //        $"You are about to PERMANENTLY DELETE {purchases.Count} paid purchase(s)\n" +
        //        $"({rangeLabel}).\n\n" +
        //        "This will also remove all linked payment records.\n\n" +
        //        "This action CANNOT be undone. Are you sure?",
        //        "Confirm Delete",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Warning,
        //        MessageBoxDefaultButton.Button2);   // No is default

        //    if (confirm != DialogResult.Yes) return;

        //    // Second confirmation for safety
        //    var confirm2 = MessageBox.Show(
        //        $"FINAL WARNING: Delete {purchases.Count} paid purchase(s) permanently?",
        //        "Final Confirmation",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Stop,
        //        MessageBoxDefaultButton.Button2);

        //    if (confirm2 != DialogResult.Yes) return;

        //    using (var txn = _db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            int deletedPurchases = 0;
        //            var orphanedPaymentIds = new HashSet<int>();

        //            foreach (var purchase in purchases)
        //            {
        //                int purchaseId = purchase.Id;

        //                // 1. Collect SupplierPayment IDs linked to this purchase
        //                var linkedPaymentIds = _db.SupplierPaymentDetails
        //                    .Where(d => d.PurchaseId == purchaseId)
        //                    .Select(d => d.SupplierPaymentId)
        //                    .Distinct()
        //                    .ToList();

        //                foreach (var pid in linkedPaymentIds)
        //                    orphanedPaymentIds.Add(pid);

        //                // 2. Delete SupplierPaymentDetail rows for this purchase
        //                var details = _db.SupplierPaymentDetails
        //                    .Where(d => d.PurchaseId == purchaseId)
        //                    .ToList();
        //                _db.SupplierPaymentDetails.RemoveRange(details);

        //                // 3. Delete PurchaseItems
        //                var items = _db.PurchaseItems
        //                    .Where(i => i.PurchaseId == purchaseId)
        //                    .ToList();
        //                _db.PurchaseItems.RemoveRange(items);

        //                // 4. Delete Purchase header
        //                var p = _db.Purchases.Find(purchaseId);
        //                if (p != null) _db.Purchases.Remove(p);

        //                deletedPurchases++;
        //            }

        //            _db.SaveChanges();

        //            // 5. Delete SupplierPayment headers that now have NO remaining details
        //            int deletedPayments = 0;
        //            foreach (int paymentId in orphanedPaymentIds)
        //            {
        //                bool hasOtherDetails = _db.SupplierPaymentDetails
        //                    .Any(d => d.SupplierPaymentId == paymentId);

        //                if (!hasOtherDetails)
        //                {
        //                    var sp = _db.SupplierPayments.Find(paymentId);
        //                    if (sp != null)
        //                    {
        //                        _db.SupplierPayments.Remove(sp);
        //                        deletedPayments++;
        //                    }
        //                }
        //            }

        //            _db.SaveChanges();
        //            txn.Commit();

        //            MessageBox.Show(
        //                $"✔  Deletion complete!\n\n" +
        //                $"Paid purchases deleted   :  {deletedPurchases}\n" +
        //                $"Payment headers removed  :  {deletedPayments}",
        //                "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            RefreshPreview();
        //        }
        //        catch (Exception ex)
        //        {
        //            txn.Rollback();
        //            MessageBox.Show(
        //                "Delete failed — all changes rolled back.\n\n" + ex.Message,
        //                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}


        // deekseej

        //private void BtnDelete_Click(object sender, EventArgs e)
        //{
        //    var purchases = GetEligiblePurchases();
        //    if (purchases.Count == 0) { RefreshPreview(); return; }

        //    string rangeLabel = chkOld.Checked
        //        ? "older than 1.5 months"
        //        : $"{dtpFrom.Value:dd-MMM-yyyy}  to  {dtpTo.Value:dd-MMM-yyyy}";

        //    var confirm = MessageBox.Show(
        //        $"You are about to PERMANENTLY DELETE {purchases.Count} paid purchase(s)\n" +
        //        $"({rangeLabel}).\n\n" +
        //        "This will also remove all linked payment records.\n\n" +
        //        "This action CANNOT be undone. Are you sure?",
        //        "Confirm Delete",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Warning,
        //        MessageBoxDefaultButton.Button2);   // No is default

        //    if (confirm != DialogResult.Yes) return;

        //    // Second confirmation for safety
        //    var confirm2 = MessageBox.Show(
        //        $"FINAL WARNING: Delete {purchases.Count} paid purchase(s) permanently?",
        //        "Final Confirmation",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Stop,
        //        MessageBoxDefaultButton.Button2);

        //    if (confirm2 != DialogResult.Yes) return;

        //    var purchaseIds = purchases.Select(p => p.Id).ToList();

        //    using (var txn = _db.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            // SINGLE QUERY: Get all payment IDs linked to these purchases
        //            var orphanedPaymentIds = _db.SupplierPaymentDetails
        //                .Where(d => purchaseIds.Contains(d.PurchaseId))
        //                .Select(d => d.SupplierPaymentId)
        //                .Distinct()
        //                .ToList();

        //            // BULK DELETE: SupplierPaymentDetails for all purchases
        //            _db.SupplierPaymentDetails.RemoveRange(
        //                _db.SupplierPaymentDetails.Where(d => purchaseIds.Contains(d.PurchaseId))
        //            );

        //            // BULK DELETE: PurchaseItems for all purchases
        //            _db.SupplierPaymentDetails.RemoveRange(
        //                _db.SupplierPaymentDetails.Where(d => purchaseIds.Contains(d.PurchaseId))
        //            );

        //            _db.PurchaseItems.RemoveRange(
        //                _db.PurchaseItems.Where(i => purchaseIds.Contains(i.PurchaseId))
        //            );

        //            // BULK DELETE: Purchase headers
        //            _db.Purchases.RemoveRange(
        //                _db.Purchases.Where(p => purchaseIds.Contains(p.Id))
        //            );

        //            // Save all deletions at once
        //            int deletedPurchases = _db.SaveChanges();

        //            // Get payment headers that are now orphaned (have no details)
        //            var paymentsToDelete = _db.SupplierPayments
        //                .Where(sp => orphanedPaymentIds.Contains(sp.Id) &&
        //                    !_db.SupplierPaymentDetails.Any(d => d.SupplierPaymentId == sp.Id))
        //                .ToList();

        //            // BULK DELETE: Orphaned payment headers
        //            _db.SupplierPayments.RemoveRange(paymentsToDelete);
        //            int deletedPayments = _db.SaveChanges();

        //            txn.Commit();

        //            MessageBox.Show(
        //                $"✔  Deletion complete!\n\n" +
        //                $"Paid purchases deleted   :  {deletedPurchases}\n" +
        //                $"Payment headers removed  :  {deletedPayments}",
        //                "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            RefreshPreview();
        //        }
        //        catch (Exception ex)
        //        {
        //            txn.Rollback();
        //            MessageBox.Show(
        //                "Delete failed — all changes rolled back.\n\n" + ex.Message,
        //                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //}


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var purchases = GetEligiblePurchases();
            if (purchases.Count == 0) { RefreshPreview(); return; }

            string rangeLabel = chkOld.Checked
                ? "older than 1.5 months"
                : $"{dtpFrom.Value:dd-MMM-yyyy} to {dtpTo.Value:dd-MMM-yyyy}";

            var confirm = MessageBox.Show(
                $"You are about to PERMANENTLY DELETE {purchases.Count} paid purchase(s)\n" +
                $"({rangeLabel}).\n\n" +
                "This will also remove all linked payment records.\n\n" +
                "This action CANNOT be undone. Are you sure?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes) return;

            var confirm2 = MessageBox.Show(
                $"FINAL WARNING: Delete {purchases.Count} paid purchase(s) permanently?",
                "Final Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button2);

            if (confirm2 != DialogResult.Yes) return;

            using (var txn = _db.Database.BeginTransaction())
            {
                try
                {
                    // Collect purchase IDs
                    var purchaseIds = purchases.Select(p => p.Id).ToList();

                    // DB CALL 1 — Fetch ALL SupplierPaymentDetails for all purchases at once
                    var allDetails = _db.SupplierPaymentDetails
                        .Where(d => purchaseIds.Contains(d.PurchaseId))
                        .ToList();

                    // Derive orphaned payment IDs
                    var candidatePaymentIds = allDetails
                        .Select(d => d.SupplierPaymentId)
                        .Distinct()
                        .ToList();

                    // DB CALL 2 — Fetch ALL PurchaseItems for all purchases at once
                    var allItems = _db.PurchaseItems
                        .Where(i => purchaseIds.Contains(i.PurchaseId))
                        .ToList();

                    // Bulk remove payment details and purchase items
                    _db.SupplierPaymentDetails.RemoveRange(allDetails);
                    _db.PurchaseItems.RemoveRange(allItems);
                    _db.Purchases.RemoveRange(purchases);

                    // DB CALL 3 — Single SaveChanges for all the deletes above
                    _db.SaveChanges();

                    // DB CALL 4 — Find payment IDs that still have remaining details
                    var stillUsedPaymentIds = _db.SupplierPaymentDetails
                        .Where(d => candidatePaymentIds.Contains(d.SupplierPaymentId))
                        .Select(d => d.SupplierPaymentId)
                        .Distinct()
                        .ToHashSet();

                    // Find truly orphaned payment IDs
                    var orphanedPaymentIds = candidatePaymentIds
                        .Where(id => !stillUsedPaymentIds.Contains(id))
                        .ToList();

                    // DB CALL 5 — Fetch all orphaned SupplierPayment headers at once
                    var orphanedPayments = _db.SupplierPayments
                        .Where(sp => orphanedPaymentIds.Contains(sp.Id))
                        .ToList();

                    // Remove orphaned payments
                    _db.SupplierPayments.RemoveRange(orphanedPayments);

                    // DB CALL 6 — Final SaveChanges
                    _db.SaveChanges();
                    txn.Commit();

                    MessageBox.Show(
                        $"Deletion complete!\n\n" +
                        $"Paid purchases deleted   :  {purchases.Count}\n" +
                        $"Payment headers removed  :  {orphanedPayments.Count}",
                        "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshPreview();
                }
                catch (Exception ex)
                {
                    txn.Rollback();
                    MessageBox.Show(
                        "Delete failed - all changes rolled back.\n\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _db.Dispose();
        }
    }
}