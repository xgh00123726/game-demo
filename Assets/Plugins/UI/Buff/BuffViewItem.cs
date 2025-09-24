using TMPro;
using UnityEngine;


namespace GameBase.UI
{
    public class BuffViewItem : BaseViewItem
    {
        internal Material iconMaterial;
        internal TextMeshProUGUI stackNumTMP;

        public bool removeFlag;
        public float durationRemain;
        public float durationSet;

        public new int IconTextureID
        {
            get => iconTextureID;
            set => iconTextureID = value;
        }
    }
}
