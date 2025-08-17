using GameBase.Modify;
using GameBase.UI;
using Instance.Modify;

public partial class Player
{
    private DetailUI attrDetailUI;

    private void ShowAttrUI(Modifyable<float> attr, int texureID)
    {
        var attrUI = AttrPanel.Instance.NewEntity<AttrItem>();
        var viewable = new ViewableAttr<float>(attr, texureID);
        attrUI.bindAttr = viewable;

        attrUI.AfterInstantiateUObjectDelegate = (BasePanelItem item) =>
        {
            attrDetailUI.detailables.Add(attrUI);
        };
    }

    public void AttrUIInit()
    {
        attrDetailUI = DetailUISys.Instance.NewEntity<DetailUI>();

        ShowAttrUI(charater.ModifyableContainer["universal"], 19);
        ShowAttrUI(charater.ModifyableContainer["moveSpeed"], 20);
        ShowAttrUI(charater.ModifyableContainer["strength"], 18);
        ShowAttrUI(charater.ModifyableContainer["attackSpeed"], 21);
        ShowAttrUI(charater.ModifyableContainer["agility"], 14);
        ShowAttrUI(charater.ModifyableContainer["defense"], 13);
        ShowAttrUI(charater.ModifyableContainer["intelligence"], 15);
        ShowAttrUI(charater.ModifyableContainer["damage"], 12);
    }
}
