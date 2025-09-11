using GameBase.Spells;
using GameBase.Tools;

namespace Instance.MVC
{
    public interface ISpellModelOwner
    {
        Spell GetSpell(int index);
        void SetSpell(int index, Spell spell);
        void RemoveSpell(int index);
        ISpeller Speller { get; }
        KeyFunction GetKeyFunction(int index);
    }
}
