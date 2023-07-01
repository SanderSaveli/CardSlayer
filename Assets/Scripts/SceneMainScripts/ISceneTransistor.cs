using System;

public interface ISceneTransistor
{
    public bool isShowen { get; }
    public bool isAnimationPlaying { get; }

    public void EnterTheTransition(Action callback = null);
    public void ExitTheTransition(Action callback = null);
}
