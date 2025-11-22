using GameBase.Tools;

namespace GameBase.Texts
{
    public class EquipmentTextData : IKeywordText
    {
        public string Name {  get; set; }
        public string Detail {  get; set; }

        void IKeywordText.ReplaceKeywords()
        {
            Detail = KeywordsMgr.Instance.ReplaceString(Detail);
        }
    }
}
