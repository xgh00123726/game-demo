namespace GameBase.UI
{
    public class EquipmentItem : DetailableBaseItem
    {
        public EquipmentItem()
        {
            ObjID = 18;
        }

        public int placeholderTexureID;
        public IViewableEquipment bindEquipment;
        internal override int IconTexureID => bindEquipment.TextureID;
    }
}
