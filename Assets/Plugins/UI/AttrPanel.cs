using System.Collections.Generic;
using GameBase.Resources;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class AttrPanel : BaseUI
    {
        private static GameObject _UIComponents;

        private static List<AttrItem> _childs = new List<AttrItem>();
        public static List<AttrItem> Childs => _childs;
        private static GameObject _attrsField;

        private void Awake()
        {
            _UIComponents = transform.Find("UIComponents").gameObject;
            if (_UIComponents == null)
            {
                Debug.LogWarning("A panel must has a UIComponents");
            }
            Assert.IsNotNull(_UIComponents);
            _attrsField = _UIComponents.transform.Find("AttrsField").gameObject;
            Assert.IsNotNull(_attrsField);
        }

        public static void ToggleShow()
        {
            _UIComponents.SetActive(!_UIComponents.activeSelf);
        }
        public static void Hide()
        {
            _UIComponents.SetActive(false);
        }
        private static int ItemY(int itemIndex)
        {
            return 500 - itemIndex * 50;
        }
        public static AttrItem AddItem()
        {
            // 7,Prefabs/UI/AttrItem
            var item = GameObject.Instantiate(ResourcesLoader.GetPrefab(7)).AddComponent<AttrItem>();

            item.transform.parent = _attrsField.transform;
            item.transform.localPosition = new Vector3(0, ItemY(_childs.Count), 0);

            _childs.Add(item);

            return item;
        }
    }
}
