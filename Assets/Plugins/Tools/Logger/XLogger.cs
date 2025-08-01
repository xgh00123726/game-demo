using UnityEngine;

namespace GameBase.Tools
{
    public class XLogger
    {
        private static XLogger _instance = new XLogger();
        public static XLogger Instance => _instance;
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

        private void Reset()
        {
            _logLevel = LogLevel.Info;
            _editorOnly = true;
            _ifLog = true;
            _colorSet = false;
            _color = UnityEngine.Color.gray;
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
            return _instance;
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
            return _instance;
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
            return _instance;
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
            return _instance;
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
            return _instance;
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

            if (level == LogLevel.Info)
            {
                Debug.Log($"{colorTagBegin}[info] {info}{colorTagEnd}");
            }
            else if (level == LogLevel.Warning)
            {
                Debug.LogWarning($"{colorTagBegin}[warning] {info}{colorTagEnd}");
            }
            else if (level == LogLevel.Error)
            {
                Debug.LogError($"{colorTagBegin}[error] {info}{colorTagEnd}");
            }
            else if (level == LogLevel.Fatal)
            {
                Debug.LogError($"{colorTagBegin}[fatal] {info}{colorTagEnd}");
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

        public XLogger Log(object info)
        {
            return Log(info, true);
        }
    }
}
