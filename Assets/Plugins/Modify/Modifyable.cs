using GameBase.Tools;
using System;
using System.Collections.Generic;

namespace GameBase.Modify
{
    public class Modifyable<T> : IEntity
    {
        internal int id;
        internal T valueSet;
        internal T value;
        internal LinkedList<Modifyer<T>> modifyersNeedAdd = new();
        internal LinkedList<Modifyer<T>> modifyersNeedRemove = new();

        public List<Action<Modifyable<T>>> RegistertoActivesDelegate = new();
        public List<Action<Modifyable<T>>> RemoveFromActiveDelegate = new();

        public LinkedList<Modifyer<T>> modifyers = new ();
        public T Value => value;
        public T ValueSet => valueSet;

        public void AddModify(Modifyer<T> modifyer)
        {
            modifyersNeedAdd.AddLast(modifyer);
        }

        public void RemoveModify(Modifyer<T> modifyer)
        {
            modifyersNeedRemove.AddLast(modifyer);
        }

        int IEntity.InstanceID { get; set; }
    }
}
