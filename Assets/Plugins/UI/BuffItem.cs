using UnityEngine;
using UnityEngine.UI;
using GameBase.Buff;
using TMPro;
using GameBase.Tools;
using GameBase.Resources;

namespace GameBase.UI
{
    public class BuffItem : BaseUI
    {
        internal GameObject iconGO;
        internal GameObject maskGO;
        internal GameObject stackNumGO;

        internal TextMeshProUGUI stackNumTMP;
        internal Image maskGOImage;
        internal IViewableBuff bindBuff;
        internal bool isReleased = false;
        

        // 0: ÎÞmask
        // 1: ÌîÂú
        public float FillAmount
        {
            get => maskGOImage.fillAmount;
            set => maskGOImage.fillAmount = value;
        }
        private void Awake()
        {
            iconGO = transform.Find("Icon").gameObject;
            maskGO = transform.Find("Mask").gameObject;
            stackNumGO = transform.Find("StackNum").gameObject;

            maskGOImage = maskGO.GetComponent<Image>();
            stackNumTMP = stackNumGO.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (isReleased) return;
            if (bindBuff.DurationRemain <= 0)
            {
                isReleased = true;
            }
            maskGOImage.fillAmount = 1 - bindBuff.DurationRemain / bindBuff.DurationSet;
            stackNumTMP.text = bindBuff.StackNum.ToString();
        }

        public void Bind(IViewableBuff buff)
        {
            bindBuff = buff;
        }

        internal static void OnInstantiate(BuffItem item)
        {
            item.maskGOImage.fillAmount = 0;
            item.iconGO.SetActive(true);
            item.maskGO.SetActive(true);
            item.stackNumGO.SetActive(true);
            item.isReleased = false;
        }

        internal static void OnRelease(BuffItem item)
        {
            item.iconGO.SetActive(false);
            item.maskGO.SetActive(false);
            item.stackNumGO.SetActive(false);
            item.isReleased = true;
        }
    }
}
