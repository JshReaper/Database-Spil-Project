using System.Windows.Forms;

namespace Spillet
{
    class Player : GameObject
    {
        private bool movingRight;
        private bool movingLeft;
        private bool MovingUp;
        private bool movingDown;
        public Player(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
        }

        public override void Update(float fps)
        {
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
            if (other is House)
            {
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