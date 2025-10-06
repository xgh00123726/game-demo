using System;
using System.Collections.Generic;

namespace GameBase.Tools
{
    public class FSM
    {
        public class State
        {
            public Action OnEnter;
            public Action OnExit;
            public Action OnExec;

            internal State() { }
        }

        public class Transition
        {
            public Func<bool> Trig;
            public Action Guard;
            public State src;
            public State dest;

            internal Transition() { }
        }

        private State _currState;
        private List<State> _states = new();
        private List<Transition> _transitions = new();

        public void NewState(Action OnEnter, Action OnExit, Action OnExec)
        {
            var state = new State()
            {
                OnEnter = OnEnter,
                OnExit = OnExit,
                OnExec = OnExec
            };
            _states.Add(state);
        }

        public void NewTransition(State src, State dest, Func<bool> Trig, Action Guard)
        {
            var transition = new Transition()
            {
                src = src,
                dest = dest,
                Trig = Trig,
                Guard = Guard
            };
            _transitions.Add(transition);
        }

        public void Init(State initState)
        {
            _currState = initState;
        }

        public void Step()
        {
            foreach (var transition in _transitions)
            {
                if (_currState != transition.src)
                {
                    continue;
                }
                
                if (transition.Trig())
                {
                    _currState = transition.dest;
                    _currState.OnExit?.Invoke();
                    transition.Guard?.Invoke();
                    _currState.OnEnter?.Invoke();
                }
            }

            _currState.OnExec?.Invoke();
        }
    }
}
