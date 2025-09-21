using System;

namespace GameBase.Spells
{
    public interface ISpellInteractive
    {
        bool IsTrig { get; }
        void OnTrig();
    }
}
