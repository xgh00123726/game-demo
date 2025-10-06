using GameBase.Resources;
using GameBase.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Instance
{
    public class CommonDetailView
    {
        private GameObject _shadowObj;
        private Image _iconImage;
        private TextMeshProUGUI _textTMP;
        private RectTransform _iconRectTransform;
        private RectTransform _textRectTransform;

        private float widthMax = 1920;
        //private float heightMax = 1080;
        private float _width;
        private float _height;

        public CommonDetailView(int prefabID = 4)
        {
            _shadowObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(prefabID));

            _shadowObj.transform.SetParent(RootCanvas.Instance.Layer(1), false);

            _iconImage = _shadowObj.transform.Find("Trigger").GetComponent<Image>();

            _textTMP = _shadowObj.transform.Find("Text").GetComponent<TextMeshProUGUI>();

            _iconRectTransform = _shadowObj.transform.Find("Trigger").GetComponent<RectTransform>();

            _textRectTransform = _shadowObj.transform.Find("Text").GetComponent<RectTransform>();

            _width = _iconRectTransform.sizeDelta.x;
            _height = _iconRectTransform.sizeDelta.y;

            Hide();
        }
        public void SetText(string text)
        {
            _textTMP.text = text;
        }

        /// <summary>
        /// <list type="bullet">
        /// <item>x=0,y=1, 默认状态，框在右下</item>
        /// <item>x=0,y=0, 右上</item>
        /// <item>x=1,y=1, 左上</item>
        /// <item>x=1,y=0, 左下</item>
        /// </list>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SetPivot(float x, float y)
        {
            Vector3 iconPos = _iconRectTransform.localPosition;
            Vector3 textPos = _textRectTransform.localPosition;

            _iconRectTransform.pivot = new Vector2(x, y);
            _iconRectTransform.localPosition = iconPos;

            _textRectTransform.pivot = new Vector2(x, y);
            _textRectTransform.localPosition = textPos;
        }

        public void SetPosition(Vector3 position)
        {
            position.x = position.x + 50;
            _shadowObj.transform.position = position;

            float x = position.x;
            float y = position.y;

            float pivotX = 0;
            float pivotY = 1;
            if (x + _width > widthMax)
            {
                pivotX = 1;
            }
            if (y < _height)
            {
                pivotY = 0;
            }
            SetPivot(pivotX, pivotY);
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
