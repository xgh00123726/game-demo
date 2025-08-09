using GameBase.Config;
using GameBase.Resources;
using GameBase.Tools;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class SpellPanel : BasePanel<SpellItem, SpellPanel>
    {
        internal override int PanelObjID => UIPanelConfig.Int.Spell_panelObjID;

        internal override int MaskTexureID => UIPanelConfig.Int.Spell_maskTexureID;

        internal override float ItemWidth => UIPanelConfig.Float.Spell_itemWidth;

        internal override float ItemHeight => UIPanelConfig.Float.Spell_itemHeight;

        internal override float XInterval => UIPanelConfig.Float.Spell_xInterval;

        internal override float YInterval => UIPanelConfig.Float.Spell_yInterval;

        internal override float MaxPanelWidth => UIPanelConfig.Float.Spell_maxPanelWidth;
        internal override float PanelX => UIPanelConfig.Float.Spell_panelX;
        internal override float PanelY =>  UIPanelConfig.Float.Spell_panelY;

        internal override int ItemAlign =>  UIPanelConfig.Int.Spell_itemAlign;

        protected override GameObject InstantiateObj(SpellItem e)
        {
            var obj = base.InstantiateObj(e);

            e.timeTMP = obj.transform.Find("CoolingDownText").GetComponent<TextMeshProUGUI>();

            e.chargeTMP = obj.transform.Find("Charge").GetComponent<TextMeshProUGUI>();

            return obj;
        }
        protected override void UpdateEntity(SpellItem e)
        {
            base.UpdateEntity(e);

            float coolingTimeRemain = e.bindSpell.CoolingRemain;

            float fullVal = coolingTimeRemain / e.bindSpell.CoolingSet;
            e.iconMaterial.SetFloat("_MaskFull", fullVal);

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
        }
    }
}
