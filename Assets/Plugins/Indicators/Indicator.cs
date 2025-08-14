using GameBase.Tools;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameBase.Indicators
{
    public class Indicator : IUEntity<GameObject>
    {
        public PossibleObj<IIndicatorOwner> owner;
        public Vector3 position;
        public int textureID;

        internal Material decalMaterial;
        internal bool setSize;
        internal Vector3 size;
        internal protected DecalProjector urpProjector;

        public Vector3 Size
        {
            set
            {
                setSize = true;
                size = value;
            }
        }
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        int IEntity.InstanceID { get; set; }
        public virtual Action AfterInstantiateObj { get; set; } = null;
    }
}


