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
            var item = BuffPanel.Instance.NewEntity((BuffItem e) =>
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
        var e = BuffPanel.Instance;
        charater.BuffContainer.OnAddBuff = OnAddBuff;
        charater.BuffContainer.OnRemoveBuff = OnRemoveBuff;
    }
}
