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
        bool consumable;
        private ItemType myItemType;
        public Item(float speed, string imgPath, Vector2D pos, float scaleFactor, float animationSpeed, byte id) : base(speed, imgPath, pos, scaleFactor, animationSpeed)
        {
            this.id = id;
            if (DataManager.RetriveItemType(id) == "Note")
                myItemType = ItemType.Note;
            if (DataManager.RetriveItemType(id) == "Lantern")
                myItemType = ItemType.Lantern;
            if (DataManager.RetriveItemType(id) == "Oil")
                myItemType = ItemType.Oil;
            name = DataManager.RetriveItemName(id);
            consumable = DataManager.RetriveItemBool(id);
        }

        public override void Update(float fps)
        {

        }


        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {

            }
        }
    }
}