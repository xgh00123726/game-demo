using System;
using System.Collections.Generic;
using UnityEngine;
namespace GameBase.EntitySystem
{
    [Serializable]
    public class EnableAction
    {
        public Action action;
        public string name;
        public bool enable;
    }

    public class SingletonInstanceUpdateBehavior : MonoBehaviour
    {
        public List<EnableAction> actions = new();
        internal static SingletonInstanceUpdateBehavior instance;

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
        private void Awake()
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
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