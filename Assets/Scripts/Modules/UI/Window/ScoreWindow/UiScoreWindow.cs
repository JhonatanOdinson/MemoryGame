using Modules.UI.Interface;
using UnityEngine;

namespace Modules.UI.Window.ScoreWindow
{
    public class UiScoreWindow : WindowBase
    {
        [SerializeField] private UiScoreProvider _provider;
        [SerializeField] private UiScoreHandler _handler;

        [SerializeField] private UiScoreText _scoreText;

        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
            _scoreText.Init();
            
        }
        public override void FreeWindow() { }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            _scoreText.UpdateScore(score);
        }
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override IWindowProvider GetProvider()
        {
            return _provider;
        }

        public override void Destruct()
        { 
            _scoreText.Destruct();
            _provider.Destruct();
            _handler.Destruct();
        }
        
    }
}
