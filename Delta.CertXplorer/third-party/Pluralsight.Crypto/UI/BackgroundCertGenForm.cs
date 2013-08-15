using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Pluralsight.Crypto.UI
{
    public partial class BackgroundCertGenForm : Form
    {
        public SelfSignedCertProperties CertProperties { get; set; }
        public X509Certificate2 Certificate { get; set; }
        private bool backgroundWorkerRunning;
        private volatile int backgroundThreadId;

        public BackgroundCertGenForm()
        {
            InitializeComponent();
        }

        private void GenerateCertForm_Load(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
            tmTakingAwhile.Start();
            backgroundWorkerRunning = true;
        }

        private void GenerateCertForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (backgroundWorkerRunning)
            {
                // do our best to whack the background thread
                // note that this would be a very bad idea in a long-lived server process,
                // but in a client app, it's a nice way to give the user back her CPU.
                IntPtr hThread = OpenThread(1, false, backgroundThreadId);
                if (IntPtr.Zero != hThread)
                {
                    TerminateThread(hThread, 0);
                    CloseHandle(hThread);
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmTakingAwhile_Tick(object sender, EventArgs e)
        {
            pnlOne.Left = 1024; // move over so we can see what's underneath

            tmTakingAwhile.Stop();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundThreadId = GetCurrentThreadId();

            using (CryptContext ctx = new CryptContext())
            {
                ctx.Open();

                Certificate = ctx.CreateSelfSignedCertificate(CertProperties);
            }

            BeginInvoke(new Action(BackroundWorkerFinished), null);
        }

        private void BackroundWorkerFinished()
        {
            backgroundWorkerRunning = false;
            Close();
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern int GetCurrentThreadId();

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr OpenThread(int desiredAccess, [MarshalAs(UnmanagedType.Bool)] bool inheritHandle, int threadId);

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool TerminateThread(IntPtr hThread, int exitCode);

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);
    }
}
