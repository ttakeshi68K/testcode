using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SocketRequest
    {
        private string _serverID = string.Empty;
        private string _kyokuNo = string.Empty;
        private string _stnNo = string.Empty;
        private string _telNo = string.Empty;
        private string _recFileName = string.Empty;
        private string _lineNo = string.Empty;
        private string _monitorReq = string.Empty;

        public string ServerID
        {
            get { return this._serverID; }
            set { this._serverID = value; }
        }
        public string KyokuNo
        {
            get { return this._kyokuNo; }
            set { this._kyokuNo = value; }
        }
        public string StnNo
        {
            get { return this._stnNo; }
            set { this._stnNo = value; }
        }
        public string TelNo
        {
            get { return this._telNo; }
            set { this._telNo = value; }
        }
        public string RecFileName
        {
            get { return this._recFileName; }
            set { this._recFileName = value; }
        }
        public string LineNo
        {
            get { return this._lineNo; }
            set { this._lineNo = value; }
        }
        public string MonitorReq
        {
            get { return this._monitorReq; }
            set { this._monitorReq = value; }
        }
        
    }
}
