using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Library.Scripts.Modules.Ui;
using UnityEngine;

namespace Core {
  public class EnterPoint : MonoBehaviour {
    private static bool _isInitGC = false;
    private bool _initProcess = false;
    private bool _destructProcess;
    [SerializeField] private List<WindowData> _loadWindowList = new List<WindowData>();
    public Action OnEnterPointInited;
    
    public IEnumerable<WindowData> LoadWindowList => _loadWindowList;

    async void Start()
    {
      Application.targetFrameRate = 60;
      if(_isInitGC) return;
      await Init();
    }

    public async UniTask Init() {
      if(_initProcess) return;
      _initProcess = true;
      GameDirector.SetEnterPoint(this);
      if (!_isInitGC)
        await InitGlobalControllers();
      await CommonComponents.Instance.Init(this);
      OnEnterPointInited?.Invoke();
    }

    private async UniTask InitGlobalControllers() {
      CommonComponents.LoadInstance();
      await CommonComponents.Instance.LoadData();
      _isInitGC = true;
    }
    

    public void OnApplicationQuit() {
      if(!Application.isEditor) return;
      Destruct(); //call unsubscribe
    }

    private void Destruct() {
      if(_destructProcess) return;
      _destructProcess = true;
      StopAllCoroutines();
      CommonComponents.Instance.FreeControllers();
    }
  }
}
