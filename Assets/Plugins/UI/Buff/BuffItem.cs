using TMPro;
using UnityEngine;


namespace GameBase.UI
{
    public class BuffItem : BasePanelItem
    {
        internal Material iconMaterial;
        internal TextMeshProUGUI stackNumTMP;

        public int iconTextureID;
        public IViewableBuff bindBuff;

        internal override int IconTexureID => iconTextureID;
        public BuffItem()
        {
            ObjID = 9;
        }
    }
}
