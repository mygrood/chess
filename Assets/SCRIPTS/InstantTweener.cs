using System;
using UnityEngine;

namespace Game
{
    public class InstantTweener:MonoBehaviour,IObjectTweener
    {
        public void MoveTo(Transform transform, Vector3 targetPosition)
        {
            transform.position = targetPosition;
        }
        
    }
}