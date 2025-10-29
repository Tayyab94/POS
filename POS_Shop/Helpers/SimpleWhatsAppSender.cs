using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Helpers
{
    public class SimpleWhatsAppSender
    {
        public void SendInvoice1(string phoneNumber, string customerName, string orderNumber, decimal totalAmount)
        {
            try
            {
                // 1. Clean the phone number (remove spaces, dashes, etc.)
                string cleanPhone = new string(phoneNumber.Where(char.IsDigit).ToArray());

                // 2. Create the message
                string message = $"Invoice #{orderNumber}\n\nDear {customerName},\n\nYour order total is ${totalAmount}. Please find your invoice attached.\n\nThank you!";

                // 3. Create WhatsApp URL
                string encodedMessage = Uri.EscapeDataString(message);
                //string whatsappUrl = $"https://wa.me/{cleanPhone}?text={encodedMessage}";

                string whatsappUrl = $"whatsapp://send?phone={cleanPhone}&text={encodedMessage}";

                // 4. Open WhatsApp
                Process.Start(new ProcessStartInfo
                {
                    FileName = whatsappUrl,
                    UseShellExecute = true
                });

                // 5. Show simple instruction
                MessageBox.Show("WhatsApp opened! Now manually attach the PDF file and press Send.",
                              "Attach PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //public void SendInvoice(string phoneNumber, string customerName, string orderNumber, decimal totalAmount, string pdfFilePath)
        //{
        //    try
        //    {
        //        // 1. Validate PDF file exists
        //        if (!File.Exists(pdfFilePath))
        //        {
        //            MessageBox.Show("PDF file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return;
        //        }

        //        // 2. Clean the phone number
        //        string cleanPhone = new string(phoneNumber.Where(char.IsDigit).ToArray());

        //        // 3. Create the message
        //        string message = $"Invoice #{orderNumber}\n\nDear {customerName},\n\nYour order total is ${totalAmount}. Please find your invoice attached.\n\nThank you!";
        //        string encodedMessage = Uri.EscapeDataString(message);

        //        // 4. Copy PDF file to clipboard
        //        var fileCollection = new System.Collections.Specialized.StringCollection();
        //        fileCollection.Add(pdfFilePath);
        //        Clipboard.SetFileDropList(fileCollection);

        //        // 5. Open WhatsApp
        //        string whatsappUrl = $"whatsapp://send?phone={cleanPhone}&text={encodedMessage}";

        //        Process.Start(new ProcessStartInfo
        //        {
        //            FileName = whatsappUrl,
        //            UseShellExecute = true
        //        });

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        public void SendInvoice(string phoneNumber, string orderNumber, decimal totalAmount, string pdfFilePath)
        {
            try
            {
                // 1. Validate PDF file exists
                if (!File.Exists(pdfFilePath))
                {
                    MessageBox.Show("PDF file not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 2. Clean the phone number
                string cleanPhone = new string(phoneNumber.Where(char.IsDigit).ToArray());

                // 3. Create the message
                string message = $"Invoice #{orderNumber}\n\nDear Customer,\n\nYour order total is Rs: {totalAmount}. Please find your invoice attached.\n\nThank you!";
                string encodedMessage = Uri.EscapeDataString(message);

                // 4. Open WhatsApp with message
                string whatsappUrl = $"whatsapp://send?phone={cleanPhone}&text={encodedMessage}";

                Process.Start(new ProcessStartInfo
                {
                    FileName = whatsappUrl,
                    UseShellExecute = true
                });

                // 5. Wait a moment then open file location
                Task.Delay(1000).ContinueWith(t =>
                {
                    Process.Start("explorer.exe", $"/select,\"{pdfFilePath}\"");
                });

                //// 6. Show clear attachment instructions
                //MessageBox.Show($"Follow these steps to attach the PDF:\n\n" +
                //               $"1. WhatsApp is now open with your message\n" +
                //               $"2. File Explorer opened with your PDF selected\n" +
                //               $"3. In WhatsApp: Click the 📎 PAPERCLIP icon\n" +
                //               $"4. Select 'Document' or 'File'\n" +
                //               $"5. Navigate to and select your PDF file\n" +
                //               $"6. Press SEND\n\n" +
                //               $"File: {Path.GetFileName(pdfFilePath)}",
                //               "Attach PDF File",
                //               MessageBoxButtons.OK,
                //               MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
