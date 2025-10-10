using GameBase.EntitySystem;
using System.Collections.Generic;

namespace GameBase.Modify
{
    public class Modifyable
    {
        internal float valueSet;
        internal float value;
        internal LinkedList<Modifyer> modifyersNeedAdd = new();
        internal LinkedList<Modifyer> modifyersNeedRemove = new();

        public LinkedList<Modifyer> modifyers = new ();
        public float Value => value;
        public float ValueSet => valueSet;

        public void AddValue(float value)
        {
            var modifier = ModifyerSys.Instance.NewEntity();
            modifier.value = value;
            modifier.type = ModifyType.Once | ModifyType.Forever;
            modifyersNeedAdd.AddLast(modifier);
        }

        public void AddModifier(Modifyer modifyer)
        {
            modifyersNeedAdd.AddLast(modifyer);
        }

        public void RemoveModifier(Modifyer modifyer)
        {
            modifyersNeedRemove.AddLast(modifyer);
        }
    }
}
