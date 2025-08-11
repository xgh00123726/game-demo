using GameBase.Tools;
using System;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public abstract class BasePanelItem : IUEntity<BaseUI>
    {
        internal Material iconMaterial;

        internal abstract int IconTexureID { get; }
        public Action<BasePanelItem> AfterInstantiateUObjectDelegate;
        public BaseUI Obj { get; set; }
        public int ObjID { get; set; }
        public int InstanceID { get; set; }
    }
}
