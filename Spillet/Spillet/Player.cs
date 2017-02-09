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
        public Player(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
        }

        private bool toggle = false;
        public override void Update(float fps)
        {
            
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
                Posistion.Y -= 1;
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
                Posistion.Y += 1;
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

                Posistion.X += 1;
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
                Posistion.X -= 1;

            }
        }

        public override void OnCollision(GameObject other)
        {
            var house = other as House;
            if (house != null)
            {
                houseToEnter = house;
                if (MovingUp)
                {
                    Posistion.Y += 1;
                    MovingUp = false;
                }
                if (movingDown)
                {
                    Posistion.Y -= 1;
                    movingDown = false;
                }
                 if (movingLeft)
                {
                    Posistion.X += 1;
                    movingLeft = false;
                }
                 if (movingRight)
                {
                    Posistion.X -= 1;
                    movingRight = false;
                }
            }
        }
    }
}