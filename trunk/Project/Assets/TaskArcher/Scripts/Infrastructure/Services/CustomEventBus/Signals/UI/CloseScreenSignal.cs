using UnityEngine;

namespace TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI
{
    public class CloseScreenSignal : Signal
    {
        public readonly GameObject Value;

        public CloseScreenSignal(GameObject value)
        {
            Value = value;
        }
    }
}