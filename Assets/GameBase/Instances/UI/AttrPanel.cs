using System.Collections.Generic;
using GameBase.Resources;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class AttrPanel : BaseUI
    {
        internal static AttrPanel _instance;
        public static AttrPanel Instance => _instance;

        private List<AttrItem> _childs = new List<AttrItem>();
        public List<AttrItem> Childs => _childs;
        private GameObject _uiComponents;
        private GameObject _attrsField;

        private void Awake()
        {
            _uiComponents = transform.Find("UIComponents").gameObject;
            Assert.IsNotNull(_uiComponents);

            _attrsField = _uiComponents.transform.Find("AttrsField").gameObject;
            Assert.IsNotNull(_attrsField);
        }

        public void ToggleShow()
        {
            _uiComponents.SetActive(!_uiComponents.activeSelf);
        }
        private int ItemY(int itemIndex)
        {
            return 500 - itemIndex * 50;
        }
        public AttrItem AddItem()
        {
            var item = PrefabMgr.Instance.GetNotfromPool<AttrItem>();

            item.transform.parent = _attrsField.transform;
            item.transform.localPosition = new Vector3(0, ItemY(_childs.Count), 0);

            _childs.Add(item);

            return item;
        }
    }
}
