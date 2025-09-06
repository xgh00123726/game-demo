using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public abstract class InventoryViewPanel<T, T_instance> : BaseViewPanel<T, T_instance>
        where T : InventoryViewItem, new()
        where T_instance : InventoryViewPanel<T, T_instance>, new()
    {
        protected LinearNonReleaseEntityContainer<T> _container;

        protected override RectTransform GetRectTransform(T e)
        {
            return e.Obj.transform.Find("Icon").GetComponent<RectTransform>();
        }

        protected override BaseUI InstantiateObj(T e)
        {
            var ui = base.InstantiateObj(e);

            e.iconImage = e.Obj.transform.Find("Icon").GetComponent<Image>();
            if (e.iconImage == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            return ui;
        }

        protected override void Awake()
        {
            base.Awake();

            _container = new LinearNonReleaseEntityContainer<T>();
            Container = _container;
        }

        public int IndexOf(T e)
        {
            return _container.IndexOf(e);
        }

        public T this[int i]
        {
            get => _container[i];
            set => _container[i] = value;
        }
    }
}
