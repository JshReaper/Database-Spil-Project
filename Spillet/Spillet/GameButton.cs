using System;
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
    }
    class GameButton: GameObject
    {
        private ButtonType myType;
        public GameButton(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, ButtonType myType) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.myType = myType;
        }

        public override void OnCollision(GameObject other)
        {
            if (other is MouseClick)
            {
                if (myType == ButtonType.Start)
                {
                    GameWorld.currentScene = 1;
                }
                if (myType == ButtonType.Load)
                {

                }
                if (myType == ButtonType.Options)
                {

                }
            }
        }
    }
}
