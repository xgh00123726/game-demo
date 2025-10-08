using GameBase.Tools;
using UnityEngine;

namespace GameBase.Triggers
{
    public class TriggerGizmos : MonoBehaviour
    {
        public static bool isInit = false;
        public static bool drawGizmos = false;
        public static float drawY = -7;
        public static TriggerSys sysInstance;

        private static void Init()
        {
            sysInstance = TriggerSys.Instance;
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
            Command.Register("toggle-projectile-gizmos", ToggleShow);
        }

        void OnDrawGizmos()
        {
            if (!drawGizmos)
            {
                return;
            }
            foreach (var e in sysInstance.Entities)
            {
                Gizmos.color = Color.green;
                GizmosAppend.DrawShape(e.shape, drawY);
            }
        }
    }
}
