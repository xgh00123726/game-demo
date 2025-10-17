using Constructor.Spells.Interactive;
using GameBase.Indicators;
using GameBase.Spells;
using GameBase.EntitySystem;

namespace Constructor.Spells.Main
{
    public struct CommonData
    {
        public Interactive.Type interactiveType;
        public int interactiveID;
        public Action.Type actionType;
        public int actionInterfaceID; 
        public int coolingTime;
        public IndicatorType indicatorType;
        public int iconTextureID;
    }

    public class Common : SealedConstructor<CommonData, Spell, Common>
    {
        protected override string RelativePath => "Spell/Main/Common.csv";

        protected override Spell GetFromData(in CommonData data)
        {
            var e = SpellSys.Instance.NewEntity();
            e.interactive = Interactive.InteractiveFactory.Instance.Get(data.interactiveType, data.interactiveID);
            if (e.interactive is DotExternalSet interactive)
            {
                interactive.indicatorType = data.indicatorType;
            }

            e.action = Action.SpellActionFactory.Instance.Get(data.actionType, data.actionInterfaceID);
            e.spellCoolingdown.CoolingSet = data.coolingTime;
            e.iconTextureID = data.iconTextureID;
            return e;
        }
    }
}
