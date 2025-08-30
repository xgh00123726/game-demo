using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Buffs
{
    public class BuffSys : CommonEntitySys<Buff, BuffSys>
    {
        protected override void OnRegisterEntityToActives(Buff e)
        {
            e.RegistertoActivesDelegate?.Invoke();
        }

        protected override void OnRemoveEntityFromActives(Buff e)
        {
            e.RemoveFromActiveDelegate?.Invoke();
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

        protected override void BeforeFirstUpdate(Buff e)
        {
            e.durationRemain = e.durationSet;
            foreach (var em in e.modifyers.FixedModifyers)
            {
                e.owner.Modifyables.AddModify(em.Key, em.Value);
            }
            foreach (var em in e.modifyers.SetModifyers)
            {
                e.owner.Modifyables.AddModify(em.Key, em.Value);
            }
            foreach (var em in e.modifyers.CurrModifyers)
            {
                e.owner.Modifyables.AddModify(em.Key, em.Value);
            }
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
