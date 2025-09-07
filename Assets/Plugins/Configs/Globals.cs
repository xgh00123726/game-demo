using System.Collections.Generic;
using UnityEngine;

namespace GameBase.Infos
{
    public static class Globals
    {
        private static List<IPlayerGlobal> _players = new();

        public static void RegisterPlayer(IPlayerGlobal player)
        {
            _players.Add(player);
        }

        public static Vector3 GetPlayerPosition(int index)
        {
            return _players[index].Position;
        }

        public static int GetPlayerGold(int index)
        {
            return _players[index].Gold;
        }
        public static void SetPlayerGold(int index, int value)
        {
            _players[index].Gold = value;
        }
    }
}
