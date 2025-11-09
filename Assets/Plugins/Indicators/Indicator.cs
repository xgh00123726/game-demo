using GameBase.EntitySystem;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameBase.Indicators
{
    public class Indicator : IKeyEntity<string>
    {
        public string textureName;

        internal Material decalMaterial;
        internal bool setSize;
        internal bool setPivot;
        internal Vector3 size;
        internal Vector3 pivot;
        internal protected DecalProjector urpProjector;

        public Color Color
        {
            get => decalMaterial.GetColor("_Color");
            set => decalMaterial.SetColor("_Color", value);
        }

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
        public GameObject obj;
        public string Key { get; set; }
    }
}


