using GameBase.Tools;
using GameBase.EntitySystem;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.Effects
{
    public class EffectSys : UObjEntitySys<Effect, ParticleSystem, EffectSys>
    {
        protected override void AfterInstantiateEUObject(Effect e)
        {
            e.instantiateTime = Time.time;
            e.Obj.gameObject.SetActive(true);

            e.Obj.Play();
        }

        protected override void BeforeReleaseEUObject(Effect e)
        {
            e.Obj.gameObject.SetActive(false);
        }

        protected override ParticleSystem InstantiateObj(Effect e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            var particle = obj.GetComponent<ParticleSystem>();
            if (particle == null)
            {
                XLogger.Instance.Log($"this prefab has no particle, id:{e.ObjID}");
            }

            return particle;
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
