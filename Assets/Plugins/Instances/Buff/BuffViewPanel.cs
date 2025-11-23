using GameBase.Buffs;
using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using GameBase.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Instance
{
    public class BuffViewPanel : BaseViewPanel<BuffViewItem, BuffViewPanel>
    {
        private static BuffViewPanel _instance;
        protected override string PanelPrefabName => "Prefabs/UI/BuffPanel";
        protected override string ItemPrefabName => "Prefabs/UI/BuffItem";
        protected override void OnGet(BuffViewItem e)
        {
            base.OnGet(e);
            var image = e.UIScript.GetComponent<Image>();
            e.iconMaterial = new Material(image.material);
            image.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);
        }

        public void UpdateItem(Buff buff, int index)
        {
            FillItem(index + 1);

            var item = this[index];

            item.Obj.SetActive(true);
            item.buff = buff;
            item.iconMaterial.SetTexture("_Target", GameObject.Instantiate(ResourceMgr.Texture2D.Get(buff.TextureName)));
        }

        public void UpdatePanel(Creature c)
        {
            int index = 0;
            foreach (var buff in c.Buffs)
            {
                UpdateItem(buff, index++);
            }
            for (int i = index; i < Entities.Count; i++)
            {
                this[i].Obj.SetActive(false);
            }
        }

        protected override void UpdateEntity(BuffViewItem e)
        {
            base.UpdateEntity(e);

            if (!e.buff.ALive)
            {
                RemoveEntity(e);
            }

            var durationRemain = e.buff.DurationRemain;
            var durationSet = e.buff.DurationSet;
            e.iconMaterial.SetFloat("_MaskFull", 1 - durationRemain / durationSet);
        }
    }
}
