using GameBase.Tools;
using GameBase.Spell;

namespace GameBase.Instance
{
    public class PanelSpell : Spell.Spell
    {
        public PanelSpell()
        {
            if (targetable)
            {
                ReadyDelegate = SpellReady;
                CastDelegate = SpellCast;
            }
            else
            {
                CastDelegate = SpellReady;
            }

            CancelDelegate = SpellCancel;
        }

        public KeyFunction hotKey;

        private bool SpellReady(Spell.Spell spell)
        {
            return Inputs.GetKeyDown(hotKey);
        }

        private bool SpellCast(Spell.Spell spell)
        {
            return Inputs.GetKeyDown(KeyFunction.MouseConfirm);
        }

        private bool SpellCancel(Spell.Spell spell)
        {
            return Inputs.GetKeyDown(KeyFunction.Cancel);
        }
    }
}
