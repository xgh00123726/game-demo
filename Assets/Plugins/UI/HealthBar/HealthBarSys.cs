using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBarSys : UObjEntitySys<HealthBar, GameObject, HealthBarSys>
    {
        public static float losingSpeed = 0.2f;

        protected override GameObject InstantiateObj(HealthBar e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            obj.transform.SetParent(WorldCanvs.Instance.transform, false);

            e.current = obj.transform.Find("Current").gameObject;
            e.currentRectTransform = e.current.GetComponent<RectTransform>();

            e.losing = obj.transform.Find("Losing").gameObject;
            e.losingRectTransform = e.losing.GetComponent<RectTransform>();

            return obj;
        }



        protected override void AfterInstantiateEUObject(HealthBar e)
        {
            e.currPercent = 1f;
            e.losingPercent = 1f;

            e.Obj.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(HealthBar e)
        {
            e.Obj.SetActive(false);
        }

        private void SetWidth(RectTransform bar, float percent, float widthMax)
        {
            float width = percent * widthMax;
            float widthloss = widthMax - width;
            bar.sizeDelta = new Vector2(widthMax - widthloss, bar.sizeDelta.y);
            bar.localPosition = new Vector3(-widthloss / 2, bar.localPosition.y, bar.localPosition.z);
            //bar.offsetMax = new Vector2(percent * widthMax - widthMax, bar.offsetMax.y);
        }

        protected override void UpdateEntity(HealthBar e)
        {
            if (!e.owner.ALive)
            {
                RemoveEntity(e);
                return;
            }

            if (e.healthBarFollow)
            {
                e.Obj.transform.position = e.owner.HealthBarPosition;
            }

            if (e.losingPercent > e.currPercent)
            {
                e.losingPercent -= losingSpeed * Time.deltaTime;
                SetWidth(e.losingRectTransform, e.losingPercent, e.width);
            }

            int currHP = (int)e.owner.CurrHP;
            int maxHP = (int)e.owner.MaxHP;

            if (currHP == e.lastCurrHP && maxHP == e.lastMaxHP)
            {
                return;
            }

            e.currPercent = Mathf.Clamp01(e.owner.CurrHP / e.owner.MaxHP);
            SetWidth(e.currentRectTransform, e.currPercent, e.width);

            e.lastCurrHP = currHP;
            e.lastMaxHP = maxHP;
        }
    }
}
