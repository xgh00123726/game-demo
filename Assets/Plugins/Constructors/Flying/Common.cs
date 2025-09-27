using NReco.Csv;
using GameBase.Flyings;
using GameBase.EntitySystem;
using System;
using GameBase.Tools;

namespace Constructor.Flyings
{
    public struct CommonData
    {
        public int objID;
        public Effects.Type releaseEffectType;
        public int releaseEffectID;
        public float speed;
        public float minExistTime;
        public CurveFactory.CurveType curveType;
        public Effects.Type hitEffectType;
        public int hitEffectID;
    }
    public class Common : EntityConstructor<CommonData, Flying, FlyingSys, Common>
    {
        protected override string RelativePath => "Flyings/Common.csv";

        protected override FlyingSys SysInstance => FlyingSys.Instance;

        protected override void ESet(Flying e, in CommonData data)
        {
            e.ObjID = data.objID;

            if (data.curveType != CurveFactory.CurveType.None)
            {
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
                    er.Position = e.Obj.transform.position;
                };
            }
            var hitEffectID = data.hitEffectID;
            if (hitEffectID >= 0)
            {
                e.OnHit += () =>
                {
                    var er = Effects.Common.Instance.Get(hitEffectID);
                    er.Position = e.Obj.transform.position;
                };
            }
        }
    }
}
