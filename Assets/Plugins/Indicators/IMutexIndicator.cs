using System;

namespace GameBase.Indicators
{
    public interface IMutexIndicator
    {
        void Hide();
        Action OnShow { get; set; }
    }
}
