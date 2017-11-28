using System;
using System.Diagnostics;
using System.Configuration;

// SQLServer
using System.Data.SqlClient;

// log4net
using log4net;
using log4net.Appender;
using log4net.Repository;

using ConsoleApplication1.Properties;

namespace ConsoleApplication1
{
    class Program
    {


        /// <summary>
        /// ログ出力機能
        /// </summary>
        private static ILog _iLog = LogManager.GetLogger(C_LOG_NAME.INTERNAL_LOG);
        private static ILog _cLog = LogManager.GetLogger(C_LOG_NAME.CALL_LOG);

        private static SettingReader curentSettig;

        static void Main(string[] args)
        {
                bool l4nSetingLoadSucess = _iLog.Logger.Repository.Configured;

                if (!l4nSetingLoadSucess)
                {
                    var errorIventMsg = "log4net.xml load Error:Repository.Configured=" + l4nSetingLoadSucess.ToString();

                    EventLog.WriteEntry("TESTPG", errorIventMsg, EventLogEntryType.Error, 9999);

                    throw new Exception();
                }


            curentSettig = new SettingReader();

            // 

                foreach (ILoggerRepository repository in LogManager.GetAllRepositories())
                {
                    foreach (IAppender appender in repository.GetAppenders())
                    {
                        if (appender.Name.Equals(C_LOG_NAME.CALL_LOG))
                        {
                            FileAppender fileAppender = appender as FileAppender;
                            if (fileAppender != null)
                            {
                                string file = fileAppender.File;
                                if (!string.IsNullOrEmpty(file))
                                {
                                    if (file.Contains("IVRCALL"))
                                    {
                                        fileAppender.File = curentSettig.setting.LogPath;
                                        fileAppender.ActivateOptions();
                                        /*変更前のファイルを消してしまって良いならばコメントをはずす*/
                                        /*
                                        if (File.Exists(file))
                                        {
                                            File.Delete(file);
                                        }
                                        */
                                    }
                                }
                            }
                        }
                    }
                }
            
            _cLog.Info("*** TEST AP START ***");

            // _cLog.Debug("TEST MSG Debug");
            // _cLog.Info("TEST MSG Info");


            try
            {


                // 接続文字列を生成する
                // string connectionString = ConfigurationManager.ConnectionStrings["SQLCON"].ConnectionString;


                // SqlConnection の新しいインスタンスを生成する (接続文字列を指定)
                SqlConnection cSqlConnection = (
                    new System.Data.SqlClient.SqlConnection(curentSettig.setting.ConnectionStr)
                );

                // データベース接続を開く
                cSqlConnection.Open();


                string sql1 = "";





                // データベース接続を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                cSqlConnection.Close();
                cSqlConnection.Dispose();



            }
            catch (Exception ex){
                _iLog.Error("error",ex);
            }
            finally
            {
                _cLog.Info("*** TEST AP END ***");
            }
        }
    }
    public class SettingReader {

        /// <summary>
        /// ログ出力機能
        /// </summary>
        private static ILog _iLog = LogManager.GetLogger(C_LOG_NAME.INTERNAL_LOG);

        public OverWriteSetting setting;

        public SettingReader()
        {
            setting = Initialize();
        }

        internal OverWriteSetting Initialize()
        {
            string _Path = PGTSetting.Default.SettingFilePath + "\\" + PGTSetting.Default.SettingFileName;

            OverWriteSetting curentSettig = null;

            try
            {

                _iLog.Info("PGTSetting.config'S PATH=" + _Path);
                curentSettig = (OverWriteSetting)XmlStrreamer.Import(_Path, typeof(OverWriteSetting));
                _iLog.Info(curentSettig);

            }
            catch (Exception ex)
            {
                _iLog.Error("error", ex);
                throw ex;
            }

            return curentSettig;

        }
    }
}
