using GameBase.Flyings;
using GameBase.EntitySystem;

namespace Constructor.Flyings
{
    public class FlyingYamlFactory : YamlFactory<FlyingData, Flying, FlyingYamlFactory>
    {
        protected override string YamlFolder => null;

        protected override Flying GetEntity(FlyingData data)
        {
            var e = FlyingSys.Instance.NewEntity(data.prefabName);

            e.curve = CurveFactory.CreateInstance(data.curve);
            e.Speed = data.speed;
            e.minExistTime = data.minExistTime;

            e.releaseEffect = data.releaseEffectName;

            return e;
        }
    }
}
