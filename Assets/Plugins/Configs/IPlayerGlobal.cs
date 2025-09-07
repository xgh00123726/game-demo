using UnityEngine;

namespace GameBase.Infos
{
    public interface IPlayerGlobal
    {
        Vector3 Position { get; }
        int Gold { get; set; }
    }
}
