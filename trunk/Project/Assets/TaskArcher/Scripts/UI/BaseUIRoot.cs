using System.Collections;
using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using TaskArcher.Infrastructure.Services.CustomEventBus.Signals.UI;
using TaskArcher.Scripts.UI;
using UnityEngine;

namespace TaskArcher.UI
{
    public class BaseUIRoot : MonoBehaviour
    {
        public Canvas baseCanvas;

        public HudPanel topPanel;
        public FadeInFadeOut fadeInFadeOut;

        private IEventBus _eventBus;

        private void InitBaseUIRoot()
        {
            _eventBus = AllServices.Container.Single<IEventBus>();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);

            InitBaseUIRoot();
        }
        
        private IEnumerator FadeInFadeOutCoroutine()
        {
            fadeInFadeOut.FadeIn(0.5f);
            yield return new WaitForSeconds(0.5f);
            fadeInFadeOut.FadeOut(0.5f);
            yield return new WaitForSeconds(0.5f);
        }

        private void CloseScreen(CloseScreenSignal signal)
        {
            SetScreenInactive(signal.Value);
        }
        
        private void SetScreenInactive(GameObject screen)
        {
            screen.SetActive(false);
        }
    }
}
