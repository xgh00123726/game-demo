using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public class HealthBarSys : UObjEntitySys<HealthBar, CSObjectPool<HealthBar>, GameObject, UObjectPool<GameObject>>
    {
        public static float losingSpeed = 1f;

        protected override int ContainerCapacity => ResourcesLoader.PrefabCount;

        protected override GameObject InstantiateObj(HealthBar e)
        {
            return GameObject.Instantiate(ResourcesLoader.GetPrefab(e.bodyID));
        }

        protected override void OnInstantiateUObject(HealthBar e)
        {
            e.rectTransform = e.body.GetComponent<RectTransform>();

            e.textObj = e.body.transform.Find("Text").gameObject;
            e.textComponent = e.textObj.GetComponent<TextMeshProUGUI>();

            e.current = e.body.transform.Find("Current").gameObject;
            e.currentRectTransform = e.current.GetComponent<RectTransform>();

            e.losing = e.body.transform.Find("Losing").gameObject;
            e.losingRectTransform = e.losing.GetComponent<RectTransform>();

            e.widthMax = e.rectTransform.rect.width;

            e.body.SetActive(true);
        }

        protected override void OnReleaseUObject(HealthBar e)
        {
            e.body.SetActive(false);
        }

        private void SetWidth(RectTransform bar, float percent, float widthMax)
        {
            bar.offsetMax = new Vector2(percent * widthMax - widthMax, bar.offsetMax.y);
        }

        protected override void UpdateEntity(HealthBar e)
        {
            e.body.transform.position = e.owner.HealthBarPosition;

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
