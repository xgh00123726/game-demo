using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Tools
{
    public class Eventer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            foreach (var action in _updateActions)
            {
                action();
            }
        }

        static internal void AddUpdate(Action action)
        {
            _updateActions.Add(action);
        }

        static private List<Action> _updateActions = new List<Action>();



    }
}
