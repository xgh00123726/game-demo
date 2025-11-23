using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    [Serializable]
    public class SingletonEntitySysInfo
    {
        public IBaseSys sys;
        public string name;
        public bool enable;
        public int entityCount;
        public int activeCount;
        public int releasedCount;
    }
    public class SingletonEntitySysInstance : MonoBehaviour
    {
        internal static SingletonEntitySysInstance instance;
        public List<SingletonEntitySysInfo> SysInstance = new();

        public static void CreateShadowMono<T>(T sys) where T : IBaseSys
        {
            instance.SysInstance.Add(new()
            {
                sys = sys,
                name = sys.GetType().Name,
                enable = true,
            });
        }

        private void Update()
        {
            for (int i = 0; i < SysInstance.Count; i++)
            {
                var instance = SysInstance[i];
                if (instance.enable)
                {
                    instance.sys.Update();
                }
                var sys = instance.sys;

                instance.activeCount = sys.GetActiveCount();
                instance.entityCount = sys.GetEntityCount();
                instance.releasedCount = sys.GetReleasedCount();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < SysInstance.Count; i++)
            {
                var instance = SysInstance[i];
                if (instance.enable)
                {
                    instance.sys.FixedUpdate();
                }
            }
        }

        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
