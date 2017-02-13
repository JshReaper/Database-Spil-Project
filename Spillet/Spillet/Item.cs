using System.Net.Configuration;

namespace Spillet
{
    class Item : GameObject
    {
        private string name;
        public Item(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed,byte ID) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            
        }

        public override void OnCollision(GameObject other)
        {
            //coll stuff
        }
    }
}