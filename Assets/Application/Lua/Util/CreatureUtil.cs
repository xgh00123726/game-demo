using Constructor.Creatures;
using UnityEngine;

namespace LuaUtil
{
    public static class CreatureUtil
    {
        public static int Gen(int type, int id)
        {
            var e = Factory.Instance.Get((Type)type, id);

            return e.InstanceID;
        }

        public static int Gen(int type, int id, float x, float z)
        {
            var e = Factory.Instance.Get((Type)type, id);
            e.Position = new Vector3(x, -7, z);

            return e.InstanceID;
        }
    }
}
