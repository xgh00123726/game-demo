using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;
using Instance.Buffs;

public partial class Player
{
    private void OnAddBuff(Buff buff)
    {
        if (buff.uiStyle == UIStyle.Buff)
        {
            var item = BuffViewPanel.Instance.NewEntity((BuffViewItem e) =>
            {
                e.iconTextureID = buff.textureID;
                e.bindBuff = new ViewableBuff(buff);
            });
        }
    }

    private void OnRemoveBuff(Buff buff)
    {
    }

    private void BuffUIInit()
    {
        var e = BuffViewPanel.Instance;
        character.BuffContainer.OnAddBuff = OnAddBuff;
        character.BuffContainer.OnRemoveBuff = OnRemoveBuff;
    }
}
