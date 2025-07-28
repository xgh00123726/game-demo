using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;
using Logger = GameBase.Tools.Logger;

namespace GameBase.Projectile
{
    public partial class ProjectileObject : MonoBehaviour,
        IPoolableObject,
        ICurveProjectile
    {
        public delegate void HitTargetEventCallback(IProjectileTarget target);

        protected virtual void OnRelease() { }
        protected virtual void OnInstantiate() { }

        private Vector3 _src;                 // 源位置
        public Vector3 _dest;                // 目标位置
        protected bool _hasTarget = false;      // 是否具有目标对象
        protected IProjectileTarget _target;    // 目标对象
        public float damage;                    // 伤害
        public bool isImmediatly = false;   // 是否瞬间型射弹
        public float _instantiateTime = 0f; // 出生时刻
        public float _releaseTime = 10f;    // 最大持续时间
        public float _releaseDis = 10f;     // 最大运动距离
        public float _equalConst = 0.01f;   // 相等常数，当与目标距离小于这个常数则认为相等
        public float _disToTarget = 0f;     // 到目标的距离
        public bool _canRelease = false;
        
        public bool hasOwner = false;
        public HitTargetEventCallback HitTargetEventAction;

        public bool CanRelease
        {
            get => _canRelease;
        }

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

        public Vector3 Src
        {
            get => _src;
            set
            {
                _src = value;
                transform.position = _src;
            }
        }

        public Vector3 Dest
        {
            set => _dest = value;
            get => _hasTarget ? _target.Center : _dest;
        }
        Vector3 ICurveProjectile.Position
        {
            get => transform.position;
            set => transform.position = value;
        }
        Vector3 ICurveProjectile.Dir
        {
            get => Dir;
            set => Dir = value;
        }
        Vector3 ICurveProjectile.Dest => Dest;

        Vector3 ICurveProjectile.Src => Src;

        float ICurveProjectile.LifeTime => LifeTime;

        protected void HitTarget(IProjectileTarget target)
        {
            target.GetDamage(damage);
            HitTargetEventAction?.Invoke(target);
        }

        protected virtual void BeforeHit() { }

        /// <summary><list type="bullet">
        /// <item>技能击中时，对目标造成伤害</item>
        /// <item>target表示技能的主目标或单体目标</item>
        /// <item>targets表示可能存在多个目标的技能的多目标</item>
        /// </list></summary>
        protected virtual void OnHit() 
        {
            BeforeHit();
            if (_hasTarget)
            {
                HitTarget(_target);
            }
        }

        void JugRelease()
        {
            if (isImmediatly)
            {
                transform.position = Dest;
                OnHit();
                _canRelease = true;
                return;
            }
            
            if (Time.time > _releaseTime + _instantiateTime)
            {
                _canRelease = true;
                return;
            }

            _disToTarget = (Dest - transform.position).magnitude;
            if (_disToTarget < _equalConst)
            {
                OnHit();
                _canRelease = true;
                return;
            }

            _canRelease = false;
        }

        internal virtual void _Update()
        {
            CurveUpdate();
            JugRelease();
        }

        void IPoolableObject.OnInstantiate()
        {
            OnInstantiate();
            _whites.Clear();
            gameObject.SetActive(true);
            _instantiateTime = Time.time;
        }

        void IPoolableObject.OnRelease()
        {
            OnRelease();
            gameObject.SetActive(false);
        }
    }
}
