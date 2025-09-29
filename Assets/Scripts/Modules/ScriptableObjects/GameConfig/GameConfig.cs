using UnityEngine;

namespace Modules.ScriptableObjects.GameConfig
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableData/Core/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _cardCount;
        [SerializeField] private int _cardShowTime;
        [SerializeField] private int _delayAfterMistake;
        [SerializeField] private int _delayAfterCorrect;
        [SerializeField] private int _delayAfterFinish;
        [SerializeField] private string _jsonUrl;
        
        public int CardCount => _cardCount;
        public int CardShowTime => _cardShowTime;
        public string JsonUrl => _jsonUrl;
        public int DelayAfterMistake => _delayAfterMistake;
        public int DelayAfterCorrect => _delayAfterCorrect;
        public int DelayAfterFinish => _delayAfterFinish;
    }
}
