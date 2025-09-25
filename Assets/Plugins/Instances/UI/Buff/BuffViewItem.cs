using GameBase.Buffs;
using GameBase.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace GameBase.UI
{
    public class BuffViewItem : BaseViewItem
    {
        internal Material iconMaterial;
        internal Buff buff;

        public bool removeFlag;
        public float durationRemain;
        public float durationSet = 1;

        public void SetOwner(Buff buff)
        {
            this.buff = buff;
        }

        public void SetIcon(int iconTextureID)
        {
            iconMaterial.SetTexture("_Target", GameObject.Instantiate(ResourcesLoader.GetTexture2D(iconTextureID)));
        }
    }
}
