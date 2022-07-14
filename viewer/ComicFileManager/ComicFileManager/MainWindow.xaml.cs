using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComicFileManager
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private ComicFileManagerSettings setting = new ComicFileManagerSettings();

        public MainWindow()
        {
            InitializeComponent();

            Initialize();
        }

        #region 起動時処理
        private void Initialize()
        {
            txtSearch.DataContext = this;
        }
        #endregion

        #region 画面処理
        private void ShowFolders()
        {
            lstFolders.DataContext = new ComicFolderViewModel();
        }
        #endregion

        public void Search()
        {
            Console.Write("TEST");
        }
    }
}
