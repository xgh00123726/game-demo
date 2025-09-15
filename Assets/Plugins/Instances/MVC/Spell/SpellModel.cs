using Constructor.Spells.Interactive;
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
        public int iconTextureID;
    }
    public class SpellModel : IInventoryModel<SpellItemData>
    {
        internal ISpellModelOwner owner;
        private SInventoryModel<SpellItemData> _inventoryModel = new()
        {
            Size = MAX_SPELL_NUM
        };

        public const int MAX_SPELL_NUM = 5;
        
        public SpellModel(ISpellModelOwner owner)
        {
            this.owner = owner;
        }

        SpellItemData IInventoryModel<SpellItemData>.this[int index] => _inventoryModel[index];

        int IInventoryModel<SpellItemData>.Size
        {
            get => _inventoryModel.Size;
            set => _inventoryModel.Size = value;
        }

        int IInventoryModel<SpellItemData>.AddItem(SpellItemData item)
        {
            var ret = _inventoryModel.AddItem(item);
            var spell = Factory.Instance.Get(item.type, item.id);

            Constructor.Spells.Interactive.Factory.Instance
                .SetHotKey(spell.interactive, owner.GetKeyFunction(ret));

            spell.speller = owner.Speller;
            owner.SetSpell(ret, spell);
            
            return ret;
        }

        int IInventoryModel<SpellItemData>.AddItem(SpellItemData item, int index)
        {
            _inventoryModel.AddItem(item, index);
            _inventoryModel.SortItems();

            var ret = _inventoryModel.Count - 1;
            var spell = Factory.Instance.Get(item.type, item.id);

            Constructor.Spells.Interactive.Factory.Instance
                .SetHotKey(spell.interactive, owner.GetKeyFunction(ret));

            spell.speller = owner.Speller;
            owner.SetSpell(ret, spell);

            return ret;
        }

        bool IInventoryModel<SpellItemData>.HasItem(int index)
        {
            return _inventoryModel.HasItem(index);
        }

        void IInventoryModel<SpellItemData>.RemoveItem(int index)
        {
            _inventoryModel.RemoveItem(index);
            owner.RemoveSpell(index);
        }

        void IInventoryModel<SpellItemData>.Swap(int p1, int p2)
        {
            _inventoryModel.Swap(p1, p2);
            var s1 = owner.GetSpell(p1);
            var s2 = owner.GetSpell(p2);
            
            owner.SetSpell(p2, s1);
            if (s1.interactive is KeyCommon keyCommon1)
            {
                keyCommon1.readyKey = owner.GetKeyFunction(p2);
            }

            owner.SetSpell(p1, s2);
            if (s2.interactive is KeyCommon keyCommon2)
            {
                keyCommon2.readyKey = owner.GetKeyFunction(p1);
            }
        }
    }
}
