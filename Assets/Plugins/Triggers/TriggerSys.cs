using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Triggers
{
    public class TriggerSys : SealedEntitySys<Trigger, TriggerSys>
    {
        public TriggerSys()
        {
            _fixedUpdate = true;
        }

        protected override void OnGet(Trigger e)
        {
            e.trigPeriod = 1;
            e.trigStyle = TrigStyle.External;
            e.existTime = 1;
            e.actualEffectTimes = 0;
            e.maxEffectTimes = 1;
            e.hasWhite = false;
            e.isTrig = false;
            e.whites = null;
            e.targetsSet = null;
            e.target = null;
            e.owner = null;
            e.action = null;
            e.instantiateTime = Time.time;
            e.lastTrigTime = Time.time;
            e.shape = null;
            e.hitEffect = null;
            e.createEffect = null;
            e.hitAudio = null;
            e.createAudio = null;
            e.OnTrig = null;
            e.OnTrigEnd = null;
        }

        private void PlayCreateEffect(Trigger e)
        {
            if (e.createEffect == null)
            {
                return;
            }

            EffectSys.Instance.PlayAtP(e.createEffect, e.attach.Position);
        }

        private void PlayHitEffect(Trigger e, Vector3 position)
        {
            if (e.hitEffect == null)
            {
                return;
            }

            float size = e.shape.Size;
            EffectSys.Instance.PlayAtPS(e.hitEffect, position, new Vector3(size, size, size));
        }

        private void PlayTrigEffect(Trigger e)
        {
            if (e.trigEffect == null)
            {
                return;
            }

            float size = e.shape.Size;
            Vector2 shapeDir = e.shape.Dir;
            Vector3 dir = new Vector3(shapeDir.x, 0, shapeDir.y);
            EffectSys.Instance.PlayAtPSD(e.trigEffect, e.attach.Position, new Vector3(size, size, size), dir);
        }

        private void PlayCreateAudio(Trigger e)
        {
            if (e.createAudio == null)
            {
                return;
            }

            AudioMgr.PlayAt(e.createAudio, e.attach.Position);
        }

        private void PlayHitAudio(Trigger e, Vector3 position)
        {
            if (e.hitAudio == null)
            {
                return;
            }

            AudioMgr.PlayAt(e.hitAudio, position);
        }

        private void PlayTrigAudio(Trigger e)
        {
            if (e.trigAudio == null)
            {
                return;
            }

            AudioMgr.PlayAt(e.trigAudio, e.attach.Position);
        }

        internal void HitTarget(Trigger e)
        {
            if (e.target != null && e.actualEffectTimes < e.maxEffectTimes)
            {
                EffectTarget(e, e.target);
                PlayHitAudio(e, e.target.Position);
                PlayHitEffect(e, e.target.Position);
            }
        }

        private void EffectTarget(Trigger e, ITriggerTarget target)
        {
            if (e.hasWhite && e.whites.Contains(target))
            {
                return;
            }

            e.action?.Effect(e, target);
            PlayHitAudio(e, target.Position);
            PlayHitEffect(e, target.Position);
            e.actualEffectTimes++;
            if (e.hasWhite)
            {
                e.whites.Add(target);
            }
        }

        protected override void EntityStart(Trigger e)
        {
            if (e.hasWhite)
            {
                e.whites = new();
            }
            if (e.trigStyle == TrigStyle.Period || e.trigStyle == TrigStyle.Once)
            {
                e.isTrig = true;
            }
            PlayCreateAudio(e);
            PlayCreateEffect(e);
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

            if (Time.time < e.instantiateTime + e.delay)
            {
                return;
            }

            if (e.trigStyle == TrigStyle.Period)
            {
                if (Time.time >= e.lastTrigTime + e.trigPeriod)
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
                PlayTrigAudio(e);
                PlayTrigEffect(e);
                HitTarget(e);
                if (e.shape != null)
                {
                    e.shape.Center = new Vector2(e.attach.Position.x, e.attach.Position.z);
                    if (e.targetsSet == null)
                    {
                        XLogger.Instance.Level(XLogger.LogLevel.Warning)
                            .Log("trigger has no targetset, will hit none target");
                    }
                    else
                    {
                        foreach (var target in e.targetsSet.TargetsInShape(e.shape, e.camp))
                        {
                            if (e.actualEffectTimes >= e.maxEffectTimes)
                            {
                                break;
                            }
                            EffectTarget(e, target);
                        }
                    }
                }
                e.lastTrigTime = Time.time;
                e.isTrig = false;
                e.OnTrigEnd?.Invoke();
            }

            if (e.actualEffectTimes >= e.maxEffectTimes)
            {
                RemoveEntity(e);
                return;
            }

            if (Time.time >= e.instantiateTime + e.existTime
                || e.trigStyle == TrigStyle.Once)
            {
                RemoveEntity(e);
                return;
            }
        }
    }
}
