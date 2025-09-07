using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellSys : CommonEntitySys<Spell, SpellSys>
    {
        protected override void OnRegisterEntityToActives(Spell e)
        {
            e.RegistertoActivesDelegate?.Invoke(e);
        }

        protected override void OnRemoveEntityFromActives(Spell e)
        {
            e.RemoveFromActiveDelegate?.Invoke(e);
        }

        protected override void UpdateEntity(Spell e)
        {
            if (e.speller == null || e.interactive == null)
            {
                RemoveEntity(e);
                return;
            }

            // 冷却时间更新
            if (!e.coolReady)
            {
                float acc = 1;
                acc = e.speller.CoolingAccelerate * 0.01f + 1;
                e.coolingTimeRemain -= Time.deltaTime * acc;
                e.coolReady = e.coolingTimeRemain <= 0;
            }

            if (e.coolReady)
            {
                e.interactive.Update(e.speller);
                if (e.interactive.IsTrig)
                {
                    e.coolReady = false;
                    e.coolingTimeRemain = e.coolingTimeSet;
                    e.actionInterface?.CastAction(e);
                }
            }
        }
    }
}
