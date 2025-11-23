using GameBase.UI;

namespace Instance
{
    public class BonusSelectPanel : BaseViewPanel<BonusSelectItem, BonusSelectPanel>
    {
        protected override string PanelPrefabName => "UI/BonusSelectPanel.prefab";
        protected override string ItemPrefabName => "UI/BonusSelectItem.prefab";
    }
}
