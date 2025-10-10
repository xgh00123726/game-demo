using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Triggers
{
    public class TriggerSys : CommonEntitySys<Trigger, TriggerSys>
    {
        public TriggerSys()
        {
            _fixedUpdate = true;
        }

        protected override void OnRegisterEntityToActives(Trigger e)
        {
            if (e.hasWhite)
            {
                e.whites = new();
            }

            e.instantiateTime = Time.time;
            e.lastTrigTime = Time.time;
            if (e.trigStyle == TrigStyle.PeriodImmediate)
            {
                e.isTrig = true;
            }
        }

        internal void HitTarget(Trigger e)
        {
            if (e.target != null && e.actualEffectTimes < e.maxeffectTimes)
            {
                EffectTarget(e, e.target);
            }
        }

        private void EffectTarget(Trigger e, ITriggerTarget target)
        {
            if (e.hasWhite && e.whites.Contains(target))
            {
                return;
            }

            e.action?.Effect(e, target);
            e.actualEffectTimes++;
            if (e.hasWhite)
            {
                e.whites.Add(target);
            }
        }

        protected override void UpdateEntity(Trigger e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("null owner");
                RemoveEntity(e);
                return;
            }

            if (e.trigStyle == TrigStyle.PeriodImmediate || e.trigStyle == TrigStyle.PeriodNext)
            {
                if (Time.time > e.lastTrigTime + e.trigPeriod)
                {
                    e.isTrig = true;
                }
            }

            if (e.trigStyle == TrigStyle.Always)
            {
                e.isTrig = true;
            }

            if (e.isTrig)
            {
                e.OnTrig?.Invoke();
                HitTarget(e);

                if (e.shape != null)
                {
                    e.shape.Center = new Vector2(e.attach.Position.x, e.attach.Position.z);
                    foreach (var target in e.targetsSet.TargetsInShape(e.shape))
                    {
                        if (e.actualEffectTimes >= e.maxeffectTimes)
                        {
                            break;
                        }
                        EffectTarget(e, target);
                    }
                }
                e.lastTrigTime = Time.time;
                e.isTrig = false;
            }

            if (e.actualEffectTimes >= e.maxeffectTimes)
            {
                RemoveEntity(e);
                return;
            }

            if (Time.time > e.instantiateTime + e.existTime)
            {
                RemoveEntity(e);
                return;
            }
        }
    }
}
