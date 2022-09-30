namespace FileSyncCopyer
{
    partial class frmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lstFiles = new System.Windows.Forms.ListBox();
            this.lblCreateFolder = new System.Windows.Forms.Label();
            this.txtCreateFolder = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.rbtnNow = new System.Windows.Forms.RadioButton();
            this.rbtnFin = new System.Windows.Forms.RadioButton();
            this.txtMain = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.btnMain = new System.Windows.Forms.Button();
            this.btnServer = new System.Windows.Forms.Button();
            this.lblMain = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.treeFolder = new System.Windows.Forms.TreeView();
            this.menuFolderList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenFolderMainPC = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFolderServer = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToFin = new System.Windows.Forms.ToolStripMenuItem();
            this.MoveToNow = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuCreateList = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lstContents = new System.Windows.Forms.ListBox();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblCopy = new System.Windows.Forms.Label();
            this.chkVariety = new System.Windows.Forms.CheckBox();
            this.lblCount = new System.Windows.Forms.Label();
            this.menuFolderList.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstFiles
            // 
            this.lstFiles.AllowDrop = true;
            this.lstFiles.FormattingEnabled = true;
            this.lstFiles.HorizontalScrollbar = true;
            this.lstFiles.ItemHeight = 12;
            this.lstFiles.Location = new System.Drawing.Point(11, 16);
            this.lstFiles.Name = "lstFiles";
            this.lstFiles.Size = new System.Drawing.Size(512, 184);
            this.lstFiles.TabIndex = 0;
            this.lstFiles.SelectedIndexChanged += new System.EventHandler(this.lstFiles_SelectedIndexChanged);
            this.lstFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstFiles_DragDrop);
            this.lstFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstFiles_DragEnter);
            this.lstFiles.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstFiles_KeyUp);
            this.lstFiles.MouseEnter += new System.EventHandler(this.lstFiles_MouseEnter);
            this.lstFiles.MouseLeave += new System.EventHandler(this.lstFiles_MouseLeave);
            // 
            // lblCreateFolder
            // 
            this.lblCreateFolder.AutoSize = true;
            this.lblCreateFolder.Location = new System.Drawing.Point(12, 209);
            this.lblCreateFolder.Name = "lblCreateFolder";
            this.lblCreateFolder.Size = new System.Drawing.Size(113, 12);
            this.lblCreateFolder.TabIndex = 1;
            this.lblCreateFolder.Text = "コピー対象のフォルダ名";
            // 
            // txtCreateFolder
            // 
            this.txtCreateFolder.Location = new System.Drawing.Point(11, 224);
            this.txtCreateFolder.Name = "txtCreateFolder";
            this.txtCreateFolder.Size = new System.Drawing.Size(423, 19);
            this.txtCreateFolder.TabIndex = 2;
            this.txtCreateFolder.TextChanged += new System.EventHandler(this.txtCreateFolder_TextChanged);
            this.txtCreateFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCreateFolder_KeyPress);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(439, 209);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(84, 34);
            this.btnRun.TabIndex = 3;
            this.btnRun.Text = "実行";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Location = new System.Drawing.Point(325, 249);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(109, 16);
            this.chkOverwrite.TabIndex = 4;
            this.chkOverwrite.Text = "強制上書きをする";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            this.chkOverwrite.CheckedChanged += new System.EventHandler(this.chkOverwrite_CheckedChanged);
            // 
            // rbtnNow
            // 
            this.rbtnNow.AutoSize = true;
            this.rbtnNow.Location = new System.Drawing.Point(11, 249);
            this.rbtnNow.Name = "rbtnNow";
            this.rbtnNow.Size = new System.Drawing.Size(59, 16);
            this.rbtnNow.TabIndex = 5;
            this.rbtnNow.TabStop = true;
            this.rbtnNow.Text = "連載中";
            this.rbtnNow.UseVisualStyleBackColor = true;
            this.rbtnNow.CheckedChanged += new System.EventHandler(this.rbtnNow_CheckedChanged);
            // 
            // rbtnFin
            // 
            this.rbtnFin.AutoSize = true;
            this.rbtnFin.Location = new System.Drawing.Point(76, 249);
            this.rbtnFin.Name = "rbtnFin";
            this.rbtnFin.Size = new System.Drawing.Size(71, 16);
            this.rbtnFin.TabIndex = 6;
            this.rbtnFin.TabStop = true;
            this.rbtnFin.Text = "連載終了";
            this.rbtnFin.UseVisualStyleBackColor = true;
            this.rbtnFin.CheckedChanged += new System.EventHandler(this.rbtnFin_CheckedChanged);
            // 
            // txtMain
            // 
            this.txtMain.Location = new System.Drawing.Point(10, 296);
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(451, 19);
            this.txtMain.TabIndex = 7;
            this.txtMain.TextChanged += new System.EventHandler(this.txtMain_TextChanged);
            this.txtMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMain_KeyPress);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(10, 333);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(451, 19);
            this.txtServer.TabIndex = 8;
            this.txtServer.TextChanged += new System.EventHandler(this.txtServer_TextChanged);
            this.txtServer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtServer_KeyPress);
            // 
            // btnMain
            // 
            this.btnMain.Location = new System.Drawing.Point(467, 294);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(47, 23);
            this.btnMain.TabIndex = 9;
            this.btnMain.Text = "選択";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            // 
            // btnServer
            // 
            this.btnServer.Location = new System.Drawing.Point(467, 331);
            this.btnServer.Name = "btnServer";
            this.btnServer.Size = new System.Drawing.Size(47, 23);
            this.btnServer.TabIndex = 10;
            this.btnServer.Text = "選択";
            this.btnServer.UseVisualStyleBackColor = true;
            this.btnServer.Click += new System.EventHandler(this.btnServer_Click);
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.Location = new System.Drawing.Point(9, 281);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(46, 12);
            this.lblMain.TabIndex = 11;
            this.lblMain.Text = "メインPC";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(8, 318);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(45, 12);
            this.lblServer.TabIndex = 12;
            this.lblServer.Text = "サーバー";
            // 
            // treeFolder
            // 
            this.treeFolder.ContextMenuStrip = this.menuFolderList;
            this.treeFolder.HideSelection = false;
            this.treeFolder.Location = new System.Drawing.Point(552, 43);
            this.treeFolder.Name = "treeFolder";
            this.treeFolder.Size = new System.Drawing.Size(352, 200);
            this.treeFolder.TabIndex = 13;
            this.treeFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFolder_AfterSelect);
            this.treeFolder.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeFolder_KeyPress);
            this.treeFolder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeFolder_MouseDoubleClick);
            this.treeFolder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeFolder_MouseDown);
            this.treeFolder.MouseEnter += new System.EventHandler(this.treeFolder_MouseEnter);
            this.treeFolder.MouseLeave += new System.EventHandler(this.treeFolder_MouseLeave);
            // 
            // menuFolderList
            // 
            this.menuFolderList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFolderMainPC,
            this.OpenFolderServer,
            this.MoveToFin,
            this.MoveToNow,
            this.MenuCreateList});
            this.menuFolderList.Name = "menuFolderList";
            this.menuFolderList.Size = new System.Drawing.Size(192, 114);
            // 
            // OpenFolderMainPC
            // 
            this.OpenFolderMainPC.Name = "OpenFolderMainPC";
            this.OpenFolderMainPC.Size = new System.Drawing.Size(191, 22);
            this.OpenFolderMainPC.Text = "メインPCのフォルダを開く";
            this.OpenFolderMainPC.Click += new System.EventHandler(this.OpenMainPCFolder);
            // 
            // OpenFolderServer
            // 
            this.OpenFolderServer.Name = "OpenFolderServer";
            this.OpenFolderServer.Size = new System.Drawing.Size(191, 22);
            this.OpenFolderServer.Text = "サーバーのフォルダを開く";
            this.OpenFolderServer.Click += new System.EventHandler(this.OpenServerFolder);
            // 
            // MoveToFin
            // 
            this.MoveToFin.Name = "MoveToFin";
            this.MoveToFin.Size = new System.Drawing.Size(191, 22);
            this.MoveToFin.Text = "連載終了フォルダへ移動";
            this.MoveToFin.Click += new System.EventHandler(this.MoveFolderToFin);
            // 
            // MoveToNow
            // 
            this.MoveToNow.Name = "MoveToNow";
            this.MoveToNow.Size = new System.Drawing.Size(191, 22);
            this.MoveToNow.Text = "連載中フォルダへ移動";
            this.MoveToNow.Click += new System.EventHandler(this.MoveFolderToNow);
            // 
            // MenuCreateList
            // 
            this.MenuCreateList.Name = "MenuCreateList";
            this.MenuCreateList.Size = new System.Drawing.Size(191, 22);
            this.MenuCreateList.Text = "リストを作成する";
            this.MenuCreateList.Click += new System.EventHandler(this.CreateList);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(550, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "フォルダ検索";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(552, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(291, 19);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(849, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(55, 23);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "検索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(439, 245);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(84, 34);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "クリア";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(550, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "フォルダ内容";
            // 
            // lstContents
            // 
            this.lstContents.FormattingEnabled = true;
            this.lstContents.HorizontalScrollbar = true;
            this.lstContents.ItemHeight = 12;
            this.lstContents.Location = new System.Drawing.Point(552, 266);
            this.lstContents.Name = "lstContents";
            this.lstContents.Size = new System.Drawing.Size(352, 88);
            this.lstContents.TabIndex = 21;
            this.lstContents.Click += new System.EventHandler(this.lstContents_Click);
            this.lstContents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstContents_MouseDown);
            this.lstContents.MouseEnter += new System.EventHandler(this.lstContents_MouseEnter);
            this.lstContents.MouseLeave += new System.EventHandler(this.lstContents_MouseLeave);
            // 
            // lblFileCount
            // 
            this.lblFileCount.Location = new System.Drawing.Point(711, 249);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(191, 16);
            this.lblFileCount.TabIndex = 22;
            this.lblFileCount.Text = "ファイル数";
            this.lblFileCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCopy
            // 
            this.lblCopy.AutoSize = true;
            this.lblCopy.Location = new System.Drawing.Point(9, 4);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(85, 12);
            this.lblCopy.TabIndex = 23;
            this.lblCopy.Text = "コピーするファイル";
            // 
            // chkVariety
            // 
            this.chkVariety.AutoSize = true;
            this.chkVariety.Location = new System.Drawing.Point(224, 249);
            this.chkVariety.Name = "chkVariety";
            this.chkVariety.Size = new System.Drawing.Size(95, 16);
            this.chkVariety.TabIndex = 24;
            this.chkVariety.Text = "バラエティモード";
            this.chkVariety.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(437, 1);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(51, 12);
            this.lblCount.TabIndex = 25;
            this.lblCount.Text = "ファイル数";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 362);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.chkVariety);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.lblFileCount);
            this.Controls.Add(this.lstContents);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeFolder);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.btnServer);
            this.Controls.Add(this.btnMain);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.txtMain);
            this.Controls.Add(this.rbtnFin);
            this.Controls.Add(this.rbtnNow);
            this.Controls.Add(this.chkOverwrite);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtCreateFolder);
            this.Controls.Add(this.lblCreateFolder);
            this.Controls.Add(this.lstFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "ファイル同期コピーツール";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.menuFolderList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Label lblCreateFolder;
        private System.Windows.Forms.TextBox txtCreateFolder;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.RadioButton rbtnNow;
        private System.Windows.Forms.RadioButton rbtnFin;
        private System.Windows.Forms.TextBox txtMain;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Button btnMain;
        private System.Windows.Forms.Button btnServer;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TreeView treeFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstContents;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.CheckBox chkVariety;
        private System.Windows.Forms.ContextMenuStrip menuFolderList;
        private System.Windows.Forms.ToolStripMenuItem OpenFolderMainPC;
        private System.Windows.Forms.ToolStripMenuItem OpenFolderServer;
        private System.Windows.Forms.ToolStripMenuItem MoveToFin;
        private System.Windows.Forms.ToolStripMenuItem MoveToNow;
        private System.Windows.Forms.ToolStripMenuItem MenuCreateList;
        private System.Windows.Forms.Label lblCount;
    }
}

