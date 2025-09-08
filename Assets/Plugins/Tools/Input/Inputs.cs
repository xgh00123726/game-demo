using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Tools
{
    public partial class Inputs
    {
        // 按键码和按键功能对应表，程序中有初始值
        private static Dictionary<List<KeyCode>, List<KeyFunction>> _funcOfCode = new Dictionary<List<KeyCode>, List<KeyFunction>>
        {
            {new List<KeyCode>{ KeyCode.Mouse0 }, new List<KeyFunction>{ KeyFunction.Attack } },
            {new List<KeyCode>{ KeyCode.Mouse1 }, new List<KeyFunction>{ KeyFunction.MoveTo } },
            {new List<KeyCode>{ KeyCode.W },      new List<KeyFunction>{ KeyFunction.Up } },
            {new List<KeyCode>{ KeyCode.S },      new List<KeyFunction>{ KeyFunction.Stop, KeyFunction.Down } },
            {new List<KeyCode>{ KeyCode.A },      new List<KeyFunction>{ KeyFunction.Aim, KeyFunction.Left } },
            {new List<KeyCode>{ KeyCode.D },      new List<KeyFunction>{ KeyFunction.Right } },
            {new List<KeyCode>{ KeyCode.Alpha1 }, new List<KeyFunction>{ KeyFunction.Spell1 } },
            {new List<KeyCode>{ KeyCode.Alpha2 }, new List<KeyFunction>{ KeyFunction.Spell2 } },
            {new List<KeyCode>{ KeyCode.Alpha3 }, new List<KeyFunction>{ KeyFunction.Spell3 } },
            {new List<KeyCode>{ KeyCode.Alpha4 }, new List<KeyFunction>{ KeyFunction.Spell4 } },
            {new List<KeyCode>{ KeyCode.Alpha5 }, new List<KeyFunction>{ KeyFunction.Spell5 } },
            {new List<KeyCode>{ KeyCode.Alpha6 }, new List<KeyFunction>{ KeyFunction.Spell6 } },
            {new List<KeyCode>{ KeyCode.Alpha7 }, new List<KeyFunction>{ KeyFunction.Spell7 } },
            {new List<KeyCode>{KeyCode.Alpha5}, new List<KeyFunction> { KeyFunction.Spell5 } },
            {new List<KeyCode>{ KeyCode.Space },  new List<KeyFunction>{ KeyFunction.Blink } },
            {new List<KeyCode>{ KeyCode.F },      new List<KeyFunction>{ KeyFunction.Interaction } },
            {new List<KeyCode>{ KeyCode.Y },      new List<KeyFunction>{ KeyFunction.LockView } }, // 锁定视角
            {new List<KeyCode>{ KeyCode.L },      new List<KeyFunction>{ KeyFunction.LockCamHeight } }, // 锁定高度
            {new List<KeyCode>{ KeyCode.Z },      new List<KeyFunction>{ KeyFunction.SettingPitch } }, // 正在设置俯仰角
            {new List<KeyCode>{ KeyCode.X },      new List<KeyFunction>{ KeyFunction.SettingYawClockWise } }, // 顺时针旋转视角
            {new List<KeyCode>{ KeyCode.C },      new List<KeyFunction>{ KeyFunction.SettingYawAntiClockWise } }, // 逆时针旋转视角
            {new List<KeyCode>{ KeyCode.X, KeyCode.LeftShift}, new List<KeyFunction>{ KeyFunction.SettingYawClockWiseFaster } }, // 顺时针旋转视角（更快地）
            {new List<KeyCode>{ KeyCode.C, KeyCode.LeftShift}, new List<KeyFunction>{ KeyFunction.SettingYawAntiClockWiseFaster } }, // 逆时针旋转视角（更快地）
            {new List<KeyCode>{ KeyCode.Z, KeyCode.X, KeyCode.C}, new List<KeyFunction>{ KeyFunction.ResetView } },
            {new List<KeyCode>{ KeyCode.Slash}, new List<KeyFunction>{ KeyFunction.ToggleCmd} },
            {new List<KeyCode>{KeyCode.Return}, new List<KeyFunction>{KeyFunction.CmdConfirm } },
            {new List<KeyCode> {KeyCode.Backspace}, new List<KeyFunction>{KeyFunction.CmdDelete} },
            {new List<KeyCode>{KeyCode.Escape}, new List<KeyFunction>{ KeyFunction.Cancel} },
            {new List<KeyCode>{KeyCode.Mouse0}, new List<KeyFunction>{KeyFunction.MouseConfirm, KeyFunction.TestPosKey} },
            {new List<KeyCode>{KeyCode.Tab}, new List<KeyFunction> { KeyFunction.ToggleAttrPanel, KeyFunction.ChooseText} }, // 切换属性显示栏显示
            {new List<KeyCode>{KeyCode.LeftControl, KeyCode.LeftShift, KeyCode.A }, new List<KeyFunction>{ KeyFunction.SummonEnemy} },
            {new List<KeyCode>{ KeyCode.LeftControl, KeyCode.LeftShift, KeyCode.C },new List<KeyFunction>{ KeyFunction.SummonAllies }  },
            {new List<KeyCode>{KeyCode.E}, new List<KeyFunction>{KeyFunction.OpenShop, KeyFunction.CloseShop } },
            {new List<KeyCode>{KeyCode.Mouse2}, new List<KeyFunction>{KeyFunction.DragScreen} },
            {new List<KeyCode> {KeyCode.UpArrow}, new List<KeyFunction>{KeyFunction.CmdUseLast} },
        };
        // 按键功能和按键码对应表，由构造函数计算获取
        private static Dictionary<KeyFunction, List<KeyCode>> _codeOfFunc = new Dictionary<KeyFunction, List<KeyCode>> { };
        // 按键码和按键功能类，用于保存设置文件
        private static List<InputKey> GInputKeys = new List<InputKey> { };

        private static Dictionary<string, bool> _callersLock = new();

        static Inputs()
        {
            ConvertDictToList();
            ConvertListToDict();
        }

        public static string SettingSavePath
        {
            get
            {
                return Application.streamingAssetsPath + "/KeySetting/" + "KeySetting.json";
            }
        }

        private static void Write()
        {
            StreamWriter streamWriter = new StreamWriter(SettingSavePath);

            foreach (var key in GInputKeys)
            {
                streamWriter.WriteLine(JsonUtility.ToJson(key));
            }
            streamWriter.Close();
        }

        private static void Read()
        {
            GInputKeys.Clear();
            StreamReader streamReader = new StreamReader(SettingSavePath);
            string json;
            while (!streamReader.EndOfStream)
            {
                json = streamReader.ReadLine();
                var item = JsonUtility.FromJson<InputKey>(json);
                GInputKeys.Add(item);
            }

            streamReader.Close();
        }

        private static void ConvertListToDict()
        {
            _codeOfFunc.Clear();
            _funcOfCode.Clear();
            foreach (var key in GInputKeys)
            {
                foreach (var func in key.keyFuncs)
                {
                    _codeOfFunc[func] = key.keyCodes;
                }
                _funcOfCode[key.keyCodes] = key.keyFuncs;
            }
        }

        private static void ConvertDictToList()
        {
            GInputKeys.Clear();
            foreach (var kv in _funcOfCode)
            {
                GInputKeys.Add(new InputKey()
                {
                    keyCodes = kv.Key,
                    keyFuncs = kv.Value,
                });
            }
        }

        public static void LockCaller(string caller)
        {
            if (_callersLock.ContainsKey(caller))
            {
                _callersLock[caller] = true;
            }
            else
            {
                _callersLock.Add(caller, true);
            }
        }

        public static void LockAll()
        {
            string[] keys = new string[_callersLock.Keys.Count];
            _callersLock.Keys.CopyTo(keys, 0);
            foreach (var c in keys)
            {
                _callersLock[c] = true;
            }
        }

        public static void LockOthers(string caller)
        {
            string[] keys = new string[_callersLock.Keys.Count];
            _callersLock.Keys.CopyTo(keys, 0);
            foreach (var c in keys)
            {
                if (caller == c)
                {
                    continue;
                }
                _callersLock[c] = true;
            }
        }
        public static void ReleaseCaller(string caller)
        {
            if (_callersLock.ContainsKey(caller))
            {
                _callersLock[caller] = false;
            }
        }
        public static void ReleaseAll()
        {
            string[] keys = new string[_callersLock.Keys.Count];
            _callersLock.Keys.CopyTo(keys, 0);
            foreach (var c in keys)
            {
                _callersLock[c] = false;
            }
        }

        public static void ReleaseOthers(string caller)
        {
            string[] keys = new string[_callersLock.Keys.Count];
            _callersLock.Keys.CopyTo(keys, 0);
            foreach (var c in keys)
            {
                if (caller == c)
                {
                    continue;
                }
                _callersLock[c] = false;
            }
        }

        public static bool GetKeyDown(KeyFunction keyFunction)
        {
            if (!_codeOfFunc.ContainsKey(keyFunction))
            {
                Tools.XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"try get invalid keyfuncion:{keyFunction}");
                return false;
            }

            foreach (var kc in _codeOfFunc[keyFunction])
            {
                if (!UnityEngine.Input.GetKeyDown(kc))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool GetKeyDown(KeyFunction keyFunction, string caller)
        {
            if (!_callersLock.ContainsKey(caller))
            {
                _callersLock.Add(caller, false);
            }

            if (_callersLock[caller])
            {
                return false;
            }
            else
            {
                return GetKeyDown(keyFunction);
            }
        }

        public static bool GetKeyUp(KeyFunction keyFunction)
        {
            if (!_codeOfFunc.ContainsKey(keyFunction))
            {
                Tools.XLogger.Instance.Level(XLogger.LogLevel.Warning)
                    .Log($"try get invalid keyfuncion:{keyFunction}");
                return false;
            }

            foreach (var kc in _codeOfFunc[keyFunction])
            {
                if (!UnityEngine.Input.GetKeyUp(kc))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool GetKeyUp(KeyFunction keyFunction, string caller)
        {
            if (!_callersLock.ContainsKey(caller))
            {
                _callersLock.Add(caller, false);
            }

            if (_callersLock[caller])
            {
                return false;
            }
            else
            {
                return GetKeyUp(keyFunction);
            }
        }

        public static bool GetKey(KeyFunction keyFunction)
        {
            foreach (var kc in _codeOfFunc[keyFunction])
            {
                if (!UnityEngine.Input.GetKey(kc)) return false;
            }
            return true;
        }

        public static bool GetKey(KeyFunction keyFunction, string caller)
        {
            if (!_callersLock.ContainsKey(caller))
            {
                _callersLock.Add(caller, false);
            }

            if (_callersLock[caller])
            {
                return false;
            }
            else
            {
                return GetKey(keyFunction);
            }
        }

        public static void Remap(KeyFunction keyFunction, List<KeyCode> newKeyCode)
        {
            List<KeyCode> oldCode = _codeOfFunc[keyFunction];
            if (oldCode == newKeyCode) return;
            
            _codeOfFunc[keyFunction] = newKeyCode;
            _funcOfCode[oldCode].Remove(keyFunction);
            _funcOfCode[newKeyCode].Add(keyFunction);
        }

        public static void SaveSetting()
        {
            ConvertDictToList();
            Write();
        }

        public static void LoadSetting()
        {
            Read();
            ConvertListToDict();
        }
    }

}