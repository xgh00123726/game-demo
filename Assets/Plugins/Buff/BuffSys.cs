using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Buffs
{
    public class BuffSys : CommonEntitySys<Buff, BuffSys>
    {
        protected override void OnRegisterEntityToActives(Buff e)
        {
        }

        protected override void OnRemoveEntityFromActives(Buff e)
        {
            foreach (var em in e.modifyers.FixedModifyers)
            {
                em.Value.externalClear = true;
            }
            foreach (var em in e.modifyers.SetModifyers)
            {
                em.Value.externalClear = true;
            }
            foreach (var em in e.modifyers.CurrModifyers)
            {
                em.Value.externalClear = true;
            }
            e.modifyers.Clear();
        }

        protected override void UpdateEntity(Buff e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("buff has no owner");
                RemoveEntity(e);
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
            }
        }

        internal void RemoveBuff(Buff e)
        {
            RemoveEntity(e);
        }
    }
}
