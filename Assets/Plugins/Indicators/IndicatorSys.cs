using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameBase.Indicators
{
    public class IndicatorSys : KeyEntitySys<int, Indicator, IndicatorSys>
    {
        protected override void OnGet(Indicator e)
        {
            e.obj.SetActive(true);
        }

        protected override void OnRelease(Indicator e)
        {
            e.obj.SetActive(false);
        }

        protected override Indicator CtorT(int k)
        {
            var e = new Indicator();
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(k));
            e.urpProjector = obj.GetComponent<DecalProjector>();

            e.decalMaterial = new Material(e.urpProjector.material);
            e.urpProjector.material = e.decalMaterial;

            e.obj = obj;

            return e;
        }

        protected override void EntityStart(Indicator e)
        {
            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.textureID));

            e.decalMaterial.SetTexture("_Texture2D", texture);
        }

        protected override void UpdateEntity(Indicator e)
        {
            if (!e.obj.activeSelf)
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
