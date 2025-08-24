using GameBase.Tools;
using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.Projectiles
{
    public class ProjectileSys : SimplestEntitySys<Projectile, SimpleEntityContainer, ProjectileSys>
    {
        protected override void UpdateEntity(Projectile e)
        { 
        }

        protected override void BeforeFirstUpdate(Projectile e)
        {
            if (e.hasWhite)
            {
                e.whites = new System.Collections.Generic.HashSet<int>();
            }

            e.flying.OnHit += () => HitTarget(e);
        }

        private void HitTarget(Projectile e)
        {
            if (e.target != null && e.actualEffectTimes < e.maxeffectTimes)
            {
                EffectTarget(e, e.target);
            }
        }

        private void EffectTarget(Projectile e, IProjectileTarget target)
        {
            if (e.hasWhite && e.whites.Contains(target.InstanceID))
            {
                return;
            }

            var effect = e.effectConstructor?.Invoke();
            if (effect == null)
            {
                return;
            }

            effect.owner = e.owner;
            effect.target = target;
            e.actualEffectTimes++;
            if (e.hasWhite)
            {
                e.whites.Add(target.InstanceID);
            }
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
            if (!e.flying.Alive)
            {
                RemoveEntity(e);
                return;
            }

            var objTransform = e.flying.Obj.transform;
            if (e.target != null)
            {
                e.flying.dest = e.target.Center;
            }

            if (e.shape != null)
            {
                e.shape.Center = new Vector2(objTransform.position.x, objTransform.position.z);
                foreach (var target in e.targetsSet.TargetsInShape(e.shape))
                {
                    if (e.actualEffectTimes >= e.maxeffectTimes)
                    {
                        break;
                    }
                    EffectTarget(e, target);
                }
            }
        }
    }
}
