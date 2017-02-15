using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillet
{
    class GameButton: GameObject
    {
        public GameButton(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
        }
    }
}
