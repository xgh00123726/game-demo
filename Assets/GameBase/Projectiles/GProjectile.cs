using GameBase.Tools;
using GameBase.Object;
using UnityEngine;
using GameBase.Resources;

namespace GameBase.Projectile
{
    public class GProjectile : MonoBehaviour,
        IPoolableObject
    {
        public delegate bool ReleaseCallback();
        protected virtual void OnHit() { }
        protected virtual void OnEmit() { }
        protected virtual void OnMove() { }
        protected virtual void OnRelease() { }

        public ReleaseCallback ReleaseCondition;
        protected Vector3 _src;               // 源位置
        protected Vector3 _dest;              // 目标位置
        protected bool _hasTarget = false;    // 是否具有目标对象
        protected IProjectileTarget _target;  // 目标对象
        public IProjectileTarget Target
        {
            get => _target;
            set
            {
                _target = value;
                _hasTarget = true;
            }
        }
        public Vector3 Dest => _hasTarget ? _target.Center : _dest;
        public float _instantiateTime = 0f; // 出生时刻
        public float _releaseTime = 10f;    // 最大持续时间
        public float _releaseDis = 10f;     // 最大运动距离
        public float _equalConst = 0.01f;   // 相等常数，当与目标距离小于这个常数则认为相等
        public float _disToTarget = 0f;


        internal bool CanRelease()
        {
            if (ReleaseCondition?.Invoke() == true) return true;
            
            if (Time.time > _releaseTime + _instantiateTime) return true;

            _disToTarget = (Dest - transform.position).magnitude;
            if (_disToTarget < _equalConst) return true;

            return false;
        }

        public void SetTrack(Vector3 src, Vector3 dest)
        {
            _src = src;
            _dest = dest;
            OnEmit();
        }

        internal virtual void _Update()
        {
            
        }

        void IPoolableObject.OnInstantiate()
        {
            gameObject.SetActive(true);
            _instantiateTime = Time.time;
            _src = transform.position;
        }

        void IPoolableObject.OnRelease()
        {
            OnRelease();
            gameObject.SetActive(false);
        }
    }
}
