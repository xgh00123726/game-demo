using UnityEngine;

namespace GameBase.Object
{
    public partial class MoveComponent : GComponent
    {
        public bool usePhysic = true;
        private Rigidbody _rigidbody;             // ◊‘…Ì∏’ÃÂ

        private Ray _hRay;
        private RaycastHit _hRayHit;

        private void MovePositionUsePhysic(Vector3 position)
        {
            _rigidbody.MovePosition(position);
        }

        private void MovePositionWithoutPhysic(Vector3 position)
        {
            transform.position = position;
        }

        public void MovePosition(Vector3 position)
        {
            if (float.IsNaN(position.z) || float.IsNaN(position.y) || float.IsNaN(position.z)) return;

            if (usePhysic)
            {
                MovePositionUsePhysic(position);
            }
            else
            {
                MovePositionWithoutPhysic(position);
            }
        }
    }
}
