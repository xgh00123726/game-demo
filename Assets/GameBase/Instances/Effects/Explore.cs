using System.Collections.Generic;
using System.Linq;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.Effects
{
    public class Explore : PoolablePrefab
    {
        private ParticleSystem _particle;
        internal string prefabName = null;

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
