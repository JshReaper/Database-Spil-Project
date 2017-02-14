using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spillet
{
    class StaticObject: GameObject
    {
        public StaticObject(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, bool canCollide) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.CanCollide = canCollide;
        }

        public bool CanCollide { get; }
    }
}
