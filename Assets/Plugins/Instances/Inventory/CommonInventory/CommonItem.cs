using GameBase.Inventorys;

namespace Instance.Inventory
{
    public struct CommonItem : IItem
    {
        public int ID { get; set; }
        public int iconTextureID;
    }
}
