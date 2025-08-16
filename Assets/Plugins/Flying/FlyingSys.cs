using GameBase.Resources;
using GameBase.Tools;
using System.Collections.Generic;
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

            var effectObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.releaseEffectID));
            e.releaseEffect = effectObj.GetComponent<ParticleSystem>();

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

            // 射弹生成位置 = 主位置 + 位置偏移
            // 主位置 = 如果有主人：主人手部，否则：0
            // 位置偏移 = 自行赋值更改
            e.Obj.transform.position = e.src + e.srcOffset;

            e.Obj.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(Flying e)
        {
            e.Obj.SetActive(false);

            e.Obj.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 当射弹活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:射弹引用</param></item>
        /// </list></summary>
        protected override void UpdateEntity(Flying e)
        {
            if (e.curve == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("flying has no curve");
                RemoveEntity(e);
            }

            // 射弹超时
            if (Time.time > e.maxExistTime + e.instantiateTime)
            {
                e.releaseEffect.transform.position = e.Obj.transform.position;
                e.releaseEffect.Play();
                RemoveEntity(e);
            }

            // 具有轨迹的射弹逻辑
            if (e.curve != null)
            {
                e.curve.speed = e.speed;
                e.curve.DirUpdate();
                e.curve.PosUpdate();
            }

            if ((e.dest - e.Obj.transform.position).magnitude <= e.releaseDistance)
            {
                e.releaseEffect.transform.position = e.Obj.transform.position;
                e.releaseEffect.Play();
                RemoveEntity(e);
            }
        }
    }
}
