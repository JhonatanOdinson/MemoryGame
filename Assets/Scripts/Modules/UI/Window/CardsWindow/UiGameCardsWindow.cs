using System.Collections.Generic;
using Core;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Modules.Tool.UniversalPool;
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

        private ResourceController _resourceController;
        
        public override void Init()
        {
            _provider.Init(this);
            _handler.Init(this);

            _resourceController = CommonComponents.ResourceController;
            _cardPool.Initialize();
            FillGameField();
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public async void FillGameField()
        {
            List<CardData> sequence = CommonComponents.GameProcessController.CreateSequence();
            foreach (var cardData in sequence)
            {
                await CreateBar(cardData);
            }
        }

        public async UniTask CreateBar(CardData cardData) {
            UiGameCardItem cardItem = _cardPool.Take();
            cardItem.Init(cardData);
            cardItem.SetImage(await _resourceController.GetImage(cardData.CardId, cardData.Url));
            cardItem.Show(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public void KillTweens()
        {
            DOTween.Kill("ShowBg",true);
        }

        public override IWindowProvider GetProvider()
        {
            return _provider;
        }

        public override void FreeWindow()
        {
            _cardPool.Clear();
        }
        
        public override void Destruct()
        {
            FreeWindow();
            _provider.Destruct();
            _handler.Destruct();
            KillTweens();
        }
    }
}
