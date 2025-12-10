using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.UI
{
    public class TodoNode
    {
        private TodoNode parent;

        public ITodoView View { get; private set; }
        public ITodoTarget Target { get; private set; }
        public Func<bool> DirtyEvent { get; set; }
        public TodoNode Parent
        {
            get => parent;
            set
            {
                parent = value;
                parent?.Children.Add(this);
            }
        }
        public List<TodoNode> Children { get; private set; } = new();
        public bool IsDirty { get; internal set; }
        public Vector3 Offset { get; set; }
        public TodoNode(ITodoView view, ITodoTarget target, Func<bool> DirtyEvent, Vector3 offset = default)
        {
            View = view;
            Target = target;
            Offset = offset;
            this.DirtyEvent = DirtyEvent;
            if (parent != null)
            {
                Parent = parent;
                Parent.Children.Add(this);
            }
        }

        public void DirtyUpdate()
        {
            IsDirty = DirtyEvent?.Invoke() == true;
            foreach (var child in Children)
            {
                child.DirtyUpdate();
                IsDirty |= child.IsDirty;
            }
        }
    }
}
