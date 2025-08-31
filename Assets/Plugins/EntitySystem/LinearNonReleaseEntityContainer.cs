using GameBase.EntitySystem;
using GameBase.Tools;
using System.Collections.Generic;

public class LinearNonReleaseEntityContainer<T> : IEContainer<T>
    where T : new()
{
    private List<T> _entities = new();

    public T this[int i] => _entities[i];

    int IEContainer<T>.Count => _entities.Count;

    T IEContainer<T>.GetEntity()
    {
        var ret = new T();
        _entities.Add(ret);
        return ret;
    }

    void IEContainer<T>.ReleaseEntity(T e)
    {
        XLogger.Instance.Level(XLogger.LogLevel.Error).
            Log("trying to release entity to a non-release container");
    }
}
