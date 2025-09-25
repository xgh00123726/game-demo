using GameBase.Creatures;
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
        private static SpellViewPanel _instance = new ();
        public static SpellViewPanel Instance => _instance;
        public SpellViewPanel(int prefabID = 12,
            int defaultObjID = 11) : base(
            prefabID,
            defaultObjID)
        {
            if (_instance != null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("instance has only one");
            }
        }

        protected override BaseUI InstantiateObj(SpellViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.timeTMP = e.obj.transform.Find("CoolingDownText").GetComponent<TextMeshProUGUI>();

            e.chargeTMP = e.obj.transform.Find("Charge").GetComponent<TextMeshProUGUI>();

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

            if (e.uiScript.IsPointerDown)
            {
                OnClickedItem?.Invoke(e.ItemIndex);
                _lastClickedItemIndex = e.ItemIndex;
            }
        }

        public void SetOwner(Creature c)
        {
            FillItem(c.spells.Count);
            for (int i = 0; i < c.spells.Count; i++)
            {
                SetIcon(this[i], c.spells[i].iconTextureID);
            }
        }

        public void SetCooling(Creature c)
        {
            for (int i = 0; i < c.spells.Count; ++i)
            {
                this[i].coolingTimeRemain = c.spells[i].spellCoolingdown.CoolingRemain;
                this[i].coolingTimeSet = c.spells[i].spellCoolingdown.CoolingSet;
            }
        }

        /// <summary>
        /// 设置e的物品贴图和材质
        /// </summary>
        /// <param name="e"></param>
        public void SetIcon(SpellViewItem e, int iconTextureID)
        {
            e.iconTextureID = iconTextureID;

            var image = e.uiScript.GetComponent<Image>();

            e.iconMaterial = new Material(image.material);
            image.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureID));

            e.iconMaterial.SetTexture("_Target", texture);
        }

        public int LastClickedItemIndex => _lastClickedItemIndex;
    }
}
