using UnityEngine;

namespace Game
{
    public interface IObjectTweener
    {
        void MoveTo(Transform transform,Vector3 targetPosition);
    }
}