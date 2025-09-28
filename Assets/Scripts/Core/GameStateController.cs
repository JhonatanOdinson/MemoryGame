using System;
using Core.GameEvents;

using UnityEngine;

namespace Core
{
    public class GameStateController : MonoBehaviour
    {
        public enum GameStateE {
            None = 0,
            Idle = 1,
            Play = 2,
        }
        private GameStateE _currentState = GameStateE.None;
        public Action<GameStateE> OnStateChange;
        public GameEvent onGameEventStateChange;

        public GameStateE CurrentState => _currentState;

        public void Init() {
      
        }
        
        public void ChangeState(GameStateE state) {
            if(_currentState == state) return;
            _currentState = state;

            OnStateChange?.Invoke(state);
            onGameEventStateChange?.Check(null,state);
        }
    }
}
