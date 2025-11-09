using GameBase.EntitySystem;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBar : IKeyEntity<string>
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
        public string Key { get; set; }
    }
}
