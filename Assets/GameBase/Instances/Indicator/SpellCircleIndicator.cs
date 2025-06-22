using UnityEngine;
using GameBase.Spell;
using GameBase.GCamera;

namespace GameBase.Effects
{
    // 圆形的技能指示器
    public class SpellCircleIndicator : CircleIndicator,
        IIndicatorCircleSpell
    {
        float IIndicatorCircleSpell.Radius 
        { 
            get => Radius; 
            set => Radius = value; 
        }

        void IIndicatorSpell.Hide()
        {
            Hide();
        }

        void IIndicatorSpell.Move()
        {
            PlayAt(PlayerCamera.MouseHitPoint + new Vector3(0, 2, 0));
        }

        void IIndicatorSpell.Show()
        {
            PlayAt(PlayerCamera.MouseHitPoint + new Vector3(0, 2, 0));
        }
    }
}
