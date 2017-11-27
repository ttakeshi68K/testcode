using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApplication1
{
    class PGTXmlStrreamer

    {
        // 指定された型で取り込む
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
                throw ex;
            }
            finally
            {
                if (fs != null) fs.Close();
            }

            return dat;
        }

        // オブジェクトの出力
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
                throw ex;
            }
            finally
            {
                fs.Close();
            }

        }


    }
}
