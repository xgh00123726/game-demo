using GameBase.AI;
using GameBase.Math;
using GameBase.Triggers;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creatures
{
    public class CreatureGizmosDraw : MonoBehaviour
    {
        public static bool isInit = false;
        public static float drawY = -7;

        public static int drawTimes = 10;

        public static bool isDrawGizmos = false;
        public static bool isDrawCollider = true;
        public static bool isDrawMove = true;
        public static bool isDrawForce = false;
        public static bool isDrawAI = false;
        public static bool isDrawAttackRange = true;

        public static float forceLenTimes = 5;

        public static CreatureSys sysInstance;
        private static CreatureGizmosDraw _instance;
        public static CreatureGizmosDraw Instance => _instance;

        public static void Init()
        {
            if (isInit)
            {
                return;
            }
            isInit = true;
            sysInstance = CreatureSys.Instance;
        }

        private void Awake()
        {
            _instance = this;
            Command.Register("toggle-creature-gizmos", () =>
            {
                isDrawGizmos = !isDrawGizmos;
            });
            Command.Register("toggle-draw-force", () =>
            {
                isDrawForce = !isDrawForce;
            });
            Command.Register("toggle-draw-collide", () =>
            {
                isDrawCollider = !isDrawCollider;
            });
            Command.Register("toggle-draw-move", () =>
            {
                isDrawMove = !isDrawMove;
            });
        }

        private void OnDestroy()
        {
            isDrawGizmos = false;
        }

        private void DrawCollide(Creature e)
        {
            if (!isDrawCollider)
            {
                return;
            }
            if (e.collider == null)
            {
                return;
            }
            if (e.collider.isCollide)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            GizmosAppend.DrawCircle(new GMath.Circle
            {
                c = new Vector2(e.collider.position.x, e.collider.position.y),
                r = e.collider.r,
            }, drawY);

            if (isDrawForce && e.collider.isCollide)
            {
                Vector3 c = new Vector3(e.collider.position.x, drawY, e.collider.position.y);
                Vector3 dir = new Vector3(e.collider.force.x, 0, e.collider.force.y) * forceLenTimes;

                Gizmos.color = new Color(0.85f, 0.29f, 0.77f, 1f);
                for (int i = 0; i < drawTimes; ++i)
                {
                    Gizmos.DrawLine(c, c + dir);
                }
            }

            Gizmos.color = Color.green;
        }

        private void DrawMove(Creature e)
        {
            
            if (!isDrawMove)
            {
                return;
            }

            if (e.mover.IsMoving)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.white;
            }
            GizmosAppend.DrawCircle(new GMath.Circle
            {
                c = new Vector2(e.mover.owner.Position.x, e.mover.owner.Position.z),
                r = e.radius,
            }, drawY);
        }

        private void DrawAttackRange(Creature e)
        {
            if (!isDrawAttackRange)
            {
                return;
            }

            Gizmos.color = Color.white;
            GizmosAppend.DrawCircle(new GMath.Circle
            {
                c = new Vector2(e.mover.owner.Position.x, e.mover.owner.Position.z),
                r = e.modifyables["attackRange"].Value,
            }, drawY);
        }

        private void DrawFollowAttackAI(Creature e)
        {
            if (!isDrawAI)
            {
                return;
            }

            if (e.ai is AIFollowAttack ai)
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
                    c = new Vector2(e.mover.owner.Position.x, e.mover.owner.Position.z),
                    r = e.radius,
                }, drawY);

                Gizmos.color = Color.yellow;
                GizmosAppend.DrawCircle(new GMath.Circle
                {
                    c = new Vector2(e.mover.owner.Position.x, e.mover.owner.Position.z),
                    r = ai.arriveDis,
                }, drawY);

                if (e.HasPossibleAttr("FollowRange"))
                {
                    Gizmos.color = Color.white;
                    GizmosAppend.DrawCircle(new GMath.Circle
                    {
                        c = new Vector2(e.mover.owner.Position.x, e.mover.owner.Position.z),
                        r = e.GetPossibleAttr("FollowRange"),
                    }, drawY);
                }
            }
        }

        void OnDrawGizmos()
        {
            if (!isDrawGizmos) 
            {
                return;
            }
            foreach (var e in sysInstance.Entities)
            {
                DrawCollide(e);
                DrawMove(e);
                DrawFollowAttackAI(e);
                DrawAttackRange(e);
            }
        }
    }
}
