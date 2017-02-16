using System;
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


                string player = "create table Player (id integer primary key,sanity float, posX float, posY float, house int, inventory int, cluetoken int)";
                string inventory = "create table Inventory(id integer primary key, item1 int,item2 int,item3 int,item4 int,item5 int,item6 int,item7 int,item8 int,item9 int,item10 int)";
                string item = "create table Item ( id integer primary key, name string, consumable integer, type string)";
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
                string eventMaker = "insert into Event values(null,\"As you enter the house you notice a shadow that quickly disapears underneath \nsome of the furnature\",1)";
                commandOnCreate = new SQLiteCommand(eventMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                eventMaker = "insert into Event values(null,\"As you enter the house you feel a cold chill rising through your body,\nyou look around but all windows seems to be closed \n and there seems to be no people around.\",1)";
                commandOnCreate = new SQLiteCommand(eventMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                eventMaker = "insert into Event values(null,\"You enter the house and immidiatly you feel something is wrong, you look around \n but you can't seem to find anything wrong, your gaze falls on a shadow on the floor seemingly from nowhere.\",0)";
                commandOnCreate = new SQLiteCommand(eventMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                eventMaker = "insert into Event values(null,\"You enter the house and immidiatly you feel something is wrong, you look around \n and as your gaze falls on the wall you see large claw marks \nyou are sure that theese are not the claws of a normal animal but something larger. \nThe room seems otherwise empty.\",1)";
                commandOnCreate = new SQLiteCommand(eventMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                string itemMaker = "insert into Item values(null,\"Note1\",0,\"Note\")";
                commandOnCreate = new SQLiteCommand(itemMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                itemMaker = "insert into Item values(null,\"Lantern\",0,\"Lantern\")";
                commandOnCreate = new SQLiteCommand(itemMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                itemMaker = "insert into Item values(null,\"Oil\",1,\"Oil\")";
                commandOnCreate = new SQLiteCommand(itemMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                itemMaker = "insert into Item values(null,\"Note2\",0,\"Note\")";
                commandOnCreate = new SQLiteCommand(itemMaker, dbConnOnCreate);
                commandOnCreate.ExecuteNonQuery();
                string inventoryMaker = "insert into inventory(id) values(null)";
                commandOnCreate = new SQLiteCommand(inventoryMaker, dbConnOnCreate);
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
            //Only execute commands with specific ID in them or THIS WILL fail!

            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            SQLiteCommand dbCom = new SQLiteCommand(toRetrive, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            dr.Read();
            string toReturn = dr.GetString(1);
            dbCon.Close();
            return toReturn;
        }
        public static int RetriveEventClues(string toRetrive)
        {
            

            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            SQLiteCommand dbCom = new SQLiteCommand(toRetrive, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();
            dr.Read();
            int toReturn = dr.GetInt32(2);
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
            dr.Read();
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
            dr.Read();
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
            dr.Read();
            bool toReturn = dr.GetBoolean(2);
            dbCon.Close();
            return toReturn;
        }
        public static void ContinueGame()
        {
            SQLiteConnection dbCon = new SQLiteConnection("Data Source=Data.db;Version=3;");
            string retrieve ="select * from Player where id = 1";

            SQLiteCommand dbCom = new SQLiteCommand(retrieve, dbCon);
            dbCon.Open();
            SQLiteDataReader dr = dbCom.ExecuteReader();

            dr.Read();

            GameWorld.Player.Sanity = dr.GetFloat(1);
            GameWorld.Player.Posistion.X = dr.GetFloat(2);
            GameWorld.Player.Posistion.Y = dr.GetFloat(3);
            GameWorld.currentScene = dr.GetInt32(4);
            GameWorld.Player.ClueToken = dr.GetInt32(6);
            retrieve = "select * from inventory where id = 1";
            dbCom = new SQLiteCommand(retrieve,dbCon);
            dr = dbCom.ExecuteReader();
            dr.Read();
            for (int i = 0; i < GameWorld.Player.Inventory.Length; i++)
            {
                if(dr.GetInt32(i) != 0)
                GameWorld.Player.Inventory[i] = new Item(0, $@"Art Assets\Props\{DataManager.RetriveItemType(dr.GetInt32(i))}.png", new Vector2D(0,0),1,0,(byte)dr.GetInt32(i));
                for (int j = 0; j < GameWorld.AllGameObjects.Count; j++)
                {
                    var item = GameWorld.AllGameObjects[j] as Item;
                    if (item != null && GameWorld.Player.Inventory[i] != null)
                    {
                        if (item.Id == GameWorld.Player.Inventory[i].Id)
                        {
                            item.IamInInventory = true;

                        }
                    }
                }
            }
            dbCon.Close();

        }
        public static void  NewGame()
        {
            SQLiteConnection dbConn = new SQLiteConnection("Data Source=Data.db;Version=3;");
            dbConn.Open();
            //ongoing logic
            string playerSave = String.Format("delete from player;"+"Insert into player(id,sanity, posX , posY , house , inventory , cluetoken ) values(null,{0},{1},{2},{3},{4},{5});", GameWorld.Player.Sanity,GameWorld.Player.Posistion.X,GameWorld.Player.Posistion.Y,GameWorld.currentScene,GameWorld.Player.Id,0);
            string inventorySave = "delete from inventory;" + "insert into inventory(id) values(null)";
            SQLiteCommand commandOnCreate = new SQLiteCommand(playerSave, dbConn);
            commandOnCreate.ExecuteNonQuery();
            commandOnCreate = new SQLiteCommand(inventorySave, dbConn);
            commandOnCreate.ExecuteNonQuery();

            //end logic
            dbConn.Close();
        }
        public static void Save()
        {
            SQLiteConnection dbConn = new SQLiteConnection("Data Source=Data.db;Version=3;");
            dbConn.Open();
            //ongoing logic
            string playerSave = String.Format(
                "Update player set sanity = {0} where ID = {1};" +
                "Update player set posX = {2} where ID = {1};" +
                "Update player set posY = {3} where ID = {1};" +
                "Update player set house = {4} where ID = {1};" +
                "Update player set inventory = {5} where ID = {1};" +
                "Update player set cluetoken = {6} where ID = {1};", 
                GameWorld.Player.Sanity , GameWorld.Player.Id, (int)GameWorld.Player.Posistion.X, (int)GameWorld.Player.Posistion.Y, GameWorld.currentScene, GameWorld.Player.Id,GameWorld.Player.ClueToken);

            int[] itemslots = new int[10];

            for (int i = 0; i < GameWorld.Player.Inventory.Length; i++)
            {
                if (GameWorld.Player.Inventory[i] != null)
                {
                    itemslots[i] = GameWorld.Player.Inventory[i].Id;
                }
                else
                {
                    itemslots[i] = 0;
                }
            }
            string inventorySave = string.Format("Update inventory set item1 = {1} where ID = {0};" +
                                                 "Update inventory set item2 = {2} where ID = {0};" +
                                                 "Update inventory set item3 = {3} where ID = {0};" +
                                                 "Update inventory set item4 = {4} where ID = {0};" +
                                                 "Update inventory set item5 = {5} where ID = {0};" +
                                                 "Update inventory set item6 = {6} where ID = {0};" +
                                                 "Update inventory set item7 = {7} where ID = {0};" +
                                                 "Update inventory set item8 = {8} where ID = {0};" +
                                                 "Update inventory set item9 = {9} where ID = {0};" +
                                                 "Update inventory set item10 = {10} where ID = {0};",
                                                 1, itemslots[0], itemslots[1], itemslots[2], itemslots[3], itemslots[4], itemslots[5], itemslots[6], itemslots[7], itemslots[8], itemslots[9]);
            SQLiteCommand commandOnCreate = new SQLiteCommand(playerSave, dbConn);
            commandOnCreate.ExecuteNonQuery();
            commandOnCreate = new SQLiteCommand(inventorySave, dbConn);
            commandOnCreate.ExecuteNonQuery();
            //end logic
            dbConn.Close();
        }
    }
}
