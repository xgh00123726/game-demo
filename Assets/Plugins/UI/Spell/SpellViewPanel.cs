using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class SpellViewPanel : BaseViewPanel<SpellViewItem>
    {
        private int _lastClickedItemIndex = -1;

        public Action<int> OnClickedItem;

        public SpellViewPanel(int prefabID = 12,
            int defaultObjID = 11) : base(
            prefabID,
            defaultObjID)
        {
        }

        protected override BaseUI InstantiateObj(SpellViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.timeTMP = obj.transform.Find("CoolingDownText").GetComponent<TextMeshProUGUI>();

            e.chargeTMP = obj.transform.Find("Charge").GetComponent<TextMeshProUGUI>();

            var image = obj.transform.Find("Icon").GetComponent<Image>();
            if (image == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("panel item must has icon object");
            }

            e.iconImage = image;

            return obj;
        }

        protected override void UpdateEntity(SpellViewItem e)
        {
            base.UpdateEntity(e);

            float coolingTimeRemain = e.coolingTimeRemain;

            float fullVal = coolingTimeRemain / e.coolingTimeSet;
            e.iconMaterial.SetFloat("_MaskFull", fullVal);

            string coolingText = string.Empty;
            if (coolingTimeRemain > 1)
            {
                coolingText = ((int)coolingTimeRemain).ToString();
            }
            else if (coolingTimeRemain > 0)
            {
                coolingText = $".{(int)(coolingTimeRemain * 10)}";
            }
            e.timeTMP.text = coolingText;

            if (e.Obj.isPointerDown)
            {
                OnClickedItem(e.ItemIndex);
                _lastClickedItemIndex = e.ItemIndex;
            }
        }

        /// <summary>
        /// 设置e的物品贴图和材质
        /// </summary>
        /// <param name="e"></param>
        public void SetIcon(SpellViewItem e, int iconTextureID)
        {
            e.iconTextureID = iconTextureID;

            e.iconMaterial = new Material(e.iconImage.material);
            e.iconImage.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureID));

            e.iconMaterial.SetTexture("_Target", texture);
        }

        public void SetCoolingTimeSet(int index, float value)
        {
            container[index].coolingTimeSet = value;
        }

        public void SetCoolingTimeRemain(int index, float value)
        {
            container[index].coolingTimeRemain = value;
        }

        public int LastClickedItemIndex => _lastClickedItemIndex;
    }
}
