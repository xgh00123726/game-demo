using GameBase.EntitySystem;
using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using Instance;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BuffViewPanel : BaseViewPanel<BuffViewItem>
    {
        private static BuffViewPanel _instance = new ();
        public static BuffViewPanel Instance => _instance;

        private BaseObjectPool<GameObject> _pool = new();

        public BuffViewPanel(int prefabID = 10,
            int defaultObjID = 9) : base(
            prefabID,
            defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("error");
            }

            _pool.InstantiateFunc = () => GameObject.Instantiate(ResourcesLoader.GetPrefab(defaultObjID));
            _pool.InstantiateAction = static (e) => e.SetActive(true);
            _pool.ReleaseAction = static (e) => e.SetActive(false);
        }

        protected override GameObject GetGameObject(int id)
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

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureID));

            e.iconMaterial.SetTexture("_Target", texture);

            return obj;
        }

        public override BuffViewItem NewEntity(BuffViewItem e)
        {
            var ret = base.NewEntity(e);
            ret.removeFlag = false;
            return ret;
        }

        protected override void UpdateEntity(BuffViewItem e)
        {
            base.UpdateEntity(e);

            if (!e.buff.ALive)
            {
                RemoveEntity(e);
            }

            e.durationRemain = e.buff.DurationRemain;
            e.durationSet = e.buff.durationSet;
            e.iconMaterial.SetFloat("_MaskFull", 1 - e.durationRemain / e.durationSet);
        }
    }
}
