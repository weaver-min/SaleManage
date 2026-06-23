using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace SaleManage.Database
{
    public static class connection
    {
        static string configPath;
        // ドキュメントのパス
        static string docPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "FsHanbai\\setting.config");
        // 実行フォルダのパス
        static string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "setting.config");
        [XmlRoot("settings", Namespace = "")]
        public class Settings
        {
            // DB設定
            public string DBServer { get; set; }
            public string DBName { get; set; }
            public string DBUser { get; set; }
            public string DBPass { get; set; }

            // 最新版（ファイル配布関連）
            public string FileCopyServerPath { get; set; }
            public string UserId { get; set; } // 共有フォルダのユーザー
            public string Password { get; set; } // 共有フォルダのパスワード

            // 保存先
            public string LocalPath { get; set; }

        }
        public static String GetPass()
        {
            // ドキュメント側にファイルがあるか確認
            if (File.Exists(docPath))
            {
                configPath = docPath;
            }
            else
            {
                configPath = basePath;
            }
            return configPath;
        }
        public static Settings LoadSettings(string path)
        {
            var serializer = new XmlSerializer(typeof(Settings));
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(path, Encoding.Unicode, detectEncodingFromByteOrderMarks: true);
                var settings = (Settings)serializer.Deserialize(reader);
                return settings;
            }
            catch (InvalidOperationException ex)
            {
                string detail = ex.InnerException != null
                ? $"{ex.InnerException.GetType().Name}: {ex.InnerException.Message}"
                : ex.Message;

                MessageBox.Show("XML読込に失敗しました。\n\n" + detail, "設定読込エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
        public static String GetDBPass()
        {
            configPath = GetPass();
            var settings = LoadSettings(configPath);
            String server = settings.DBServer;
            String database = settings.DBName;
            String userID = settings.DBUser;
            String password = settings.DBPass;
            string connectionString = $"Server={server};Database={database};User Id={userID};Password={password};";
            return connectionString;
        }
    }
}
