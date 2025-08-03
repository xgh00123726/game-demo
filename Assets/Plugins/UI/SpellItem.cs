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
        private Transform _coolingTimeMaskGO;        // 冷却时间遮罩的object
        private Image _maskImage;                    // 遮罩的image对象
        private TextMeshProUGUI _timeTMP;            // 冷却时间的text对象
        private TextMeshProUGUI _chargeTMP;          // 充能层数的text对象


        public void SetIcon(int id)
        {
            _iconGO.GetComponent<Image>().sprite = GameObject.Instantiate(ResourcesLoader.GetSprite(id));
        }

        protected virtual void Awake()
        {
            _chargeGO = transform.Find("Charge");
            _iconGO = transform.Find("Icon").gameObject;
            _coolingTimeTextGO = transform.Find("CoolingDownText");
            _coolingTimeMaskGO = transform.Find("CoolingDownMask");

            _maskImage = _coolingTimeMaskGO.GetComponent<Image>();
            _timeTMP = _coolingTimeTextGO.GetComponent<TextMeshProUGUI>();
            _chargeTMP = _chargeGO.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            float coolingTimeRemain = spell.CoolingTimeRemain;
            _maskImage.fillAmount = coolingTimeRemain / spell.coolingTimeSet;

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
