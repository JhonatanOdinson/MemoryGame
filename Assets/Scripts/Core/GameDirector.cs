using Modules.ScriptableObjects.GameConfig;
using Modules.Tools;

namespace Core {
    public static class GameDirector {
        private static EnterPoint _enterPoint;
        private static GameConfig _gameConfig;

        public static EnterPoint GetEnterPoint => _enterPoint;
        public static GameConfig GetGameConfig => _gameConfig;

        static GameDirector() {
            LoadData();
        }

        private static void LoadData() {
            _gameConfig = LocalAssetLoader.LoadAsset<GameConfig>("GameConfig", true).Result;
        }
        
        public static void SetEnterPoint(EnterPoint enterPoint) {
            _enterPoint = enterPoint;
        }
    }
}