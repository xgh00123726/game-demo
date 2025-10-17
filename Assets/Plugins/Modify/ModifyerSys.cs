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
            e.target = null;
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
            if (e.target == null)
            {
                RemoveEntity(e);
                return;
            }

            bool enable = false;

            if ((e.type & ModifyType.Aways) != 0 || (e.type & ModifyType.Once) != 0)
            {
                enable = true;
            }

            if ((e.type & ModifyType.Periodoic) != 0)
            {
                if (Time.time - e.lastEnableTime > e.dt)
                {
                    enable = true;
                    e.lastEnableTime = Time.time;
                }
            }

            if (enable)
            {
                if ((e.type & ModifyType.Temporary) != 0)
                {
                    e.target.tempValue += e.value;
                }
                else if ((e.type & ModifyType.Forever) != 0)
                {
                    e.target.valueSet += e.value;
                }
                e.OnModify?.Invoke();
            }

            if ((e.type & ModifyType.Once) != 0)
            {
                RemoveEntity(e);
            }
        }
    }
}
