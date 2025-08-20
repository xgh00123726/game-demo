using GameBase.EntitySystem;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class DetailUISys : UObjEntitySys<DetailUI, SimpleEntityContainer, BaseUI, DetailUISys>
    {
        protected override void AfterInstantiateEUObject(DetailUI e)
        {
            
        }

        protected override void BeforeReleaseEUObject(DetailUI e)
        {
            
        }

        protected override BaseUI InstantiateObj(DetailUI e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));
            var ui = obj.AddComponent<BaseUI>();

            e.textTMP = obj.transform.Find("Text").GetComponent<TextMeshProUGUI>();

            if (e is ISwitchable se)
            {
                ui.OnSwitchOn = se.OnSwitchOn;
                ui.OnSwitchOff = se.OnSwitchOff;
            }

            obj.transform.SetParent(RootCanvas.instance.transform, false);

            var image = obj.transform.Find("Icon").GetComponent<Image>();
            if (image == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            e.iconMaterial = new Material(image.material);
            image.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);

            return ui;
        }

        protected override void UpdateEntity(DetailUI e)
        {
            foreach (var detailable in e.detailables)
            {
                if (!detailable.IsPointerOn)
                {
                    continue;
                }

                e.Obj.transform.position = detailable.ShowPosition;
            }

            bool active = e.Obj.gameObject.activeSelf;

            // 悬浮在任意一个可显示详细信息的UI上就显示详细UI面板
            foreach (var detailable in e.detailables)
            {
                if (!active && detailable.IsPointerOn)
                {
                    active = true;
                    e.textTMP.text = detailable.Content.value;
                    break;
                }
            }

            // 如果所有可显示详细信息的UI上都没检测到悬浮就隐藏面板
            bool inActiveFlag = true;
            foreach (var detailable in e.detailables)
            {
                if (detailable.IsPointerOn)
                {
                    inActiveFlag = false;
                }
            }
            
            if (inActiveFlag && e.Obj.gameObject.activeSelf)
            {
                active = false;
            }

            if (active != e.Obj.gameObject.activeSelf)
            {
                e.Obj.gameObject.SetActive(active);
            }
        }
    }
}
