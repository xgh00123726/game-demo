using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spell
{
    public class SpellSys : SimplestEntitySys<Spell, CSObjectPool<Spell>>
    {
        protected override void OnRegisterEntity(Spell e)
        {
            
        }

        protected override void OnRemoveEntity(Spell e)
        {
            
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

            bool coolReady = e.coolReady;

            bool userReady;
            if (!e.targetable)
            {
                userReady = true;
            }
            else
            {
                userReady = e.userReady = e.ReadyDelegate?.Invoke(e) == true;
            }

            if (coolReady)
            {
                if (!userReady && e.ReadyDelegate?.Invoke(e) == true)
                {
                    e.userReady = true;
                }
                else if (userReady && e.CancelDelegate?.Invoke(e) == true)
                {
                    e.userReady = false;
                }
                else if (userReady && e.CastDelegate?.Invoke(e) == true)
                {
                    e.coolReady = false;
                    e.coolingTimeRemain = e.coolingTimeSet;
                    e.CastAction?.Invoke(e);
                }
            }
        }
    }
}
