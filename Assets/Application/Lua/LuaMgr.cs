using GameBase.Network;
using System;
using System.IO;
using UnityEngine;
using XLua;

public static class LuaMgr
{
    internal static LuaEnv luaEnv = new();
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;

    private static Action LuaUpdate;
    public static LuaTable Table { get; private set; }

    public static void Init()
    {
        Table = luaEnv.NewTable();

        luaEnv.AddLoader((ref string fileName) =>
        {
            string path = $"{Application.streamingAssetsPath}/Lua/{fileName}.lua";
            string info = File.ReadAllText(path);
            return System.Text.Encoding.UTF8.GetBytes(info);
        });

        luaEnv.DoString("require 'Main'");


        // 设置其元表的 __index, 使其能够访问全局变量
        using (LuaTable meta = luaEnv.NewTable())
        {
            meta.Set("__index", luaEnv.Global);
            Table.SetMetaTable(meta);
        }


        // 从 Lua 脚本域中获取定义的函数
        Table.Get<string, Action>("OnInitOK", out var OnInitOK);
        Table.Get("Update", out LuaUpdate);

        OnInitOK?.Invoke();
    }

    public static void Update()
    {
        LuaUpdate?.Invoke();


        if (Time.time - lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            lastGCTime = Time.time;
        }
    }

    public static void Dispose()
    {
        Table.Dispose();
        NetworkMgr.Instance.Dispose();
        LuaUpdate = null;
    }
}
