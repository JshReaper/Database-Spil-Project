using System;

namespace Spillet
{
    class GameEvent
    {
        int clueTokens;
        string eventText;
    public GameEvent(byte ID)
        {
            string reciver = string.Format("Select * from Event where id = {0}", ID);
            eventText = DataManager.RetriveEventInfo(reciver);
            reciver = string.Format("Select * from Event where id = {0}",ID);
            clueTokens = Convert.ToInt32(DataManager.RetriveEventClues(reciver));
        }
            

    }
    
}