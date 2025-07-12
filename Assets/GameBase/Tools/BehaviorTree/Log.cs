using UnityEngine;

namespace GameBase.Tools
{
    public class Log : Behavior
    {
        private string _word;
        public Log(string word)
        {
            _word = word;
        }
        protected override Status OnUpdate()
        {
            Logger.Log(_word);
            return Status.Success;
        }
    }
}
