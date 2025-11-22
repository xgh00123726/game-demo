using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBar : IKeyEntity<string>
    {
        internal int lastCurrHP;
        internal int lastMaxHP;
        internal float currPercent;
        internal float losingPercent;
        internal GameObject current;
        internal RectTransform currentRectTransform;
        internal GameObject losing;
        internal RectTransform losingRectTransform;

        public IHealthBarOwner Owner { get; set; }
        public float Width { get; set; } = 0.9f;
        public bool HealthBarFollow { get; set; } = true;
        public GameObject Obj { get; set; }
        public string Key { get; set; }
    }
}
