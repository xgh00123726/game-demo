using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.Modify
{
    public class Modifyable<T> : IEntity,
        IPoolableObject
    {
        internal int id;
        internal T valueSet;
        internal T value;
        public LinkedList<Func<T, T>> modifyBehaviors = new LinkedList<Func<T, T>>();
        public T Value => value;

        public void AddModify(Func<T, T> func)
        {
            modifyBehaviors.AddLast(func);
        }

        public void RemoveModify(Func<T, T> func)
        {
            if (modifyBehaviors.Contains(func))
            {
                modifyBehaviors.Remove(func);
            }
        }

        int IEntity.ID
        {
            get => id;
            set => id = value;
        }

        public T Get() => value;

        void IPoolableObject.OnInstantiate()
        {
        }

        void IPoolableObject.OnRelease()
        {
            modifyBehaviors.Clear();
        }
    }
}
