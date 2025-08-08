using GameBase.Resources;
using GameBase.Tools;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
namespace GameBase.UI
{
    public class TextSys : UObjEntitySys<FloatText, SimpleEntityContainer, GameObject, TextSys>
    {
        protected override GameObject InstantiateObj(FloatText e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));
            e.rectTransform = obj.GetComponent<RectTransform>();
            e.textObj = obj.GetComponent<TextMeshProUGUI>();

            return obj;
        }

        protected override void BeforeInstantiateEUObject(FloatText e)
        {

        }

        protected override void AfterInstantiateEUObject(FloatText e)
        {
            e.duration = 1.5f;
            e.instantiateTime = Time.time;

            e.yFactorA = Random.Range(FloatText._yFactorAMin, FloatText._yFactorAMax);
            e.yFactorB = Random.Range(FloatText._yFactorBMin, FloatText._yFactorBMax);
            e.yFactorC = Random.Range(FloatText._yFactorCMin, FloatText._yFactorCMax);
            e.horizontalSpeed = Random.Range(FloatText._horizontalSpeedMin, FloatText._horizontalSpeedMax);
            float rad = Random.Range(0f, Mathf.PI * 2);
            e.xFactor = Mathf.Sin(rad) * e.horizontalSpeed;
            e.zFactor = Mathf.Cos(rad) * e.horizontalSpeed;

            e.textObj.text = e.value;

            e.Obj.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(FloatText e)
        {
            e.Obj.SetActive(false);
        }

        protected override void UpdateEntity(FloatText e)
        {
            float t = Time.time - e.instantiateTime;
            if (t > e.duration)
            {
                RemoveEntity(e);
            }

            float y = e.yFactorA * t * t + e.yFactorB * t + e.yFactorC * t;
            float x = e.xFactor * t;
            float z = e.zFactor * t;
            e.rectTransform.localPosition = e.showPosition + new Vector3(x, y, z);
        }
    }
}
