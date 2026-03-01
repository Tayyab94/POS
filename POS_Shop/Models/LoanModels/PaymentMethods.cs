using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Models.LoanModels
{

    public static class PaymentMethods
    {
        public const string Cash = "Cash";
        public const string BankTransfer = "BankTransfer";
        public const string Cheque = "Cheque";
        public const string MobilePayment = "MobilePayment";

        public static string ToDisplay(string method)
        {
            switch (method)
            {
                case Cash: return "💵 Cash";
                case BankTransfer: return "🏦 Bank Transfer";
                case Cheque: return "📄 Cheque";
                case MobilePayment: return "📱 Mobile Payment";
                default: return method;
            }
        }
    }
}
