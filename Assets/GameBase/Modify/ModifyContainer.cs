using System.Collections;
using System.Collections.Generic;

namespace GameBase.Modify
{
    internal class ModifyContainer
    {
        internal List<GModify> _modifies = new List<GModify>();

        internal void Add(GModify modify)
        {
            _modifies.Add(modify);
        }

        internal void Remove(GModify modify)
        {
            _modifies.Remove(modify);
        }
    }
}
