using DG.Tweening;
using Game;
using UnityEngine;

namespace Chess
{
    public class ChessCameraController:MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField] private Camera gameCamera;  
       
        [Header("Movement Settings")]
        [SerializeField] private float rotateDuration = 1f;  
        [SerializeField] private float resetDuration = 1f; 
        
        private Vector3 basePosition;
        private Quaternion baseRotation;
        
        private void OnEnable()
        {
            AnimationEventManager.OnMoveComplete += RotateCamera;
        }

        private void OnDisable()
        {
            AnimationEventManager.OnMoveComplete -= RotateCamera;
        }

        
        void Start()
        {
            if (gameCamera != null)
            {
                basePosition = gameCamera.transform.position;
                baseRotation = gameCamera.transform.rotation;
            }
            else
            {
                Debug.LogWarning("GameCamera is not assigned!");
            }
        }

       
        public void RotateCamera()
        {
            //gameCamera.transform.position = new Vector3(gameCamera.transform.position.x, gameCamera.transform.position.y, -gameCamera.transform.position.z);
            // gameCamera.transform.Rotate(0, 180, 0, Space.World);
            float currentXRotation = gameCamera.transform.rotation.eulerAngles.x;
            float currentYRotation = gameCamera.transform.rotation.eulerAngles.y;

            gameCamera.transform.DOMove(new Vector3(gameCamera.transform.position.x, gameCamera.transform.position.y, -gameCamera.transform.position.z), rotateDuration);
            gameCamera.transform.DORotate(new Vector3(currentXRotation, currentYRotation + 180, 0), rotateDuration, RotateMode.FastBeyond360);
        }

       
        public void ResetCamera()
        {
            gameCamera.transform.DOMove(basePosition, resetDuration);
            gameCamera.transform.DORotate(baseRotation.eulerAngles, resetDuration, RotateMode.FastBeyond360);
        }
    }
}