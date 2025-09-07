using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Instance.Inventory
{
    public class DetailableShadowView<T>
        where T : InventoryViewItem
    {
        private static DetailableShadowView<T> _instance = new();
        private GameObject _shadowObj;
        private Image _iconImage;
        private TextMeshProUGUI _textTMP;
        public DetailableShadowView()
        {
            _shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(4));

            _shadowObj.transform.SetParent(RootCanvas.Instance.transform, false);

            _iconImage = _shadowObj.transform.Find("Icon").GetComponent<Image>();

            _textTMP = _shadowObj.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            if (_iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }
        }

        public static DetailableShadowView<T> Instance => _instance;

        public void SetText(string text)
        {
            _textTMP.text = text;
        }


        public void SetPosition(Vector3 position)
        {
            _shadowObj.SetActive(true);
            _shadowObj.transform.position = position;
        }

        public void Hide()
        {
            _shadowObj.SetActive(false);
        }
    }
}
