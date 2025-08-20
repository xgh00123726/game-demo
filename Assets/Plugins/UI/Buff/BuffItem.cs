using TMPro;


namespace GameBase.UI
{
    public class BuffItem : DetailableBaseItem
    {
        public BuffItem()
        {
            ObjID = 9;
        }
        public int iconTextureID;
        public IViewableBuff bindBuff;
        internal TextMeshProUGUI stackNumTMP;
        internal override int IconTexureID => iconTextureID;
    }
}
