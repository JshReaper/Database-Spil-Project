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
        Rectangle displayRectangle;
        BufferedGraphics backBuffer;
        float currentFps;
        Graphics dc;
        DateTime endTime;
        private static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> GameObjects { get { return gameObjects; } }
        public static int currentScene { get; set; }
        private Player player;
        private House house;
        public GameWorld(Graphics dc, Rectangle displayRectangle)
        {
            SetupWorld();
            this.displayRectangle = displayRectangle;
            backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            

        }

        void SetupWorld()
        {
            currentScene = 0;
            //add objects and so on which should be there on load
            player = new Player(1, @"Art Assets\\Player\\player_Idle_Right.png", new Vector2D(200, 200), 1, 1,100);
            
            house = new House(1, @"Art Assets\\Buildings\\house.png", new Vector2D(500, 300), 0.4f, 1,1);
            
        }

        public void GameLoop()
        {
            DateTime startTime = DateTime.Now; // Log start time
            TimeSpan deltaTime = startTime - endTime; //Time it took since last loop
            int milliSeconds = deltaTime.Milliseconds > 0 ? deltaTime.Milliseconds : 1;
            // Get milliseconds since last gameloop from the deltaTime
            currentFps = 1000f / milliSeconds; // Calculates current Fps
            endTime = DateTime.Now; // Log end time
            Update(currentFps); // Updates the game
            UpdateAnimation(currentFps); // Updates the animation
            Draw(); // Draws the game
            GC.Collect();
        }
         void Draw()
        {
            //clear all content
            dc.Clear(Color.Gray);
            //background
            switch (currentScene)
            {
                case 0:
                    if (!playerHasBeenReset)
                        resetPlayerPos();
                    gameObjects.Clear();
                    dc.DrawImage(Image.FromFile(@"Art Assets\Scenes\owbg.jpg"), 0, 0, displayRectangle.Height,
                        displayRectangle.Height);
                    gameObjects.Add(player);
                    gameObjects.Add(house);

                    break;
                case 1:
                    playerHasBeenReset = false;
                    if (!playerHasEnteredHouse)
                    {
                        playerOgPX = player.Posistion.X;
                        playerOgPY = player.Posistion.Y;
                        player.Posistion.X = 200;
                        player.Posistion.Y = 200;
                        playerHasEnteredHouse = true;
                    }
                    gameObjects.Clear();
                    gameObjects.Add(player);
                    break;
                   
            }
            foreach (var go in gameObjects) // Makes sure that we call draw on all gameobjects
                go.Draw(dc);
            backBuffer.Render();

        }

        private float playerOgPX = 200;
        private float playerOgPY = 200;
        private bool playerHasBeenReset;
        private bool playerHasEnteredHouse;
        void resetPlayerPos()
        {
            player.Posistion.X = playerOgPX;
            player.Posistion.Y = playerOgPY;
            playerHasBeenReset = true;
            playerHasEnteredHouse = false;
        }
        void Update(float fps)
        {
            player.FixedUpdate();
            foreach (var go in gameObjects)
            {
                go.Update(fps);
            }
            foreach (var gameObject in gameObjects)
            {
                gameObject.CheckCollision();
            }
        }

        void UpdateAnimation(float fps)
        {
            foreach (var go in gameObjects) // Makes sure that we call the UpdateAnimation on all GameObjects
                go.UpdateAnimation(fps);
        }
    }
}