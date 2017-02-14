﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
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
                string eventMaker = "insert into Event values(null,\"As you enter the house you notice a shadow that quickly disapears underneath some of the furnature\",1)";
                commandOnCreate = new SQLiteCommand(eventMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                eventMaker = "insert into Event values(null,\"As you enter the house you feel a cold chill rising through your body, you look around but all windows seems to be closed \n and there seems to be no people around.\",1)";
                commandOnCreate = new SQLiteCommand(eventMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                eventMaker = "insert into Event values(null,\"You enter the house and immidiatly you feel something is wrong, you look around \n but you can't seem to find anything wrong, your gaze falls on a shadow on the floor seemingly from nowhere.\",0)";
                commandOnCreate = new SQLiteCommand(eventMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                string itemMaker = "insert into Item values(null,\"Note1\",false,\"Note\")";
                commandOnCreate = new SQLiteCommand(itemMaker, dbConnOnCreate);
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
        public static string RetriveEventInfo(string toRetrive)
        {
            //Only execute commands with specific ID in them or this WILLLLLL fail.

            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            SQLiteCommand dbCom = new SQLiteCommand(toRetrive, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            string toReturn = dr.GetString(1);
            dbCon.Close();
            return toReturn;
        }
        public static string RetriveEventClues(string toRetrive)
        {
            

            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            SQLiteCommand dbCom = new SQLiteCommand(toRetrive, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            string toReturn = dr.GetString(2);
            dbCon.Close();
            return toReturn;
        }

        public static string RetriveItemType(int itemID)
        {  
            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            string retrieve = string.Format("Select * from Item where id = {0}",itemID);
            SQLiteCommand dbCom = new SQLiteCommand(retrieve, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            string toReturn = dr.GetString(3);
            dbCon.Close();
            return toReturn;
        }
        public static string RetriveItemName(int itemID)
        {
            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            string retrieve = string.Format("Select * from Item where id = {0}", itemID);
            SQLiteCommand dbCom = new SQLiteCommand(retrieve, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            string toReturn = dr.GetString(1);
            dbCon.Close();
            return toReturn;
        }
        public static bool RetriveItemBool(int itemID)
        {
            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            string retrieve = string.Format("Select * from Item where id = {0}", itemID);
            SQLiteCommand dbCom = new SQLiteCommand(retrieve, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            bool toReturn = dr.GetBoolean(2);
            dbCon.Close();
            return toReturn;
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
