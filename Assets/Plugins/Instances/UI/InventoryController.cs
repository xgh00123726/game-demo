using GameBase.Inventorys;
using GameBase.Tools;
using Instance.UI.MVC;
using UnityEngine;

namespace GameBase.UI
{
    public class InventoryController<T_View, T_Data>
        where T_View : BaseViewItem, new()
        where T_Data : new()
    {
        public struct VDPair
        {
            public T_View vItem;
            public T_Data dItem;
        }

        protected BaseViewPanel<T_View> _view;
        protected CommonInventory<T_Data> _model;

        public InventoryController(BaseViewPanel<T_View> view, 
            CommonInventory<T_Data> model)
        {
            _view = view;
            _model = model;
        }

        public int Size
        {
            get => _model.Size;
            set
            {
                _model.Size = value;
                _view.FillItem(value);
            }
        }
        public virtual bool IsShow => _view.IsShow;

        public VDPair this[int i] => new VDPair() { vItem = _view[i], dItem = _model[i] };

        /// <summary>
        /// 通过modeldata设置viewItem
        /// </summary>
        /// <param name="data"></param>
        /// <param name="viewItem"></param>
        protected virtual void SetViewItem(T_Data data, T_View viewItem)
        {

        }

        /// <summary>
        /// modelData不存在时，设置viewItem
        /// </summary>
        /// <param name="viewItem"></param>
        protected virtual void SetNullViewItem(T_View viewItem)
        {
            viewItem.obj.SetActive(false);
            viewItem.triggerImage.SetIcon(-1);
            viewItem.triggerImage.Hide();
            viewItem.triggerImage.HideColor();
        }

        /// <summary>
        /// 往背包中添加一个物品，位置自行指定
        /// </summary>
        /// <param name="data"></param>
        public void Add(T_Data data)
        {
            var index = _model.AddItem(data);
            _view.FillItem(index + 1);
            SetViewItem(data, _view[index]);
        }

        /// <summary>
        /// 往背包中添加一个物品到index位置中，index从0开始
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        public void Add(T_Data data, int index)
        {
            _model.AddItem(data, index);
            _view.FillItem(index + 1);
            SetViewItem(data, _view[index]);
        }

        /// <summary>
        /// 移除指定位置的物品
        /// </summary>
        /// <param name="index"></param>
        public virtual void Remove(int index)
        {
            _model.RemoveItem(index);
            SetNullViewItem(_view[index]);
        }

        public void Swap(int p1, int p2)
        {
            _model.Swap(p1, p2);
            _view.SwapIconSprite(p1, p2);
            RefreshView(p1);
            RefreshView(p2);
        }

        /// <summary>
        /// 刷新index位置的视图
        /// </summary>
        /// <param name="index"></param>
        public void RefreshView(int index)
        {
            if (_model.HasItem(index))
            {
                SetViewItem(_model[index], _view[index]);
            }
            else
            {
                SetNullViewItem(_view[index]);
            }
        }

        /// <summary>
        /// 强制刷新所有显示界面
        /// </summary>
        public void RefreshView()
        {
            for (int i = 0; i < Size; i++)
            {
                RefreshView(i);
            }
        }

        /// <summary>
        /// index位置是否存在数据
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool HasItem(int index)
        {
            return _model.HasItem(index);
        }

        public T_View GetView(Vector3 position)
        {
            foreach (var e in _view.Entities)
            {
                Rect r = e.RectTransform.rect;
                r.center = e.uiScript.transform.position;
                if (r.Contains(position))
                {
                    return e;
                }
            }

            return null;
        }

        /// <summary>
        /// 显示视图
        /// </summary>
        public virtual void Show()
        {
            _view.Show();
        }

        /// <summary>
        /// 隐藏视图
        /// </summary>
        public virtual void Hide()
        {
            _view.Hide();
        }

        /// <summary>
        /// 切换视图可见性
        /// </summary>
        public virtual void Toggle()
        {
            _view.Toggle();
        }

        protected virtual void Update() { }
    }
}
