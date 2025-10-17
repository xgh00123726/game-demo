using GameBase.EntitySystem;

namespace Constructor.Spells.Action
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
    public class FlyingDistanceModifierCon : SealedConstructor<FlyingDistanceModifierData, FlyingDistanceModifier, FlyingDistanceModifierCon>
    {
        protected override string RelativePath => "Spell/Action/Modifyable/Modifier/FlyingDistanceModifier.csv";

        protected override FlyingDistanceModifier GetFromData(in FlyingDistanceModifierData data)
        {
            var e = new FlyingDistanceModifier(0);
            e.data = data;
            return e;
        }
    }
}
