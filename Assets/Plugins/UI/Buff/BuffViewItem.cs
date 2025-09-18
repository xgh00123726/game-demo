using TMPro;
using UnityEngine;


namespace GameBase.UI
{
    public class BuffViewItem : BaseViewItem
    {
        internal Material iconMaterial;
        internal TextMeshProUGUI stackNumTMP;

        public new int IconTextureID
        {
            get => iconTextureID;
            set => iconTextureID = value;
        }

        public IViewableBuff bindBuff;

        public BuffViewItem()
        {
            ObjID = 9;
        }
    }
}
