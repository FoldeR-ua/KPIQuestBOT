using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System;
using Newtonsoft.Json;

namespace KPIQuestBOT
{
    class DataBase
    {
        MySqlConnection connection = new MySqlConnection("datasource=127.0.0.1;port=3306;username=root;password=;database=userlog;SSL Mode = 0;");

        public void OpenConnection()
        { 
            if (connection.State == ConnectionState.Closed) connection.Open();
        }
        public void CloseConnection() => connection.Close();
        public MySqlConnection GetConnection() => connection;
    }
    class DataBaseWork
    {
        public void CheckingData(string password, ref bool logined)
        {
            DataBase data = new DataBase();
            data.OpenConnection();
            
            MySqlCommand command = new MySqlCommand("SELECT * FROM `quest` WHERE `password` = @pass", data.GetConnection());
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            
            adapter.SelectCommand = command;
            adapter.Fill(table);
            data.CloseConnection();
            if (table.Rows.Count == 1) logined = true; else logined = false;
        }
        public void ReadJSON(string password, ref List<QuestionAnswear> questionAnswears)
        {
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
