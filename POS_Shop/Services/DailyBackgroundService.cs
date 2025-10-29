using POS_Shop.Helpers;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Services
{
    public class DailyBackgroundService : IDisposable
    {
        private System.Threading.Timer _dailyTimer;
        private readonly TimeSpan _scheduledTime = new TimeSpan(19, 0, 0); // Midnight
        private bool _disposed = false;


        public DailyBackgroundService()
        {
            InitializeScheduler();
        }

        public void InitializeScheduler()
        {

            var now = DateTime.Now;
            var todayScheduled = DateTime.Today.Add(_scheduledTime);

            try
            {
                if (now > todayScheduled)
                {
                    todayScheduled = todayScheduled.AddDays(1);
                }

                var initialInterval = todayScheduled - now;
                _dailyTimer = new System.Threading.Timer(
                    callback: _ => ExecuteDailyTask(),
                    state: null,
                    dueTime: initialInterval,
                    period: TimeSpan.FromHours(24) // Repeat every 24 hours
                    );

                Logger.LogMessage("Background Service started initialized to run at " + todayScheduled.ToString());
            }
            catch (Exception e)
            {
                Logger.LogMessage("Background Service started initialized to run at " + todayScheduled.ToString());
            }


        }

        private async void ExecuteDailyTask()
        {
            try
            {
                // Place your daily task logic here
                await Task.Run(() => PerformDailyOperations());
            }
            catch (Exception e)
            {

                throw;
            }

        }

        private void PerformDailyOperations()
        {
            DeleteTemSavedRecords();
            DeleteThreeMonthOldOrders();
        }
        private void DeleteTemSavedRecords()
        {
            try
            {
                using(var context = new POSDbContext())
                {

                    var cutOffDate=DateTime.Now.AddDays(-1);
                    var tempInvs = context.TempOrders.Where(s=>s.CreatedDate < cutOffDate).ToList();

                    var invIds=tempInvs.Select(s=>s.InvoiceNumber).ToList();
                    var tempOrderDetails = context.TempOrderDetails.Where(s=> invIds.Contains(s.TempInvoiceNumber)).ToList();   
                    context.TempOrderDetails.RemoveRange(tempOrderDetails);

                    context.TempOrders.RemoveRange(tempInvs);
                    context.SaveChanges();
                    Logger.LogMessage($"Deleted {tempInvs.Count} temporary saved records.");

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        private void DeleteThreeMonthOldOrders()
        {
            try
            {
                using (var context = new POSDbContext())
                {

                    //var cutOffDate = DateTime.Now.AddDays(-3);
                    //var orders = context.Orders.Where(s => s.CreatedDate < cutOffDate).ToList();

                    //var invIds = orders.Select(s => s.Id).ToList();
                    //var tempOrderDetails = context.OrderDetails.Where(s => invIds.Contains(s.OrderId)).ToList();
                    //context.OrderDetails.RemoveRange(tempOrderDetails);

                    //context.Orders.RemoveRange(orders);
                    //context.SaveChanges();
                    //Logger.LogMessage($"Deleted {orders.Count} orders records.");

                    using (var ctx = new POSDbContext())
                    {

                        // Now safely delete all products
                        ctx.Database.ExecuteSqlCommand(@"
                        DELETE
                        FROM OrderDetails
                        WHERE OrderId IN(
                            SELECT Id
                            FROM Orders   WHERE CreatedDate <= DATEADD(month, -3, GETDATE()));");

                        // Optional: Reset identity seed if needed
                        ctx.Database.ExecuteSqlCommand(@"DELETE FROM Orders   WHERE CreatedDate <= DATEADD(month, -3, GETDATE());");

                        Logger.LogMessage($"Data Cleanup Successful. All records older than 3 months have been permanently purged.");
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    _dailyTimer?.Dispose();
                }
                // Dispose unmanaged resources if any
                _disposed = true;
            }
        }
        ~DailyBackgroundService()
        {
            Dispose(false);
        }
    }
}
