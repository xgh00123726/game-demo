using GameBase.EntitySystem;
using System;
using TMPro;
using UnityEngine;
namespace GameBase.UI
{
    public class FloatText : IKeyEntity<string>
    {
        // 被抛出的text的y坐标随时间以二次函数曲线变化y = a*t*t+b*t+c
        public static float _yFactorAMin = -4f;
        public static float _yFactorAMax = -5f;
        public static float _yFactorBMin = 1f;
        public static float _yFactorBMax = 1.2f;
        public static float _yFactorCMin = 5f;
        public static float _yFactorCMax = 5f;
        public static float _horizontalSpeedMin = 0.3f;
        public static float _horizontalSpeedMax = 0.4f;

        public Vector3 showPosition;

        internal float yFactorA = 0f;
        internal float yFactorB = 0f;
        internal float yFactorC = 0f;
        internal float xFactor = 0f;
        internal float zFactor = 0f;
        internal float horizontalSpeed = 0f;
        internal float duration;
        internal float instantiateTime;

        internal RectTransform rectTransform;
        internal TextMeshProUGUI textObj;

        public GameObject obj;
        public string Key { get; set; } = "Prefabs/UI/FloatText";
        public Color Color
        {
            get => textObj.color;
            set => textObj.color = value;
        }

        public string Value
        {
            get => textObj.text;
            set => textObj.text = value;
        }
    }
}
