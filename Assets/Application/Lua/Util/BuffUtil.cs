using GameBase.Modify;

namespace LuaUtil
{
    public static class BuffUtil
    {
        public static void AddBuffToCreature(int buffID, int creatureID, float duration)
        {
            var c = CreatureSys.Instance.GetCreature(creatureID);
            if (c == null)
            {
                return;
            }
            c.AddBuff(buffID, duration);
        }
    }
}
