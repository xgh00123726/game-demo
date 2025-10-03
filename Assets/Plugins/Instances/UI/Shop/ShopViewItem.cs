using System;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class ShopViewItem : BaseViewItem
    {
        internal SuperImage identifyImage;
        internal GameObject identifyIconObj; 
        internal TextMeshProUGUI priceText;
        internal TextMeshProUGUI identifyText;

        public int Value
        {
            set
            {
                priceText.text = $"${value}";
            }
        }

        public void HideValue()
        {
            priceText.text = "";
        }
    }
}
