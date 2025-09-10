using GameBase.Infos;
using GameBase.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class AttrViewPanel : BaseViewPanel<AttrViewItem, AttrViewPanel>
    {
        internal override float ItemWidth => UIPanelConfig.Float.Attr_itemWidth;
        internal override float ItemHeight => UIPanelConfig.Float.Attr_itemHeight;
        internal override float XInterval => UIPanelConfig.Float.Attr_xInterval;
        internal override float YInterval => UIPanelConfig.Float.Attr_yInterval;
        internal override float MaxPanelWidth => UIPanelConfig.Float.Attr_maxPanelWidth;
        internal override float PanelX => UIPanelConfig.Float.Attr_panelX;
        internal override float PanelY => UIPanelConfig.Float.Attr_panelY;
        internal override int PanelObjID => UIPanelConfig.Int.Attr_panelObjID;
        internal override int ShapeTexureID => UIPanelConfig.Int.Attr_shapeTexureID;
        internal override int ContourTexureID => UIPanelConfig.Int.Attr_contourTexureID;
        internal override int ItemAlign => UIPanelConfig.Int.Attr_itemAlign;

        protected override BaseUI InstantiateObj(AttrViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.valueTMP = obj.transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            var texture = Texture2D.Instantiate(ResourcesLoader.GetTexture2D(e.bindAttr.IconTextureID));

            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            obj.transform.Find("Icon").GetComponent<Image>().sprite = sprite;

            return obj;
        }

        protected override void UpdateEntity(AttrViewItem e)
        {
            base.UpdateEntity(e);

            e.valueTMP.text = e.bindAttr.Value;
        }
    }
}
