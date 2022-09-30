using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FileSyncCopyer
{
    /// <summary>
    /// Mutex制御クラス
    /// </summary>
    public static class MutexOperator
    {
        private static Mutex _mutex = null;
        private static string MUTEX_NAME = "utsumi.com";

        /// <summary>
        /// Mutexの確認を行う
        /// </summary>
        /// <returns>Mutexが存在すればtrue</returns>
        private static bool IsExistsMutex()
        {
            bool ret = false;
            Mutex chkMutex = null;

            try
            {
                chkMutex = Mutex.OpenExisting(MUTEX_NAME);

                // Mutexが存在する
                ret = true;
            }
            catch (WaitHandleCannotBeOpenedException)
            {
                // Mutexが存在しない
                ret = false;
            }
            catch (UnauthorizedAccessException)
            {
                // Mutexが存在する
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Mutexを作成する
        /// </summary>
        /// <returns>既にMutexが存在して作成に失敗した場合にfalse</returns>
        public static bool CreateMutex()
        {
            bool ret = false;

            // Mutexの確認
            if (!IsExistsMutex())
            {
                // Mutexが存在しなければ作成
                _mutex = new Mutex(true, MUTEX_NAME);
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Mutexを解放する
        /// </summary>
        public static void ReleaseMutex()
        {
            if (_mutex != null)
            {
                _mutex.ReleaseMutex();
                _mutex.Close();
                _mutex = null;
            }
        }
    }
}
