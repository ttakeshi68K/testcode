using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;

using log4net;
using log4net.Config;

using ConsoleApplication1;

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
            try
            {
                bool l4nSetingLoadSucess = _cLog.Logger.Repository.Configured;

                if (!l4nSetingLoadSucess)
                {
                    var errorIventMsg = "log4net.xml load Error:Repository.Configured=" + l4nSetingLoadSucess.ToString();

                    EventLog.WriteEntry("TESTPG", errorIventMsg, EventLogEntryType.Error, 9999);

                    throw new Exception();
                }

                _cLog.Debug("TEST MSG Debug");

                _cLog.Info("TEST MSG Info");

            }
            catch {

            }
        }
    }
}
