using GameBase.Tools;
using UnityEngine;
using Logger = GameBase.Tools.Logger;

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
    public partial class ProjectileObject : MonoBehaviour,
        IPoolableObject
    {
        
        protected virtual void OnRelease() { }

        public Vector3 _src;                 // 源位置
        public Vector3 _dest;                // 目标位置
        protected bool _hasTarget = false;      // 是否具有目标对象
        protected IProjectileTarget _target;    // 目标对象
        protected DestoryReson _destoryReson;     // 摧毁原因
        public float damage;                    // 伤害
        public float radius = 1f;            // 影响范围
        public bool isImmediatly = false;   // 是否瞬间型技能
        public float _instantiateTime = 0f; // 出生时刻
        public float _releaseTime = 10f;    // 最大持续时间
        public float _releaseDis = 10f;     // 最大运动距离
        public float _equalConst = 0.01f;   // 相等常数，当与目标距离小于这个常数则认为相等
        public float _disToTarget = 0f;     // 到目标的距离
        public bool canRelease = false;
        public bool hasOwner = false;


        internal IProjectileOwner _owner;
        public IProjectileOwner Owner
        {
            get => _owner;
            set
            {
                if (value == null) return;

                hasOwner = true;
                _owner = value;
            }
        }

        public DestoryReson DestoryReson => _destoryReson;

        public IProjectileTarget Target
        {
            get => _target;
            set
            {
                if (value == null) return;

                _target = value;
                _hasTarget = true;
            }
        }
        public Vector3 Dest
        {
            set => _dest = value;
            get => _hasTarget ? _target.Center : _dest;
        }


        protected virtual void OnHit() 
        {
            if (_hasTarget)
            {
                _target.GetDamage(damage);
            }
        }

        void JugRelease()
        {
            if (isImmediatly)
            {
                _destoryReson = DestoryReson.Hit;
                OnHit();
                canRelease = true;
                return;
            }
            
            if (Time.time > _releaseTime + _instantiateTime)
            {
                _destoryReson = DestoryReson.TimeOut;
                canRelease = true;
                return;
            }

            _disToTarget = (Dest - transform.position).magnitude;
            if (_disToTarget < _equalConst)
            {
                _destoryReson = DestoryReson.Hit;
                OnHit();
                canRelease = true;
                return;
            }

            canRelease = false;
        }

        internal virtual void _Update()
        {
            CurveUpdate();
            JugRelease();
        }

        void IPoolableObject.OnInstantiate()
        {
            gameObject.SetActive(true);
            _instantiateTime = Time.time;
            _src = transform.position;
            _destoryReson = DestoryReson.UnDestroy;
        }

        void IPoolableObject.OnRelease()
        {
            OnRelease();
            gameObject.SetActive(false);
        }
    }
}
