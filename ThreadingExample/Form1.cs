using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadingExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int param = (int)e.Argument;
            for (int i = 0; i <=100; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Thread.Sleep(param * 25);
                backgroundWorker1.ReportProgress(i);

                //if (i>10)
                //{
                //    throw new ArgumentException("Sum Ting Wong");
                //}
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            if (e.Cancelled)
            {
                MessageBox.Show("Cancelled");
                progressBar1.Value = 0;
                return;
            }


            if (e.Error == null)
            {
                MessageBox.Show($"Thread {Thread.CurrentThread.ManagedThreadId} ended");
            }
            else
            {
                MessageBox.Show($"Thread {Thread.CurrentThread.ManagedThreadId} crashed and burned\n" + e.Error.Message);
                progressBar1.Value = 0;
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {

            int waitTime = 1;
            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync(waitTime);

            }
        }

        //tento môže volať GUI, ale nemá vytvárať vlastné vlákno
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
    }
}
