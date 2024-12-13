using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class Knight: Piece
    {
        private Vector2Int[] offsets = new Vector2Int[]
        {
            new Vector2Int(-1, 2),
            new Vector2Int(1, 2),
            new Vector2Int(2,1),
            new Vector2Int(2,-1),
            new Vector2Int(1,-2),
            new Vector2Int(-1,-2),
            new Vector2Int(-2,-1),
            new Vector2Int(-2,1),
        };
        
        public override List<Vector2Int> SelectAvailableSquares()
        {
            availableMoves.Clear();
            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2Int nextCoords = occupiedSquare + offsets[i];
                Piece piece = board.GetPieceOnSquare(nextCoords);
                if(!board.CheckIfCoordinatesAreOnBoard(nextCoords))
                    continue;
                if(piece == null||!piece.IsFromSameTeam(this))
                    TryToAddMove(nextCoords);
            }
            return availableMoves; 
        }
    }
}