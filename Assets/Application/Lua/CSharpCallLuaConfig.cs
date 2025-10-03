using GameBase.Inventorys;
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
    };
}
