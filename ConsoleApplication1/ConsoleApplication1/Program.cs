using System;
using System.Diagnostics;
using System.Configuration;

// SQLServer
using System.Data.SqlClient;

// log4net
using log4net;

using ConsoleApplication1.Properties;

namespace ConsoleApplication1
{
    class Program
    {
        /// <summary>
        /// ログ出力機能
        /// </summary>
        private static ILog _cLog = LogManager.GetLogger(C_LOG_NAME.CALL_LOG);

        static void Main(string[] args)
        {
                bool l4nSetingLoadSucess = _cLog.Logger.Repository.Configured;

                if (!l4nSetingLoadSucess)
                {
                    var errorIventMsg = "log4net.xml load Error:Repository.Configured=" + l4nSetingLoadSucess.ToString();

                    EventLog.WriteEntry("TESTPG", errorIventMsg, EventLogEntryType.Error, 9999);

                    throw new Exception();
                }

            _cLog.Info("*** TEST AP START ***");

            // _cLog.Debug("TEST MSG Debug");
            // _cLog.Info("TEST MSG Info");


            try
            {

                SettingReader.Initialize();

                // 接続文字列を生成する
                string connectionString = ConfigurationManager.ConnectionStrings["SQLCON"].ConnectionString;


                // SqlConnection の新しいインスタンスを生成する (接続文字列を指定)
                SqlConnection cSqlConnection = (
                    new System.Data.SqlClient.SqlConnection(connectionString)
                );

                // データベース接続を開く
                cSqlConnection.Open();


                string sql1 = "";





                // データベース接続を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                cSqlConnection.Close();
                cSqlConnection.Dispose();



            }
            catch (Exception ex){
                _cLog.Error("error",ex);
            }
            finally
            {
                _cLog.Info("*** TEST AP END ***");
            }
        }
    }
    public class SettingReader {
        internal static void Initialize()
        {
            string setingFilePath = PGTSetting.Default.SettingFilePath;
            string setingFileName = PGTSetting.Default.SettingFileName;

            // PGTXmlStrreamer.Import()


        }
    }
}
