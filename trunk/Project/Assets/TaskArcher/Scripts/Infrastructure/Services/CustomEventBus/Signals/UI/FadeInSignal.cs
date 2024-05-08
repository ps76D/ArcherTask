namespace TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI
{
    public class FadeInSignal : Signal
    {
        public readonly float Duration;

        public FadeInSignal(float duration)
        {
            Duration = duration;
        }
    }
}