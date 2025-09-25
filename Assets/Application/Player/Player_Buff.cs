using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;

public partial class Player
{
    BuffViewPanel _buffPanel;
    private void OnAddBuff(Buff buff)
    {
        if (buff.uiStyle == UIStyle.Buff)
        {
            var item = _buffPanel.NewEntity();
            item.SetIcon(buff.textureID);
            item.SetOwner(buff);
        }
    }

    private void OnRemoveBuff(Buff buff)
    {
    }

    private void BuffUIInit()
    {
        _buffPanel = BuffViewPanel.Instance;
        _buffPanel.layout = new DefaultLayout()
        {
            xInterval = 31.5f,
            yInterval = 31.5f,
            width = 1000
        };

        _buffPanel.Show();
        character.BuffContainer.OnAddBuff = OnAddBuff;
        character.BuffContainer.OnRemoveBuff = OnRemoveBuff;
    }
}
