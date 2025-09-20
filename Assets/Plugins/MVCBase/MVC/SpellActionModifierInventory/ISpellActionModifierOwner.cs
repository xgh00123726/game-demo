using GameBase.Spells;

namespace Instance.UI.MVC
{
    public interface ISpellActionModifierOwner
    {
        Spell Spell { get; }
    }

    public class SpellActionModifierOwner : ISpellActionModifierOwner
    {
        private ISpellModelOwner _owner;
        private int _index;

        public SpellActionModifierOwner(ISpellModelOwner owner, int index)
        {
            _owner = owner;
            _index = index;
        }

        Spell ISpellActionModifierOwner.Spell => _owner.GetSpell(_index);
    }
}
