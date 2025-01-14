using UnityEngine;
using DG.Tweening;
namespace Game
{
    public class ArcTweener:MonoBehaviour,IObjectTweener
    {
        [SerializeField] private float speed;
        [SerializeField] private float height;
        public void MoveTo(Transform transform, Vector3 targetPosition)
        {
            float distance = Vector3.Distance(transform.position, targetPosition);
            transform.DOJump(targetPosition, height,1, distance/speed);
        }
    }
}