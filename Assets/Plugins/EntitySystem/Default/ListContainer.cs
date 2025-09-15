using GameBase.Tools;
using System.Collections;
using System.Collections.Generic;

namespace GameBase.EntitySystem
{
    public class ListContainer<T> : List<T>, IEContainer<T>
    {
    }
}
