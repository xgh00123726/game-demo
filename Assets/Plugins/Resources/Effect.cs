using GameBase.EntitySystem;
using UnityEngine;
namespace GameBase.Resources
{
    public class Effect : IKeyEntity<string>
    {
        internal float duration;
        internal float instantiateTime;

        internal ParticleSystem particle;
        public string Key {  get; set; }
    }
}
