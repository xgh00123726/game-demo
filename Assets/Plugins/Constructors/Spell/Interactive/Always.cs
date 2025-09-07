using GameBase.Spells;
using GameBase.Tools;
using NReco.Csv;

namespace Constructor.Spells.Interactive
{
    public struct AlwaysData
    {
    }

    public class AlwaysInteractive : ISpellInteractive
    {
        public KeyFunction castKey;

        bool ISpellInteractive.IsTrig => true;

        void ISpellInteractive.Update(ISpeller speller)
        {
        }
    }
    public class Always : BaseConstructor<AlwaysData, AlwaysInteractive, Always>
    {
        protected override string RelativePath => "Spell/Interactive/Always.csv";

        protected override AlwaysInteractive Get()
        {
            return new AlwaysInteractive();
        }

        protected override void Parse(CsvReader line, ref AlwaysData data)
        {
        }

        protected override void Set(AlwaysInteractive e, in AlwaysData data)
        {
        }
    }
}
