using GameBase.Spells;
using NReco.Csv;
using System;

namespace Constructor.Spells.Main
{
    public struct CommonData
    {
        public Interactive.Type interactiveType;
        public int interactiveID;
        public Action.Type actionType;
        public int actionInterfaceID; 
        public int coolingTime;
    }

    public class Common : EntityConstructor<CommonData, Spell, SpellSys, Common>
    {
        protected override string RelativePath => "Spell/Main/Common.csv";

        protected override SpellSys SysInstance => SpellSys.Instance;

        protected override void Parse(CsvReader line, ref CommonData data)
        {
            Enum.TryParse(line[1], out data.interactiveType);
            data.interactiveID = int.Parse(line[2]);

            Enum.TryParse(line[3], out data.actionType);
            data.actionInterfaceID = int.Parse(line[4]);
            data.coolingTime = int.Parse(line[5]);
        }

        protected override void ESet(Spell e, in CommonData data)
        {
            e.interactive = Interactive.Factory.Instance.Get(data.interactiveType, data.interactiveID);
            e.actionInterface = Action.Factory.Instance.Get(data.actionType, data.actionInterfaceID);
            e.coolingTimeSet = data.coolingTime;
        }
    }
}
