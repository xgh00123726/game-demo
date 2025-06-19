using UnityEngine;
using UnityEngine.UI;
using GameBase.Buff;
using TMPro;
using GameBase.Tools;
using GameBase.Resources;

namespace GameBase.UI
{
    public class BuffItem : BaseUI,
        IPoolableObject
    {
        private GameObject _iconGO;
        private GameObject _maskGO;
        private GameObject _stackNumGO;

        private TextMeshProUGUI _stackNumTMP;
        private Image _maskGOImage;
        internal IViewableBuff _bindBuff;
        internal bool _isReleased = false;

        // 0: ÎÞmask
        // 1: ÌîÂú
        public float FillAmount
        {
            get => _maskGOImage.fillAmount;
            set => _maskGOImage.fillAmount = value;
        }
        private void Awake()
        {
            _iconGO = transform.Find("Icon").gameObject;
            _maskGO = transform.Find("Mask").gameObject;
            _stackNumGO = transform.Find("StackNum").gameObject;

            _maskGOImage = _maskGO.GetComponent<Image>();
            _stackNumTMP = _stackNumGO.GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (_isReleased) return;
            if (_bindBuff.DurationRemain <= 0)
            {
                PoolableMonoMgr<BuffItem>.Instance.Release(this);
                _isReleased = true;
            }
            _maskGOImage.fillAmount = 1 - _bindBuff.DurationRemain / _bindBuff.DurationSet;
            _stackNumTMP.text = _bindBuff.StackNum.ToString();
        }

        public void Bind(IViewableBuff buff)
        {
            _bindBuff = buff;
        }

        void IPoolableObject.OnInstantiate()
        {
            _maskGOImage.fillAmount = 0;
            _iconGO.SetActive(true);
            _maskGO.SetActive(true);
            _stackNumGO.SetActive(true);
            _isReleased = false;
        }

        void IPoolableObject.OnRelease()
        {
            _iconGO.SetActive(false);
            _maskGO.SetActive(false);
            _stackNumGO.SetActive(false);
            _isReleased = true;
        }
    }
}
