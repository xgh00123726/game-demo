using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BuffViewPanel : BaseViewPanel<BuffViewItem, BuffViewPanel>
    {
        internal override int PanelObjID => UIPanelConfig.Int.Buff_panelObjID;
        internal override int ShapeTexureID => UIPanelConfig.Int.Buff_shapeTexureID;
        internal override int ContourTexureID => UIPanelConfig.Int.Buff_contourTexureID;
        internal override float ItemWidth => UIPanelConfig.Float.Buff_itemWidth;
        internal override float ItemHeight => UIPanelConfig.Float.Buff_itemHeight;
        internal override float XInterval => UIPanelConfig.Float.Buff_xInterval;
        internal override float YInterval => UIPanelConfig.Float.Buff_yInterval;
        internal override float MaxPanelWidth => UIPanelConfig.Float.Buff_maxPanelWidth;
        internal override float PanelX => UIPanelConfig.Float.Buff_panelX;
        internal override float PanelY => UIPanelConfig.Float.Buff_panelY;
        internal override int ItemAlign => UIPanelConfig.Int.Buff_itemAlign;

        protected override void AfterInstantiateEUObject(BuffViewItem e)
        {
            base.AfterInstantiateEUObject(e);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureID)); 
            e.iconMaterial.SetTexture("_Target", texture);
        }

        protected override BaseUI InstantiateObj(BuffViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.iconMaterial = new Material(e.iconImage.material);
            e.iconImage.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureID));
            var shape = GameObject.Instantiate(ResourcesLoader.GetTexture2D(ShapeTexureID));
            var contour = GameObject.Instantiate(ResourcesLoader.GetTexture2D(ContourTexureID));

            e.iconMaterial.SetTexture("_Shape", shape);
            e.iconMaterial.SetTexture("_Contour", contour);
            e.iconMaterial.SetTexture("_Target", texture);

            e.stackNumTMP = obj.transform.Find("StackNum").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        protected override void UpdateEntity(BuffViewItem e)
        {
            base.UpdateEntity(e);

            if (!e.bindBuff.Alive)
            {
                RemoveEntity(e);
            }

            e.iconMaterial.SetFloat("_MaskFull", 1 - e.bindBuff.DurationRemain / e.bindBuff.DurationSet);
        }
    }
}
