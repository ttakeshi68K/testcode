using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class OverWriteSetting
    {
        private  string _logPath = string.Empty;
        private string _connectionStr = string.Empty;
        private int _playCount = 0;
        private string _severIP = string.Empty;
        private string _severPort = string.Empty;
        private string _exNo = string.Empty;

        public string LogPath
        {
            get { return this._logPath; }
            set { this._logPath = value; }
        }
        public string ConnectionStr
        {
            get { return this._connectionStr; }
            set { this._connectionStr = value; }
        }


        public int PlayCount
        {
            get { return this._playCount; }
            set { this._playCount = value; }
        }

        public string SeverIP
        {
            get { return this._severIP; }
            set { this._severIP = value; }
        }

        public string SeverPort
        {
            get { return this._severPort; }
            set { this._severPort = value; }
        }

        public string ExNo
        {
            get { return this._exNo; }
            set { this._exNo = value; }
        }



    }
}
