using UnityEngine;

namespace GameBase.UI
{
    public interface IDetailable : IEnterExist
    {
        public int TexureID { get; }
        public DetailContent Content { get; }
        public bool IsPointerOn { get; }

        public Vector3 ShowPosition { get; }
    }
}
