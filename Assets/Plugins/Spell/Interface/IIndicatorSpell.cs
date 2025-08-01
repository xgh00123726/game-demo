using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Spell
{
    public interface IIndicatorSpell
    {
        void Show();
        void Move();
        void Hide();
    }
}
