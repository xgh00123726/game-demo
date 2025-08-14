using GameBase.Indicators;
using GameBase.Spells;
using UnityEngine;

namespace Instance.Indicators
{
    public class LinearIndicator : Indicator,
        IIndicator
    {
        public LinearIndicator()
        {
            ObjID = 21;
            textureID = 11;
        }

        public float Length
        {
            set
            {
                Size = new Vector3(0.2f, value);
                Pivot = new Vector3(0f, value / 2, 0f);
            }
        }

        public Vector3 Dir
        {
            set
            {
                float x = value.x;
                float z = value.z;
                float angle = Vector2.SignedAngle(new Vector2(x, z), new Vector2(0, 1));
                Obj.transform.rotation = Quaternion.Euler(90, angle, 0);
            }
            get
            {
                return Obj.transform.right;
            }
        }

        void IIndicator.Hide()
        {
            visible = false;
        }

        void IIndicator.Show()
        {
            visible = true;
        }
    }
}
