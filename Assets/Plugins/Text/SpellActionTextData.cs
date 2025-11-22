using GameBase.Tools;

namespace GameBase.Texts
{
    public class SpellActionTextData : IKeywordText
    {
        public string Name;
        public string Detail;

        void IKeywordText.ReplaceKeywords()
        {
            Detail = KeywordsMgr.Instance.ReplaceString(Detail);
        }
    }
}