using GameBase.Spells;
using System;

namespace Constructor.Spells.Action
{
    public class Editable : ISpellAction
    {
        public Func<Spell, bool> Action;

        bool ISpellAction.CastAction(Spell spell)
        {
            return Action?.Invoke(spell) == true;
        }
    }
}
