using GameBase.Indicators;
using GameBase.Spells;
using UnityEngine;

namespace Instance.Indicators
{
    public class LinearIndicator : Indicator,
        ISpellIndicator
    {
        public LinearIndicator()
        {
            ObjID = 21;
            textureID = 11;
        }

        protected bool _fixedLength = false;
        public float Length
        {
            set
            {
                _fixedLength = true;
                SetLength(value);
            }
        }

        private void SetLength(float value)
        {
            Size = new Vector3(0.2f, value);
            Pivot = new Vector3(0f, value / 2, 0f);
        }

        void ISpellIndicator.Hide()
        {
            Obj.SetActive(false);
        }

        void ISpellIndicator.Update(ISpeller speller, Vector3 position)
        {
            Obj.transform.position = speller.Position;
            Vector3 dir = position - speller.Position;
            float x = dir.x;
            float z = dir.z;
            float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
            Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            if (!_fixedLength)
            {
                SetLength(dir.magnitude);
            }
        }

        void ISpellIndicator.Show()
        {
            Obj.SetActive(true);
        }
    }
}
