using System;
using System.Drawing;
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

        public void drawDesc(Graphics dc)
        {
            dc.DrawRectangle(new Pen(Brushes.DarkGray),new Rectangle(new Point(40,490),new Size(600,250)));
            dc.DrawString(eventText, new Font("Arial", 11), Brushes.Black, 50, 500);
        }
    }
    
}