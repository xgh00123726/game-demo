using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class EpicBarSys : KeyEntitySys<string, EpicBar, EpicBarSys>
    {
        public static float losingSpeed = 1f;

        protected override EpicBar CtorT(string k)
        {
            var e = new EpicBar();
            var obj = GameObject.Instantiate(ResourcesLoader.Prefab.Get(k));

            obj.transform.SetParent(RootCanvas.Instance.Layer(2), false);

            e.textObj = obj.transform.Find("Text").gameObject;
            e.textComponent = e.textObj.GetComponent<TextMeshProUGUI>();

            e.current = obj.transform.Find("Current").gameObject;
            e.currentRectTransform = e.current.GetComponent<RectTransform>();

            e.losing = obj.transform.Find("Losing").gameObject;
            e.losingRectTransform = e.losing.GetComponent<RectTransform>();

            e.regenText = obj.transform.Find("RegenText").gameObject.GetComponent<TextMeshProUGUI>();

            e.obj = obj;
            return e;
        }



        protected override void OnGet(EpicBar e)
        {
            e.currPercent = 1f;
            e.losingPercent = 1f;
            e.hpChange = true;

            e.obj.SetActive(true);
        }

        protected override void OnRelease(EpicBar e)
        {
            e.obj.SetActive(false);
        }

        private void SetWidth(RectTransform bar, float percent, float widthMax)
        {
            float width = percent * widthMax;
            float widthloss = widthMax - width;
            bar.sizeDelta = new Vector2(widthMax - widthloss, bar.sizeDelta.y);
            bar.localPosition = new Vector3(-widthloss / 2, bar.localPosition.y, bar.localPosition.z);
        }

        protected override void UpdateEntity(EpicBar e)
        {
            if (e.losingPercent > e.currPercent)
            {
                e.losingPercent -= losingSpeed * Time.deltaTime;
                SetWidth(e.losingRectTransform, e.losingPercent, e.width);
            }

            if (e.regen > 0)
            {
                e.regenText.text = $"+{String.Format("{0:0.#}", e.regen)}/s";
            }
            else
            {
                e.regenText.text = "";
            }

            if (!e.hpChange)
            {
                return;
            }

            e.hpChange = false;

            if (e.maxHP < Mathf.Epsilon) return;

            e.currPercent = Mathf.Clamp01(e.currHP / e.maxHP);
            e.textComponent.text = $"{String.Format("{0:0}", e.currHP)} / {String.Format("{0:0}", e.maxHP)}";
            SetWidth(e.currentRectTransform, e.currPercent, e.width);
        }
    }
}
