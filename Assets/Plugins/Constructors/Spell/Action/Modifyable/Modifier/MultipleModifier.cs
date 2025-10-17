using GameBase.EntitySystem;

namespace Constructor.Spells.Action
{
    public struct MultipleModifierData
    {
        public int castTimeModify;
    }
    public class MultipleModifier : BaseModifier
    {
        public MultipleModifierData data;
        public MultipleModifier(int castTimesModify)
        {
            data.castTimeModify = castTimesModify;
        }

        internal override void Modify(ref ModifyableModifyData modifyData)
        {
            modifyData.castTimes += data.castTimeModify;
        }
    }
}


