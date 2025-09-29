using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.LoadingScreen
{
    public class UiLoadingScreenWindow : WindowBase
    {
        [SerializeField] private UiLoadingScreenProvider _provider;
        [SerializeField] private UiLoadingScreenHandler _handler;
        [SerializeField] private UiLoadBar _loadBar;
        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
            _loadBar.Init();
        }

        public void SetTargetLoad(int targetLoaded)
        {
            _loadBar.SetTarget(targetLoaded);
        }

        public void UpdateBar()
        {
            _loadBar.UpdateBar();
        }

        public void LoadComplete()
        {
            _provider.LoadComplete();
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            FreeWindow();
            gameObject.SetActive(false);
        }

        public override IWindowProvider GetProvider()
        {
            return _provider;
        }

        public override void FreeWindow()
        {
            _handler.Destruct();
        }

        public override void Destruct()
        {
            _provider.Destruct();
            _handler.Destruct();
        }
        
    }
}
