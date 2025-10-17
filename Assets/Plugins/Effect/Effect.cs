using GameBase.EntitySystem;
using UnityEngine;
namespace GameBase.Effects
{
    public class Effect : IKeyEntity<int>
    {
        public float existTime;

        internal float instantiateTime;
        internal bool hasParticle;

        public ParticleSystem particle;
        public int Key {  get; set; }
    }
}
