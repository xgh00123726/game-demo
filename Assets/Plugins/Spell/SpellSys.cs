using GameBase.EntitySystem;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Spells
{
    public class SpellSys : SealedEntitySys<Spell, SpellSys>
    {
        protected override void OnRelease(Spell e)
        {
            e.isTrig = false;
            e.isWaitCast = false;
        }

        protected override void EntityStart(Spell e)
        {
            if (e.Speller == null)
            {
                XLogger.Instance.Log($"spell has been remove.");
                RemoveEntity(e);
                return;
            }

            e.TargetCamp = e.TargetCampSet.GetCamp(e.Speller.Camp);
        }

        protected override void UpdateEntity(Spell e)
        {
            if (e.Speller == null)
            {
                XLogger.Instance.Log($"spell has been remove.");
                RemoveEntity(e);
                return;
            }

            if (e.isTrig)
            {
                if (e.isCoolOver)
                {
                    e.Speller.LookAt(e.CastPosition);
                    e.isWaitCast = true;
                }
                e.isTrig = false;
            }

            if (e.isWaitCast)
            {
                var dir1 = e.CastPosition - e.Speller.Position;
                dir1.y = 0;
                var dir2 = new Vector3(e.Speller.Dir.x, 0, e.Speller.Dir.z);
                float angle = Vector3.Angle(dir1, dir2);
                if (angle < Mathf.Max(e.MinCastAngle, e.MinCastAngle))
                {
                    if (e.Action?.CastAction(e) == true)
                    {
                        e.cooldownRemain = e.Cooldown;
                    }
                    e.isWaitCast = false;
                }
            }

            float acc = e.Speller.CoolingAccelerate * 0.01f + 1;
            if (e.cooldownRemain > 0)
            {
                e.cooldownRemain -= Time.deltaTime * acc;
                e.isCoolOver = false;
            }
            else if (e.cooldownRemain <= 0)
            {
                e.isCoolOver = true;
            }
        }
    }
}
