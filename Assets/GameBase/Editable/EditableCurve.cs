using UnityEngine;

namespace GameBase.Editable
{
    public class EditableCurve : MonoBehaviour
    {
        public AnimationCurve curve;
        public Vector3 beginPos;
        public Vector3 endPos;

        private void Awake()
        {
        }

        private void OnDrawGizmosSelected()
        {
            Vector3 from = beginPos + transform.position;
            Vector3 to = endPos + transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(from, 0.5f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(to, 0.5f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(from, to);
        }
    }
}
