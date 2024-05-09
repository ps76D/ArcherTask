using DG.Tweening;
using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI;
using UnityEngine;

namespace TaskArcher.UI
{
    public class FadeInFadeOut: MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        private Tween _fadeTween;

        private IEventBus _eventBus;

        private void InitFadeInFadeOut()
        {
            _eventBus = AllServices.Container.Single<IEventBus>();
        }

        private void Awake()
        {
            InitFadeInFadeOut();
            
            _eventBus.Subscribe<FadeInSignal>(StartFadeIn);
            _eventBus.Subscribe<FadeOutSignal>(StartFadeOut);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<FadeInSignal>(StartFadeIn);
            _eventBus.Unsubscribe<FadeOutSignal>(StartFadeOut);
        }


        private void StartFadeIn(FadeInSignal signal)
        {
            FadeIn(signal.Duration);
        }

        private void StartFadeOut(FadeOutSignal signal)
        {
            FadeOut(signal.Duration);
        }

        public void FadeIn(float duration)
        {
            Fade(1f, duration, () =>
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            });
        }

        public void FadeOut(float duration)
        {
            Fade(0f, duration, () =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }

        private void Fade(float endValue, float duration, TweenCallback onEnd)
        {
            if (_fadeTween != null)
            {
                _fadeTween.Kill(false);
            }

            _fadeTween = canvasGroup.DOFade(endValue, duration);
            _fadeTween.onComplete += onEnd;
        }
    }
}