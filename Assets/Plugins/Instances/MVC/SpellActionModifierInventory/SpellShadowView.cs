using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Instance.MVC
{
    /// <summary>
    /// 技能修饰器上标识被修饰的技能的UI
    /// </summary>
    public class SpellShadowView
    {
        private static GameObject _shadowObj;
        private static Image _iconImage;
        private static Color _defaultColor;

        static SpellShadowView()
        {
            _shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(44));

            _shadowObj.name = "SpellShadowView";

            _shadowObj.transform.SetParent(RootCanvas.Instance.transform, false);

            _iconImage = _shadowObj.transform.Find("Icon").GetComponent<Image>();
            if (_iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            _defaultColor = _iconImage.color;
        }

        public static void SetPosition(Vector3 position)
        {
            _shadowObj.SetActive(true);
            _shadowObj.transform.position = position;
        }

        public static void ShowColor(Color color)
        {
            _iconImage.color = color;
        }

        public static void RestoreColor()
        {
            _iconImage.color = _defaultColor;
        }

        public static void Show()
        {
            _shadowObj.SetActive(true);
        }

        public static void Hide()
        {
            _shadowObj.SetActive(false);
        }

        public static void CopyIcon(BaseViewItem e)
        {
            _iconImage.sprite = e.InstantiateSpriteFromIconTextureID(e.IconTextureID);
        }
    }
}
