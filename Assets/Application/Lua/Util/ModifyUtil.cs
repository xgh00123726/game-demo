using GameBase.Modify;

namespace LuaUtil
{
    public static class ModifyUtil
    {
        public static void AddModifier(int modifierID, int creatureID)
        {
            var c = CreatureSys.Instance.GetCreature(creatureID);
            if (c == null)
            {
                return;
            }
            var modifyInfo = ModifierDataBase.Instance[modifierID];
            var modifier = ModifyerSys.Instance.NewEntity();
            modifier.value = modifyInfo.value;
            modifier.type = modifyInfo.type1 | modifyInfo.type2;
            c.Modifyables.ModifySet(modifyInfo.key, modifier);
        }
    }
}
