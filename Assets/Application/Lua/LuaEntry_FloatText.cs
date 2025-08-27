using GameBase.Infos;

public partial class LuaEntry
{
    public void OnLoadFloatText()
    {
        var floatFields = typeof(FloatTextConfig.Float).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

        foreach (var field in floatFields)
        {
            var name = field.Name;
            var val = scriptScopeTable.Get<float>(name);
            field.SetValue(null, val);
        }
    }
}
