using GameBase.Creatures;
using GameBase.Modify;
using GameBase.Tools;
using GameBase.UI;
using TMPro;

namespace Instance
{
    public class AttrViewPanel : BaseViewPanel<AttrViewItem>
    {
        private static AttrViewPanel _instance;
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
            _instance = this;
        }
        protected override BaseUI InstantiateObj(AttrViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.valueTMP = e.obj.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        public void SetAttrKey(int key, int index)
        {
            FillItem(index + 1);
            this[index].attrKey = key;
            this[index].triggerImage.SetIcon(ModifyTable.GetIconTextureID(key));
        }

        public void SetAttrValue(Creature c)
        {
            if (c == null)
            {
                return;
            }
            SetAttrValue(c.modifyables);
        }

        public void SetAttrValue(Modifyables modifyables)
        {
            if (modifyables == null)
            {
                return;
            }
            foreach (var e in Entities)
            {
                if (modifyables.ContainsKey(e.attrKey))
                {
                    e.Value = modifyables[e.attrKey];
                }
            }
        }
    }
}
