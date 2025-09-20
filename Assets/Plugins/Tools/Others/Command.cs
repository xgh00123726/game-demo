using System;
using System.Collections.Generic;
using System.Linq;

namespace GameBase.Tools
{
    public class Command
    {
        private static Dictionary<string, Action<string[]>> _commands = new();
        private static HashSet<string> _commandKeys = new();

        public static HashSet<string> CommandKeys => _commandKeys;

        public static void Register(string key, Action action)
        {
            Register(key, (string[] args) => action?.Invoke());
        }

        public static void Register(string key, Action<string> action)
        {
            Register(key, (string[] args) =>
            {
                if (args.Length < 2)
                {
                    return;
                }
                else
                {
                    action?.Invoke(args[1]);
                }
            });
        }

        public static void Register(string key, Action<int> action)
        {
            Register(key, (string[] args) =>
            {
                if (args.Length < 2)
                {
                    return;
                }
                else
                {
                    if (int.TryParse(args[1], out int value))
                    {
                        action?.Invoke(value);
                    }
                }
            });
        }

        public static void Register(string key, Action<int, int> action)
        {
            Register(key, (string[] args) =>
            {
                if (args.Length < 3)
                {
                    return;
                }
                else
                {
                    if (int.TryParse(args[1], out int v1) && int.TryParse(args[2], out int v2))
                    {
                        action?.Invoke(v1, v2);
                    }
                }
            });
        }

        public static void Register(string key, Action<string[]> action)
        {
            _commands[key] = action;
            _commandKeys.Add(key);
        }

        public static void Exec(string cmd)
        {
            string[] args = cmd.Split(" ");
            
            if (args.Length == 0)
            {
                return;
            }

            string key = args[0];

            if (_commands.ContainsKey(key))
            {
                _commands[key]?.Invoke(args);
            }
        }
    }
}
