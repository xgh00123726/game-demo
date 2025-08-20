using GameBase.Projectiles;
using GameBase.GCamera;
using GameBase.Modify;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace Instance.Spells
{
    public class TraceProjAction : IAction
    {
        public TraceProjAction(int projectileID = 0)
        {
            this.projectileID = projectileID;
        }

        public int projectileID;

        void IAction.CastAction(Spell spell)
        {
            var mOwner = spell.speller as IModifyOwner<float>;
            if (mOwner == null)
            {
                XLogger.Instance.Log("owner null");
                return;
            }
            if (!mOwner.Modifyables.ContainsValueWith("attackRange"))
            {
                XLogger.Instance.Log("no attack range");
                return;
            }
            float attackRange = mOwner.Modifyables["attackRange"].Value;
            Vector3 center = new Vector3(spell.speller.Position.x, 0, spell.speller.Position.z);

            var target = TargetSetFactorary.GetTargetSet("Common").NearestTarget(center, attackRange);
            if (target == null)
            {
                XLogger.Instance.Log($"pos:{center}, no target");
                return;
            }

            var pOwner = spell.speller as IProjectileOwner;

            var e = Constructor.Projectiles.Tracer1.Instance.Get(projectileID);
            e.flying.Src = pOwner.HandPosition + new Vector3(0, 1, 0);
            e.flying.dest = CameraSys.MouseHitPosition;
            e.owner = pOwner;
            e.target = target;
        }
    }
}
