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
        private GameObject _losing;
        private RectTransform _losingRectTransform;

        private float _widthMax = 3f;
        private float _HPMax = 100f;
        private float _currHP;
        private float _losingPercent = 1f;
        private float _targetLosingPercent = 1f;

        // 每秒掉血速度
        public float losingSpeed = 0.5f;
        

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _textObj = transform.Find("Text").gameObject;
            _textComponent = _textObj.GetComponent<TextMeshProUGUI>();
            
            _current = transform.Find("Current").gameObject;
            _currentRectTransform = _current.GetComponent<RectTransform>();
            
            _losing = transform.Find("Losing").gameObject;
            _losingRectTransform = _losing.GetComponent<RectTransform>();

            _widthMax = _rectTransform.rect.width;
        }

        private float Width
        {
            set => _currentRectTransform.offsetMax = new Vector2(value - _widthMax, _currentRectTransform.offsetMax.y);
        }

        private float LosingWidth
        {
            set
            {
                _losingRectTransform.offsetMax = new Vector2(value - _widthMax, _losingRectTransform.offsetMax.y);
            }
        }

        private float LosingPercent
        {
            set
            {
                LosingWidth = Mathf.Clamp01(value) * _widthMax;
                _losingPercent = value;
            }
            get => _losingPercent;
        }

        private void HealbarUpdOnce()
        {
            _textComponent.text = $"{_currHP} / {_HPMax}";
            Percent = _currHP / _HPMax;
            _targetLosingPercent = _currHP / _HPMax;
        }

        /// <summary>
        /// 赋值给Percent，血条长度会随之变化
        /// </summary>
        public float Percent
        {
            set => Width = Mathf.Clamp01(value) * _widthMax;
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



        internal void _Update()
        {
            transform.position = _owner.HealthBarPosition;
            if (_losingPercent > _targetLosingPercent)
            {
                LosingPercent = _losingPercent - losingSpeed * Time.deltaTime;
            }
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
