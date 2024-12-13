using UnityEngine;

namespace Chess
{
    public class Board:MonoBehaviour
    {
        [SerializeField] private Transform bottomLeftSquareTransform;
        [SerializeField] private float squareSize;
        internal Vector3 CalculatePositionFromCoords(Vector2Int coords)
        {
            return bottomLeftSquareTransform.position + new Vector3(squareSize * coords.x, 0, squareSize * coords.y);
        }
    }
}