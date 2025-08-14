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
    }
}
