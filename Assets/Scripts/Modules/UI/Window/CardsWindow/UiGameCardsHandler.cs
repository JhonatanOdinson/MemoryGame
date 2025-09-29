using System.Collections.Generic;
using Core;
using Core.GameEvents;
using Modules.UI.Interface;
using Modules.UI.Window.CardsWindow.CardItem;
using UnityEngine;

namespace Modules.UI.Window.CardsWindow
{
    public class UiGameCardsHandler : MonoBehaviour, IWindowHandler
    {
        private UiGameCardsWindow _window;

        public GameEvent OnGamePrepared;
        public GameEvent OnGameStart;
        public GameEvent OnPairFound;
        
        
        public void Init(WindowBase window)
        {
            _window = (UiGameCardsWindow)window;
            Subscribes();
        }

        private void Subscribes()
        {
            OnGamePrepared.Subscribe(this,OnGamePreparedHandler);
            OnGameStart.Subscribe(this,OnGameStartedHandler);
        }

        public void OnPairFoundHandler()
        {
           OnPairFound.Check(null, null);
        }

        private void OnGameStartedHandler(object obj)
        {
            _window.FlipAllCard();
        }

        private void OnGamePreparedHandler(object sequence)
        {
            if (sequence is List<CardData> cardDataSequence)
                _window.FillGameField(cardDataSequence);
        }

        private void Unsubscribes()
        {
            OnGamePrepared.Unsubscribe(this,OnGamePreparedHandler);
            OnGameStart.Unsubscribe(this,OnGameStartedHandler);
        }

        public void Destruct()
        {
            Unsubscribes();
        }
    }
}
