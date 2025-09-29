using Core;
using Modules.UI.Interface;
using Modules.UI.Window.CardsWindow;
using Modules.UI.Window.ScoreWindow;
using UnityEngine;

namespace Modules.UI.Window.LoadingScreen
{
    public class UiLoadingScreenProvider : MonoBehaviour, IWindowProvider
    {
        private UiLoadingScreenWindow _window;
        public void Init(WindowBase windowBase)
        {
            _window = (UiLoadingScreenWindow)windowBase;
            
            if (_window.Data.ShowOnStart)
                ShowWindow(true);
            else
                HideWindow(true);
        }

        public void ShowWindow(object obj)
        {
            _window.Show();
        }

        public void HideWindow(object obj)
        {
            _window.Hide();
        }

        public void Destruct()
        {
            _window.FreeWindow();
        }

        public void LoadComplete()
        {
            CommonComponents.UiCanvas.GetWindow<UiGameCardsWindow>().GetProvider().ShowWindow(null);
            CommonComponents.UiCanvas.GetWindow<UiScoreWindow>().GetProvider().ShowWindow(null);
            _window.Hide();
        }
    }
}
