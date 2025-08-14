using GameBase.Tools;
using System;
using TMPro;
using UnityEngine;
namespace GameBase.UI
{
    public class FloatText : IUEntity<GameObject>
    {
        // 被抛出的text的y坐标随时间以二次函数曲线变化y = a*t*t+b*t+c
        public static float _yFactorAMin = -4f;
        public static float _yFactorAMax = -5f;
        public static float _yFactorBMin = 1f;
        public static float _yFactorBMax = 1.2f;
        public static float _yFactorCMin = 4f;
        public static float _yFactorCMax = 4f;
        public static float _horizontalSpeedMin = 1.1f;
        public static float _horizontalSpeedMax = 1.4f;

        public string value;
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

        public int InstanceID { get; set; }

        public GameObject Obj { get; set; }
        public int ObjID { get; set; } = 5;
        public virtual Action AfterInstantiateObj { get; set; } = null;
    }
}
