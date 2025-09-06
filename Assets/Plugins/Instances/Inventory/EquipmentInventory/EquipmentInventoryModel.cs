using GameBase.Buffs;
using GameBase.Inventorys;
using Instance.Inventory;
using System.Collections.Generic;

public class EquipmentInventoryModel : InventoryModel<CommonItemData>
{
    private List<Buff> buffs;
    public EquipmentInventoryModel(int capacity)
    {
        buffs = new List<Buff>(6);
    }

    public Buff GetBuff(int index)
    {
        return buffs[index];
    }
}
