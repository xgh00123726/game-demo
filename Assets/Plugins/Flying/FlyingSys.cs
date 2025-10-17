using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;
using static GameBase.Flyings.CurveFactory;
using static UnityEngine.UI.CanvasScaler;
namespace GameBase.Flyings
{
    public class FlyingSys : KeyEntitySys<int, Flying, FlyingSys>
    {
        public static readonly float ProjectileHitDis = 0.1f;

        protected override Flying CtorT(int k)
        {
            Flying e = new Flying();
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(k));
            e.obj = obj;
            return e;
        }

        protected override void OnGet(Flying e)
        {
            if (e.curveType != CurveFactory.CurveType.None)
            {
                e.curve = CurveFactory.CreateInstance(e.curveType, e);
                e.curve.speed = e.speed;
            }

            e.arriveDis = 0.1f;
            e.maxExistTime = 10f;
            e.minExistTime = 0f;
            e.speed = 5f;
            e.alive = true;
            e.hitFlag = false;

            e.instantiateTime = Time.time;

            e.obj.SetActive(true);
        }

        protected override void OnRelease(Flying e)
        {
            e.obj.SetActive(false);
            e.OnReleased?.Invoke();
            e.obj.transform.localScale = Vector3.one;

            e.speed = 0;
            e.curveType = CurveFactory.CurveType.None;
            e.curve = null;
            e.alive = false;
            e.OnReleased = null;
            e.OnHit = null;
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
                return;
            }

            // 具有轨迹的射弹逻辑
            if (e.curve != null)
            {
                e.curve.speed = e.speed;
                e.curve.DirUpdate();
                e.curve.PosUpdate();
            }

            if (e.hitFlag && Time.time > e.minExistTime + e.instantiateTime)
            {
                RemoveEntity(e);
                return;
            }

            if (!e.hitFlag && (e.target.Position - e.obj.transform.position).magnitude <= e.arriveDis)
            {
                e.hitFlag = true;
                e.OnHit?.Invoke();
            }
        }
    }
}
