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

        public override bool ReleaseTrigger => (_particle.time >= particleLifeTime || !_particle.isPlaying) && gameObject.activeSelf;
        public bool releaseTrigger;
        public float particleTime;
        public bool activeSelf;
        public bool isPlaying;
        public bool isPaused;
        public Vector3 beginOffset;
        public Vector3 endOffset;
        public float particleLifeTime = 1f;
        public Vector3 positionSet;

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
            particleLifeTime = _particle.main.startLifetime.constant;
        }

        private void Update()
        {
            releaseTrigger = ReleaseTrigger;
            particleTime = _particle.time;
            activeSelf = gameObject.activeSelf;
            isPaused = _particle.isPaused;
            isPlaying = _particle.isPlaying;
            transform.position = positionSet + Vector3.Lerp(beginOffset, endOffset, particleTime / particleLifeTime);
        }

        public void PlayAt(Vector3 pos)
        {
            gameObject.transform.position = pos;
            positionSet = pos;
            _particle.Simulate(0f);
            _particle.Play();
        }
    }
}
