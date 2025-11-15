using Constructor.Spells;
using GameBase.EntitySystem;
using GameBase.Tools;
using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

public class GamesExtend
{
    [MenuItem("Tools/SaveKeyReflectSetting", false)]
    public static void SaveKeyReflectSetting()
    {
        Inputs.SaveSetting();
    }

    [MenuItem("Tools/ReadKeyReflectSetting", false)]
    public static void ReadKeyReflectSetting()
    {
        Inputs.LoadSetting();
    }

    /// <summary>
    /// 13213
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns>1656</returns>
    private static bool IsTargetAssembly(Assembly assembly)
    {
        foreach (var assemblyRef in assembly.GetReferencedAssemblies())
        {
            if (assemblyRef.Name == "EntitySystem")
            {
                return true;
            }
        }
        return false;
    }

    [MenuItem("Tools/GenerateAllYamlTemplate", false)]
    public static void GenerateAllYamlTemplate()
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => IsTargetAssembly(a)))
        {
            var allTypes = assembly.GetTypes();
            var factoryTypes = allTypes.Where(o =>
            {
                foreach (var attr in System.Attribute.GetCustomAttributes(o, true))
                {
                    if (attr is GenTemplateAttribute)
                    {
                        return true;
                    }
                }

                return false;
            }); 

            foreach (var factoryType in factoryTypes)
            {
                var factory = Activator.CreateInstance(factoryType);
                MethodInfo method = factoryType.GetMethod("GenerateTemplate");
                method.Invoke(factory, null);
            }
        }
    }
}
