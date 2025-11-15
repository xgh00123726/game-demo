using GameBase.Creatures;
using GameBase.Inventorys;
using GameBase.Spells;
using Instance;
using System;
using System.Collections.Generic;
using XLua;

public static class LuaCallCSharpConfig
{
    [CSharpCallLua]
    public static List<System.Type> csharpCallLuaList = new()
    {
        typeof(Action<int>),
        typeof(ShopItemInfo),
        typeof(Action<Shop>),
        typeof(Action<Creature, int>),
        typeof(Func<Spell, bool>)
    };
}
