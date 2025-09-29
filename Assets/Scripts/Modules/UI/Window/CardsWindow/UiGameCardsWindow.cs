using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Tools.UniversalPool;
using Modules.UI.Interface;
using Modules.UI.Window.CardsWindow.CardItem;
using UnityEngine;

namespace Modules.UI.Window.CardsWindow
{
    public class UiGameCardsWindow : WindowBase
    {
        [SerializeField] private UiGameCardsProvider _provider;
        [SerializeField] private UiGameCardsHandler _handler;
        [SerializeField] private UniversalPool<UiGameCardItem> _cardPool;

        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);
            _cardPool.Initialize();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void FillGameField(List<CardData> sequence)
        {
            FreeCardPool();
            foreach (var cardData in sequence)
            { 
                CreateCard(cardData);
            }
        }

        public void FlipAllCard()
        {
            foreach (var cardItem in _cardPool.GetBusy())
            {
                cardItem.Flip();
                cardItem.SetInteractive(true);
            }
        }

        public void CreateCard(CardData cardData) {
            UiGameCardItem cardItem = _cardPool.Take();
            cardItem.Init(cardData);
            cardItem.SetImage(_provider.GetImage(cardData));
            cardItem.Show(true);
            cardItem.SetInteractive(false);
            cardItem.OnCardSelected += OnCardSelectedHandler;
        }

        public void FreeCardPool()
        {
            List<UiGameCardItem> cardItems = _cardPool.GetBusy().ToList();
            for (int i = 0; i < cardItems.Count(); i++)
            {
                cardItems[i].OnCardSelected -= OnCardSelectedHandler;
                FreeСard(cardItems[i]);
            }
        }

        private void OnCardSelectedHandler(UiGameCardItem cardData)
        {
            cardData.SetInteractive(false);
            _provider.AddPair(cardData);
        }

        public void OnPairFoundHandler(bool pairIsValid)
        {
            if (pairIsValid)
            {
                _handler.OnPairFoundHandler();
            }
        }
        
        public void FreeСard(UiGameCardItem cardItem) {
            _cardPool.Return(cardItem);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override IWindowProvider GetProvider()
        {
            return _provider;
        }

        public override void FreeWindow()
        {
            FreeCardPool();
        }
        
        public override void Destruct()
        {
            FreeWindow();
            FreeCardPool();
            _provider.Destruct();
            _handler.Destruct();
        }
    }
}
