using GameBase.AI;
using GameBase.Math;
using GameBase.Triggers;
using GameBase.Tools;
using UnityEngine;
using Instance;

namespace GameBase.Creatures
{
    public class CreatureGizmos : MonoBehaviour
    {
        private void OnDestroy()
        {
            GizmosCfg.IsDrawGizmos = false;
        }

        private void DrawMove(Creature e)
        {
            if (!GizmosCfg.IsDrawMove)
            {
                return;
            }

            if (!e.Mover.IsMoving)
            {
                return;
            }
            if (e.Mover.DestQueue.Count == 0)
            {
                return;
            }

            Gizmos.color = Color.green;

            Vector3 from = e.Position;
            Vector3 to = e.Mover.Dest;
            GizmosAppend.DrawLine(from, to, 5);
            foreach (var pos in e.Mover.DestQueue)
            {
                from = to;
                to = pos;
                GizmosAppend.DrawLine(from, to, 5);
            }
        }

        private void DrawAttackRange(Creature e)
        {
            if (!GizmosCfg.IsDrawAttackRange)
            {
                return;
            }

            Gizmos.color = Color.white;
            GizmosAppend.DrawCircle(new GMath.Circle
            {
                c = new Vector2(e.Mover.Owner.Position.x, e.Mover.Owner.Position.z),
                r = e.Modifyables["attackRange"].Value,
            }, GizmosCfg.drawY);
        }

        private void DrawFollowAttackAI(Creature e)
        {
            if (!GizmosCfg.IsDrawAI)
            {
                return;
            }

            if (e.AI is AIFollowAttack ai)
            {
                if (ai.target != null)
                {
                    Gizmos.color = Color.green;
                }
                else if (ai.target == null && ai.mover.IsMoving)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                GizmosAppend.DrawCircle(new GMath.Circle
                {
                    c = new Vector2(e.Mover.Owner.Position.x, e.Mover.Owner.Position.z),
                    r = e.Radius,
                }, GizmosCfg.drawY);

                Gizmos.color = Color.yellow;
                GizmosAppend.DrawCircle(new GMath.Circle
                {
                    c = new Vector2(e.Mover.Owner.Position.x, e.Mover.Owner.Position.z),
                    r = ai.arriveDis,
                }, GizmosCfg.drawY);

                if (e.HasPossibleAttr("FollowRange"))
                {
                    Gizmos.color = Color.white;
                    GizmosAppend.DrawCircle(new GMath.Circle
                    {
                        c = new Vector2(e.Mover.Owner.Position.x, e.Mover.Owner.Position.z),
                        r = e.GetPossibleAttr("FollowRange"),
                    }, GizmosCfg.drawY);
                }
            }
        }

        void OnDrawGizmos()
        {
            if (!GizmosCfg.IsDrawGizmos)
            {
                return;
            }
            foreach (var e in CreatureSys.Instance.Entities)
            {
                DrawMove(e);
                DrawFollowAttackAI(e);
                DrawAttackRange(e);
            }
        }
    }
}
