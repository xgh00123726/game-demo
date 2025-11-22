using GameBase.EntitySystem;
using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
namespace GameBase.UI
{
    public class TextSys : KeyEntitySys<string, FloatText, TextSys>
    {
        protected override FloatText CtorT(string k)
        {
            var e = new FloatText();
            var obj = GameObject.Instantiate(ResourceMgr.Prefab.Get(k));
            obj.transform.SetParent(WorldCanvs.Instance.transform, false);
            e.Obj = obj;
            return e;
        }


        protected override void OnGet(FloatText e)
        {
            e.rectTransform = e.Obj.GetComponent<RectTransform>();
            e.textObj = e.Obj.GetComponent<TextMeshProUGUI>();

            e.duration = FloatTextConfig.Float.existTime;
            e.instantiateTime = Time.time;

            e.yFactorA = Random.Range(FloatTextConfig.Float.yFactorAMin, FloatTextConfig.Float.yFactorAMax);
            e.yFactorB = Random.Range(FloatTextConfig.Float.yFactorBMin, FloatTextConfig.Float.yFactorBMax);
            e.yFactorC = Random.Range(FloatTextConfig.Float.yFactorCMin, FloatTextConfig.Float.yFactorCMax);
            e.horizontalSpeed = Random.Range(FloatTextConfig.Float.horizontalSpeedMin, FloatTextConfig.Float.horizontalSpeedMax);
            float rad = Random.Range(0f, Mathf.PI * 2);
            e.xFactor = Mathf.Sin(rad) * e.horizontalSpeed;
            e.zFactor = Mathf.Cos(rad) * e.horizontalSpeed;
        }

        protected override void EntityStart(FloatText e)
        {
            e.Obj.SetActive(true);
        }


        protected override void OnRelease(FloatText e)
        {
            e.Obj.SetActive(false);
        }

        protected override void UpdateEntity(FloatText e)
        {
            float t = Time.time - e.instantiateTime;
            if (t > e.duration)
            {
                RemoveEntity(e);
                return;
            }

            float y = e.yFactorA * t * t + e.yFactorB * t + e.yFactorC * t;
            float x = e.xFactor * t;
            float z = e.zFactor * t;
            e.rectTransform.localPosition = e.showPosition + new Vector3(x, y, z);
        }
    }
}
