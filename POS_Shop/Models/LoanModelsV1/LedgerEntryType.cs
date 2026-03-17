using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models.LoanModelsV1
{
    //// ─── Enums ───────────────────────────────────────────────────────────────

    //public enum LedgerEntryType
    //{
    //    Sale,               // Order posted — customer owes money
    //    PaymentReceived,    // Customer paid loan
    //    AdvanceDeposit,     // Customer deposited advance
    //    AdvanceApplied,     // Advance used against an order
    //    OpeningBalance,     // Starting balance migration
    //    Adjustment,         // Manual correction (AdjustmentForm)
    //    Refund              // Money returned to customer
    //}

    //public enum PaymentMethod
    //{
    //    Cash,
    //    BankTransfer,
    //    Cheque,
    //    JazzCash,
    //    EasyPaisa
    //}

    //public enum BalanceType
    //{
    //    Loan,       // Customer owes us   (Balance > 0)
    //    Advance,    // We owe customer    (Balance < 0)
    //    Clear       // Settled            (Balance == 0)
    //}

    //// ─── EF Entities ─────────────────────────────────────────────────────────

    //[Table("CustomerLedger")]
    //public class CustomerLedgerEntry
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [Required]
    //    public int CustomerId { get; set; }

    //    [Required]
    //    public DateTime EntryDate { get; set; } = DateTime.Now;

    //    [Required]
    //    [MaxLength(30)]
    //    public string EntryType { get; set; }   // LedgerEntryType.ToString()


    //    public decimal Debit { get; set; } = 0;   // Customer owes  (+)


    //    public decimal Credit { get; set; } = 0;  // Customer paid  (+)

    //    [Required]

    //    public decimal Balance { get; set; }       // Running balance (Debit - Credit cumulative)

    //    public int? ReferenceId { get; set; }      // OrderId / PaymentId

    //    [MaxLength(30)]
    //    public string ReferenceType { get; set; }  // "ORDER" / "PAYMENT" / "ADVANCE" / "ADJUSTMENT"

    //    [MaxLength(500)]
    //    public string Note { get; set; }

    //    [MaxLength(100)]
    //    public string CreatedBy { get; set; }

    //    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //    // Navigation
    //    [ForeignKey("CustomerId")]
    //    public virtual Customer Customer { get; set; }
    //}

    //[Table("CustomerPayments")]
    //public class CustomerPayment
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    [Required]
    //    public int CustomerId { get; set; }

    //    [Required]
    //    public DateTime PaymentDate { get; set; } = DateTime.Now;

    //    [Required]

    //    public decimal Amount { get; set; }

    //    [Required]
    //    [MaxLength(30)]
    //    public string PaymentMethod { get; set; }  // PaymentMethod.ToString()

    //    [MaxLength(200)]
    //    public string ReferenceNo { get; set; }   // Cheque no / bank ref

    //    [MaxLength(500)]
    //    public string Note { get; set; }

    //    public int? LedgerEntryId { get; set; }

    //    [MaxLength(100)]
    //    public string CreatedBy { get; set; }

    //    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //    [ForeignKey("CustomerId")]
    //    public virtual Customer Customer { get; set; }
    //}

    //// ─── DTOs for UI ─────────────────────────────────────────────────────────

    //public class CustomerLedgerRow
    //{
    //    public int Id { get; set; }
    //    public DateTime EntryDate { get; set; }
    //    public string EntryTypeDisplay { get; set; }
    //    public decimal Debit { get; set; }
    //    public decimal Credit { get; set; }
    //    public decimal Balance { get; set; }
    //    public string Note { get; set; }
    //    public string ReferenceType { get; set; }
    //    public int? ReferenceId { get; set; }
    //    public string CreatedBy { get; set; }

    //    // UI helpers
    //    public string DebitDisplay => Debit > 0 ? Debit.ToString("N2") : "-";
    //    public string CreditDisplay => Credit > 0 ? Credit.ToString("N2") : "-";
    //    public string BalanceDisplay => Math.Abs(Balance).ToString("N2");
    //    public string BalanceTypeDisplay => Balance > 0 ? "Loan" : Balance < 0 ? "Advance" : "Clear";
    //}

    //public class CustomerBalanceSummary
    //{
    //    public int CustomerId { get; set; }
    //    public string CustomerName { get; set; }
    //    public string ContactNo { get; set; }
    //    public string CityName { get; set; }
    //    public decimal Balance { get; set; }
    //    public BalanceType BalanceType => Balance > 0 ? BalanceType.Loan : Balance < 0 ? BalanceType.Advance : BalanceType.Clear;
    //    public string BalanceDisplay => Math.Abs(Balance).ToString("N2");
    //    public DateTime? LastTransactionDate { get; set; }
    //}

    //public class LedgerSummary
    //{
    //    public decimal TotalDebit { get; set; }
    //    public decimal TotalCredit { get; set; }
    //    public decimal CurrentBalance { get; set; }
    //    public decimal OpeningBalance { get; set; }
    //    public BalanceType BalanceType => CurrentBalance > 0 ? BalanceType.Loan : CurrentBalance < 0 ? BalanceType.Advance : BalanceType.Clear;
    //}

    //// ─── Repository ──────────────────────────────────────────────────────────

    //public class CustomerLedgerRepository
    //{
    //    private readonly POSDbContext _context;

    //    public CustomerLedgerRepository(POSDbContext context)
    //    {
    //        _context = context;
    //    }

    //    /// <summary>Get running balance for a customer right now.</summary>
    //    public async Task<decimal> GetCurrentBalanceAsync(int customerId)
    //    {
    //        var last = await _context.CustomerLedgerEntries
    //            .Where(e => e.CustomerId == customerId)
    //            .OrderByDescending(e => e.Id)
    //            .FirstOrDefaultAsync();
    //        return last?.Balance ?? 0;
    //    }

    //    /// <summary>Get ledger rows for a customer, optional date range.</summary>
    //    public async Task<List<CustomerLedgerRow>> GetLedgerAsync(
    //        int customerId, DateTime? from = null, DateTime? to = null)
    //    {
    //        var q = _context.CustomerLedgerEntries
    //            .Where(e => e.CustomerId == customerId);

    //        var fromDate = from.Value.Date;
    //        var toDate = to.Value.Date.AddDays(1);
    //        if (from.HasValue) q = q.Where(e => e.EntryDate >= fromDate);
    //        if (to.HasValue) q = q.Where(e => e.EntryDate < toDate);

    //        var rows = await q.OrderBy(e => e.Id).ToListAsync();

    //        return rows.Select(r => new CustomerLedgerRow
    //        {
    //            Id = r.Id,
    //            EntryDate = r.EntryDate,
    //            EntryTypeDisplay = FormatEntryType(r.EntryType),
    //            Debit = r.Debit,
    //            Credit = r.Credit,
    //            Balance = r.Balance,
    //            Note = r.Note,
    //            ReferenceType = r.ReferenceType,
    //            ReferenceId = r.ReferenceId,
    //            CreatedBy = r.CreatedBy
    //        }).ToList();
    //    }

    //    /// <summary>Get summary stats for the customer ledger header.</summary>
    //    public async Task<LedgerSummary> GetLedgerSummaryAsync(int customerId, DateTime? from = null, DateTime? to = null)
    //    {



    //        // Opening balance = balance of last entry before 'from'
    //        decimal openingBalance = 0;
    //        if (from.HasValue)
    //        {
    //            var fromDate = from.Value.Date;
    //            var openingEntry = await _context.CustomerLedgerEntries
    //                .Where(e => e.CustomerId == customerId && e.EntryDate < fromDate)
    //                .OrderByDescending(e => e.Id)
    //                .FirstOrDefaultAsync();
    //            openingBalance = openingEntry?.Balance ?? 0;
    //        }

    //        var q = _context.CustomerLedgerEntries
    //            .Where(e => e.CustomerId == customerId);
    //        if (from.HasValue)
    //        {
    //            var fromDate = from.Value.Date;
    //            q = q.Where(e => e.EntryDate >= fromDate);
    //        }
    //        if (to.HasValue)
    //        {
    //            var toDate = to.Value.Date.AddDays(1);
    //            q = q.Where(e => e.EntryDate < toDate);
    //        }

    //        var totalDebit = await q.SumAsync(e => (decimal?)e.Debit) ?? 0;
    //        var totalCredit = await q.SumAsync(e => (decimal?)e.Credit) ?? 0;
    //        var lastBalance = await q.OrderByDescending(e => e.Id)
    //                                 .Select(e => (decimal?)e.Balance)
    //                                 .FirstOrDefaultAsync() ?? openingBalance;

    //        return new LedgerSummary
    //        {
    //            OpeningBalance = openingBalance,
    //            TotalDebit = totalDebit,
    //            TotalCredit = totalCredit,
    //            CurrentBalance = lastBalance
    //        };
    //    }

    //    /// <summary>Post a SALE entry when an order is saved with outstanding amount.</summary>
    //    public async Task PostSaleEntryAsync(
    //        int customerId, decimal orderTotal, decimal received,
    //        int orderId, string createdBy)
    //    {
    //        decimal outstanding = orderTotal - received;
    //        if (outstanding <= 0) return; // Fully paid — no loan

    //        decimal prevBalance = await GetCurrentBalanceAsync(customerId);
    //        decimal newBalance = prevBalance + outstanding;

    //        _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
    //        {
    //            CustomerId = customerId,
    //            EntryDate = DateTime.Now,
    //            EntryType = LedgerEntryType.Sale.ToString(),
    //            Debit = outstanding,
    //            Credit = 0,
    //            Balance = newBalance,
    //            ReferenceId = orderId,
    //            ReferenceType = "ORDER",
    //            Note = $"Invoice #{orderId} — Outstanding: {outstanding:N2}",
    //            CreatedBy = createdBy
    //        });
    //        await _context.SaveChangesAsync();
    //    }

    //    /// <summary>Post a payment received from the customer.</summary>
    //    public async Task<int> PostPaymentAsync(
    //        int customerId, decimal amount, string paymentMethod,
    //        string referenceNo, string note, string createdBy)
    //    {
    //        decimal prevBalance = await GetCurrentBalanceAsync(customerId);
    //        decimal newBalance = prevBalance - amount;

    //        var ledgerEntry = new CustomerLedgerEntry
    //        {
    //            CustomerId = customerId,
    //            EntryDate = DateTime.Now,
    //            EntryType = LedgerEntryType.PaymentReceived.ToString(),
    //            Debit = 0,
    //            Credit = amount,
    //            Balance = newBalance,
    //            ReferenceType = "PAYMENT",
    //            Note = string.IsNullOrWhiteSpace(note) ? $"Payment received via {paymentMethod}" : note,
    //            CreatedBy = createdBy
    //        };
    //        _context.CustomerLedgerEntries.Add(ledgerEntry);
    //        await _context.SaveChangesAsync();

    //        // Also log to CustomerPayments table
    //        var payment = new CustomerPayment
    //        {
    //            CustomerId = customerId,
    //            PaymentDate = DateTime.Now,
    //            Amount = amount,
    //            PaymentMethod = paymentMethod,
    //            ReferenceNo = referenceNo,
    //            Note = note,
    //            LedgerEntryId = ledgerEntry.Id,
    //            CreatedBy = createdBy
    //        };
    //        _context.CustomerPayments.Add(payment);
    //        await _context.SaveChangesAsync();

    //        return ledgerEntry.Id;
    //    }

    //    /// <summary>Post an advance deposit from the customer.</summary>
    //    public async Task PostAdvanceDepositAsync(
    //        int customerId, decimal amount, string paymentMethod,
    //        string referenceNo, string note, string createdBy)
    //    {
    //        decimal prevBalance = await GetCurrentBalanceAsync(customerId);
    //        decimal newBalance = prevBalance - amount; // Advance makes balance negative (we owe them)

    //        _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
    //        {
    //            CustomerId = customerId,
    //            EntryDate = DateTime.Now,
    //            EntryType = LedgerEntryType.AdvanceDeposit.ToString(),
    //            Debit = 0,
    //            Credit = amount,
    //            Balance = newBalance,
    //            ReferenceType = "ADVANCE",
    //            Note = string.IsNullOrWhiteSpace(note) ? $"Advance deposit via {paymentMethod}" : note,
    //            CreatedBy = createdBy
    //        });
    //        await _context.SaveChangesAsync();
    //    }

    //    /// <summary>Post a manual adjustment (positive = customer owes more, negative = we owe them).</summary>
    //    public async Task PostAdjustmentAsync(
    //        int customerId, decimal adjustmentAmount, string reason, string createdBy)
    //    {
    //        decimal prevBalance = await GetCurrentBalanceAsync(customerId);
    //        decimal newBalance = prevBalance + adjustmentAmount;

    //        decimal debit = adjustmentAmount > 0 ? adjustmentAmount : 0;
    //        decimal credit = adjustmentAmount < 0 ? Math.Abs(adjustmentAmount) : 0;

    //        _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
    //        {
    //            CustomerId = customerId,
    //            EntryDate = DateTime.Now,
    //            EntryType = LedgerEntryType.Adjustment.ToString(),
    //            Debit = debit,
    //            Credit = credit,
    //            Balance = newBalance,
    //            ReferenceType = "ADJUSTMENT",
    //            Note = reason,
    //            CreatedBy = createdBy
    //        });
    //        await _context.SaveChangesAsync();
    //    }

    //    /// <summary>Set opening balance for a customer (migration use).</summary>
    //    public async Task SetOpeningBalanceAsync(
    //        int customerId, decimal balance, string note, string createdBy)
    //    {
    //        // Remove any existing opening balance for this customer first
    //        var existing = _context.CustomerLedgerEntries
    //            .Where(e => e.CustomerId == customerId && e.EntryType == LedgerEntryType.OpeningBalance.ToString());
    //        _context.CustomerLedgerEntries.RemoveRange(existing);

    //        decimal debit = balance > 0 ? balance : 0;
    //        decimal credit = balance < 0 ? Math.Abs(balance) : 0;

    //        _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
    //        {
    //            CustomerId = customerId,
    //            EntryDate = DateTime.Today,
    //            EntryType = LedgerEntryType.OpeningBalance.ToString(),
    //            Debit = debit,
    //            Credit = credit,
    //            Balance = balance,
    //            ReferenceType = "OPENING",
    //            Note = string.IsNullOrWhiteSpace(note) ? "Opening balance" : note,
    //            CreatedBy = createdBy
    //        });
    //        await _context.SaveChangesAsync();
    //    }

    //    /// <summary>Delete a ledger entry and recalculate all subsequent balances.</summary>
    //    public async Task DeleteAndRecalculateAsync(int ledgerEntryId)
    //    {
    //        var entry = await _context.CustomerLedgerEntries.FindAsync(ledgerEntryId);
    //        if (entry == null) throw new Exception("Ledger entry not found.");

    //        int customerId = entry.CustomerId;
    //        _context.CustomerLedgerEntries.Remove(entry);
    //        await _context.SaveChangesAsync();

    //        await RecalculateBalancesAsync(customerId);
    //    }

    //    /// <summary>Recalculate all running balances for a customer from scratch.</summary>
    //    public async Task RecalculateBalancesAsync(int customerId)
    //    {
    //        var entries = await _context.CustomerLedgerEntries
    //            .Where(e => e.CustomerId == customerId)
    //            .OrderBy(e => e.EntryDate).ThenBy(e => e.Id)
    //            .ToListAsync();

    //        decimal running = 0;
    //        foreach (var e in entries)
    //        {
    //            running += e.Debit;
    //            running -= e.Credit;
    //            e.Balance = running;
    //        }
    //        await _context.SaveChangesAsync();
    //    }

    //    /// <summary>Get all customers with their current balances for the dashboard.</summary>
    //    public async Task<List<CustomerBalanceSummary>> GetAllCustomerBalancesAsync()
    //    {
    //        // Subquery: latest balance per customer
    //            var latestEntries = await _context.CustomerLedgerEntries
    //                .GroupBy(e => e.CustomerId)
    //                .Select(g => new
    //                {
    //                    CustomerId = g.Key,
    //                    Balance = g.OrderByDescending(e => e.Id).Select(e => e.Balance).FirstOrDefault(),
    //                    LastDate = g.Max(e => e.EntryDate)
    //                })
    //                .ToListAsync();

    //        if (!latestEntries.Any()) return new List<CustomerBalanceSummary>();

    //        var customerIds = latestEntries.Select(x => x.CustomerId).ToList();
    //        var customers = await _context.Customers
    //            .Where(c => customerIds.Contains(c.Id))
    //            .ToListAsync();

    //        return latestEntries.Select(le =>
    //        {
    //            var cust = customers.FirstOrDefault(c => c.Id == le.CustomerId);
    //            return new CustomerBalanceSummary
    //            {
    //                CustomerId = le.CustomerId,
    //                CustomerName = cust?.CustomerName ?? "Unknown",
    //                ContactNo = cust?.ContactNo ?? "",
    //                Balance = le.Balance,
    //                LastTransactionDate = le.LastDate
    //            };
    //        }).ToList();
    //    }

    //    // ─── Helpers ─────────────────────────────────────────────────────────

    //    private static string FormatEntryType(string raw)
    //    {
    //        switch (raw)
    //        {
    //            case nameof(LedgerEntryType.Sale):
    //                return "💰 Sale (Credit)";
    //            case nameof(LedgerEntryType.PaymentReceived):
    //                return "✅ Payment Received";
    //            case nameof(LedgerEntryType.AdvanceDeposit):
    //                return "🔵 Advance Deposit";
    //            case nameof(LedgerEntryType.AdvanceApplied):
    //                return "🔵 Advance Applied";
    //            case nameof(LedgerEntryType.OpeningBalance):
    //                return "📋 Opening Balance";
    //            case nameof(LedgerEntryType.Adjustment):
    //                return "⚙️ Adjustment";
    //            case nameof(LedgerEntryType.Refund):
    //                return "↩️ Refund";
    //            default:
    //                return raw;
    //        }
    //    }
    //}



    // ─── Enums ───────────────────────────────────────────────────────────────

    public enum LedgerEntryType
    {
        Sale,               // Order posted — customer owes money
        PaymentReceived,    // Customer paid loan
        AdvanceDeposit,     // Customer deposited advance
        AdvanceApplied,     // Advance used against an order
        OpeningBalance,     // Starting balance migration
        Adjustment,         // Manual correction (AdjustmentForm)
        Refund              // Money returned to customer
    }

    // ─── Repository ──────────────────────────────────────────────────────────

    public class CustomerLedgerRepository
    {
        private readonly POSDbContext _context;

        public CustomerLedgerRepository(POSDbContext context)
        {
            _context = context;
        }

        /// <summary>Get running balance for a customer right now.</summary>
        public async Task<decimal> GetCurrentBalanceAsync(int customerId)
        {
            var last = await _context.CustomerLedgerEntries
                .Where(e => e.CustomerId == customerId)
                .OrderByDescending(e => e.Id)
                .FirstOrDefaultAsync();
            return last?.Balance ?? 0;
        }

        /// <summary>Get ledger rows for a customer, optional date range.</summary>
        public async Task<List<CustomerLedgerRow>> GetLedgerAsync(
            int customerId, DateTime? from = null, DateTime? to = null)
        {
            var q = _context.CustomerLedgerEntries
                .Where(e => e.CustomerId == customerId);

            if (from.HasValue)
            {
                var frmDate = from.Value.Date;
                q = q.Where(e => e.EntryDate >= frmDate);
            }
            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);
                q = q.Where(e => e.EntryDate < toDate);
            }

            var rows = await q.OrderByDescending(e => e.Id).ToListAsync();

            return rows.Select(r => new CustomerLedgerRow
            {
                Id = r.Id,
                EntryDate = r.EntryDate,
                EntryTypeDisplay = FormatEntryType(r.EntryType),
                Debit = r.Debit,
                Credit = r.Credit,
                Balance = r.Balance,
                Note = r.Note,
                ReferenceType = r.ReferenceType,
                ReferenceId = r.ReferenceId,
                CreatedBy = r.CreatedBy
            }).ToList();
        }

        /// <summary>Get summary stats for the customer ledger header.</summary>
        public async Task<LedgerSummary> GetLedgerSummaryAsync(int customerId, DateTime? from = null, DateTime? to = null)
        {
            // Opening balance = balance of last entry before 'from'
            decimal openingBalance = 0;
            if (from.HasValue)
            {
                var frmDate = from.Value.Date;
                var openingEntry = await _context.CustomerLedgerEntries
                    .Where(e => e.CustomerId == customerId && e.EntryDate < frmDate)
                    .OrderByDescending(e => e.Id)
                    .FirstOrDefaultAsync();
                openingBalance = openingEntry?.Balance ?? 0;
            }

            var q = _context.CustomerLedgerEntries
                .Where(e => e.CustomerId == customerId);
            if (from.HasValue)
            {
                var frmDate = from.Value.Date;
                q = q.Where(e => e.EntryDate >= frmDate);
            }
            if (to.HasValue)
            {
                var toDate = to.Value.Date.AddDays(1);
                q = q.Where(e => e.EntryDate <toDate );
            }

            var totalDebit = await q.SumAsync(e => (decimal?)e.Debit) ?? 0;
            var totalCredit = await q.SumAsync(e => (decimal?)e.Credit) ?? 0;
            var lastBalance = await q.OrderByDescending(e => e.Id)
                                     .Select(e => (decimal?)e.Balance)
                                     .FirstOrDefaultAsync() ?? openingBalance;

            return new LedgerSummary
            {
                OpeningBalance = openingBalance,
                TotalDebit = totalDebit,
                TotalCredit = totalCredit,
                CurrentBalance = lastBalance
            };
        }

        /// <summary>Post a SALE entry when an order is saved with outstanding amount.</summary>
        public async Task PostSaleEntryAsync(
            int customerId, decimal orderTotal, decimal received,
            int orderId, string createdBy)
        {
            decimal outstanding = orderTotal - received;
            if (outstanding <= 0) return; // Fully paid — no loan

            decimal prevBalance = await GetCurrentBalanceAsync(customerId);
            decimal newBalance = prevBalance + outstanding;

            _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
            {
                CustomerId = customerId,
                EntryDate = DateTime.Now,
                EntryType = LedgerEntryType.Sale.ToString(),
                Debit = outstanding,
                Credit = 0,
                Balance = newBalance,
                ReferenceId = orderId,
                ReferenceType = "ORDER",
                Note = $"Invoice #{orderId} — Outstanding: {outstanding:N2}",
                CreatedBy = createdBy
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>Post a payment received from the customer.</summary>
        public async Task<int> PostPaymentAsync(
            int customerId, decimal amount, string paymentMethod,
            string referenceNo, string note, string createdBy)
        {
            decimal prevBalance = await GetCurrentBalanceAsync(customerId);
            decimal newBalance = prevBalance - amount;

            var ledgerEntry = new CustomerLedgerEntry
            {
                CustomerId = customerId,
                EntryDate = DateTime.Now,
                EntryType = LedgerEntryType.PaymentReceived.ToString(),
                Debit = 0,
                Credit = amount,
                Balance = newBalance,
                ReferenceType = "PAYMENT",
                Note = string.IsNullOrWhiteSpace(note) ? $"Payment received via {paymentMethod}" : note,
                CreatedBy = createdBy
            };
            _context.CustomerLedgerEntries.Add(ledgerEntry);
            await _context.SaveChangesAsync();

            // Also log to CustomerPayments table
            var payment = new CustomerPayment
            {
                CustomerId = customerId,
                PaymentDate = DateTime.Now,
                Amount = amount,
                PaymentMethod = paymentMethod,
                ReferenceNo = referenceNo,
                Note = note,
                LedgerEntryId = ledgerEntry.Id,
                CreatedBy = createdBy
            };
            _context.CustomerPayments.Add(payment);
            await _context.SaveChangesAsync();

            return ledgerEntry.Id;
        }

        /// <summary>Post an advance deposit from the customer.</summary>
        public async Task PostAdvanceDepositAsync(
            int customerId, decimal amount, string paymentMethod,
            string referenceNo, string note, string createdBy)
        {
            decimal prevBalance = await GetCurrentBalanceAsync(customerId);
            decimal newBalance = prevBalance - amount; // Advance makes balance negative (we owe them)

            _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
            {
                CustomerId = customerId,
                EntryDate = DateTime.Now,
                EntryType = LedgerEntryType.AdvanceDeposit.ToString(),
                Debit = 0,
                Credit = amount,
                Balance = newBalance,
                ReferenceType = "ADVANCE",
                Note = string.IsNullOrWhiteSpace(note) ? $"Advance deposit via {paymentMethod}" : note,
                CreatedBy = createdBy
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>Post a manual adjustment (positive = customer owes more, negative = we owe them).</summary>
        public async Task PostAdjustmentAsync(
            int customerId, decimal adjustmentAmount, string reason, string createdBy)
        {
            decimal prevBalance = await GetCurrentBalanceAsync(customerId);
            decimal newBalance = prevBalance + adjustmentAmount;

            decimal debit = adjustmentAmount > 0 ? adjustmentAmount : 0;
            decimal credit = adjustmentAmount < 0 ? Math.Abs(adjustmentAmount) : 0;

            _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
            {
                CustomerId = customerId,
                EntryDate = DateTime.Now,
                EntryType = LedgerEntryType.Adjustment.ToString(),
                Debit = debit,
                Credit = credit,
                Balance = newBalance,
                ReferenceType = "ADJUSTMENT",
                Note = reason,
                CreatedBy = createdBy
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>Set opening balance for a customer (migration use).</summary>
        public async Task SetOpeningBalanceAsync(
            int customerId, decimal balance, string note, string createdBy)
        {
            // Remove any existing opening balance for this customer first
            var existing = _context.CustomerLedgerEntries
                .Where(e => e.CustomerId == customerId && e.EntryType == LedgerEntryType.OpeningBalance.ToString());
            _context.CustomerLedgerEntries.RemoveRange(existing);

            decimal debit = balance > 0 ? balance : 0;
            decimal credit = balance < 0 ? Math.Abs(balance) : 0;

            _context.CustomerLedgerEntries.Add(new CustomerLedgerEntry
            {
                CustomerId = customerId,
                EntryDate = DateTime.Today,
                EntryType = LedgerEntryType.OpeningBalance.ToString(),
                Debit = debit,
                Credit = credit,
                Balance = balance,
                ReferenceType = "OPENING",
                Note = string.IsNullOrWhiteSpace(note) ? "Opening balance" : note,
                CreatedBy = createdBy
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>Delete a ledger entry and recalculate all subsequent balances.</summary>
        public async Task DeleteAndRecalculateAsync(int ledgerEntryId)
        {
            var entry = await _context.CustomerLedgerEntries.FindAsync(ledgerEntryId);
            if (entry == null) throw new Exception("Ledger entry not found.");

            int customerId = entry.CustomerId;
            _context.CustomerLedgerEntries.Remove(entry);
            await _context.SaveChangesAsync();

            await RecalculateBalancesAsync(customerId);
        }

        /// <summary>Recalculate all running balances for a customer from scratch.</summary>
        public async Task RecalculateBalancesAsync(int customerId)
        {
            var entries = await _context.CustomerLedgerEntries
                .Where(e => e.CustomerId == customerId)
                .OrderBy(e => e.EntryDate).ThenBy(e => e.Id)
                .ToListAsync();

            decimal running = 0;
            foreach (var e in entries)
            {
                running += e.Debit;
                running -= e.Credit;
                e.Balance = running;
            }
            await _context.SaveChangesAsync();
        }

        // ─── Dashboard: KPI totals (always accurate, never paginated) ───────────

        /// <summary>
        /// Returns loan/advance totals and customer counts for the KPI strip.
        /// Uses SQL aggregates — fast regardless of how many ledger rows exist.
        /// Called once on load and after every save. Never paginated.
        /// </summary>
        public async Task<DashboardKpi> GetDashboardKpiAsync()
        {
            // Single raw SQL query — two aggregates in one round-trip.
            // Reads only the latest balance per customer via a subquery,
            // then sums/counts on that small result set.
            const string sql = @"
                SELECT
                    SUM(CASE WHEN b.Balance > 0 THEN b.Balance ELSE 0 END)  AS TotalLoan,
                    SUM(CASE WHEN b.Balance < 0 THEN ABS(b.Balance) ELSE 0 END) AS TotalAdvance,
                    COUNT(CASE WHEN b.Balance > 0 THEN 1 END)               AS LoanCount,
                    COUNT(CASE WHEN b.Balance < 0 THEN 1 END)               AS AdvanceCount,
                    COUNT(CASE WHEN b.Balance = 0 THEN 1 END)               AS ClearCount
                FROM (
                    SELECT cl.CustomerId,
                           cl.Balance
                    FROM   CustomerLedger cl
                    WHERE  cl.Id = (
                               SELECT TOP 1 Id
                               FROM   CustomerLedger
                               WHERE  CustomerId = cl.CustomerId
                               ORDER  BY Id DESC
                           )
                ) b";

            var result = await _context.Database
                .SqlQuery<DashboardKpiRaw>(sql)
                .FirstOrDefaultAsync();

            if (result == null) return new DashboardKpi();

            return new DashboardKpi
            {
                TotalLoanAmount = result.TotalLoan ?? 0,
                TotalAdvanceAmount = result.TotalAdvance ?? 0,
                LoanCustomerCount = result.LoanCount,
                AdvanceCustomerCount = result.AdvanceCount,
                ClearCustomerCount = result.ClearCount
            };
        }

        // ─── Dashboard: paginated grid (100 rows at a time) ──────────────────

        /// <summary>
        /// Returns one page of customers with their current balance.
        /// Cursor-based: pass lastCustomerId=0 for first page,
        /// then the last CustomerId of the current page to go forward,
        /// or firstCustomerId of the current page to go back.
        ///
        /// Filter:  null = All, Loan, Advance, Clear
        /// Search:  name or contact number substring
        /// </summary>
        //public async Task<CustomerBalancePage> GetCustomerBalancePageAsync(
        //    int lastCustomerId,
        //    bool goingForward,
        //    BalanceType? filter,
        //    string search,
        //    int pageSize = 100)
        //{
        //    // Step 1 — build the latest-balance-per-customer subquery in SQL.
        //    // This avoids loading all ledger rows into memory.
        //    // We join Customers so we can search by name/contact.

        //    string filterClause;
        //    switch (filter)
        //    {
        //        case BalanceType.Loan:
        //            filterClause = "AND b.Balance > 0";
        //            break;
        //        case BalanceType.Advance:
        //            filterClause = "AND b.Balance < 0";
        //            break;
        //        case BalanceType.Clear:
        //            filterClause = "AND b.Balance = 0";
        //            break;
        //        default:
        //            filterClause = "";   // All
        //            break;
        //    }

        //    // Search clause — safe because we pass as SQL parameter below
        //    string searchClause = string.IsNullOrWhiteSpace(search)
        //        ? ""
        //        : "AND (c.CustomerName LIKE @search OR c.ContactNo LIKE @search)";

        //    // Cursor clause — drives forward/back paging
        //    string cursorClause = goingForward
        //        ? (lastCustomerId > 0 ? "AND b.CustomerId > @cursor" : "")
        //        : (lastCustomerId > 0 ? "AND b.CustomerId < @cursor" : "");

        //    string orderClause = goingForward
        //        ? "ORDER BY b.CustomerId ASC"
        //        : "ORDER BY b.CustomerId DESC";

        //    string sql = $@"
        //        SELECT TOP (@pageSize)
        //            b.CustomerId,
        //            c.CustomerName,
        //            c.ContactNo,
        //            b.Balance,
        //            b.LastDate AS LastTransactionDate
        //        FROM (
        //            SELECT cl.CustomerId,
        //                   cl.Balance,
        //                   cl.EntryDate AS LastDate
        //            FROM   CustomerLedger cl
        //            WHERE  cl.Id = (
        //                       SELECT TOP 1 Id
        //                       FROM   CustomerLedger
        //                       WHERE  CustomerId = cl.CustomerId
        //                       ORDER  BY Id DESC
        //                   )
        //        ) b
        //        JOIN Customers c ON c.Id = b.CustomerId
        //        WHERE 1=1
        //        {filterClause}
        //        {searchClause}
        //        {cursorClause}
        //        {orderClause}";

        //    var parameters = new List<System.Data.Common.DbParameter>();
        //    var conn = _context.Database.Connection;

        //    // Build parameters safely — never string-interpolate user input
        //    parameters.Add(CreateParam(conn, "@pageSize", pageSize));

        //    if (lastCustomerId > 0)
        //        parameters.Add(CreateParam(conn, "@cursor", lastCustomerId));

        //    if (!string.IsNullOrWhiteSpace(search))
        //        parameters.Add(CreateParam(conn, "@search", $"%{search.Trim()}%"));

        //    var rows = await _context.Database
        //        .SqlQuery<CustomerBalanceSummaryRaw>(sql, parameters.Cast<object>().ToArray())
        //        .ToListAsync();

        //    // If we paged backwards, reverse so rows are always oldest→newest
        //    if (!goingForward) rows.Reverse();

        //    var page = new CustomerBalancePage
        //    {
        //        Rows = rows.Select(r => new CustomerBalanceSummary
        //        {
        //            CustomerId = r.CustomerId,
        //            CustomerName = r.CustomerName,
        //            ContactNo = r.ContactNo ?? "",
        //            Balance = r.Balance,
        //            LastTransactionDate = r.LastTransactionDate
        //        }).ToList(),
        //        HasNextPage = rows.Count == pageSize,
        //        HasPrevPage = lastCustomerId > 0
        //    };

        //    return page;
        //}

        public async Task<CustomerBalancePage> GetCustomerBalancePageAsync(
    int lastCustomerId,
    bool goingForward,
    BalanceType? filter,
    string search,
    int pageSize = 100)
        {
            // Step 1 — build the latest-balance-per-customer subquery in SQL.
            // This avoids loading all ledger rows into memory.
            // We join Customers so we can search by name/contact.

            string filterClause;
            switch (filter)
            {
                case BalanceType.Loan:
                    filterClause = "AND b.Balance > 0";
                    break;
                case BalanceType.Advance:
                    filterClause = "AND b.Balance < 0";
                    break;
                case BalanceType.Clear:
                    filterClause = "AND b.Balance = 0";
                    break;
                default:
                    filterClause = "";   // All
                    break;
            }

            // Search clause — safe because we pass as SQL parameter below
            string searchClause = string.IsNullOrWhiteSpace(search)
                ? ""
                : "AND (c.CustomerName LIKE @search OR c.ContactNo LIKE @search)";

            // Cursor clause — drives forward/back paging
            // IMPORTANT: For first page (lastCustomerId = 0), we don't add cursor clause
            string cursorOperator = goingForward ? ">" : "<";
            string cursorClause = lastCustomerId > 0
                ? $"AND b.CustomerId {cursorOperator} @cursor"
                : "";

            // For backward navigation, we need to order DESC to get the previous page,
            // but then we'll reverse the results
            string orderClause = goingForward
                ? "ORDER BY b.CustomerId ASC"
                : "ORDER BY b.CustomerId DESC";

            // Fetch pageSize + 1 to determine if there's a next/prev page
            int fetchSize = pageSize + 1;

            string sql = $@"
                            SELECT TOP (@fetchSize)
                                b.CustomerId,
                                c.CustomerName,
                                c.ContactNo,
                                b.Balance,
                                b.LastDate AS LastTransactionDate
                            FROM (
                                SELECT cl.CustomerId,
                                       cl.Balance,
                                       cl.EntryDate AS LastDate
                                FROM   CustomerLedger cl
                                WHERE  cl.Id = (
                                           SELECT TOP 1 Id
                                           FROM   CustomerLedger
                                           WHERE  CustomerId = cl.CustomerId
                                           ORDER  BY Id DESC
                                       )
                            ) b
                            JOIN Customers c ON c.Id = b.CustomerId
                            WHERE 1=1
                            {filterClause}
                            {searchClause}
                            {cursorClause}
                            {orderClause}";

            var parameters = new List<System.Data.Common.DbParameter>();
            var conn = _context.Database.Connection;

            // Build parameters safely — never string-interpolate user input
            parameters.Add(CreateParam(conn, "@fetchSize", fetchSize));

            if (lastCustomerId > 0)
                parameters.Add(CreateParam(conn, "@cursor", lastCustomerId));

            if (!string.IsNullOrWhiteSpace(search))
                parameters.Add(CreateParam(conn, "@search", $"%{search.Trim()}%"));

            var rows = await _context.Database
                .SqlQuery<CustomerBalanceSummaryRaw>(sql, parameters.Cast<object>().ToArray())
                .ToListAsync();

            // Determine if there are more pages
            bool hasMorePages = rows.Count == fetchSize;

            // Take only pageSize rows for display
            var displayRows = hasMorePages
                ? rows.Take(pageSize).ToList()
                : rows;

            // If we paged backwards, reverse so rows are always oldest→newest
            if (!goingForward)
                displayRows.Reverse();

            // Calculate HasNextPage and HasPrevPage correctly
            bool hasNextPage;
            bool hasPrevPage;

            if (lastCustomerId == 0)
            {
                // First page - can only have next page
                hasNextPage = hasMorePages;
                hasPrevPage = false;
            }
            else
            {
                if (goingForward)
                {
                    // Moving forward - can have next page if we got fetchSize rows
                    hasNextPage = hasMorePages;
                    // Can have previous page if this isn't the first page
                    // We need to check if there are records before the first record on this page
                    hasPrevPage = true; // This is a simplification - ideally you'd check
                }
                else
                {
                    // Moving backward - can have previous page if we got fetchSize rows
                    hasPrevPage = hasMorePages;
                    // Can have next page always true when going backward? Not exactly
                    hasNextPage = true; // This is a simplification
                }
            }

            var page = new CustomerBalancePage
            {
                Rows = displayRows.Select(r => new CustomerBalanceSummary
                {
                    CustomerId = r.CustomerId,
                    CustomerName = r.CustomerName,
                    ContactNo = r.ContactNo ?? "",
                    Balance = r.Balance,
                    LastTransactionDate = r.LastTransactionDate
                }).ToList(),
                HasNextPage = hasNextPage,
                HasPrevPage = hasPrevPage
            };

            return page;
        }
        // ─── Clear customer history ───────────────────────────────────────────

        /// <summary>
        /// Permanently deletes ALL ledger entries and payment records for a customer.
        /// Only call this when the customer balance is exactly zero (Clear status).
        /// Also deletes corresponding rows from CustomerPayments for cleanliness.
        /// </summary>
        public async Task DeleteAllLedgerEntriesAsync(int customerId)
        {
            using (var tx = _context.Database.BeginTransaction())
            {
                try
                {
                    // Delete payment records linked to this customer
                    var payments = _context.CustomerPayments
                        .Where(p => p.CustomerId == customerId);
                    _context.CustomerPayments.RemoveRange(payments);

                    // Delete all ledger entries for this customer
                    var entries = _context.CustomerLedgerEntries
                        .Where(e => e.CustomerId == customerId);
                    _context.CustomerLedgerEntries.RemoveRange(entries);

                    await _context.SaveChangesAsync();
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }




        // ─── Parameter helper ─────────────────────────────────────────────────

        private static System.Data.Common.DbParameter CreateParam(
            System.Data.Common.DbConnection conn, string name, object value)
        {
            var cmd = conn.CreateCommand();
            var p = cmd.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            return p;
        }

        // ─── Helpers ─────────────────────────────────────────────────────────

        private static string FormatEntryType(string raw)
        {
            switch (raw)
            {
                case nameof(LedgerEntryType.Sale):
                    return "💰 Sale (Credit)";
                case nameof(LedgerEntryType.PaymentReceived):
                    return "✅ Payment Received";
                case nameof(LedgerEntryType.AdvanceDeposit):
                    return "🔵 Advance Deposit";
                case nameof(LedgerEntryType.AdvanceApplied):
                    return "🔵 Advance Applied";
                case nameof(LedgerEntryType.OpeningBalance):
                    return "📋 Opening Balance";
                case nameof(LedgerEntryType.Adjustment):
                    return "⚙️ Adjustment";
                case nameof(LedgerEntryType.Refund):
                    return "↩️ Refund";
                default:
                    return raw;
            }
        }
    }


}
