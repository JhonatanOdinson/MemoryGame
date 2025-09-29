using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.ScoreWindow
{
    public class UiScoreProvider : MonoBehaviour, IWindowProvider
    {
        private UiScoreWindow _window;
        
        public void Init(WindowBase windowBase)
        {
            _window = (UiScoreWindow)windowBase;
        }

        public void ShowWindow(object obj)
        {
            _window.Show();
        }

        public void HideWindow(object obj)
        {
            _window.Hide();
        }

        public void Destruct() { }
    }
}
