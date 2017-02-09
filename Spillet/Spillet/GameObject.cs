﻿using System.Collections.Generic;
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
        private List<Image> animationFrames;
        protected Rectangle spritePart;
        private float speed;

        public GameObject(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed)
        {
            this.speed = speed; // Sets the movement speed
            this.scaleFactor = scaleFactor; // Sets the scalefactor
            this.animationSpeed = animationSpeed; // Sets the animation speed
            string[] imagePaths = imgPath.Split(';'); // Stores all paths in an array
            this.position = pos; // Sets the start position 
            this.animationFrames = new List<Image>(); // Instantiates the list of animations
          
                foreach (string path in imagePaths) // Adds all images to the list
                {
                    animationFrames.Add(Image.FromFile(path));
                }
                this.sprite = this.animationFrames[0]; // Selects a default sprite
            
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

        public virtual void Update(float fps)
       {
           
       }

       public void Draw(Graphics dc)
       {
            dc.DrawImage(sprite, position.X, position.Y, sprite.Width * scaleFactor, sprite.Width * scaleFactor);
       }

        public void UpdateAnimation(float fps)
        {
            // Calculates the factor to make the animatoin framerate independent
            float factor = 1 / fps;
            // Claculates the current index
            currentFrameIndex += factor * animationSpeed;
            // Checks if we need to reset the animation 
            if (currentFrameIndex >= animationFrames.Count)
            {
                currentFrameIndex = 0;
            }
            // Changes the sprite
            sprite = animationFrames[(int)currentFrameIndex];

        }
    }
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