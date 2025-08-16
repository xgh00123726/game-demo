using UnityEngine;
using GameBase.GCamera;
using GameBase.Flyings;
using GameBase.Tools;

namespace Constructor.Flyings
{
    public class FlyingRegister
    {
        private static Flying FlyingGen_0()
        {
            var e = FlyingSys.Instance.NewEntity<Flying>();
            e.ObjID = 22; // Prefabs/Projectile/Sword12_Green
            e.maxExistTime = 10f;
            e.speed = 30f;

            return e;
        }

        private static Flying FlyingGen_1()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();
            proj.ObjID = 3;
            var curve = CurveFactory.CreateInstance(CurveFactory.CurveType.Slower, proj) as Slower;
            curve.factor = 0f;
            curve.slowDis = 1.5f;
            proj.curve = curve;
            proj.maxExistTime = 10f;

            return proj;
        }

        private static Flying FlyingGen_2()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();
            proj.ObjID = 2;
            proj.maxExistTime = 10f;

            return proj;
        }

        private static Flying FlyingGen_3()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();
            proj.ObjID = 3;
            proj.maxExistTime = 10f;

            return proj;
        }

        private static Flying FlyingGen_4()
        {
            var proj = FlyingSys.Instance.NewEntity<Flying>();

            proj.ObjID = 20;
            proj.maxExistTime = 10f;
            XLogger.Instance.Log("flying gen 4");

            return proj;
        }

        public static void RegisterGenerator()
        {
            FlyingSys.Instance.RegisterEntityGenerateDeletate(0, FlyingGen_0);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(1, FlyingGen_1);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(2, FlyingGen_2);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(3, FlyingGen_3);
            FlyingSys.Instance.RegisterEntityGenerateDeletate(4, FlyingGen_4);
        }
    }
}


