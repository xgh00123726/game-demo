using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;

public partial class Player
{
    BuffViewPanel _buffPanel;
    private void OnGetBuff(Buff buff)
    {
        var info = BuffDataBase.Instance[buff.id];
        if (info.type == BuffType.Common)
        {
            var item = _buffPanel.NewEntity();
            item.SetIcon(info.iconTextureID);
            item.SetOwner(buff);
        }
    }

    private void BuffInit()
    {
        _buffPanel = new BuffViewPanel();
        _buffPanel.layout = new DefaultLayout()
        {
            xInterval = 31.5f,
            yInterval = 31.5f,
            width = 1000
        };

        _buffPanel.Show();
        character.OnGetBuffAction += OnGetBuff;
    }

    private void BuffUpdate()
    {
        
    }
}
