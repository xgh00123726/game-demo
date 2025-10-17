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
}
