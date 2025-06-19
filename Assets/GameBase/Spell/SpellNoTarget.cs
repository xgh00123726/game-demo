using System;

namespace GameBase.Spell
{
    public class SpellNoTarget : GSpell
    {
        public SpellNoTarget(ISpeller speller) : base(speller) { }
        public override bool IsChoosing => true;

        public Action spellAction;
        protected override void OnCast()
        {
            spellAction?.Invoke();
        }
    }
}
