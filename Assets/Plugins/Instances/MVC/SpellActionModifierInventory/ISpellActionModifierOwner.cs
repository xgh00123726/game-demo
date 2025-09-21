using GameBase.Creatures;
using GameBase.Spells;

namespace Instance.UI.MVC
{
    public interface ISpellActionModifierOwner
    {
        Spell Spell { get; }
    }

    public class SpellActionModifierOwner : ISpellActionModifierOwner
    {
        private Creature _owner;
        private int _index;

        public SpellActionModifierOwner(Creature owner, int index)
        {
            _owner = owner;
            _index = index;
        }

        Spell ISpellActionModifierOwner.Spell => _owner.spells[_index];
    }
}
