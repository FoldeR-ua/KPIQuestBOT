using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPIQuestBOT
{
    class DBInfo
    {
        private string datasource;
        private string port;
        private string username;
        private string password;
        private string database;
        private string SSLmode;

        public string Datasource { get => datasource; set => datasource = value; }
        public string Port { get => port; set => port = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Database { get => database; set => database = value; }
        public string SSLMode { get => SSLmode; set => SSLmode = value; }
    }
}
