using System;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using System.Reflection;

// SQLServer
using System.Data.SqlClient;

// log4net
using log4net;
using log4net.Appender;
using log4net.Repository;

using ConsoleApplication1.Properties;
using System.IO;

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

        private static SqlConnection dblConnection;
        private static SqlTransaction dbTransaction;
        private static SqlCommand dbCommand;

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

            clogFilePathUpdate(); // CallLogの出力先をPGTSetting.configから取得した出力先に変更する
             
            _cLog.Info("*** TEST AP START ***");

            // _cLog.Debug("TEST MSG Debug");
            // _cLog.Info("TEST MSG Info");


            SocketRequest sr = new SocketRequest();

            sr.KyokuNo = "03"; 
            sr.LineNo = "10";
            sr.MonitorReq = "1";
            sr.RecFileName = "test_test.wav";
            sr.ServerID = "10";
            sr.StnNo = "1";
            sr.TelNo = "0312345678";

            string tmpStreem = XmlStrreamer.TransPortString(sr);    // クラステキスト化

            // 整形 
            tmpStreem = tmpStreem.Replace("\r\n", "");
            tmpStreem = tmpStreem.Replace("  ", ""); // 簡易版　汎用性？
            string headClass = " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"";
            tmpStreem = tmpStreem.Replace(headClass, "");
            string headXML = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            tmpStreem = tmpStreem.Replace(headXML, "");
            _cLog.Info("SocketRequest.toString=" + tmpStreem);


            // 接続文字列を生成する
            // string connectionString = ConfigurationManager.ConnectionStrings["SQLCON"].ConnectionString;

            // SqlConnection の新しいインスタンスを生成する (接続文字列を指定)
            try
            {
                dblConnection = new SqlConnection(curentSettig.setting.ConnectionStr);
            }
            catch (Exception ex)
            {
                _iLog.Error("error", ex);
            }

            // データベース接続を開く
            try
            {
                dblConnection.Open();
            }
            catch (Exception ex)
            {
                _iLog.Error("error", ex);
            }


            try
            {
                string sql1 = @"SELECT WATCH_FLG FROM T_SYSTEM WITH(XLOCK,ROWLOCK,NOWAIT)";

                dbCommand = dblConnection.CreateCommand();          // コマンドオブジェクト作成
                dbTransaction = dblConnection.BeginTransaction();   // トランザクションオブジェクト作成
                dbCommand.Transaction = dbTransaction;              // コマンドにトランザクションを関連付ける

                dbCommand.CommandText = sql1;

                SqlDataReader sdr = dbCommand.ExecuteReader();

                _cLog.Info("T_SYSTEM'S Has RowS=" + sdr.HasRows.ToString());


                int rowCount = 0;

                while(sdr.Read())
                {
                    rowCount++;
                    _cLog.Info("T_SYSTEM'S WATCH_FLG=" + sdr["WATCH_FLG"].ToString());
                }

                if(rowCount !=1)
                {
                    _cLog.Info("T_SYSTEM'S FOUND COUNT OVVER COUNt=" + rowCount.ToString());
                } 

                sdr.Close();
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                _iLog.Error("error", ex);

            }


            try
            {

                string sql2 = @"";

            }
            catch (Exception ex){
                _iLog.Error("error",ex);
            }
            finally
            {
                // データベース接続を閉じる (正しくは オブジェクトの破棄を保証する を参照)
                dblConnection.Close();
                dblConnection.Dispose();

            }
            _cLog.Info("*** TEST AP END ***");

        }

        //　CallLogの出力先をPGTSetting.configから取得した出力先に変更する 
        static private void clogFilePathUpdate()
        {
            bool findLogFile = false;
            string tmpFileName = string.Empty;

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
                                if (file.Contains(PGTSetting.Default.ApLogPrefix))
                                {
                                    tmpFileName = curentSettig.setting.LogPath.Replace(".log","");  // .logを削除する
                                    tmpFileName = tmpFileName.Replace(".LOG", "");  // .logを削除する
                                    tmpFileName = tmpFileName.Replace(".Log", "");  // .logを削除する
                                    fileAppender.File = tmpFileName;

                                    // ログファイルを排他にしない。
                                    fileAppender.LockingModel = new log4net.Appender.FileAppender.MinimalLock();
                                    fileAppender.ActivateOptions();
                                    findLogFile = true;
                                    /*変更前のファイルを消してしまって良いならばコメントをはずす*/
                                    if (File.Exists(file))
                                    {
                                        File.Delete(file);
                                    }
                                }
                                else
                                {
                                    _iLog.Error("log4net.xml logfileNameError");
                                }
                            }
                        }
                        else
                        {
                            _iLog.Error("log4net.xml CallLog fileApenderError Not Find");
                        }
                    }
                }
            }

            if (!findLogFile)
            {
                _iLog.Error("PGTSetting.config CallLog file Path Change Error! cheack log4net.xml!");
            }
            else

            {
                _iLog.Info("Loding fileAppender.File=" + tmpFileName);
            }
        }

    }

    // xml形式設定ファイル読み込み
    public class SettingReader {

        /// <summary>
        /// ログ出力機能
        /// </summary>
        private static ILog _iLog = LogManager.GetLogger(C_LOG_NAME.INTERNAL_LOG);

        public OverWriteSetting setting;

        public SettingReader()
        {
            setting = this.Initialize();
        }

        internal OverWriteSetting Initialize()
        {
            string _Path = PGTSetting.Default.SettingFilePath + "\\" + PGTSetting.Default.SettingFileName;

            OverWriteSetting curentSettig = null;

            try
            {

                _iLog.Info("PGTSetting.config'S PATH=" + _Path);
                curentSettig = (OverWriteSetting)XmlStrreamer.Import(_Path, typeof(OverWriteSetting));
                _iLog.Info("LodingPropaties=\n" + getPropertiesFormat(curentSettig, 1));

            }
            catch (Exception ex)
            {
                _iLog.Error("error", ex);
                throw ex;
            }

            return curentSettig;

        }

        // オブジェクトプロパティの文字列化
        protected static string getPropertiesFormat(object obj, int indent)
        {
            StringBuilder sb = new StringBuilder();

            string indent_space = string.Empty;
            for(int i = 0; i < indent; i++)
            {
                indent_space += "　";
            }

            try
            {
                Type t = obj.GetType();

                if (t.Name.EndsWith("List"))
                {
                    // List処理
                    PropertyInfo[] pArray = obj.GetType().GetProperties();

                    if (pArray == null) { return sb.ToString(); }

                    // 配列のListの長さを取得
                    int count_list = 0;
                    foreach (PropertyInfo p in pArray)
                    {
                        if (p == null) { continue; }
                        if (p.Name == null) { continue; }
                        if (p.Name.Equals("Count"))
                        {
                            // Countの取得
                            count_list = (int)p.GetValue(obj, null);
                            sb.Append(string.Format("{0}{1}={2}|n", indent_space, p.Name, count_list));
                        }
                        else if (p.Name.Equals("Index"))
                        {
                            // インデックスの取得
                            sb.Append(string.Format("{0}{1}={2}|n", indent_space, p.Name, p.GetValue(obj, null)));
                        }
                        else if (p.Name.Equals("Item"))
                        {
                            for (int i = 0; i < count_list; i++)
                            {
                                object newObj = p.GetValue(obj, new object[] { i });
                                sb.Append(string.Format("{0}[{1}]\n", indent_space, i));
                                sb.Append(getPropertiesFormat(newObj, indent + 1));
                            }
                        }
                    }
                }
                else if (t.IsArray)
                {
                    object[] array = (object[])obj;
                    if(array == null) { return sb.ToString(); }
                    for(int i = 0; i < array.Length; i++)
                    {
                        if (array[i] == null) { continue; }
                        switch (t.Name)
                        {
                            case "String[]" :
                            case "Int[]" :
                            case "Int16[]" :
                            case "Int32[]" :
                            case "Int64[]" :
                                if(array[i] == null || array[i].ToString().Equals(string.Empty)) { continue; }
                                else
                                {
                                    sb.Append(string.Format("{0}[{1}]={2}\n", indent_space, i, array[i].ToString()));
                                }
                                break;
                            default :
                                sb.Append(string.Format("", indent_space, i));
                                sb.Append(getPropertiesFormat(array[i], indent + 1));
                                break;
                        }
                    }
                }
                else
                {
                    PropertyInfo[] pArray = t.GetProperties();
                    if (pArray == null) { return sb.ToString(); }

                    foreach (PropertyInfo p in pArray)
                    {
                        if (p == null) { continue; }
                        if (p.Name == null) { continue; }
                        if (p.Name.Equals("Item")) { continue; }

                        object pObj = p.GetValue(obj, null);
                        Type pT = p.PropertyType;

                        switch (pT.Name)
                        {
                            case "String":
                            case "Int":
                            case "Int16":
                            case "Int32":
                            case "Int64":
                                if (pObj == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    if (p.Name.Equals("CurrentPswd") || p.Name.Equals("NewPswd") || p.Name.Equals("NewPswd2"))
                                    {
                                        sb.Append(string.Format("{0}{1}={2}\n", indent_space, p.Name, "********"));
                                    }
                                    else
                                    {
                                        if (pObj.ToString().Equals(string.Empty))
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            sb.Append(string.Format("{0}{1}={2}\n", indent_space, p.Name, pObj.ToString()));
                                        }
                                    }
                                }
                                break;
                            case "Boolean":
                                if (pObj == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    sb.Append(string.Format("{0}{1}={2}\n", indent_space, p.Name, pObj.ToString()));
                                }
                                break;
                            default:
                                if (pObj == null) { continue; }
                                if (pT.IsEnum)
                                {
                                    sb.Append(string.Format("{0}{1}={2}\n", indent_space, p.Name, ((int)pObj).ToString()));
                                }
                                else
                                {
                                    sb.Append(string.Format("{0}{1}\n", indent_space, p.Name));
                                    sb.Append(getPropertiesFormat(pObj, indent + 1));
                                }
                                break;
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                _iLog.Error("オブジェクトプロパティ　フォーマットエラー");
                _iLog.Error(ex.Message);
                _iLog.Error(ex.Source);
                _iLog.Error(ex.StackTrace);
                _iLog.Error("ObjectType" + obj.GetType());
                _iLog.Error(sb.ToString());
            }

            return sb.ToString();
        }
    }
}
