using GameBase.EntitySystem;
using GameBase.Tools;
using System.Collections;
using System.Collections.Generic;

public class LinearNonReleaseConstructor<T> : IEConstructor<T>, IEnumerable<T>
    where T : new()
{
    private List<T> _entities = new();

    public T this[int i]
    {
        get => _entities[i];
        set => _entities[i] = value;
    }

    public int Count => _entities.Count;

    public int IndexOf(T e)
    {
        return _entities.IndexOf(e);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return ((IEnumerable<T>)_entities).GetEnumerator();
    }

    T IEConstructor<T>.GetEntity()
    {
        var ret = new T();
        _entities.Add(ret);
        return ret;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_entities).GetEnumerator();
    }

    void IEConstructor<T>.ReleaseEntity(T e)
    {
        XLogger.Instance.Level(XLogger.LogLevel.Error).
            Log("trying to release entity to a non-release container");
    }
}
