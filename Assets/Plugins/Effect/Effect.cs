using GameBase.EntitySystem;
using UnityEngine;
namespace GameBase.Effects
{
    public class Effect : IUEntity<ParticleSystem>
    {
        public float existTime;

        internal float instantiateTime;
        internal bool hasParticle;

        public Vector3 Position
        {
            set
            {
                if (hasParticle)
                {
                    Obj.gameObject.transform.position = value;
                }
            }
        }
        public ParticleSystem Obj { get; set; }
        public int ObjID {  get; set; }
        public int InstanceID {  get; set; }
    }
}
