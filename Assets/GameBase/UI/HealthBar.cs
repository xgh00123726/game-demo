using GameBase.Tools;
using UnityEngine;
using TMPro;
namespace GameBase.UI
{
    /// <summary>
    /// 管理血条预制件
    /// </summary>
    public class HealthBar : MonoBehaviour,
        IPoolableObject
    {
        internal IHealthBarOwner _owner;
        private RectTransform _rectTransform;
        private GameObject _textObj;
        private TextMeshProUGUI _textComponent;
        private GameObject _current;
        private RectTransform _currentRectTransform;
        private float _widthMax = 3f;
        private float _HPMax = 100f;
        private float _currHP;
        

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _widthMax = _rectTransform.rect.width;
            _textObj = transform.Find("Text").gameObject;
            _textComponent = _textObj.GetComponent<TextMeshProUGUI>();
            _current = transform.Find("Current").gameObject;
            _currentRectTransform = _current.GetComponent<RectTransform>();

            //UIMgr.AttachToRoot(transform);
        }

        /// <summary>
        /// 赋值给Percent，血条长度会随之变化
        /// </summary>
        public float Percent
        {
            set => Width = Mathf.Clamp01(value) * _widthMax;
        }

        private void HealbarUpdOnce()
        {
            _textComponent.text = $"{_currHP} / {_HPMax}";
            Percent = _currHP / _HPMax;
        }

        public float CurrHP
        {
            get => _currHP;
            set
            {
                _currHP = value;
                HealbarUpdOnce();
            }
        }

        public float HPMax
        {
            get => _HPMax;
            set
            {
                _HPMax = value;
                HealbarUpdOnce();
            }
        }

        private float Width
        {
            set => _currentRectTransform.offsetMax = new Vector2(value - _widthMax, _currentRectTransform.offsetMax.y);
        }

        internal void _Update()
        {
            transform.position = _owner.HealthBarPosition;
        }

        void IPoolableObject.OnInstantiate()
        {
            gameObject.SetActive(true);
        }

        void IPoolableObject.OnRelease()
        {
            gameObject.SetActive(false);
        }
    }
}
