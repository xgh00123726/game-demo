using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBar : IUEntity<GameObject>
    {
        public IHealthBarOwner owner;

        internal float currHP;
        internal float maxHP;
        internal bool HPChange;
        internal float currPercent;
        internal float losingPercent;
        internal GameObject textObj;
        internal TextMeshProUGUI textComponent;
        internal GameObject current;
        internal RectTransform currentRectTransform;
        internal GameObject losing;
        internal RectTransform losingRectTransform;


        public float CurrHP
        {
            get => currHP;
            set
            {
                currHP = value;
                HPChange = true;
            }
        }

        public float MaxHP
        {
            get => maxHP;
            set
            {
                maxHP = value;
                HPChange = true;
            }
        }

        public GameObject Obj { get; set; }

        public int ObjID { get; set; }

        public int InstanceID { get; set; }
    }
}
