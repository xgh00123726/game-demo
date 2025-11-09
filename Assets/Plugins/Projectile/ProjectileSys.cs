using GameBase.EntitySystem;
using GameBase.Flyings;
using GameBase.Tools;
using GameBase.Triggers;

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
            e.arriveDis = 0.1f;
        }

        protected override void OnRelease(Projectile e)
        {
            FlyingSys.Instance.RemoveEntity(e.flying);
            TriggerSys.Instance.RemoveEntity(e.trigger);
        }

        protected override void EntityStart(Projectile e)
        {
            if (e.searchTargetStyle == SearchTargetStyle.TraceTarget)
            {
                e.flying.target = e.target;
            }

            if ((e.tag & Tag.TrigOnlyWhenHitMainTarget) == 0)
            {
                e.trigger.trigStyle = Triggers.TrigStyle.Always;
            }
            else
            {
                e.trigger.trigStyle = Triggers.TrigStyle.External;
            }

            e.trigger.attach = e.flying;
            e.trigger.target = e.target;
        }

        private void TraceMainTargetProcess(Projectile e)
        {
            if ((e.tag & Tag.TrigOnlyWhenHitMainTarget) != 0)
            {
                float arriveDis = e.arriveDis;
                if (e.target != null && e.searchTargetStyle == SearchTargetStyle.TraceTarget)
                {
                    arriveDis += e.target.radius;
                }
                if (e.flying.target == null)
                {
                    XLogger.Instance.Level(XLogger.LogLevel.Warning)
                        .Log($"projectile has tag:{Tag.TrigOnlyWhenHitMainTarget}, but has no flying target");
                    return;
                }
                if ((e.flying.Dest - e.flying.obj.transform.position).magnitude <= arriveDis)
                {
                    e.trigger.Trig();
                }
            }
        }

        private void DestroyProcess(Projectile e)
        {
            if ((e.tag & Tag.DestroyOnEffectMaxTimes) != 0)
            {
                if (e.trigger.ActualEffectTimes >= e.trigger.maxEffectTimes)
                {
                    RemoveEntity(e);
                    return;
                }
            }
            if (e.flying != null && !e.flying.Alive)
            {
                RemoveEntity(e);
                return;
            }
        }

        protected override void UpdateEntity(Projectile e)
        {
            if (e.flying != null)
            {
                TraceMainTargetProcess(e);
            }
            DestroyProcess(e);
        }
    }
}
