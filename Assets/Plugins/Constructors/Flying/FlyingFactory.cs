using GameBase.Flyings;
using GameBase.EntitySystem;

namespace Constructor.Flyings
{
    public class FlyingFactory : YamlFactory<FlyingData, Flying, FlyingFactory>
    {
        protected override string YamlFolder => null;

        protected override Flying GetEntity(FlyingData data)
        {
            var e = FlyingSys.Instance.NewEntity(data.prefabName);

            if (data.curveType != CurveFactory.CurveType.None)
            {
                e.curveType = data.curveType;
                e.curve = CurveFactory.CreateInstance(data.curveType, e);
                e.curve.speed = data.speed;
            }

            e.speed = data.speed;
            e.minExistTime = data.minExistTime;

            e.releaseEffect = data.releaseEffectName;
            e.hitEffect = data.hitEffectName;

            return e;
        }
    }
}
