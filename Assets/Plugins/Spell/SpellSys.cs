using GameBase.EntitySystem;
using GameBase.Tools;

namespace GameBase.Spells
{
    public class SpellSys : SealedEntitySys<Spell, SpellSys>
    {
        protected override void OnRelease(Spell e)
        {
            e.isTrig = false;
        }

        protected override void UpdateEntity(Spell e)
        {
            if (e.speller == null || e.interactive == null)
            {
                XLogger.Instance.Log($"spell has been remove. spller:{e.speller}, interactive:{e.interactive}");
                RemoveEntity(e);
                return;
            }

            if (e.isTrig)
            {
                if (e.spellCoolingdown.IsCoolingOver)
                {
                    e.interactive.OnTrig();
                    if (e.action?.CastAction(e) == true)
                    {
                        e.spellCoolingdown.Recooling();
                    }
                }
                e.isTrig = false;
            }

            e.spellCoolingdown.Update(e.speller.CoolingAccelerate);
        }
    }
}
