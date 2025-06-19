using System.Collections.Generic;
using GameBase.Tools;
using UnityEngine;

namespace GameBase.XCard
{
    public class LivingBodyMgr : MonoBehaviour
    {
        private List<LivingBody> _currents;
        public List<LivingBody> Currents => _currents;
    }
}
