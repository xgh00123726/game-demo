using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBarSys : KeyEntitySys<string, HealthBar, HealthBarSys>
    {
        public static float losingSpeed = 0.2f;

        protected override HealthBar CtorT(string k)
        {
            var e = new HealthBar();

            var obj = GameObject.Instantiate(ResourceMgr.Prefab.Get(k));

            obj.transform.SetParent(WorldCanvs.Instance.Transform, false);

            e.Obj = obj;

            return e;
        }



        protected override void OnGet(HealthBar e)
        {
            e.current = e.Obj.transform.Find("Current").gameObject;
            e.currentRectTransform = e.current.GetComponent<RectTransform>();

            e.losing = e.Obj.transform.Find("Losing").gameObject;
            e.losingRectTransform = e.losing.GetComponent<RectTransform>();

            e.currPercent = 1f;
            e.losingPercent = 1f;

            e.Obj.SetActive(true);
        }

        protected override void OnRelease(HealthBar e)
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
            if (!e.Owner.Alive)
            {
                RemoveEntity(e);
                return;
            }

            if (e.HealthBarFollow)
            {
                e.Obj.transform.position = e.Owner.HealthBarPosition;
            }

            if (e.losingPercent > e.currPercent)
            {
                e.losingPercent -= losingSpeed * Time.deltaTime;
                SetWidth(e.losingRectTransform, e.losingPercent, e.Width);
            }

            int currHP = (int)e.Owner.CurrHP;
            int maxHP = (int)e.Owner.MaxHP;

            if (currHP == e.lastCurrHP && maxHP == e.lastMaxHP)
            {
                return;
            }

            e.currPercent = Mathf.Clamp01(e.Owner.CurrHP / e.Owner.MaxHP);
            SetWidth(e.currentRectTransform, e.currPercent, e.Width);

            e.lastCurrHP = currHP;
            e.lastMaxHP = maxHP;
        }
    }
}
