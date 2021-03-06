﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillet
{
    enum ButtonType
    {
        Start,
        Load,
        Options,
        Save,
    }
    class GameButton: GameObject
    {
        private ButtonType myType;
        public GameButton(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, ButtonType myType) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.myType = myType;
        }

        private bool newGameGenerated = false;
        private bool gameHasLoaded = false;
        public override void OnCollision(GameObject other)
        {
            if (other is MouseClick)
            {
                if (myType == ButtonType.Start)
                {
                    if (!newGameGenerated)
                    { 
                        DataManager.NewGame();
                        GameWorld.currentScene = 0;
                        newGameGenerated = true;
                    }
                }
                if (myType == ButtonType.Load)
                {
                    if (!gameHasLoaded)
                    { 
                        DataManager.ContinueGame();
                        gameHasLoaded = true;
                    }
                }
                if (myType == ButtonType.Options)
                {

                }
                if (myType == ButtonType.Save)
                {
                    DataManager.Save();
                }
            }
        }
    }
}
