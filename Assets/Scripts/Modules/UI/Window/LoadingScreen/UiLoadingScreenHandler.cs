using Core.GameEvents;
using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.LoadingScreen
{
    public class UiLoadingScreenHandler : MonoBehaviour, IWindowHandler
    {
        private UiLoadingScreenWindow _window;
        
        [SerializeField] private GameEvent OnCacheStart;
        [SerializeField] private GameEvent OnCacheUpdated;
        [SerializeField] private GameEvent OnCacheComplete;
        
        public void Init(WindowBase windowBase)
        {
            _window = (UiLoadingScreenWindow)windowBase;
            Subscribe();
        }

        private void Subscribe()
        {
            OnCacheStart.Subscribe(this,OnCacheStartHandler);
            OnCacheUpdated.Subscribe(this,OnCacheUpdatedHandler);
            OnCacheComplete.Subscribe(this,OnCacheCompleteHandler);
        }

        private void OnCacheUpdatedHandler(object obj)
        {
            _window.UpdateBar();
        }

        private void OnCacheCompleteHandler(object obj)
        {
            _window.LoadComplete();
        }

        private void OnCacheStartHandler(object targetLoaded)
        {
            if(targetLoaded is int targetCount)
                _window.SetTargetLoad(targetCount);
        }

        private void Unsubscribe()
        {
            OnCacheStart.Unsubscribe(this,OnCacheStartHandler);
            OnCacheUpdated.Unsubscribe(this,OnCacheUpdatedHandler);
            OnCacheComplete.Unsubscribe(this,OnCacheCompleteHandler);
        }

        public void Destruct()
        {
            Unsubscribe();
        }
    }
}
