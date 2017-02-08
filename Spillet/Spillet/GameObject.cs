using System.Drawing;

namespace Spillet
{
   abstract class GameObject
   {
        private bool alive;
        private Vector2D position;
        protected Rectangle displayRectangle;
        private float animationSpeed;
        private float scaleFactor;
        private float currentFrameIndex;
        private List<Image> animationFrame;
        protected Rectangle spritePart;

        public GameObject()
        {

        }
        //Alive
        public bool Alive
        {
            get
            {
                return alive;
            }
            set
            {
                alive = value;
            }
        }
        //Henter og sætter værdigen af position
        protected Vector2D Posistion
        {
            get { return position; }
            set { position = value; }
        }

       public void Update(float fps)
       {
           
       }
   }
}