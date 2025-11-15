using GameBase.Math;
using GameBase.Tools;
using GameBase.Triggers;
using System.Collections.Generic;
using UnityEngine;

namespace Instance
{
    public class TriggerGizmos : MonoBehaviour
    {
        public class DelayDraw
        {
            public IShape2D shape;
            public float instantiateTime;
        }

        private List<DelayDraw> _delayDraws = new();
        private List<DelayDraw> _delayDrawsNeedRemove = new();
        private IShape2D testShape;

        private void Start()
        {
            testShape = new GMath.Line(new Vector2(0, 0), new Vector2(1, 1), 3)
            {
                width = 1
            };
        }

        private void DrawTestGizmos()
        {
            var c = CreatureSys.Instance.GetCreatureFromID(0);
            if (c != null && testShape.Contains(c.Position))
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.green;
            }
            GizmosAppend.DrawShape(testShape, GizmosCfg.drawY);
        }

        void OnDrawGizmos()
        {
            if (!GizmosCfg.isDrawGizmos)
            {
                return;
            }
            //DrawTestGizmos();

            foreach (var e in TriggerSys.Instance.Entities)
            {
                if (e.trigStyle == TrigStyle.Once)
                {
                    if (e.shape is GMath.Circle circle)
                    {
                        _delayDraws.Add(new DelayDraw()
                        {
                            shape = circle,
                            instantiateTime = Time.time
                        });
                    }
                    else if (e.shape is GMath.Line line)
                    {
                        _delayDraws.Add(new DelayDraw()
                        {
                            shape = line,
                            instantiateTime = Time.time
                        });
                    }
                }
                else
                {
                    Gizmos.color = Color.green;
                    GizmosAppend.DrawShape(e.shape, GizmosCfg.drawY);
                }
            }
            foreach (var e in _delayDraws)
            {
                if (Time.time < e.instantiateTime + 1f)
                {
                    Gizmos.color = Color.red;
                    GizmosAppend.DrawShape(e.shape, GizmosCfg.drawY);
                }
                else
                {
                    _delayDrawsNeedRemove.Add(e);
                }
            }
            foreach (var e in _delayDrawsNeedRemove)
            {
                _delayDraws.Remove(e);
            }
            _delayDrawsNeedRemove.Clear();
        }
    }
}
