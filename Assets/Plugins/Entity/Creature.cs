using GameBase.Projectile;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creature
{
    public class Creature : IEntity<GameObject>,
        IPoolableObject,
        IProjectileTarget
    {
        public int bodyID;
        public int radius;
        public Vector3 genPos;

        internal GameObject body;

        Vector3 IProjectileTarget.Center => body.transform.position;

        float IProjectileTarget.Radius => radius;

        public GameObject Obj
        {
            get => body;
            set => body = value;
        }
        public int ID => bodyID;

        void IProjectileTarget.GetDamage(float damage)
        {
            
        }

        void IPoolableObject.OnInstantiate()
        {
            
        }

        void IPoolableObject.OnRelease()
        {
            
        }
    }
}
