using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.CardsWindow
{
    public class UiGameCardsProvider : MonoBehaviour, IWindowProvider
    {
        private UiGameCardsWindow _window;
        public void Init(WindowBase window)
        {
            _window = (UiGameCardsWindow)window;
            
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
            
        }
    }
}
