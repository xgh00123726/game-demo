using System;
using System.IO;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaMain : MonoBehaviour
{
    internal static LuaEnv luaEnv = new LuaEnv(); 
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;

    private static Action OnInitOK;
    private static Action LuaUpdate;

    private static LuaTable _table;
    public static LuaTable Table => _table;

    void Awake()
    {
        _table = luaEnv.NewTable();

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
            _table.SetMetaTable(meta);
        }


        // 从 Lua 脚本域中获取定义的函数
        _table.Get("OnInitOK", out OnInitOK);
        _table.Get("Update", out LuaUpdate);

        OnInitOK?.Invoke();

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        LuaUpdate?.Invoke();

        if (Time.time - LuaMain.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaMain.lastGCTime = Time.time;
        }
    }

    void OnDestroy()
    {
        _table.Dispose();
        LuaUpdate = null;
    }
}
