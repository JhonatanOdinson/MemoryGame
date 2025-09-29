using System.Collections.Generic;
using System.Linq;
using Core;
using Cysharp.Threading.Tasks;
using Modules.UI.Interface;
using Modules.UI.Window.CardsWindow.CardItem;
using UnityEngine;

namespace Modules.UI.Window.CardsWindow
{
    public class UiGameCardsProvider : MonoBehaviour, IWindowProvider
    {
        [SerializeField] private List<UiGameCardItem> _pairList = new();
        
        private UiGameCardsWindow _window;
        private ResourceController _resourceController;
        private int _delayAfterMistake;
        private int _delayAfterCorrect;
       
        public void Init(WindowBase window)
        {
            _window = (UiGameCardsWindow)window;
            
            _resourceController = CommonComponents.ResourceController;

            _delayAfterMistake = GameDirector.GetGameConfig.DelayAfterMistake;
            _delayAfterCorrect = GameDirector.GetGameConfig.DelayAfterCorrect;
            
            if (_window.Data.ShowOnStart)
                ShowWindow(true);
            else
                HideWindow(true);
        }

        public void AddPair(UiGameCardItem cardItem)
        {
            if(_pairList.Count < 2) _pairList.Add(cardItem);
            if(_pairList.Count == 2) CheckPair();
        }

        private void CheckPair()
        {
            bool pairIsValid = _pairList.First().Data.CardId == _pairList.Last().Data.CardId;
            
            if(pairIsValid)
                _pairList.ForEach(e => e.Fade(_delayAfterCorrect));
            else
                _pairList.ForEach(e =>
                {
                    e.SetInteractive(true);
                    e.Flip(_delayAfterMistake);
                });
            
            _window.OnPairFoundHandler(pairIsValid);
            _pairList.Clear();
        }

        public Sprite GetImage(CardData cardData)
        {
            return _resourceController.GetImage(cardData.CardId);
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
