using System.Net.Configuration;

namespace Spillet
{
    enum ItemType
    {
        Note,
        Lantern,
        Oil,
    }
    class Item : GameObject
    {
        private string name;
        private byte id;
        private ItemType myItemType;
        public Item(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, byte id) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.id = id;
            switch (id)
            {
                case 1:
                    myItemType = ItemType.Note;
                    name = "First Note";
                    break;
                default:
                    break;
            }
        }

        public override void OnCollision(GameObject other)
        {
            //coll stuff
        }
    }
}