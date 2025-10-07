using GameBase.Creatures;
using GameBase.Tools;
using GameBase.UI;
using System;
using TMPro;
using UnityEngine.UI;

namespace Instance
{
    public class SpellViewPanel : BaseViewPanel<SpellViewItem>
    {
        private int _lastClickedItemIndex = -1;

        public Action<int> OnClickedItem;
        private static SpellViewPanel _instance;
        public static SpellViewPanel Instance => _instance;
        public SpellViewPanel(int prefabID = 12,
            int defaultObjID = 11) : base(
            prefabID,
            defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
            _instance = this;
        }

        protected override BaseUI InstantiateObj(SpellViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.timeTMP = e.obj.transform.Find("CoolingDownText").GetComponent<TextMeshProUGUI>();

            e.chargeTMP = e.obj.transform.Find("Charge").GetComponent<TextMeshProUGUI>();

            var image = e.uiScript.GetComponent<Image>();

            e.maskImage = new MaskImage(image);

            return obj;
        }

        protected override void UpdateEntity(SpellViewItem e)
        {
            base.UpdateEntity(e);

            float coolingTimeRemain = e.coolingTimeRemain;

            float fullVal = coolingTimeRemain / e.coolingTimeSet;
            e.maskImage.Material.SetFloat("_MaskFull", fullVal);

            string coolingText = string.Empty;
            if (coolingTimeRemain > 1)
            {
                coolingText = ((int)coolingTimeRemain).ToString();
            }
            else if (coolingTimeRemain > 0)
            {
                coolingText = $".{(int)(coolingTimeRemain * 10)}";
            }
            e.timeTMP.text = coolingText;

            if (e.uiScript.IsPointerDown)
            {
                OnClickedItem?.Invoke(e.ItemIndex);
                _lastClickedItemIndex = e.ItemIndex;
            }
        }

        public void UpdatePanel(Creature c)
        {
            var spells = c.spells;
            var size = c.spells.Size;
            FillItem(size);
            for (int i = 0; i < size; ++i)
            {
                this[i].maskImage.SetIcon((int)spells[i].iconTextureID);
            }
            for (int i = 0; i < spells.Size; ++i)
            {
                this[i].coolingTimeRemain = spells[i].spellCoolingdown.CoolingRemain;
                this[i].coolingTimeSet = spells[i].spellCoolingdown.CoolingSet;
            }
        }

        public int LastClickedItemIndex => _lastClickedItemIndex;
    }
}
