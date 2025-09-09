using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;

namespace Constructor.Spells.Interactive
{
    public struct KeyFastData
    {
    }

    public class KeyFast : ISpellInteractive
    {
        public KeyFunction castKey;

        bool ISpellInteractive.IsTrig => Inputs.GetKeyDown(castKey, "spell");

        void ISpellInteractive.OnTrig(ISpeller speller)
        {
            
        }

        void ISpellInteractive.Update(ISpeller speller)
        {
        }
    }
    public class KeyFastCon : BaseConstructor<KeyFastData, KeyFast, KeyFastCon>
    {
        protected override string RelativePath => "Spell/Interactive/KeyFast.csv";

        protected override KeyFast Get()
        {
            return new KeyFast();
        }

        protected override void Parse(CsvReader line, ref KeyFastData data)
        {
        }

        protected override void Set(KeyFast e, in KeyFastData data)
        {
        }
    }
}
