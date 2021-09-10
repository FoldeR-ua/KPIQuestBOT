using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPIQuestBOT
{
    class HashMethod
    {
        private string passtring;
        private  byte[] hashpass;
        public HashMethod(string pass)
        {
            passtring = pass;
            using (SHA256 hash = SHA256.Create())
            {
                hashpass = hash.ComputeHash(Encoding.UTF8.GetBytes(passtring));
            }
        }
        public string GetHash
        {
            get
            {
                string val = "";
                foreach (var one in hashpass)
                    val += one;
                return val;
            }
        }
    }
}
