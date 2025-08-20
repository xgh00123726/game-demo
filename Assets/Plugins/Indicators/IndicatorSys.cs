using GameBase.EntitySystem;
using GameBase.Resources;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameBase.Indicators
{
    public class IndicatorSys : UObjEntitySys<Indicator, SimpleEntityContainer, GameObject, IndicatorSys>
    {
        protected override void AfterInstantiateEUObject(Indicator e)
        {
            e.Obj.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(Indicator e)
        {
            e.Obj.SetActive(false);
        }

        protected override GameObject InstantiateObj(Indicator e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));
            
            e.urpProjector = obj.GetComponent<DecalProjector>();

            e.decalMaterial = new Material(e.urpProjector.material);
            e.urpProjector.material = e.decalMaterial;

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.textureID));

            e.decalMaterial.SetTexture("_Texture2D", texture);

            return obj;
        }

        protected override void UpdateEntity(Indicator e)
        {
            if (!e.Obj.activeSelf)
            {
                return;
            }

            if (e.setSize)
            {
                e.setSize = false;
                e.size.z = e.urpProjector.size.z;
                e.urpProjector.size = e.size;
            }
            if (e.setPivot)
            {
                e.setPivot = false;
                e.urpProjector.pivot = e.pivot;
            }
        }

        public void RemoveIndicator(Indicator indicator)
        {
            RemoveEntity(indicator);
        }
    }
}
