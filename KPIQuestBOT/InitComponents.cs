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
                List<GetToken> items = JsonConvert.DeserializeObject<List<GetToken>>(json);
                token = items[0].Token;
            }
            using (StreamReader sr = new StreamReader(@"D:\Coding\Project\C#\KPIQuestBOT\KPIQuestBOT\source\dbinfo.json"))
            {
                string db = sr.ReadToEnd();

            }
        }
        public string Token
        {
            get => token;
        }
    }
}
