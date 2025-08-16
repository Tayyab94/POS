using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
          
            InitializeComponent();
            logoutBtn.Enabled = (bool)Properties.Settings.Default.IsLoggedIn ? true : false;
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            // Clear session
            Properties.Settings.Default.IsLoggedIn = false;
            Properties.Settings.Default.AuthToken = string.Empty;
            Properties.Settings.Default.UserName = string.Empty;
            Properties.Settings.Default.Save();


            this.Close();
        }
    }
}
