using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Triggers
{
    public class TriggerSys : SealedEntitySys<Trigger, TriggerSys>
    {
        protected override bool FixedUpdate => true;

        protected override void OnGet(Trigger e)
        {
            e.TrigPeriod = 1;
            e.TrigStyle = TrigStyle.External;
            e.ExistTime = 10;
            e.actualEffectTimes = 0;
            e.MaxEffectTimes = 1;
            e.HasWhite = false;
            e.isTrig = false;
            e.whites = null;
            e.TargetsSet = null;
            e.Target = null;
            e.Owner = null;
            e.Action = null;
            e.instantiateTime = Time.time;
            e.lastTrigTime = Time.time;
            e.Shape = null;
            e.HitEffect = null;
            e.CreateEffect = null;
            e.HitAudio = null;
            e.CreateAudio = null;
            e.Attach = null;
            e.OnTrig = null;
            e.OnTrigEnd = null;
        }

        private void PlayCreateEffect(Trigger e)
        {
            if (e.CreateEffect == null)
            {
                return;
            }

            float size = 1;
            Vector3 dir = Vector3.one;
            if (e.Shape != null)
            {
                size = e.Shape.Size;
                dir = e.Shape.Dir;
            }
            EffectSys.Instance.PlayAtPSD(e.CreateEffect, e.Attach.Position, new Vector3(size, size, size), dir);
        }

        private void PlayHitEffect(Trigger e, Vector3 position)
        {
            if (e.HitEffect == null)
            {
                return;
            }

            float size = 1;
            Vector3 dir = Vector3.one;
            if (e.Shape != null)
            {
                size = e.Shape.Size;
                dir = e.Shape.Dir;
            }
            EffectSys.Instance.PlayAtPSD(e.HitEffect, position, new Vector3(size, size, size), dir);
        }

        private void PlayTrigEffect(Trigger e)
        {
            if (e.TrigEffect == null)
            {
                return;
            }

            float size = 1;
            Vector3 dir = Vector3.one;
            if (e.Shape != null)
            {
                size = e.Shape.Size;
                dir = e.Shape.Dir;
            }
            EffectSys.Instance.PlayAtPSD(e.TrigEffect, e.Attach.Position, new Vector3(size, size, size), dir);
        }

        private void PlayCreateAudio(Trigger e)
        {
            if (e.CreateAudio == null)
            {
                return;
            }

            AudioMgr.PlayAt(e.CreateAudio, e.Attach.Position);
        }

        private void PlayHitAudio(Trigger e, Vector3 position)
        {
            if (e.HitAudio == null)
            {
                return;
            }

            AudioMgr.PlayAt(e.HitAudio, position);
        }

        private void PlayTrigAudio(Trigger e)
        {
            if (e.TrigAudio == null)
            {
                return;
            }

            AudioMgr.PlayAt(e.TrigAudio, e.Attach.Position);
        }

        internal void HitTarget(Trigger e)
        {
            if (e.Target != null && e.actualEffectTimes < e.MaxEffectTimes)
            {
                EffectTarget(e, e.Target);
                PlayHitAudio(e, e.Target.Position);
                PlayHitEffect(e, e.Target.Position);
            }
        }

        private void EffectTarget(Trigger e, ITriggerTarget target)
        {
            if (e.HasWhite && e.whites.Contains(target))
            {
                return;
            }

            e.Action?.Effect(e, target);
            PlayHitAudio(e, target.Position);
            PlayHitEffect(e, target.Position);
            e.actualEffectTimes++;
            if (e.HasWhite)
            {
                e.whites.Add(target);
            }
        }

        protected override void EntityStart(Trigger e)
        {
            if (e.HasWhite)
            {
                e.whites = new();
            }
            if (e.TrigStyle == TrigStyle.Period || e.TrigStyle == TrigStyle.Once)
            {
                e.isTrig = true;
            }
            if (e.Shape != null && e.Attach != null)
            {
                e.Shape.Center = e.Attach.Position;
            }
            PlayCreateAudio(e);
            PlayCreateEffect(e);
        }

        protected override void UpdateEntity(Trigger e)
        {
            if (e.Owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("null owner");
                RemoveEntity(e);
                return;
            }

            if (Time.time < e.instantiateTime + e.Delay)
            {
                return;
            }

            if (e.TrigStyle == TrigStyle.Period)
            {
                if (Time.time >= e.lastTrigTime + e.TrigPeriod)
                {
                    e.isTrig = true;
                }
            }

            if (e.TrigStyle == TrigStyle.Always)
            {
                e.isTrig = true;
            }

            if (e.isTrig)
            {
                e.OnTrig?.Invoke();
                PlayTrigAudio(e);
                PlayTrigEffect(e);
                HitTarget(e);
                if (e.Shape != null)
                {
                    e.Shape.Center = e.Attach.Position;
                    if (e.TargetsSet == null)
                    {
                        XLogger.Instance.Level(XLogger.LogLevel.Warning)
                            .Log("trigger has no targetset, will hit none target");
                    }
                    else
                    {
                        foreach (var target in e.TargetsSet.TargetsInShape(e.Shape, e.TargetCamp))
                        {
                            if (e.actualEffectTimes >= e.MaxEffectTimes)
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

            if (e.actualEffectTimes >= e.MaxEffectTimes)
            {
                RemoveEntity(e);
                return;
            }

            if (Time.time >= e.instantiateTime + e.ExistTime
                || e.TrigStyle == TrigStyle.Once)
            {
                RemoveEntity(e);
                return;
            }
        }
    }
}
