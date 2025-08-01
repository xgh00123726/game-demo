using System.Collections;
using System.Collections.Generic;
using GameBase.Math;
using UnityEngine;

namespace GameBase.Editable
{
    public class EditableRect : EditableShape
    {
        public Rect shape;
        public float yDelta;
        public override bool IsInShape(float x, float y)
        {
            return shape.Contains(new Vector2(x, y));
        }
        public override Vector2 ShapeSpcaceToWorldVec2(float x, float y)
        {
            return new Vector2(transform.position.x + shape.width / 2 * (x + 1) + shape.x,
                transform.position.z + shape.height / 2 * (y + 1) + shape.y);
        }

        public override Vector3 ShapeSpcaceToWorldVec3(float x, float y)
        {
            return new Vector3(transform.position.x + shape.width / 2 * (x + 1) + shape.x,
                transform.position.y + yDelta,
                transform.position.z + shape.height / 2 * (y + 1) + shape.y);
        }

        private void OnDrawGizmosSelected()
        {
            float x = transform.position.x;
            float y = transform.position.y;
            float z = transform.position.z;
            Gizmos.color = Color.green;
            Vector3 p1 = new Vector3(x + shape.xMin, y + yDelta, z + shape.yMin);
            Vector3 p2 = new Vector3(x + shape.xMin, y + yDelta, z + shape.yMax);
            Vector3 p3 = new Vector3(x + shape.xMax, y + yDelta, z + shape.yMax);
            Vector3 p4 = new Vector3(x + shape.xMax, y + yDelta, z + shape.yMin);
            Gizmos.DrawLine(p1, p2);
            Gizmos.DrawLine(p2, p3);
            Gizmos.DrawLine(p3, p4);
            Gizmos.DrawLine(p4, p1);
        }
    }
}
