using GameBase.Tools;
using UnityEngine;

namespace GameBase.Creature
{
    public class Creature : IUEntity<GameObject>,
        IPoolableObject
    {
        public int bodyID;
        public int radius;
        public Vector3 genPos;

        internal int id;
        internal GameObject body;


        public int ID
        {
            get => id;
            set => id = value;
        }

        public GameObject Obj
        {
            get => body;
            set => body = value;
        }
        public int ObjID => bodyID;

        void IPoolableObject.OnInstantiate()
        {
            
        }

        void IPoolableObject.OnRelease()
        {
            
        }
    }
}
