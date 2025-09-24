using GameBase.Infos;
using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BuffViewPanel : BaseViewPanel<BuffViewItem>
    {
        //protected override void AfterInstantiateEUObject(BuffViewItem e)
        //{
        //    var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureID)); 
        //    e.iconMaterial.SetTexture("_Target", texture);
        //}
        public BuffViewPanel(int prefabID = 10,
            int defaultObjID = 9) : base(
            prefabID,
            defaultObjID)
        {
        }
        protected override BaseUI InstantiateObj(BuffViewItem e)
        {
            var obj = base.InstantiateObj(e);

            e.iconMaterial = new Material(e.iconImage.material);
            e.iconImage.material = e.iconMaterial;

            e.iconMaterial.SetFloat("_Dir1", -1f);
            e.iconMaterial.SetFloat("_Dir2", -1f);

            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.iconTextureID));

            e.iconMaterial.SetTexture("_Target", texture);

            e.stackNumTMP = obj.transform.Find("StackNum").GetComponent<TextMeshProUGUI>();

            return obj;
        }

        protected override void UpdateEntity(BuffViewItem e)
        {
            base.UpdateEntity(e);

            if (!e.removeFlag)
            {
                RemoveEntity(e);
            }

            e.iconMaterial.SetFloat("_MaskFull", 1 - e.bindBuff.DurationRemain / e.bindBuff.DurationSet);
        }
    }
}
