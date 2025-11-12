using GameBase.Creatures;
using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace GameBase.Projectiles
{
    public class ProjectileSys : SealedEntitySys<Projectile, ProjectileSys>
    {
        public ProjectileSys()
        {
            _fixedUpdate = true;
        }
        protected override void OnGet(Projectile e)
        {
            e.alive = true;
            e.arriveDis = 0.1f;
            e.damage = 1;
            e.ampFactor = 1f;
            e.damageTextColor = Color.white;
            e.damageTextPrefabName = "Prefabs/UI/FloatText";
        }

        protected override void OnRelease(Projectile e)
        {
            e.alive = false;
            e.trigger.targetCamp = null;

            if (e.flying.Alive)
            {
                FlyingSys.Instance.RemoveEntity(e.flying);
            }
            TriggerSys.Instance.RemoveEntity(e.trigger);

            e.flying = null;
            e.trigger = null;
            e.owner = null;
            e.target = null;
        }

        protected override void EntityStart(Projectile e)
        {
            if (e.flying == null || e.trigger == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("projectile must has flying and trigger");
            }

            e.flying.target = e.target;
            e.flying.Src = e.owner.HandPosition;

            if (e.IsTrigOnlyWhenHitMainTarget())
            {
                e.trigger.trigStyle = Triggers.TrigStyle.External;
            }
            else
            {
                e.trigger.trigStyle = Triggers.TrigStyle.Always;
            }

            // 如果没有阵营，则默认射弹向敌方发出
            e.trigger.targetCamp ??= new CampSet()
                {
                    include = CampSet.Typedef.Others,
                }.GetCamp(e.owner.camp);

            e.trigger.action = new ProjectileAction()
            {
                damage = e.owner.modifyables["damage"].Value * e.ampFactor + e.damage,
                prefabName = e.damageTextPrefabName,
                color = e.damageTextColor,
            };

            e.trigger.attach = e.flying;
            e.trigger.target = e.target;
        }

        private void TraceMainTargetProcess(Projectile e)
        {
            if (e.IsTrigOnlyWhenHitMainTarget() && e.target != null)
            {
                float arriveDis = e.arriveDis;
                arriveDis += e.target.radius;
                if ((e.flying.Dest - e.flying.Position).magnitude <= arriveDis)
                {
                    e.trigger.target = e.target;
                    e.trigger.Trig();
                }
            }
        }

        private void FindTargetProcess(Projectile e)
        {
            if (e.IsAutoFindPossibleTarget() && e.target == null)
            {
                var c = CreatureSys.Instance.NearestEntity(e.flying.Position, (Camp)e.trigger.targetCamp, e.findTargetRange);
                if (c != null)
                {
                    e.Target = c;
                    e.flying.target = c;
                }
            }
        }

        private void DestroyProcess(Projectile e)
        {
            if (e.IsDestroyOnEffectMaxTimes())
            {
                if (e.trigger.ActualEffectTimes >= e.trigger.maxEffectTimes)
                {
                    RemoveEntity(e);
                    return;
                }
            }
            if (e.flying != null)
            {
                if (!e.flying.Alive)
                {
                    RemoveEntity(e);
                    return;
                }
                if (e.IsDestroyOnFlyingEnd() && e.flying.IsEnd)
                {
                    RemoveEntity(e);
                    return;
                }
            }
        }

        protected override void UpdateEntity(Projectile e)
        {
            DestroyProcess(e);
            if (!e.alive)
            {
                return;
            }
            FindTargetProcess(e);
            if (e.flying != null)
            {
                TraceMainTargetProcess(e);
            }
        }
    }
}
