namespace TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI
{
    public class FadeOutSignal : Signal
    {
        public readonly float Duration;

        public FadeOutSignal(float duration)
        {
            Duration = duration;
        }
    }
}