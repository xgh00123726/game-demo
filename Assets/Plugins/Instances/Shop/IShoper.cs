using UnityEngine;

namespace Instance.Shops
{
    public interface IShoper
    {
        int Gold { get; set; }
        Vector3 Position { get; }
    }
}
