using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

namespace GameBase.UI
{
    public class AttrItem : BaseUI, IPointerEnterHandler, IPointerExitHandler
    {
        internal GameObject highLight;
        internal GameObject keyObj;
        internal GameObject valueObj;
        internal GameObject backGround;

        internal TextMeshProUGUI keyText;
        internal TextMeshProUGUI valueText;

        public string KeyText
        {
            get => keyText.text;
            set => keyText.text = value;
        }
        public string ValueText
        {
            get => valueText.text;
            set => valueText.text = value;
        }

        private void Awake()
        {
            highLight = transform.Find("HighLight").gameObject;
            keyObj = transform.Find("Text_Key").gameObject;
            valueObj = transform.Find("Text_Value").gameObject;
            backGround = transform.Find("BackGround").gameObject;

            keyText = keyObj.GetComponent<TextMeshProUGUI>();
            valueText = valueObj.GetComponent <TextMeshProUGUI>();
        }
        public void SetHighLight(bool enable)
        {
            highLight.SetActive(enable);
            backGround.SetActive(!enable);
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
