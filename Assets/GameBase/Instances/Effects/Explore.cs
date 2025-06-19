using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Effects
{
    public class Explore : PoolablePrefab
    {
        private ParticleSystem _particle;

        public override bool ReleaseTrigger => (_particle.time >= 1.5f || !_particle.isPlaying) && gameObject.activeSelf;
        protected override void OnInstantiate()
        {
            gameObject.SetActive(true);
            _particle.Simulate(0f);
            _particle.Play();
        }
        protected override void OnRelease()
        {
            gameObject.SetActive(false);
        }

        void Awake()
        {
            _particle = gameObject.GetComponent<ParticleSystem>();
        }
    }
}
