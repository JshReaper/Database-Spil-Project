namespace Spillet
{
    class Item : GameObject
    {
        public Item(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
        }

        public override void OnCollision(GameObject other)
        {
            //coll stuff
        }
    }
}