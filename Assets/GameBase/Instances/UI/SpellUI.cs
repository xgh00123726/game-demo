using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameBase.UI
{
    public class SpellUI : BaseUI
    {
        public float _coolingTimeSet = 10;
        public float _coolingRemainPercent = 1;
        public float _coolingTimeRemain = 0;
        public int _charge = 0;
        public int _maxCharge = 1;

        public float CoolingTimeSet
        {
            get => _coolingTimeSet;
            set => _coolingTimeSet = value;
        }
        public float CoolingRemainPercent
        {
            get => _coolingRemainPercent;
            set
            {
                _coolingRemainPercent = Mathf.Clamp01(value);
                _coolingTimeRemain = _coolingRemainPercent * _coolingTimeSet;
                SetCooling();
            }
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

        private Transform _chargeGO;
        private Transform _coolingTimeTextGO;
        private Transform _coolingTimeMaskGO;
        private Image _maskImage;
        private TextMeshProUGUI _timeTMP;
        private TextMeshProUGUI _chargeTMP;

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

        protected virtual void Start()
        {
            _chargeGO = transform.Find("Charge");
            _coolingTimeTextGO = transform.Find("CoolingDownText");
            _coolingTimeMaskGO = transform.Find("CoolingDownMask");

            _maskImage = _coolingTimeMaskGO.GetComponent<Image>();
            _timeTMP = _coolingTimeTextGO.GetComponent<TextMeshProUGUI>();
            _chargeTMP = _chargeGO.GetComponent<TextMeshProUGUI>();
        }
    }
}
