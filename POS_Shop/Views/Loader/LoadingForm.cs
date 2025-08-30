using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Loader
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();

            // Optional: set properties programmatically here
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.StartPosition = FormStartPosition.CenterScreen;
            //this.TopMost = true;
            //this.ControlBox = false;
            //this.ShowInTaskbar = false;



            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.ControlBox = false;
            this.ShowInTaskbar = false;
            this.BackColor = System.Drawing.Color.White;
            this.Padding = new Padding(20);

        }
    }
}
