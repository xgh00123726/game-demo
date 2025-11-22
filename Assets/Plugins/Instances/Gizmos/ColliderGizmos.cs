using GameBase.Math;
using GameBase.Move;
using GameBase.Tools;
using UnityEngine;

namespace Instance
{
    public class ColliderGizmos : MonoBehaviour
    {
        private void DrawCircleCollide(GameBase.Move.Collider c)
        {
            if (!GizmosCfg.IsDrawCollider)
            {
                return;
            }
            if (c is not CircleCollider cc)
            {
                return;
            }

            if (cc.IsCollide)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            GizmosAppend.DrawCircle(new GMath.Circle
            {
                c = new Vector2(cc.Position.x, cc.Position.y),
                r = cc.r,
            }, GizmosCfg.drawY);
        }

        private void DrawRectCollider(GameBase.Move.Collider c)
        {
            if (!GizmosCfg.IsDrawCollider)
            {
                return;
            }
            if (c is not RectCollider rc)
            {
                return;
            }

            if (rc.IsCollide)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            GizmosAppend.DrawRect(new GMath.Rect2D()
            {
                rect = Rect.MinMaxRect(rc.XMin, rc.YMin, rc.XMax, rc.YMax)
            }, GizmosCfg.drawY);
        }

        private void DrawForce(GameBase.Move.Collider c)
        {
            if (!GizmosCfg.IsDrawForce)
            {
                return;
            }

            if (!c.IsCollide)
            {
                return;
            }

            Vector3 center = new Vector3(c.Position.x, GizmosCfg.drawY, c.Position.y);
            Vector3 dir = new Vector3(c.Force.x, 0, c.Force.y) * GizmosCfg.ForceLenTimes;

            Gizmos.color = new Color(0.85f, 0.29f, 0.77f, 1f);
            for (int i = 0; i < GizmosCfg.drawTimes; ++i)
            {
                Gizmos.DrawLine(center, center + dir);
            }
        }

        private void OnDrawGizmos()
        {
            if (!GizmosCfg.IsDrawGizmos)
            {
                return;
            }

            foreach (var c in CollideSys.Instance.Entities)
            {
                DrawCircleCollide(c);
                DrawRectCollider(c);
                DrawForce(c);
            }
        }
    }
}
