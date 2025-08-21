using NReco.Csv;
using GameBase.Flyings;
using GameBase.EntitySystem;

namespace Constructor.Flyings
{
    public struct CommonData
    {
        public int objID;
        public int releaseEffectID;
        public float speed;
        public float minExistTime;
        public CurveFactory.CurveType curveType;
    }
    public class Common : EntityConstructor<CommonData, Flying, SimpleEntityContainer, FlyingSys, Common>
    {
        protected override string RelativePath => "Flyings/Common.csv";

        protected override FlyingSys SysInstance => FlyingSys.Instance;

        protected override void Parse(CsvReader line, ref CommonData data)
        {
            int objID = int.Parse(line[1]);
            int releaseEffectID = int.Parse(line[2]);
            float speed = float.Parse(line[3]);
            float minExistTime = float.Parse(line[4]);
            CurveFactory.CurveType curveType = (CurveFactory.CurveType)int.Parse(line[5]);

            data.objID = objID;
            data.releaseEffectID = releaseEffectID;
            data.speed = speed;
            data.minExistTime = minExistTime;
            data.curveType = curveType;
        }

        protected override void Set(Flying e, in CommonData data)
        {
            e.ObjID = data.objID;

            if (data.curveType != CurveFactory.CurveType.None)
            {
                e.curve = CurveFactory.CreateInstance(data.curveType, e);
                e.curve.speed = data.speed;
            }

            e.speed = data.speed;
            e.minExistTime = data.minExistTime;
            e.releaseEffectID = data.releaseEffectID;
            if (e.releaseEffectID >= 0)
            {
                e.OnReleased = () =>
                {
                    var er = Constructor.Effects.Common.Instance.Get(e.releaseEffectID);
                    er.Position = e.Obj.transform.position;
                };
            }
        }
    }
}
