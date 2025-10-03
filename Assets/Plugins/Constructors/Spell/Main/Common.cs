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

    public class Common : EntityConstructor<CommonData, Spell, SpellSys, Common>
    {
        protected override string RelativePath => "Spell/Main/Common.csv";

        protected override SpellSys SysInstance => SpellSys.Instance;

        protected override void ESet(Spell e, in CommonData data)
        {
            e.interactive = Interactive.Factory.Instance.Get(data.interactiveType, data.interactiveID);
            if (e.interactive is Invokable invokable)
            {
                invokable.indicatorType = data.indicatorType;
            }

            e.action = Action.Factory.Instance.Get(data.actionType, data.actionInterfaceID);
            e.spellCoolingdown.CoolingSet = data.coolingTime;
            e.iconTextureID = data.iconTextureID;
        }
    }
}
