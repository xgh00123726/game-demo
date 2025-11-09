using GameBase.EntitySystem;
using GameBase.Tools;
using System;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class EpicBar : IKeyEntity<string>
    {
        public float width = 960f;
        public bool healthBarFollow = true; 
        public int contourTexureID = 7;
        public int targetTexureID = 9;
        public int shapeTexureID = 4;
        public PossibleObj<Vector3> positionSet;
        public float regen = 0;

        internal bool hpChange;
        internal float currHP;
        internal float maxHP;
        internal float currPercent;
        internal float losingPercent;
        internal GameObject textObj;
        internal TextMeshProUGUI textComponent;
        internal GameObject current;
        internal RectTransform currentRectTransform;
        internal GameObject losing;
        internal RectTransform losingRectTransform;
        internal TextMeshProUGUI regenText;

        public GameObject obj;
        public string Key { get; set; } = "Prefabs/UI/EpicHealthBar";
        public float CurrHP
        {
            set
            {
                hpChange = true;
                currHP = value;
            }
        }
        public float MaxHP
        {
            set
            {
                hpChange = true;
                maxHP = value;
            }
        }
    }
}
