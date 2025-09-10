using GameBase.Buffs;
using GameBase.Inventorys;
using Instance.MVC;
using System;
using System.Collections.Generic;

public class EquipmentInventoryModel : InventoryModel<CommonItemData>, IInventoryModel<CommonItemData>
{
    private List<Buff> buffs;

    public EquipmentInventoryModel(int capacity)
    {
        buffs = new List<Buff>(capacity);
        for (int i = 0; i < capacity; ++i)
        {
            buffs.Add(null);
        }
        Size = capacity;
    }

    public override void RemoveItem(int position)
    {
        base.RemoveItem(position);
        buffs[position] = null;
    }

    public override int AddItem(CommonItemData item, int index)
    {
        var ret = base.AddItem(item, index);
        var eb = Constructor.Buffs.Factory.Instance.Get(Constructor.Buffs.Type.Common, item.buffID);
        eb.uiStyle = GameBase.Buffs.UIStyle.None;
        eb.durationSet = 9999;
        buffs[index] = eb;

        return ret;
    }

    public override void Swap(int p1, int p2)
    {
        base.Swap(p1, p2);
        (buffs[p1], buffs[p2]) = (buffs[p2], buffs[p1]);
    }

    public Buff GetBuff(int index)
    {
        return buffs[index];
    }
}
