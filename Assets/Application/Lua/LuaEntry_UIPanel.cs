using GameBase.Infos;
using GameBase.Tools;
using UnityEngine;
using XLua;

public partial class LuaEntry
{
    public void OnLoadUIPanel()
    {
        var intFields = typeof(UIPanelConfig.Int).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

        foreach (var field in intFields)
        {
            var name = field.Name;
            var val = scriptScopeTable.Get<int>(name);
            field.SetValue(null, val);
        }

        var floatFields = typeof(UIPanelConfig.Float).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

        foreach(var field in floatFields)
        {
            var name = field.Name;
            var val = scriptScopeTable.Get<float>(name);
            field.SetValue(null, val);
        }
    }
}
