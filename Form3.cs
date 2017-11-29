using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ThreadTest
{
    // 方式三 最簡單 不過可能會有同時存取問題
    public partial class Form3 : Form
    {
        Thread workThead = null;
        bool run = false;

        public Form3()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            run = true;
            workThead = new Thread(WorK);
            workThead.Start();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            run = false;
        }

        // 執行緒動作
        private void WorK()
        {
            while (run)
            {
                Thread.Sleep(200);
                oT01.AppendText(".");
            }

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            oT01.Text = "";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (workThead != null)
            {
                workThead.Abort();
                workThead.Join();
            }
            Application.Exit();
        }
    }
}
