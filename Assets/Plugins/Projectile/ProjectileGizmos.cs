using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectiles
{
    public class ProjectileGizmos : MonoBehaviour
    {
        public bool isInit = false;
        public bool drawGizmos = false;
        public static ProjectileSys sysInstance;
        public static ProjectileGizmos Instance => _instance;
        public static ProjectileGizmos _instance;

        private void Init()
        {
            sysInstance = ProjectileSys.Instance;
        }

        public void ToggleShow()
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
                GizmosAppend.DrawShape(e.shape, -7);
            }
        }
    }
}
