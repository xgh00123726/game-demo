using GameBase.Tools;
using UnityEngine;

namespace GameBase.Projectile
{
    public enum DestoryReson
    {
        UnDestroy,
        Trigger,
        Hit,
        TimeOut,
        DistanceOut
    }
    public class Projectile : MonoBehaviour,
        IPoolableObject
    {
        public delegate bool ReleaseTrigger();
        protected virtual void OnHit() { }
        protected virtual void OnEmit() { }
        protected virtual void OnMove() { }
        protected virtual void OnRelease() { }

        private SequentialBool _emitFlag = new SequentialBool(); // Emit标志位
        public ReleaseTrigger ReleaseCondition; // 外部释放（摧毁）触发
        protected Vector3 _src;                 // 源位置
        protected Vector3 _dest;                // 目标位置
        protected bool _hasTarget = false;      // 是否具有目标对象
        protected IProjectileTarget _target;    // 目标对象
        private DestoryReson _destoryReson;     // 摧毁原因
        public float damage;                    // 伤害

        internal IProjectileOwner _owner;
        public IProjectileOwner Owner => _owner;
        public DestoryReson DestoryReson => _destoryReson;

        public IProjectileTarget Target
        {
            get => _target;
            set
            {
                _target = value;
                _hasTarget = true;
            }
        }
        public Vector3 Dest
        {
            set => _dest = value;
            get => _hasTarget ? _target.Center : _dest;
        }
        public float _instantiateTime = 0f; // 出生时刻
        public float _releaseTime = 10f;    // 最大持续时间
        public float _releaseDis = 10f;     // 最大运动距离
        public float _equalConst = 0.01f;   // 相等常数，当与目标距离小于这个常数则认为相等
        public float _disToTarget = 0f;     // 到目标的距离


        internal bool JugRelease()
        {
            if (ReleaseCondition?.Invoke() == true)
            {
                _destoryReson = DestoryReson.Trigger;
                return true;
            }
            
            if (Time.time > _releaseTime + _instantiateTime)
            {
                _destoryReson = DestoryReson.TimeOut;
                return true;
            }

            _disToTarget = (Dest - transform.position).magnitude;
            if (_disToTarget < _equalConst)
            {
                _destoryReson = DestoryReson.Hit;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 默认在射弹击中时造成伤害
        /// <list type="bullet">
        /// <item><param name="reson"><paramref name="reson"/>:被摧毁的原因</param></item>
        /// </list></summary>
        internal protected virtual void ReactToDestroyState(DestoryReson reson)
        {
            if (reson == DestoryReson.Hit && _hasTarget)
            {
                _target.GetDamage(damage);
            }
        }

        internal virtual void _Update()
        {
            if (_emitFlag.EdgeRising) OnEmit();
        }

        void IPoolableObject.OnInstantiate()
        {
            gameObject.SetActive(true);
            _instantiateTime = Time.time;
            _src = transform.position;
            _destoryReson = DestoryReson.UnDestroy;
            _emitFlag.Set();
        }

        void IPoolableObject.OnRelease()
        {
            OnRelease();
            _emitFlag.Reset();
            gameObject.SetActive(false);
        }
    }
}
