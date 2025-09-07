using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;

namespace Constructor.Spells.Interactive
{
    public struct KeyFastData
    {
    }

    public class KeyFastInteractive : ISpellInteractive
    {
        public KeyFunction castKey;

        bool ISpellInteractive.IsTrig => Inputs.GetKeyDown(castKey, "spell");

        void ISpellInteractive.Update(ISpeller speller)
        {
        }
    }
    public class KeyFast : BaseConstructor<KeyFastData, KeyFastInteractive, KeyFast>
    {
        protected override string RelativePath => "Spell/Interactive/KeyFast.csv";

        protected override KeyFastInteractive Get()
        {
            return new KeyFastInteractive();
        }

        protected override void Parse(CsvReader line, ref KeyFastData data)
        {
        }

        protected override void Set(KeyFastInteractive e, in KeyFastData data)
        {
        }
    }
}
