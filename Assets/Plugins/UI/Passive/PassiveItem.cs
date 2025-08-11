using TMPro;

namespace GameBase.UI
{
    public class PassiveItem : DetailableBaseItem
    {
        public PassiveItem()
        {
            ObjID = 13;
        }

        public IViewablePassive bindPassive;
        internal TextMeshProUGUI timeTMP;
        internal TextMeshProUGUI chargeTMP;

        internal override int IconTexureID => bindPassive.TextureID;
    }
}