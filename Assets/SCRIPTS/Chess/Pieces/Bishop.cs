using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class Bishop: Piece
    {
        public override List<Vector2Int> SelectAvailableSquares()
        {
            availableMoves.Clear();
            availableMoves.Add(occupiedSquare+new Vector2Int(0, 1));
            return availableMoves;
        }
    }
}