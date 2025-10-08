using System;

namespace GameBase.Triggers
{
    public interface ITriggerAction
    {
        void Effect(Trigger e, ITriggerTarget target);
    }
}
