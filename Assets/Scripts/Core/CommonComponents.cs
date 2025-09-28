using System.Threading.Tasks;
using Library.Scripts.Core;
using Library.Scripts.Modules.Tools;
using Library.Scripts.Modules.Ui;
using Modules.Input;
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

        [SerializeField] private TouchInputController _touchInputController;
        
      
        [SerializeField] private GameStateController _gameStateController;

        public static TouchInputController TouchInputController => _instance._touchInputController;
        public static UiCanvas UiCanvas => _instance._uiCanvas;
        public static GameStateController GameStateController => _instance._gameStateController;

        public static void LoadInstance()
        {
            if (_instance != null) return;
            _instance = LocalAssetLoader.InstantiateAsset<CommonComponents>("CommonComponents", null, true).Result;
            DontDestroyOnLoad(_instance.gameObject);
        }

        public async Task Init(EnterPoint enterPoint)
        {
            _uiCanvas.Init(enterPoint.LoadWindowList);
        }

        public async Task LoadData()
        {
            await Task.WhenAll(
                //_objectPoolController.LoadData()
            );
        }

        public void InitGlobal()
        {
            //_gameStateController.Init();
            _touchInputController.Init();
        }

        public void FreeControllers()
        {
            _uiCanvas.Destruct();
            _touchInputController.Free();
        }
    }
}
