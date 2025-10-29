using GameBase.EntitySystem;
using GameBase.Flyings;

namespace Constructor.Flyings
{
    public struct CommonData
    {
        public int objID;
        public float speed;
        public float minExistTime;
        public CurveFactory.CurveType curveType;
        public string releaseEffectName;
        public string hitEffectName;
    }
    public class Common : KeyConstructor<CommonData, Flying, Common>
    {
        protected override string RelativePath => "Flyings/Common.csv";

        protected override Flying GetFromData(in CommonData data)
        {
            var e = FlyingSys.Instance.NewEntity(data.objID);
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
