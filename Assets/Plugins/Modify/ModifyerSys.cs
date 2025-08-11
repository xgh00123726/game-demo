using GameBase.Tools;
using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyerSys<T> : SimplestEntitySys<Modifyer<T>, SimpleEntityContainer, ModifyerSys<T>>
    {
        protected override void OnRegisterEntityToActives(Modifyer<T> e)
        {
            e.instantiateTime = Time.time;
            e.lastEnableTime = Time.time;
            e.isRelease = false;

            if (e.trigOnGive)
            {
                e.enable = true;
            }

            if ((e.type & ModifyType.Once) != 0)
            {
                e.enable = true;
            }
        }

        protected override void OnRemoveEntityFromActives(Modifyer<T> e)
        {
            e.enable = false;
            e.isRelease = true;
            e.ClearTrigger = null;
            e.ModifyFunc = null;
            e.modifyableRelease = false;
        }

        protected override void UpdateEntity(Modifyer<T> e)
        {
            if ((e.type & ModifyType.Aways) != 0)
            {
                e.enable = true;
            }

            if ((e.type & ModifyType.Periodoic) != 0)
            {
                if (Time.time - e.lastEnableTime > e.dt)
                {
                    e.enable = true;
                    e.lastEnableTime = Time.time;
                }
            }

            if (e.ClearTrigger?.Invoke() == true // 外部触发
                || e.modifyableRelease // 被modifyable内部触发，ModifyType.Once内部处理
                || Time.time - e.instantiateTime > e.duration) // 超时
            {
                RemoveEntity(e);
            }
        }
    }
}
