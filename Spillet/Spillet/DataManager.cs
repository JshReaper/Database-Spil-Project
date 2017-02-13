using System;
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
        private static int currentSave = 1;

        public static void GenerateDataBase()
        {
            bool fileExists = false;
            if (!File.Exists("Data.db"))
            {
                
                SQLiteConnection.CreateFile("Data.db");
                SQLiteConnection dbConnOnCreate = new SQLiteConnection("Data Source=Data.db;Version=3;");
                dbConnOnCreate.Open();
                //initial logic


                string player = "create table Player (id integer primary key,sanity float, posX int, posY int, house int, inventory int, cluetoken int)";
                string inventory = "create table Inventory(id integer primary key, item int)";
                string item = "create table Item ( id integer primary key, name string, consumable bool, type string)";
                string enemy = "create table Enemy (id integer primary key, type string, fearFactor float)";
                string house = "create table House (id integer primary key, name string, event int, level int)";
                string villager = "create table Villager (id integer primary key, state int)";
                string sqlevent = "create table Event (id integer primary key, description string, cluetoken int)";
                SQLiteCommand commandOnCreate = new SQLiteCommand(player, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                commandOnCreate = new SQLiteCommand(inventory, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                commandOnCreate = new SQLiteCommand(item, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                commandOnCreate = new SQLiteCommand(enemy, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                commandOnCreate = new SQLiteCommand(house, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                commandOnCreate = new SQLiteCommand(villager, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                commandOnCreate = new SQLiteCommand(sqlevent, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();

                currentSave = 1;
                //end logic
                dbConnOnCreate.Close();
                
                fileExists = true;
            }
            if (File.Exists("Data.db") && !fileExists)
            {
                //message to user?
                //select save or start new save game
            }
        }
        public static string RetriveInfo(string toRetrive)
        {
            //Only execute commands with specific ID in them or this WILLLLLL fail.

            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            SQLiteCommand dbCom = new SQLiteCommand(toRetrive, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            dbCon.Close();
            return null;
        }

        static void ContinueGame()
        {
            //get a saved game from the database
        }
        static void NewGame()
        {
            SQLiteConnection dbConn = new SQLiteConnection("Data Source=Data.db;Version=3;");
            dbConn.Open();
            //ongoing logic
            string playerSave = String.Format("Insert into player(id,sanity, posX , posY , house , inventory , cluetoken ) values(null,{0},{1},{2},{3},{4},{5},{6});", GameWorld.Player.Sanity);
            SQLiteCommand commandOnCreate = new SQLiteCommand(playerSave, dbConn);
            commandOnCreate.ExecuteNonQuery();

            //end logic
            dbConn.Close();
        }
        static void Save()
        {
            SQLiteConnection dbConn = new SQLiteConnection("Data Source=Data.db;Version=3;");
            dbConn.Open();
            //ongoing logic
            string playerSave = String.Format("Update player set sanity = {0} where ID = {1});", GameWorld.Player.Sanity , currentSave);
            SQLiteCommand commandOnCreate = new SQLiteCommand(playerSave, dbConn);
            commandOnCreate.ExecuteNonQuery();

            //end logic
            dbConn.Close();
        }

        
        
    }
}
