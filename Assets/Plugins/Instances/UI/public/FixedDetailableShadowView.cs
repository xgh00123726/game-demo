using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Instance.UI
{
    public class FixedDetailableShadowView<T> : Singleton<FixedDetailableShadowView<T>>
        where T : InventoryViewItem
    {
        private GameObject _shadowObj;
        private Image _iconImage;
        private TextMeshProUGUI _textTMP;

        public FixedDetailableShadowView()
        {
            _shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(38));

            _shadowObj.transform.SetParent(RootCanvas.Instance.transform, false);

            _iconImage = _shadowObj.transform.Find("Icon").GetComponent<Image>();

            _textTMP = _shadowObj.transform.Find("Text").GetComponent<TextMeshProUGUI>();

            if (_iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }
        }

        public void SetText(string text)
        {
            _textTMP.text = text;
        }

        public void Show()
        {
            _shadowObj.SetActive(true);
        }

        public void Hide()
        {
            _shadowObj.SetActive(false);
        }
    }
}
