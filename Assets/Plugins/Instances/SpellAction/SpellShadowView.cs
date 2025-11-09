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
        internal GameObject shadowObj;
        public SuperImage image;
        public SpellShadowView(Transform parent)
        {
            shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab("Prefabs/UI/SpellItemShadowView.prefab"));

            shadowObj.transform.SetParent(parent, false);

            shadowObj.name = "SpellShadowView";

            var _rawImage = shadowObj.transform.Find("Trigger").GetComponent<Image>();

            image = new SuperImage(_rawImage);
            if (image == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            image.SetHideColor(_rawImage.color);
        }
    }
}
