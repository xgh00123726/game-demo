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
            if (e.speller == null)
            {
                XLogger.Instance.Log($"spell has been remove.");
                RemoveEntity(e);
                return;
            }
            e.camp = CampUtil.GetCampType(e.campInclude, e.campExclude, e.speller.Camp) | e.campFixed;
        }

        protected override void UpdateEntity(Spell e)
        {
            if (e.speller == null)
            {
                XLogger.Instance.Log($"spell has been remove.");
                RemoveEntity(e);
                return;
            }

            if (e.isTrig)
            {
                if (e.isCoolOver)
                {
                    e.speller.LookAt(e.castPosition);
                    e.isWaitCast = true;
                }
                e.isTrig = false;
            }

            if (e.isWaitCast)
            {
                var dir1 = e.castPosition - e.speller.Position;
                dir1.y = 0;
                var dir2 = new Vector3(e.speller.Dir.x, 0, e.speller.Dir.z);
                float angle = Vector3.Angle(dir1, dir2);
                if (angle < Mathf.Max(e.minCastAngle, e.minCastAngle))
                {
                    if (e.action?.CastAction(e) == true)
                    {
                        e.cooldownRemain = e.cooldown;
                    }
                    e.isWaitCast = false;
                }
            }

            float acc = e.speller.CoolingAccelerate * 0.01f + 1;
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
