using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    public class AttrItem : BaseUI, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject _highLight;
        private GameObject _keyObj;
        private GameObject _valueObj;
        private GameObject _backGround;

        private TextMeshProUGUI _keyText;
        private TextMeshProUGUI _valueText;

        public string KeyText
        {
            get => _keyText.text;
            set => _keyText.text = value;
        }
        public string ValueText
        {
            get => _valueText.text;
            set => _valueText.text = value;
        }

        private void Awake()
        {
            _highLight = transform.Find("HighLight").gameObject;
            _keyObj = transform.Find("Text_Key").gameObject;
            _valueObj = transform.Find("Text_Value").gameObject;
            _backGround = transform.Find("BackGround").gameObject;

            _keyText = _keyObj.GetComponent<TextMeshProUGUI>();
            _valueText = _valueObj.GetComponent <TextMeshProUGUI>();
        }
        public void SetHighLight(bool enable)
        {
            _highLight.SetActive(enable);
            _backGround.SetActive(!enable);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            SetHighLight(true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            SetHighLight(false);
        }
    }
}
