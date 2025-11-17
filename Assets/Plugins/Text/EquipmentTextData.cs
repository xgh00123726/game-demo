using GameBase.Tools;

namespace GameBase.Texts
{
    public class EquipmentTextData : IKeywordText
    {
        public string name;
        public string detail;

        void IKeywordText.ReplaceKeywords()
        {
            detail = KeywordsMgr.Instance.ReplaceString(detail);
        }
    }
}
