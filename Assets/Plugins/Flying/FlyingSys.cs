using GameBase.EntitySystem;
using GameBase.Resources;
using UnityEngine;
namespace GameBase.Flyings
{
    public class FlyingSys : KeyEntitySys<string, Flying, FlyingSys>
    {
        public static readonly float ProjectileHitDis = 0.1f;

        private void OnHit(Flying e)
        {
            AudioMgr.PlayAt(e.hitAudio, e.obj.transform.position);
            EffectSys.Instance.PlayAtPS(e.hitEffect, e.obj.transform.position, e.obj.transform.localScale);
        }

        protected override Flying CtorT(string k)
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
            AudioMgr.PlayAt(e.releaseAudio, e.obj.transform.position);
            EffectSys.Instance.PlayAtPS(e.releaseEffect, e.obj.transform.position, e.obj.transform.localScale);
            e.OnReleased?.Invoke();
            e.obj.transform.localScale = Vector3.one;

            e.speed = 0;
            e.curveType = CurveFactory.CurveType.None;
            e.curve = null;
            e.alive = false;
            e.OnReleased = null;
            e.OnHit = null;
        }

        protected override void EntityStart(Flying e)
        {
            e.Dir = e.target.Position - e.src;
            Quaternion rotate = Quaternion.Euler(0, e.startAngleOffset, 0);
            e.Dir = rotate * e.Dir;
            e.curve.Start();
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
                OnHit(e);
                e.OnHit?.Invoke();
            }
        }
    }
}
