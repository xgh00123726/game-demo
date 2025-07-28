using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.Projectile
{
    public class ProjectileSys : EntitySys<Projectile, GameObject>
    {
        public static readonly float ProjectileHitDis = 0.1f;

        protected override int IDCount => ResourcesLoader.ProjectileBodyPrefabCount;

        protected override void OnObjectGet(Projectile p)
        {
            // 记录射弹生成时刻
            p.instantiateTime = Time.time;

            // 如果射弹存在主人，则射弹生成位置是其主人的手部位置
            p.body.transform.position = p.Src;

            p.body.SetActive(true);

            if (p.curveType != CurveFactory.CurveType.None)
            {
                p.curve = CurveFactory.CreateInstance(p.curveType, p);
            }
        }

        protected override void OnObjectRelease(Projectile p)
        {
            p.body.SetActive(false);
        }

        protected override GameObject InstantiateObject(int id)
        {
            return GameObject.Instantiate(ResourcesLoader.GetProjectileBodyPrefab(id));
        }

        /// <summary>
        /// 当射弹击中时调用
        /// <list type="bullet">
        /// <item><param name="p"><paramref name="p"/>:射弹引用</param></item>
        /// </list></summary>
        void OnProjectileHit(Projectile p)
        {
            if (p.target.Exist)
            {
                p.target.Get().GetDamage(p.damage);
            }
        }

        /// <summary>
        /// 当射弹活跃时调用
        /// <list type="bullet">
        /// <item><param name="p"><paramref name="p"/>:射弹引用</param></item>
        /// </list></summary>
        protected override void UpdateEntity(Projectile p)
        {
            if (p.isImmediately)
            {
                p.body.transform.position = p.Dest;
                OnProjectileHit(p);
                RemoveEntity(p);
                return;
            }

            if (Time.time > p.maxExistTime + p.instantiateTime)
            {
                RemoveEntity(p);
                return;
            }

            p.DisToTarget = (p.Dest - p.body.transform.position).magnitude;
            float hitDis = ProjectileHitDis;

            if (p.target.Exist)
            {
                hitDis += p.target.Get().Radius;
            }

            if (p.DisToTarget < hitDis)
            {
                OnProjectileHit(p);
                RemoveEntity(p);
                return;
            }

            if (p.curve != null)
            {
                p.curve.speed = p.speed;
                p.curve.DirUpdate();
                p.curve.PosUpdate();
            }
        }
    }
}
