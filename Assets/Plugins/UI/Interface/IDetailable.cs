using UnityEngine;

namespace GameBase.UI
{
    public interface IDetailable<T>
    {
        public bool NeedDetail(T e);
        public void OnDetail(T e);
    }
}
