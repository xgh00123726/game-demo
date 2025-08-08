using GameBase.Resources;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BuffPanel : UObjEntitySys<BuffItem, SimpleEntityContainer, GameObject, BuffPanel>
    {
        public float buffWidth = 30f;
        public float buffHeight = 30f;
        public float buffInterval = 1.5f;
        public float maxPanelWidth = 1000f;

        public GameObject panel;


        public static Vector3 BuffPositionDelta(int index)
        {
            return new Vector3(32 * index, 0, 0);
        }

        protected override GameObject InstantiateObj(BuffItem e)
        {
            var obj = GameObject.Instantiate(ResourcesLoader.GetPrefab(e.ObjID));

            obj.transform.SetParent(panel.transform, false);

            e.stackNumTMP = obj.transform.Find("StackNum").GetComponent<TextMeshProUGUI>();

            e.image = obj.transform.Find("Icon").GetComponent<Image>();
            e.iconMaterial = new Material(e.image.material);
            e.image.material = e.iconMaterial;

            return obj;
        }

        protected override void AfterInstantiateEUObject(BuffItem e)
        {
            var texture = GameObject.Instantiate(ResourcesLoader.GetTexture2D(e.textureID));

            e.Obj.SetActive(true);
            XLogger.Instance.Log(e.image);
            XLogger.Instance.Log(e.image.material);
            XLogger.Instance.Log(e.iconMaterial);
            e.iconMaterial.SetTexture("_Target", texture);
        }

        protected override void BeforeReleaseEUObject(BuffItem e)
        {
            e.Obj.SetActive(false);
            XLogger.Instance.Color(Color.blue).Log(e.image);
            XLogger.Instance.Color(Color.blue).Log(e.image.material);
            XLogger.Instance.Color(Color.blue).Log(e.iconMaterial);
        }

        protected override void UpdateEntity(BuffItem e)
        {
            if (!e.bindBuff.Alive)
            {
                RemoveEntity(e);
            }

            e.iconMaterial.SetFloat("_MaskFull", e.bindBuff.DurationRemain / e.bindBuff.DurationSet);

            
            //e.Obj.transform.localPosition = BuffPositionDelta(idx++);
        }

        protected override void Awake()
        {
            base.Awake();

            // 10,Prefabs/UI/BuffPanel
            panel = GameObject.Instantiate(ResourcesLoader.GetPrefab(10));
            panel.transform.SetParent(RootCanvas.Instance.transform, false);
        }
    }
}
