using GameBase.Resources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameBase.UI
{
    public class SpellPanel : BaseUI
    {
        private static GameObject _UIComponents;
        private static List<SpellItem> _childs = new List<SpellItem>();
        public static List<SpellItem> Childs => _childs;
        public static SpellItem AddItem()
        {
            // 11,Prefabs / UI / SpellItem
            var ui = GameObject.Instantiate(ResourcesLoader.GetPrefab(11)).AddComponent<SpellItem>();
            ui.transform.SetParent(_UIComponents.transform, false);
            ui.GetComponent<RectTransform>().position = new Vector2(540 + 120 * _childs.Count, 75);

            _childs.Add(ui);
            return ui;
        }

        private void Awake()
        {
            _UIComponents = transform.Find("UIComponents").gameObject;
            if (_UIComponents == null)
            {
                Debug.LogWarning("A panel must has a UIComponents");
            }
            Assert.IsNotNull(_UIComponents);
        }
    }
}
