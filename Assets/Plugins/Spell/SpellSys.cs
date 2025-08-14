using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellSys : SimplestEntitySys<Spell, SimpleEntityContainer, SpellSys>
    {
        protected override void OnRegisterEntityToActives(Spell e)
        {
            e.RegistertoActivesDelegate?.Invoke(e);
        }

        protected override void OnRemoveEntityFromActives(Spell e)
        {
            e.RemoveFromActiveDelegate?.Invoke(e);
        }

        protected override void UpdateEntity(Spell e)
        {
            // 冷却时间更新
            if (!e.coolReady)
            {
                float acc = 1;
                if (e.speller.Exist)
                {
                    acc = e.speller.Get().CoolingAccelerate * 0.01f + 1;
                }
                e.coolingTimeRemain -= Time.deltaTime * acc;
                e.coolReady = e.coolingTimeRemain <= 0;
            }

            if (e.coolReady)
            {
                if (e.targetable && !e.userReady)
                {
                    e.userReady = e.ReadyJugDelegate?.Invoke(e) == true;

                    if (e.userReady)
                    {
                        e.SpellToReadyDelegate?.Invoke();
                    }
                }
                else if (!e.targetable)
                {
                    e.userReady = true;
                }

                if (e.userReady && e.CancelJugDelegate?.Invoke(e) == true)
                {
                    e.userReady = false;
                    e.SpellExitReadyDelegate?.Invoke();
                }
                else if (e.userReady && e.CastJugDelegate?.Invoke(e) == true)
                {
                    e.coolReady = false;
                    e.userReady = false;
                    e.coolingTimeRemain = e.coolingTimeSet;
                    e.CastAction?.Invoke(e);
                    e.SpellExitReadyDelegate?.Invoke();
                }
            }

            if (e.userReady)
            {
                e.SpellReadyingDelegate?.Invoke();
            }
        }
    }
}
