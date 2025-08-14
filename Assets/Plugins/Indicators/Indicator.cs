using GameBase.Tools;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameBase.Indicators
{
    public class Indicator : IUEntity<GameObject>
    {
        public Vector3 position;
        public bool visible;
        public int textureID;

        internal Material decalMaterial;
        internal bool setSize;
        internal bool setPivot;
        internal Vector3 size;
        internal Vector3 pivot;
        internal protected DecalProjector urpProjector;

        public Vector3 Size
        {
            set
            {
                setSize = true;
                size.x = value.x;
                size.y = value.y;
            }
        }
        public Vector3 Pivot
        {
            set
            {
                setPivot = true;
                pivot = value;
            }
        }
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        int IEntity.InstanceID { get; set; }
        public virtual Action AfterInstantiateObj { get; set; } = null;
    }
}


