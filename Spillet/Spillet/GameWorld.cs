using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillet
{
    class GameWorld
    {
        private static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> GameObjects { get { return gameObjects; } }

        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
            SetupWorld();
        }
        
        void SetupWorld()
        {
            //add objects and so on which should be there on load
        }

        public void GameLoop()
        {
            
        }
        public void Draw()
        {
            
        }

        public void Update(float fps)
        {
            
        }
    }
}