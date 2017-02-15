using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Spillet
{
    class Player : GameObject
    {
        private bool movingRight;
        private bool movingLeft;
        private bool MovingUp;
        private bool movingDown;
        private House houseToEnter;
        private float sanity;
        private List<Item> inventory = new List<Item>();

        public List<Item> Invintory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public float Sanity
        {
            get {return sanity; }
            set { sanity = value; }
        }

        public float MoveSpeed { get { return moveSpeed; } }
        private float moveSpeed;
        private byte id;
        public byte Id { get
        {
            return id;
        } }
        public Player(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, float sanity, byte id) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.sanity = sanity;
            this.id = id;
        }

        private bool toggle = false;
        private float enterHouseTimer = 0;

        void OnEnemyEncounter(Enemy enemy)
        {
            sanity -= enemy.FearFactor;
        }

        public void FixedUpdate()
        {
            for (int i = 0; i < GameWorld.GameObjects.Count; i++)
            {
                if (GameWorld.GameObjects[i] is Enemy)
                {
                    Enemy enemy = (Enemy)GameWorld.GameObjects[i];
                    if (GameWorld.GameObjects[i].Posistion.Dist(Posistion) <= 10 && !enemy.HaveEncountered)
                    {
                        
                        OnEnemyEncounter((Enemy)GameWorld.GameObjects[i]);
                        enemy.HaveEncountered = true;
                        removeEnemyTimer = 0;
                    }
                }
            }
            if (removeEnemyTimer > 3)
            {
                for (int i = 0; i < GameWorld.GameObjects.Count; i++)
                {
                    if (GameWorld.GameObjects[i] is Enemy)
                    {
                        Enemy enemy = (Enemy)GameWorld.GameObjects[i];
                        if(enemy.HaveEncountered)
                        GameWorld.GameObjects.Remove(enemy);
                    }
                }
            }
        }

        private float removeEnemyTimer;
        private float translation;
        public override void Draw(Graphics dc)
        {
            base.Draw(dc);
          //  pickUpItem = true;
            Font font1 = new Font("Arial", 15);
            Brush brush1 = new SolidBrush(Color.White);
            string text;
            if (pickUpItem)
            {
                text = "Press E to\nPickup item";
                dc.DrawString(text, font1, brush1, 765, 50);
            }
            if (enterHouse)
            {
                text = "Press E to\nEnter the building";
                dc.DrawString(text, font1, brush1, 765, 50);
            }
        }

        public override void Update(float fps)
        {
            
            translation = 1 / fps;
            enterHouseTimer += 1/ fps;
            removeEnemyTimer += 1 / fps;
            moveSpeed = speed * translation;
            if (Posistion.Y <= 0)
            {
                Posistion.Y += moveSpeed;
            }
            if (Posistion.Y >= GameWorld.DisplayRectangle.Bottom-(sprite.Height/1.4f))
            {
                Posistion.Y -= moveSpeed;
            }
            if (Posistion.X <= 0)
            {
                Posistion.X += moveSpeed;
            }
            if (Posistion.X >= GameWorld.DisplayRectangle.Right- sprite.Width)
            {
                Posistion.X -= moveSpeed;
            }
            if (enterHouseTimer >= 3)
            {
                houseToEnter = null;
                enterHouse = false;
            }
            if (Keyboard.IsKeyDown(Keys.E) && houseToEnter != null && !toggle)
            {
                GameWorld.currentScene = houseToEnter.MyNumber;
                houseToEnter = null;
                toggle = !toggle;
            }
            else if (Keyboard.IsKeyDown(Keys.E) && itemToPickUp != null && !toggle)
            {
                itemToPickUp.Posistion.X = 700;
                itemToPickUp.Posistion.Y = 500;
                inventory.Add(itemToPickUp);
                itemToPickUp = null;
                pickUpItem = false;
                toggle = !toggle;
            }
            else if(Keyboard.IsKeyDown(Keys.E) && !toggle && Posistion.Y <= 120 && Posistion.X <= 50)
            {
                GameWorld.currentScene = 0;
                toggle = !toggle;
            }
            else if (!Keyboard.IsKeyDown(Keys.E) && toggle)
            {
                toggle = !toggle;
            }
            if (Keyboard.IsKeyDown(Keys.W))
            {
                MovingUp = true;
            }
            else
            {
                MovingUp = false;
            }
            if (MovingUp)
            {
                Posistion.Y -= moveSpeed;
            }
            if (Keyboard.IsKeyDown(Keys.S))
            {
                movingDown = true;
            }
            else
            {
                movingDown = false;
            }
            if (movingDown)
            { 
                Posistion.Y += moveSpeed;
            }
            if (Keyboard.IsKeyDown(Keys.D))
            {
                movingRight = true;
            }
            else
            {
                movingRight = false;
            }
            if (movingRight)
            {

                Posistion.X += moveSpeed;
            }
            if (Keyboard.IsKeyDown(Keys.A))
            {
                movingLeft = true;
            }
            else
            {
                movingLeft = false;
            }
            if (movingLeft)
            {
                Posistion.X -= moveSpeed;

            }
        }

        private bool pickUpItem;
        private bool enterHouse;
        private Item itemToPickUp;
        public override void OnCollision(GameObject other)
        {
            var house = other as House;
            var staticObject = other as StaticObject;
            var item = other as Item;
            
                if (item != null)
                {
                    pickUpItem = true;
                    itemToPickUp = item;
                }
                else
                {
                    pickUpItem = false;
                }
            if (house != null || staticObject !=null && staticObject.CanCollide)
            {
                if (house != null)
                {
                    enterHouse = true;
                }
                enterHouseTimer = 0;
                houseToEnter = house;
                if (MovingUp)
                {
                    Posistion.Y += moveSpeed;
                }
                if (movingDown)
                {
                    Posistion.Y -= moveSpeed;
                }
                 if (movingLeft)
                {
                    Posistion.X += moveSpeed;
                }
                 if (movingRight)
                {
                    Posistion.X -= moveSpeed;
                }
            }
        }
    }
}