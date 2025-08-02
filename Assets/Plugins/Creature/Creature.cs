using GameBase.Projectile;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creature
{
    public class Creature : IUEntity<GameObject>,
        IPoolableObject,
        IProjectileTarget
    {
        public int bodyID;
        public int radius;
        public Vector3 genPos;

        internal int id;
        internal GameObject body;

        Vector3 IProjectileTarget.Center => body.transform.position;

        float IProjectileTarget.Radius => radius;

        public int ID
        {
            get => id;
            set => id = value;
        }

        public GameObject Obj
        {
            get => body;
            set => body = value;
        }
        public int ObjID => bodyID;

        protected virtual void OnGetDamage(float damage)
        {

        }

        void IProjectileTarget.GetDamage(float damage)
        {
            OnGetDamage(damage);
        }

        void IPoolableObject.OnInstantiate()
        {
            
        }

        void IPoolableObject.OnRelease()
        {
            
        }
    }
}
