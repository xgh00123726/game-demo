using NReco.Csv;
using GameBase.Tools;

namespace Constructor.Spells.Action.Modifyables.Modifier
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
    public class MultipleModifierCon : BaseConstructor<MultipleModifierData, MultipleModifier, MultipleModifierCon>
    {
        protected override string RelativePath => throw new System.NotImplementedException();

        protected override MultipleModifier Get()
        {
            throw new System.NotImplementedException();
        }

        protected override void Set(MultipleModifier e, in MultipleModifierData data)
        {
            throw new System.NotImplementedException();
        }
    }
}


