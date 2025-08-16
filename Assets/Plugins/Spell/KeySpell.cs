using GameBase.Tools;
using System.Globalization;

namespace GameBase.Spells
{
    public class KeySpell : Spell
    {
        public KeySpell()
        {
            RegistertoActivesDelegate += (Spell spell) =>
            {
                if (targetable)
                {
                    ReadyJugDelegate = SpellReady;
                    CastJugDelegate = SpellCast;
                }
                else
                {
                    CastJugDelegate = SpellReady;
                }

                CancelJugDelegate = SpellCancel;
            };
        }

        public KeyFunction readyKey;
        public KeyFunction castKey = KeyFunction.MouseConfirm;
        public KeyFunction cancelKey = KeyFunction.Cancel;

        private bool SpellReady(Spell spell)
        {
            return Inputs.GetKeyDown(readyKey);
        }

        private bool SpellCast(Spell spell)
        {
            return Inputs.GetKeyDown(castKey);
        }

        private bool SpellCancel(Spell spell)
        {
            return Inputs.GetKeyDown(cancelKey);
        }
    }
}
