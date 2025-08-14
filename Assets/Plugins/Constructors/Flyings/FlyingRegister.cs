using UnityEngine;
using GameBase.GCamera;
using GameBase.Flyings;

namespace Constructor.Flyings
{
    public class FlyingRegister
    {
        private static Flying FlyingGen_0()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();
            proj.ObjID = 2;
            proj.curveType = CurveFactory.CurveType.Tracer;
            proj.speed = 30f;
            proj.maxExistTime = 10f;

            return proj;
        }

        private static Flying FlyingGen_1()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();
            proj.ObjID = 3;
            proj.dest = CameraSys.MouseHitPosition;
            var curve = CurveFactory.CreateInstance(CurveFactory.CurveType.Slower, proj) as Slower;
            curve.factor = 0f;
            curve.slowDis = 1.5f;
            proj.curve = curve;
            proj.speed = 3f;
            proj.maxExistTime = 10f;

            return proj;
        }

        private static Flying FlyingGen_2()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();
            proj.ObjID = 2;
            proj.curveType = CurveFactory.CurveType.Tracer;
            proj.speed = 30f;
            proj.maxExistTime = 10f;

            return proj;
        }

        private static Flying FlyingGen_3()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();
            proj.ObjID = 3;
            proj.dest = CameraSys.MouseHitPosition;
            proj.curveType = CurveFactory.CurveType.Liner;
            proj.speed = 3f;
            proj.maxExistTime = 10f;

            return proj;
        }

        private static Flying FlyingGen_4()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();

            proj.ObjID = 20;
            proj.srcOffset = new Vector3(0, 10, 0);
            proj.dest = CameraSys.MouseHitPosition;
            proj.curveType = CurveFactory.CurveType.Fall;
            proj.speed = 10f;
            proj.maxExistTime = 10f;

            return proj;
        }

        public static void RegisterGenerator()
        {
            FlyingSys.Instance.RegisterEntityGenerateDeletate(FlyingGen_0);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(FlyingGen_1);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(FlyingGen_2);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(FlyingGen_3);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(FlyingGen_4);
        }
    }
}


