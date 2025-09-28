using System.Collections.Generic;
using Modules.UI.Window.CardsWindow.CardItem;
using UnityEngine;

namespace Core
{
    public class GameProcessController : MonoBehaviour
    {
        [SerializeField] private List<CardData> _cardDataList = new();
        private int _sequenceSize;
        
        public void Init()
        {
            _sequenceSize = GameDirector.GetGameConfig.CardCount;
            string jsonUrl = GameDirector.GetGameConfig.JsonUrl;
            _cardDataList = CommonComponents.ResourceController.ParseFromJson<CardData>(jsonUrl);
            CreateSequence();
        }

        public List<CardData> CreateSequence()
        {
            List<CardData> _cardDataSequence = GetRandomCard(_cardDataList, _sequenceSize / 2);
            _cardDataSequence.AddRange(_cardDataSequence);
            _cardDataSequence = ShuffleSequence(_cardDataSequence);

            return _cardDataSequence;
        }

        private List<CardData> GetRandomCard(List<CardData> source, int count)
        {
            List<CardData> copy = new List<CardData>(source);
            copy = ShuffleSequence(copy);
            return copy.GetRange(0, count);
        }

        private List<CardData> ShuffleSequence(List<CardData> cardSequence)
        {
            for (int i = cardSequence.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (cardSequence[i], cardSequence[j]) = (cardSequence[j], cardSequence[i]);
            }

            return cardSequence;
        }

        public void Free()
        {
            _cardDataList.Clear();
        }
    }
}
