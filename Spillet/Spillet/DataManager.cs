﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace Spillet
{
    static class DataManager
    {
        public static void GenerateDataBase()
        {
            bool fileExists = false;
            if (!File.Exists("Data.db"))
            {
                SQLiteConnection.CreateFile("Data.db");
                SQLiteConnection dbConnOnCreate = new SQLiteConnection("Data Source=Data.db;Version=3;");
                dbConnOnCreate.Open();
                //initial logic


/*example       String sqlOnCreate = "create table highscores (id integer primary key,name varchar(20), score int)";
                SQLiteCommand commandOnCreate = new SQLiteCommand(sqlOnCreate, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
*/



                //end logic
                dbConnOnCreate.Close();
                
                fileExists = true;
            }
            if (File.Exists("Data.db") && !fileExists)
            {
                //message to user?
            }
        }

        static void Save()
        {
            SQLiteConnection dbConn = new SQLiteConnection("Data Source=Data.db;Version=3;");

            dbConn.Open();
            //ongoing logic

            //end logic
        }
    }
}