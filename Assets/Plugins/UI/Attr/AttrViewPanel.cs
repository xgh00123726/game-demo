using TMPro;

namespace GameBase.UI
{
    public class AttrViewPanel : BaseViewPanel<AttrViewItem>
    {
        public AttrViewPanel(int prefabID = 15, int defaultObjID = 16) : base(
            prefabID,
            defaultObjID)
        {
        }
        protected override BaseUI InstantiateObj(AttrViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.valueTMP = obj.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            return obj;
        }
    }
}
