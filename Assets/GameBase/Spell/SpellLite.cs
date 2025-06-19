using System;

namespace GameBase.Spell
{
    public class SpellLite : GSpell
    {
        public SpellLite(ISpeller speller) : base(speller) { }

        public Action spellAction;
        protected override void OnCast()
        {
            spellAction?.Invoke();
        }
    }
}
