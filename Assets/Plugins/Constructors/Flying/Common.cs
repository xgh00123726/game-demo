using Constructor.Effects;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Tools;
using NReco.Csv;
using System;

namespace Constructor.Flyings
{
    public struct CommonData
    {
        public int objID;
        public EffectType releaseEffectType;
        public int releaseEffectID;
        public float speed;
        public float minExistTime;
        public CurveFactory.CurveType curveType;
        public EffectType hitEffectType;
        public int hitEffectID;
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
            var releaseEffectID = data.releaseEffectID;
            if (releaseEffectID >= 0)
            {
                e.OnReleased += () =>
                {
                    var er = Constructor.Effects.Common.Instance.Get(releaseEffectID);
                    er.particle.transform.position = e.obj.transform.position;
                };
            }
            var hitEffectID = data.hitEffectID;
            if (hitEffectID >= 0)
            {
                e.OnHit += () =>
                {
                    var er = Effects.Common.Instance.Get(hitEffectID);
                    er.particle.transform.position = e.obj.transform.position;
                };
            }

            return e;
        }
    }
}
