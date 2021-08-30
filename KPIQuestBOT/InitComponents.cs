using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace KPIQuestBOT
{
    class InitComponents
    {
        private string token;
        private string dbinfo;
        public InitComponents()
        {
            using (StreamReader r = new StreamReader(@"D:\Coding\Project\C#\KPIQuestBOT\KPIQuestBOT\source\token.json"))
            {
                string json = r.ReadToEnd();
                List<BotToken> tokens = JsonConvert.DeserializeObject<List<BotToken>>(json);
                token = tokens[0].Token;
            }
            using (StreamReader sr = new StreamReader(@"D:\Coding\Project\C#\KPIQuestBOT\KPIQuestBOT\source\dbinfo.json"))
            {
                string db = sr.ReadToEnd();
                List<DBInfo> info = JsonConvert.DeserializeObject<List<DBInfo>>(db);
                dbinfo = $"datasource = {info[0].Datasource}; port = {info[0].Port}; username = {info[0].Username}; password ={info[0].Password}; database = {info[0].Database}; SSL Mode = 0;";
            }
        }
        public string Token
        {
            get => token;
        }
        public string DBinfo
        {
            get => dbinfo;
        }
    }
}
