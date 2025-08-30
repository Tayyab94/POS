using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS_Shop.Views.Loader;
using System;
using System.Threading;
using System.Windows.Forms;


namespace POS_Shop.Helpers
{
    
    public static class LoadingManager
    {
        private static LoadingForm loadingForm = null;
        private static Thread loadingThread = null;

        public static void ShowLoading()
        {
            if (loadingForm != null)
                return; // Already shown

            loadingThread = new Thread(() =>
            {
                loadingForm = new LoadingForm();
                Application.Run(loadingForm);
            });
            loadingThread.SetApartmentState(ApartmentState.STA);
            loadingThread.IsBackground = true;
            loadingThread.Start();
        }

        public static void HideLoading()
        {
            if (loadingForm == null)
                return;

            loadingForm.Invoke(new Action(() =>
            {
                loadingForm.Close();
                loadingForm.Dispose();
                loadingForm = null;
            }));
            loadingThread.Join();
            loadingThread = null;
        }
    }

}
