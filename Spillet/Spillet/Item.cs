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
        private string name; //would be printed out to the player while he hover over the item
        private byte id; //the ID of the item 
        public byte Id { get { return id; } }
        bool consumable; //example oil would be consumable to fill the latern
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