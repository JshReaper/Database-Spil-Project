namespace Spillet
{
    class House : GameObject
    {
        private int myNumber;
        public int MyNumber { get { return myNumber; } }
        public House(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed,int myNumber) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.myNumber = myNumber;
        }

        public override void OnCollision(GameObject other)
        {
            //col stuff
        }
    }
}