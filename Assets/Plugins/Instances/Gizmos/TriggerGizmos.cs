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
            public IShape2D Shape { get; set; }
            public float InstantiateTime {  get; set; }
        }

        private List<DelayDraw> _delayDraws = new();
        private List<DelayDraw> _delayDrawsNeedRemove = new();

        private void Start()
        {
            TriggerSys.Instance.OnTrig((e) =>
            {
                if (e.TrigStyle == TrigStyle.Once)
                {
                    if (e.Shape is GMath.Circle circle)
                    {
                        _delayDraws.Add(new DelayDraw()
                        {
                            Shape = circle,
                            InstantiateTime = Time.time
                        });
                    }
                    else if (e.Shape is GMath.Line line)
                    {
                        _delayDraws.Add(new DelayDraw()
                        {
                            Shape = line,
                            InstantiateTime = Time.time
                        });
                    }
                }
            });
        }

        void OnDrawGizmos()
        {
            if (!GizmosCfg.IsDrawGizmos)
            {
                return;
            }

            foreach (var e in TriggerSys.Instance.Entities)
            {
                if(e.TrigStyle != TrigStyle.Once)
                {
                    Gizmos.color = Color.green;
                    GizmosAppend.DrawShape(e.Shape, GizmosCfg.drawY);
                }
            }


            foreach (var e in _delayDraws)
            {
                if (Time.time < e.InstantiateTime + 1f)
                {
                    Gizmos.color = Color.red;
                    GizmosAppend.DrawShape(e.Shape, GizmosCfg.drawY);
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
