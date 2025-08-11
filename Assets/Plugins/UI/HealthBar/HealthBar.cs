using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBar : IUEntity<GameObject>
    {
        public IHealthBarOwner owner;
        public float width = 0.9f;
        public bool healthBarFollow = true;

        internal float currPercent;
        internal float losingPercent;
        internal GameObject textObj;
        internal TextMeshProUGUI textComponent;
        internal GameObject current;
        internal RectTransform currentRectTransform;
        internal GameObject losing;
        internal RectTransform losingRectTransform;

        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        public int InstanceID { get; set; }
    }
}
