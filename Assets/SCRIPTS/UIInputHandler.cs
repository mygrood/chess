using System;
using UnityEngine;

namespace Game
{
    public class UIInputHandler:MonoBehaviour,IInputHandler
    {
        public void ProcessInput(Vector3 inputPosition, GameObject selectedObject, Action callback)
        {
            callback?.Invoke();
        }
    }
}