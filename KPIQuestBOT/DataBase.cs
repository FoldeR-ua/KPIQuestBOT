using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using Newtonsoft.Json;

namespace KPIQuestBOT
{
    class DataBase
    {

        MySqlConnection connection = new MySqlConnection(new InitComponents("json").DBinfo);

        public void OpenConnection()
        { 
            if (connection.State == ConnectionState.Closed) connection.Open();
        }
        public void CloseConnection() => connection.Close();
        public MySqlConnection GetConnection() => connection;
    }
    class DataBaseWork
    {
        public void CheckingData(string password, ref bool logined, long userId)
        {
            long id = -1;
            DataBase data = new DataBase();
            password = new HashMethod(password).GetHash;

            data.OpenConnection();
            
            MySqlCommand command = new MySqlCommand("SELECT * FROM `quest` WHERE `password` = @pass", data.GetConnection());
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id = reader.GetInt64("user");
            }
            reader.Close();

            
            if(id == 0)
            {
                logined = true;
                MySqlCommand command2 = new MySqlCommand("UPDATE `quest` SET `inuse`= 1, `user`= @us WHERE `password`=@hpass", data.GetConnection());
                command2.Parameters.Add("@us", MySqlDbType.VarChar).Value = userId;
                command2.Parameters.Add("@hpass", MySqlDbType.VarChar).Value = password;
                command2.ExecuteNonQuery();
            }
            
            if (id == userId)
            {
                logined = true; 
            }
            else logined = false;


            data.CloseConnection();   
        }
        public void ReadJSON(string password, ref List<QuestionAnswear> questionAnswears)
        {
            password = new HashMethod(password).GetHash;
            string json = "";
            DataBase data = new DataBase();
            data.OpenConnection();
            MySqlCommand command = new MySqlCommand("SELECT `jsonQA` FROM `quest` WHERE `password` = @pass", data.GetConnection());
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            MySqlDataReader reader = command.ExecuteReader();
            
            if (reader.HasRows)
                while (reader.Read())
                    json = reader.GetString(0);

            questionAnswears = JsonConvert.DeserializeObject<List<QuestionAnswear>>(json);
        }
    }

}
