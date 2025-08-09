using TMPro;
using UnityEngine;
using GameBase.Config;

namespace GameBase.UI
{
    public class BuffPanel : BasePanel<BuffItem, BuffPanel>
    {
        internal override int PanelObjID => UIPanelConfig.Int.Buff_panelObjID;

        internal override int MaskTexureID => UIPanelConfig.Int.Buff_maskTexureID;

        internal override float ItemWidth => UIPanelConfig.Float.Buff_itemWidth;

        internal override float ItemHeight => UIPanelConfig.Float.Buff_itemHeight;

        internal override float XInterval => UIPanelConfig.Float.Buff_xInterval;

        internal override float YInterval => UIPanelConfig.Float.Buff_yInterval;

        internal override float MaxPanelWidth => UIPanelConfig.Float.Buff_maxPanelWidth;
        internal override float PanelX => UIPanelConfig.Float.Buff_panelX;
        internal override float PanelY => UIPanelConfig.Float.Buff_panelY;

        internal override int ItemAlign => UIPanelConfig.Int.Buff_itemAlign;

        protected override GameObject InstantiateObj(BuffItem e)
        {
            var obj = base.InstantiateObj(e);

            e.stackNumTMP = obj.transform.Find("StackNum").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        protected override void UpdateEntity(BuffItem e)
        {
            base.UpdateEntity(e);

            if (!e.bindBuff.Alive)
            {
                RemoveEntity(e);
            }

            e.iconMaterial.SetFloat("_MaskFull", e.bindBuff.DurationRemain / e.bindBuff.DurationSet);
        }
    }
}
