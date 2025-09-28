using System;
using UnityEngine;

namespace Modules.UI.Window.CardsWindow.CardItem
{
    [Serializable]
    public class CardData
    {
        [SerializeField] private string _cardId;
        [SerializeField] private string _url;

        public CardData(){}

        public CardData(string cardId, string url)
        {
            _cardId = cardId;
            _url = url;
        }

        public string CardId
        {
            get => _cardId;
            set => _cardId = value;
        }

        public string Url
        {
            get => _url;
            set => _url = value;
        }
    }
}
