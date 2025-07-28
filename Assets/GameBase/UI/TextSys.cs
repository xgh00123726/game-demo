using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
namespace GameBase.UI
{
    public class TextSys : EntitySys<FloatText, GameObject>
    {
        protected override int IDCount => ResourcesLoader.UIPrefabCount;

        protected override GameObject InstantiateObject(int id)
        {
            return GameObject.Instantiate(ResourcesLoader.GetUIPrefab(id));
        }

        protected override void OnObjectGet(FloatText e)
        {
            e.rectTransform = e.body.GetComponent<RectTransform>();
        }

        protected override void OnObjectRelease(FloatText e)
        {
            
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
