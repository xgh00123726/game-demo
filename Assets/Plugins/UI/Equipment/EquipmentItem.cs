namespace GameBase.UI
{
    public class EquipmentItem : InventoryItem
    {
        public int placeholderTexureID;
        public IViewableEquipment bindEquipment;

        public EquipmentItem()
        {
            ObjID = 18;
        }

        internal override int IconTexureID => bindEquipment.TextureID;
    }
}
