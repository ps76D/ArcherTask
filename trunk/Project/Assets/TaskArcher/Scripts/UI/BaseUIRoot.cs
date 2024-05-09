using TaskArcher.Infrastructure;
using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using TaskArcher.Scripts.UI;
using UnityEngine;

namespace TaskArcher.UI
{
    public class BaseUIRoot : MonoBehaviour
    {
        public Canvas baseCanvas;

        public HudPanel topPanel;
        public FadeInFadeOut fadeInFadeOut;

        public GameBootstrapper gameBootstrapper;
        private IEventBus _eventBus;

        private void InitBaseUIRoot()
        {
            gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            _eventBus = AllServices.Container.Single<IEventBus>();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);

            InitBaseUIRoot();
        }
    }
}
