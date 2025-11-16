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
            e.obj = obj;
            return e;
        }

        protected override void OnGet(Flying e)
        {
            e.arriveDis = 0.1f;
            e.maxExistTime = 10f;
            e.minExistTime = 0f;
            e.speed = 5f;
            e.alive = true;

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
            e.curve = null;
            e.alive = false;
            e.OnReleased = null;
            e.target = null;
        }

        protected override void EntityStart(Flying e)
        {
            if (e.curve != null)
            {
                e.curve.SetOwner(e);
            }

            if (e.target != null)
            {
                e.Dir = e.target.Position - e.src;
            }
            Quaternion rotate = Quaternion.Euler(0, e.startAngleOffset, 0);
            e.Dir = rotate * e.Dir;
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

            // 如果飞行物有目标则飞向目标，否则飞向固定的位置
            if (e.target != null)
            {
                e.dest = e.target.Position;
            }

            // 具有轨迹的射弹逻辑
            if (e.curve != null)
            {
                e.curve.speed = e.speed;
                e.curve.Update();
            }
        }
    }
}
