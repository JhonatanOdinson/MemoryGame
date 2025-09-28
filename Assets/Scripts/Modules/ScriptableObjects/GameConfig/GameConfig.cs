using UnityEngine;

namespace Modules.ScriptableObjects.GameConfig
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableData/Core/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _cardCount;
        [SerializeField] private string _jsonUrl;
        
        public int CardCount => _cardCount;
        public string JsonUrl => _jsonUrl;
    }
}
