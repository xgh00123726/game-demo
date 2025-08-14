using GameBase.Tools;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameBase.Buffs
{
    public class BuffSys : SimplestEntitySys<Buff, SimpleEntityContainer, BuffSys>
    {
        protected override void OnRegisterEntityToActives(Buff e)
        {
            foreach (var func in e.RegistertoActivesDelegate)
            {
                func?.Invoke(e);
            }
            e.durationRemain = e.durationSet;
            e.alive = true;
        }

        protected override void OnRemoveEntityFromActives(Buff e)
        {
            e.alive = false;
            foreach (var func in e.RemoveFromActiveDelegate)
            {
                func?.Invoke(e);
            }
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
