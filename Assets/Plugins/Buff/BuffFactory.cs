using GameBase.EntitySystem;
using GameBase.Tools;

namespace GameBase.Buffs
{
    public struct BuffName
    {
        public string name;
    }
    public class BuffNameDataBase : CsvDataBase<BuffName, BuffNameDataBase> { }
    public class BuffFactory : MultiFactory<Buff, BuffFactory>
    {
        public BuffFactory()
        {
            Register(BuffYamlFactory.Instance.Get);
        }

        public string GetName(int id)
        {
            if (id >= BuffNameDataBase.Instance.Size)
            {
                return null;
            }
            return BuffNameDataBase.Instance[id].name;
        }

        public Buff GetByID(int id)
        {
            return BuffYamlFactory.Instance.GetFromData(GetData(id));
        }

        public BuffData GetData(int id)
        {
            if (id >= BuffNameDataBase.Instance.Size)
            {
                return null;
            }

            return BuffYamlFactory.Instance.GetData(GetName(id));
        }
    }
}
