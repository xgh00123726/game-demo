using UnityEngine;
using GameBase.Spell;
using GameBase.GCamera;
using GameBase.Effects;

namespace GameBase.Instance
{
    // 圆形的技能指示器
    public class SpellCircleIndicator : CircleIndicator,
        IIndicatorSpell
    {
        void IIndicatorSpell.Hide()
        {
            Hide();
        }

        void IIndicatorSpell.Move()
        {
        }

        void IIndicatorSpell.Show()
        {
        }
    }
}
