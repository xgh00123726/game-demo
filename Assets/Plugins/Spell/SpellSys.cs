using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellSys : SimplestEntitySys<Spell, SimpleEntityContainer, SpellSys>
    {
        protected override void OnRegisterEntityToActives(Spell e)
        {
            e.RegistertoActivesDelegate?.Invoke(e);
        }

        protected override void OnRemoveEntityFromActives(Spell e)
        {
            e.RemoveFromActiveDelegate?.Invoke(e);
        }

        protected override void BeforeFirstUpdate(Spell e)
        {
            if (e.speller == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("null speller");
            }
        }

        protected override void UpdateEntity(Spell e)
        {
            if (e.speller == null)
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
                if (!e.userReady && e.interactive.ReadyTrig)
                {
                    e.userReady = true;
                    e.interactive.OnReady();
                }


                if (e.userReady && e.interactive.CancelTrig)
                {
                    e.userReady = false;
                    e.interactive.OnCancel();
                }
                else if (e.userReady && e.interactive.CastTrig)
                {
                    e.coolReady = false;
                    e.userReady = false;
                    e.coolingTimeRemain = e.coolingTimeSet;
                    if (e.actionInterface != null)
                    {
                        e.actionInterface.CastAction(e);
                    }
                    e.interactive.OnCast();
                }
            }

            if (e.userReady)
            {
                e.interactive.OnReadying(e.speller);
            }
        }
    }
}
