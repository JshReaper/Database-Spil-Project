namespace Spillet
{
    class ClueToken : GameObject
    {
        public ClueToken(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {

        }

        public override void OnCollision(GameObject other)
        {
            //col stuff
        }
    }
}