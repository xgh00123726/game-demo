using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.Buffs
{
    public class BuffSys : SimplestEntitySys<Buff, SimpleEntityContainer, BuffSys>
    {
        protected override void OnRegisterEntityToActives(Buff e)
        {
            e.RegistertoActivesDelegate?.Invoke();
            e.durationRemain = e.durationSet;
        }

        protected override void OnRemoveEntityFromActives(Buff e)
        {
            e.RemoveFromActiveDelegate?.Invoke();
        }

        protected override void UpdateEntity(Buff e)
        {
            if (e.owner == null)
            {
                return;
            }

            if (e.durationRemain > 0)
            {
                e.durationRemain -= Time.deltaTime;
            }

            if (e.durationRemain <= 0)
            {
                e.owner.Buffs.RemoveBuff(e);
                RemoveEntity(e);
            }
        }
    }
}
