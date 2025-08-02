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
        public static IProjectileTargetSys ProjectileTargetSys { set; get; }

        protected override int ContainerCapacity => ResourcesLoader.PrefabCount;

        protected override GameObject InstantiateObj(Projectile e)
        {
            return GameObject.Instantiate(ResourcesLoader.GetPrefab(e.bodyID));
        }

        protected override void OnInstantiateUObject(Projectile e)
        {
            // 如果射弹存在主人，则射弹生成位置是其主人的手部位置
            e.body.transform.position = e.Src;

            e.body.SetActive(true);
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
            if (!e.target.Exist)
            {
                return;
            }

            var target = e.target.Get();
            if (e.whites.Exist && !e.whites.Get().Contains(target.ID))
            {
                e.whites.Get().Add(target.ID);
                e.target.Get().GetDamage(e.damage);
            }
            else if (!e.whites.Exist)
            {
                e.target.Get().GetDamage(e.damage);
            }
        }

        void OnProjectileHitTargets(Projectile e, LinkedList<IProjectileTarget> targets)
        {
            if (targets == null) return;

            foreach (var target in targets)
            {
                if (e.whites.Exist && !e.whites.Get().Contains(target.ID))
                {
                    e.whites.Get().Add(target.ID);
                    target.GetDamage(e.damage);
                }
            }
        }

        /// <summary>
        /// 当射弹活跃时调用
        /// <list type="bullet">
        /// <item><param name="e"><paramref name="e"/>:射弹引用</param></item>
        /// </list></summary>
        protected override void UpdateEntity(Projectile e)
        {
            // tick逻辑
            if (e.tick++ < e.tickRate)
            {
                return;
            }
            else
            {
                e.tick = 0;
            }

            // 瞬发型弹幕的逻辑
            if (e.isImmediately)
            {
                e.body.transform.position = e.Dest;
                OnProjectileHit(e);
                RemoveEntity(e);
                return;
            }

            // 射弹超时
            if (Time.time > e.maxExistTime + e.instantiateTime)
            {
                RemoveEntity(e);
                return;
            }

            // 范围型弹幕的逻辑
            if (e.shape.Exist)
            {
                e.Size = Time.time - e.instantiateTime + 1;
                e.shape.Get().Center = new Vector2(e.body.transform.position.x, e.body.transform.position.z);
                OnProjectileHitTargets(e, ProjectileTargetSys.TargetsInShape(e.shape.Get(), null));
            }

            // 正常逻辑
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

            // 具有轨迹的射弹逻辑
            if (e.curve != null)
            {
                e.curve.speed = e.speed;
                e.curve.DirUpdate();
                e.curve.PosUpdate();
            }
        }
    }
}
