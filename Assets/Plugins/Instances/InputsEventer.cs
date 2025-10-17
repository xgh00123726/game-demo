using GameBase.EntitySystem;
using GameBase.Tools;
using System;

namespace Instance
{
    public class InputsEventer : SingletonInstance<InputsEventer>
    {
        private static Action[] _keyDownActions;
        public InputsEventer()
        {
            _keyDownActions = new Action[(int)KeyFunction.FunctionNum];
        }

        public static void RegisterKeyDownEvent(KeyFunction func, Action callback)
        {
            var index = (int)func;
            _keyDownActions[index] += callback;
        }

        protected override void Update()
        {
            for (int i = 0; i < _keyDownActions.Length; i++) 
            {
                var action = _keyDownActions[i];
                if (action != null)
                {
                    if (Inputs.GetKeyDown((KeyFunction)i))
                    {
                        action();
                    }
                }
            }
        }
    }
}
