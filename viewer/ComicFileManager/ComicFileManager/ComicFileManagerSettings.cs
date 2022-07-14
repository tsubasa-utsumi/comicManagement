using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ComicFileManager
{
    public class ComicFileManagerSettings
    {
        public string LocalFolder { get; set; }
        public string ServerFolder { get; set; }
        public string RequestURL { get; set; }

        public ComicFileManagerSettings()
        {
            // TODO: test
            testSettings();
        }

        /// <summary>
        /// テスト用初期化
        /// </summary>
        private void testSettings()
        {
            LocalFolder = @"I:\Comic\test";
            ServerFolder = @"\\Server\ServerRAID\Comic\test";
            RequestURL = "";
        }
    }
}
