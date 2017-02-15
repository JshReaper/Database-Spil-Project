namespace Spillet
{
    class Enemy : GameObject
    {
        private float fearFactor;

        public Enemy(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, float fearFactor) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.fearFactor = fearFactor;

        }

        public float FearFactor
        {
            get
            {
                return fearFactor;
            }
        }
    

        public bool HaveEncountered { get; set; }

        public override void OnCollision(GameObject other)
        {
            //coll stuff
        }
    }
}