using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermarket
{
    public partial class Splash : Form
    {
        Form1 log = new Form1();
        public int startPoint = 0;
        public Splash()
        {
            InitializeComponent();
        }

        private void bunifuProgressBar1_ProgressChanged(object sender, Bunifu.UI.WinForms.BunifuProgressBar.ProgressChangedEventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPoint +=2;
            bunifuProgressBar1.Value = startPoint;
            if (bunifuProgressBar1.Value == 100)
            { 
                bunifuProgressBar1.Value = 0;
                timer1.Stop();
                this.Hide();
                log.Show();
            }
        }
    }
}
