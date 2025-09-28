using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Modules.Input
{
    public class TouchInputController : MonoBehaviour
    {
        private Controls _controls;

        public event Action OnClickPerformed;
        public event Action OnClickCancel;

        public void Init()
        {
            _controls = new Controls();
            _controls.Enable();
            Subscribe();
        }

        private void Subscribe()
        {
            _controls.UI.Click.performed += OnClickPerformedHandler;
            _controls.UI.Click.canceled += OnClickCancelHandler;
        }

        private void Unsubscribe()
        {
            _controls.UI.Click.performed -= OnClickPerformedHandler; 
            _controls.UI.Click.canceled -= OnClickCancelHandler;
        }

        private void OnClickCancelHandler(InputAction.CallbackContext obj)
        {
            OnClickCancel?.Invoke();
        }

        private void OnClickPerformedHandler(InputAction.CallbackContext obj)
        {
            OnClickPerformed?.Invoke();
        }

        public void Free()
        {
            Unsubscribe();
        }
    }
}
