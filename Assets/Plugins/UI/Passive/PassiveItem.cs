using TMPro;

namespace GameBase.UI
{
    public class PassiveItem : BasePanelItem
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