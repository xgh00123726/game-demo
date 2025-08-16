using GameBase.Flyings;
using GameBase.GEffects;
using GameBase.Tools;
using System;
using UnityEngine;

namespace Combines.Projectiles
{
    public class ProjectileSys : SimplestEntitySys<Projectile, SimpleEntityContainer, ProjectileSys>
    {
        protected override void UpdateEntity(Projectile e)
        { 
        }

        protected override void OnRegisterEntityToActives(Projectile e)
        {
            e.flying.src = e.owner.HandPosition;
            e.flying.dest = e.target.Center;

            e.flying.AfterInstantiateObj = () => e.flyingGeneratedObj = true;
        }

        private void EffectTarget(Func<GEffect<IProjectileOwner, IProjectileTarget>> effectConstructor, IProjectileOwner owner, IProjectileTarget target)
        {
            var effect = effectConstructor?.Invoke();
            if (effect == null)
            {
                return;
            }

            effect.owner = owner;
            effect.target = target;
        }

        protected override void FixedUpdateEntity(Projectile e)
        {
            if (e.owner == null || e.flying == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("null owner/flying");
                RemoveEntity(e);
                return;
            }
            if (e.target == null && (e.shape == null && e.targetsSet == null))
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("projectile has null target");
                RemoveEntity(e);
                return;
            }
            if (!e.flying.Alive)
            {
                RemoveEntity(e);
                return;
            }
            if (!e.flyingGeneratedObj)
            {
                return;
            }

            var objTransform = e.flying.Obj.transform;
            e.flying.dest = e.target.Center;

            if (e.shape != null)
            {
                e.shape.Center = new Vector2(objTransform.position.x, objTransform.position.z);
                foreach (var target in e.targetsSet.TargetsInShape(e.shape))
                {
                    if (e.actualEffectTimes >= e.maxeffectTimes)
                    {
                        break;
                    }
                    EffectTarget(e.effectConstructor, e.owner, target);
                    e.actualEffectTimes++;
                }
            }

            if (e.target != null && e.actualEffectTimes < e.maxeffectTimes)
            {
                float distoTarget = (objTransform.position - e.target.Center).magnitude;
                
                if (distoTarget < e.target.Radius)
                {
                    EffectTarget(e.effectConstructor, e.owner, e.target);
                    e.actualEffectTimes++;
                }
            }
        }
    }
}
