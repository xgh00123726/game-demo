using GameBase.Creatures;
using GameBase.Inventorys;
using GameBase.Spells;
using UnityEngine;
using Factory = Constructor.Spells.Main.Factory;

namespace Instance.MVC
{
    public struct SpellItemData
    {
        public Constructor.Spells.Main.Type type;
        public int id;
    }
    public class SpellModel : IInventoryModel<SpellItemData>
    {
        private InventoryModel<SpellItemData> _inventoryModel = new();

        public const int MAX_SPELL_NUM = 5;
        public Creature owner;

        SpellItemData IInventoryModel<SpellItemData>.this[int index] => _inventoryModel[index];

        int IInventoryModel<SpellItemData>.Size
        {
            get => _inventoryModel.Size;
            set => _inventoryModel.Size = value;
        }

        int IInventoryModel<SpellItemData>.AddItem(SpellItemData item)
        {
            var ret = _inventoryModel.AddItem(item);
            owner.spells[ret] = Factory.Instance.Get(item.type, item.id);
            return ret;
        }

        int IInventoryModel<SpellItemData>.AddItem(SpellItemData item, int index)
        {
            var ret = _inventoryModel.AddItem(item, index);
            owner.spells[ret] = Factory.Instance.Get(item.type, item.id);
            return ret;
        }

        bool IInventoryModel<SpellItemData>.HasItem(int index)
        {
            return _inventoryModel.HasItem(index);
        }

        void IInventoryModel<SpellItemData>.RemoveItem(int index)
        {
            _inventoryModel.RemoveItem(index);
            owner.spells[index] = null;
        }

        void IInventoryModel<SpellItemData>.Swap(int p1, int p2)
        {
            _inventoryModel.Swap(p1, p2);
            (owner.spells[p1], owner.spells[p2]) = (owner.spells[p2], owner.spells[p1]);
        }
    }
}
