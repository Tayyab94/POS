using POS_Shop.Models;
using POS_Shop.Models.LoanModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Repositories.LoanRepositories
{
    // ════════════════════════════════════════════════════════════════════════════
    //  DTOs
    // ════════════════════════════════════════════════════════════════════════════

    public class CustomerLedgerSummaryDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public string City { get; set; }
        public decimal RunningBalance { get; set; }
        public DateTime LastTransactionDate { get; set; }

        public bool IsDebit => RunningBalance > 0;
        public bool IsCredit => RunningBalance < 0;
        public bool IsZero => RunningBalance == 0;

        public string BalanceDisplay
        {
            get
            {
                if (RunningBalance == 0) return "Nil";
                return RunningBalance > 0
                    ? $"Dr  Rs. {RunningBalance:N0}"
                    : $"Cr  Rs. {Math.Abs(RunningBalance):N0}";
            }
        }
    }

    public class LedgerPostResult
    {
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        public decimal AdvanceApplied { get; set; }
        public decimal LoanAdded { get; set; }
        public decimal AdvanceAdded { get; set; }

        public string SummaryMessage
        {
            get
            {
                if (AdvanceApplied > 0 && LoanAdded > 0)
                    return $"Advance of Rs. {AdvanceApplied:N0} applied. Remaining loan: Rs. {LoanAdded:N0}";
                if (AdvanceApplied > 0 && LoanAdded == 0)
                    return $"Rs. {AdvanceApplied:N0} deducted from advance credit.";
                if (LoanAdded > 0)
                    return $"Rs. {LoanAdded:N0} added to customer loan.";
                if (AdvanceAdded > 0)
                    return $"Rs. {AdvanceAdded:N0} excess stored as advance credit.";
                return "Bill fully settled.";
            }
        }
    }

    // ════════════════════════════════════════════════════════════════════════════
    //  CustomerLedgerRepository
    // ════════════════════════════════════════════════════════════════════════════

    public class CustomerLedgerRepository
    {
        private readonly POSDbContext _db;

        public CustomerLedgerRepository(POSDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        // ── Ledger queries ───────────────────────────────────────────────────

        public async Task<CustomerLedger> GetOrCreateLedgerAsync(int customerId)
        {
            var ledger = await _db.CustomerLedgers
                 .FirstOrDefaultAsync(l => l.CustomerId == customerId);

            if (ledger == null)
            {
                ledger = new CustomerLedger
                {
                    CustomerId = customerId,
                    RunningBalance = 0,
                    LastTransactionDate = DateTime.Now
                };
                _db.CustomerLedgers.Add(ledger);
                await _db.SaveChangesAsync();
            }
            return ledger;
        }

        public async Task<decimal> GetRunningBalanceAsync(int customerId)
        {
            var ledger = await _db.CustomerLedgers
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.CustomerId == customerId);
            return ledger?.RunningBalance ?? 0m;
        }

        /// <summary>Paged transaction history — newest first.</summary>
        //public async Task<(List<CustomerTransaction> Rows, int TotalCount)> GetHistoryAsync(
        //    int customerId, int page, int pageSize,
        //    DateTime? from = null, DateTime? to = null)
        //{
        //    var q = _db.CustomerTransactions
        //        .Where(t => t.CustomerId == customerId && !t.IsDeleted);

        //    if (from.HasValue) q = q.Where(t => t.TransactionDate >= from.Value.Date);
        //    if (to.HasValue) q = q.Where(t => t.TransactionDate < to.Value.Date.AddDays(1));

        //    int total = await q.CountAsync();
        //    var rows = await q.OrderByDescending(t => t.TransactionDate)
        //                       .ThenByDescending(t => t.Id)
        //                       .Skip((page - 1) * pageSize)
        //                       .Take(pageSize)
        //                       .ToListAsync();
        //    return (rows, total);
        //}


        public async Task<(List<CustomerTransaction> Rows, int TotalCount)> GetHistoryAsync(
    int customerId, int page, int pageSize,
    DateTime? from = null, DateTime? to = null)
        {
            IQueryable<CustomerTransaction> q = _db.CustomerTransactions
                .AsNoTracking()
                .Where(t => t.CustomerId == customerId && !t.IsDeleted);

            if (from.HasValue)
            {
                var fromDate = from.Value.Date;          // computed in memory
                q = q.Where(t => t.TransactionDate >= fromDate);
            }

            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);   // computed in memory
                q = q.Where(t => t.TransactionDate < toDate);
            }

            page = Math.Max(page, 1);

            int total = await q.CountAsync();

            var rows = await q
                .OrderByDescending(t => t.TransactionDate)
                .ThenByDescending(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (rows, total);
        }

        /// <summary>All customers with their balances — for the report screen.</summary>
        //public async Task<List<CustomerLedgerSummaryDto>> GetAllBalancesAsync(
        //    string search = null, bool onlyWithBalance = false)
        //{

        //    var q = _db.CustomerLedgers.Include("Customer").AsNoTracking();

        //    if (onlyWithBalance)
        //        q = q.Where(l => l.RunningBalance != 0);

        //    if (!string.IsNullOrWhiteSpace(search))
        //    {
        //        string s = search.ToLower();
        //        q = q.Where(l => l.Customer.CustomerName.ToLower().Contains(s)
        //                       || l.Customer.ContactNo.Contains(s)).AsNoTracking();
        //    }

        //    var data = await q.OrderByDescending(l => Math.Abs(l.RunningBalance)).ToListAsync();

        //    return data.Select(l => new CustomerLedgerSummaryDto
        //    {
        //        CustomerId = l.CustomerId,
        //        CustomerName = l.Customer.CustomerName,
        //        ContactNo = l.Customer.ContactNo,
        //        RunningBalance = l.RunningBalance,
        //        LastTransactionDate = l.LastTransactionDate
        //    }).ToList();
        //}

        public async Task<List<CustomerLedgerSummaryDto>> GetAllBalancesAsync(
    string search = null, bool onlyWithBalance = false)
        {
            IQueryable<CustomerLedger> q = _db.CustomerLedgers
                .Include(s=>s.Customer)
                .AsNoTracking();

            if (onlyWithBalance)
                q = q.Where(l => l.RunningBalance != 0);

            if (!string.IsNullOrWhiteSpace(search))
            {
                string s = search.ToLower();

                q = q.Where(l =>
                    l.Customer.CustomerName.ToLower().Contains(s) ||
                    l.Customer.ContactNo.Contains(s));
            }

            var data = await q
                .OrderByDescending(l => Math.Abs(l.RunningBalance))
                .ToListAsync();

            return data.Select(l => new CustomerLedgerSummaryDto
            {
                CustomerId = l.CustomerId,
                CustomerName = l.Customer.CustomerName,
                ContactNo = l.Customer.ContactNo,
                RunningBalance = l.RunningBalance,
                LastTransactionDate = l.LastTransactionDate
            }).ToList();
        }

        // ── Order integration ────────────────────────────────────────────────

        /// <summary>
        /// Called from BillPadForm after an order is saved.
        /// Automatically handles: loan, advance, advance-applied-to-loan scenarios.
        /// Call inside the SAME db transaction as the order save.
        /// </summary>
        public async Task<LedgerPostResult> PostOrderAsync(
            int customerId, int orderId,
            decimal totalBill, decimal amountReceived,
            string createdBy = null)
        {
            var ledger = await GetOrCreateLedgerAsync(customerId);
            decimal balBefore = ledger.RunningBalance;
            decimal advApplied = 0m, loanAdded = 0m, advAdded = 0m;

            decimal unpaid = totalBill - amountReceived;

            if (unpaid > 0)
            {
                // Customer underpaid — they owe us.
                decimal existingCredit = ledger.RunningBalance < 0
                    ? Math.Abs(ledger.RunningBalance) : 0m;

                if (existingCredit > 0)
                {
                    // First, absorb as much advance credit as possible.
                    advApplied = Math.Min(existingCredit, unpaid);
                    Post(ledger, customerId, orderId, null,
                         TransactionTypes.AdvanceUsed, advApplied, "D",
                         createdBy, $"Advance applied to order #{orderId}");

                    decimal remainder = unpaid - advApplied;
                    if (remainder > 0)
                    {
                        Post(ledger, customerId, orderId, null,
                             TransactionTypes.SaleLoan, remainder, "D",
                             createdBy, $"Loan — order #{orderId}");
                        loanAdded = remainder;
                    }
                }
                else
                {
                    // No advance — full unpaid goes to loan.
                    Post(ledger, customerId, orderId, null,
                         TransactionTypes.SaleLoan, unpaid, "D",
                         createdBy, $"Loan — order #{orderId}");
                    loanAdded = unpaid;
                }
            }
            else if (unpaid < 0)
            {
                // Customer overpaid — surplus becomes advance credit.
                decimal surplus = Math.Abs(unpaid);

                if (ledger.RunningBalance > 0)
                {
                    // Existing loan: clear it first, then any true advance.
                    decimal loanCleared = Math.Min(ledger.RunningBalance, surplus);
                    Post(ledger, customerId, orderId, null,
                         TransactionTypes.Payment, loanCleared, "C",
                         createdBy, $"Overpayment clearing loan — order #{orderId}");

                    decimal trueAdv = surplus - loanCleared;
                    if (trueAdv > 0)
                    {
                        Post(ledger, customerId, orderId, null,
                             TransactionTypes.Advance, trueAdv, "C",
                             createdBy, $"Advance stored — order #{orderId}");
                        advAdded = trueAdv;
                    }
                }
                else
                {
                    Post(ledger, customerId, orderId, null,
                         TransactionTypes.Advance, surplus, "C",
                         createdBy, $"Advance payment — order #{orderId}");
                    advAdded = surplus;
                }
            }
            // unpaid == 0 → fully paid, no ledger entry needed.

            await _db.SaveChangesAsync();

            return new LedgerPostResult
            {
                BalanceBefore = balBefore,
                BalanceAfter = ledger.RunningBalance,
                AdvanceApplied = advApplied,
                LoanAdded = loanAdded,
                AdvanceAdded = advAdded
            };
        }

        /// <summary>Reverses all transactions for a voided/deleted order.</summary>
        public async Task ReverseOrderAsync(int orderId, string reversedBy = null)
        {
            var txs = await _db.CustomerTransactions
                .Where(t => t.OrderId == orderId && !t.IsDeleted)
                .ToListAsync();

            if (!txs.Any()) return;

            int customerId = txs.First().CustomerId;
            var ledger = await GetOrCreateLedgerAsync(customerId);

            foreach (var tx in txs)
            {
                string flip = tx.DebitCredit == "D" ? "C" : "D";
                Post(ledger, customerId, orderId, null,
                     TransactionTypes.Adjustment, tx.Amount, flip,
                     reversedBy, $"Reversal of tx#{tx.Id} (order #{orderId} voided)");
                tx.IsDeleted = true;
            }

            await _db.SaveChangesAsync();
        }

        // ── Standalone payments ──────────────────────────────────────────────

        public async Task<CustomerPayment> RecordPaymentAsync(
            int customerId, decimal amount, string paymentMethod,
            string referenceNo = null, string transactionId = null,
            string notes = null, string createdBy = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero.");

            var ledger = await GetOrCreateLedgerAsync(customerId);

            var payment = new CustomerPayment
            {
                CustomerId = customerId,
                PaymentDate = DateTime.Now,
                AmountPaid = amount,
                PaymentMethod = paymentMethod,
                ReferenceNo = referenceNo,
                TransactionId = transactionId,
                Notes = notes,
                CreatedBy = createdBy,
                BalanceBefore = ledger.RunningBalance
            };
            _db.CustomerPayments.Add(payment);
            await _db.SaveChangesAsync(); // get payment.Id

            Post(ledger, customerId, null, payment.Id,
                 TransactionTypes.Payment, amount, "C",
                 createdBy, notes ?? $"Payment — {PaymentMethods.ToDisplay(paymentMethod)}");

            payment.BalanceAfter = ledger.RunningBalance;
            await _db.SaveChangesAsync();
            return payment;
        }

        public async Task ReversePaymentAsync(int paymentId, string deletedBy, string reason)
        {
            var p = await _db.CustomerPayments
                .FirstOrDefaultAsync(x => x.Id == paymentId && !x.IsDeleted);
            if (p == null)
                throw new InvalidOperationException($"Payment #{paymentId} not found or already reversed.");

            var ledger = await GetOrCreateLedgerAsync(p.CustomerId);

            Post(ledger, p.CustomerId, null, paymentId,
                 TransactionTypes.Adjustment, p.AmountPaid, "D",
                 deletedBy, $"Reversal of payment #{paymentId}: {reason}");

            p.IsDeleted = true;
            p.DeletedBy = deletedBy;
            p.DeletedAt = DateTime.Now;
            p.DeleteReason = reason;

            await _db.SaveChangesAsync();
        }

        // ── Manual adjustments ───────────────────────────────────────────────

        public async Task PostAdjustmentAsync(
            int customerId, decimal amount, string debitCredit,
            string notes, string createdBy = null)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            if (debitCredit != "D" && debitCredit != "C")
                throw new ArgumentException("debitCredit must be 'D' or 'C'.");

            var ledger = await GetOrCreateLedgerAsync(customerId);
            Post(ledger, customerId, null, null,
                 TransactionTypes.Adjustment, amount, debitCredit,
                 createdBy, notes);

            await _db.SaveChangesAsync();
        }

        // ── Private core ─────────────────────────────────────────────────────

        /// <summary>
        /// THE ONLY place that creates a transaction row and updates RunningBalance.
        /// Never call SaveChanges here — caller batches.
        /// </summary>
        private void Post(CustomerLedger ledger,
            int customerId, int? orderId, int? paymentId,
            string type, decimal amount, string dc,
            string createdBy, string notes)
        {
            ledger.RunningBalance += dc == "D" ? amount : -amount;
            ledger.LastTransactionDate = DateTime.Now;

            _db.CustomerTransactions.Add(new CustomerTransaction
            {
                CustomerId = customerId,
                TransactionDate = DateTime.Now,
                TransactionType = type,
                Amount = amount,
                DebitCredit = dc,
                BalanceAfter = ledger.RunningBalance,
                OrderId = orderId,
                CustomerPaymentId = paymentId,
                Notes = notes,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            });
        }





        public async Task<(List<CustomerTransaction> Rows, int Total)>
            GetTransactionHistoryAsync(int customerId, int page, int pageSize,
                DateTime? from = null, DateTime? to = null)
        {
            var q = _db.CustomerTransactions
                .Include("Order")
                .Include("CustomerPayment")
                .Where(t => t.CustomerId == customerId && !t.IsDeleted);

            // Compute boundaries in C# first — .Date is not translatable in LINQ to Entities (EF6).
            if (from.HasValue)
            {
                DateTime fromStart = from.Value.Date;                   // e.g. 2025-03-01 00:00:00
                q = q.Where(t => t.TransactionDate >= fromStart);
            }
            if (to.HasValue)
            {
                DateTime toEnd = to.Value.Date.AddDays(1);              // e.g. 2025-03-02 00:00:00 (exclusive)
                q = q.Where(t => t.TransactionDate < toEnd);
            }

            int total = await q.CountAsync();
            var rows = await q.OrderByDescending(t => t.Id)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();
            return (rows, total);
        }

        public async Task<List<CustomerLedgerSummaryDto>> GetAllCustomerBalancesAsync(
         string search = null, bool onlyWithBalance = false)
        {
            IQueryable<CustomerLedger> q = _db.CustomerLedgers
                .Include(l => l.Customer)   // strongly typed include
                .AsNoTracking();

            if (onlyWithBalance)
                q = q.Where(l => l.RunningBalance != 0);

            if (!string.IsNullOrWhiteSpace(search))
            {
                string s = search.ToLower();

                q = q.Where(l =>
                    l.Customer.CustomerName.ToLower().Contains(s) ||
                    l.Customer.ContactNo.Contains(s));
            }

            return await q
                .OrderByDescending(l => Math.Abs(l.RunningBalance))
                .Select(l => new CustomerLedgerSummaryDto
                {
                    CustomerId = l.CustomerId,
                    CustomerName = l.Customer.CustomerName,
                    ContactNo = l.Customer.ContactNo,
                    City = l.Customer.City != null ? l.Customer.City.Name : "-",
                    RunningBalance = l.RunningBalance,
                    LastTransactionDate = l.LastTransactionDate
                })
                .ToListAsync();
        }
        // ════════════════════════════════════════════════════════════════════
        // Order integration
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Called after an order is saved.  Handles all four scenarios:
        ///   A) Underpaid, no advance   → SaleLoan (D)
        ///   B) Underpaid, has advance  → AdvanceUsed (D) + optional SaleLoan (D)
        ///   C) Overpaid, had old loan  → Payment (C) to clear loan + optional Advance (C)
        ///   D) Overpaid, no old loan   → Advance (C)
        ///   E) Exactly paid            → nothing posted
        /// </summary>
        public async Task<LedgerPostResult> PostOrderTransactionAsync(
            int customerId, int orderId,
            decimal totalBill, decimal amountReceived,
            string createdBy = null)
        {
            var ledger = await GetOrCreateLedgerAsync(customerId);
            decimal balBefore = ledger.RunningBalance;

            decimal unpaid = totalBill - amountReceived;  // +ve = owes us, -ve = overpaid

            decimal advanceApplied = 0, loanAdded = 0, advanceAdded = 0;

            if (unpaid > 0)
            {
                // Customer underpaid
                decimal credit = ledger.RunningBalance < 0
                    ? Math.Abs(ledger.RunningBalance) : 0m;  // existing advance credit

                if (credit > 0)
                {
                    decimal use = Math.Min(credit, unpaid);
                    decimal rest = unpaid - use;

                    AddTx(customerId, orderId, null,
                        TransactionTypes.AdvanceUsed, use, "D", ledger, createdBy,
                        $"Advance applied to order #{orderId}");
                    advanceApplied = use;

                    if (rest > 0)
                    {
                        AddTx(customerId, orderId, null,
                            TransactionTypes.SaleLoan, rest, "D", ledger, createdBy,
                            $"Loan on order #{orderId}");
                        loanAdded = rest;
                    }
                }
                else
                {
                    AddTx(customerId, orderId, null,
                        TransactionTypes.SaleLoan, unpaid, "D", ledger, createdBy,
                        $"Loan on order #{orderId}");
                    loanAdded = unpaid;
                }
            }
            else if (unpaid < 0)
            {
                // Customer overpaid
                decimal surplus = Math.Abs(unpaid);

                if (ledger.RunningBalance > 0)
                {
                    // Clear existing loan first
                    decimal clear = Math.Min(ledger.RunningBalance, surplus);
                    decimal advance = surplus - clear;

                    AddTx(customerId, orderId, null,
                        TransactionTypes.Payment, clear, "C", ledger, createdBy,
                        $"Overpayment clearing loan, order #{orderId}");

                    if (advance > 0)
                    {
                        AddTx(customerId, orderId, null,
                            TransactionTypes.Advance, advance, "C", ledger, createdBy,
                            $"Advance stored from order #{orderId}");
                        advanceAdded = advance;
                    }
                }
                else
                {
                    AddTx(customerId, orderId, null,
                        TransactionTypes.Advance, surplus, "C", ledger, createdBy,
                        $"Advance payment, order #{orderId}");
                    advanceAdded = surplus;
                }
            }
            // unpaid == 0 → fully settled, nothing to post

            await _db.SaveChangesAsync();

            return new LedgerPostResult
            {
                BalanceBefore = balBefore,
                BalanceAfter = ledger.RunningBalance,
                AdvanceApplied = advanceApplied,
                LoanAdded = loanAdded,
                AdvanceAdded = advanceAdded
            };
        }

        public async Task ReverseOrderTransactionAsync(int orderId, string reversedBy = null)
        {
            var txs = await _db.CustomerTransactions
                .Where(t => t.OrderId == orderId && !t.IsDeleted)
                .ToListAsync();
            if (!txs.Any()) return;

            int customerId = txs.First().CustomerId;
            var ledger = await GetOrCreateLedgerAsync(customerId);

            foreach (var tx in txs)
            {
                // Flip D↔C to reverse
                string rev = tx.DebitCredit == "D" ? "C" : "D";
                AddTx(customerId, orderId, null,
                    TransactionTypes.Adjustment, tx.Amount, rev, ledger, reversedBy,
                    $"Reversal of tx#{tx.Id} — order #{orderId} voided");
                tx.IsDeleted = true;
            }

            await _db.SaveChangesAsync();
        }

        // ════════════════════════════════════════════════════════════════════
        // Standalone payments
        // ════════════════════════════════════════════════════════════════════

        //public async Task<CustomerPayment> RecordPaymentAsync(
        //    int customerId, decimal amount, string paymentMethod,
        //    string referenceNo = null, string transactionId = null,
        //    string notes = null, string createdBy = null)
        //{
        //    if (amount <= 0)
        //        throw new ArgumentException("Amount must be > 0.");

        //    var ledger = await GetOrCreateLedgerAsync(customerId);

        //    var payment = new CustomerPayment
        //    {
        //        CustomerId = customerId,
        //        PaymentDate = DateTime.Now,
        //        AmountPaid = amount,
        //        PaymentMethod = paymentMethod,
        //        ReferenceNo = referenceNo,
        //        TransactionId = transactionId,
        //        Notes = notes,
        //        CreatedBy = createdBy,
        //        BalanceBefore = ledger.RunningBalance
        //    };
        //    _db.CustomerPayments.Add(payment);
        //    await _db.SaveChangesAsync();  // get payment.Id

        //    AddTx(customerId, null, payment.Id,
        //        TransactionTypes.Payment, amount, "C", ledger, createdBy,
        //        notes ?? $"Payment — {PaymentMethods.ToDisplay(paymentMethod)}");

        //    payment.BalanceAfter = ledger.RunningBalance;
        //    await _db.SaveChangesAsync();

        //    return payment;
        //}

        //public async Task ReversePaymentAsync(int paymentId, string deletedBy, string reason)
        //{
        //    var payment = await _db.CustomerPayments
        //        .FirstOrDefaultAsync(p => p.Id == paymentId && !p.IsDeleted);
        //    if (payment == null)
        //        throw new InvalidOperationException($"Payment #{paymentId} not found.");

        //    var ledger = await GetOrCreateLedgerAsync(payment.CustomerId);

        //    AddTx(payment.CustomerId, null, paymentId,
        //        TransactionTypes.Adjustment, payment.AmountPaid, "D", ledger, deletedBy,
        //        $"Reversal of payment #{paymentId}: {reason}");

        //    payment.IsDeleted = true;
        //    payment.DeletedBy = deletedBy;
        //    payment.DeletedAt = DateTime.Now;
        //    payment.DeleteReason = reason;

        //    await _db.SaveChangesAsync();
        //}

        // ════════════════════════════════════════════════════════════════════
        // Manual adjustments
        // ════════════════════════════════════════════════════════════════════

        //public async Task PostAdjustmentAsync(
        //    int customerId, decimal amount, string debitCredit,
        //    string notes, string createdBy = null)
        //{
        //    if (amount <= 0) throw new ArgumentException("Amount must be > 0.");
        //    if (debitCredit != "D" && debitCredit != "C")
        //        throw new ArgumentException("debitCredit must be D or C.");

        //    var ledger = await GetOrCreateLedgerAsync(customerId);
        //    AddTx(customerId, null, null,
        //        TransactionTypes.Adjustment, amount, debitCredit, ledger, createdBy, notes);
        //    await _db.SaveChangesAsync();
        //}

        // ════════════════════════════════════════════════════════════════════
        // PRIVATE — single write-path for balance + transaction insert
        // ════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Adjusts RunningBalance and queues a CustomerTransaction for insert.
        /// Does NOT call SaveChanges — caller batches saves intentionally.
        /// </summary>
        private void AddTx(
            int customerId, int? orderId, int? paymentId,
            string type, decimal amount, string dc,
            CustomerLedger ledger, string createdBy, string notes)
        {
            if (dc == "D") ledger.RunningBalance += amount;
            else ledger.RunningBalance -= amount;

            ledger.LastTransactionDate = DateTime.Now;

            _db.CustomerTransactions.Add(new CustomerTransaction
            {
                CustomerId = customerId,
                TransactionDate = DateTime.Now,
                TransactionType = type,
                Amount = amount,
                DebitCredit = dc,
                BalanceAfter = ledger.RunningBalance,
                OrderId = orderId,
                CustomerPaymentId = paymentId,
                Notes = notes,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now,
                IsDeleted = false
            });
        }
    }
}
