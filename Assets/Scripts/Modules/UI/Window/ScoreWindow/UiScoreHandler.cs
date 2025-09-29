using Core.GameEvents;
using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.ScoreWindow
{
    public class UiScoreHandler : MonoBehaviour, IWindowHandler
    {
        private UiScoreWindow _window;

        public GameEvent OnScoreUpdate;
        
        public void Init(WindowBase windowBase)
        {
            _window = (UiScoreWindow)windowBase;
            Subscribe();
        }

        private void Subscribe()
        {
            OnScoreUpdate.Subscribe(this, UpdateScoreHandler);
        }

        private void Unsubscribe()
        {
            OnScoreUpdate.Unsubscribe(this, UpdateScoreHandler);
        }

        private void UpdateScoreHandler(object scoreCount)
        {
            if (scoreCount is int scoreValue)
                _window.UpdateScore(scoreValue);
        }

        public void Destruct()
        {
            Unsubscribe();
        }
    }
}
