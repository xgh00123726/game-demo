using GameBase.EntitySystem;
using GameBase.Modify;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameBase.Buffs
{
    public class BuffSys : SealedEntitySys<Buff, BuffSys>
    {
        protected override void OnGet(Buff e)
        {
            e.alive = true;
            e.durationRemain = 0;
            e.instantiateTime = Time.time;
        }

        protected override void OnRelease(Buff e)
        {
            e.owner.OnRemoveBuff(e);
            foreach (var m in e.Modifyers)
            {
                m.Release();
            }
            e.Modifyers.Clear();
            e.alive = false;
            e.owner = null;
        }

        protected override void EntityStart(Buff e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log("buff has no owner");
                RemoveEntity(e);
                return;
            }

            foreach (var kvp in e.IntKeyModifiers)
            {
                var m = ModifyerSys.Instance.NewEntity();
                m.Value = kvp.Value;
                m.Type = ModifyType.Temporary | ModifyType.Always;
                m.AddTo(e.owner.Modifyables[kvp.Key]);
                e.Modifyers.Add(m);
            }

            e.durationRemain = e.DurationSet;
            e.owner.OnGetBuff(e);
        }

        protected override void UpdateEntity(Buff e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("buff has no owner");
                RemoveEntity(e);
                return;
            }

            if (e.IsInfiDuration)
            {
                e.DurationSet = 9999f;
                e.durationRemain = e.DurationSet;
            }
            else
            {
                e.durationRemain = e.DurationSet - (Time.time - e.instantiateTime);
            }
                

            if (e.durationRemain <= 0)
            {
                RemoveEntity(e);
            }
        }
    }
}
