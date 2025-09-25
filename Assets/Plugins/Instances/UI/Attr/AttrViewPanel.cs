using GameBase.Creatures;
using GameBase.Modify;
using GameBase.Tools;
using TMPro;

namespace GameBase.UI
{
    public class AttrViewPanel : BaseViewPanel<AttrViewItem>
    {
        private static AttrViewPanel _instance = new(15, 16);
        public static AttrViewPanel Instance => _instance;
        public AttrViewPanel(int prefabID = 15, int defaultObjID = 16) : base(
            prefabID,
            defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("error");
            }
        }
        protected override BaseUI InstantiateObj(AttrViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.valueTMP = e.obj.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        public void SetAttrValue(int[] attrIDs, Creature c)
        {
            SetAttrValue(attrIDs, c.Modifyables);
        }

        public void SetAttrValue(int[] attrIDs, Modifyables modifyables)
        {
            for (int i = 0; i < attrIDs.Length; i++) 
            {
                int id = attrIDs[i];
                float value = modifyables[id];
                this[i].Value = value;
            }
        }
        public void SetAttrIcon(int[] attrIDs)
        {
            FillItem(attrIDs.Length);
            for (int i = 0; i < attrIDs.Length; i++)
            {
                int id = attrIDs[i];
                this[i].triggerImage.SetIcon(ModifyTable.GetIconTextureID(id));
            }
        }
    }
}
