using System.Collections.Generic;

namespace GameBase.Items
{

    public abstract class ItemData
    {
        public int ID { get; set; }
        public ItemDataType Type {  get; set; }
    }
}
