using TMPro;
using UnityEngine;


namespace GameBase.UI
{
    public class BuffViewItem : BaseViewItem
    {
        internal Material iconMaterial;
        internal TextMeshProUGUI stackNumTMP;

        public int iconTextureID;
        public IViewableBuff bindBuff;

        public BuffViewItem()
        {
            ObjID = 9;
        }
    }
}
