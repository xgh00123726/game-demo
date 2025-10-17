using GameBase.EntitySystem;
using GameBase.Tools;
using GameBase.Triggers;
using UnityEngine;

namespace Instance
{
    public class TriggerMonitor : SingletonInstance<TriggerMonitor>
    {
        protected override void Update()
        {
            foreach (var t in TriggerSys.Instance.Entities)
            {
            }
        }
    }
}
