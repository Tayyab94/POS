using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views
{
    public partial class InputDialog : Form
    {
        

        public string InputValue { get; private set; }

        public InputDialog(string message, string title, bool isTextBoxProtected=false)
        {
            InitializeComponent();
            this.Text = title;
            LabelMessage.Text = message;
            if(isTextBoxProtected)
            {
                InputTxt.UseSystemPasswordChar = true;
            }

        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            InputValue = InputTxt.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InputTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                OkBtn.PerformClick();
            }
        }
    }
}
