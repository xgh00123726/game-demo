using GameBase.Inventorys;

namespace Instance.Inventory
{
    public struct CommonItemData : IModelItem
    {
        public int ID { get; set; }
        public int iconTextureID;
        public int buffID;
    }
}
