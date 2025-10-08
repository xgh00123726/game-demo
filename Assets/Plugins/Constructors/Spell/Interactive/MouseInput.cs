using GameBase.GCamera;
using GameBase.Indicators;
using GameBase.Spells;
using GameBase.Tools;
using UnityEngine;

namespace Constructor.Spells.Interactive
{
    public class MouseInput : ISpellInteractive, IDotInput
    {
        public IndicatorType indicatorType;
        public float length = 10;
        public float radius;

        private Vector3 _position;
        
        public Vector3 Position => _position;

        public static MouseInput Get(int id)
        {
            return new MouseInput();
        }

        void ISpellInteractive.OnTrig()
        {
            _position = CameraSys.MouseHitPosition;
        }
    }
}
