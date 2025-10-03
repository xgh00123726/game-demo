using GameBase.Creatures;

namespace LuaUtil
{
    public static class PlayerUtil
    {
        private static Creature _player;
        public static Creature Player => _player;

        public static int GetPlayerID()
        {
            return _player.InstanceID;
        }

        public static void SetPlayer(int id)
        {
            _player = CreatureSys.Instance.GetCreature(id);
        }
    }
}
