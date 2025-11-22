using GameBase.EntitySystem;
using GameBase.Tools;
using System;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class EpicBar : IKeyEntity<string>
    {
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

        public float Width { get; set; } = 960f;
        public float Regen { get; set; } = 0;

        public GameObject Obj { get; set; }
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
