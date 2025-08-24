using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Category
{
    public partial class CategoryForm : MainForm
    {
        
        public CategoryForm()
        {
       
            InitializeComponent();
        
        }

        private void categorySaveBtn_Click(object sender, EventArgs e)
        {

        }

        // Optional: If you want to enable the button when this form closes
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            // Notify the main form to update button states
            if (this.Owner is MainForm mainForm)
            {
                mainForm.UpdateButtonStates("CategoryForm");
            }

            base.OnFormClosed(e);
        }
    }
}
