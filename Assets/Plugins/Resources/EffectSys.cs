using GameBase.Tools;
using GameBase.EntitySystem;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.Resources
{
    public class EffectSys : KeyEntitySys<string, Effect, EffectSys>
    {
        protected override void OnGet(Effect e)
        {
            e.instantiateTime = Time.time;
            e.particle.gameObject.SetActive(true);

            e.particle.Play();
        }

        protected override void OnRelease(Effect e)
        {
            e.particle.gameObject.SetActive(false);
        }

        protected override Effect CtorT(string k)
        {
            Effect e = new();
            var particleObj = GameObject.Instantiate(ResourcesLoader.LoadAddressable<GameObject>(k));
            e.particle = particleObj.GetComponent<ParticleSystem>();
            e.duration = e.particle.main.duration;
            return e;
        }

        protected override void UpdateEntity(Effect e)
        {
            if (Time.time > e.instantiateTime + e.duration)
            {
                RemoveEntity(e);
            }
        }

        public void PlayAtP(string filePath, Vector3 position)
        {
            PlayAtPSD(filePath, position, Vector3.one, Vector3.forward);
        }

        public void PlayAtPS(string filePath, Vector3 position, Vector3 scale)
        {
            PlayAtPSD(filePath, position, scale, Vector3.forward);
        }

        public void PlayAtPSD(string filePath, Vector3 position, Vector3 scale, Vector3 dir)
        {
            if (filePath == null || filePath.Length <= 1)
            {
                return;
            }
            var e = NewEntity(filePath);
            e.particle.transform.position = position;
            e.particle.transform.localScale = scale;
            e.particle.transform.forward = dir;
        }
    }
}
