using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

// log4net
using log4net;

namespace ConsoleApplication1
{
    class XmlStrreamer

    {
        /// <summary>
        /// ログ出力機能
        /// </summary>
        private static ILog _iLog = LogManager.GetLogger(C_LOG_NAME.INTERNAL_LOG);

        // xmlファイルを指定された型で取り込む
        public static object Import(string path, Type type)
        {
            XmlSerializer seri = new XmlSerializer(type);

            FileStream fs = null;
            object dat = null;
            try
            {
                fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                dat = seri.Deserialize(fs);
            }
            catch (Exception ex)
            {
                _iLog.Error(ex.Message);
                _iLog.Error(ex.Source);
                _iLog.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }

            return dat;
        }

        // オブジェクトをxmlファイルで出力
        public static void Export(string path, object dat)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);

                XmlSerializer seri = new XmlSerializer(dat.GetType());

                seri.Serialize(fs, dat);

            }
            catch (Exception ex)
            {
                _iLog.Error(ex.Message);
                _iLog.Error(ex.Source);
                _iLog.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
                fs.Close();
            }

        }

        // オブジェクトをxml形式で文字列に変換
        public static String TransPortString (object dat)
        {
            try
            {

                StringWriter sw = new StringWriter();   // 空のStringWriter

                XmlSerializer seri = new XmlSerializer(dat.GetType());

                seri.Serialize(sw, dat);

                return sw.ToString();

            }
            catch (Exception ex)
            {
                _iLog.Error(ex.Message);
                _iLog.Error(ex.Source);
                _iLog.Error(ex.StackTrace);
                throw ex;
            }
            finally
            {
            }

        }
    }
}
