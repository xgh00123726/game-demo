using GameBase.Resources;
using GameBase.EntitySystem;
using UnityEngine;
namespace GameBase.Flyings
{
    public class FlyingSys : UObjEntitySys<Flying, SimpleEntityContainer, GameObject, FlyingSys>
    {
        public static readonly float ProjectileHitDis = 0.1f;

        protected override float FixedFreq => 60;

        protected override GameObject InstantiateObj(Flying e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            return obj;
        }

        protected override void AfterInstantiateEUObject(Flying e)
        {
            if (e.curveType != CurveFactory.CurveType.None)
            {
                e.curve = CurveFactory.CreateInstance(e.curveType, e);
                e.curve.speed = e.speed;
            }

            e.instantiateTime = Time.time;

            e.Obj.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(Flying e)
        {
            e.Obj.SetActive(false);
            e.OnReleased?.Invoke();
            e.Obj.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 当射弹活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:射弹引用</param></item>
        /// </list></summary>
        protected override void UpdateEntity(Flying e)
        {
            // 射弹超时
            if (Time.time > e.maxExistTime + e.instantiateTime)
            {
                RemoveEntity(e);
            }

            // 具有轨迹的射弹逻辑
            if (e.curve != null)
            {
                e.curve.speed = e.speed;
                e.curve.DirUpdate();
                e.curve.PosUpdate();
            }

            if ((e.dest - e.Obj.transform.position).magnitude <= e.releaseDistance
                && Time.time > e.minExistTime + e.instantiateTime)
            {
                RemoveEntity(e);
            }
        }
    }
}
