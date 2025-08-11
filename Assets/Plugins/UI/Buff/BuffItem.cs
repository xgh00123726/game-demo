using TMPro;


namespace GameBase.UI
{
    public class BuffItem : DetailableBaseItem
    {
        public BuffItem()
        {
            ObjID = 9;
        }
        public IViewableBuff bindBuff;
        internal TextMeshProUGUI stackNumTMP;
        internal override int IconTexureID => bindBuff.TextureID;
    }
}
