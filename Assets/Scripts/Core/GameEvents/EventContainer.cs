using System;

namespace Core.GameEvents {
  [Serializable]
  public class EventContainer {
    public Action<object> OnComplete;
    public object Reciever;

    public EventContainer( object reciever, Action<object> act) {
      Reciever = reciever;
      OnComplete = act;
    }
    
  }
}