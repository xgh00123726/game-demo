using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBar : IKeyEntity<int>
    {
        public IHealthBarOwner owner;
        public float width = 0.9f;
        public bool healthBarFollow = true;

        internal int lastCurrHP;
        internal int lastMaxHP;
        internal float currPercent;
        internal float losingPercent;
        internal GameObject current;
        internal RectTransform currentRectTransform;
        internal GameObject losing;
        internal RectTransform losingRectTransform;

        public GameObject obj;
        public int Key { get; set; }
    }
}
