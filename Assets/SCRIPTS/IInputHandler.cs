using System;
using UnityEngine;

namespace Game
{
    public interface IInputHandler
    {
        void ProcessInput(Vector3 inputPosition,GameObject selectedObject,Action callback);
    }
}