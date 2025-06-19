using System.Collections;
using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.Effects
{
    public class EffectMgr
    {
        private static EffectMgr _instance;
        public static EffectMgr Instance
        {
            get
            {
                _instance ??= new EffectMgr();
                return _instance;
            }
        }
    }
}
