using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.CardsWindow
{
    public class UiGameCardsHandler : MonoBehaviour, IWindowHandler
    {
        private UiGameCardsWindow _window;
        public void Init(WindowBase window)
        {
            _window = (UiGameCardsWindow)window;
        }

        public void Destruct()
        {
            throw new System.NotImplementedException();
        }
    }
}
