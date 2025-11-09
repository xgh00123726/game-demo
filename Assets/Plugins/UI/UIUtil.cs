namespace GameBase.UI
{
    public static class UIUtil
    {
        public static BaseViewPanel<BaseViewItem> NewDefaultPanel(string prefabID, string defaultObjID)
        {
            return new BaseViewPanel<BaseViewItem>(prefabID, defaultObjID);
        }
    }
}
