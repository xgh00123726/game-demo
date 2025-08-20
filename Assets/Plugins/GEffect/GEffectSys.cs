using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.GEffects
{
    public class GEffectSys<T_Owner, T_Target> : SimplestEntitySys<GEffect<T_Owner, T_Target>, SimpleEntityContainer, GEffectSys<T_Owner, T_Target>>
    {
        protected override void OnRegisterEntityToActives(GEffect<T_Owner, T_Target> e)
        {
            if (e.trigOnGet)
            {
                e.lastTrig = 0;
            }
            else
            {
                e.lastTrig = Time.time;
            }
        }

        protected override void UpdateEntity(GEffect<T_Owner, T_Target> e)
        {
            if (e.trigedTimes >= e.maxTrigTimes)
            {
                RemoveEntity(e);
                return;
            }

            if (Time.time > e.lastTrig + e.interval)
            {
                e.EffectAction?.Invoke(e.owner, e.target);
                e.EffectCallback?.Invoke();
                e.lastTrig = Time.time;
                e.trigedTimes++;
            }
        }
    }
}
