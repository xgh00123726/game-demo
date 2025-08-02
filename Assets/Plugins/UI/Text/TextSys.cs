using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
namespace GameBase.UI
{
    public class TextSys : UObjEntitySys<FloatText, CSObjectPool<FloatText>, GameObject, UObjectPool<GameObject>>
    {
        protected override int ContainerCapacity => ResourcesLoader.PrefabCount;

        protected override GameObject InstantiateObj(FloatText e)
        {
            return GameObject.Instantiate(ResourcesLoader.GetPrefab(e.bodyID));
        }

        protected override void OnInstantiateUObject(FloatText e)
        {
            e.rectTransform = e.body.GetComponent<RectTransform>();
            e.textObj = e.body.GetComponent<TextMeshProUGUI>();
            e.textObj.text = e.value;
            e.body.SetActive(true);
        }

        protected override void OnReleaseUObject(FloatText e)
        {
            e.body.SetActive(false);
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
