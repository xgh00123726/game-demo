using GameBase.EntitySystem;
using GameBase.Math;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellSys : SealedEntitySys<Spell, SpellSys>
    {
        public const float MIN_CAST_ANGLE = 10f;

        protected override void OnRelease(Spell e)
        {
            e.isTrig = false;
            e.isWaitCast = false;
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
                    e.speller.LookAt(e.interactive.TrigPosition);
                    e.isWaitCast = true;
                }
                e.isTrig = false;
            }

            if (e.isWaitCast)
            {
                var dir1 = e.interactive.TrigPosition - e.speller.Position;
                dir1.y = 0;
                var dir2 = new Vector3(e.speller.Dir.x, 0, e.speller.Dir.z);
                float angle = Vector3.Angle(dir1, dir2);
                if (angle < Mathf.Max(e.minCastAngle, MIN_CAST_ANGLE))
                {
                    if (e.action?.CastAction(e) == true)
                    {
                        e.spellCoolingdown.Recooling();
                    }
                    e.isWaitCast = false;
                }
            }

            e.spellCoolingdown.Update(e.speller.CoolingAccelerate);
        }
    }
}
