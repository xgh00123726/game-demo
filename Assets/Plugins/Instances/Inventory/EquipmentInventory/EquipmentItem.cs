using GameBase.Inventorys;

namespace Instance.Inventory
{
    public struct EquipmentItem : IItem
    {
        public int ID { get; set; }
        public int iconTextureID;
        public int buffID;
    }
}
