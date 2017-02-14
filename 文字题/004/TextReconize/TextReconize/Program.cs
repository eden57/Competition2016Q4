using System;
using System.Threading;
using System.Windows.Forms;

namespace TextReconize
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
            Application.ThreadException += Exeption_Handler;
        }

        static void Exeption_Handler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
