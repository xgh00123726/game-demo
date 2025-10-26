using GameBase.EntitySystem;
using UnityEngine;
namespace GameBase.Effects
{
    public class Effect : IKeyEntity<int>
    {
        public float existTime;

        internal float instantiateTime;
        internal bool hasParticle;
        internal bool isExist;

        public ParticleSystem particle;
        public bool IsExist => isExist;
        public int Key {  get; set; }
    }
}
