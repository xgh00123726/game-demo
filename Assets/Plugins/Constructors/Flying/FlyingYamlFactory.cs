using GameBase.Flyings;
using GameBase.EntitySystem;

namespace Constructor.Flyings
{
    public class FlyingYamlFactory : YamlFactory<FlyingData, Flying, FlyingYamlFactory>
    {
        protected override string Folder => null;

        protected override Flying GetEntity(FlyingData data)
        {
            var e = FlyingSys.Instance.NewEntity(data.PrefabName);

            e.Curve = CurveFactory.CreateInstance(data.Curve);
            e.Speed = data.Speed;
            e.MinExistTime = data.MinExistTime;

            e.ReleaseEffect = data.ReleaseEffectName;

            return e;
        }
    }
}
