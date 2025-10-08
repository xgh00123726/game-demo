using UnityEngine;

namespace GameBase.Tools
{
    public class Log : Behavior
    {
        private string _word;
        private Status _ret;
        public Log(string word, Status ret)
        {
            _word = word;
            _ret = ret;
        }
        protected override Status OnUpdate()
        {
            XLogger.Instance.Log(_word);
            return _ret;
        }
    }
}
