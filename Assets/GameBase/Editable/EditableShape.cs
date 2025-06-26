using GameBase.Math;
using UnityEngine;

namespace GameBase.Editable
{
    public abstract class EditableShape : MonoBehaviour
    {
        public abstract bool IsInShape(float x, float y);
        public abstract Vector2 ShapeSpcaceToWorldVec2(float x, float y);
        public abstract Vector3 ShapeSpcaceToWorldVec3(float x, float y);
    }
}
