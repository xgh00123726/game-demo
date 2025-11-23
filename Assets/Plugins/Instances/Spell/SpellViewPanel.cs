using GameBase.Creatures;
using GameBase.Tools;
using GameBase.UI;
using System;
using TMPro;
using UnityEngine.UI;

namespace Instance
{
    public class SpellViewPanel : BaseViewPanel<SpellViewItem, SpellViewPanel>
    {
        protected override string PanelPrefabName => "Prefabs/UI/SpellPanel";
        protected override string ItemPrefabName => "Prefabs/UI/SpellItem";
        private int _lastClickedItemIndex = -1;
        public Action<int> OnClickedItem;

        protected override void OnGet(SpellViewItem e)
        {
            base.OnGet(e);
            e.timeTMP = e.Obj.transform.Find("CoolingDownText").GetComponent<TextMeshProUGUI>();

            e.chargeTMP = e.Obj.transform.Find("Charge").GetComponent<TextMeshProUGUI>();

            var image = e.UIScript.GetComponent<Image>();

            e.MaskImage = new MaskImage(image);
        }

        protected override void UpdateEntity(SpellViewItem e)
        {
            base.UpdateEntity(e);

            float coolingTimeRemain = e.CoolingTimeRemain;

            float fullVal = coolingTimeRemain / e.CoolingTimeSet;
            e.MaskImage.Material.SetFloat("_MaskFull", fullVal);

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

            if (e.UIScript.IsPointerDown)
            {
                OnClickedItem?.Invoke(e.ItemIndex);
                _lastClickedItemIndex = e.ItemIndex;
            }
        }

        public void UpdatePanel(Creature c)
        {
            var spells = c.Spells;
            var size = c.Spells.Size;
            FillItem(size);
            for (int i = 0; i < size; ++i)
            {
                this[i].Obj.SetActive(true);
                this[i].MaskImage.SetIcon(spells[i].TextureName);
            }
            for (int i = 0; i < size; ++i)
            {
                this[i].CoolingTimeRemain = spells[i].CooldownRemain;
                this[i].CoolingTimeSet = spells[i].Cooldown;
            }

            for (int i = size; i < Entities.Count; ++i)
            {
                this[i].Obj.SetActive(false);
            }
        }

        public int LastClickedItemIndex => _lastClickedItemIndex;
    }
}
