using GameBase.EntitySystem;
using GameBase.UI;
using System;

namespace Instance
{
    public abstract class UIInteractive<T, T_Panel, T_PanelItem> : SingletonInstance<T>
        where T : UIInteractive<T, T_Panel, T_PanelItem>, new()
        where T_Panel : BaseViewPanel<T_PanelItem>
        where T_PanelItem : BaseViewItem, new()
    {
        public float enterDragTime = 0.1f;
        public float enterDetailTime = 0.2f;

        public Func<int, bool> IsUIItemDrag;
        public Func<int, bool> IsUIItemDetail;

        public UIInteractive()
        {
            IsUIItemDrag += DefaultIsUIItemDrag;
            IsUIItemDetail += DefaultIsUIItemDetail;
        }

        protected abstract T_Panel GetPanel();

        private bool DefaultIsUIItemDrag(int i)
        {
            var panel = Instance.GetPanel();
            if (panel == null)
            {
                return false;
            }
            if (!panel.IsShow)
            {
                return false;
            }

            return panel[i].uiScript.PointerDownTime > enterDragTime;
        }

        private bool DefaultIsUIItemDetail(int i)
        {
            var panel = Instance.GetPanel();
            if (panel == null)
            {
                return false;
            }
            if (!panel.IsShow)
            {
                return false;
            }

            return panel[i].uiScript.EnterTime > enterDetailTime;
        }

        protected override void Update()
        {
            var panel = Instance.GetPanel();
            for (int i = 0; i < panel.Entities.Count; i++)
            {
                if (IsUIItemDrag?.Invoke(i) == true)
                {
                    panel.EnterDragState(i);
                }
                else if (IsUIItemDrag?.Invoke(i) == false)
                {
                    panel.ExitDragState(i);
                }

                if (IsUIItemDetail?.Invoke(i) == true) 
                { 
                    panel.EnterDetailState(i);
                }
                else if (IsUIItemDetail?.Invoke(i) == false)
                {
                    panel.ExitDetailState(i);
                }
            }
        }
    }
}
