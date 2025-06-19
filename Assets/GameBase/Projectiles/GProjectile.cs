using GameBase.Tools;
using GameBase.Object;
using UnityEngine;
using GameBase.Resources;

namespace GameBase.Projectile
{
    public class GProjectile : PoolablePrefab,
        IPoolableObject
    {
        protected virtual void OnHit() { }
        protected virtual void OnEmit() { }
        protected virtual void OnMove() { }
        protected virtual void OnStop() { }

        public Vector3 _dest;
        public Vector3 Dest
        {
            get => _dest;
            set
            {
                _dest = value;
                _cruiseComponent.StartTrack(_dest);
                OnEmit();
            }
        }

        private CruiseComponent _cruiseComponent;
        public CuriseBase Curise { set => _cruiseComponent.Curise = value; }

        public override bool ReleaseTrigger => !_cruiseComponent.InTrack && gameObject.activeSelf;


        protected void Awake()
        {
            _cruiseComponent = GetComponent<CruiseComponent>();
            _cruiseComponent.OnTrackEnd = OnStop;
        }

        

        void IPoolableObject.OnInstantiate()
        {
            gameObject.SetActive(true);
        }

        void IPoolableObject.OnRelease()
        {
            gameObject.SetActive(false);
        }
    }
}
