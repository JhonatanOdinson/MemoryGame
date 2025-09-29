using System.Collections.Generic;
using Core.GameEvents;
using Cysharp.Threading.Tasks;
using Modules.UI.Window.CardsWindow.CardItem;
using UnityEngine;

namespace Core
{
    public class GameProcessController : MonoBehaviour
    {
        [SerializeField] private int _totalPairsCollected;
        [SerializeField] private List<CardData> _cardDataList = new();

        private int _sequenceSize;
        private int _cardShowTime;
        private int _delayAfterFinish;
        private int _sessionCollectedPairs;

        public GameEvent OnGamePrepared;
        public GameEvent OnGameStart;

        public GameEvent OnPairFounded;
        public GameEvent OnScoreUpdate;

        public void Init()
        {
            _sequenceSize = GameDirector.GetGameConfig.CardCount;
            _cardShowTime = GameDirector.GetGameConfig.CardShowTime;
            _delayAfterFinish = GameDirector.GetGameConfig.DelayAfterFinish;
            string jsonUrl = GameDirector.GetGameConfig.JsonUrl;
            
            _cardDataList = CommonComponents.ResourceController.ParseFromJson<CardData>(jsonUrl);
            Subscribes();
        }

        private void Subscribes()
        {
            GameDirector.GetEnterPoint.OnEnterPointInited += StartGame;
            OnPairFounded.Subscribe(this,OnPairFoundedHandler);
        }

        private void OnPairFoundedHandler(object obj)
        {
            _totalPairsCollected++;
            _sessionCollectedPairs++;
            
            OnScoreUpdate.Check(null,_totalPairsCollected);
            
            CheckRestart();
        }

        private async void CheckRestart()
        {
            int pairsToCollect = _sequenceSize / 2;
            if (_sessionCollectedPairs >= pairsToCollect)
            {
                _sessionCollectedPairs = 0;
                await UniTask.Delay(_delayAfterFinish * 1000); // Convert second to millisecond (second * 1000)
                StartGame();
            }
        }

        private void Unsubscribes()
        {
            GameDirector.GetEnterPoint.OnEnterPointInited -= StartGame;
            OnPairFounded.Unsubscribe(this,OnPairFoundedHandler);
        }

        private async void StartGame()
        {
            OnGamePrepared.Check(null,CreateSequence());
            await UniTask.Delay(_cardShowTime * 1000); // Convert second to millisecond (second * 1000)
            OnGameStart.Check(null, null);
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
            Unsubscribes();
            _cardDataList.Clear();
        }
    }
}
