using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;

namespace Constructor.Spells.Interactive
{
    public struct AlwaysData
    {
    }

    public class Always : ISpellInteractive
    {
        public KeyFunction castKey;

        bool ISpellInteractive.IsTrig => true;

        void ISpellInteractive.OnTrig(ISpeller speller)
        {
            
        }

        void ISpellInteractive.Update(ISpeller speller)
        {
        }
    }
    public class AlwaysCon : BaseConstructor<AlwaysData, Always, AlwaysCon>
    {
        protected override string RelativePath => "Spell/Interactive/Always.csv";

        protected override Always Get()
        {
            return new Always();
        }

        protected override void Parse(CsvReader line, ref AlwaysData data)
        {
        }

        protected override void Set(Always e, in AlwaysData data)
        {
        }
    }
}
