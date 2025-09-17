using GameBase.UI;
using UnityEngine;

namespace Instance.UI.MVC
{
    public class SpellActionModifierDragableControl : MVCDragableControl<SpellActionModifierViewItem>
    {
        public CommonInventoryController CC => CommonInventoryController.Instance;
        public SpellActionModifierController SC => SpellActionModifierController.Instance;

        protected override void OnExitDrag(SpellActionModifierViewItem dragedItem, int dragedIndex)
        {
            if (SC.TryGetItemUI(Input.mousePosition, out var eEntity, out var eIndex))
            {
                SC.Swap(dragedIndex, eIndex);
                eEntity.ShowIcon();
            }

            if (CC.TryGetItemUI(Input.mousePosition, out var cEntity, out var cIndex))
            {
                cEntity.SwapIconSprite(dragedItem);
                cEntity.ShowIcon();

                if (SC.TryGetData(dragedIndex, out var data))
                {
                    CC.AddItem(data, cIndex);
                    SC.RemoveItem(dragedIndex);
                }
            }
        }
    }
}
