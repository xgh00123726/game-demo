using GameBase.Tools;
using GameBase.EntitySystem;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.Effects
{
    public class EffectSys : KeyEntitySys<int, Effect, EffectSys>
    {
        protected override void OnGet(Effect e)
        {
            e.instantiateTime = Time.time;
            e.particle.gameObject.SetActive(true);

            e.particle.Play();
            e.isExist = true;
        }

        protected override void OnRelease(Effect e)
        {
            e.isExist = false;
            e.particle.gameObject.SetActive(false);
        }

        protected override Effect CtorT(int k)
        {
            Effect e = new();
            var particleObj = GameObject.Instantiate(ResourcesLoader.GetPrefab(k));
            e.particle = particleObj.GetComponent<ParticleSystem>();
            return e;
        }

        protected override void UpdateEntity(Effect e)
        {
            if (Time.time > e.instantiateTime + e.existTime)
            {
                RemoveEntity(e);
            }
        }
    }
}
