using GameBase.Math;
using UnityEngine;

namespace GameBase.Object
{
    public partial class MoveComponent : GComponent
    {
        public RotateComponent _rotateComponent;  // 旋转组件
        public bool _hasRotateComponent;          // 是否具有旋转组件
        public Vector3 _targetDir;                // 转向目标方向
        public float TurnMoveSpeedFactor       // 转向移动时的移动速度系数
        {
            get
            {
                if (!Inturning) return 1;
                return Mathf.Clamp(1 - GMath.AngleOfLines2D(transform.forward, _targetDir, GMath.Axis.Y) / Mathf.PI, 0.2f, 0.9f);
            }
        }      

        public bool Inturning
        {
            get
            {
                if (!_hasRotateComponent)
                {
                    return false;
                }
                return _rotateComponent.InRotating;
            }
        }

        private void MoveToWithRotate(Vector3 position)
        {
            _isMovingDest = true;
            _targetDir = _moveDest - transform.position;
            float rotateAngle = GMath.AngleOfLines2D(transform.forward, _targetDir, GMath.Axis.Y) * Mathf.Rad2Deg;
            float cross = transform.forward.x * _targetDir.z - transform.forward.z * _targetDir.x;
            if (cross < 0)
            {
                _rotateComponent.RotateY(rotateAngle);
            }
            else
            {
                _rotateComponent.RotateY(-rotateAngle);
            }
        }
    }
}
