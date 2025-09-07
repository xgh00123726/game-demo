using GameBase.Modify;
using GameBase.UI;
using Instance.Modify;

public partial class Player
{
    private void ShowAttrUI(Modifyable<float> attr, int texureID)
    {
        var attrUI = AttrViewPanel.Instance.NewEntity((AttrViewItem e) =>
        {
            e.bindAttr = new ViewableAttr<float>(attr, texureID);
        });
    }

    public void AttrUIInit()
    {
        var e = AttrViewPanel.Instance;

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
