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

        ShowAttrUI(character.ModifyableContainer["universal"], 19);
        ShowAttrUI(character.ModifyableContainer["moveSpeed"], 20);
        ShowAttrUI(character.ModifyableContainer["strength"], 18);
        ShowAttrUI(character.ModifyableContainer["attackSpeed"], 21);
        ShowAttrUI(character.ModifyableContainer["agility"], 14);
        ShowAttrUI(character.ModifyableContainer["defense"], 13);
        ShowAttrUI(character.ModifyableContainer["intelligence"], 15);
        ShowAttrUI(character.ModifyableContainer["damage"], 12);
    }
}
