using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GameBase.Tools
{
    public class XLogger : SingletonInstance<XLogger>
    {
        public static string relativeFolderPath = @"D:\project\game-demo\Assets\Log";
        public enum LogLevel
        {
            Fatal,
            Error,
            Warning,
            Info,
        }
        private LogLevel _logLevel = LogLevel.Info;
        private bool _editorOnly = true;
        private LogLevel _ignoreLevel = LogLevel.Info;
        private bool _ifLog = true;
        private Color _color = UnityEngine.Color.gray;
        private bool _colorSet = false;
        private bool _withFrameCount = true;
        private bool _toFile = false;
        private string _filePath = "FullLog.html";
        private bool _toConsole = true;
        private bool _inSubThread = false;
        private object _logQueueLock = new();
        private Queue<object> _logQueue = new();

        private void Reset()
        {
            _logLevel = LogLevel.Info;
            _editorOnly = true;
            _ifLog = true;
            _colorSet = false;
            _color = UnityEngine.Color.gray;
            _withFrameCount = true;
            _toFile = false;
            _filePath = "FullLog.html";
            _toConsole = true;
            _inSubThread = false;
        }

        /// <summary>
        /// 是否输出
        /// <list type="bullet">
        /// <item><param name="ifLog"><paramref name="ifLog"/>:是否输出</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public XLogger IF(bool ifLog)
        {
            _ifLog = ifLog;
            return Instance;
        }

        /// <summary>
        /// 不输出到控制台
        /// </summary>
        /// <returns></returns>
        public XLogger DontToConsole()
        {
            _toConsole = false;
            return Instance;
        }

        /// <summary>
        /// 子线程中使用
        /// </summary>
        /// <returns></returns>
        public XLogger InSubThread()
        {
            _inSubThread = true;
            return Instance;
        }

        /// <summary>
        /// 不输出当前帧
        /// </summary>
        /// <returns></returns>
        public XLogger WithOutFrame()
        {
            _withFrameCount = false;
            return Instance;
        }

        /// <summary>
        /// 停止输出目标等级以下的所有信息
        /// <list type="bullet">
        /// <item><param name="level"><paramref name="level"/>:目标等级</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public XLogger Ignore(LogLevel level)
        {
            _ignoreLevel = level;
            return Instance;
        }

        /// <summary>
        /// 设置打印信息等级
        /// <list type="bullet">
        /// <item><param name="level"><paramref name="level"/>:打印等级</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public XLogger Level(LogLevel level)
        {
            _logLevel = level;
            if (level == LogLevel.Error)
            {
                _toFile = true;
            }
            return Instance;
        }

        /// <summary>
        /// 设置打印信息颜色
        /// <list type="bullet">
        /// <item><param name="color"><paramref name="color"/>:打印颜色</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public XLogger Color(Color color)
        {
            _color = color;
            _colorSet = true;
            return Instance;
        }

        /// <summary>
        /// 输出到文件中
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public XLogger ToFile(string path = null)
        {
            _toFile = true;
            if (path != null)
            {
                _filePath = path;
            }
            return Instance;
        }

        /// <summary>
        /// 设置是否仅编辑模式打印
        /// <list type="bullet">
        /// <item><param name="editorOnly"><paramref name="editorOnly"/>:是否仅编辑模式打印</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public XLogger EditorOnly(bool editorOnly)
        {
            _editorOnly = editorOnly;
            return Instance;
        }

        private void WriteToFile(string content)
        {
            File.AppendAllText($"{relativeFolderPath}\\{_filePath}", content + "  <br>\n");
        }

        private void LogMessage(object info, LogLevel level)
        {
            if (!_ifLog) return;  // 主动禁止输出

            if (level > _ignoreLevel) return; // 忽略_ignoreLevel等级以下的所有日志

            string colorTagBegin = "";
            string colorTagEnd = "";
            if (_colorSet)
            {
                colorTagBegin = $"<color=#{ColorUtility.ToHtmlStringRGBA(_color)}>";
                colorTagEnd = "</color>";
            }

            if (_withFrameCount)
            {
                info = $"<color=#66ccff>[frame:{Time.frameCount}]</color>{info}";
            }

            if (level == LogLevel.Info)
            {
                string cInfo = $"<color=#00ff00>[info]</color>{colorTagBegin} {info}{colorTagEnd}";
                if (_toFile)
                {
                    WriteToFile(cInfo);
                }
                if (_toConsole)
                {
                    Debug.Log(cInfo);
                }
            }
            else if (level == LogLevel.Warning)
            {
                string cWarning = $"<color=#F8C20D>[warning]</color>{colorTagBegin} {info}{colorTagEnd}";
                if (_toFile)
                {
                    WriteToFile(cWarning);
                }
                if (_toConsole)
                {
                    Debug.LogWarning(cWarning);
                }
            }
            else if (level == LogLevel.Error)
            {
                string cError = $"<color=#ff0000>[error]</color>{colorTagBegin} {info}{colorTagEnd}";
                if (_toFile)
                {
                    WriteToFile(cError);
                }
                if (_toConsole)
                {
                    Debug.LogError(cError);
                }
            }
        }

        /// <summary>
        /// 输出日志, 并清除设置信息
        /// <list type="bullet">
        /// <item><param name="info"><paramref name="info"/>:需要打印的信息</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public XLogger Log(object info, bool reserve = true)
        {
            if (_inSubThread)
            {
                lock(_logQueueLock)
                {
                    _logQueue.Enqueue(info);
                }
            }
            else
            {
                if (_editorOnly)
                {
#if UNITY_EDITOR
                    LogMessage(info, _logLevel);
#endif
                }
                else
                {
                    LogMessage(info, _logLevel);
                }
            }

            Reset();
            return Instance;
        }

        public XLogger Log(object info)
        {
            return Log(info, true);
        }


        protected override void Update()
        {
            foreach (var o in _logQueue)
            {
                Log(o);
            }
            _logQueue.Clear();
        }
    }
}
