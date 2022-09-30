using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FileSyncCopyer
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] param)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmMain frm = new frmMain(param);

            bool checkMutex = true;
            if (frm.Setting != null)
            {
                checkMutex = frm.Setting.MutexEnable;
            }

            // Mutex制御で多重起動不可
            if (checkMutex && !MutexOperator.CreateMutex())
            {
                MessageBox.Show("多重起動不可");
                return;
            }
            else
            {
                Application.Run(new frmMain(param));
                MutexOperator.ReleaseMutex();
            }
        }
    }
}
