using GameBase.EntitySystem;

namespace Constructor.Spells.Action
{
    public struct FlyingNumModifierData
    {
        public int flyingNumsModify;
    }
    public class FlyingNumModifier : BaseModifier
    {
        public FlyingNumModifierData data;
        public FlyingNumModifier(int flyingNumsModify)
        {
            data.flyingNumsModify = flyingNumsModify;
        }

        internal override void Modify(ref ModifyableModifyData modifyData)
        {
            modifyData.flyingNums = data.flyingNumsModify;
        }
    }

    public class FlyingNumModifierCon : BaseConstructor<FlyingNumModifierData, FlyingNumModifier, FlyingNumModifierCon>
    {
        protected override string RelativePath => "Spell/Action/Modifyable/Modifier/FlyingNumModifier.csv";

        protected override FlyingNumModifier Get()
        {
            return new FlyingNumModifier(0);
        }

        protected override void Set(FlyingNumModifier e, in FlyingNumModifierData data)
        {
            e.data = data;
        }
    }
}
