using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;

using SCCommand = System.CommandLine.Command;

namespace GameBase.Tools
{
    public class Command
    {
        private static RootCommand _root = new();
        private static Dictionary<string, SCCommand> _subCmds = new();

        public static List<string> GetCompletions(string input)
        {
            var result = new List<string>();
            foreach (var cmd in _root.Parse(input).GetCompletions())
            {
                result.Add(cmd.Label);
            }

            return result;
        }

        public static string GetMatchest(string input)
        {
            var completions = _root.Parse(input).GetCompletions();
            foreach (var c in completions)
            {
                return c.Label;
            }

            return null;
        }

        public static SCCommand RegisterCommand(string name)
        {
            var cmd = new SCCommand(name);
            _root.Add(cmd);
            _subCmds.Add(name, cmd);
            return cmd;
        }

        public static Option<string> RegisterOption(string cmdName, string optionName)
        {
            if (_subCmds.TryGetValue(cmdName, out var cmd))
            {
                var option = new Option<string>(optionName);
                cmd.Options.Add(option);
                return option;
            }

            return null;
        }

        public static void Exec(string cmd)
        {
            _root.Parse(cmd).Invoke();
        }
    }
}
