using System.Collections.Generic;
using UnityEngine;
using GameBase.Resources;

namespace GameBase.Effects
{
    public class MoveIndicator
    {
        private static MoveIndicator _instance;
        public static MoveIndicator Instance
        {
            get
            {
                _instance ??= new MoveIndicator();
                return _instance;
            }
        }

        public void PlayAt(Vector3 pos)
        {
            MoveIndicatorEffect effect = new MoveIndicatorEffect();
            effect.PlayAt(pos);
        }

        private MoveIndicator()
        {
        }

    }
}
