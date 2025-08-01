using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.Projectile
{
    public class ProjectileSys : UObjEntitySys<Projectile, CSObjectPool<Projectile>, GameObject, UObjectPool<GameObject>>
    {
        public static readonly float ProjectileHitDis = 0.1f;

        protected override int ContainerCapacity => ResourcesLoader.ProjectileBodyPrefabCount;

        protected override GameObject InstantiateObj(IEntity<GameObject> e)
        {
            return GameObject.Instantiate(ResourcesLoader.GetProjectileBodyPrefab(e.ID));
        }

        protected override void OnInstantiateUObject(Projectile e)
        {
            // 记录射弹生成时刻
            e.instantiateTime = Time.time;

            // 如果射弹存在主人，则射弹生成位置是其主人的手部位置
            e.body.transform.position = e.Src;

            e.body.SetActive(true);

            if (e.curveType != CurveFactory.CurveType.None)
            {
                e.curve = CurveFactory.CreateInstance(e.curveType, e);
            }
        }

        protected override void OnReleaseUObject(Projectile e)
        {
            e.body.SetActive(false);
        }

        /// <summary>
        /// 当射弹击中时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:射弹引用</param></item>
        /// </list></summary>
        void OnProjectileHit(Projectile e)
        {
            if (e.target.Exist)
            {
                e.target.Get().GetDamage(e.damage);
            }
        }

        /// <summary>
        /// 当射弹活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:射弹引用</param></item>
        /// </list></summary>
        protected override void UpdateEntity(Projectile e)
        {
            if (e.isImmediately)
            {
                e.body.transform.position = e.Dest;
                OnProjectileHit(e);
                RemoveEntity(e);
                return;
            }

            if (Time.time > e.maxExistTime + e.instantiateTime)
            {
                RemoveEntity(e);
                return;
            }

            e.DisToTarget = (e.Dest - e.body.transform.position).magnitude;
            float hitDis = ProjectileHitDis;

            if (e.target.Exist)
            {
                hitDis += e.target.Get().Radius;
            }

            if (e.DisToTarget < hitDis)
            {
                OnProjectileHit(e);
                RemoveEntity(e);
                return;
            }

            if (e.curve != null)
            {
                e.curve.speed = e.speed;
                e.curve.DirUpdate();
                e.curve.PosUpdate();
            }
        }
    }
}
