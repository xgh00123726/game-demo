local PlayerUtil = CS.LuaUtil.PlayerUtil

Player = {
    --- @ret playerID : int
    GetPlayerID = PlayerUtil.GetPlayerID,

    --- @ret player : Creature
    GetPlayer = function ()
        return PlayerUtil.Player
    end
}