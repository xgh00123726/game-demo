using System;
using UnityEngine;

namespace GameBase.EntitySystem
{
    public class ShadowMono : MonoBehaviour
    {
        private IBaseSys _baseSys;

        public int entityCount;
        public int activeCount;
        public int releasedCount;

        public static void CreateShadowMono<T>(T instance) where T : IBaseSys
        {
            var shadowMono = new GameObject(instance.GetType().Name).AddComponent<ShadowMono>();
            shadowMono._baseSys = instance;
        }

        private void Update()
        {
            _baseSys.Update();
            entityCount = _baseSys.GetEntityCount();
            activeCount = _baseSys.GetActiveCount();
            releasedCount = _baseSys.GetReleasedCount();
        }

        private void FixedUpdate()
        {
            _baseSys.FixedUpdate();
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
