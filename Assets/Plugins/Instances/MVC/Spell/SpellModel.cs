using Constructor.Spells.Interactive;
using GameBase.Inventorys;
using GameBase.UI.MVC;
using GameBase.Creatures;
using Factory = Constructor.Spells.Main.Factory;
using GameBase.Tools;

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

        SpellItemData IMVCModel<SpellItemData>.this[int index] => default;

        int IMVCModel<SpellItemData>.Size
        {
            get => owner.spells.Size;
            set => owner.spells.Size = value;
        }

        int IMVCModel<SpellItemData>.AddItem(SpellItemData item)
        {
            var spell = Factory.Instance.Get(item.type, item.id);
            spell.iconTextureID = item.iconTextureID;
            spell.speller = owner;

            var ret = owner.spells.AddItem(spell);
            return ret;
        }

        int IMVCModel<SpellItemData>.AddItem(SpellItemData item, int index)
        {
            var spell = Factory.Instance.Get(item.type, item.id);
            spell.iconTextureID = item.iconTextureID;
            spell.speller = owner;

            var ret = owner.spells.AddItem(spell, index);

            return ret;
        }

        bool IMVCModel<SpellItemData>.HasItem(int index)
        {
            return owner.spells.HasItem(index);
        }

        bool IMVCModel<SpellItemData>.RemoveItem(int index)
        {
            owner.spells.RemoveItem(index);

            return false;
        }

        void IMVCModel<SpellItemData>.Swap(int p1, int p2)
        {
            owner.spells.Swap(p1, p2);
        }
    }
}
