using System.Drawing;
using System.Windows.Forms;

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

        public float MoveSpeed { get { return moveSpeed; } }
        private float moveSpeed;
        public Player(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, float sanity) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.sanity = sanity;
            

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
        public override void Update(float fps)
        {
            translation = 1 / fps;
            enterHouseTimer += 1/ fps;
            removeEnemyTimer += 1 / fps;
            moveSpeed = speed * translation;
            if (enterHouseTimer >= 3)
            {
                houseToEnter = null;
            }
            if (Keyboard.IsKeyDown(Keys.E) && houseToEnter != null && !toggle)
            {
                GameWorld.currentScene = houseToEnter.MyNumber;
                houseToEnter = null;
                toggle = !toggle;
            }
            else if(Keyboard.IsKeyDown(Keys.E) && !toggle)
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

        public override void OnCollision(GameObject other)
        {
            var house = other as House;
            if (house != null)
            {
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