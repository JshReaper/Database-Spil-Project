namespace Spillet
{
   abstract class GameObject
    {
        private Vector2D posistion;

        public GameObject(Vector2D posistion)
        {
            this.posistion = posistion;
        }
    }
}