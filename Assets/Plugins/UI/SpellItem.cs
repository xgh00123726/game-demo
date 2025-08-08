using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GameBase.Resources;

namespace GameBase.UI
{
    public class SpellItem : BaseUI
    {
        public Spell.Spell spell;

        private GameObject _iconGO;                  // 图标的gameobjcet
        private Transform _chargeGO;                 // 充能数字的gameobject
        private Transform _coolingTimeTextGO;        // 冷却时间文本的object
        private TextMeshProUGUI _timeTMP;            // 冷却时间的text对象
        private TextMeshProUGUI _chargeTMP;          // 充能层数的text对象

        internal CanvasRenderer canvasRenderer;
        internal MeshRenderer meshRenderer;
        internal Image image;

        public void SetIcon(int id)
        {
            _iconGO.GetComponent<Image>().sprite = GameObject.Instantiate(ResourcesLoader.GetSprite(id));
        }

        protected virtual void Awake()
        {
            _chargeGO = transform.Find("Charge");
            _iconGO = transform.Find("Icon").gameObject;

            _timeTMP = transform.Find("CoolingDownText").GetComponent<TextMeshProUGUI>();
            _chargeTMP = _chargeGO.GetComponent<TextMeshProUGUI>();
            image = _iconGO.GetComponent<Image>();

            canvasRenderer = _iconGO.GetComponent<CanvasRenderer>();
            
            image.material  = new Material(image.material);
        }

        private void Update()
        {
            float coolingTimeRemain = spell.CoolingTimeRemain;

            //_maskImage.fillAmount = coolingTimeRemain / spell.coolingTimeSet;
            float fullVal = coolingTimeRemain / spell.coolingTimeSet;
            image.material.SetFloat("_MaskFull", fullVal);

            string coolingText = string.Empty;
            if (coolingTimeRemain > 1)
            {
                coolingText = ((int)coolingTimeRemain).ToString();
            }
            else if (coolingTimeRemain > 0)
            {
                coolingText = $".{(int)(coolingTimeRemain * 10)}";
            }
            _timeTMP.text = coolingText;
        }
    }
}
