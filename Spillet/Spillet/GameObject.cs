using System.Collections.Generic;
using System.Drawing;

namespace Spillet
{
   abstract class GameObject
   {
        private bool alive;
        private Vector2D position;
        protected Image sprite;
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


        /// <summary>
        /// laver en hitbox til billede
        /// </summary>

        public RectangleF CollisionBox
        {
            get
            {
                return new RectangleF(position.X, position.Y, sprite.Width * scaleFactor, sprite.Height * scaleFactor);
            }
            set { CollisionBox = value; }
        }

        /// <summary>
        /// Tager variablerne og instantier dem.
        /// </summary>
        public GameObject(string imagepath, float animationSpeed, float scaleFactor, Vector2D startPos, Rectangle spritePart)
        {

            this.Alive = true;
            this.spritePart = spritePart;

            this.Position = startPos;
            this.scaleFactor = scaleFactor;
            this.animationSpeed = animationSpeed;
            string[] imagePaths = imagepath.Split(';');
            this.animationFrame = new List<Image>();

            foreach (string path in imagePaths)
            {
                animationFrame.Add(Image.FromFile(path));
            }
            this.sprite = this.animationFrame[0];
        }


        /// <summary>
        /// Tjekker om der er et GameObject hitbox som kolider med andre GameObjects
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsCollidingWith(GameObject other)
        {
            return CollisionBox.IntersectsWith(other.CollisionBox);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fps"></param>
        public virtual void Update(float fps)
        {
            float Translation = (1 / fps);
            CheackCollision();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fps"></param>
        public virtual void UpdateAnimation(float fps)
        {
            float factor = (1 / fps);
            currentFrameIndex += factor * animationSpeed;
            if (currentFrameIndex >= animationFrame.Count)
            {
                currentFrameIndex = 0;
            }
            sprite = animationFrame[(int)currentFrameIndex];
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="dc"></param>
        public virtual void Draw(Graphics dc)
        {
            dc.DrawImage(sprite, position.X, position.Y, sprite.Width * scaleFactor, sprite.Width * scaleFactor);
            //#if Debug
            // dc.DrawRectangle(new Pen(Brushes.Red), CollisionBox.X, CollisionBox.Y, CollisionBox.Width, CollisionBox.Height);
            //#endif 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        public abstract void OnCollision(GameObject other);
        /// <summary>
        /// /
        /// </summary>
        public void CheackCollision()
        {
            foreach (GameObject go in GameWorld.Objects)
            {
                if (go != this)
                {
                    if (this.IsCollidingWith(go))
                    {
                        OnCollision(go);
                    }
                }
            }
        }
    }
}