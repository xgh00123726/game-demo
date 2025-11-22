using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Instance
{
    /// <summary>
    /// 技能修饰器上标识被修饰的技能的UI
    /// </summary>
    public class SpellShadowView
    {
        internal GameObject obj;
        public SuperImage Image {  get; set; }
        public SpellShadowView(Transform parent)
        {
            obj = GameObject.Instantiate(ResourceMgr.Prefab.Get("Prefabs/UI/SpellItemShadowView.prefab"));

            obj.transform.SetParent(parent, false);

            obj.name = "SpellShadowView";

            var _rawImage = obj.transform.Find("Trigger").GetComponent<Image>();

            Image = new SuperImage(_rawImage);
            if (Image == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            Image.SetHideColor(_rawImage.color);
        }
    }
}
