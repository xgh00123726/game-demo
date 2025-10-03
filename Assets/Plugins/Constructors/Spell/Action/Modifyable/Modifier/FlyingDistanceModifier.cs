using GameBase.EntitySystem;

namespace Constructor.Spells.Action.Modifyables.Modifier
{
    public struct FlyingDistanceModifierData
    {
        public float distanceModify;
    }
    public class FlyingDistanceModifier : BaseModifier
    {
        public FlyingDistanceModifierData data;
        public FlyingDistanceModifier(float distanceModify)
        {
            data.distanceModify = distanceModify;
        }

        internal override void Modify(ref ModifyableModifyData modifyData)
        {
            modifyData.flyingDistance += data.distanceModify;
        }
    }
    public class FlyingDistanceModifierCon : BaseConstructor<FlyingDistanceModifierData, FlyingDistanceModifier, FlyingDistanceModifierCon>
    {
        protected override string RelativePath => "Spell/Action/Modifyable/Modifier/FlyingDistanceModifier.csv";

        protected override FlyingDistanceModifier Get()
        {
            return new FlyingDistanceModifier(0);
        }

        protected override void Set(FlyingDistanceModifier e, in FlyingDistanceModifierData data)
        {
            e.data = data;
        }
    }
}
