using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace GameBase.UI
{
    public class HealthBarSys : UObjEntitySys<HealthBar, SimpleEntityContainer, GameObject, HealthBarSys>
    {
        public static float losingSpeed = 1f;

        protected override GameObject InstantiateObj(HealthBar e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            e.rectTransform = obj.GetComponent<RectTransform>();
            e.widthMax = e.rectTransform.rect.width;

            e.textObj = obj.transform.Find("Text").gameObject;
            e.textComponent = e.textObj.GetComponent<TextMeshProUGUI>();

            e.current = obj.transform.Find("Current").gameObject;
            e.currentRectTransform = e.current.GetComponent<RectTransform>();

            e.losing = obj.transform.Find("Losing").gameObject;
            e.losingRectTransform = e.losing.GetComponent<RectTransform>();

            return obj;
        }



        protected override void AfterInstantiateEUObject(HealthBar e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("health bar must has a owner");
            }

            e.currPercent = 1f;
            e.losingPercent = 1f;
            e.HPChange = true;

            e.Obj.SetActive(true);
        }

        protected override void BeforeReleaseEUObject(HealthBar e)
        {
            e.Obj.SetActive(false);
        }

        private void SetWidth(RectTransform bar, float percent, float widthMax)
        {
            bar.offsetMax = new Vector2(percent * widthMax - widthMax, bar.offsetMax.y);
        }

        protected override void UpdateEntity(HealthBar e)
        {
            e.Obj.transform.position = e.owner.HealthBarPosition;

            if (e.losingPercent > e.currPercent)
            {
                e.losingPercent -= losingSpeed * Time.deltaTime;
                SetWidth(e.losingRectTransform, e.losingPercent, e.widthMax);
            }


            if (!e.HPChange)
            {
                return;
            }

            e.HPChange = false;
            e.currPercent = Mathf.Clamp01(e.currHP / e.maxHP);
            e.textComponent.text = $"{e.currHP} / {e.maxHP}";
            SetWidth(e.currentRectTransform, e.currPercent, e.widthMax);
        }
    }
}
