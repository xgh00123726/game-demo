using GameBase.EntitySystem;
using System.Collections.Generic;

namespace GameBase.Modify
{
    public class Modifyable : IEntity
    {
        internal float valueSet;
        internal float value;
        internal LinkedList<Modifyer> modifyersNeedAdd = new();
        internal LinkedList<Modifyer> modifyersNeedRemove = new();

        public LinkedList<Modifyer> modifyers = new ();
        public float Value => value;
        public float ValueSet => valueSet;

        public void AddModify(Modifyer modifyer)
        {
            modifyersNeedAdd.AddLast(modifyer);
        }

        public void RemoveModify(Modifyer modifyer)
        {
            modifyersNeedRemove.AddLast(modifyer);
        }

        int IEntity.InstanceID { get; set; }
    }
}
