using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FileSyncCopyer
{
    /// <summary>
    /// 設定クラス
    /// </summary>
    public class FileSyncCopyerSetting
    {
        private string _mainPC = "";
        private string _server = "";
        private bool _overwrite = false;
        private bool _finChecked = false;
        private bool _mutexEnable = true;
        private string _historyFileName = "";
        private List<string> _additionalHistoryFilePath = new List<string>();
        private string _updateServer = "";
        private string _updateUser = "";
        private string _updatePass = "";

        /// <summary>
        /// メインPCのパス
        /// </summary>
        public string MainPC
        {
            get { return _mainPC; }
            set { _mainPC = value; }
        }

        /// <summary>
        /// サーバのパス
        /// </summary>
        public string Server
        {
            get { return _server; }
            set { _server = value; }
        }

        /// <summary>
        /// 強制上書きモード
        /// </summary>
        public bool Overwrite
        {
            get { return _overwrite; }
            set { _overwrite = value; }
        }

        /// <summary>
        /// 連載終了フラグ
        /// </summary>
        public bool FinChecked
        {
            get { return _finChecked; }
            set { _finChecked = value; }
        }

        /// <summary>
        /// 多重起動抑止
        /// </summary>
        public bool MutexEnable
        {
            get { return _mutexEnable; }
            set { _mutexEnable = value; }
        }

        /// <summary>
        /// 履歴ファイル名
        /// </summary>
        public string HistoryFileName
        {
            get { return _historyFileName; }
            set { _historyFileName = value; }
        }

        /// <summary>
        /// 追加履歴ファイル出力先
        /// </summary>
        [XmlArrayItem("FolderPath")]
        public List<string> AdditionalHistoryFilePath
        {
            get { return _additionalHistoryFilePath; }
            set { _additionalHistoryFilePath = value; }
        }

        public string UpdateServer
        {
            get { return _updateServer; }
            set { _updateServer = value; }
        }

        public string UpdateUser
        {
            get { return _updateUser; }
            set { _updateUser = value; }
        }

        public string UpdatePass
        {
            get { return _updatePass; }
            set { _updatePass = value; }
        }
    }
}
