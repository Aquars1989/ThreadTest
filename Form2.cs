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
    // 使用SynchronizationContext
    public partial class Form2 : Form
    {
        SynchronizationContext mainThread;
        Thread workThread = null;
        bool run = false;

        public Form2()
        {
            InitializeComponent();
            mainThread = SynchronizationContext.Current;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            run = true;
            workThread = new Thread(Work);
            workThread.Start();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            run = false;
        }

        // 執行緒動作
        private void Work()
        {
            while (run)
            {
                Thread.Sleep(200);
                // 呼叫主線程執行
                mainThread.Post((x) =>
                {
                    oT01.AppendText(".");
                }, null);
            }

            mainThread.Post((x) =>
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                oT01.Text = "";
            }, null);
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (workThread != null)
            {
                workThread.Abort();
                workThread.Join();
            }
            Application.Exit();
        }
    }
}
