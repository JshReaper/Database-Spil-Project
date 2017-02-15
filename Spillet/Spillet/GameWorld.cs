using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spillet
{
    class GameWorld
    {
        Form1 form;
        Rectangle displayRectangle;
        private static Rectangle staticDisplayRectangle;
        public static Rectangle DisplayRectangle { get { return staticDisplayRectangle; }}
        BufferedGraphics backBuffer;
        float currentFps;
        Graphics dc;
        DateTime endTime;
        private static List<GameObject> gameObjects = new List<GameObject>();
        public static List<GameObject> GameObjects { get { return gameObjects; } }
        public static int currentScene { get; set; }
        private float playerOgPX = 200;
        private float playerOgPY = 200;
        private bool playerHasBeenReset;
        private bool playerHasEnteredHouse;

        //gameobjects
        private House house;
        private House house2;
        private House house3;
        private StaticObject inBg1;
        private StaticObject bed;
        private Item note;
        private Player player;
        private static Player staticPlayer;
        private StaticObject inBg3;
        private StaticObject inBg2;
        public static Player Player{ get { return staticPlayer; } }

        //GameEvents
        GameEvent he;
        //constructer for gameworld
        public GameWorld(Graphics dc, Rectangle displayRectangle, Form1 f)
        {
            form = f;
            SetupWorld();
            this.displayRectangle = displayRectangle;
            backBuffer = BufferedGraphicsManager.Current.Allocate(dc, displayRectangle);
            this.dc = backBuffer.Graphics;
            

        }

        private GameButton load_btn;
        private GameButton options_btn;
        private GameButton start_btn;
        private StaticObject MenuTitle;
        void SetupWorld()
        {
            currentScene = -1;
            //add objects and so on which should be there on load

            //scene -1 assets (menu)
            load_btn = new GameButton(0, @"Art assets\\Menus\\Main Menu\\Load Button.png", new Vector2D(50, 155), 1, 0,ButtonType.Load);
            MenuTitle = new StaticObject(0,@"Art Assets\Menus\Main Menu\Main title menu.png",new Vector2D(0,0),1,0,false );
            options_btn = new GameButton(0, @"Art assets\\Menus\\Main Menu\\Options Button.png", new Vector2D(50, 180), 1, 0,ButtonType.Options);
            start_btn = new GameButton(0, @"Art assets\\Menus\\Main Menu\\Start Button.png", new Vector2D(50, 130), 1, 0,ButtonType.Start);
            

            //scene 0 assets
            house = new House(1, @"Art Assets\\Buildings\\house.png", new Vector2D(300, 250), 0.3f, 0,1);
            house2 = new House(1, @"Art Assets\\Buildings\\house.png", new Vector2D(330, 100), 0.3f, 0, 2);
            house3 = new House(1, @"Art Assets\\Buildings\\house.png", new Vector2D(50, 300), 0.3f, 0, 3);

            //create player last
            player = new Player(60, @"Art Assets\\Player\\player_Idle_Right.png", new Vector2D(200, 200), 0.75f, 1, 100,1);
            
            //scene 1 assets
            inBg1 = new StaticObject(0, @"Art Assets\Scenes\House0.png", new Vector2D(0, 0), 1.06f, 0, false);
            bed = new StaticObject(0, @"Art Assets\Props\bed0.png", new Vector2D(630, 270), 0.13f, 0, true);
            note = new Item(0, @"Art Assets\Props\note.png", new Vector2D(100, 100), 1, 0, 1);

            //scene 2 assets
            inBg2 = new StaticObject(0, @"Art Assets\Scenes\House1.png", new Vector2D(0, 0), 1.06f, 0, false);
            //scene 3 assets
            inBg3 = new StaticObject(0, @"Art Assets\Scenes\House2.png", new Vector2D(0, 0), 1.06f, 0, false);


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
            PrintText();
            foreach (var go in gameObjects) // Makes sure that we call draw on all gameobjects
                go.Draw(dc);
            if (he != null)
                he.drawDesc(dc);
            backBuffer.Render();
        }

        void PrintText()
        {
            Font font1 = new Font("Arial", 15);
            Brush brush1 = new SolidBrush(Color.White);
            string text;
            if (currentScene != 0 && player.Posistion.Y <= 120 && player.Posistion.X <= 50)
            {
                text = "Press E to\nExit the building";
                dc.DrawString(text, font1, brush1, 765, 50);
            }
        }
        
        void resetPlayerPos()
        {
            player.Posistion.X = playerOgPX;
            player.Posistion.Y = playerOgPY;
            playerHasBeenReset = true;
            playerHasEnteredHouse = false;
        }
        bool Limiter = true;
        void Update(float fps)
        {
            staticPlayer = player;
            staticDisplayRectangle = displayRectangle;
            player.FixedUpdate();
            foreach (var go in gameObjects)
            {
                go.Update(fps);
            }
            foreach (var gameObject in gameObjects)
            {
                gameObject.CheckCollision();
            }
            SceneController(player.MoveSpeed);
            if (player.Sanity <= 0)
            {
                GameOver();
            }

            if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.LButton) && Limiter)
            {
               PointF f = form.PointToClient(System.Windows.Forms.Cursor.Position);
                MouseClick click = new MouseClick(0,@"Art Assets\Mouse\mouse.png",new Vector2D(f.X,f.Y),0.01f,0);
                gameObjects.Add(click);
                Limiter = false;
            }else if (!Keyboard.IsKeyDown(System.Windows.Forms.Keys.LButton) && !Limiter)
            {
                Limiter = true;
            }
        }

        void NewGame()
        {
            
        }

        void ContinueGame()
        {
            
        }
        private void GameOver()
        {
            //the game ends // player get shown restart or similar screen
        }

        void UpdateAnimation(float fps)
        {
            foreach (var go in gameObjects) // Makes sure that we call the UpdateAnimation on all GameObjects
                go.UpdateAnimation(fps);
        }

        void SceneController(float movespeed)
        {
            switch (currentScene)
            {
                case -1:
                    gameObjects.Add(MenuTitle);
                    gameObjects.Add(start_btn);
                    gameObjects.Add(load_btn);
                    gameObjects.Add(options_btn);
                    break;
                case 0:
                    if (!playerHasBeenReset)
                        resetPlayerPos();
                    gameObjects.Clear();
                    StaticObject outBg = new StaticObject(0, @"Art Assets\Scenes\greenBg.png", new Vector2D(0, 0), 0.6f, 0, false);
                    gameObjects.Add(outBg);
                    gameObjects.Add(house);
                    gameObjects.Add(house2);
                    gameObjects.Add(house3);
                    //removing the Event
                    he = null;
                    //insert player last
                    gameObjects.Add(player);

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
                        if (player.Sanity >= 80)
                        {
                            he = new GameEvent(1);
                        }else
                        if (player.Sanity >= 60)
                        {
                            he = new GameEvent(2);
                        }else
                        if (player.Sanity >= 40)
                        {
                            he = new GameEvent(3);
                        }else
                        if (player.Sanity < 40)
                        {
                            he = new GameEvent(4);
                        }
                    }
                    
                    gameObjects.Clear();
                    
                    gameObjects.Add(inBg1);
                    gameObjects.Add(bed);
                    gameObjects.Add(note);
                    //insert player last
                    gameObjects.Add(player);
                    if (player.Posistion.Y < 44)
                    {
                        player.Posistion.Y += movespeed;
                    }
                    if (player.Posistion.Y > 296)
                    {
                        player.Posistion.Y -= movespeed;
                    }
                    if (player.Posistion.X < 16)
                    {
                        player.Posistion.X += movespeed;
                    }
                    if (player.Posistion.X > 698)
                    {
                        player.Posistion.X -= movespeed;
                    }
                    break;
                case 2:
                    playerHasBeenReset = false;
                    if (!playerHasEnteredHouse)
                    {
                        playerOgPX = player.Posistion.X;
                        playerOgPY = player.Posistion.Y;
                        player.Posistion.X = 200;
                        player.Posistion.Y = 200;
                        playerHasEnteredHouse = true;
                        if (player.Sanity >= 80)
                        {
                            he = new GameEvent(1);
                        }
                        else
                        if (player.Sanity >= 60)
                        {
                            he = new GameEvent(2);
                        }
                        else
                        if (player.Sanity >= 40)
                        {
                            he = new GameEvent(3);
                        }
                        else
                        if (player.Sanity < 40)
                        {
                            he = new GameEvent(4);
                        }
                    }
                    gameObjects.Clear();

                    gameObjects.Add(inBg1);
                    gameObjects.Add(bed);
                    gameObjects.Add(note);
                    //insert player last
                    gameObjects.Add(player);
                    if (player.Posistion.Y < 44)
                    {
                        player.Posistion.Y += movespeed;
                    }
                    if (player.Posistion.Y > 296)
                    {
                        player.Posistion.Y -= movespeed;
                    }
                    if (player.Posistion.X < 16)
                    {
                        player.Posistion.X += movespeed;
                    }
                    if (player.Posistion.X > 698)
                    {
                        player.Posistion.X -= movespeed;
                    }
                    break;
                case 3:
                    playerHasBeenReset = false;
                    if (!playerHasEnteredHouse)
                    {
                        playerOgPX = player.Posistion.X;
                        playerOgPY = player.Posistion.Y;
                        player.Posistion.X = 200;
                        player.Posistion.Y = 200;
                        playerHasEnteredHouse = true;
                        if (player.Sanity >= 80)
                        {
                            he = new GameEvent(1);
                        }
                        else
                        if (player.Sanity >= 60)
                        {
                            he = new GameEvent(2);
                        }
                        else
                        if (player.Sanity >= 40)
                        {
                            he = new GameEvent(3);
                        }
                        else
                        if (player.Sanity < 40)
                        {
                            he = new GameEvent(4);
                        }
                    }
                    gameObjects.Clear();

                    gameObjects.Add(inBg1);
                    gameObjects.Add(bed);
                    gameObjects.Add(note);
                    //insert player last
                    gameObjects.Add(player);
                    if (player.Posistion.Y < 44)
                    {
                        player.Posistion.Y += movespeed;
                    }
                    if (player.Posistion.Y > 296)
                    {
                        player.Posistion.Y -= movespeed;
                    }
                    if (player.Posistion.X < 16)
                    {
                        player.Posistion.X += movespeed;
                    }
                    if (player.Posistion.X > 698)
                    {
                        player.Posistion.X -= movespeed;
                    }
                    break;
            }
        }
    }
}