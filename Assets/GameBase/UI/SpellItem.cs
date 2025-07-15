using UnityEngine;
using UnityEngine.UI;
using GameBase.Tools;
using TMPro;
using GameBase.Resources;

namespace GameBase.UI
{
    public class SpellItem : BaseUI
    {
        public float _coolingTimeSet = 10;           // 图标的冷却时间
        public float _coolingRemainPercent = 1;      // 图标冷却时间百分比
        public float _coolingTimeRemain = 0;         // 图标剩余冷却时间
        public int _charge = 0;                      // 当前充能
        public int _maxCharge = 1;                   // 最大充能

        private GameObject _iconGO;                  // 图标的gameobjcet
        private Transform _chargeGO;                 // 充能数字的gameobject
        private Transform _coolingTimeTextGO;        // 冷却时间文本的object
        private Transform _coolingTimeMaskGO;        // 冷却时间遮罩的object
        private Image _maskImage;                    // 遮罩的image对象
        private TextMeshProUGUI _timeTMP;            // 冷却时间的text对象
        private TextMeshProUGUI _chargeTMP;          // 充能层数的text对象



        public float CoolingTimeSet
        {
            get => _coolingTimeSet;
            set => _coolingTimeSet = value;
        }
        public float CoolingTimeRemain
        {
            get => _coolingTimeRemain;
            set
            {
                _coolingTimeRemain = Mathf.Clamp(value, 0, _coolingTimeSet);
                _coolingRemainPercent = _coolingTimeRemain / _coolingTimeSet;
                SetCooling();
            }
        }
        public int Charge
        {
            get => _charge;
            set
            {
                _charge = value;
                _chargeTMP.text = _charge.ToString();
            }
        }
        public int MaxCharge
        {
            get => _maxCharge;
            set
            {
                _maxCharge = value;
                if (_maxCharge > 1)
                {
                    _chargeGO.gameObject.SetActive(true);
                }
                else
                {
                    _chargeGO.gameObject.SetActive(false);
                }
            }
        }


        private void SetCooling()
        {
            string _coolingText = string.Empty;
            if (_coolingTimeRemain > 1)
            {
                _coolingText = ((int)_coolingTimeRemain).ToString();
            }
            else if (_coolingTimeRemain > 0)
            {
                _coolingText = $".{(int)(_coolingTimeRemain * 10)}";
            }
            _timeTMP.text = _coolingText;
            _maskImage.fillAmount = _coolingRemainPercent;
        }

        public void SetIcon(string name)
        {
            _iconGO.GetComponent<Image>().sprite = ResourceMgr.InstantiateSprite("Spell_ArrowRain");
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

            MaxCharge = _maxCharge;
        }
    }
}
