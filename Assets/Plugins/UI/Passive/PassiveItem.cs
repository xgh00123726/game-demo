using TMPro;

namespace GameBase.UI
{
    public class PassiveItem : BaseViewItem
    {
        public PassiveItem()
        {
            ObjID = 13;
        }

        public IViewablePassive bindPassive;
    }
}