using System;
using System.Collections;
using System.Collections.Generic;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Effects
{
    internal class MoveIndicatorEffect : PoolablePrefab
    {
        internal ParticleSystem _particle;

        public override bool ReleaseTrigger => (_particle.time >= 1.5f || !_particle.isPlaying) && gameObject.activeSelf;
        public bool releaseTrigger;
        public float particleTime;
        public bool activeSelf;
        public bool isPlaying;
        public bool isPaused;

        protected override void OnInstantiate()
        {
            gameObject.SetActive(true);
        }

        protected override void OnRelease()
        {
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            _particle = gameObject.GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            releaseTrigger = ReleaseTrigger;
            particleTime = _particle.time;
            activeSelf = gameObject.activeSelf;
            isPaused = _particle.isPaused;
            isPlaying = _particle.isPlaying;
        }

        public void PlayAt(Vector3 pos)
        {
            gameObject.transform.position = pos;
            _particle.Simulate(0f);
            _particle.Play();
        }
    }
}
