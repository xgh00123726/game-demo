using Constructor.Spells.Action.Modifyables;
using GameBase.Inventorys;
using System;
using static UnityEditor.Progress;

namespace Instance.UI.MVC
{
    public class SpellActionModifierInventoryModel : CInventoryModel<InventoryData>, IInventoryModel<InventoryData>
    {
        internal ISpellActionModifierOwner owner;
        public override int AddItem(InventoryData item, int index)
        {
            var ret = base.AddItem(item, index);

            if (item.tag == InventoryTag.SpellActionModify)
            {
                var modify = Constructor.Spells.Action.Modifyables.Modifier.Factory.Instance.Get(item.spellActionModifyerID);
                modify.ModifyTo(owner.Spell, index);
            }

            return ret;
        }

        public override bool RemoveItem(int position)
        {
            if (base.RemoveItem(position))
            {
                var item = this[position];
                if (item.tag == InventoryTag.SpellActionModify)
                {
                    ModifyableAction.TryRemoveModifyer(owner.Spell, position);
                }

                return true;
            }


            return false;
        }
    }
}
