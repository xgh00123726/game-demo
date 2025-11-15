using GameBase.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;

namespace ToolGood.CodeCounts
{
    class CodelineStat
    {
        static List<string> excludeExts = new List<string>() {
            ".deps.json",".dev.json",".runtimeconfig.json","CodeCounts.txt",
            ".designer.cs" };

        static List<string> excludeFolder = new List<string>() { "bin", "obj", ".git", ".svn", ".vs", "packages" };

        [MenuItem("Tools/CodeLineStat", false)]
        public static void StatWithInfo()
        {
            Stat(true);
        }

        [MenuItem("Tools/CodeLineStatWithoutSingleFileInfo", false)]
        public static void StatWithoutSingleFileInfo()
        {
            Stat(false);
        }

        private static void Stat(bool logFileInfo)
        {
            int sum = 0;
            sum += StatFolder("/Assets/Plugins", new() { ".cs" }, logFileInfo);
            sum += StatFolder("/Assets/Application", new() { ".cs" }, logFileInfo);
            sum += StatFolder("/Assets/Editor", new() { ".cs" }, logFileInfo);
            sum += StatFolder("/Assets/StreamingAssets", new() { ".cs", ".json", ".csv", ".lua" }, logFileInfo);
            XLogger.Instance.Log($"总行数: {sum}");
        }

        private static int StatFolder(string subFolder, List<string> exts, bool logFileInfo = true)
        {
            var folder = Directory.GetCurrentDirectory() + subFolder;
            var files = GetAllFiles(folder, exts);
            files = files.Distinct().ToList();
            var sum = 0;
            long bytes = 0;
            var changeFiles = 0;
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                bytes += fi.Length;
                if (fi.LastWriteTime.Date == DateTime.Now.Date)
                {
                    changeFiles++;
                }

                var lines = File.ReadAllLines(file);
                sum += lines.Length;
                if (logFileInfo)
                {
                    XLogger.Instance.Log($"{file}|{lines.Length}");
                }
            }
            XLogger.Instance.Log($"统计文件夹:{folder}, 总行数：{sum}");
            return sum;
        }

        static List<string> GetAllFiles(string folder, List<string> exts)
        {
            List<string> list = new List<string>();
            GetAllFiles(folder, list, exts);
            return list;
        }

        static void GetAllFiles(string folder, List<string> list, List<string> exts)
        {
            var files = GetFiles(folder, exts);
            list.AddRange(files);

            var folders = GetFolders(folder);
            foreach (var item in folders)
            {
                GetAllFiles(item, list, exts);
            }
        }

        static List<string> GetFolders(string folder)
        {
            List<string> list = new List<string>();
            var files = Directory.GetDirectories(folder);

            foreach (var file in files)
            {
                var ext = Path.GetFileName(file);
                if (excludeFolder.Contains(ext) == false)
                {
                    list.Add(file);
                }
            }
            return list;
        }

        static List<string> GetFiles(string folder, List<string> exts)
        {
            List<string> list = new List<string>();
            var files = Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file).ToLower();
                var b = false;
                foreach (var item in excludeExts)
                {
                    if (file.EndsWith(item))
                    {
                        b = true;
                        break;
                    }
                }
                if (b)
                {
                    continue;
                }

                foreach (var item in exts)
                {
                    if (ext.EndsWith(item))
                    {

                        list.Add(file);
                        break;
                    }
                }
            }
            return list;
        }

        static int GetLineCount(string file)
        {
            var lines = File.ReadAllLines(file);
            XLogger.Instance.Log($"{file}|{lines.Length}");
            return lines.Length;
        }

    }
}
