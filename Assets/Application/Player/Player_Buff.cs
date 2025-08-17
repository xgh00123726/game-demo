using GameBase.Buffs;
using GameBase.Tools;
using GameBase.UI;
using Instance.Buffs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    private void OnAddBuff(Buff buff)
    {
        if (buff.uiStyle == UIStyle.Buff)
        {
            var item = BuffPanel.Instance.NewEntity<BuffItem>();
            var viewable = new ViewableBuff(buff, 0);
            item.bindBuff = viewable;
        }
    }

    private void OnRemoveBuff(Buff buff)
    {
    }

    private void BuffUIInit()
    {
        charater.BuffContainer.OnAddBuff = OnAddBuff;
        charater.BuffContainer.OnRemoveBuff = OnRemoveBuff;
    }
}
