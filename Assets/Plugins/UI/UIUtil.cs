namespace GameBase.UI
{
    public static class UIUtil
    {
        public static BaseViewPanel<BaseViewItem> NewDefaultPanel(int prefabID, int defaultObjID)
        {
            return new BaseViewPanel<BaseViewItem>(prefabID, defaultObjID);
        }
    }
}
