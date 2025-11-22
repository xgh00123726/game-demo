using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Modify
{
    public class ModifyerSys : SealedEntitySys<Modifyer, ModifyerSys>
    {
        protected override void OnGet(Modifyer e)
        {
            e.instantiateTime = Time.time;
            e.lastEnableTime = Time.time;
            e.Target = null;
        }

        protected override void OnRelease(Modifyer e)
        {
            e.OnModify = null;
        }

        protected override void Update()
        {
            foreach (var m in ModifyableSys.Instance.Entities)
            {
                m.tempValue = m.valueSet;
            }
            base.Update();
            foreach (var m in ModifyableSys.Instance.Entities)
            {
                m.value = m.tempValue;
            }
        }

        protected override void UpdateEntity(Modifyer e)
        {
            if (e.Target == null)
            {
                RemoveEntity(e);
                return;
            }

            bool enable = false;

            if ((e.Type & ModifyType.Always) != 0 || (e.Type & ModifyType.Once) != 0)
            {
                enable = true;
            }

            if ((e.Type & ModifyType.Periodoic) != 0)
            {
                if (Time.time - e.lastEnableTime > e.DT)
                {
                    enable = true;
                    e.lastEnableTime = Time.time;
                }
            }

            if (enable)
            {
                if ((e.Type & ModifyType.Temporary) != 0)
                {
                    e.Target.tempValue += e.Value;
                }
                else if ((e.Type & ModifyType.Forever) != 0)
                {
                    e.Target.valueSet += e.Value;
                }
                e.OnModify?.Invoke();
            }

            if ((e.Type & ModifyType.Once) != 0)
            {
                RemoveEntity(e);
            }
        }
    }
}
