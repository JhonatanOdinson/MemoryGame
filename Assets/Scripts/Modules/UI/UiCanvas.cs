using System;
using System.Collections.Generic;
using System.Linq;
using Modules.ScriptableObjects;
using Modules.UI.Window;
using UnityEngine;

namespace Modules.UI
{
   public class UiCanvas : MonoBehaviour
   {
      [SerializeField] private Canvas _canvasRef;
      [SerializeField] private Transform _windowContainer;
      [SerializeField] private RectTransform _canvasRect;
      [SerializeField] private CanvasGroup _canvasGroup;

      [SerializeField] private List<WindowBase> _windowList = new();

      public void Init(IEnumerable<WindowData> windowList)
      {
         foreach (WindowData windowData in windowList)
         {
            var window = Instantiate(windowData.GetWindowRef.LoadFromPrefab(true).Result, _windowContainer);
            window.Init();
            _windowList.Add(window);
         }
      }

      public T GetWindow<T>() where T : WindowBase {
         return (T) _windowList.First(e => e.GetType() == typeof(T));
      }

      public void Destruct()
      {
         foreach (WindowBase windowBase in _windowList)
         {
            windowBase.Destruct();
         }
         _windowList.Clear();
      }
   }
}
