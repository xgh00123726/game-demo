using GameBase.Tools;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameBase.Buffs
{
    public class BuffSys : SimplestEntitySys<Buff, SimpleEntityContainer, BuffSys>
    {
        protected override void OnRegisterEntityToActives(Buff e)
        {
            e.InstantiateDelegate?.Invoke(e);
            e.durationRemain = e.durationSet;
            e.alive = true;
        }

        protected override void OnRemoveEntityFromActives(Buff e)
        {
            e.alive = false;
            e.ReleaseDelegate?.Invoke(e);
        }

        protected override void UpdateEntity(Buff e)
        {
            if (e.durationRemain > 0)
            {
                e.durationRemain -= Time.deltaTime;
            }

            if (e.durationRemain <= 0 && e.owner.Exist)
            {
                e.owner.Get().Buffs.RemoveBuff(e);
                RemoveEntity(e);
            }
        }
    }
}
