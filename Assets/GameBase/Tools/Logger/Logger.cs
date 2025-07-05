using UnityEngine;

namespace GameBase.Tools
{
    public class Logger
    {
        private static Logger _instance = new Logger();
        public enum LogLevel
        {
            Fatal,
            Error,
            Warning,
            Info,
        }
        private static LogLevel _logLevel = LogLevel.Info;
        private static bool _editorOnly = true;
        private static LogLevel _ignoreLevel = LogLevel.Info;
        private static void Reset()
        {
            _logLevel = LogLevel.Info;
            _editorOnly = true;
        }

        /// <summary>
        /// 停止输出目标等级以下的所有信息
        /// <list type="bullet">
        /// <item><param name="level"><paramref name="level"/>:目标等级</param></item>
        /// </list></summary>
        public static void Ignore(LogLevel level)
        {
            _ignoreLevel = level;
        }

        /// <summary>
        /// 设置打印信息等级
        /// <list type="bullet">
        /// <item><param name="level"><paramref name="level"/>:打印等级</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public static Logger Level(LogLevel level)
        {
            _logLevel = level;
            return _instance;
        }

        /// <summary>
        /// 设置是否仅编辑模式打印
        /// <list type="bullet">
        /// <item><param name="editorOnly"><paramref name="editorOnly"/>:是否仅编辑模式打印</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public static Logger EditorOnly(bool editorOnly)
        {
            _editorOnly = editorOnly;
            return _instance;
        }

        private static void LogMessage(object info, LogLevel level)
        {
            if (level > _ignoreLevel) return; // 忽略_ignoreLevel等级以下的所有日志

            if (level == LogLevel.Info)
            {
                Debug.Log($"[info] {info}");
            }
            else if (level == LogLevel.Warning)
            {
                Debug.LogWarning($"[warning] {info}");
            }
            else if (level == LogLevel.Error)
            {
                Debug.LogError($"[error] {info}");
            }
            else if (level == LogLevel.Fatal)
            {
                Debug.LogError($"[fatal] {info}");
            }
        }

        /// <summary>
        /// 输出日志, 并清除设置信息
        /// <list type="bullet">
        /// <item><param name="info"><paramref name="info"/>:需要打印的信息</param></item>
        /// </list></summary>
        /// <returns>供链式调用的logger</returns>
        public static Logger Log(object info, bool reserve = true)
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

            Reset();
            return _instance;
        }

        public Logger Log(object info)
        {
            return Log(info, true);
        }
    }
}
