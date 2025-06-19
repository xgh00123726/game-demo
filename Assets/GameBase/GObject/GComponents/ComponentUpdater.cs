using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Object
{
    public class Actions
    {
        private List<Action> _actions = new List<Action>();
        public int Count { get =>  _actions.Count; }
        public void Add(Action action)
        {
            _actions.Add(action);
        }
        public void Remove(Action action)
        {
            _actions.Remove(action);
        }
        public void Update()
        {
            foreach (var item in _actions)
            {
                item();
            }
        }
    }
    public class ComponentUpdater : MonoBehaviour
    {
        [SerializeField]
        public int interactColliderActionNum;
        public int moveActionNum;
        public int unorderedActionNum;


        internal static Actions colliderActions = new Actions();
        internal static Actions moveActions = new Actions();
        public static Actions unorderedActions = new Actions();

        private void DebugUpdate()
        {
            interactColliderActionNum = colliderActions.Count;
            moveActionNum = moveActions.Count;
        }

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        // Update is called once per frame
        protected void Update()
        {
            colliderActions.Update();
            moveActions.Update();
            unorderedActions.Update();
            DebugUpdate();
        }
    }
}
