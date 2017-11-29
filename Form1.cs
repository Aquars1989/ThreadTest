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
    //方式一 傳統方式
    public partial class Form1 : Form
    {
        Thread threadWork = null;
        bool run = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            run = true;
            threadWork = new Thread(Work);
            threadWork.Start();

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            run = false;
        }


        //更新介面
        private delegate void deTextBoxStr(TextBox ctl, string cText);
        private void UpdateText(TextBox ctl, string cText)
        {
            if (this.InvokeRequired) // 如果不是在主執行緒中
            {
                deTextBoxStr Wok = new deTextBoxStr(UpdateText);
                this.Invoke(Wok, ctl, cText);  // 就在主執行緒中執行同函數
            }
            else
            {
                ctl.AppendText(cText); // 否則直接執行
            }
        }

        //通知停止
        private delegate void deNothing();
        private void Stoped()
        {
            if (this.InvokeRequired)
            {
                deNothing Wok = new deNothing(Stoped);
                this.Invoke(Wok);
            }
            else
            {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                oT01.Text = "";
            }
        }

        //執行緒動作
        private void Work()
        {

            while (run)
            {
                Thread.Sleep(200);
                UpdateText(oT01, ".");
            }
            Stoped();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (threadWork != null)
            {
                threadWork.Abort();
                threadWork.Join();
            }
            Application.Exit();
        }
    }
}
