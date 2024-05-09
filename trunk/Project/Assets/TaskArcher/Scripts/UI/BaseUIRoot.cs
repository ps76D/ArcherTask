using TaskArcher.Infrastructure;
using TaskArcher.Infrastructure.Services;
using TaskArcher.Infrastructure.Services.CustomEventBus;
using UnityEngine;

namespace TaskArcher.UI
{
    public class BaseUIRoot : MonoBehaviour
    {
        public Canvas baseCanvas;

        public HudPanel topPanel;
        public InGameScreen inGameScreen;
        
        public FadeInFadeOut fadeInFadeOut;

        public GameBootstrapper GameBootstrapper {
            get;
            private set;
        }
        
        private IEventBus _eventBus;

        private void InitBaseUIRoot()
        {
            GameBootstrapper = FindObjectOfType<GameBootstrapper>();
            _eventBus = AllServices.Container.Single<IEventBus>();
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(this);

            InitBaseUIRoot();
        }
    }
}
