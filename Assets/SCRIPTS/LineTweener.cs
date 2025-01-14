using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

namespace Game
{
    public class LineTweener:MonoBehaviour,IObjectTweener
    {
        [SerializeField] private float speed;
        public void MoveTo(Transform transform, Vector3 targetPosition)
        {
            float distance = Vector3.Distance(targetPosition, transform.position);
            transform.DOMove(targetPosition, distance/speed);
        }
    }
}