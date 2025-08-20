using System;
using UnityEngine;

namespace GameBase.EntitySystem
{
    public class ShadowMono : MonoBehaviour
    {
        private Action _Awake;
        private Action _Update;

        private Func<int> _EntityCountGetterDelegate;
        private Func<int> _EntityReleasedCountGetterDelegate;
        private Func<int> _EntityActiveCountGetterDelegate;


        public int entityCount;
        public int activeCount;
        public int releasedCount;

        public static void CreateShadowMono<T>(T instance) where T : IBaseSys
        {
            var shadowMono = new GameObject(typeof(T).Name).AddComponent<ShadowMono>();
            instance.Awake();
            shadowMono._Update = instance.Update;
            shadowMono._EntityCountGetterDelegate = instance.GetEntityCount;
            shadowMono._EntityReleasedCountGetterDelegate = instance.GetReleasedCount;
            shadowMono._EntityActiveCountGetterDelegate = instance.GetActiveCount;
        }

        private void Update()
        {
            _Update?.Invoke();
            entityCount = _EntityCountGetterDelegate();
            activeCount = _EntityActiveCountGetterDelegate();
            releasedCount = _EntityReleasedCountGetterDelegate();
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
