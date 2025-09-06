using GameBase.Inventorys;
using GameBase.Resources;
using GameBase.UI;
using Instance.Inventory;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class InventoryController<T_ModelItem, T_Model, T_ViewItem, T_View, T_Controller>
    where T_ModelItem : struct, IModelItem
    where T_Model : InventoryModel<T_ModelItem>
    where T_ViewItem : InventoryViewItem, new()
    where T_View : InventoryViewPanel<T_ViewItem, T_View>, new()
    where T_Controller : InventoryController<T_ModelItem, T_Model, T_ViewItem, T_View, T_Controller>, new()
{
    private static T_Controller _controller;
    public static T_Controller Instance
    {
        get
        {
            _controller ??= new();
            return _controller;
        }
    }

    protected abstract T_Model Model { get; }
    protected abstract T_View View { get; }

    public void SetIconSprite(int index, int iconID)
    {
        var texture = ResourcesLoader.GetTexture2D(iconID);
        View[index].IconSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public bool TryGetDataOfView(T_ViewItem item, out T_ModelItem data)
    {
        var index = View.IndexOf(item);
        var ret = HasItemData(index);

        if (ret)
        {
            data = Model[index];
        }
        else
        {
            data = default;
        }

        return ret;
    }

    public bool TryGetItemUI(Vector3 position, out T_ViewItem e, out int index)
    {
        return View.TryGetItem(position, out e, out index);
    }

    public bool HasItemData(int index)
    {
        return Model.HasItem(index);
    }

    public T_ModelItem GetItemData(int index)
    {
        return Model[index];
    }

    public T_ViewItem GetItemUI(int index)
    {
        return View[index];
    }

    public void AddItem(T_ModelItem item)
    {
        var index = Model.AddItem(item);
    }

    public void RemoveItem(int position)
    {
        Model.RemoveItem(position);
    }

    public void SwapItem(int p1, int p2)
    {
        Model.Swap(p1, p2);
    }
}
