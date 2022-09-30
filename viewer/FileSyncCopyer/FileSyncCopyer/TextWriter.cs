using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FileSyncCopyer
{
    /// <summary>
    /// テキスト出力クラス
    /// </summary>
    public class TextWriter
    {
        static StreamWriter fsMain = null;
        static StreamWriter fsServer = null;

        string _main = "";
        string _server = "";
        bool serverAlive = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="main"></param>
        /// <param name="server"></param>
        public TextWriter(string main, string server, bool alive)
        {
            _main = main;
            _server = server;
            serverAlive = alive;

            if (!_main.EndsWith("\\"))
            {
                _main += "\\";
            }

            if (!_server.EndsWith("\\"))
            {
                _server += "\\";
            }
        }

        /// <summary>
        /// ファイルをオープンする
        /// </summary>
        /// <param name="mainFileName"></param>
        /// <param name="serverFileName"></param>
        /// <returns></returns>
        public bool Open(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            try
            {
                fsMain = new StreamWriter(_main + fileName, true, Encoding.UTF8);
                if (serverAlive)
                {
                    fsServer = new StreamWriter(_server + fileName, true, Encoding.UTF8);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ファイルをクローズする
        /// </summary>
        public void Close()
        {
            try
            {
                if (fsMain != null)
                {
                    fsMain.Close();
                    fsMain = null;
                }
            }
            catch
            {
            }

            try
            {
                if (fsServer != null)
                {
                    fsServer.Close();
                    fsServer = null;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// ファイルのコピー情報を出力
        /// </summary>
        /// <param name="kind">コピー先フォルダ</param>
        /// <param name="text">本文</param>
        public void CopyInfo(string kind, string text)
        {
            Writer("CopyInfo", kind, text);
        }

        /// <summary>
        /// 新規作成の情報
        /// </summary>
        /// <param name="kind">作成先フォルダ</param>
        /// <param name="bookTitle">タイトル</param>
        public void CreateInfo(string kind, string bookTitle)
        {
            Writer("CreateInfo", kind, bookTitle);
        }

        /// <summary>
        /// 移動情報
        /// </summary>
        /// <param name="srcKind">元フォルダ</param>
        /// <param name="dstKind">先フォルダ</param>
        /// <param name="bookTitle">タイトル</param>
        public void MoveInfo(string srcKind, string dstKind, string bookTitle)
        {
            Writer("MoveInfo", srcKind + " -> " + dstKind, bookTitle);
        }

        /// <summary>
        /// 実際の書き込み処理
        /// </summary>
        /// <param name="writeFormat"></param>
        /// <param name="text"></param>
        private void Writer(string writeFormat, string kind, string text)
        {
            string output = string.Format("[{0}][{1}][{2}]\t{3}", DateTime.Now.ToString("yyyy/MM/dd"), writeFormat, kind, text);

            fsMain.WriteLine(output);
            if (serverAlive)
            {
                fsServer.WriteLine(output);
            }
        }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~TextWriter()
        {
            if (fsMain != null)
            {
                fsMain.Close();
                fsMain = null;
            }

            if (fsServer != null)
            {
                fsServer.Close();
                fsServer = null;
            }
        }
    }
}
