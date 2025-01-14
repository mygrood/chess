using System;
using UnityEngine;

namespace Game
{
    public static class AnimationEventManager
    {
       
        public static event Action OnMoveComplete;
       
        public static void TriggerMoveComplete()
        {
            OnMoveComplete?.Invoke();
        }
    }
}