using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Modules.Tools;
using Modules.UI;
using UnityEngine;

namespace Core
{
    public class CommonComponents : MonoBehaviour
    {
        #region Instance

        private static CommonComponents _instance;

        public static CommonComponents Instance
        {
            get
            {
                if (_instance == null)
                {
                    LoadInstance();
                }

                return _instance;
            }

            set { _instance = value; }
        }

        #endregion

        [SerializeField] private UiCanvas _uiCanvas;
        [SerializeField] private GameProcessController _gameProcessController;
        [SerializeField] private ResourceController _resourceController;
        
        public static UiCanvas UiCanvas => _instance._uiCanvas;
        public static GameProcessController GameProcessController => _instance._gameProcessController;
        public static ResourceController ResourceController => _instance._resourceController;

        public static void LoadInstance()
        {
            if (_instance != null) return;
            _instance = LocalAssetLoader.InstantiateAsset<CommonComponents>("CommonComponents", null, true).Result;
            DontDestroyOnLoad(_instance.gameObject);
        }

        public async UniTask Init(EnterPoint enterPoint)
        {
            _uiCanvas.Init(enterPoint.LoadWindowList);
            await _resourceController.Init();
            _gameProcessController.Init();
        }

        public async Task LoadData()
        {
            await Task.WhenAll(
                    
            );
        }

        public void FreeControllers()
        {
            _uiCanvas.Destruct();
            _gameProcessController.Free();
            _resourceController.Free();
        }
    }
}
