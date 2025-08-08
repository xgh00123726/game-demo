using GameBase.Buffs;
using GameBase.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.UI
{
    public class BuffItem : IUEntity<GameObject>
    {

        public IViewableBuff bindBuff;
        internal int textureID;
        internal TextMeshProUGUI stackNumTMP;
        internal Material iconMaterial;
        internal Image image;

        public GameObject Obj { get; set; }

        public int ObjID { get; set; } = 9;

        public int InstanceID {  get; set; }
    }
}
