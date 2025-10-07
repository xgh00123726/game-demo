using GameBase.Math;
using GameBase.Projectiles;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creatures
{
    public class CreatureGizmosDraw : MonoBehaviour
    {
        public static bool isInit = false;
        public static bool drawGizmos = false;
        public static int drawTimes = 10;
        public static float forceLenTimes = 5;
        public static bool drawForce = true;
        public static float drawY = -7;

        public bool healthBarDebugMode;
        public Vector3 healthbarOffset;

        public static CreatureSys sysInstance;
        private static CreatureGizmosDraw _instance;
        public static CreatureGizmosDraw Instance => _instance;

        private static void Init()
        {
            sysInstance = CreatureSys.Instance;
        }

        public static void ToggleShow()
        {
            if (!isInit)
            {
                Init();
                isInit = true;
            }

            drawGizmos = !drawGizmos;
        }

        private void Awake()
        {
            _instance = this;
            Command.Register("toggle-creature-gizmos", ToggleShow);
        }

        private void OnDestroy()
        {
            drawGizmos = false;
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos) 
            {
                return;
            }
            foreach (var e in sysInstance.Entities)
            {
                if (e.collider == null)
                {
                    continue;
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

                if (drawForce && e.collider.isCollide)
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
        }
    }
}
