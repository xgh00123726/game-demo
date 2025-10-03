using XLua;

namespace LuaUtil
{
    [LuaCallCSharp]
    public static class EquipmentUtil
    {
        public static void AddEquipmentToPlayer(int id, int index)
        {
            Player.Instance.character.AddEquipment(id, index);
        }
    }
}
