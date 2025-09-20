using Constructor.Spells.Interactive;
using GameBase.Inventorys;
using GameBase.UI.MVC;
using GameBase.Creatures;
using Factory = Constructor.Spells.Main.Factory;

namespace Instance.UI.MVC
{
    public struct SpellItemData
    {
        public Constructor.Spells.Main.Type type;
        public int id;
        public int iconTextureID;
    }
    public class SpellModel : IMVCModel<SpellItemData>
    {
        public Creature owner;
        private DynInventoryModel<SpellItemData> _inventoryModel = new();

        SpellItemData IMVCModel<SpellItemData>.this[int index] => _inventoryModel[index];

        int IMVCModel<SpellItemData>.Size
        {
            get => _inventoryModel.Size;
            set => _inventoryModel.Size = value;
        }

        int IMVCModel<SpellItemData>.AddItem(SpellItemData item)
        {
            var ret = _inventoryModel.AddItem(item);
            var spell = Factory.Instance.Get(item.type, item.id);

            owner.spells.AddItem(spell);
            
            return ret;
        }

        int IMVCModel<SpellItemData>.AddItem(SpellItemData item, int index)
        {
            _inventoryModel.AddItem(item, index);
            _inventoryModel.SortItems();

            var ret = _inventoryModel.Count - 1;
            var spell = Factory.Instance.Get(item.type, item.id);

            owner.spells.AddItem(spell, index);

            return ret;
        }

        bool IMVCModel<SpellItemData>.HasItem(int index)
        {
            return _inventoryModel.HasItem(index);
        }

        bool IMVCModel<SpellItemData>.RemoveItem(int index)
        {
            if(_inventoryModel.RemoveItem(index))
            {
                return owner.spells.RemoveItem(index);
            }

            return false;
        }

        void IMVCModel<SpellItemData>.Swap(int p1, int p2)
        {
            _inventoryModel.Swap(p1, p2);
            owner.spells.Swap(p1, p2);
        }
    }
}
