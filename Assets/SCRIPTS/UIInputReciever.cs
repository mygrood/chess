using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class UIInputReciever:InputReciever
    {
        [SerializeField] private UnityEvent clickEvent;
        public override void OnInputRecieved()
        {
            foreach (var handler in inputHandlers)
            {
                handler.ProcessInput(Input.mousePosition,gameObject,()=>clickEvent.Invoke());
            }
        }
    }
}