using System.Collections.Generic;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class SpellPanel : BaseUI
    {
        internal static SpellPanel _instance;
        public static SpellPanel Instance => _instance;
        public Transform UIComponents;
        private List<SpellUI> _childs = new List<SpellUI>();
        public List<SpellUI> Childs => _childs;
        public SpellUI AddItem()
        {
            var ui = PrefabMgr.Instance.GetNotfromPool<SpellUI>();
            ui.transform.SetParent(transform.Find("UIComponents"), false);
            ui.GetComponent<RectTransform>().position = new Vector2(540 + 120 * _childs.Count, 75);

            _childs.Add(ui);
            return ui;
        }

        private void Awake()
        {
            UIComponents = transform.Find("UIComponents");
        }
    }
}
