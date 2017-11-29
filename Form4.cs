using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadTest
{
    //方式四 Await Async 需.net 4.5以上 
    public partial class Form4 : Form
    {
        bool run = false;

        public Form4()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            run = true;
            var cot = await WorK();
            MessageBox.Show(cot.ToString());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            run = false;
        }

        //非同步動作
        async Task<int> WorK()
        {
            while (run)
            {
                await Task.Delay(200);
                oT01.AppendText(".");
            }
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            int result = oT01.TextLength;
            oT01.Text = "";
            return result;
        }
    }
}
