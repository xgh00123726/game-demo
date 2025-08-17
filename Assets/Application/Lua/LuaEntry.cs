using GameBase.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using XLua.LuaDLL;

[System.Serializable]
public class LuaLoadConfig
{
    public TextAsset asset;
    public string methodName;
}

[LuaCallCSharp]
public partial class LuaEntry : MonoBehaviour
{
    public LuaLoadConfig[] luaLoads;

    internal static LuaEnv luaEnv = new LuaEnv(); //all lua behaviour shared one luaenv only!
    internal static float lastGCTime = 0;
    internal const float GCInterval = 1;//1 second 

    private Action luaStart;
    private Action luaUpdate;
    private Action luaOnDestroy;

    private LuaTable scriptScopeTable;

    private void LoadLua(LuaLoadConfig cfg)
    {
        luaEnv.DoString(cfg.asset.text, cfg.asset.name, scriptScopeTable);

        if (cfg.methodName != null && cfg.methodName.Length > 0)
        {
            var method = GetType().GetMethod(cfg.methodName);
            method.Invoke(this, null);
        }
    }

    void Awake()
    {
        // 为每个脚本设置一个独立的脚本域，可一定程度上防止脚本间全局变量、函数冲突
        scriptScopeTable = luaEnv.NewTable();

        // 设置其元表的 __index, 使其能够访问全局变量
        using (LuaTable meta = luaEnv.NewTable())
        {
            meta.Set("__index", luaEnv.Global);
            scriptScopeTable.SetMetaTable(meta);
        }

        // 将所需值注入到 Lua 脚本域中
        scriptScopeTable.Set("self", this);

        // 如果你希望在脚本内能够设置全局变量, 也可以直接将全局脚本域注入到当前脚本的脚本域中
        // 这样, 你就可以在 Lua 脚本中通过 Global.XXX 来访问全局变量
        // scriptScopeTable.Set("Global", luaEnv.Global);

        // 执行脚本
        foreach (var lua in luaLoads)
        {
            LoadLua(lua);
            Command.Register($"loadlua-{lua.asset.name}", () =>
            {
                LoadLua(lua);
            });
        }

        // 从 Lua 脚本域中获取定义的函数
        Action luaAwake = scriptScopeTable.Get<Action>("awake");
        scriptScopeTable.Get("start", out luaStart);
        scriptScopeTable.Get("update", out luaUpdate);
        scriptScopeTable.Get("ondestroy", out luaOnDestroy);

        if (luaAwake != null)
        {
            luaAwake();
        }

        DontDestroyOnLoad(this);
    }

    

    // Use this for initialization
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }

        if (Time.time - LuaEntry.lastGCTime > GCInterval)
        {
            luaEnv.Tick();
            LuaEntry.lastGCTime = Time.time;
        }
    }

    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }

        scriptScopeTable.Dispose();
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
    }
}
