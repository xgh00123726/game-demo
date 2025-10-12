using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameBase.Buffs
{
    public class BuffSys : CommonEntitySys<Buff, BuffSys>
    {
        protected override void OnRegisterEntityToActives(Buff e)
        {
            e.isInfiDuration = false;
            e.alive = true;
            e.durationRemain = 0;
            e.instantiateTime = Time.time;
        }

        protected override void OnRemoveEntityFromActives(Buff e)
        {
            foreach (var m in e.modifyers)
            {
                m.Release();
            }
            e.modifyers.Clear();
            e.alive = false;
            e.owner = null;
        }

        protected override void UpdateEntity(Buff e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("buff has no owner");
                RemoveEntity(e);
                e.owner.OnRemoveBuff(e);
                return;
            }

            if (e.isInfiDuration)
            {
                e.durationSet = 9999f;
                e.durationRemain = e.durationSet;
            }
            else
            {
                e.durationRemain = e.durationSet - (Time.time - e.instantiateTime);
            }
                

            if (e.durationRemain <= 0)
            {
                RemoveEntity(e);
                e.owner.OnRemoveBuff(e);
            }
        }
    }
}
