using GameBase.Indicators;
using GameBase.Tools;
using UnityEngine;

namespace Instance.Indicators
{
    public class RectIndicator : Indicator
    {
        public RectIndicator()
        {
            ObjID = 21;
            textureID = 11;
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
    }
}
