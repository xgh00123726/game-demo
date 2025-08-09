using GameBase.Tools;
using TMPro;
using UnityEngine;

namespace GameBase.UI
{
    public abstract class BasePanelItem : IUEntity<GameObject>
    {
        internal Material iconMaterial;

        internal abstract int IconTexureID { get; }
        public GameObject Obj { get; set; }
        public int ObjID { get; set; }
        int IEntity.InstanceID { get; set; }
    }
}
