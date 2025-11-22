using System.Collections.Generic;

namespace GameBase.Items
{
    public interface IItemDataRegister<T> where T : ItemData
    {
        List<T> GetRegisteredItems();
    }
}
