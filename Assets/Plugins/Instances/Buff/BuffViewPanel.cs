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
    public class BuffViewPanel : BaseViewPanel<BuffViewItem>
    {
        private static BuffViewPanel _instance;
        public static BuffViewPanel Instance => _instance;

        private BaseObjectPool<GameObject> _pool = new();

        public BuffViewPanel(string prefabName = "Prefabs/UI/BuffPanel",
            string itemPrefabName = "Prefabs/UI/BuffItem") : base(
            prefabName,
            itemPrefabName)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("error");
            }
            _instance = this;
            _pool.InstantiateFunc = () => GameObject.Instantiate(ResourcesLoader.GetPrefab(itemPrefabName));
            _pool.InstantiateAction = static (e) => e.SetActive(true);
            _pool.ReleaseAction = static (e) => e.SetActive(false);
        }

        protected override GameObject GetGameObject(string name)
        {
            return _pool.Get();
        }

        protected override void Remove(BuffViewItem e)
        {
            base.Remove(e);
            _pool.Release(e.obj);
        }

        protected override BaseUI InstantiateObj(BuffViewItem e)
        {
            var obj = base.InstantiateObj(e);

            var image = e.uiScript.GetComponent<Image>();
            e.iconMaterial = new Material(image.material);
            image.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureName));

            e.iconMaterial.SetTexture("_Target", texture);

            return obj;
        }

        public void UpdateItem(Buff buff, int index)
        {
            FillItem(index + 1);

            var item = this[index];
            var info = BuffDataBase.Instance[buff.id];

            item.obj.SetActive(true);
            item.buff = buff;
            item.iconMaterial.SetTexture("_Target", GameObject.Instantiate(ResourcesLoader.GetTexture2D(info.textureName)));
        }

        public void UpdatePanel(Creature c)
        {
            int index = 0;
            foreach (var buff in c.buffs)
            {
                var info = BuffDataBase.Instance[buff.id];
                if (info.type == BuffType.Common)
                {
                    UpdateItem(buff, index++);
                }
            }
            for (int i = index; i < Entities.Count; i++)
            {
                this[i].obj.SetActive(false);
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
            var durationSet = e.buff.durationSet;
            e.iconMaterial.SetFloat("_MaskFull", 1 - durationRemain / durationSet);
        }
    }
}
