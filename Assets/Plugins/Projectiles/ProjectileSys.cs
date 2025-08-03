using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.Projectile
{
    public class ProjectileSys : UObjEntitySys<Projectile, CSObjectPool<Projectile>, GameObject, UObjectPool<GameObject>, ProjectileSys>
    {
        public float fixedFreq = 60;
        public int actualFreq = 0;

        public static readonly float ProjectileHitDis = 0.1f;

        public static IProjectileTargetSys ProjectileTargetSys { set; get; }

        protected override int ContainerCapacity => ResourcesLoader.PrefabCount;

        protected override float FixedFreq => fixedFreq;

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

            // 伤害间隔外无效果
            if (e.hitInterval > 1 && fixedTick % e.hitInterval != 0)
            {
                return;
            }

            var target = e.target.Get();
            // 存在白名单
            // 并且白名单中没有该射弹
            // 并且射弹穿透没有到达上限
            if (e.whites.Exist 
                && !e.whites.Get().Contains(target.ID) 
                && e.actualPenetrate < e.penetrate)
            {
                e.whites.Get().Add(target.ID);
                e.target.Get().GetDamage(e.damage);
                e.actualPenetrate++;
            }
            // 不存在白名单
            // 并且射弹穿透没有到达上限
            else if (!e.whites.Exist
                && e.actualPenetrate < e.penetrate)
            {
                e.target.Get().GetDamage(e.damage);
                e.actualPenetrate++;
            }
        }

        void OnProjectileHitTargets(Projectile e, LinkedList<IProjectileTarget> targets)
        {
            if (targets == null) return;

            // 伤害间隔外无效果
            if (e.hitInterval > 1 && fixedTick % e.hitInterval != 0)
            {
                return;
            }

            foreach (var target in targets)
            {
                if (e.whites.Exist 
                    && !e.whites.Get().Contains(target.ID)
                    && e.actualPenetrate < e.penetrate)
                {
                    e.whites.Get().Add(target.ID);
                    target.GetDamage(e.damage);
                    e.actualPenetrate++;
                }
                else if (!e.whites.Exist
                    && e.actualPenetrate < e.penetrate)
                {
                    target.GetDamage(e.damage);
                    e.actualPenetrate++;
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
            // 射弹超时
            if (Time.time > e.maxExistTime + e.instantiateTime)
            {
                RemoveEntity(e);
                return;
            }

            e.OnAlive?.Invoke(e);

            // 具有轨迹的射弹逻辑
            if (e.curve != null)
            {
                e.curve.speed = e.speed;
                e.curve.DirUpdate();
                e.curve.PosUpdate();
            }
        }

        protected override void FixedUpdateEntity(Projectile e)
        {
            // tick逻辑
            if (e.tickRate > 1 && fixedTick % e.tickRate != 0)
            {
                return;
            }

            e.OnAliveFixed?.Invoke(e);

            // 瞬发型弹幕的逻辑
            if (e.isImmediately)
            {
                e.body.transform.position = e.Dest;
            }

            // 范围型弹幕的逻辑
            if (e.shape != null)
            {
                e.shape.Center = new Vector2(e.body.transform.position.x, e.body.transform.position.z);
                OnProjectileHitTargets(e, ProjectileTargetSys.TargetsInShape(e.shape));
            }

            // 击中逻辑
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
        }
    }
}
