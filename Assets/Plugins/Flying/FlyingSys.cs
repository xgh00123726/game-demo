using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.Flyings
{
    public class FlyingSys : KeyEntitySys<string, Flying, FlyingSys>
    {
        public static readonly float ProjectileHitDis = 0.1f;

        protected override Flying CtorT(string k)
        {
            Flying e = new Flying();
            var obj = GameObject.Instantiate(ResourceMgr.Prefab.Get(k));
            e.Obj = obj;
            return e;
        }

        protected override void OnGet(Flying e)
        {
            e.ArriveDis = 0.1f;
            e.MaxExistTime = 10f;
            e.MinExistTime = 0f;
            e.speed = 5f;
            e.alive = true;

            e.instantiateTime = Time.time;

            e.Obj.SetActive(true);
        }

        protected override void OnRelease(Flying e)
        {
            e.Obj.SetActive(false);
            AudioMgr.PlayAt(e.ReleaseAudio, e.Obj.transform.position);
            EffectSys.Instance.PlayAtPS(e.ReleaseEffect, e.Obj.transform.position, e.Obj.transform.localScale);
            e.OnReleased?.Invoke();
            e.Obj.transform.localScale = Vector3.one;
            e.speed = 0;
            e.Curve = null;
            e.alive = false;
            e.OnReleased = null;
            e.Target = null;
        }

        protected override void EntityStart(Flying e)
        {
            if (e.Curve != null)
            {
                e.Curve.SetOwner(e);
            }

            if (e.Target != null)
            {
                e.Dir = e.Target.Position - e.src;
            }
            Quaternion rotate = Quaternion.Euler(0, e.StartAngleOffset, 0);
            e.Dir = rotate * e.Dir;
        }

        /// <summary>
        /// 当射弹活跃时调用
        /// <list Type="bullet">
        /// <item><param name="e"><paramref name="e"/>:射弹引用</param></item>
        /// </list></summary>
        protected override void UpdateEntity(Flying e)
        {
            // 射弹超时
            if (Time.time > e.MaxExistTime + e.instantiateTime)
            {
                RemoveEntity(e);
                return;
            }

            // 如果飞行物有目标则飞向目标，否则飞向固定的位置
            if (e.Target != null)
            {
                e.Dest = e.Target.Position;
            }

            // 具有轨迹的射弹逻辑
            if (e.Curve != null)
            {
                e.Curve.Speed = e.speed;
                e.Curve.Update();
            }
        }
    }
}
