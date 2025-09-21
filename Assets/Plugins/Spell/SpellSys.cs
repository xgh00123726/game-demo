using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellSys : CommonEntitySys<Spell, SpellSys>
    {        
        protected override void UpdateEntity(Spell e)
        {
            if (e.speller == null || e.interactive == null)
            {
                XLogger.Instance.Log($"spell has been remove. spller:{e.speller}, interactive:{e.interactive}");
                RemoveEntity(e);
                return;
            }

            if (e.interactive.IsTrig)
            {
                if (e.spellCoolingdown.IsCoolingOver)
                {
                    if (e.action?.CastAction(e) != true)
                    {
                    }
                    else
                    {
                        e.spellCoolingdown.Recooling();
                    }
                }
                e.interactive.OnTrig();
            }

            e.spellCoolingdown.Update(e.speller.CoolingAccelerate);
        }
    }
}
