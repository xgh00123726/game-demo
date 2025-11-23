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
        protected override void OnGet(Projectile e)
        {
            e.alive = true;
            e.ArriveDis = 0.1f;
            e.Damage = 1;
            e.AmpFactor = 1f;
            e.DamageTextColor = Color.white;
            e.DamageTextPrefabName = "Prefabs/UI/FloatText";
        }

        protected override void OnRelease(Projectile e)
        {
            e.alive = false;
            e.Trigger.TargetCamp = null;

            if (e.Flying.Alive)
            {
                FlyingSys.Instance.RemoveEntity(e.Flying);
            }
            TriggerSys.Instance.RemoveEntity(e.Trigger);

            e.Flying = null;
            e.Trigger = null;
            e.owner = null;
            e.target = null;
        }

        protected override void EntityStart(Projectile e)
        {
            if (e.Flying == null || e.Trigger == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("projectile must has flying and trigger");
                return;
            }

            if (e.IsAutoFindPossibleTarget && e.target == null)
            {
                var c = CreatureSys.Instance.NearestEntity(e.Flying.Position, (Camp)e.Trigger.TargetCamp, e.FindTargetRange);
                if (c != null)
                {
                    e.Target = c;
                    e.Flying.Target = c;
                }
                else
                {
                    RemoveEntity(e);
                    return;
                }
            }

            e.Flying.Target = e.target;
            e.Flying.Src = e.owner.HandPosition;

            if (e.IsTrigOnlyWhenHitMainTarget)
            {
                e.Trigger.TrigStyle = Triggers.TrigStyle.External;
            }
            else
            {
                e.Trigger.TrigStyle = Triggers.TrigStyle.Always;
            }

            // 如果没有阵营，则默认射弹向敌方发出
            e.Trigger.TargetCamp ??= new CampSet()
                {
                    include = CampSet.Typedef.Others,
                }.GetCamp(e.owner.Camp);

            e.Trigger.Action = new ProjectileAction()
            {
                damage = e.owner.Modifyables["damage"].Value * e.AmpFactor + e.Damage,
                prefabName = e.DamageTextPrefabName,
                color = e.DamageTextColor,
            };

            e.Trigger.Attach = e.Flying;
            e.Trigger.Target = e.target;
        }

        private void TraceMainTargetProcess(Projectile e)
        {
            if (e.IsTrigOnlyWhenHitMainTarget && e.target != null)
            {
                float arriveDis = e.ArriveDis;
                arriveDis += e.target.Radius;
                if ((e.Flying.Dest - e.Flying.Position).magnitude <= arriveDis)
                {
                    e.Trigger.Target = e.target;
                    e.Trigger.Trig();
                }
            }
        }

        private void DestroyProcess(Projectile e)
        {
            if (e.IsDestroyOnEffectMaxTimes)
            {
                if (e.Trigger.ActualEffectTimes >= e.Trigger.MaxEffectTimes)
                {
                    RemoveEntity(e);
                    return;
                }
            }
            if (e.Flying != null)
            {
                if (!e.Flying.Alive)
                {
                    RemoveEntity(e);
                    return;
                }
                if (e.IsDestroyOnFlyingEnd && e.Flying.IsEnd)
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
            if (e.Flying != null)
            {
                TraceMainTargetProcess(e);
            }
        }
    }
}
