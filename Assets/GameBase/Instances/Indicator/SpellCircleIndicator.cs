using UnityEngine;
using GameBase.Spell;
using GameBase.GCamera;

namespace GameBase.Effects
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
            PlayAt(PlayerCamera.MouseHitPoint);
        }

        void IIndicatorSpell.Show()
        {
            PlayAt(PlayerCamera.MouseHitPoint);
        }
    }
}
