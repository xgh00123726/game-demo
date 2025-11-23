using GameBase.Creatures;
using GameBase.Modify;
using GameBase.Tools;
using GameBase.UI;
using TMPro;

namespace Instance
{
    public class AttrViewPanel : BaseViewPanel<AttrViewItem, AttrViewPanel>
    {
        protected override string PanelPrefabName => "Prefabs/UI/AttrPanel";
        protected override string ItemPrefabName => "Prefabs/UI/AttrItem";
        protected override void OnGet(AttrViewItem e)
        {
            base.OnGet(e);
            e.valueTMP = e.Obj.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();
        }

        public void SetAttrKey(int key, int index)
        {
            FillItem(index + 1);
            this[index].attrKey = key;
            this[index].TriggerImage.SetIcon(ModifyTable.GetTextureName(key));
        }

        public void SetAttrValue(Creature c)
        {
            if (c == null)
            {
                return;
            }
            SetAttrValue(c.Modifyables);
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
                    e.Value = modifyables[e.attrKey].Value;
                }
            }
        }
    }
}
