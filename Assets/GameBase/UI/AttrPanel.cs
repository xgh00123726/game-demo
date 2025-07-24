using System.Collections.Generic;
using GameBase.Resources;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class AttrPanel : BasePanel
    {
        internal static AttrPanel _instance;
        public static AttrPanel Instance => _instance;

        private List<AttrItem> _childs = new List<AttrItem>();
        public List<AttrItem> Childs => _childs;
        private GameObject _attrsField;

        protected override void Awake()
        {
            base.Awake();
            _attrsField = _UIComponents.transform.Find("AttrsField").gameObject;
            Assert.IsNotNull(_attrsField);
        }

        public void ToggleShow()
        {
            _UIComponents.SetActive(!_UIComponents.activeSelf);
        }
        public void Hide()
        {
            _UIComponents.SetActive(false);
        }
        private int ItemY(int itemIndex)
        {
            return 500 - itemIndex * 50;
        }
        public AttrItem AddItem()
        {
            var item = PoolablePrefabMgr.GetNotfromPool<AttrItem>(PrefabType.UI);

            item.transform.parent = _attrsField.transform;
            item.transform.localPosition = new Vector3(0, ItemY(_childs.Count), 0);

            _childs.Add(item);

            return item;
        }
    }
}
