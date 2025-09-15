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

            e.colorHide = e.iconImage.color;
            e.colorShow = new Color(e.colorHide.r, e.colorHide.g, e.colorHide.b, 1);

            return ui;
        }

        public void Swap(int p1, int p2)
        {
            container[p1].SwapIconSprite(container[p2]);
        }
    }
}
