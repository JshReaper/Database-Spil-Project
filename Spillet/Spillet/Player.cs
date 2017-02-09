namespace Spillet
{
    class Player : GameObject
    {
        public Player(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
        }

        public override void Update(float fps)
        {
            //add player movement
        }
    }
}