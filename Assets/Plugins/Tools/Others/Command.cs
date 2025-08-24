using System;
using System.Collections.Generic;

namespace GameBase.Tools
{
    public class Command
    {
        private static Dictionary<string, Action> _commands = new();
        private static Dictionary<string, Action<string>> _commandsWithPara = new();
        private static Dictionary<string, Action<int>> _commandsWithIntPara = new();
        private static List<string> _commandKeys = new();

        public static List<string> CommandKeys => _commandKeys;

        public static void Register(string key, Action action)
        {
            _commands[key] = action;
            _commandKeys.Add(key);
        }

        public static void Register(string key, Action<string> action)
        {
            _commandsWithPara[key] = action;
            _commandKeys.Add(key);
        }

        public static void Register(string key, Action<int> action)
        {
            _commandsWithIntPara[key] = action;
            _commandKeys.Add(key);
        }

        public static void Exec(string cmd)
        {
            Exec(cmd, null);
        }

        public static void Exec(string cmd, string para)
        {
            if (para == null)
            {
                if (_commands.ContainsKey(cmd))
                {
                    _commands[cmd]?.Invoke();
                }
            }
            else
            {
                if (_commandsWithPara.ContainsKey(cmd))
                {
                    _commandsWithPara[cmd]?.Invoke(para);
                }
                else if (_commandsWithIntPara.ContainsKey(cmd))
                {
                    int.TryParse(para, out var intPara);

                    _commandsWithIntPara[cmd]?.Invoke(intPara);
                }
            }
        }
    }
}
