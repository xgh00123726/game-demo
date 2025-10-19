using GameBase.EntitySystem;
using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBase.AI
{
    public class AISys : InheritableSys<BaseAI, AISys>
    {
        public AISys()
        {
            _fixedUpdate = true;
        }
        protected override void UpdateEntity(BaseAI e)
        {
            if (e.owner == null)
            {
                XLogger.Instance.Level(XLogger.LogLevel.Error)
                    .Log("ai has null owner");
                RemoveEntity(e);
                return;
            }
            if (Time.time > e.enableRecoverTime)
            {
                e.enable = true;
            }
            if (e.enable)
            {
                e.Update();
            }
        }
    }
}
