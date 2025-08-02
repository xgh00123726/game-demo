using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBar : IUEntity<GameObject>,
        IPoolableObject
    {
        public int bodyID;
        public IHealthBarOwner owner;

        internal float currHP;
        internal float maxHP;
        internal bool HPChange;
        internal float widthMax;
        internal float currPercent;
        internal float losingPercent;
        internal int id;
        internal GameObject body;
        internal RectTransform rectTransform;
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

        public GameObject Obj
        {
            get => body;
            set => body = value;
        }

        public int ObjID => bodyID;

        public int ID
        {
            get => id;
            set => id = value;
        }

        void IPoolableObject.OnInstantiate()
        {
            currPercent = 1f;
            losingPercent = 1f;
            HPChange = true;

            if (owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("health bar must has a owner");
            }
        }

        void IPoolableObject.OnRelease()
        {
            
        }
    }
}
