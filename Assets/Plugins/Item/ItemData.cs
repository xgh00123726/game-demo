using System.Collections.Generic;

namespace GameBase.Items
{

    public abstract class ItemData
    {
        public int ID { get; internal set; }
        public ItemDataType Type {  get; internal set; }
    }
}
