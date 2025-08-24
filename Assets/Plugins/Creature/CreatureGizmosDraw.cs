using UnityEngine;

namespace GameBase.Creatures
{
    public class CreatureGizmosDraw : MonoBehaviour
    {
        public bool healthBarDebugMode = false;
        public Vector3 healthbarOffset;
        private static CreatureGizmosDraw _instance;
        public static CreatureGizmosDraw Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }
    }
}
