using GameBase.Tools;
using TMPro;
using UnityEngine;
namespace GameBase.UI
{
    public class FloatText : IPoolableObject,
        IUEntity<GameObject>
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

        public int bodyID = -1;
        public string value;
        public Vector3 showPosition;

        internal int id;
        internal float yFactorA = 0f;
        internal float yFactorB = 0f;
        internal float yFactorC = 0f;
        internal float xFactor = 0f;
        internal float zFactor = 0f;
        internal float horizontalSpeed = 0f;
        internal float duration;
        internal float instantiateTime;

        internal GameObject body;
        internal RectTransform rectTransform;
        internal TextMeshProUGUI textObj;

        public FloatText()
        {

        }

        public int ID
        {
            get => id;
            set => id = value;
        }

        public GameObject Obj 
        {
            get => body;
            set => body = value;
        }
        public int ObjID => bodyID;

        void IPoolableObject.OnInstantiate()
        {
            bodyID = 5;

            duration = 4f;
            instantiateTime = Time.time;

            yFactorA = Random.Range(_yFactorAMin, _yFactorAMax);
            yFactorB = Random.Range(_yFactorBMin, _yFactorBMax);
            yFactorC = Random.Range(_yFactorCMin, _yFactorCMax);
            horizontalSpeed = Random.Range(_horizontalSpeedMin, _horizontalSpeedMax);
            float rad = Random.Range(0f, Mathf.PI * 2);
            xFactor = Mathf.Sin(rad) * horizontalSpeed;
            zFactor = Mathf.Cos(rad) * horizontalSpeed;
        }

        void IPoolableObject.OnRelease()
        {
        }
    }
}
