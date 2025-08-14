using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellSys : SimplestEntitySys<Spell, SimpleEntityContainer, SpellSys>
    {
        protected override void OnRegisterEntityToActives(Spell e)
        {
            e.RegistertoActivesDelegate?.Invoke(e);

            if (e.speller != null)
            {
                e.acceletate = e.speller.CoolingAccelerate;
            }
        }

        protected override void OnRemoveEntityFromActives(Spell e)
        {
            e.RemoveFromActiveDelegate?.Invoke(e);
        }

        protected override void UpdateEntity(Spell e)
        {
            if (e.speller == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning).EditorOnly(true)
                    .Log("spell must have speller");
                RemoveEntity(e);
                return;
            }

            // 冷却时间更新
            if (!e.coolReady)
            {
                float acc = 1;
                acc = e.acceletate * 0.01f + 1;
                e.coolingTimeRemain -= Time.deltaTime * acc;
                e.coolReady = e.coolingTimeRemain <= 0;
            }

            if (e.coolReady)
            {
                if (e.targetable && !e.userReady)
                {
                    e.userReady = e.ReadyJugDelegate?.Invoke(e) == true;

                    if (e.userReady && e.indicator != null)
                    {
                        e.indicator.Show();
                    }
                }
                else if (!e.targetable)
                {
                    e.userReady = true;
                }

                if (e.userReady && e.CancelJugDelegate?.Invoke(e) == true)
                {
                    e.userReady = false;
                    if (e.indicator != null)
                    {
                        e.indicator.Hide();
                    }
                }
                else if (e.userReady && e.CastJugDelegate?.Invoke(e) == true)
                {
                    e.coolReady = false;
                    e.userReady = false;
                    e.coolingTimeRemain = e.coolingTimeSet;
                    e.CastAction?.Invoke(e);
                    if (e.indicator != null)
                    {
                        e.indicator.Hide();
                    }
                }
            }

            if (e.userReady)
            {
                e.SpellReadyingDelegate?.Invoke();
            }
        }
    }
}
