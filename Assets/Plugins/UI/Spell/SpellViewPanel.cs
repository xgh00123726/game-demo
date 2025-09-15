using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class SpellViewPanel : BaseViewPanel<SpellViewItem, SpellViewPanel>
    {
        private int _lastClickedItemIndex = -1;
        internal override int PanelObjID => UIPanelConfig.Int.Spell_panelObjID;
        internal override int ShapeTexureID => UIPanelConfig.Int.Spell_shapeTexureID;
        internal override int ContourTexureID => UIPanelConfig.Int.Spell_contourTexureID;
        internal override float ItemWidth => UIPanelConfig.Float.Spell_itemWidth;
        internal override float ItemHeight => UIPanelConfig.Float.Spell_itemHeight;
        internal override float XInterval => UIPanelConfig.Float.Spell_xInterval;
        internal override float YInterval => UIPanelConfig.Float.Spell_yInterval;
        internal override float MaxPanelWidth => UIPanelConfig.Float.Spell_maxPanelWidth;
        internal override float PanelX => UIPanelConfig.Float.Spell_panelX;
        internal override float PanelY =>  UIPanelConfig.Float.Spell_panelY;
        internal override int ItemAlign =>  UIPanelConfig.Int.Spell_itemAlign;

        protected override BaseUI InstantiateObj(SpellViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.timeTMP = obj.transform.Find("CoolingDownText").GetComponent<TextMeshProUGUI>();

            e.chargeTMP = obj.transform.Find("Charge").GetComponent<TextMeshProUGUI>();

            var image = obj.transform.Find("Icon").GetComponent<Image>();
            if (image == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            e.iconImage = image;

            return obj;
        }

        protected override void UpdateEntity(SpellViewItem e)
        {
            base.UpdateEntity(e);

            float coolingTimeRemain = e.viewInfo.CoolingRemain;

            float fullVal = coolingTimeRemain / e.viewInfo.CoolingSet;
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

            if (e.Obj.isPointerDown)
            {
                _lastClickedItemIndex = CurrentIterateIndex;
            }
        }

        /// <summary>
        /// 设置e的物品贴图和材质
        /// </summary>
        /// <param name="e"></param>
        public void SetIcon(SpellViewItem e)
        {
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
        }

        public int LastClickedItemIndex => _lastClickedItemIndex;
    }
}
