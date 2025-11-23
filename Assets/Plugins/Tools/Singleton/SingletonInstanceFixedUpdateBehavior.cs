using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    public class SingletonInstanceFixedUpdateBehavior : MonoBehaviour
    {
        public List<EnableAction> actions = new();
        internal static SingletonInstanceFixedUpdateBehavior instance;

        internal static int RegisterUpdate(Action action, string name)
        {
            int ret = instance.actions.Count;
            instance.actions.Add(new EnableAction()
            {
                action = action,
                name = name,
                enable = true
            });
            return ret;
        }

        internal static void SetActive(int index, bool active)
        {
            instance.actions[index].enable = active;
        }
        internal static bool ActiveSelf(int index)
        {
            return instance.actions[index].enable;
        }
        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i].enable)
                {
                    actions[i].action?.Invoke();
                }
            }
        }
    }
}