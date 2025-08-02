using System.Collections;
using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    internal class BaseText : BaseUI,
        IPoolableObject
    {
        protected GameObject _textObj;
        protected TextMeshProUGUI _textComponent;
        private float _duration;
        private bool _updateEnable = false;
        protected Vector3 _showPosition;
        private float _instantiateTime = 0;

        // 被抛出的text的y坐标随时间以二次函数曲线变化y = a*t*t+b*t+c
        public static float _yFactorAMin = -4f;
        public static float _yFactorAMax = -5f;
        public static float _yFactorBMin = 1f;
        public static float _yFactorBMax = 1.2f;
        public static float _yFactorCMin = 4f;
        public static float _yFactorCMax = 4f;
        public static float _horizontalSpeedMin = 1.1f;
        public static float _horizontalSpeedMax = 1.4f;


        public float _yFactorA = 0f;
        public float _yFactorB = 0f;
        public float _yFactorC = 0f;
        public float _xFactor = 0f;
        public float _zFactor = 0f;

        public float _horizontalSpeed = 0f;
        public float Duration
        {
            get => _duration;
            internal set
            {
                _duration = value;
            }
        }

        public Vector3 ShowPosition
        {
            get => _showPosition;
            internal set
            {
                _showPosition = value;
                transform.position = _showPosition;
            }
        }

        public string Text
        {
            get => _textComponent.text;
            set => _textComponent.text = value;
        }
   
        protected virtual void Awake()
        {
            _textObj = transform.Find("Text").gameObject;
            _textComponent = _textObj.GetComponent<TextMeshProUGUI>();
            TextMgr.RegisterTextUpdate(OnUpdate);
        }

        private void OnUpdate()
        {
            if (!_updateEnable) return;
            _Update();
        }

        protected virtual void _Update() 
        {
            float t = Time.time - _instantiateTime;
            float y = _yFactorA * t * t + _yFactorB * t + _yFactorC * t;
            float x = _xFactor * t;
            float z = _zFactor * t;
            transform.position = _showPosition + new Vector3(x, y, z);
        }

        void IPoolableObject.OnInstantiate()
        {
            _updateEnable = true;
            _instantiateTime = Time.time;
            _yFactorA = Random.Range(_yFactorAMin, _yFactorAMax);
            _yFactorB = Random.Range(_yFactorBMin, _yFactorBMax);
            _yFactorC = Random.Range(_yFactorCMin, _yFactorCMax);
            _horizontalSpeed = Random.Range(_horizontalSpeedMin, _horizontalSpeedMax);
            float rad = Random.Range(0f, Mathf.PI * 2);
            _xFactor = Mathf.Sin(rad) * _horizontalSpeed;
            _zFactor = Mathf.Cos(rad) * _horizontalSpeed;

            // backup
            //_yFactorA = Random.Range(UIMgr.Instance._yFactorAMin, UIMgr.Instance._yFactorAMax);
            //_yFactorB = Random.Range(UIMgr.Instance._yFactorBMin, UIMgr.Instance._yFactorBMax);
            //_yFactorC = Random.Range(UIMgr.Instance._yFactorCMin, UIMgr.Instance._yFactorCMax);
            //_horizontalSpeed = Random.Range(UIMgr.Instance._horizontalSpeedMin, UIMgr.Instance._horizontalSpeedMax);
            gameObject.SetActive(true);
        }



        void IPoolableObject.OnRelease()
        {
            _updateEnable = false;
            gameObject.SetActive(false);
        }
    }
}
