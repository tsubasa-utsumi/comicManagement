using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Net.Http;
using System.Text;

namespace FileSyncCopyer
{
    public partial class frmMain : Form
    {
        #region フィールド

        #region 固定文字列

        private const string XML_FILE = "FileSyncCopyerSetting.xml";
        private const string DEFAULT_LOG_FILE = "History.txt";
        private const string WORD_NOW = "連載中";
        private const string WORD_FIN = "連載終了";

        #endregion

        /// <summary>
        /// 設定クラス
        /// </summary>
        private FileSyncCopyerSetting setting = null;

        /// <summary>
        /// ロックオブジェクト
        /// </summary>
        private object locker = new object();

        /// <summary>
        /// 前回検索した値
        /// </summary>
        private string _lastSearchWord = "";

        /// <summary>
        /// アクティブなコントロール
        /// </summary>
        Control activedControl = null;

        /// <summary>
        /// フォーカスの変更が可能か
        /// 検索テキストボックスにフォーカスがある場合はフォーカスの移動は禁止
        /// </summary>
        bool isFocusable = true;

        /// <summary>
        /// ファイルの変更があったか
        /// </summary>
        bool isModified = false;

        #endregion

        #region プロパティ

        /// <summary>
        /// 設定クラス読み込み
        /// </summary>
        public FileSyncCopyerSetting Setting
        {
            get { return setting; }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public frmMain(string[] param)
        {
            InitializeComponent();

            if (LoadSetting() == false)
            {
                setting = new FileSyncCopyerSetting();
                SaveSetting();
            }

            // テキストボックスの背景色変更イベント登録
            TextBoxColorChangeHandler += TextBoxColorChanger;

            SettingReflection();
            CreateTree();
            FileListReflection(param);
        }

        #endregion

        #region 設定

        /// <summary>
        /// 設定読み込み
        /// </summary>
        /// <returns></returns>
        private bool LoadSetting()
        {
            lock (locker)
            {
                if (File.Exists(XML_FILE))
                {
                    try
                    {
                        using (FileStream fs = new FileStream(XML_FILE, FileMode.Open))
                        {
                            XmlSerializer ser = new XmlSerializer(typeof(FileSyncCopyerSetting));
                            setting = ser.Deserialize(fs) as FileSyncCopyerSetting;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 設定保存
        /// </summary>
        /// <returns></returns>
        private bool SaveSetting()
        {
            lock (locker)
            {
                try
                {
                    using (FileStream fs = new FileStream(XML_FILE, FileMode.Create))
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(FileSyncCopyerSetting));
                        ser.Serialize(fs, setting);
                    }
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 画面表示

        /// <summary>
        /// 設定を画面に反映
        /// </summary>
        private void SettingReflection()
        {
            if (setting != null)
            {
                txtMain.Text = setting.MainPC;
                txtServer.Text = setting.Server;
                chkOverwrite.Checked = setting.Overwrite;
                if (setting.FinChecked)
                {
                    rbtnFin.Checked = true;
                }
                else
                {
                    rbtnNow.Checked = true;
                }
            }
        }

        /// <summary>
        /// フォルダツリー作成
        /// </summary>
        private void CreateTree()
        {
            if (string.IsNullOrEmpty(txtMain.Text))
            {
                return;
            }

            ShowContents(null);
            treeFolder.Nodes.Clear();

            TreeNode trNow = new TreeNode(WORD_NOW);
            string nowPath = txtMain.Text.TrimEnd('\\') + "\\" + WORD_NOW;

            foreach (string path in Directory.GetDirectories(nowPath))
            {
                trNow.Nodes.Add(new TreeNode(Path.GetFileName(path)));
            }

            TreeNode trFin = new TreeNode(WORD_FIN);
            string finPath = txtMain.Text.TrimEnd('\\') + "\\" + WORD_FIN;

            foreach (string path in Directory.GetDirectories(finPath))
            {
                trFin.Nodes.Add(new TreeNode(Path.GetFileName(path)));
            }

            treeFolder.Nodes.Add(trNow);
            treeFolder.Nodes.Add(trFin);
        }

        /// <summary>
        /// フォルダ内のコンテンツを表示する
        /// </summary>
        private void ShowContents(string param)
        {
            // 表示内容をリセットするため、表示を空にする
            lstContents.Items.Clear();
            lblFileCount.Text = "";

            // 引数が無効なら終了
            if (string.IsNullOrEmpty(param))
            {
                return;
            }

            string folderPath = txtMain.Text + "\\" + param;

            // フォルダが存在しなければ終了
            if (!Directory.Exists(folderPath))
            {
                return;
            }

            // フォルダの中身が無い場合終了
            string[] contents = Directory.GetFiles(folderPath);
            if (contents == null)
            {
                return;
            }

            // ファイルの中身を表示
            foreach (string content in contents)
            {
                lstContents.Items.Add(Path.GetFileName(content));
            }

            // ファイル数の表示
            lblFileCount.Text = string.Format("ファイル数： {0}", contents.Length);

            // クリップボードにファイル名をコピー
            SetClipboard();
        }

        /// <summary>
        /// ファイルパス配列をリストに追加
        /// </summary>
        /// <param name="files"></param>
        private void FileListReflection(string[] files)
        {
            if (files == null || files.Length == 0)
            {
                return;
            }

            // 配列をソート
            Array.Sort(files);
            foreach (string file in files)
            {
                if (!lstFiles.Items.Contains(file))
                {
                    if (File.Exists(file))
                    {
                        // 既にリストに含まれておらず、ファイルの存在が確認出来た場合にリストに追加
                        lstFiles.Items.Add(file);
                    }
                }
            }

            // 作成フォルダを初期化
            txtCreateFolder.Text = FilePathToFolderPath(lstFiles.Items[0].ToString());
            // ファイル数の表示
            lblCount.Text = string.Format("ファイル数： {0}", lstFiles.Items.Count);
        }

        #endregion

        #region フォルダ選択

        /// <summary>
        /// メインPCのフォルダ指定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMain_Click(object sender, EventArgs e)
        {
            string selectedPath = SelectFolder(txtMain.Text);
            if (!string.IsNullOrEmpty(selectedPath))
            {
                setting.MainPC = selectedPath;
                SaveSetting();
                SettingReflection();
            }
        }

        /// <summary>
        /// サーバのフォルダ指定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnServer_Click(object sender, EventArgs e)
        {
            string selectedPath = SelectFolder(txtServer.Text);
            if (!string.IsNullOrEmpty(selectedPath))
            {
                setting.Server = selectedPath;
                SaveSetting();
                SettingReflection();
            }
        }

        /// <summary>
        /// フォルダ選択処理
        /// </summary>
        /// <param name="initPath"></param>
        /// <returns></returns>
        private string SelectFolder(string initPath)
        {
            string selectedPath = "";

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(initPath))
            {
                fbd.SelectedPath = initPath;
            }

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                selectedPath = fbd.SelectedPath;
            }

            return selectedPath;
        }

        #endregion

        #region ボタン押下処理

        /// <summary>
        /// 実行ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (chkVariety.Checked == false &&
                FilePathToFolderPath(lstFiles.Items[0].ToString()) != txtCreateFolder.Text)
            {
                if (MessageBox.Show("ファイル名とフォルダ名に食い違いがあります\r\nこのまま処理を続行しますか？", "コピー先確認",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            // サーバ側の生存確認
            bool serverAlive = Directory.Exists(txtServer.Text);

            TextWriter logger = new TextWriter(txtMain.Text, txtServer.Text, serverAlive);
            if (!logger.Open(setting.HistoryFileName))
            {
                if (!logger.Open(DEFAULT_LOG_FILE))
                {
                    logger = null;
                }
            }

            string targetStatus = "";
            if (rbtnNow.Checked)
            {
                targetStatus = WORD_NOW;
            }
            else
            {
                targetStatus = WORD_FIN;
            }

            string mainPCFolder = string.Format("{0}\\{1}\\{2}\\", txtMain.Text, targetStatus, txtCreateFolder.Text);
            string serverFolder = string.Format("{0}\\{1}\\{2}\\", txtServer.Text, targetStatus, txtCreateFolder.Text);

            // 失敗したファイル
            List<string> failedFiles = new List<string>();

            foreach (object listItem in lstFiles.Items)
            {
                if (listItem is string)
                {
                    // バラエティモード時は毎回対象のフォルダを作る
                    if (chkVariety.Checked)
                    {
                        txtCreateFolder.Text = FilePathToFolderPath(listItem.ToString());

                        mainPCFolder = string.Format("{0}\\{1}\\{2}\\", txtMain.Text, targetStatus, txtCreateFolder.Text);
                        serverFolder = string.Format("{0}\\{1}\\{2}\\", txtServer.Text, targetStatus, txtCreateFolder.Text);

                        if (!(Directory.Exists(mainPCFolder) && Directory.Exists(serverFolder)))
                        {
                            failedFiles.Add(listItem.ToString());
                            continue;
                        }
                    }

                    // コピー先フォルダが無ければ作る
                    bool isCreated = false;
                    if (!Directory.Exists(mainPCFolder))
                    {
                        Directory.CreateDirectory(mainPCFolder);
                        isCreated = true;
                    }

                    if (serverAlive)
                    {
                        if (!Directory.Exists(serverFolder))
                        {
                            Directory.CreateDirectory(serverFolder);
                            isCreated = true;
                        }
                    }

                    if (isCreated)
                    {
                        logger.CreateInfo(targetStatus, txtCreateFolder.Text);
                    }

                    string srcPath = listItem.ToString();
                    string mainPCPath = mainPCFolder + Path.GetFileName(srcPath);
                    string serverPath = serverFolder + Path.GetFileName(srcPath);

                    bool isSure = false;
                    try
                    {
                        // 強制上書きしない場合
                        if (!chkOverwrite.Checked)
                        {
                            if (File.Exists(mainPCPath) || File.Exists(serverPath))
                            {
                                if (MessageBox.Show("同名のファイルが存在します。上書きしますか？\r\n" + Path.GetFileName(srcPath), "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                                {
                                    failedFiles.Add(listItem.ToString());
                                    continue;
                                }
                            }
                        }

                        // ファイルのコピー
                        File.Copy(srcPath, mainPCPath, true);
                        if (serverAlive)
                        {
                            File.Copy(srcPath, serverPath, true);
                        }

                        // 変更フラグをON
                        isModified = true;

                        // ログ出力
                        if (logger != null)
                        {
                            string log = string.Format("{0}\\{1}", txtCreateFolder.Text, Path.GetFileName(srcPath));
                            logger.CopyInfo(targetStatus, log);
                        }
                    }
                    catch (Exception exc)
                    {
                        // 例外発生
                        if (MessageBox.Show("ファイルのコピーに失敗しました", "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                        {
                            isSure = true;

                            // ログクラスの廃棄
                            if (logger != null)
                            {
                                logger.Close();
                            }

                            return;
                        }
                    }

                    // コピーしたファイルの存在を確認
                    if ((File.Exists(mainPCPath) == false || (serverAlive && File.Exists(serverPath) == false)) && isSure == false)
                    {
                        if (MessageBox.Show("ファイルのコピーに失敗しました", "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                        {
                            // ログクラスの廃棄
                            if (logger != null)
                            {
                                logger.Close();
                            }
                            return;
                        }
                    }
                    else
                    {
                        // コピーに成功していたら元ファイルを削除
                        File.Delete(srcPath);
                    }
                }
            }

            // コピー失敗したファイルを表示
            lstFiles.Items.Clear();
            if (failedFiles.Count > 0)
            {
                MessageBox.Show("コピーに失敗したファイルを表示します");
                foreach (string failed in failedFiles)
                {
                    lstFiles.Items.Add(failed);
                }
            }

            // ファイルツリーの再作成
            CreateTree();

            // ログクラスの廃棄
            if (logger != null)
            {
                logger.Close();
            }
        }

        /// <summary>
        /// 検索ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            Regex regex = null;
            try
            {
                regex = new Regex(string.Format(".*{0}.*", txtSearch.Text), RegexOptions.IgnoreCase);
            }
            catch
            {
                return;
            }

            // 候補リストを作成する
            List<TreeNode> trList = new List<TreeNode>();
            foreach (TreeNode trp in treeFolder.Nodes)
            {
                foreach (TreeNode tr in trp.Nodes)
                {
                    if (regex.IsMatch(tr.Text))
                    {
                        trList.Add(tr);
                    }
                }
            }

            if (trList.Count > 0)
            {
                // 一番最初のノードをとりあえず選ぶ
                TreeNode selectNode = trList[0];

                // 検索条件が前回と同じ場合、次の候補を選ぶ
                // 次の候補が無ければ自動的に一番最初のノードになる
                if (_lastSearchWord == txtSearch.Text)
                {
                    bool isPreviewSelected = false;
                    foreach (TreeNode tr in trList)
                    {
                        if (isPreviewSelected)
                        {
                            selectNode = tr;
                            break;
                        }

                        if (tr.IsSelected)
                        {
                            isPreviewSelected = true;
                        }
                    }
                }

                treeFolder.SelectedNode = selectNode;
                treeFolder.Focus();

                // フォルダ内容の表示
                ShowContents(treeFolder.SelectedNode.Parent.Text + "\\" + treeFolder.SelectedNode.Text);
            }
            else
            {
                MessageBox.Show("候補が見つかりませんでした");
            }

            _lastSearchWord = txtSearch.Text;
        }

        /// <summary>
        /// クリアボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            lstFiles.Items.Clear();
        }

        #endregion

        #region リスト、ツリービュー選択

        /// <summary>
        /// ツリービュー選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // 親ノードなら終了
            if (treeFolder.SelectedNode.Text == WORD_NOW ||
                treeFolder.SelectedNode.Text == WORD_FIN)
            {
                // フォルダ内容をリセット
                ShowContents(null);
                return;
            }

            // 子ノードじゃなけりゃ終了(ないだろうけど)
            if (treeFolder.SelectedNode.Parent.Text != WORD_NOW &&
                treeFolder.SelectedNode.Parent.Text != WORD_FIN)
            {
                // フォルダ内容をリセット
                ShowContents(null);
                return;
            }

            // 親ノードに対応するボタンをチェック
            if (treeFolder.SelectedNode.Parent.Text == WORD_NOW)
            {
                rbtnNow.Checked = true;
            }
            else if (treeFolder.SelectedNode.Parent.Text == WORD_FIN)
            {
                rbtnFin.Checked = true;
            }

            // コピー対象のフォルダ名を入力
            txtCreateFolder.Text = treeFolder.SelectedNode.Text;

            // フォルダ内容の表示
            ShowContents(treeFolder.SelectedNode.Parent.Text + "\\" + treeFolder.SelectedNode.Text);
        }

        /// <summary>
        /// ツリービューのダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // フォルダを開く
            OpenFolder(TargetFolder.MainPC);
        }

        /// <summary>
        /// ツリービューを右クリックでもアイテム選択できるようにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeFolder_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                treeFolder.SelectedNode = treeFolder.GetNodeAt(e.X, e.Y);
            }
        }

        /// <summary>
        /// リスト選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstFiles.SelectedItem == null)
            {
                return;
            }

            // コピー対象のフォルダに選択した結果を代入
            txtCreateFolder.Text = FilePathToFolderPath(lstFiles.SelectedItem.ToString());
        }

        /// <summary>
        /// ファイル一覧選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstContents_Click(object sender, EventArgs e)
        {
            SetClipboard();
        }

        #endregion

        #region キーボード押下処理

        #region エンターキー対応

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, null);
            }
        }

        private void txtCreateFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnRun_Click(sender, null);
            }
        }

        private void txtMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnMain_Click(sender, null);
            }
        }

        private void txtServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnServer_Click(sender, null);
            }
        }

        private void treeFolder_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch_Click(sender, null);
            }
        }

        /// <summary>
        /// 全てのコントロールのダイアログキー押下時処理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                // Ctrl+Fが押されたら、検索テキストボックスにフォーカスを移動
                case Keys.Control | Keys.F:
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        /// <summary>
        /// ファイル一覧でキーが押された場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFiles_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                // Deleteキーの場合は選択中のファイルをリストから削除
                int index = lstFiles.SelectedIndex;
                if (index > -1)
                {
                    lstFiles.Items.RemoveAt(index);

                    // 削除した後、ファイルを選択しなおす
                    if (lstFiles.Items.Count > index)
                    {
                        lstFiles.SelectedIndex = index;
                    }
                    else
                    {
                        lstFiles.SelectedIndex = index - 1;
                    }
                }
            }
        }

        #endregion

        #region Drag & Drop

        /// <summary>
        /// リストにドラッグされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFiles_DragEnter(object sender, DragEventArgs e)
        {
            string[] drop = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (drop != null && drop.Length > 0)
            {
                foreach (string file in drop)
                {
                    if (!File.Exists(file))
                    {
                        return;
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }
        }

        /// <summary>
        /// リストにドロップされた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstFiles_DragDrop(object sender, DragEventArgs e)
        {
            FileListReflection((string[])e.Data.GetData(DataFormats.FileDrop));
        }

        #endregion

        #region コントロールの値変更イベント

        #region コピー対象フォルダーか、コピー対象の連載中・連載終了が変更されたらフォルダチェックを行う

        private void txtCreateFolder_TextChanged(object sender, EventArgs e)
        {
            ExistsCheck();
        }

        private void rbtnNow_CheckedChanged(object sender, EventArgs e)
        {
            ExistsCheck();
        }

        private void rbtnFin_CheckedChanged(object sender, EventArgs e)
        {
            ExistsCheck();
        }

        #endregion

        #region メインPC・サーバのフォルダが変更されたら保存する

        private void txtMain_TextChanged(object sender, EventArgs e)
        {
            setting.MainPC = txtMain.Text;
            SaveSetting();

            // フォルダツリーの再構築を行う
            CreateTree();
        }

        private void txtServer_TextChanged(object sender, EventArgs e)
        {
            setting.Server = txtServer.Text;
            SaveSetting();
        }

        #endregion

        #region 強制上書きのチェックが変更されたら保存する

        private void chkOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            setting.Overwrite = chkOverwrite.Checked;
            SaveSetting();
        }

        #endregion

        #endregion

        #region マウスがリストかツリービュー上に来たらフォーカスを当てる

        #region 検索テキストボックスにフォーカスがある場合はフォーカスの移動をしない

        /// <summary>
        /// 検索テキストボックスにフォーカスが当たった場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            // フォーカスの移動を禁止する
            isFocusable = false;
        }

        /// <summary>
        /// 検索テキストボックスからフォーカスが離れた場合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_Leave(object sender, EventArgs e)
        {
            // フォーカスの移動を許可する
            isFocusable = true;
        }

        #endregion

        #region マウスがコントロール上に来た場合

        private void treeFolder_MouseEnter(object sender, EventArgs e)
        {
            EnterFocus(treeFolder);
        }

        private void lstContents_MouseEnter(object sender, EventArgs e)
        {
            EnterFocus(lstContents);
        }

        private void lstFiles_MouseEnter(object sender, EventArgs e)
        {
            EnterFocus(lstFiles);
        }

        #endregion

        #region マウスがコントロール上に無くなった場合

        private void treeFolder_MouseLeave(object sender, EventArgs e)
        {
            LeaveFocus();
        }

        private void lstContents_MouseLeave(object sender, EventArgs e)
        {
            LeaveFocus();
        }

        private void lstFiles_MouseLeave(object sender, EventArgs e)
        {
            LeaveFocus();
        }

        #endregion

        /// <summary>
        /// フォーカスを移動する
        /// </summary>
        /// <param name="ctr"></param>
        private void EnterFocus(Control ctr)
        {
            // フォーカスの移動が禁止されていない場合のみ実行
            if (isFocusable)
            {
                if (this.ActiveControl is TextBox)
                {
                    // 前のフォーカスがテキストボックスの場合だけ覚えておく
                    activedControl = this.ActiveControl;
                }
                else
                {
                    activedControl = null;
                }

                ctr.Focus();
            }
        }

        /// <summary>
        /// 前のフォーカスに戻す
        /// </summary>
        private void LeaveFocus()
        {
            if (activedControl != null)
            {
                activedControl.Focus();
                activedControl = null;
            }
        }

        #endregion

        #region 汎用内部メソッド

        /// <summary>
        /// ファイルパスからフォルダパスを生成する
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private string FilePathToFolderPath(string filePath)
        {
            string ret = "";

            // リストの項目から拡張子抜きのファイル名をコピー対象のフォルダ名に入力
            ret = Path.GetFileNameWithoutExtension(filePath);

            // 正規表現で「 第n(数字1～3桁)巻」を取り除く
            Regex reg = new Regex(@" 第\d{1,3}巻", RegexOptions.IgnoreCase);
            if (reg.IsMatch(filePath))
            {
                ret = ret.Replace(reg.Match(filePath).Value, "");
            }

            return ret;
        }

        /// <summary>
        /// 開くフォルダーの種類
        /// </summary>
        private enum TargetFolder
        {
            MainPC,
            Server
        };

        /// <summary>
        /// フォルダを開く
        /// </summary>
        /// <param name="target"></param>
        private void OpenFolder(TargetFolder target)
        {
            // 親ノードなら終了
            if (treeFolder.SelectedNode == null ||
                treeFolder.SelectedNode.Text == WORD_NOW ||
                treeFolder.SelectedNode.Text == WORD_FIN)
            {
                return;
            }

            // 子ノードじゃなけりゃ終了(ないだろうけど)
            if (treeFolder.SelectedNode.Parent.Text != WORD_NOW &&
                treeFolder.SelectedNode.Parent.Text != WORD_FIN)
            {
                return;
            }

            // フォルダを開く
            string targetFolder = target == TargetFolder.MainPC ? txtMain.Text : txtServer.Text;
            string openFolder = string.Format("{0}\\{1}\\{2}", targetFolder, treeFolder.SelectedNode.Parent.Text, treeFolder.SelectedNode.Text);
            if (Directory.Exists(openFolder))
            {
                System.Diagnostics.Process.Start(openFolder);
            }
        }

        /// <summary>
        /// フォルダの移動先
        /// </summary>
        private enum MoveTo
        {
            Now,
            Fin
        };

        /// <summary>
        /// フォルダの移動
        /// </summary>
        /// <param name="destination"></param>
        private void MoveFolder(MoveTo destination)
        {
            // 親ノードなら終了
            if (treeFolder.SelectedNode.Text == WORD_NOW ||
                treeFolder.SelectedNode.Text == WORD_FIN)
            {
                return;
            }

            // 子ノードじゃなけりゃ終了(ないだろうけど)
            if (treeFolder.SelectedNode.Parent.Text != WORD_NOW &&
                treeFolder.SelectedNode.Parent.Text != WORD_FIN)
            {
                return;
            }

            string srcTarget = destination == MoveTo.Fin ? WORD_NOW : WORD_FIN;
            string dstTarget = destination == MoveTo.Fin ? WORD_FIN : WORD_NOW;

            // 移動前後のフォルダパス作成
            string srcMainFolder = string.Format("{0}\\{1}\\{2}", txtMain.Text, srcTarget, treeFolder.SelectedNode.Text);
            string dstMainFolder = string.Format("{0}\\{1}\\{2}", txtMain.Text, dstTarget, treeFolder.SelectedNode.Text);
            string srcServerFolder = string.Format("{0}\\{1}\\{2}", txtServer.Text, srcTarget, treeFolder.SelectedNode.Text);
            string dstServerFolder = string.Format("{0}\\{1}\\{2}", txtServer.Text, dstTarget, treeFolder.SelectedNode.Text);

            // フォルダチェック
            if (!Directory.Exists(srcMainFolder) ||
                 Directory.Exists(dstMainFolder) ||
                !Directory.Exists(srcServerFolder) ||
                 Directory.Exists(dstServerFolder))
            {
                return;
            }

            // フォルダ移動
            Directory.Move(srcMainFolder, dstMainFolder);
            Directory.Move(srcServerFolder, dstServerFolder);

            // 変更フラグをON
            isModified = true;

            // ツリービュー再作成
            CreateTree();
        }

        /// <summary>
        /// クリップボードにファイル名をコピーする
        /// </summary>
        private void SetClipboard()
        {
            if (lstContents.Items.Count > 0)
            {
                // クリップボードにファイル名をコピーする
                Clipboard.SetText(Path.GetFileNameWithoutExtension(lstContents.Items[lstContents.Items.Count - 1].ToString()));
            }
        }

        #endregion

        #region イベント関連

        #region テキストボックスの背景色変更イベント

        /// <summary>
        /// テキストボックスの背景色変更デリゲート
        /// </summary>
        /// <param name="mainColor"></param>
        /// <param name="serverColor"></param>
        delegate void TextBoxColorChange(Color mainColor, Color serverColor);

        /// <summary>テキストボックスの背景色変更イベントハンドラー</summary>
        private event TextBoxColorChange TextBoxColorChangeHandler = delegate(Color a, Color b) { };
        /// <summary>テキストボックスの背景色変更イベント専用サブスレッド</summary>
        private Thread ChangeTextBoxColorThread = null;

        /// <summary>
        /// フォルダ存在チェックを別スレッドで実行
        /// </summary>
        private void ExistsCheck()
        {
            if (ChangeTextBoxColorThread != null)
            {
                // 既に存在する場合は中止する
                ChangeTextBoxColorThread.Abort();
                ChangeTextBoxColorThread = null;
            }

            // サブスレッドの内容作成と実行
            ChangeTextBoxColorThread = new Thread(delegate() { ExistsCheckEvent(this); });
            ChangeTextBoxColorThread.Start();
        }

        /// <summary>
        /// フォルダの存在をチェックして色を返る
        /// </summary>
        private void ExistsCheckEvent(Form mainForm)
        {
            // 空なら背景を白にする
            if (string.IsNullOrEmpty(txtCreateFolder.Text))
            {
                txtMain.BackColor = Color.White;
                txtServer.BackColor = Color.White;
                return;
            }

            // チェックするパスを取得
            string targetStatus = "";
            if (rbtnNow.Checked)
            {
                targetStatus = WORD_NOW;
            }
            else
            {
                targetStatus = WORD_FIN;
            }
            string mainPCFolder = string.Format("{0}\\{1}\\{2}\\", txtMain.Text, targetStatus, txtCreateFolder.Text);
            string serverFolder = string.Format("{0}\\{1}\\{2}\\", txtServer.Text, targetStatus, txtCreateFolder.Text);

            // テキストボックスの色
            Color mainColor = Color.GreenYellow;
            Color serverColor = Color.GreenYellow;

            // メインPC側のフォルダをチェック
            if (Directory.Exists(mainPCFolder))
            {
                mainColor = Color.GreenYellow;
            }
            else
            {
                mainColor = Color.HotPink;
            }

            // サーバ側のフォルダをチェック
            if (Directory.Exists(serverFolder))
            {
                serverColor = Color.GreenYellow;
            }
            else
            {
                serverColor = Color.HotPink;
            }

            // テキストボックスの背景色を変更するため、メインスレッドと同期
            mainForm.BeginInvoke(TextBoxColorChangeHandler, new object[] { mainColor, serverColor });
        }

        /// <summary>
        /// テキストボックスの背景色を変更
        /// </summary>
        /// <param name="mainColor"></param>
        /// <param name="serverColor"></param>
        private void TextBoxColorChanger(Color mainColor, Color serverColor)
        {
            txtMain.BackColor = mainColor;
            txtServer.BackColor = serverColor;
        }

        #endregion

        /// <summary>
        /// フォーム終了処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ChangeTextBoxColorThread != null)
            {
                // テキストボックスの背景色変更イベントを中止する
                ChangeTextBoxColorThread.Abort();
                ChangeTextBoxColorThread = null;
            }

            // テキストボックスの背景色変更イベント登録解除
            TextBoxColorChangeHandler -= TextBoxColorChanger;

            // ファイル変更があった場合の処理を行う
            if (isModified)
            {
                // 履歴ファイルを追加コピーする
                if (setting.AdditionalHistoryFilePath != null)
                {
                    string originalHistoryFile = Path.Combine(txtMain.Text, setting.HistoryFileName);
                    foreach (string addPath in setting.AdditionalHistoryFilePath)
                    {
                        if (!Directory.Exists(addPath))
                        {
                            continue;
                        }

                        string addHistoryFile = Path.Combine(addPath, setting.HistoryFileName);
                        try
                        {
                            File.Copy(originalHistoryFile, addHistoryFile, true);
                        }
                        catch
                        {
                        }
                    }
                }

                // サーバーの新作更新をする
                if (string.IsNullOrEmpty(setting.UpdateServer) || string.IsNullOrEmpty(setting.UpdateUser) || string.IsNullOrEmpty(setting.UpdatePass)) return;

                var req = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(setting.UpdateServer)
                };
                req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", setting.UpdateUser, setting.UpdatePass)))
                );

                using (var client = new HttpClient())
                {
                    await client.SendAsync(req);
                }
            }
        }

        #endregion

        #region 右クリックメニュー

        /// <summary>
        /// メインPCのフォルダを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenMainPCFolder(object sender, EventArgs e)
        {
            OpenFolder(TargetFolder.MainPC);
        }

        /// <summary>
        /// サーバーのフォルダを開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OpenServerFolder(object sender, EventArgs e)
        {
            OpenFolder(TargetFolder.Server);
        }

        /// <summary>
        /// 連載中へフォルダを移動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MoveFolderToNow(object sender, EventArgs e)
        {
            MoveFolder(MoveTo.Now);
        }

        /// <summary>
        /// 連載終了へフォルダを移動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MoveFolderToFin(object sender, EventArgs e)
        {
            MoveFolder(MoveTo.Fin);
        }

        /// <summary>
        /// リストを作成する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CreateList(object sender, EventArgs e)
        {
        }

        #endregion

        private void lstContents_MouseDown(object sender, MouseEventArgs e)
        {
            if (lstContents.SelectedItem != null)
            {
                string filePath = string.Format("{0}\\{1}\\{2}\\{3}",
                    txtMain.Text, treeFolder.SelectedNode.Parent.Text, treeFolder.SelectedNode.Text, lstContents.SelectedItem.ToString());
                lstContents.DoDragDrop(filePath, DragDropEffects.Link);
            }
        }
    }
}
