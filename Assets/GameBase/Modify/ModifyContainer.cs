using System.Collections;
using System.Collections.Generic;

namespace GameBase.Modify
{
    internal class ModifyContainer
    {
        internal List<Modify> _modifies = new List<Modify>();

        internal void Add(Modify modify)
        {
            _modifies.Add(modify);
        }

        internal void Remove(Modify modify)
        {
            _modifies.Remove(modify);
        }
    }
}
