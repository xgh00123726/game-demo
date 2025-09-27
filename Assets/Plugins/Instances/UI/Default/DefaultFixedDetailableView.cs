using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class DefaultFixedDetailableView
    {
        public GameObject shadowObj;
        private Image _iconImage;
        private TextMeshProUGUI _textTMP;

        public DefaultFixedDetailableView()
        {
            shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(38));

            shadowObj.transform.SetParent(RootCanvas.Instance.transform, false);

            _iconImage = shadowObj.transform.Find("Trigger").GetComponent<Image>();

            _textTMP = shadowObj.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        }

        public void SetText(string text)
        {
            _textTMP.text = text;
        }

        public void Show()
        {
            shadowObj.SetActive(true);
        }

        public void Hide()
        {
            shadowObj.SetActive(false);
        }
    }
}
