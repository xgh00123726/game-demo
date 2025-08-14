using GameBase.Flyings;
using GameBase.GEffects;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Combines.Projectiles
{
    public class ProjectileSys : SimplestEntitySys<Projectile, SimpleEntityContainer, ProjectileSys>
    {
        protected override void UpdateEntity(Projectile e)
        { 
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
            if (e.owner == null || e.effectConstructor == null || e.flying == null)
            {
                RemoveEntity(e);
                return;
            }
            if (e.target == null && (e.shape == null && e.targetsSet == null))
            {
                RemoveEntity(e);
                return;
            }
            if (!e.updateEnable)
            {
                return;
            }
            if (!e.flying.Alive)
            {
                RemoveEntity(e);
                return;
            }

            var objTransform = e.flying.Obj.transform;

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
