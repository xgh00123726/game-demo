using System.Collections.Generic;
using GameBase.Resources;
using UnityEngine;

namespace GameBase.UI
{
    public class SpellPanel : BasePanel
    {
        internal static SpellPanel _instance;
        public static SpellPanel Instance => _instance;
        public Transform UIComponents;
        private List<SpellItem> _childs = new List<SpellItem>();
        public List<SpellItem> Childs => _childs;
        public SpellItem AddItem()
        {
            var ui = PoolablePrefabMgr.GetNotfromPool<SpellItem>(PrefabType.UI);
            ui.transform.SetParent(_UIComponents.transform, false);
            ui.GetComponent<RectTransform>().position = new Vector2(540 + 120 * _childs.Count, 75);

            _childs.Add(ui);
            return ui;
        }
    }
}
